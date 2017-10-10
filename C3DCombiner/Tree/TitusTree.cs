using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;

namespace C3DCombiner
{
    public List<String> pahs = new List<string>();

    class TitusTree: TreeView
    {
        public TitusTree()
        {            
            PathSeparator = @"\";
            this.Dock = DockStyle.Fill;
            
            setImageIco();
            setMenuContext();
            //setRutaRaiz();
            //Refrescar();

            DoubleClick += TitusTree_DoubleClick;
            NodeMouseClick += TitusTree_Click;            
        }

       

        private void TitusTree_Click(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.SelectedNode = e.Node;
            }
        }

        private void setRutaRaiz(String raiz)
        {
            int contador = 0;
            int i = raiz.Length - 1;
            while(contador < 1 && i >= 0)
            {
                if (raiz[i].ToString() == @"\")
                {
                    contador++;
                }
                i--;
            }
            //subraiz = raiz.Substring(0, i+1) + @"\";
        }
        
        public void Refrescar(String raiz)
        {
            if (FileSystem.DirectoryExists(raiz))
            {
                ListaDirectorio(this, raiz);
            }else
            {
                this.Nodes.Clear();
            }   
        }

        private void ListaDirectorio(TreeView Vista, string ruta)
        {
            Vista.Nodes.Clear();
            var raizDirectorioInfo = new DirectoryInfo(ruta);
            if (FileSystem.DirectoryExists(ruta))
            {
                Vista.Nodes.Add(CrearDirectorioNodo(raizDirectorioInfo));
                Vista.GetNodeAt(0, 0).Expand();
            }
            
        }
        private void setImageIco()
        {
            ImageList lista = new ImageList();
            lista.Images.Add("Carpeta", Properties.Resources.Nuevo);
            lista.Images.Add("Archivo", Image.FromFile(@"jc.ico"));
            lista.Images.Add("Archivo", Image.FromFile(@"acf.ico"));
            this.ImageList = lista;
        }

        private void setMenuContext()
        {
            ContextMenuStrip docMenu = new ContextMenuStrip();            

            ToolStripMenuItem nuevoacf= new ToolStripMenuItem("Crear archivo", Properties.Resources.Nuevo);
            nuevoacf.Click += NuevoArchivo_Click;

            ToolStripMenuItem LCrearCarpeta = new ToolStripMenuItem("Crear carpeta");
            LCrearCarpeta.Click += LCrearCarpeta_Click;

            ToolStripMenuItem LEliminar = new ToolStripMenuItem("Eliminar");
            LEliminar.Click += BorrarNodo;

            ToolStripMenuItem LRuta = new ToolStripMenuItem("Ver ubicacion");
            LRuta.Click += LRuta_Click;

            docMenu.Items.AddRange(new ToolStripMenuItem[] { nuevoacf, LCrearCarpeta,LEliminar, LRuta});

            this.ContextMenuStrip = docMenu;
        }

        private void LCrearCarpeta_Click(object sender, EventArgs e)
        {
            string ruta = /*subraiz +*/ SelectedNode.FullPath;
            if (FileSystem.DirectoryExists(ruta))
            {
                ruta = /*subraiz + */SelectedNode.FullPath;
                CrearCarpeta(ruta);
            }else
            {
                ruta = /*subraiz + */SelectedNode.Parent.FullPath;
                CrearCarpeta(ruta);
            }
        }

        private void CrearCarpeta(string ruta)
        {
            string result=  Microsoft.VisualBasic.Interaction.InputBox("Ingrese el nombre de la carpeta", "Crear carpeta","");
 
            if (!String.IsNullOrWhiteSpace(result))
            {
                string nuevaruta = ruta + @"\" + result;
                if (FileSystem.DirectoryExists(nuevaruta))
                {
                    MessageBox.Show("La carpeta ya existe, ingrese otro nombre.","Alerta",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }else
                {
                    Directory.CreateDirectory(nuevaruta);
                    Refrescar("");//cambiar ruta
                }
                
            }
        }


        private void LRuta_Click(object sender, EventArgs e)
        {
            if (SelectedNode != null)
            {
                string ruta = /*subraiz + */SelectedNode.FullPath;
                MessageBox.Show(ruta, "Ubicacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }            
        }

        private void BorrarNodo(object sender, EventArgs e)
        {
            string ruta = /*subraiz + */SelectedNode.FullPath;
            if (FileSystem.FileExists(ruta))
            {
                FileSystem.DeleteFile(ruta, UIOption.AllDialogs, RecycleOption.SendToRecycleBin, UICancelOption.DoNothing);
                Refrescar("");
            }
            else if (FileSystem.DirectoryExists(ruta))
            {
                FileSystem.DeleteDirectory(ruta, UIOption.AllDialogs, RecycleOption.SendToRecycleBin, UICancelOption.DoNothing);
                Refrescar("");
            }            
        }               

        private void NuevoArchivo_Click(object sender, EventArgs e)
        {
            string ruta = /*subraiz + */SelectedNode.FullPath;
            if (FileSystem.DirectoryExists(ruta))
            {
                CrearArchivo(ruta);
            }
            else
            {
                ruta = /*subraiz + */SelectedNode.Parent.FullPath;
                CrearArchivo(ruta);               
            }            
        }
        private void CrearArchivo(string ruta)
        {
            SaveFileDialog dialogo = new SaveFileDialog();
            dialogo.Filter = "Tree File|*.tree|OLC File|*.olc|3D File|*.ddd";
            dialogo.Title = "Crear Archivo";
            dialogo.InitialDirectory = ruta;
            dialogo.ShowDialog();
            
            if (dialogo.FileName != "")
            {
                System.IO.FileStream fs = (System.IO.FileStream)dialogo.OpenFile();
                fs.Close();
                Refrescar("");
            }
        }

        private void OpenLabel_Click(object sender, EventArgs e)
        {
            Console.WriteLine(this.SelectedNode.FullPath);
            
        }        

        private static TreeNode CrearDirectorioNodo(DirectoryInfo DirectorioInfo)
        {
            var DirectorioNodo = new TreeNode(DirectorioInfo.Name);
            
            foreach (var directory in DirectorioInfo.GetDirectories())
            {
                DirectorioNodo.Nodes.Add(CrearDirectorioNodo(directory));
                DirectorioNodo.ImageIndex = 0;
                DirectorioNodo.SelectedImageIndex = 0;
            }
            foreach (var file in DirectorioInfo.GetFiles())
            {
                if (Path.GetExtension(file.ToString()).ToLower() == ".jc")
                {                   
                    DirectorioNodo.Nodes.Add(new TreeNode(file.Name,1,1));
                              
                }else if (Path.GetExtension(file.ToString()).ToLower() == ".acf")
                {
                    DirectorioNodo.Nodes.Add(new TreeNode(file.Name, 2, 2));
                }
            }
            
            return DirectorioNodo;
        }

        //metodos para que los modifiques xD

        private void TitusTree_DoubleClick(object sender, EventArgs e)
        {
            string ruta = /*subraiz +*/ SelectedNode.FullPath;
            if (FileSystem.FileExists(ruta))
            {
                //aqui metes el codigo para abrir un archivo XD
                Console.WriteLine(ruta);
            }
        }
    }
}
