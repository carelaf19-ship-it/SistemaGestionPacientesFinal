namespace SistemaGestionPacientes.Services;

/// <summary>
/// Mantiene una sola instancia del gestor para que todos los formularios
/// trabajen con la misma lista de pacientes.
/// </summary>
public static class AppData
{
    public static GestorPacientes Gestor { get; } = new();
}
