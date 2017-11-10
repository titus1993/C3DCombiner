using C3DCombiner.Ejecucion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3DCombiner.Funciones
{
    class FCaso
    {
        public FNodoExpresion Valor;
        public Ambito Ambito;

        public Simbolo Padre;

        public FCaso(FNodoExpresion valor, Ambito ambito)
        {
            this.Valor = valor;
            this.Ambito = ambito;
        }
        public FCaso(Ambito ambito)
        {
            this.Ambito = ambito;
        }

        public String Generar3D()
        {
            String cadena = "";
            foreach (Simbolo sim in Ambito.TablaSimbolo)//cuerpo si es verdadero
            {
                cadena += sim.Generar3D();
            }

            return cadena;
        }

        public String Generar3D(Nodo3D izq)
        {
            String cadena = "";

            cadena = "\t\t//Comienza caso " + Valor.Cadena + "\n";
            Nodo3D cond = Relacional3D(izq, Valor, Valor.Fila, Valor.Columna);

            if (!TitusTools.HayErrores())
            {
                if (cond.Tipo == Constante.TBooleano)
                {
                    if (cond.V != "" || cond.F != "")
                    {
                        cadena += cond.Codigo;
                        foreach (Simbolo sim in Ambito.TablaSimbolo)//cuerpo si es verdadero
                        {
                            cadena += sim.Generar3D();
                        }
                    }
                    else
                    {
                        cond.F = TitusTools.GetEtq();
                        cadena += "\t\t" + "ifFalse " + cond.Valor + " == 1 goto " + cond.F + ";\n";

                        cadena += cond.Codigo;
                        foreach (Simbolo sim in Ambito.TablaSimbolo)//cuerpo si es verdadero
                        {
                            cadena += sim.Generar3D();
                        }
                    }

                    cadena += "\t" + cond.F + "://Termina caso " + Valor.Cadena + "\n";
                }
                else
                {
                    TitusTools.InsertarError(Constante.TErrorSemantico, "La sentencia caso esperaba un tipo booleano no un tipo " + cond.Tipo, TitusTools.GetRuta(), Padre.Fila, Padre.Columna);
                }
            }
            

            return cadena;
        }

        private Nodo3D Relacional3D(Nodo3D izq, FNodoExpresion der, int Fila, int Columna)
        {
            var codigo3d = new Nodo3D();
            var auxder = der.Generar3D();

            if (!TitusTools.HayErrores())
            {
                switch (izq.Tipo)
                {
                    case Constante.TEntero:
                        {
                            switch (auxder.Tipo)
                            {
                                case Constante.TEntero:
                                    {
                                        String cad = "";
                                        codigo3d.F = TitusTools.GetEtq();

                                        cad += "\t\t" + "ifFalse " + izq.Valor + " " + "==" + " " + auxder.Valor + " goto " + codigo3d.F + ";\n";

                                        codigo3d.Codigo = auxder.Codigo + cad;

                                        codigo3d.Tipo = Constante.TBooleano;
                                    }
                                    break;

                                case Constante.TDecimal:
                                    {
                                        String cad = "";
                                        codigo3d.F = TitusTools.GetEtq();

                                        cad += "\t\t" + "ifFalse " + izq.Valor + " " + "==" + " " + auxder.Valor + " goto " + codigo3d.F + ";\n";

                                        codigo3d.Codigo = auxder.Codigo + cad;

                                        codigo3d.Tipo = Constante.TBooleano;
                                    }
                                    break;

                                case Constante.TCaracter:
                                    {
                                        String cad = "";

                                        codigo3d.F = TitusTools.GetEtq();

                                        cad += "\t\t" + "ifFalse " + izq.Valor + " " + "==" + " " + auxder.Valor + " goto " + codigo3d.F + ";\n";

                                        codigo3d.Codigo = auxder.Codigo + cad;

                                        codigo3d.Tipo = Constante.TBooleano;
                                    }
                                    break;

                                default:
                                    TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede " + izq.Tipo + " " + "==" + " " + auxder.Tipo, TitusTools.GetRuta(), Fila, Columna);
                                    break;
                            }
                        }
                        break;

                    case Constante.TDecimal:
                        {
                            switch (auxder.Tipo)
                            {
                                case Constante.TEntero:
                                    {
                                        String cad = "";

                                        codigo3d.F = TitusTools.GetEtq();

                                        cad += "\t\t" + "ifFalse " + izq.Valor + " " + "==" + " " + auxder.Valor + " goto " + codigo3d.F + ";\n";

                                        codigo3d.Codigo = auxder.Codigo + cad;

                                        codigo3d.Tipo = Constante.TBooleano;
                                    }
                                    break;

                                case Constante.TDecimal:
                                    {
                                        String cad = "";

                                        codigo3d.F = TitusTools.GetEtq();

                                        cad += "\t\t" + "if " + izq.Valor + " " + "==" + " " + auxder.Valor + " goto " + codigo3d.F + ";\n";

                                        codigo3d.Codigo = auxder.Codigo + cad;

                                        codigo3d.Tipo = Constante.TBooleano;
                                    }
                                    break;

                                case Constante.TCaracter:
                                    {
                                        String cad = "";

                                        codigo3d.F = TitusTools.GetEtq();

                                        cad += "\t\t" + "if " + izq.Valor + " " + "==" + " " + auxder.Valor + " goto " + codigo3d.F + ";\n";

                                        codigo3d.Codigo = auxder.Codigo + cad;

                                        codigo3d.Tipo = Constante.TBooleano;
                                    }
                                    break;

                                default:
                                    TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede " + izq.Tipo + " " + "==" + " " + auxder.Tipo, TitusTools.GetRuta(), Fila, Columna);
                                    break;
                            }
                        }
                        break;

                    case Constante.TCaracter:
                        {
                            switch (auxder.Tipo)
                            {
                                case Constante.TEntero:
                                    {
                                        String cad = "";

                                        codigo3d.F = TitusTools.GetEtq();

                                        cad += "\t\t" + "if " + izq.Valor + " " + "==" + " " + auxder.Valor + " goto " + codigo3d.F + ";\n";

                                        codigo3d.Codigo = auxder.Codigo + cad;

                                        codigo3d.Tipo = Constante.TBooleano;
                                    }
                                    break;

                                case Constante.TDecimal:
                                    {
                                        String cad = "";

                                        codigo3d.F = TitusTools.GetEtq();

                                        cad += "\t\t" + "if " + izq.Valor + " " + "==" + " " + auxder.Valor + " goto " + codigo3d.F + ";\n";

                                        codigo3d.Codigo = auxder.Codigo + cad;

                                        codigo3d.Tipo = Constante.TBooleano;
                                    }
                                    break;

                                case Constante.TCaracter:
                                    {
                                        String cad = "";

                                        codigo3d.F = TitusTools.GetEtq();

                                        cad += "\t\t" + "if " + izq.Valor + " " + "==" + " " + auxder.Valor + " goto " + codigo3d.F + ";\n";

                                        codigo3d.Codigo = auxder.Codigo + cad;

                                        codigo3d.Tipo = Constante.TBooleano;
                                    }
                                    break;

                                case Constante.TCadena:
                                    {
                                        String cad = "";

                                        String etqcad1 = TitusTools.GetEtq();
                                        String etqcad2 = TitusTools.GetEtq();

                                        String tempascii2 = TitusTools.GetTemp();
                                        String tempcad2 = TitusTools.GetTemp();

                                        cad += "\t\t" + tempascii2 + " = 0 ;\n";
                                        cad += "\t" + etqcad1 + ":\n";
                                        cad += "\t\t" + tempcad2 + " = Heap[" + auxder.Valor + "];\n";
                                        cad += "\t\t" + "if " + tempcad2 + " == 0 goto " + etqcad2 + ";\n";
                                        cad += "\t\t" + tempascii2 + " = " + tempascii2 + " + " + tempcad2 + ";\n";
                                        cad += "\t\t" + auxder.Valor + " = " + auxder.Valor + " + 1;\n";
                                        cad += "\t\t" + "goto " + etqcad1 + ";\n";
                                        cad += "\t" + etqcad2 + ":\n";

                                        codigo3d.F = TitusTools.GetEtq();

                                        cad += "\t\t" + "if " + izq.Valor + " " + "==" + " " + tempascii2 + " goto " + codigo3d.F + ";\n";

                                        codigo3d.Codigo = auxder.Codigo + cad;

                                        codigo3d.Tipo = Constante.TBooleano;
                                    }
                                    break;

                                default:
                                    TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede " + izq.Tipo + " " + "==" + " " + auxder.Tipo, TitusTools.GetRuta(), Fila, Columna);
                                    break;
                            }
                        }
                        break;

                    case Constante.TCadena:
                        {
                            switch (auxder.Tipo)
                            {
                                case Constante.TCaracter:
                                    {
                                        String cad = "";

                                        String tempascii1 = TitusTools.GetTemp();
                                        String tempcad1 = TitusTools.GetTemp();
                                        String etqcad1 = TitusTools.GetEtq();
                                        String etqcad2 = TitusTools.GetEtq();

                                        cad += "\t\t" + tempascii1 + " = 0 ;\n";
                                        cad += "\t" + etqcad1 + ":\n";
                                        cad += "\t\t" + tempcad1 + " = Heap[" + izq.Valor + "];\n";
                                        cad += "\t\t" + "if " + tempcad1 + " == 0 goto " + etqcad2 + ";\n";
                                        cad += "\t\t" + tempascii1 + " = " + tempascii1 + " + " + tempcad1 + ";\n";
                                        cad += "\t\t" + izq.Valor + " = " + izq.Valor + " + 1;\n";
                                        cad += "\t\t" + "goto " + etqcad1 + ";\n";
                                        cad += "\t" + etqcad2 + ":\n";

                                        codigo3d.F = TitusTools.GetEtq();

                                        cad += "\t\t" + "if " + tempascii1 + " " + "==" + " " + auxder.Valor + " goto " + codigo3d.F + ";\n";

                                        codigo3d.Codigo = auxder.Codigo + cad;

                                        codigo3d.Tipo = Constante.TBooleano;
                                    }
                                    break;

                                case Constante.TCadena:
                                    {
                                        String cad = "";

                                        String tempascii1 = TitusTools.GetTemp();
                                        String tempcad1 = TitusTools.GetTemp();
                                        String etqcad1 = TitusTools.GetEtq();
                                        String etqcad2 = TitusTools.GetEtq();

                                        cad += "\t\t" + tempascii1 + " = 0 ;\n";
                                        cad += "\t" + etqcad1 + ":\n";
                                        cad += "\t\t" + tempcad1 + " = Heap[" + izq.Valor + "];\n";
                                        cad += "\t\t" + "if " + tempcad1 + " == 0 goto " + etqcad2 + ";\n";
                                        cad += "\t\t" + tempascii1 + " = " + tempascii1 + " + " + tempcad1 + ";\n";
                                        cad += "\t\t" + izq.Valor + " = " + izq.Valor + " + 1;\n";
                                        cad += "\t\t" + "goto " + etqcad1 + ";\n";
                                        cad += "\t" + etqcad2 + ":\n";

                                        String tempascii2 = TitusTools.GetTemp();
                                        String tempcad2 = TitusTools.GetTemp();
                                        etqcad1 = TitusTools.GetEtq();
                                        etqcad2 = TitusTools.GetEtq();

                                        cad += "\t\t" + tempascii2 + " = 0 ;\n";
                                        cad += "\t" + etqcad1 + ":\n";
                                        cad += "\t\t" + tempcad2 + " = Heap[" + auxder.Valor + "];\n";
                                        cad += "\t\t" + "if " + tempcad2 + " == 0 goto " + etqcad2 + ";\n";
                                        cad += "\t\t" + tempascii2 + " = " + tempascii2 + " + " + tempcad2 + ";\n";
                                        cad += "\t\t" + auxder.Valor + " = " + auxder.Valor + " + 1;\n";
                                        cad += "\t\t" + "goto " + etqcad1 + ";\n";
                                        cad += "\t" + etqcad2 + ":\n";

                                        codigo3d.F = TitusTools.GetEtq();

                                        cad += "\t\t" + "if " + tempascii1 + " " + "==" + " " + tempascii2 + " goto " + codigo3d.F + ";\n";

                                        codigo3d.Codigo = auxder.Codigo + cad;

                                        codigo3d.Tipo = Constante.TBooleano;
                                    }
                                    break;

                                default:
                                    TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede " + izq.Tipo + " " + "==" + " " + auxder.Tipo, TitusTools.GetRuta(), Fila, Columna);
                                    break;
                            }
                        }
                        break;
                }
            }
            return codigo3d;
        }
    }
}
