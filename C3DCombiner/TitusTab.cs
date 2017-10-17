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
//using _Compi2_Practica_201213587.Ejecucion;
namespace C3DCombiner
{
    class TitusTab : LidorSystems.IntegralUI.Containers.TabPage
    {
        IronyFCTB TBContenido;
        Label panel;
        bool modificado;
        string Ruta;
        int Tipo; // 0 ocl, 1 tree, 2 ddd

        //Variables para usar Irony
        TreeGrammar GramaticaTree;
        OLCGrammar GramaticaOLC;

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
                        this.Text += ".ocl";
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
            initComponent(texto);
        }


        public void Analizar()
        {
            if (guardarArchivo())
            {
                TitusTools.Limpiar();
                ParseTree arbol = parser.Parse(TBContenido.Text);

                if (arbol.Root != null && arbol.ParserMessages.Count == 0)
                {                    
                    TitusTools.Arbol = new ArbolSintactico(arbol.Root, this.Tipo, this.Ruta);
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
                }
            }

        }

        private void initComponent(String texto)
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
                        TBContenido = new IronyFCTB();
                        TBContenido.Grammar = GramaticaOLC;
                    }
                    break;

                case 1:
                    {
                        //Inicializamos la gramatica y su lenguage para tener el parse
                        GramaticaTree = new TreeGrammar();
                        language = new LanguageData(GramaticaTree);
                        parser = new Parser(language);

                        //creamos el textbox
                        TBContenido = new IronyFCTB();
                        TBContenido.Grammar = GramaticaTree;
                    }
                    break;

                case 2:
                    {
                        //Inicializamos la gramatica y su lenguage para tener el parse
                        /*Gramatica = new TreeGrammar();
                        language = new LanguageData(Gramatica);
                        parser = new Parser(language);*/

                        //creamos el textbox
                        TBContenido = new IronyFCTB();
                        //TBContenido.Grammar = Gramatica;
                    }
                    break;
            }



            TBContenido.Multiline = true;
            TBContenido.Text = texto;
            //TBContenido.ScrollBars = RichTextBoxScrollBars.Both;

            TBContenido.WordWrap = false;
            TBContenido.Dock = DockStyle.Fill;

            //configuramos el label
            panel = new Label();
            panel.Dock = DockStyle.Bottom;
            panel.Text = "Linea: 1, Columna: 1";
            panel.TextAlign = ContentAlignment.MiddleRight;


            //agregamos los eventos
            TBContenido.TextChanged += TBContenido_TextChanged;
            TBContenido.SelectionChanged += TBContenido_SelectionChanged;

            //agregamos los elementos
            this.Controls.Add(TBContenido);
            this.Controls.Add(panel);

            //creamos el boton cerrar
            LidorSystems.IntegralUI.Controls.CommandButton closeButton = new LidorSystems.IntegralUI.Controls.CommandButton();
            closeButton.ImageIndex = 0;
            closeButton.Key = "TAB_CLOSE";

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

        public bool esModificado()
        {
            return modificado;
        }

        public Boolean guardarArchivo()
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

        public void guardarComoArchivo()
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
