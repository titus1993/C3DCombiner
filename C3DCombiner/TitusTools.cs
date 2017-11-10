using C3DCombiner.BD;
using C3DCombiner.Ejecucion;
using C3DCombiner.Gramaticas;
using FastColoredTextBoxNS;
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
        public static Usuario Usuario = new Usuario();


        public static int Temporal = 0;

        public static int Etiqueta = 0;

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

        public static DataGridView TablaSimbolos = new DataGridView();

        public static List<Archivo> Archivos_Importados = new List<Archivo>();

        public static List<String> Rutas = new List<String>();

        public static IronyFCTB Codigo3D = new IronyFCTB();
        
        public static Boolean ExisteArchivo(String ruta)
        {
            foreach (Archivo archivo in Archivos_Importados)
            {
                if (archivo.Ruta.Equals(ruta))
                {
                    return true;
                }
            }
            return false;
        }

        public static String GetRuta()
        {
            String ruta = "";

            if (Rutas.Count > 0)
            {
                ruta = Rutas[Rutas.Count - 1];
            }

            return ruta;
        }

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

            IniciarTablaSimbolos();

            Codigo3D.Dock = DockStyle.Fill;
            Codigo3D.Grammar = new _3DGrammar();
           
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
            Consola.Text +=  mensaje;
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
        
        

        public static void InsertarError(String Tipo, String Descripcion, String Ruta, int Fila, int Columna)
        {
            Errores.Rows.Insert(Errores.Rows.Count - 1, Errores.Rows.Count, Tipo, Descripcion, Ruta, Fila, Columna);
        }

        public static void LimpiarDatosErrores()
        {
            Errores.Rows.Clear();
        }

        public static bool HayErrores()
        {
            return Errores.Rows.Count - 1 > 0;
        }

        public static void IniciarTablaSimbolos()
        {
            TablaSimbolos.Columns.Add("Rol", "Rol");
            TablaSimbolos.Columns.Add("Tipo", "Tipo");
            TablaSimbolos.Columns.Add("Nombre", "Nombre");
            TablaSimbolos.Columns.Add("Ambito", "Ambito");
            TablaSimbolos.Columns.Add("Tamaño", "Tamaño");
            TablaSimbolos.Columns.Add("Posicion", "Posicion");
            TablaSimbolos.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            TablaSimbolos.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            TablaSimbolos.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            TablaSimbolos.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            TablaSimbolos.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            TablaSimbolos.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            TablaSimbolos.Dock = DockStyle.Fill;
            TablaSimbolos.ReadOnly = true;
            TablaSimbolos.ScrollBars = ScrollBars.Both;
            TablaSimbolos.AutoGenerateColumns = true;
            TablaSimbolos.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            foreach (DataGridViewColumn col in TablaSimbolos.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.HeaderCell.Style.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            }

        }

        public static void InsertarTablaSimbolos(DataTable tabla)
        {
            foreach (DataRow fila in tabla.Rows)
            {
                TablaSimbolos.Rows.Add(fila[0], fila[1], fila[2], fila[3], fila[4], fila[5]);
            }
        }

        public static void LimpiarDatosTablaSimbolos()
        {
            TablaSimbolos.Rows.Clear();
        }


        public static void LimpiarArchivos()
        {
            Archivos_Importados.Clear();
        }
        public static void Limpiar()
        {
            
            LimpiarConsola();
            LimpiarDatosErrores();
            LimpiarDatosTablaSimbolos();
            LimpiarArchivos();
            Temporal = 0;
            Etiqueta = 0;
            Codigo3D.Text = "";
        }

        public static String GetEtq()
        {
            Etiqueta++;
            return "L" + (Etiqueta-1).ToString();
        }

        public static String GetTemp()
        {
            Temporal++;
            return "t" + (Temporal - 1).ToString();
        }
    }
}
