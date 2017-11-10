using C3DCombiner.Forms;
using C3DCombiner.Funciones;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C3DCombiner
{
    public partial class FMain : Form
    {


        public FMain()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }


        private void FMain_Load(object sender, EventArgs e)
        {
            TitusTools.IniciarEstaticos();
            IniciarComponentes();
            //Agregamos el arbol de directorios al panel superior izquierdo           
            splitContainer2.Panel1.Controls.Add(TitusTools.tree);
            //Agregamos el administrador de pesta;as al panel superior derecho
            splitContainer2.Panel2.Controls.Add(TitusTools.Tabs);
            //agregamos las tabs de consola, errores, etc
            TabConsola.Controls.Add(TitusTools.Consola);
            TabErrores.Controls.Add(TitusTools.Errores);
            TabSimbolos.Controls.Add(TitusTools.TablaSimbolos);
            Tab3d.Controls.Add(TitusTools.Codigo3D);
            Tab3d.Controls.Add(panel);
            //configuramos el label


            TitusTools.Tabs.agregarNewTab(0);                        
        }

        Label panel;
        private void IniciarComponentes()
        {
            panel = new Label()
            {
                Dock = DockStyle.Bottom,
                Text = "Linea: 1, Columna: 1",
                TextAlign = ContentAlignment.MiddleRight,
                BackColor = Color.LightGray
            };

            //agregamos los eventos
            TitusTools.Codigo3D.SelectionChanged += TBContenido_SelectionChanged;
        }

        private void AbrirCarpetaToolStripMenuItem_Click(object sender, EventArgs e)
        {

            TitusTools.FBAbrirCarpeta.ShowDialog();

            String directorio = TitusTools.FBAbrirCarpeta.SelectedPath;

            if (directorio != "")
            {
                TitusTools.tree.AgregarDirectorio(directorio);
                TitusTools.FBAbrirCarpeta.SelectedPath = "";
            }
        }

        private void TBContenido_SelectionChanged(object sender, EventArgs e)
        {
            panel.Text = "Linea: " + (TitusTools.Codigo3D.Selection.Start.iLine + 1).ToString() + ", Columna: " + (TitusTools.Codigo3D.Selection.Start.iChar + 1).ToString();
        }

        private void AbrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TitusTools.Tabs.abrirTab();
        }

        private void OCLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TitusTools.Tabs.agregarNewTab(0);
        }

        private void TreeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TitusTools.Tabs.agregarNewTab(1);
        }

        private void DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TitusTools.Tabs.agregarNewTab(2);
        }

        private void GuardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TitusTools.Tabs.guardarTab();
        }

        private void GuardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TitusTools.Tabs.guardarComoTab();
        }

        private void SalirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void DiagramaUMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FUml mUml = new FUml();
            mUml.Show();
        }

        private void EjecutarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TitusTools.Tabs.Ejecutar();
        }

        private void iniciarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!TitusTools.Usuario.Estado)
            {
                Login login = new Login();
                login.Show();
            }
            else
            {
                MessageBox.Show("Ya existe una sesion abierta, cierrela para iniciar sesion nuevamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TitusTools.Usuario.Estado)
            {
                TitusTools.Usuario = new BD.Usuario();
                MessageBox.Show("Ha cerrado su sesion exitosamente, si desea utilizar el modulo de codigo compartido inicie sesion nuevamente", "Sesion cerrada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No se ha iniciado ninguna sesion", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void compartirClaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TitusTools.Usuario.Estado)
            {
                Compartir compartir = new Compartir();
                compartir.Show();
            }
            else
            {
                MessageBox.Show("No se ha iniciado ninguna sesion", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void reporteASTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ast a = new Ast();
            a.Show();
        }
    }
}
