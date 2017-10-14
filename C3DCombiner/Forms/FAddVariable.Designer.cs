namespace C3DCombiner.Forms
{
    partial class FAddVariable
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label3 = new System.Windows.Forms.Label();
            this.CBTipo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CBVisibilidad = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.TBNombre = new System.Windows.Forms.TextBox();
            this.TBObjeto = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(212, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Nombre objeto";
            // 
            // CBTipo
            // 
            this.CBTipo.FormattingEnabled = true;
            this.CBTipo.Items.AddRange(new object[] {
            "void",
            "int",
            "double",
            "char",
            "string",
            "bool",
            "objeto"});
            this.CBTipo.Location = new System.Drawing.Point(85, 59);
            this.CBTipo.Name = "CBTipo";
            this.CBTipo.Size = new System.Drawing.Size(121, 21);
            this.CBTipo.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Tipo de dato";
            // 
            // CBVisibilidad
            // 
            this.CBVisibilidad.FormattingEnabled = true;
            this.CBVisibilidad.Items.AddRange(new object[] {
            "publico",
            "privado",
            "protegido"});
            this.CBVisibilidad.Location = new System.Drawing.Point(85, 32);
            this.CBVisibilidad.Name = "CBVisibilidad";
            this.CBVisibilidad.Size = new System.Drawing.Size(121, 21);
            this.CBVisibilidad.TabIndex = 22;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "Visibilidad";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 24;
            this.label5.Text = "Nombre";
            // 
            // TBNombre
            // 
            this.TBNombre.Location = new System.Drawing.Point(85, 6);
            this.TBNombre.Name = "TBNombre";
            this.TBNombre.Size = new System.Drawing.Size(121, 20);
            this.TBNombre.TabIndex = 25;
            // 
            // TBObjeto
            // 
            this.TBObjeto.Location = new System.Drawing.Point(294, 59);
            this.TBObjeto.Name = "TBObjeto";
            this.TBObjeto.Size = new System.Drawing.Size(121, 20);
            this.TBObjeto.TabIndex = 26;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(43, 110);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(59, 17);
            this.checkBox1.TabIndex = 27;
            this.checkBox1.Text = "Arreglo";
            this.checkBox1.ThreeState = true;
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // FAddVariable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 253);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.TBObjeto);
            this.Controls.Add(this.TBNombre);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.CBVisibilidad);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.CBTipo);
            this.Controls.Add(this.label1);
            this.Name = "FAddVariable";
            this.Text = "FAddVariable";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox CBTipo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox CBVisibilidad;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TBNombre;
        private System.Windows.Forms.TextBox TBObjeto;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}