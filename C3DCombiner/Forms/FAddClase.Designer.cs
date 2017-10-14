namespace C3DCombiner
{
    partial class FAddClase
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.BEliminarVariable = new System.Windows.Forms.Button();
            this.BAgregarVariable = new System.Windows.Forms.Button();
            this.LBVariables = new System.Windows.Forms.ListBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.LBMetodos = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(417, 224);
            this.tabControl1.TabIndex = 10;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(409, 198);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Clase";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.BEliminarVariable);
            this.tabPage3.Controls.Add(this.BAgregarVariable);
            this.tabPage3.Controls.Add(this.LBVariables);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(409, 198);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Variables";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // BEliminarVariable
            // 
            this.BEliminarVariable.Location = new System.Drawing.Point(219, 133);
            this.BEliminarVariable.Name = "BEliminarVariable";
            this.BEliminarVariable.Size = new System.Drawing.Size(180, 25);
            this.BEliminarVariable.TabIndex = 13;
            this.BEliminarVariable.Text = "Eliminar";
            this.BEliminarVariable.UseVisualStyleBackColor = true;
            // 
            // BAgregarVariable
            // 
            this.BAgregarVariable.Location = new System.Drawing.Point(6, 133);
            this.BAgregarVariable.Name = "BAgregarVariable";
            this.BAgregarVariable.Size = new System.Drawing.Size(180, 25);
            this.BAgregarVariable.TabIndex = 12;
            this.BAgregarVariable.Text = "Agregar";
            this.BAgregarVariable.UseVisualStyleBackColor = true;
            // 
            // LBVariables
            // 
            this.LBVariables.FormattingEnabled = true;
            this.LBVariables.Location = new System.Drawing.Point(6, 6);
            this.LBVariables.Name = "LBVariables";
            this.LBVariables.Size = new System.Drawing.Size(393, 121);
            this.LBVariables.TabIndex = 11;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button2);
            this.tabPage2.Controls.Add(this.button3);
            this.tabPage2.Controls.Add(this.LBMetodos);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(409, 198);
            this.tabPage2.TabIndex = 3;
            this.tabPage2.Text = "Metodos";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(219, 133);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(180, 25);
            this.button2.TabIndex = 16;
            this.button2.Text = "Eliminar";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(6, 133);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(180, 25);
            this.button3.TabIndex = 15;
            this.button3.Text = "Agregar";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // LBMetodos
            // 
            this.LBMetodos.FormattingEnabled = true;
            this.LBMetodos.Location = new System.Drawing.Point(6, 6);
            this.LBMetodos.Name = "LBMetodos";
            this.LBMetodos.Size = new System.Drawing.Size(393, 121);
            this.LBMetodos.TabIndex = 14;
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.button1.Location = new System.Drawing.Point(0, 201);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(417, 23);
            this.button1.TabIndex = 15;
            this.button1.Text = "Agregar clase";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Nombre";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(81, 32);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(120, 20);
            this.textBox1.TabIndex = 15;
            // 
            // FAddClase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(417, 224);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tabControl1);
            this.Name = "FAddClase";
            this.Text = "FAddClase";
            this.Load += new System.EventHandler(this.FAddClase_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button BEliminarVariable;
        private System.Windows.Forms.Button BAgregarVariable;
        private System.Windows.Forms.ListBox LBVariables;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ListBox LBMetodos;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
    }
}