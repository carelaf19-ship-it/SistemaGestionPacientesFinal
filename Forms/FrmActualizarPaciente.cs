using SistemaGestionPacientes.Models;
using SistemaGestionPacientes.Services;

namespace SistemaGestionPacientes;

public class FrmActualizarPaciente : Form
{
    private readonly TextBox txtBuscarId = new();
    private readonly TextBox txtId = new();
    private readonly TextBox txtNombre = new();
    private readonly TextBox txtEdad = new();
    private readonly ComboBox cmbSexo = new();
    private readonly TextBox txtDiagnostico = new();
    private readonly ComboBox cmbEstado = new();
    private readonly DateTimePicker dtpFechaIngreso = new();

    private string? idOriginal;

    public FrmActualizarPaciente()
    {
        InicializarFormulario();
        CargarEnumeraciones();
        HabilitarEdicion(false);
    }

    private void InicializarFormulario()
    {
        Text = "Actualizar paciente";
        StartPosition = FormStartPosition.CenterParent;
        ClientSize = new Size(700, 640);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;

        TableLayoutPanel tabla = new()
        {
            ColumnCount = 3,
            RowCount = 9,
            Dock = DockStyle.Fill,
            Padding = new Padding(30)
        };

        tabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 170));
        tabla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        tabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 115));

        for (int i = 0; i < 8; i++)
        {
            tabla.RowStyles.Add(new RowStyle(SizeType.Absolute, 58));
        }
        tabla.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        Label lblBuscar = CrearEtiqueta("ID a localizar:");
        Button btnCargar = new() { Text = "Cargar", Dock = DockStyle.Fill, Margin = new Padding(4, 10, 4, 10) };
        btnCargar.Click += BtnCargar_Click;

        txtBuscarId.Dock = DockStyle.Fill;
        txtBuscarId.Margin = new Padding(3, 12, 3, 12);

        tabla.Controls.Add(lblBuscar, 0, 0);
        tabla.Controls.Add(txtBuscarId, 1, 0);
        tabla.Controls.Add(btnCargar, 2, 0);

        AgregarFila(tabla, 1, "ID o cédula:", txtId);
        AgregarFila(tabla, 2, "Nombre completo:", txtNombre);
        AgregarFila(tabla, 3, "Edad:", txtEdad);
        AgregarFila(tabla, 4, "Sexo:", cmbSexo);
        AgregarFila(tabla, 5, "Diagnóstico:", txtDiagnostico);
        AgregarFila(tabla, 6, "Estado:", cmbEstado);
        AgregarFila(tabla, 7, "Fecha de ingreso:", dtpFechaIngreso);

        FlowLayoutPanel acciones = new()
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.RightToLeft
        };

        Button btnGuardar = new() { Text = "Guardar cambios", Width = 145, Height = 38 };
        Button btnVolver = new() { Text = "Volver", Width = 110, Height = 38 };

        btnGuardar.Click += BtnGuardar_Click;
        btnVolver.Click += (_, _) => Close();

        acciones.Controls.Add(btnGuardar);
        acciones.Controls.Add(btnVolver);

        tabla.Controls.Add(acciones, 0, 8);
        tabla.SetColumnSpan(acciones, 3);

        Controls.Add(tabla);
    }

    private static Label CrearEtiqueta(string texto)
    {
        return new Label
        {
            Text = texto,
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleLeft,
            Font = new Font("Segoe UI", 10, FontStyle.Bold)
        };
    }

    private static void AgregarFila(TableLayoutPanel tabla, int fila, string etiqueta, Control control)
    {
        control.Dock = DockStyle.Fill;
        control.Margin = new Padding(3, 12, 3, 12);

        tabla.Controls.Add(CrearEtiqueta(etiqueta), 0, fila);
        tabla.Controls.Add(control, 1, fila);
        tabla.SetColumnSpan(control, 2);
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

    private void BtnCargar_Click(object? sender, EventArgs e)
    {
        // Descarta cualquier paciente cargado previamente antes de una nueva búsqueda.
        idOriginal = null;
        LimpiarDatosPaciente();
        HabilitarEdicion(false);

        try
        {
            Cursor = Cursors.WaitCursor;

            Paciente paciente = AppData.Gestor.BuscarPorId(txtBuscarId.Text);

            idOriginal = paciente.Id;

            txtId.Text = paciente.Id;
            txtNombre.Text = paciente.NombreCompleto;
            txtEdad.Text = paciente.Edad.ToString();
            cmbSexo.SelectedItem = paciente.Sexo;
            txtDiagnostico.Text = paciente.Diagnostico;
            cmbEstado.SelectedItem = paciente.Estado;
            dtpFechaIngreso.Value = paciente.FechaIngreso;

            HabilitarEdicion(true);

            MessageBox.Show(
                "Paciente cargado. Puede modificar sus datos.",
                "Paciente encontrado",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            HabilitarEdicion(false);
            MessageBox.Show(
                ex.Message,
                "No fue posible cargar el paciente",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        finally
        {
            Cursor = Cursors.Default;
        }
    }

    private void BtnGuardar_Click(object? sender, EventArgs e)
    {
        if (idOriginal is null)
        {
            MessageBox.Show(
                "Primero debe localizar un paciente por su ID.",
                "Paciente no cargado",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        try
        {
            Cursor = Cursors.WaitCursor;

            Paciente actualizado = new()
            {
                Id = txtId.Text.Trim(),
                NombreCompleto = txtNombre.Text.Trim(),
                Edad = int.Parse(txtEdad.Text.Trim()),
                Sexo = (Sexo)cmbSexo.SelectedItem!,
                Diagnostico = txtDiagnostico.Text.Trim(),
                Estado = (EstadoPaciente)cmbEstado.SelectedItem!,
                FechaIngreso = dtpFechaIngreso.Value.Date
            };

            AppData.Gestor.Actualizar(idOriginal, actualizado);

            MessageBox.Show(
                "Los datos del paciente fueron actualizados correctamente.",
                "Actualización exitosa",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            DialogResult respuesta = MessageBox.Show(
                "¿Desea actualizar otro paciente?",
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
        catch (FormatException)
        {
            MessageBox.Show(
                "La edad debe contener solamente números.",
                "Dato inválido",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                "Error al actualizar",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        finally
        {
            Cursor = Cursors.Default;
        }
    }

    private void HabilitarEdicion(bool habilitado)
    {
        txtId.Enabled = habilitado;
        txtNombre.Enabled = habilitado;
        txtEdad.Enabled = habilitado;
        cmbSexo.Enabled = habilitado;
        txtDiagnostico.Enabled = habilitado;
        cmbEstado.Enabled = habilitado;
        dtpFechaIngreso.Enabled = habilitado;
    }

    private void LimpiarDatosPaciente()
    {
        txtId.Clear();
        txtNombre.Clear();
        txtEdad.Clear();
        txtDiagnostico.Clear();
        dtpFechaIngreso.Value = DateTime.Today;

        if (cmbSexo.Items.Count > 0) cmbSexo.SelectedIndex = 0;
        if (cmbEstado.Items.Count > 0) cmbEstado.SelectedIndex = 0;
    }

    private void LimpiarFormulario()
    {
        idOriginal = null;
        txtBuscarId.Clear();
        LimpiarDatosPaciente();
        HabilitarEdicion(false);
        txtBuscarId.Focus();
    }
}
