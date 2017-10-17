using C3DCombiner.Ejecucion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C3DCombiner
{
    static class TitusTools
    {
        //Filtro para abrir y guardar
        public static String DialogFilter = "OLC++ File|*.olc|Tree File|*.tree|3D File|*.ddd";

        public static OpenFileDialog FDAbrirArchivo = new OpenFileDialog();

        public static SaveFileDialog FDGuardarArchivo = new SaveFileDialog();

        public static FolderBrowserDialog FBAbrirCarpeta = new FolderBrowserDialog();

        //Arbol de directorios
        public static TitusTree tree = new TitusTree();
        //Administrador de pestañas
        public static TitusTabControl Tabs = new TitusTabControl();

        public static RichTextBox Consola = new RichTextBox();

        public static DataGridView Errores = new DataGridView();

        public static ArbolSintactico Arbol = null;

        public static void IniciarEstaticos()
        {
            //iniciamos el openfiledialog
            FDAbrirArchivo.Filter = TitusTools.DialogFilter;
            FDAbrirArchivo.Title = "Abrir";
            FDAbrirArchivo.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            //iniciamos el savefiledialog
            FDGuardarArchivo.Filter = TitusTools.DialogFilter;
            FDGuardarArchivo.Title = "Guardar Archivo";
            FDGuardarArchivo.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            IniciarConsola();

            IniciarErrores();
        }

        public static void IniciarConsola()
        {
            Consola.Multiline = true;
            Consola.ScrollBars = RichTextBoxScrollBars.Both;
            Consola.WordWrap = false;
            Consola.Dock = DockStyle.Fill;
            Consola.ReadOnly = true;
            Consola.BackColor = System.Drawing.Color.LightGray;
            Consola.ForeColor = System.Drawing.Color.Black;
        }

        public static void ImprimirConsola(String mensaje)
        {
            Consola.Text = Consola.Text + "> " + mensaje + "\n";
            Consola.Select(Consola.Text.Length, 0);
            Consola.ScrollToCaret();
        }

        public static void LimpiarConsola()
        {
            Consola.Text = "";
        }

        public static void IniciarErrores()
        {
            Errores.Columns.Add("No.", "No.");
            Errores.Columns.Add("Tipo", "Tipo");
            Errores.Columns.Add("Descripcion", "Descripcion");
            Errores.Columns.Add("Archivo", "Archivo");
            Errores.Columns.Add("Linea", "Linea");
            Errores.Columns.Add("Columna", "Columna");
            Errores.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            Errores.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            Errores.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Errores.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Errores.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            Errores.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            Errores.Dock = DockStyle.Fill;
            Errores.ReadOnly = true;
            Errores.ScrollBars = ScrollBars.Both;
            Errores.AutoGenerateColumns = true;
            Errores.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            foreach (DataGridViewColumn col in Errores.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.HeaderCell.Style.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            }

        }

        public static DataGridView getErrores()
        {
            return Errores;
        }
        

        public static void InsertarError(String Tipo, String Descripcion, String Ruta, int Fila, int Columna)
        {
            Errores.Rows.Insert(Errores.Rows.Count - 1, Errores.Rows.Count, Tipo, Descripcion, Ruta, Fila, Columna);
        }

        public static void LimpiarDatosErrores()
        {
            Errores.Rows.Clear();
        }

        public static void Limpiar()
        {
            LimpiarConsola();
            LimpiarDatosErrores();
        }
    }
}
