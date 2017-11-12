namespace C3DCombiner
{
    partial class FMain
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nuevoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oCLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.treeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirCarpetaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarComoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.herramientasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.diagramaUMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ejecutarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.codigoCompartidoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iniciarSesionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cerrarSesionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compartirClaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modulosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reporteASTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.TabsNotificaciones = new System.Windows.Forms.TabControl();
            this.TabConsola = new System.Windows.Forms.TabPage();
            this.TabErrores = new System.Windows.Forms.TabPage();
            this.TabOptimizacion = new System.Windows.Forms.TabPage();
            this.Tab3d = new System.Windows.Forms.TabPage();
            this.Tab3dOptimizado = new System.Windows.Forms.TabPage();
            this.TabSimbolos = new System.Windows.Forms.TabPage();
            this.registrarseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.SuspendLayout();
            this.TabsNotificaciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem,
            this.herramientasToolStripMenuItem,
            this.ejecutarToolStripMenuItem,
            this.codigoCompartidoToolStripMenuItem,
            this.modulosToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(956, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevoToolStripMenuItem,
            this.abrirToolStripMenuItem,
            this.abrirCarpetaToolStripMenuItem,
            this.guardarToolStripMenuItem,
            this.guardarComoToolStripMenuItem,
            this.salirToolStripMenuItem});
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.archivoToolStripMenuItem.Text = "Archivo";
            // 
            // nuevoToolStripMenuItem
            // 
            this.nuevoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.oCLToolStripMenuItem,
            this.treeToolStripMenuItem,
            this.dToolStripMenuItem});
            this.nuevoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("nuevoToolStripMenuItem.Image")));
            this.nuevoToolStripMenuItem.Name = "nuevoToolStripMenuItem";
            this.nuevoToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.nuevoToolStripMenuItem.Text = "Nuevo";
            // 
            // oCLToolStripMenuItem
            // 
            this.oCLToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("oCLToolStripMenuItem.Image")));
            this.oCLToolStripMenuItem.Name = "oCLToolStripMenuItem";
            this.oCLToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.oCLToolStripMenuItem.Text = "OCL++";
            this.oCLToolStripMenuItem.Click += new System.EventHandler(this.OCLToolStripMenuItem_Click);
            // 
            // treeToolStripMenuItem
            // 
            this.treeToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("treeToolStripMenuItem.Image")));
            this.treeToolStripMenuItem.Name = "treeToolStripMenuItem";
            this.treeToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.treeToolStripMenuItem.Text = "Tree";
            this.treeToolStripMenuItem.Click += new System.EventHandler(this.TreeToolStripMenuItem_Click);
            // 
            // dToolStripMenuItem
            // 
            this.dToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("dToolStripMenuItem.Image")));
            this.dToolStripMenuItem.Name = "dToolStripMenuItem";
            this.dToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.dToolStripMenuItem.Text = "3D";
            this.dToolStripMenuItem.Click += new System.EventHandler(this.DToolStripMenuItem_Click);
            // 
            // abrirToolStripMenuItem
            // 
            this.abrirToolStripMenuItem.Image = global::C3DCombiner.Properties.Resources.Abrir;
            this.abrirToolStripMenuItem.Name = "abrirToolStripMenuItem";
            this.abrirToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.abrirToolStripMenuItem.Text = "Abrir";
            this.abrirToolStripMenuItem.Click += new System.EventHandler(this.AbrirToolStripMenuItem_Click);
            // 
            // abrirCarpetaToolStripMenuItem
            // 
            this.abrirCarpetaToolStripMenuItem.Image = global::C3DCombiner.Properties.Resources.Carpeta2;
            this.abrirCarpetaToolStripMenuItem.Name = "abrirCarpetaToolStripMenuItem";
            this.abrirCarpetaToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.abrirCarpetaToolStripMenuItem.Text = "Abrir carpeta";
            this.abrirCarpetaToolStripMenuItem.Click += new System.EventHandler(this.AbrirCarpetaToolStripMenuItem_Click);
            // 
            // guardarToolStripMenuItem
            // 
            this.guardarToolStripMenuItem.Image = global::C3DCombiner.Properties.Resources.Guardar;
            this.guardarToolStripMenuItem.Name = "guardarToolStripMenuItem";
            this.guardarToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.guardarToolStripMenuItem.Text = "Guardar";
            this.guardarToolStripMenuItem.Click += new System.EventHandler(this.GuardarToolStripMenuItem_Click);
            // 
            // guardarComoToolStripMenuItem
            // 
            this.guardarComoToolStripMenuItem.Image = global::C3DCombiner.Properties.Resources.GuardarComo2;
            this.guardarComoToolStripMenuItem.Name = "guardarComoToolStripMenuItem";
            this.guardarComoToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.guardarComoToolStripMenuItem.Text = "Guardar Como";
            this.guardarComoToolStripMenuItem.Click += new System.EventHandler(this.GuardarComoToolStripMenuItem_Click);
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Image = global::C3DCombiner.Properties.Resources.Salir;
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.salirToolStripMenuItem.Text = "Salir";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.SalirToolStripMenuItem_Click);
            // 
            // herramientasToolStripMenuItem
            // 
            this.herramientasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.diagramaUMLToolStripMenuItem});
            this.herramientasToolStripMenuItem.Name = "herramientasToolStripMenuItem";
            this.herramientasToolStripMenuItem.Size = new System.Drawing.Size(90, 20);
            this.herramientasToolStripMenuItem.Text = "Herramientas";
            // 
            // diagramaUMLToolStripMenuItem
            // 
            this.diagramaUMLToolStripMenuItem.Name = "diagramaUMLToolStripMenuItem";
            this.diagramaUMLToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.diagramaUMLToolStripMenuItem.Text = "Diagrama UML";
            this.diagramaUMLToolStripMenuItem.Click += new System.EventHandler(this.DiagramaUMLToolStripMenuItem_Click);
            // 
            // ejecutarToolStripMenuItem
            // 
            this.ejecutarToolStripMenuItem.Name = "ejecutarToolStripMenuItem";
            this.ejecutarToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.ejecutarToolStripMenuItem.Text = "Ejecutar";
            this.ejecutarToolStripMenuItem.Click += new System.EventHandler(this.EjecutarToolStripMenuItem_Click);
            // 
            // codigoCompartidoToolStripMenuItem
            // 
            this.codigoCompartidoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iniciarSesionToolStripMenuItem,
            this.cerrarSesionToolStripMenuItem,
            this.compartirClaseToolStripMenuItem,
            this.registrarseToolStripMenuItem});
            this.codigoCompartidoToolStripMenuItem.Name = "codigoCompartidoToolStripMenuItem";
            this.codigoCompartidoToolStripMenuItem.Size = new System.Drawing.Size(125, 20);
            this.codigoCompartidoToolStripMenuItem.Text = "Codigo Compartido";
            // 
            // iniciarSesionToolStripMenuItem
            // 
            this.iniciarSesionToolStripMenuItem.Name = "iniciarSesionToolStripMenuItem";
            this.iniciarSesionToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.iniciarSesionToolStripMenuItem.Text = "Iniciar sesion";
            this.iniciarSesionToolStripMenuItem.Click += new System.EventHandler(this.iniciarSesionToolStripMenuItem_Click);
            // 
            // cerrarSesionToolStripMenuItem
            // 
            this.cerrarSesionToolStripMenuItem.Name = "cerrarSesionToolStripMenuItem";
            this.cerrarSesionToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.cerrarSesionToolStripMenuItem.Text = "Cerrar sesion";
            this.cerrarSesionToolStripMenuItem.Click += new System.EventHandler(this.cerrarSesionToolStripMenuItem_Click);
            // 
            // compartirClaseToolStripMenuItem
            // 
            this.compartirClaseToolStripMenuItem.Name = "compartirClaseToolStripMenuItem";
            this.compartirClaseToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.compartirClaseToolStripMenuItem.Text = "Compartir clase";
            this.compartirClaseToolStripMenuItem.Click += new System.EventHandler(this.compartirClaseToolStripMenuItem_Click);
            // 
            // modulosToolStripMenuItem
            // 
            this.modulosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reporteASTToolStripMenuItem});
            this.modulosToolStripMenuItem.Name = "modulosToolStripMenuItem";
            this.modulosToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.modulosToolStripMenuItem.Text = "Reportes";
            // 
            // reporteASTToolStripMenuItem
            // 
            this.reporteASTToolStripMenuItem.Name = "reporteASTToolStripMenuItem";
            this.reporteASTToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.reporteASTToolStripMenuItem.Text = "Reporte AST";
            this.reporteASTToolStripMenuItem.Click += new System.EventHandler(this.reporteASTToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.TabsNotificaciones);
            this.splitContainer1.Size = new System.Drawing.Size(956, 649);
            this.splitContainer1.SplitterDistance = 444;
            this.splitContainer1.TabIndex = 1;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Size = new System.Drawing.Size(956, 444);
            this.splitContainer2.SplitterDistance = 197;
            this.splitContainer2.TabIndex = 0;
            // 
            // TabsNotificaciones
            // 
            this.TabsNotificaciones.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.TabsNotificaciones.Controls.Add(this.TabConsola);
            this.TabsNotificaciones.Controls.Add(this.TabErrores);
            this.TabsNotificaciones.Controls.Add(this.TabOptimizacion);
            this.TabsNotificaciones.Controls.Add(this.Tab3d);
            this.TabsNotificaciones.Controls.Add(this.Tab3dOptimizado);
            this.TabsNotificaciones.Controls.Add(this.TabSimbolos);
            this.TabsNotificaciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabsNotificaciones.Location = new System.Drawing.Point(0, 0);
            this.TabsNotificaciones.Name = "TabsNotificaciones";
            this.TabsNotificaciones.SelectedIndex = 0;
            this.TabsNotificaciones.Size = new System.Drawing.Size(956, 201);
            this.TabsNotificaciones.TabIndex = 0;
            // 
            // TabConsola
            // 
            this.TabConsola.Location = new System.Drawing.Point(4, 4);
            this.TabConsola.Name = "TabConsola";
            this.TabConsola.Padding = new System.Windows.Forms.Padding(3);
            this.TabConsola.Size = new System.Drawing.Size(948, 175);
            this.TabConsola.TabIndex = 0;
            this.TabConsola.Text = "Consola de Salida";
            this.TabConsola.UseVisualStyleBackColor = true;
            // 
            // TabErrores
            // 
            this.TabErrores.Location = new System.Drawing.Point(4, 4);
            this.TabErrores.Name = "TabErrores";
            this.TabErrores.Padding = new System.Windows.Forms.Padding(3);
            this.TabErrores.Size = new System.Drawing.Size(948, 175);
            this.TabErrores.TabIndex = 1;
            this.TabErrores.Text = "Errores";
            this.TabErrores.UseVisualStyleBackColor = true;
            // 
            // TabOptimizacion
            // 
            this.TabOptimizacion.Location = new System.Drawing.Point(4, 4);
            this.TabOptimizacion.Name = "TabOptimizacion";
            this.TabOptimizacion.Padding = new System.Windows.Forms.Padding(3);
            this.TabOptimizacion.Size = new System.Drawing.Size(948, 175);
            this.TabOptimizacion.TabIndex = 2;
            this.TabOptimizacion.Text = "Salida de optimizacion";
            this.TabOptimizacion.UseVisualStyleBackColor = true;
            // 
            // Tab3d
            // 
            this.Tab3d.Location = new System.Drawing.Point(4, 4);
            this.Tab3d.Name = "Tab3d";
            this.Tab3d.Padding = new System.Windows.Forms.Padding(3);
            this.Tab3d.Size = new System.Drawing.Size(948, 175);
            this.Tab3d.TabIndex = 3;
            this.Tab3d.Text = "Codigo 3D";
            this.Tab3d.UseVisualStyleBackColor = true;
            // 
            // Tab3dOptimizado
            // 
            this.Tab3dOptimizado.Location = new System.Drawing.Point(4, 4);
            this.Tab3dOptimizado.Name = "Tab3dOptimizado";
            this.Tab3dOptimizado.Padding = new System.Windows.Forms.Padding(3);
            this.Tab3dOptimizado.Size = new System.Drawing.Size(948, 175);
            this.Tab3dOptimizado.TabIndex = 4;
            this.Tab3dOptimizado.Text = "Codigo 3D Optimizado";
            this.Tab3dOptimizado.UseVisualStyleBackColor = true;
            // 
            // TabSimbolos
            // 
            this.TabSimbolos.Location = new System.Drawing.Point(4, 4);
            this.TabSimbolos.Name = "TabSimbolos";
            this.TabSimbolos.Padding = new System.Windows.Forms.Padding(3);
            this.TabSimbolos.Size = new System.Drawing.Size(948, 175);
            this.TabSimbolos.TabIndex = 5;
            this.TabSimbolos.Text = "Tabla de Simbolos";
            this.TabSimbolos.UseVisualStyleBackColor = true;
            // 
            // registrarseToolStripMenuItem
            // 
            this.registrarseToolStripMenuItem.Name = "registrarseToolStripMenuItem";
            this.registrarseToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.registrarseToolStripMenuItem.Text = "Registrarse";
            this.registrarseToolStripMenuItem.Click += new System.EventHandler(this.registrarseToolStripMenuItem_Click);
            // 
            // FMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(956, 673);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FMain";
            this.Text = "C3D Combiner";
            this.Load += new System.EventHandler(this.FMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.TabsNotificaciones.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ToolStripMenuItem nuevoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirCarpetaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem guardarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem guardarComoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.TabControl TabsNotificaciones;
        private System.Windows.Forms.TabPage TabConsola;
        private System.Windows.Forms.TabPage TabErrores;
        private System.Windows.Forms.TabPage TabOptimizacion;
        private System.Windows.Forms.TabPage Tab3d;
        private System.Windows.Forms.TabPage Tab3dOptimizado;
        private System.Windows.Forms.ToolStripMenuItem oCLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem treeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem herramientasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem diagramaUMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ejecutarToolStripMenuItem;
        private System.Windows.Forms.TabPage TabSimbolos;
        private System.Windows.Forms.ToolStripMenuItem codigoCompartidoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iniciarSesionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cerrarSesionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem compartirClaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modulosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reporteASTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem registrarseToolStripMenuItem;
    }
}

