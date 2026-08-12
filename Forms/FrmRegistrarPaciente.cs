using SistemaGestionPacientes.Models;
using SistemaGestionPacientes.Services;

namespace SistemaGestionPacientes;

public class FrmRegistrarPaciente : Form
{
    private readonly TextBox txtId = new();
    private readonly TextBox txtNombre = new();
    private readonly TextBox txtEdad = new();
    private readonly ComboBox cmbSexo = new();
    private readonly TextBox txtDiagnostico = new();
    private readonly ComboBox cmbEstado = new();
    private readonly DateTimePicker dtpFechaIngreso = new();

    public FrmRegistrarPaciente()
    {
        InicializarFormulario();
        CargarEnumeraciones();
        LimpiarFormulario();
    }

    private void InicializarFormulario()
    {
        Text = "Registrar nuevo paciente";
        StartPosition = FormStartPosition.CenterParent;
        ClientSize = new Size(650, 560);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;

        TableLayoutPanel tabla = CrearTablaFormulario();

        AgregarFila(tabla, 0, "ID o cédula:", txtId);
        AgregarFila(tabla, 1, "Nombre completo:", txtNombre);
        AgregarFila(tabla, 2, "Edad:", txtEdad);
        AgregarFila(tabla, 3, "Sexo:", cmbSexo);
        AgregarFila(tabla, 4, "Diagnóstico:", txtDiagnostico);
        AgregarFila(tabla, 5, "Estado:", cmbEstado);
        AgregarFila(tabla, 6, "Fecha de ingreso:", dtpFechaIngreso);

        FlowLayoutPanel acciones = new()
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.RightToLeft
        };

        Button btnRegistrar = new() { Text = "Registrar", Width = 120, Height = 38 };
        Button btnCancelar = new() { Text = "Volver", Width = 120, Height = 38 };

        btnRegistrar.Click += BtnRegistrar_Click;
        btnCancelar.Click += (_, _) => Close();

        acciones.Controls.Add(btnRegistrar);
        acciones.Controls.Add(btnCancelar);

        tabla.Controls.Add(acciones, 0, 7);
        tabla.SetColumnSpan(acciones, 2);

        Controls.Add(tabla);
    }

    private static TableLayoutPanel CrearTablaFormulario()
    {
        TableLayoutPanel tabla = new()
        {
            ColumnCount = 2,
            RowCount = 8,
            Dock = DockStyle.Fill,
            Padding = new Padding(35)
        };

        tabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 170));
        tabla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

        for (int i = 0; i < 7; i++)
        {
            tabla.RowStyles.Add(new RowStyle(SizeType.Absolute, 58));
        }
        tabla.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        return tabla;
    }

    private static void AgregarFila(TableLayoutPanel tabla, int fila, string etiqueta, Control control)
    {
        Label label = new()
        {
            Text = etiqueta,
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleLeft,
            Font = new Font("Segoe UI", 10, FontStyle.Bold)
        };

        control.Dock = DockStyle.Fill;
        control.Margin = new Padding(3, 12, 3, 12);

        tabla.Controls.Add(label, 0, fila);
        tabla.Controls.Add(control, 1, fila);
    }

    private void CargarEnumeraciones()
    {
        cmbSexo.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbSexo.DataSource = Enum.GetValues<Sexo>();

        cmbEstado.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbEstado.DataSource = Enum.GetValues<EstadoPaciente>();

        dtpFechaIngreso.Format = DateTimePickerFormat.Short;
        dtpFechaIngreso.MaxDate = DateTime.Today;
    }

    private void BtnRegistrar_Click(object? sender, EventArgs e)
    {
        try
        {
            Cursor = Cursors.WaitCursor;

            int edad = int.Parse(txtEdad.Text.Trim());

            Paciente nuevoPaciente = new()
            {
                Id = txtId.Text.Trim(),
                NombreCompleto = txtNombre.Text.Trim(),
                Edad = edad,
                Sexo = (Sexo)cmbSexo.SelectedItem!,
                Diagnostico = txtDiagnostico.Text.Trim(),
                Estado = (EstadoPaciente)cmbEstado.SelectedItem!,
                FechaIngreso = dtpFechaIngreso.Value.Date
            };

            AppData.Gestor.Registrar(nuevoPaciente);

            MessageBox.Show(
                "Paciente registrado correctamente.",
                "Registro exitoso",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            PreguntarSiDeseaRepetir();
        }
        catch (FormatException)
        {
            MessageBox.Show(
                "La edad debe contener solamente números.",
                "Dato inválido",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            txtEdad.Focus();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                "No fue posible registrar el paciente",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        finally
        {
            Cursor = Cursors.Default;
        }
    }

    private void PreguntarSiDeseaRepetir()
    {
        DialogResult respuesta = MessageBox.Show(
            "¿Desea registrar otro paciente?",
            "Continuar",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (respuesta == DialogResult.Yes)
        {
            LimpiarFormulario();
        }
        else
        {
            Close();
        }
    }

    private void LimpiarFormulario()
    {
        txtId.Clear();
        txtNombre.Clear();
        txtEdad.Clear();
        txtDiagnostico.Clear();

        if (cmbSexo.Items.Count > 0)
        {
            cmbSexo.SelectedIndex = 0;
        }

        if (cmbEstado.Items.Count > 0)
        {
            cmbEstado.SelectedIndex = 0;
        }

        dtpFechaIngreso.Value = DateTime.Today;
        txtId.Focus();
    }
}
