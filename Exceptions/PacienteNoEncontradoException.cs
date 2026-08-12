namespace SistemaGestionPacientes.Exceptions;

/// <summary>
/// Excepción personalizada utilizada cuando no existe el paciente solicitado.
/// </summary>
public class PacienteNoEncontradoException : Exception
{
    public PacienteNoEncontradoException()
    {
    }

    public PacienteNoEncontradoException(string mensaje) : base(mensaje)
    {
    }

    public PacienteNoEncontradoException(string mensaje, Exception innerException)
        : base(mensaje, innerException)
    {
    }
}
