namespace SistemaGestionPacientes.Models;

/// <summary>
/// Modelo que representa a un paciente del centro de salud.
/// </summary>
public class Paciente
{
    public string Id { get; set; } = string.Empty;
    public string NombreCompleto { get; set; } = string.Empty;
    public int Edad { get; set; }
    public Sexo Sexo { get; set; }
    public string Diagnostico { get; set; } = string.Empty;
    public EstadoPaciente Estado { get; set; }
    public DateTime FechaIngreso { get; set; }

    public override string ToString()
    {
        return $"{Id} - {NombreCompleto}";
    }
}
