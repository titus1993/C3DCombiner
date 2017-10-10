using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;

namespace C3DCombiner.Tree
{
    class TitusNode : TreeNode
    {
        public TitusNode(String name, int imageIndex, int selectedImageIndex, String path)
        {
            this.Name = name;
            this.ImageIndex = imageIndex;
            this.SelectedImageIndex = selectedImageIndex;

            
        }

    }
}
