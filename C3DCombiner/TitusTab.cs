using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Irony.Parsing;
using FastColoredTextBoxNS;
using C3DCombiner.Ejecucion;
using C3DCombiner.Gramaticas;
//using _Compi2_Practica_201213587.Ejecucion;
namespace C3DCombiner
{
    class TitusTab : LidorSystems.IntegralUI.Containers.TabPage
    {
        public IronyFCTB TBContenido;
        Label panel;
        bool modificado;
        public string Ruta;
        public int Tipo; // 0 ocl, 1 tree, 2 ddd

        //Variables para usar Irony
        TreeGrammar GramaticaTree;
        OLCGrammar GramaticaOLC;
        _3DGrammar Gramatica3D;
        LanguageData language;
        Parser parser;

        public TitusTab(String nombre, String texto, String ruta, int tipo, Boolean extension)
        {
            this.Text = nombre;
            this.ImageIndex = tipo;
            if (extension)
            {
                switch (tipo)
                {
                    case 0:
                        this.Text += ".olc";
                        break;

                    case 1:
                        this.Text += ".tree";
                        break;

                    case 2:
                        this.Text += ".ddd";
                        break;
                }
            }

            //inicializamos la variable que tendra la ruta del archivo
            this.Ruta = ruta;
            this.Tipo = tipo;
            InitComponent(texto);
        }


        public void Analizar()
        {
            if (GuardarArchivo())
            {
                TitusTools.Limpiar();
                ParseTree arbol = parser.Parse(TBContenido.Text);

                if (arbol.Root != null && arbol.ParserMessages.Count == 0)
                {                    
                    //generacion de la escructura del archivo ejecutad y sus imports
                    ArbolSintactico Arbol = new ArbolSintactico(arbol.Root, this.Tipo, this.Ruta);

                    //generamos el 3d si no es una archivo de 3d
                    if (this.Tipo != 2)
                    {
                        //ejecucion del primer archivo en la lista
                        if (TitusTools.Archivos_Importados.Count > 0 && !TitusTools.HayErrores())
                        {
                            TitusTools.Archivos_Importados[0].EjecutarConMain();
                        }
                        if (TitusTools.HayErrores())
                        {
                            MessageBox.Show("Se encontraron errores", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            MessageBox.Show("Generacion de codigo 3D finalizada con exito.", "Codigo 3D", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    

                    
                }
                else
                {
                    foreach (Irony.LogMessage error in arbol.ParserMessages)
                    {
                        if (error.Message.Contains("Syntax error,"))
                        {
                            TitusTools.InsertarError("Sintactico", error.Message.Replace("Syntax error", " "), this.Ruta, (error.Location.Line + 1), (error.Location.Column + 1));
                        }
                        else if (error.Message.Contains("Invalid character"))
                        {
                            TitusTools.InsertarError("Lexico", error.Message.Replace("Invalid character", "Caracter invalido"), Ruta, (error.Location.Line + 1), (error.Location.Column + 1));
                        }
                        else
                        {
                            TitusTools.InsertarError("Sintactico", error.Message.Replace("Unclosed cooment block", "Comentario de bloque sin cerrar"), Ruta, (error.Location.Line + 1), (error.Location.Column + 1));
                        }

                    }
                    MessageBox.Show("Se encontraron errores", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }

        }

        private void InitComponent(String texto)
        {
            //iniciamos la bandera para saber si ha sido guardado los datos
            this.modificado = false;

            switch (this.Tipo)
            {
                case 0:
                    {
                        //Inicializamos la gramatica y su lenguage para tener el parse
                        GramaticaOLC = new OLCGrammar();
                        language = new LanguageData(GramaticaOLC);
                        parser = new Parser(language);

                        //creamos el textbox
                        TBContenido = new IronyFCTB()
                        {
                            Grammar = GramaticaOLC
                        };
                    }
                    break;

                case 1:
                    {
                        //Inicializamos la gramatica y su lenguage para tener el parse
                        GramaticaTree = new TreeGrammar();
                        language = new LanguageData(GramaticaTree);
                        parser = new Parser(language);

                        //creamos el textbox
                        TBContenido = new IronyFCTB()
                        {
                            Grammar = GramaticaTree
                        };
                    }
                    break;

                case 2:
                    {
                        //Inicializamos la gramatica y su lenguage para tener el parse
                        Gramatica3D = new _3DGrammar();
                        language = new LanguageData(Gramatica3D);
                        parser = new Parser(language);

                        //creamos el textbox
                        TBContenido = new IronyFCTB()
                        {
                            Grammar = Gramatica3D
                        };
                    }
                    break;
            }



            TBContenido.Multiline = true;
            TBContenido.Text = texto;
            //TBContenido.ScrollBars = RichTextBoxScrollBars.Both;

            TBContenido.WordWrap = false;
            TBContenido.Dock = DockStyle.Fill;

            //configuramos el label
            panel = new Label()
            {
                Dock = DockStyle.Bottom,
                Text = "Linea: 1, Columna: 1",
                TextAlign = ContentAlignment.MiddleRight
            };


            //agregamos los eventos
            TBContenido.TextChanged += TBContenido_TextChanged;
            TBContenido.SelectionChanged += TBContenido_SelectionChanged;

            //agregamos los elementos
            this.Controls.Add(TBContenido);
            this.Controls.Add(panel);

            //creamos el boton cerrar
            LidorSystems.IntegralUI.Controls.CommandButton closeButton = new LidorSystems.IntegralUI.Controls.CommandButton()
            {
                ImageIndex = 0,
                Key = "TAB_CLOSE"
            };

            //agreagamos el boton al tab
            this.Buttons.Add(closeButton);
            this.UseParentButtons = false;
        }

        private void TBContenido_SelectionChanged(object sender, EventArgs e)
        {
            panel.Text = "Linea: " + (TBContenido.Selection.Start.iLine + 1).ToString() + ", Columna: " + (TBContenido.Selection.Start.iChar + 1).ToString();
        }

        private void TBContenido_TextChanged(object sender, EventArgs e)
        {
            modificado = true;
        }

        public bool EsModificado()
        {
            return modificado;
        }

        public Boolean GuardarArchivo()
        {
            Boolean estado = false;
            if (String.IsNullOrWhiteSpace(Ruta))
            {

                TitusTools.FDGuardarArchivo.FilterIndex = Tipo + 1;

                if (TitusTools.FDGuardarArchivo.ShowDialog() == DialogResult.OK)
                {
                    System.IO.FileStream fs = (System.IO.FileStream)TitusTools.FDGuardarArchivo.OpenFile();
                    fs.Close();
                    System.IO.File.WriteAllText(TitusTools.FDGuardarArchivo.FileName, this.TBContenido.Text);
                    Ruta = TitusTools.FDGuardarArchivo.FileName;
                    modificado = false;
                    this.Text = System.IO.Path.GetFileName(Ruta);
                    estado = true;
                }
            }
            else
            {
                System.IO.File.WriteAllText(this.Ruta, this.TBContenido.Text);
                modificado = false;
                this.Text = System.IO.Path.GetFileName(Ruta);
                estado = true;
            }
            TitusTools.FDGuardarArchivo.FileName = "";
            return estado;
        }

        public void GuardarComoArchivo()
        {
            TitusTools.FDGuardarArchivo.FilterIndex = Tipo + 1;

            if (TitusTools.FDGuardarArchivo.ShowDialog() == DialogResult.OK)
            {
                System.IO.FileStream fs = (System.IO.FileStream)TitusTools.FDGuardarArchivo.OpenFile();
                fs.Close();
                System.IO.File.WriteAllText(TitusTools.FDGuardarArchivo.FileName, this.TBContenido.Text);
                Ruta = TitusTools.FDGuardarArchivo.FileName;

                modificado = false;
                this.Text = System.IO.Path.GetFileName(Ruta);
            }
            TitusTools.FDGuardarArchivo.FileName = "";
        }
    }
}
