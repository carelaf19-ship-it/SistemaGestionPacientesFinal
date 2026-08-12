using SistemaGestionPacientes.Services;

namespace SistemaGestionPacientes;

public class FrmBuscarPaciente : Form
{
    private readonly TextBox txtCriterio = new();
    private readonly DataGridView dgvResultados = new();

    public FrmBuscarPaciente()
    {
        InicializarFormulario();
    }

    private void InicializarFormulario()
    {
        Text = "Buscar paciente por ID o nombre";
        StartPosition = FormStartPosition.CenterParent;
        ClientSize = new Size(1000, 560);
        MinimumSize = new Size(850, 500);

        Panel superior = new()
        {
            Dock = DockStyle.Top,
            Height = 85,
            Padding = new Padding(15)
        };

        Label lblBuscar = new()
        {
            Text = "ID o nombre:",
            Width = 110,
            Dock = DockStyle.Left,
            TextAlign = ContentAlignment.MiddleLeft
        };

        Button btnBuscar = new()
        {
            Text = "Buscar",
            Width = 110,
            Dock = DockStyle.Right
        };

        txtCriterio.Dock = DockStyle.Fill;
        txtCriterio.Margin = new Padding(8);

        btnBuscar.Click += BtnBuscar_Click;

        superior.Controls.Add(txtCriterio);
        superior.Controls.Add(btnBuscar);
        superior.Controls.Add(lblBuscar);

        dgvResultados.Dock = DockStyle.Fill;
        dgvResultados.ReadOnly = true;
        dgvResultados.AllowUserToAddRows = false;
        dgvResultados.AllowUserToDeleteRows = false;
        dgvResultados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvResultados.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        Button btnVolver = new()
        {
            Text = "Volver",
            Dock = DockStyle.Bottom,
            Height = 45
        };
        btnVolver.Click += (_, _) => Close();

        Controls.Add(dgvResultados);
        Controls.Add(btnVolver);
        Controls.Add(superior);
    }

    private void BtnBuscar_Click(object? sender, EventArgs e)
    {
        try
        {
            Cursor = Cursors.WaitCursor;

            var resultados = AppData.Gestor.BuscarPorIdONombre(txtCriterio.Text);

            dgvResultados.DataSource = null;
            dgvResultados.DataSource = resultados;

            if (resultados.Count == 0)
            {
                MessageBox.Show(
                    "No se encontraron pacientes con ese criterio.",
                    "Sin resultados",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

            DialogResult respuesta = MessageBox.Show(
                "¿Desea realizar otra búsqueda?",
                "Continuar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                txtCriterio.Clear();
                dgvResultados.DataSource = null;
                txtCriterio.Focus();
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
                "Error en la búsqueda",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        finally
        {
            Cursor = Cursors.Default;
        }
    }
}
