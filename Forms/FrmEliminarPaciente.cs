using SistemaGestionPacientes.Models;
using SistemaGestionPacientes.Services;

namespace SistemaGestionPacientes;

public class FrmEliminarPaciente : Form
{
    private readonly TextBox txtId = new();
    private readonly Label lblPaciente = new();
    private string? idLocalizado;

    public FrmEliminarPaciente()
    {
        InicializarFormulario();
        Limpiar();
    }

    private void InicializarFormulario()
    {
        Text = "Eliminar paciente";
        StartPosition = FormStartPosition.CenterParent;
        ClientSize = new Size(610, 300);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;

        Label titulo = new()
        {
            Text = "Eliminar paciente por ID",
            Dock = DockStyle.Top,
            Height = 60,
            TextAlign = ContentAlignment.MiddleCenter,
            Font = new Font("Segoe UI", 16, FontStyle.Bold)
        };

        Panel panelBusqueda = new()
        {
            Dock = DockStyle.Top,
            Height = 70,
            Padding = new Padding(25, 15, 25, 10)
        };

        Label lblId = new()
        {
            Text = "ID:",
            Dock = DockStyle.Left,
            Width = 60,
            TextAlign = ContentAlignment.MiddleLeft
        };

        Button btnBuscar = new()
        {
            Text = "Buscar",
            Dock = DockStyle.Right,
            Width = 110
        };

        txtId.Dock = DockStyle.Fill;
        btnBuscar.Click += BtnBuscar_Click;

        panelBusqueda.Controls.Add(txtId);
        panelBusqueda.Controls.Add(btnBuscar);
        panelBusqueda.Controls.Add(lblId);

        lblPaciente.Dock = DockStyle.Top;
        lblPaciente.Height = 70;
        lblPaciente.Padding = new Padding(25);
        lblPaciente.TextAlign = ContentAlignment.MiddleLeft;

        FlowLayoutPanel acciones = new()
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.RightToLeft,
            Padding = new Padding(25)
        };

        Button btnEliminar = new()
        {
            Text = "Eliminar",
            Width = 115,
            Height = 38
        };

        Button btnVolver = new()
        {
            Text = "Volver",
            Width = 115,
            Height = 38
        };

        btnEliminar.Click += BtnEliminar_Click;
        btnVolver.Click += (_, _) => Close();

        acciones.Controls.Add(btnEliminar);
        acciones.Controls.Add(btnVolver);

        Controls.Add(acciones);
        Controls.Add(lblPaciente);
        Controls.Add(panelBusqueda);
        Controls.Add(titulo);
    }

    private void BtnBuscar_Click(object? sender, EventArgs e)
    {
        try
        {
            Cursor = Cursors.WaitCursor;

            Paciente paciente = AppData.Gestor.BuscarPorId(txtId.Text);
            idLocalizado = paciente.Id;

            lblPaciente.Text =
                $"Paciente: {paciente.NombreCompleto} | Edad: {paciente.Edad} | Estado: {paciente.Estado}";
        }
        catch (Exception ex)
        {
            idLocalizado = null;
            lblPaciente.Text = "Paciente no localizado.";

            MessageBox.Show(
                ex.Message,
                "Búsqueda",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        finally
        {
            Cursor = Cursors.Default;
        }
    }

    private void BtnEliminar_Click(object? sender, EventArgs e)
    {
        if (idLocalizado is null)
        {
            MessageBox.Show(
                "Primero debe buscar y localizar el paciente que desea eliminar.",
                "Paciente no seleccionado",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        DialogResult confirmar = MessageBox.Show(
            $"¿Está seguro de eliminar el paciente con ID '{idLocalizado}'?",
            "Confirmar eliminación",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning);

        if (confirmar != DialogResult.Yes)
        {
            return;
        }

        try
        {
            Cursor = Cursors.WaitCursor;
            AppData.Gestor.Eliminar(idLocalizado);

            MessageBox.Show(
                "Paciente eliminado correctamente.",
                "Eliminación exitosa",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            DialogResult repetir = MessageBox.Show(
                "¿Desea eliminar otro paciente?",
                "Continuar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (repetir == DialogResult.Yes)
            {
                Limpiar();
            }
            else
            {
                Close();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                "Error al eliminar",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        finally
        {
            Cursor = Cursors.Default;
        }
    }

    private void Limpiar()
    {
        idLocalizado = null;
        txtId.Clear();
        lblPaciente.Text = "Busque un paciente para mostrar sus datos.";
        txtId.Focus();
    }
}
