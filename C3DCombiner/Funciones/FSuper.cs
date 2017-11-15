using C3DCombiner.Ejecucion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3DCombiner.Funciones
{
    class FSuper
    {
        public List<FNodoExpresion> Parametros;

        public Simbolo Padre;

        public FSuper(List<FNodoExpresion> Parametros)
        {
            this.Parametros = Parametros;
            this.Padre = null;
        }


        public String Generar3D()
        {
            String cadena = "";

            Simbolo simclasepadre = Padre.BuscarClasePadre();

            FClase clasepadre = (FClase)simclasepadre.Valor;

            if (!clasepadre.Herencia.Equals(""))
            {
                Simbolo simclasehere = simclasepadre.BuscarClase(clasepadre.Herencia, clasepadre.ArchivoPadre);
                if (simclasehere != null)
                {
                    Simbolo simulacion = Padre.BuscarMetodoPadre();

                    String tempsimulacion = TitusTools.GetTemp();

                    cadena += "\t\t" + tempsimulacion + " = P + " + (simulacion.Ambito.Tamaño).ToString() + ";//simulacion de cambio de ambito \n";
                    String posstack = TitusTools.GetTemp();
                    cadena += "\t\t" + posstack + " = Stack[P];\n"; 
                    String nuevapos = TitusTools.GetTemp();
                    cadena += "\t\t" + nuevapos + " = " + posstack + " + " + clasepadre.Ambito.Tamaño.ToString() + ";//posicion en el this de la herencia\n";

                    List<Nodo3D> resueltas = new List<Nodo3D>();

                    int i = 2;
                    foreach (FNodoExpresion p in this.Parametros)
                    {

                        String temporalstack = TitusTools.GetTemp();

                        cadena += "\t\t" + temporalstack + " = " + tempsimulacion + " + " + i.ToString() + ";\n";
                        Nodo3D r = p.Generar3D();
                        if (r.Tipo.Equals(Constante.TBooleano))//conversion siviene de una relacional
                        {
                            if (r.V == "" && r.F == "")
                            {//si trae etiquetas viene de una relacional si no es un bool nativo

                            }
                            else
                            {
                                var cad = "";

                                var auxtemp = TitusTools.GetTemp();
                                var salida = TitusTools.GetEtq();

                                cad += "\t" + r.V + ":\n";
                                cad += "\t\t" + auxtemp + " = 1;\n";
                                cad += "\t\t" + "goto " + salida + ";\n";
                                cad += "\t" + r.F + ":\n";
                                cad += "\t\t" + auxtemp + " = 0;\n";
                                cad += "\t" + salida + ":\n";

                                r.Valor = auxtemp;
                                cad += "\t\t" + r.Valor + " =  - " + auxtemp + ";\n";
                                r.Codigo = r.Codigo + cad;
                            }
                        }


                        cadena += r.Codigo;
                        resueltas.Add(r);

                        //ahora asignamos al stack el valor resuelto
                        cadena += "\t\t" + "Stack[" + temporalstack + "] = " + r.Valor + ";//Asignacino del parametro\n";

                        i++;
                    }

                    

                    String este = TitusTools.GetTemp();

                    cadena += "\t\t" + este + " = " + tempsimulacion + " + 0;//metiendo el this\n";
                    cadena += "\t\t" + "Stack[" + este + "] = " + nuevapos + ";\n";
                    

                    if (!TitusTools.HayErrores())
                    {
                        Simbolo constructor = simclasehere.BuscarConstructor(simclasehere.Nombre, resueltas);

                        cadena += "\t\t" + "P = P + " + (simulacion.Ambito.Tamaño).ToString() + ";//cambio de ambito para llamar al constructor\n";
                        if (constructor != null)
                        {
                            FMetodo m = (FMetodo)constructor.Valor;
                            if (m.Parametros.Count == this.Parametros.Count)
                            {
                                cadena += "\t\t" + simclasehere.Nombre + "_constructor";
                                foreach (Simbolo p in m.Parametros)
                                {
                                    FParametro parametro = (FParametro)p.Valor;
                                    cadena += "_" + parametro.Tipo + parametro.Dimensiones.ToString();
                                }

                                cadena += "();\n";
                            }
                            else
                            {
                                TitusTools.InsertarError(Constante.TErrorSemantico, "No se encontro el constructor para la clase " + constructor.Nombre, TitusTools.GetRuta(), Padre.Fila, Padre.Columna);
                            }

                        }
                        else
                        {
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se encontro el constructor para la clase " + simclasehere.Nombre, TitusTools.GetRuta(), Padre.Fila, Padre.Columna);
                        }
                        cadena += "\t\t" + "P = P - " + (simulacion.Ambito.Tamaño).ToString() + ";//cambio de ambito para llamar al constructor\n";

                    }
                }
                else
                {
                    TitusTools.InsertarError(Constante.TErrorSemantico, "No existe ninguna clase " + clasepadre.Herencia, TitusTools.GetRuta(), Padre.Fila, Padre.Columna);
                }

            }
            else
            {
                TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede llamar la instruccion super porque no existe una herencia", TitusTools.GetRuta(), Padre.Fila, Padre.Columna);
            }


            return cadena;
            
        }

        public String Generar3DLlamada()
        {
            String cadena = "";

            return cadena;
        }
    }
}
