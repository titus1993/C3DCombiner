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
    public partial class FUml : Form
    {
        public FUml()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void FUml_Load(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {            
            switch (listView1.FocusedItem.Index)
            {
                case 0://clase
                    FAddClase clase = new FAddClase();
                    clase.Visible = true;
                    break;

                case 1://herencia 
                    break;

                case 2://composicion
                    break;

                case 3://agregacion
                    break;

                case 4://asociacion 
                    break;

                case 5://dependencia
                    break;
            }
        }
    }
}
