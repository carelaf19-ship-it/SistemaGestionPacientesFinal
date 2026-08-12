# Sistema de Gestión de Pacientes

Proyecto final de **Programación Básica** desarrollado en **C# con Windows Forms (.NET 8)** para la Universidad Central del Este (UCE).

La aplicación simula un sistema de gestión de pacientes para una clínica. Permite realizar las operaciones CRUD (**crear, leer, actualizar y eliminar**) utilizando una lista dinámica `List<Paciente>` como almacenamiento temporal en memoria.

## Integrantes

> Completar estos datos antes de la entrega final.

- **Integrante 1:** ______________________________
- **Matrícula:** _________________________________
- **Integrante 2:** ______________________________
- **Matrícula:** _________________________________

**Universidad:** Universidad Central del Este (UCE)  
**Asignatura:** Programación Básica  
**Actividad:** Caso Práctico – Sistema de Gestión de Pacientes  
**Docente:** Gamalier Reyes del Carmen  
**Sección:** _________________________________  
**Fecha:** 12 de agosto de 2026

## Descripción breve

El sistema permite registrar, listar, buscar, actualizar y eliminar pacientes desde una interfaz gráfica Windows Forms. Cada paciente se representa mediante un objeto de la clase `Paciente`, mientras que la lógica de negocio se concentra en la clase `GestorPacientes`.

La información se mantiene únicamente mientras la aplicación está en ejecución, ya que el proyecto utiliza `List<Paciente>` como base de datos temporal en memoria, tal como requiere la práctica.

## Datos de entrada

La aplicación recibe los siguientes datos mediante controles como `TextBox`, `ComboBox` y `DateTimePicker`:

- ID o cédula.
- Nombre completo.
- Edad.
- Sexo.
- Diagnóstico.
- Estado del paciente.
- Fecha de ingreso.

## Datos que procesa

El sistema realiza las siguientes operaciones y validaciones:

- Registro de pacientes.
- Validación de campos obligatorios.
- Conversión y validación numérica de la edad.
- Validación del rango de edad.
- Verificación de ID no duplicado.
- Validación de la fecha de ingreso.
- Búsqueda por ID o nombre.
- Actualización de datos de un paciente existente.
- Eliminación con confirmación previa.
- Manejo de excepciones mediante `try/catch/finally`.
- Manejo de paciente inexistente mediante la excepción personalizada `PacienteNoEncontradoException`.

## Datos de salida

Los resultados se presentan al usuario mediante:

- `DataGridView` para mostrar listados y resultados de búsqueda.
- `MessageBox` de información para operaciones exitosas.
- `MessageBox` de confirmación para eliminar pacientes y repetir operaciones.
- `MessageBox` de advertencia y error para datos inválidos o pacientes inexistentes.

## Enumeraciones utilizadas

El proyecto utiliza enumeraciones para evitar valores de texto libre en campos predefinidos:

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

Estas enumeraciones se cargan en controles `ComboBox` de los formularios.

## Funcionalidades CRUD

### Crear — Registrar paciente

Permite registrar un nuevo paciente después de validar los datos. El sistema impide registrar un ID que ya exista.

### Leer — Listar y buscar pacientes

Permite mostrar todos los pacientes en un `DataGridView` y realizar búsquedas por ID o nombre.

### Actualizar — Modificar paciente

Localiza un paciente por su ID, carga sus datos en el formulario y permite modificar la información validando nuevamente todos los campos.

### Eliminar — Dar de baja un paciente

Localiza un paciente por ID y solicita confirmación mediante `MessageBox` antes de eliminarlo de la lista.

## Arquitectura y organización

La aplicación está separada por responsabilidades:

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

### Responsabilidades principales

- **`Paciente`**: modelo con los datos de cada paciente.
- **`Enumeraciones`**: contiene `Sexo` y `EstadoPaciente`.
- **`GestorPacientes`**: lógica CRUD, validaciones y búsquedas sobre `List<Paciente>`.
- **`AppData`**: mantiene una instancia compartida del gestor para todos los formularios.
- **`PacienteNoEncontradoException`**: excepción personalizada para búsquedas de pacientes inexistentes.
- **Formularios**: se encargan principalmente de la interacción con el usuario.

## Requisitos para ejecutar

- Windows 10 u 11.
- Visual Studio 2022 o posterior.
- Carga de trabajo **Desarrollo de escritorio de .NET**.
- .NET 8 SDK.

## Cómo ejecutar el proyecto

1. Clonar o descargar este repositorio.
2. Abrir `SistemaGestionPacientes.sln` en Visual Studio.
3. Esperar a que Visual Studio cargue el proyecto.
4. Presionar `F5` o el botón **Iniciar**.
5. Utilizar el menú principal para seleccionar la operación deseada.
6. Elegir **Salir del sistema** para finalizar la aplicación.

## Evidencias y capturas de pantalla

Las siguientes imágenes muestran los formularios y las pruebas realizadas a las funcionalidades CRUD, validaciones y mensajes del sistema.

### 1. Menú principal

![Menú principal](docs/capturas/01-menu-principal.png)

### 2. Registro de paciente

![Formulario registrar](docs/capturas/02-formulario-registrar.png)

![Registro exitoso](docs/capturas/03-registro-exitoso.png)

![Pregunta para registrar otro paciente](docs/capturas/17-repetir-registro.png)

### 3. Validaciones

**Campo obligatorio:**

![Campo obligatorio](docs/capturas/04-campo-obligatorio.png)

**Edad incorrecta:**

![Validación de edad](docs/capturas/05-validacion-edad.png)

**ID duplicado:**

![ID duplicado](docs/capturas/06-id-duplicado.png)

### 4. Listado de pacientes

![Listado de pacientes](docs/capturas/07-listado-pacientes.png)

### 5. Búsqueda de pacientes

**Búsqueda por ID:**

![Búsqueda por ID](docs/capturas/08-busqueda-id.png)

**Búsqueda por nombre:**

![Búsqueda por nombre](docs/capturas/09-busqueda-nombre.png)

**Paciente inexistente:**

![Paciente no existe](docs/capturas/10-paciente-no-existe.png)

### 6. Actualización de pacientes

![Formulario actualizar](docs/capturas/11-formulario-actualizar.png)

![Actualización exitosa](docs/capturas/12-actualizacion-exitosa.png)

### 7. Eliminación de pacientes

![Formulario eliminar](docs/capturas/13-formulario-eliminar.png)

![Confirmación antes de eliminar](docs/capturas/14-confirmacion-eliminar.png)

![Eliminación exitosa](docs/capturas/15-eliminacion-exitosa.png)

![Listado después de eliminar](docs/capturas/16-listado-despues-eliminar.png)

![Pregunta para repetir operación](docs/capturas/18-repetir-actualizar-eliminar.png)

## Matriz de cumplimiento de requisitos

| Requisito | Implementación / evidencia |
|---|---|
| Windows Forms en C# | Proyecto `SistemaGestionPacientes.csproj` con `UseWindowsForms=true` |
| `List<T>` genérica | `Services/GestorPacientes.cs` usa `List<Paciente>` |
| Clase de modelo | `Models/Paciente.cs` |
| Clase de gestión | `Services/GestorPacientes.cs` |
| Dos enumeraciones | `Models/Enumeraciones.cs` |
| CRUD completo | Métodos `Registrar`, `ListarTodos`, `BuscarPorId`, `Actualizar` y `Eliminar` |
| Validaciones | `GestorPacientes.ValidarPaciente` y formularios |
| ID no duplicado | `ExisteId` y validación en registro/actualización |
| Manejo de excepciones | `try/catch/finally` en formularios |
| Excepción personalizada | `Exceptions/PacienteNoEncontradoException.cs` |
| DataGridView | Formularios de listado y búsqueda |
| Confirmación al eliminar | `FrmEliminarPaciente.cs` |
| Pregunta después de transacciones | Formularios CRUD mediante `MessageBox` Sí/No |
| Menú principal activo | `FrmPrincipal.cs` |
| Capturas de pantalla | Carpeta `docs/capturas/` |
| README completo | Este archivo |

## Casos de prueba evidenciados

1. Registro correcto de un paciente.
2. Campo obligatorio vacío.
3. Edad con formato incorrecto.
4. Registro con ID duplicado.
5. Listado de pacientes.
6. Búsqueda por ID.
7. Búsqueda por nombre.
8. Búsqueda de paciente inexistente.
9. Actualización correcta.
10. Confirmación antes de eliminar.
11. Eliminación correcta.
12. Listado actualizado después de eliminar.
13. Mensajes para repetir una operación o regresar al menú.

## Observación

Los datos se almacenan únicamente en memoria mientras la aplicación permanece abierta. Este comportamiento es intencional para cumplir con el requisito académico de utilizar una lista dinámica `List<Paciente>` como almacenamiento temporal.