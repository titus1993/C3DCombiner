using C3DCombiner.BD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C3DCombiner.Forms
{
    public partial class Compartir : Form
    {
        public Compartir()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        BaseDatos Base = new BaseDatos();

        private void button1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(richTextBox1.Text))
            {
                TitusTab aux = (TitusTab)TitusTools.Tabs.SelectedPage;
                if (aux != null)
                {
                    if (aux.Tipo == 1)
                    {
                        if (aux.Ruta != "" && !aux.EsModificado())
                        {
                            if (Base.ExisteRepositorio(aux.Text.Replace(".tree", "")))
                            {
                                if (MessageBox.Show("Ya existe una clase con este nombre, deseea sobreescribirla", "Clase", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    Base.ModificarRepositorio(aux.Text, richTextBox1.Text, aux.TBContenido.Text);
                                    MessageBox.Show("Se ha modificado correctamente la clase", "Clase", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    this.Dispose();
                                }
                            }
                            else
                            {
                                Base.InsertarRepositorio(aux.Text, richTextBox1.Text, aux.TBContenido.Text);
                                MessageBox.Show("Se ha compartido correctamente la clase", "Clase", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Dispose();
                            }
                        }
                        else
                        {
                            MessageBox.Show("No se ha guardado el documento", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Solo se pueden copartir archivos Tree", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("No hay ningun documento seleccionado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Ingrese una descripcion", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
