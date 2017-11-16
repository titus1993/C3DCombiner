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

    class TitusTree : TreeView
    {
        List<String> Paths;

        public TitusTree()
        {
            this.Paths = new List<string>();
            PathSeparator = @"\";
            this.Dock = DockStyle.Fill;
            SetImageIco();
            SetMenuContext();
            //setRutaRaiz();
            //Refrescar();

            DoubleClick += TitusTree_DoubleClick;
            NodeMouseClick += TitusTree_Click;
        }


        public void AgregarDirectorio(String directorio)
        {
            if (!Paths.Contains(directorio))
            {
                Paths.Add(directorio);
                Refrescar();
            }
        }


        private void TitusTree_Click(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.SelectedNode = e.Node;
            }
        }

        private void SetRutaRaiz(String raiz)
        {
            int contador = 0;
            int i = raiz.Length - 1;
            while (contador < 1 && i >= 0)
            {
                if (raiz[i].ToString() == @"\")
                {
                    contador++;
                }
                i--;
            }
            //subraiz = raiz.Substring(0, i+1) + @"\";
        }

        public void Refrescar()
        {
            this.Nodes.Clear();
            foreach (String path in Paths)
            {
                if (FileSystem.DirectoryExists(path))
                {
                    ListaDirectorio(this, path);
                }
                else
                {
                    this.Nodes.Clear();
                }
            }
        }

        private void ListaDirectorio(TreeView Vista, string ruta)
        {
            var raizDirectorioInfo = new DirectoryInfo(ruta);
            if (FileSystem.DirectoryExists(ruta))
            {
                Vista.Nodes.Add(CrearDirectorioNodo(raizDirectorioInfo));
                Vista.GetNodeAt(0, 0).Expand();
            }

        }
        private void SetImageIco()
        {
            ImageList lista = new ImageList();
            lista.Images.Add("Carpeta", Image.FromFile(@"carpeta.ico"));
            lista.Images.Add("Archivo", Image.FromFile(@"ocl.ico"));
            lista.Images.Add("Archivo", Image.FromFile(@"tree.ico"));
            lista.Images.Add("Archivo", Image.FromFile(@"ddd.ico"));
            this.ImageList = lista;
        }

        private void SetMenuContext()
        {
            ContextMenuStrip docMenu = new ContextMenuStrip();

            ToolStripMenuItem nuevoacf = new ToolStripMenuItem("Crear archivo", Properties.Resources.Nuevo);
            nuevoacf.Click += NuevoArchivo_Click;

            ToolStripMenuItem LCrearCarpeta = new ToolStripMenuItem("Crear carpeta");
            LCrearCarpeta.Click += LCrearCarpeta_Click;

            ToolStripMenuItem LEliminar = new ToolStripMenuItem("Eliminar");
            LEliminar.Click += BorrarNodo;

            ToolStripMenuItem LRuta = new ToolStripMenuItem("Ver ubicacion");
            LRuta.Click += LRuta_Click;

            docMenu.Items.AddRange(new ToolStripMenuItem[] { nuevoacf, LCrearCarpeta/*, LEliminar*/, LRuta });

            this.ContextMenuStrip = docMenu;
        }

        private void LCrearCarpeta_Click(object sender, EventArgs e)
        {
            if (SelectedNode != null)
            {
                string ruta = SelectedNode.Name;
                if (FileSystem.DirectoryExists(ruta))
                {
                    ruta = SelectedNode.Name;
                    CrearCarpeta(ruta);
                }
                else
                {
                    ruta = SelectedNode.Parent.Name;
                    CrearCarpeta(ruta);
                }
            }
        }

        private void CrearCarpeta(string ruta)
        {
            string result = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el nombre de la carpeta", "Crear carpeta", "");

            if (!String.IsNullOrWhiteSpace(result))
            {
                string nuevaruta = ruta + @"\" + result;
                if (FileSystem.DirectoryExists(nuevaruta))
                {
                    MessageBox.Show("La carpeta ya existe, ingrese otro nombre.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Directory.CreateDirectory(nuevaruta);
                    Refrescar();
                }

            }
        }


        private void LRuta_Click(object sender, EventArgs e)
        {
            if (SelectedNode != null)
            {
                string ruta = SelectedNode.Name;
                MessageBox.Show(ruta, "Ubicacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BorrarNodo(object sender, EventArgs e)
        {
            if (SelectedNode != null)
            {
                string ruta = SelectedNode.Name;
                if (FileSystem.FileExists(ruta))
                {
                    FileSystem.DeleteFile(ruta, UIOption.AllDialogs, RecycleOption.SendToRecycleBin, UICancelOption.DoNothing);
                    Refrescar();
                }
                else if (FileSystem.DirectoryExists(ruta))
                {
                    FileSystem.DeleteDirectory(ruta, UIOption.AllDialogs, RecycleOption.SendToRecycleBin, UICancelOption.DoNothing);
                    Refrescar();
                }
            }

        }

        private void NuevoArchivo_Click(object sender, EventArgs e)
        {
            if (SelectedNode != null)
            {
                string ruta = SelectedNode.Name;
                if (FileSystem.DirectoryExists(ruta))
                {
                    CrearArchivo(ruta);
                }
                else
                {
                    ruta = SelectedNode.Parent.Name;
                    CrearArchivo(ruta);
                }
            }
        }
        private void CrearArchivo(string ruta)
        {
            SaveFileDialog dialogo = new SaveFileDialog()
            {
                Filter = "Tree File|*.tree|OLC++ File|*.olc|3D File|*.ddd",
                Title = "Crear Archivo",
                InitialDirectory = ruta
            };
            dialogo.ShowDialog();

            if (dialogo.FileName != "")
            {
                System.IO.FileStream fs = (System.IO.FileStream)dialogo.OpenFile();
                fs.Close();
                Refrescar();
            }
        }

        private static TreeNode CrearDirectorioNodo(DirectoryInfo DirectorioInfo)
        {
            var DirectorioNodo = new TreeNode(DirectorioInfo.Name);

            foreach (var directory in DirectorioInfo.GetDirectories())
            {
                DirectorioNodo.Nodes.Add(CrearDirectorioNodo(directory));
                DirectorioNodo.ImageIndex = 0;
                DirectorioNodo.SelectedImageIndex = 0;
                DirectorioNodo.Name = DirectorioInfo.FullName;

            }
            foreach (var file in DirectorioInfo.GetFiles())
            {
                TreeNode nodo = null;
                if (Path.GetExtension(file.ToString()).ToLower() == ".olc")
                {
                    nodo = new TreeNode(file.Name, 1, 1);
                }
                else if (Path.GetExtension(file.ToString()).ToLower() == ".tree")
                {
                    nodo = new TreeNode(file.Name, 2, 2);
                }
                else if (Path.GetExtension(file.ToString()).ToLower() == ".ddd")
                {
                    nodo = new TreeNode(file.Name, 3, 3);
                }

                if (nodo != null)
                {
                    nodo.Name = file.DirectoryName;
                    DirectorioNodo.Nodes.Add(nodo);
                }
            }

            return DirectorioNodo;
        }

        //metodos para que los modifiques xD

        private void TitusTree_DoubleClick(object sender, EventArgs e)
        {
            if (SelectedNode != null)
            {
                string ruta = SelectedNode.Name + "\\" + SelectedNode.Text;
                if (FileSystem.FileExists(ruta))
                {

                    TitusTools.Tabs.abrirTab(SelectedNode.Text, ruta, SelectedNode.SelectedImageIndex - 1);
                    //aqui metes el codigo para abrir un archivo XD
                    Console.WriteLine(ruta);
                }
            }
        }
    }
}
