using SistemaGestionPacientes.Exceptions;
using SistemaGestionPacientes.Models;

namespace SistemaGestionPacientes.Services;

/// <summary>
/// Contiene la lógica de negocio y las operaciones CRUD.
/// La lista funciona como base de datos temporal mientras la aplicación está abierta.
/// </summary>
public class GestorPacientes
{
    private readonly List<Paciente> pacientes = new();

    public IReadOnlyList<Paciente> ListarTodos()
    {
        return pacientes.AsReadOnly();
    }

    public void Registrar(Paciente paciente)
    {
        ValidarPaciente(paciente);

        if (ExisteId(paciente.Id))
        {
            throw new InvalidOperationException(
                $"Ya existe un paciente registrado con el ID '{paciente.Id}'.");
        }

        pacientes.Add(paciente);
    }

    public Paciente BuscarPorId(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException("Debe indicar un ID para realizar la búsqueda.");
        }

        Paciente? paciente = pacientes.FirstOrDefault(
            p => p.Id.Equals(id.Trim(), StringComparison.OrdinalIgnoreCase));

        if (paciente is null)
        {
            throw new PacienteNoEncontradoException(
                $"No se encontró ningún paciente con el ID '{id}'.");
        }

        return paciente;
    }

    public List<Paciente> BuscarPorIdONombre(string criterio)
    {
        if (string.IsNullOrWhiteSpace(criterio))
        {
            throw new ArgumentException("Debe escribir un ID o nombre para buscar.");
        }

        string texto = criterio.Trim();

        return pacientes
            .Where(p =>
                p.Id.Contains(texto, StringComparison.OrdinalIgnoreCase) ||
                p.NombreCompleto.Contains(texto, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public void Actualizar(string idOriginal, Paciente datosActualizados)
    {
        ValidarPaciente(datosActualizados);

        Paciente pacienteExistente = BuscarPorId(idOriginal);

        bool nuevoIdPerteneceAOtroPaciente = pacientes.Any(p =>
            !ReferenceEquals(p, pacienteExistente) &&
            p.Id.Equals(datosActualizados.Id, StringComparison.OrdinalIgnoreCase));

        if (nuevoIdPerteneceAOtroPaciente)
        {
            throw new InvalidOperationException(
                $"El ID '{datosActualizados.Id}' ya pertenece a otro paciente.");
        }

        pacienteExistente.Id = datosActualizados.Id.Trim();
        pacienteExistente.NombreCompleto = datosActualizados.NombreCompleto.Trim();
        pacienteExistente.Edad = datosActualizados.Edad;
        pacienteExistente.Sexo = datosActualizados.Sexo;
        pacienteExistente.Diagnostico = datosActualizados.Diagnostico.Trim();
        pacienteExistente.Estado = datosActualizados.Estado;
        pacienteExistente.FechaIngreso = datosActualizados.FechaIngreso;
    }

    public void Eliminar(string id)
    {
        Paciente paciente = BuscarPorId(id);
        pacientes.Remove(paciente);
    }

    public bool ExisteId(string id)
    {
        return pacientes.Any(p =>
            p.Id.Equals(id.Trim(), StringComparison.OrdinalIgnoreCase));
    }

    private static void ValidarPaciente(Paciente paciente)
    {
        if (paciente is null)
        {
            throw new ArgumentNullException(nameof(paciente));
        }

        if (string.IsNullOrWhiteSpace(paciente.Id))
        {
            throw new ArgumentException("El ID o cédula es obligatorio.");
        }

        if (string.IsNullOrWhiteSpace(paciente.NombreCompleto))
        {
            throw new ArgumentException("El nombre completo es obligatorio.");
        }

        if (paciente.Edad < 0 || paciente.Edad > 120)
        {
            throw new ArgumentOutOfRangeException(
                nameof(paciente.Edad),
                "La edad debe estar entre 0 y 120 años.");
        }

        if (string.IsNullOrWhiteSpace(paciente.Diagnostico))
        {
            throw new ArgumentException("El diagnóstico es obligatorio.");
        }

        if (paciente.FechaIngreso.Date > DateTime.Today)
        {
            throw new ArgumentException(
                "La fecha de ingreso no puede ser posterior a la fecha actual.");
        }
    }
}
