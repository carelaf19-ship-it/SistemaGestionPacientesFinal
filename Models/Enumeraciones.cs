namespace SistemaGestionPacientes.Models;

/// <summary>
/// Valores permitidos para el sexo del paciente.
/// </summary>
public enum Sexo
{
    Masculino,
    Femenino
}

/// <summary>
/// Estados permitidos dentro del centro de salud.
/// </summary>
public enum EstadoPaciente
{
    Ingresado,
    EnObservacion,
    DeAlta,
    Hospitalizado
}
