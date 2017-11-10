using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C3DCombiner
{
    class TitusTabControl : LidorSystems.IntegralUI.Containers.TabControl
    {
        int contador = 1;
        public TitusTabControl()
        {
            initComponents();
            setImageIco();
        }

        private void setImageIco()
        {
            ImageList lista = new ImageList();
            lista.Images.Add("Archivo", Image.FromFile(@"ocl.ico"));
            lista.Images.Add("Archivo", Image.FromFile(@"tree.ico"));
            lista.Images.Add("Archivo", Image.FromFile(@"ddd.ico"));
            this.ImageList = lista;
        }

        private void initComponents()
        {
            ImageList imgButtons = new System.Windows.Forms.ImageList();

            // imgButtons
            imgButtons = new ImageList();
            imgButtons.TransparentColor = System.Drawing.Color.Transparent;
            imgButtons.Images.Add("Cerrar", Image.FromFile(@"cerrar.ico"));

            //configuraciones inciales del tab
            this.Name = "TabControl";
            this.Size = new System.Drawing.Size(960, 480);
            this.Location = new System.Drawing.Point(12, 30);
            this.ButtonImageList = imgButtons;
            this.TabButtonClicked += TitusTabControl_TabButtonClicked;
            this.Dock = DockStyle.Fill;
        }

        //evento para cerrar las pestañas
        private void TitusTabControl_TabButtonClicked(object sender, LidorSystems.IntegralUI.ObjectClickEventArgs e)
        {
            if (e.Object is LidorSystems.IntegralUI.Controls.CommandButton)
            {
                LidorSystems.IntegralUI.Controls.CommandButton btn = (LidorSystems.IntegralUI.Controls.CommandButton)e.Object;

                // Check whether the type of a button is a close button
                if (btn.Key == "TAB_CLOSE")
                {
                    // Locate the tab in which the command button was clicked
                    LidorSystems.IntegralUI.Containers.TabPage page = this.GetPageAt(e.Position);

                    if (page != null)
                    {
                        // Depending on the action, you can determine whether you want to hide or dispose the tab
                        switch (this.CloseAction)
                        {
                            case LidorSystems.IntegralUI.CloseAction.Hide:
                                page.Hide();
                                break;

                            default:
                                TitusTab TTaux = (TitusTab)this.SelectedPage;
                                //preguntamos si ha sido modificado el contenido para preguntar si desea guardar
                                if (TTaux.EsModificado())
                                {
                                    switch (MessageBox.Show("Desea guardar el archivo", "Guardar archivo", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Asterisk))
                                    {
                                        case DialogResult.Yes:
                                            guardarTab();
                                            page.Remove();
                                            break;

                                        case DialogResult.No:
                                            page.Remove();
                                            break;

                                        default:
                                            break;
                                    }
                                }
                                else
                                {
                                    page.Remove();
                                }
                                break;
                        }
                    }
                }
            }
        }


        public void agregarNewTab(int tipo)
        {
            String nombre = "Nuevo Documento " + contador.ToString();
            TitusTab aux = new TitusTab(nombre, "", "", tipo, true);
            this.Pages.Add(aux);
            contador++;
        }

        public void abrirTab()
        {
            TitusTools.FDAbrirArchivo.ShowDialog();

            if (TitusTools.FDAbrirArchivo.FileName != "")
            {
                if (TitusTools.FDAbrirArchivo.FilterIndex == 1)
                {
                    TitusTab aux = new TitusTab(TitusTools.FDAbrirArchivo.SafeFileName, File.ReadAllText(TitusTools.FDAbrirArchivo.FileName), TitusTools.FDAbrirArchivo.FileName, 0, false);
                    this.Pages.Add(aux);
                }
                else if (TitusTools.FDAbrirArchivo.FilterIndex == 2)
                {
                    TitusTab aux = new TitusTab(TitusTools.FDAbrirArchivo.SafeFileName, File.ReadAllText(TitusTools.FDAbrirArchivo.FileName), TitusTools.FDAbrirArchivo.FileName, 1, false);
                    this.Pages.Add(aux);
                }
                else if (TitusTools.FDAbrirArchivo.FilterIndex == 3)
                {
                    TitusTab aux = new TitusTab(TitusTools.FDAbrirArchivo.SafeFileName, File.ReadAllText(TitusTools.FDAbrirArchivo.FileName), TitusTools.FDAbrirArchivo.FileName, 2, false);
                    this.Pages.Add(aux);
                }
            }

            TitusTools.FDAbrirArchivo.FileName = "";
        }

        public void abrirTab(String nombre, String ruta, int tipo)
        {
            TitusTab aux = new TitusTab(nombre, File.ReadAllText(ruta), ruta, tipo, false);
            this.Pages.Add(aux);
        }

        public void guardarTab()
        {
            TitusTab TTaux = (TitusTab)this.SelectedPage;
            if (TTaux != null)
            {
                TTaux.GuardarArchivo();
                this.Refresh();
                this.UpdateLayout();
                TitusTools.tree.Refrescar();
            }
        }

        public void guardarComoTab()
        {
            TitusTab TTaux = (TitusTab)this.SelectedPage;
            if (TTaux != null)
            {
                TTaux.GuardarComoArchivo();
                this.Refresh();
                this.UpdateLayout();
                TitusTools.tree.Refrescar();
            }
        }

        public void Ejecutar()
        {
            TitusTab TTaux = (TitusTab)this.SelectedPage;
            if (TTaux != null)
            {
                TTaux.Analizar();
            }
        }
    }
}
