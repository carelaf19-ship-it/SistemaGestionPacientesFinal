using SistemaGestionPacientes.Services;

namespace SistemaGestionPacientes;

public class FrmPrincipal : Form
{
    public FrmPrincipal()
    {
        InicializarFormulario();
    }

    private void InicializarFormulario()
    {
        Text = "Sistema de Gestión de Pacientes";
        StartPosition = FormStartPosition.CenterScreen;
        ClientSize = new Size(760, 520);
        MinimumSize = new Size(760, 520);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;

        Label titulo = new()
        {
            Text = "SISTEMA DE GESTIÓN DE PACIENTES",
            Font = new Font("Segoe UI", 20, FontStyle.Bold),
            AutoSize = false,
            TextAlign = ContentAlignment.MiddleCenter,
            Dock = DockStyle.Top,
            Height = 90
        };

        Label subtitulo = new()
        {
            Text = "Seleccione la operación que desea realizar",
            Font = new Font("Segoe UI", 11),
            AutoSize = false,
            TextAlign = ContentAlignment.MiddleCenter,
            Dock = DockStyle.Top,
            Height = 45
        };

        TableLayoutPanel panelBotones = new()
        {
            ColumnCount = 2,
            RowCount = 3,
            Dock = DockStyle.Fill,
            Padding = new Padding(80, 25, 80, 35)
        };

        panelBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        panelBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        for (int i = 0; i < 3; i++)
        {
            panelBotones.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
        }

        panelBotones.Controls.Add(CrearBoton("Registrar nuevo paciente", (_, _) =>
            AbrirFormulario(new FrmRegistrarPaciente())), 0, 0);

        panelBotones.Controls.Add(CrearBoton("Listar pacientes", (_, _) =>
            AbrirFormulario(new FrmListarPacientes())), 1, 0);

        panelBotones.Controls.Add(CrearBoton("Buscar paciente", (_, _) =>
            AbrirFormulario(new FrmBuscarPaciente())), 0, 1);

        panelBotones.Controls.Add(CrearBoton("Actualizar paciente", (_, _) =>
            AbrirFormulario(new FrmActualizarPaciente())), 1, 1);

        panelBotones.Controls.Add(CrearBoton("Eliminar paciente", (_, _) =>
            AbrirFormulario(new FrmEliminarPaciente())), 0, 2);

        panelBotones.Controls.Add(CrearBoton("Salir del sistema", BtnSalir_Click), 1, 2);

        Controls.Add(panelBotones);
        Controls.Add(subtitulo);
        Controls.Add(titulo);
    }

    private static Button CrearBoton(string texto, EventHandler evento)
    {
        Button boton = new()
        {
            Text = texto,
            Dock = DockStyle.Fill,
            Margin = new Padding(12),
            Font = new Font("Segoe UI", 11, FontStyle.Bold),
            Cursor = Cursors.Hand
        };
        boton.Click += evento;
        return boton;
    }

    private void AbrirFormulario(Form formulario)
    {
        Hide();

        try
        {
            formulario.ShowDialog(this);
        }
        finally
        {
            Show();
            Activate();
        }
    }

    private void BtnSalir_Click(object? sender, EventArgs e)
    {
        DialogResult respuesta = MessageBox.Show(
            "¿Está seguro de que desea salir del sistema?",
            "Confirmar salida",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (respuesta == DialogResult.Yes)
        {
            Application.Exit();
        }
    }
}
