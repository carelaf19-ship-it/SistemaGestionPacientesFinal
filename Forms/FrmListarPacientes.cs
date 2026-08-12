using SistemaGestionPacientes.Services;

namespace SistemaGestionPacientes;

public class FrmListarPacientes : Form
{
    private readonly DataGridView dgvPacientes = new();

    public FrmListarPacientes()
    {
        InicializarFormulario();
    }

    private void InicializarFormulario()
    {
        Text = "Listado de pacientes";
        StartPosition = FormStartPosition.CenterParent;
        ClientSize = new Size(1050, 560);
        MinimumSize = new Size(900, 500);

        dgvPacientes.Dock = DockStyle.Fill;
        dgvPacientes.ReadOnly = true;
        dgvPacientes.AllowUserToAddRows = false;
        dgvPacientes.AllowUserToDeleteRows = false;
        dgvPacientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvPacientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvPacientes.MultiSelect = false;

        FlowLayoutPanel acciones = new()
        {
            Dock = DockStyle.Bottom,
            Height = 65,
            FlowDirection = FlowDirection.RightToLeft,
            Padding = new Padding(10)
        };

        Button btnListar = new() { Text = "Mostrar pacientes", Width = 150, Height = 38 };
        Button btnVolver = new() { Text = "Volver", Width = 110, Height = 38 };

        btnListar.Click += BtnListar_Click;
        btnVolver.Click += (_, _) => Close();

        acciones.Controls.Add(btnListar);
        acciones.Controls.Add(btnVolver);

        Controls.Add(dgvPacientes);
        Controls.Add(acciones);
    }

    private void BtnListar_Click(object? sender, EventArgs e)
    {
        try
        {
            Cursor = Cursors.WaitCursor;
            dgvPacientes.DataSource = null;
            dgvPacientes.DataSource = AppData.Gestor.ListarTodos().ToList();

            if (dgvPacientes.Rows.Count == 0)
            {
                MessageBox.Show(
                    "Todavía no hay pacientes registrados.",
                    "Listado vacío",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

            DialogResult respuesta = MessageBox.Show(
                "¿Desea volver a consultar el listado?",
                "Continuar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta == DialogResult.No)
            {
                Close();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                "Error al listar",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        finally
        {
            Cursor = Cursors.Default;
        }
    }
}
