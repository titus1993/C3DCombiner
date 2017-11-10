using C3DCombiner.Funciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C3DCombiner.Ejecucion
{
    class Ejecucion3D
    {
        Ambito Codigo3D;

        public Ejecucion3D(Ambito codigo3d)
        {
            this.Codigo3D = codigo3d;
        }


        public void Ejecutar()
        {
            PrimerPasada();
            if (!TitusTools.HayErrores())
            {
                MessageBox.Show("Ejecucion de codigo 3D finalizada con exito.", "Codigo 3D", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Se encontraron errores", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void PrimerPasada()
        {
            //Limpiamos las variables que utilizamos para la ejecucion
            Tabla3D.IniciarTabla();

            //buscamos el main y apartamos la primera posicion de la tabla de variables
            

            foreach (Simbolo sim in Codigo3D.TablaSimbolo)
            {
                if (sim.Rol.Equals(Constante.TMain))
                {
                    if (Tabla3D.Tabla.Count == 0)
                    {
                        Tabla3D.Tabla.Insert(0, new Variable(sim.Nombre, sim.Rol, sim.Fila, sim.Columna, sim.Ambito, sim.Valor));
                    }
                    else
                    {
                        TitusTools.InsertarError(Constante.TErrorSemantico, "Exite mas de un metodo main", "", 1, 1);
                    }
                }
                else
                {
                    Tabla3D.Tabla.Add(new Variable(sim.Nombre, sim.Rol, sim.Fila, sim.Columna, sim.Ambito, sim.Valor));
                }
            }

            if (!TitusTools.HayErrores())
            {
                if (Tabla3D.Tabla.Count > 0)
                {
                    if (!Tabla3D.Tabla[0].Rol.Equals(Constante.TMain))
                    {
                        TitusTools.InsertarError(Constante.TErrorSemantico, "No se encontro un metodo main", "", 1, 1);
                    }
                    else
                    {
                        FMetodo main = (FMetodo)Tabla3D.Tabla[0].Valor;
                        main.Ejecutar();
                    }
                }
            }
            

        }
    }
}
