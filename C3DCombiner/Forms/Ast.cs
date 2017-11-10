using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C3DCombiner.Forms
{
    public partial class Ast : Form
    {
        public Ast()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Ast_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                ironyFCTB1.Grammar = new OLCGrammarAST();
            }
            else
            {
                ironyFCTB1.Grammar = new TreeGrammarAST();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ParseTreeNode nodo = ironyFCTB1.Parser.Parse(ironyFCTB1.Text).Root;
            if (ironyFCTB1.Parser.Root != null && ironyFCTB1.Parser.Parse(ironyFCTB1.Text).ParserMessages.Count == 0)
            {
                String path = GraficarAST.GenerarArbol(nodo);
                Thread.Sleep(2000);
                Image imagen = Image.FromFile(path);
                pictureBox1.Width = imagen.Width;
                pictureBox1.Height = imagen.Height;
                pictureBox1.Image = Image.FromFile(path);
            }
            else
            {
                MessageBox.Show("Existen errores en el codigo, no se puede generar el arbol.","Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
