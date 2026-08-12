# Sistema de Gestión de Pacientes

Caso práctico de **Programación Básica** desarrollado en **C# con Windows Forms (.NET 8)** para la **Universidad Central del Este (UCE)**.

El sistema simula la gestión de pacientes de un centro de salud y permite realizar las operaciones CRUD: **registrar, listar/consultar, buscar, actualizar y eliminar**. Los datos se almacenan temporalmente en memoria mediante una lista dinámica `List<Paciente>`.

## Integrantes

> Completar los datos pendientes antes de entregar el PDF final al profesor.

- **Integrante 1:** ______________________________
- **Matrícula:** _________________________________
- **Integrante 2:** ______________________________
- **Matrícula:** _________________________________

**Universidad:** Universidad Central del Este (UCE)  
**Asignatura:** Programación Básica  
**Actividad:** Caso Práctico – Sistema de Gestión de Pacientes  
**Docente:** Gamalier Reyes del Carmen  
**Sección:** _________________________________

## Descripción breve

La aplicación permite gestionar pacientes mediante una interfaz gráfica Windows Forms. Cada paciente es representado por un objeto de la clase `Paciente`. La lógica de validación y las operaciones sobre la lista se encuentran separadas en la clase `GestorPacientes`, manteniendo los formularios enfocados en la interacción con el usuario.

## Datos de entrada

El sistema recibe mediante `TextBox`, `ComboBox` y `DateTimePicker`:

- ID o cédula.
- Nombre completo.
- Edad.
- Sexo.
- Diagnóstico.
- Estado del paciente.
- Fecha de ingreso.

## Datos que procesa

El programa realiza:

- Validación de campos obligatorios.
- Validación de edad numérica y rango permitido.
- Validación de ID no duplicado.
- Validación de fecha de ingreso.
- Búsqueda por ID o nombre.
- Registro, actualización y eliminación sobre `List<Paciente>`.
- Confirmación antes de eliminar.
- Manejo de errores mediante `try/catch/finally`.
- Excepción personalizada `PacienteNoEncontradoException`.

## Datos de salida

La información se presenta mediante:

- `DataGridView` para listados y resultados de búsqueda.
- `MessageBox` de información y confirmación.
- Mensajes de advertencia y error cuando los datos no son válidos.

## Enumeraciones utilizadas

Se utilizan dos enumeraciones para representar valores predefinidos y evitar texto libre:

```csharp
public enum Sexo
{
    Masculino,
    Femenino
}

public enum EstadoPaciente
{
    Ingresado,
    EnObservacion,
    DeAlta,
    Hospitalizado
}
```

Estas enumeraciones son mostradas en la interfaz mediante controles `ComboBox`.

## Funcionalidades CRUD

### Crear — Registrar paciente

Permite registrar un paciente validando que los campos sean correctos y que el ID no exista previamente.

### Leer — Listar y buscar

Muestra los pacientes registrados en un `DataGridView` y permite buscar por ID o nombre.

### Actualizar — Modificar paciente

Localiza un paciente por su ID, carga sus datos y permite modificarlos después de aplicar nuevamente las validaciones.

### Eliminar — Dar de baja

Localiza al paciente por ID y solicita confirmación con `MessageBox` antes de eliminarlo de la lista.

## Organización del proyecto

```text
SistemaGestionPacientesFinal/
├── Exceptions/
│   └── PacienteNoEncontradoException.cs
├── Forms/
│   ├── FrmActualizarPaciente.cs
│   ├── FrmBuscarPaciente.cs
│   ├── FrmEliminarPaciente.cs
│   ├── FrmListarPacientes.cs
│   ├── FrmPrincipal.cs
│   └── FrmRegistrarPaciente.cs
├── Models/
│   ├── Enumeraciones.cs
│   └── Paciente.cs
├── Services/
│   ├── AppData.cs
│   └── GestorPacientes.cs
├── docs/
│   └── capturas/
├── .gitignore
├── Program.cs
├── SistemaGestionPacientes.csproj
├── SistemaGestionPacientes.sln
└── README.md
```

### Responsabilidades

- `Paciente.cs`: modelo de datos del paciente.
- `Enumeraciones.cs`: contiene `Sexo` y `EstadoPaciente`.
- `GestorPacientes.cs`: lógica CRUD, búsquedas y validaciones sobre `List<Paciente>`.
- `AppData.cs`: comparte una misma instancia del gestor entre los formularios.
- `PacienteNoEncontradoException.cs`: excepción personalizada.
- `Forms/`: interfaz gráfica y eventos de interacción con el usuario.

## Evidencias y capturas de pantalla

Las siguientes evidencias corresponden a las capturas suministradas después de probar el sistema.

### Evidencia 1 — Menú, registro y validaciones

Incluye:

1. Menú principal.
2. Formulario para registrar paciente.
3. Registro exitoso.
4. Validación de campo obligatorio.
5. Validación de edad incorrecta.
6. Validación de ID duplicado.

![Evidencias de menú, registro y validaciones](docs/capturas/evidencias-01-registro-validaciones.webp)

### Evidencia 2 — Listado, búsqueda, actualización y eliminación

Incluye:

7. Listado de pacientes en `DataGridView`.
8. Búsqueda por ID.
9. Búsqueda por nombre.
10. Paciente/ID inexistente.
11. Pregunta para repetir el registro.
12. Formulario de actualización.
13. Actualización exitosa.
14. Formulario de eliminación.
15. Confirmación antes de eliminar.
16. Eliminación exitosa.
17. Listado después de eliminar.
18. Pregunta para repetir una operación de actualización o eliminación.

![Evidencias del CRUD completo](docs/capturas/evidencias-02-crud-completo.webp)

## Matriz de cumplimiento

| Requisito | Implementación |
|---|---|
| C# Windows Forms | `SistemaGestionPacientes.csproj` |
| Lista dinámica `List<T>` | `Services/GestorPacientes.cs` |
| Clase modelo | `Models/Paciente.cs` |
| Clase de gestión | `Services/GestorPacientes.cs` |
| Dos enumeraciones | `Models/Enumeraciones.cs` |
| Crear | Método `Registrar` |
| Leer/listar | Método `ListarTodos` |
| Buscar | Métodos `BuscarPorId` y `BuscarPorIdONombre` |
| Actualizar | Método `Actualizar` |
| Eliminar | Método `Eliminar` |
| ID no duplicado | Método `ExisteId` y validaciones del gestor |
| `try/catch/finally` | Formularios CRUD |
| Excepción personalizada | `PacienteNoEncontradoException` |
| `DataGridView` | Formularios de listado y búsqueda |
| `MessageBox` | Confirmaciones, información, advertencias y errores |
| Menú principal | `FrmPrincipal.cs` |
| Evidencias de ejecución | `docs/capturas/` |

## Requisitos para ejecutar

- Windows 10 u 11.
- Visual Studio 2022 o posterior.
- Carga de trabajo **Desarrollo de escritorio de .NET**.
- .NET 8 SDK.

## Cómo ejecutar

1. Descargar o clonar el repositorio.
2. Abrir `SistemaGestionPacientes.sln` en Visual Studio.
3. Esperar a que cargue el proyecto.
4. Presionar `F5` o **Iniciar**.
5. Utilizar el menú principal para acceder a cada operación.
6. Para finalizar, elegir **Salir del sistema**.

## Casos de prueba realizados

- Registro exitoso.
- Campo obligatorio vacío.
- Edad incorrecta.
- ID duplicado.
- Listado de pacientes.
- Búsqueda por ID.
- Búsqueda por nombre.
- Paciente inexistente.
- Actualización exitosa.
- Confirmación de eliminación.
- Eliminación exitosa.
- Listado posterior a la eliminación.
- Repetición de operaciones mediante `MessageBox` Sí/No.

## Observación

La información se guarda únicamente durante la ejecución del programa. Al cerrar la aplicación, la lista en memoria se pierde intencionalmente, porque el proyecto exige utilizar `List<Paciente>` como almacenamiento temporal.