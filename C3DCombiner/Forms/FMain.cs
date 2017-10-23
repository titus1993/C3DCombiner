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
            //Agregamos el arbol de directorios al panel superior izquierdo           
            splitContainer2.Panel1.Controls.Add(TitusTools.tree);
            //Agregamos el administrador de pesta;as al panel superior derecho
            splitContainer2.Panel2.Controls.Add(TitusTools.Tabs);
            //agregamos las tabs de consola, errores, etc
            TabConsola.Controls.Add(TitusTools.Consola);
            TabErrores.Controls.Add(TitusTools.Errores);
            TabSimbolos.Controls.Add(TitusTools.TablaSimbolos);

            TitusTools.Tabs.agregarNewTab(0);

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
    }
}
