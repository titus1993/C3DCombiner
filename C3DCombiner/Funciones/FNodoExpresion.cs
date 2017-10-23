using C3DCombiner.Ejecucion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3DCombiner.Funciones
{
    class FNodoExpresion
    {
        public Simbolo Padre;
        public FNodoExpresion Hermano;

        public FNodoExpresion Izquierda, Derecha;
        public String Tipo, Nombre;
        public int Fila, Columna;
        public int Entero;
        public double Decimal;
        public char Caracter;
        public String Cadena;
        public Boolean Bool;
        public FLlamadaObjeto LlamadaObjeto;
        public FNuevo Nuevo;

        public FNodoExpresion(FNodoExpresion nodo)
        {
            this.Izquierda = nodo.Izquierda;
            this.Derecha = nodo.Derecha;
            this.Bool = nodo.Bool;
            this.Cadena = nodo.Cadena;
            this.Caracter = nodo.Caracter;
            this.Columna = nodo.Columna;
            this.Decimal = nodo.Decimal;
            this.Entero = nodo.Entero;
            this.Fila = nodo.Fila;
            this.Nombre = nodo.Nombre;
            this.LlamadaObjeto = nodo.LlamadaObjeto;
            this.Tipo = nodo.Tipo;
            this.Nuevo = nodo.Nuevo;
        }

        public FNodoExpresion(FNodoExpresion izq, FNodoExpresion der, String tipo, String nombre, int fila, int columna, Object valor)
        {
            this.Izquierda = izq;
            this.Derecha = der;
            this.Tipo = tipo;
            this.Nombre = nombre;
            this.Fila = fila;
            this.Columna = columna;

            switch (tipo)
            {
                case Constante.TEntero:
                    this.Entero = Int32.Parse(valor.ToString());
                    this.Cadena = valor.ToString();
                    break;

                case Constante.TDecimal:
                    this.Decimal = Double.Parse(valor.ToString());
                    this.Cadena = valor.ToString();
                    break;

                case Constante.TCaracter:
                    this.Caracter = valor.ToString()[0];
                    this.Cadena = valor.ToString();
                    this.Entero = this.Caracter;
                    break;

                case Constante.TCadena:
                    this.Cadena = (String)valor;
                    break;

                case Constante.TBooleano:
                    this.Cadena = valor.ToString();
                    if (this.Cadena.Equals(Constante.TTrue))
                    {
                        this.Bool = true;
                        this.Entero = 1;
                        this.Decimal = 1;
                    }
                    else
                    {
                        this.Bool = false;
                        this.Entero = 0;
                        this.Decimal = 0;
                    }
                    break;

                case Constante.LLAMADA_OBJETO:
                    {
                        this.LlamadaObjeto = (FLlamadaObjeto)valor;
                    }
                    break;

                case Constante.TNuevo:
                    {
                        this.Nuevo = (FNuevo)valor;
                    }
                    break;

                /*case Constante.TAls:
                    this.Objeto = (FLlamadaObjeto)valor;
                    break;

                case Constante.TObjeto:
                    this.Obj = (Objeto)valor;
                    break;

                case Constante.TArreglo:
                    this.Arreglo = (FNodoArreglo)valor;
                    break;

                case Constante.TVariableArreglo:
                    this.ArregloResuelto = (Arreglo)valor;
                    break;

                case Constante.TColumna:
                    this.Col = (FNodoExpresion)valor;
                    break;*/
            }
        }

        public FNodoExpresion ResolverExpresion()
        {
            return ResolverExpresion(this);
        }

        private FNodoExpresion ResolverExpresion(FNodoExpresion nodo)
        {
            FNodoExpresion aux = new FNodoExpresion(null, null, Constante.TErrorSemantico, Constante.TErrorSemantico, nodo.Fila, nodo.Columna, null);
            FNodoExpresion izq = nodo.Izquierda;
            FNodoExpresion der = nodo.Derecha;
            if (nodo.Izquierda != null)
            {
                izq = nodo.Izquierda.ResolverExpresion();
                /*if (izq.Tipo.Equals(Constante.TVariableArreglo))
                {
                    izq = izq.PosArreglo;
                }*/
            }
            if (nodo.Derecha != null)
            {
                der = nodo.Derecha.ResolverExpresion();
                /*if (der.Tipo.Equals(Constante.TVariableArreglo))
                {
                    der = der.PosArreglo;
                }*/
            }
            switch (nodo.Tipo)
            {
                case Constante.TMas:
                    aux = Suma(izq, der);
                    break;

                case Constante.TMenos:
                    if (nodo.Izquierda != null)
                    {
                        aux = Resta(izq, der);
                    }
                    else
                    {
                        aux = Resta(der);
                    }
                    break;

                case Constante.TPor:
                    aux = Multiplicacion(izq, der);
                    break;

                case Constante.TDivision:
                    aux = Division(izq, der);
                    break;

                case Constante.TPotencia:
                    aux = Potencia(izq, der);
                    break;

                case Constante.TPotenciaOCL:
                    aux = Potencia(izq, der);
                    break;

                case Constante.TAumento:
                    aux = Aumento(izq);
                    break;

                case Constante.TDecremento:
                    aux = Disminucion(izq);
                    break;

                case Constante.TMayor:
                    aux = Mayor(izq, der);
                    break;

                case Constante.TMenor:
                    aux = Menor(izq, der);
                    break;

                case Constante.TMayorIgual:
                    aux = MayorIgual(izq, der);
                    break;

                case Constante.TMenorIgual:
                    aux = MenorIgual(izq, der);
                    break;

                case Constante.TIgualacion:
                    aux = Igual(izq, der);
                    break;

                case Constante.TDiferenciacion:
                    aux = Diferente(izq, der);
                    break;

                case Constante.TOr:
                    aux = Or(izq, der);
                    break;

                case Constante.TXor:
                    aux = Xor(izq, der);
                    break;

                case Constante.TAnd:
                    aux = And(izq, der);
                    break;

                case Constante.TNot:
                    aux = Not(der);
                    break;

                case Constante.TEntero:
                    aux = new FNodoExpresion(nodo);
                    break;

                case Constante.TDecimal:
                    aux = new FNodoExpresion(nodo);
                    break;

                case Constante.TCaracter:
                    aux = new FNodoExpresion(nodo);
                    break;

                case Constante.TCadena:
                    aux = new FNodoExpresion(nodo);
                    break;

                case Constante.TBooleano:
                    aux = new FNodoExpresion(nodo);
                    break;

                /*case Constante.TArreglo:
                    FNodoExpresion nuevo = new FNodoExpresion(null, null, Constante.TArreglo, Constante.TArreglo, Fila, Columna, new FNodoArreglo(new ArrayList<>()));
                    for (FNodoExpresion exp : this.Arreglo.Arreglo)
                    {
                        FNodoExpresion a = exp.ResolverExpresion(tabla, pos);
                        if (a.Tipo.Equals(Constante.TVariableArreglo))
                        {
                            a = a.PosArreglo;
                        }
                        nuevo.Arreglo.Arreglo.add(a.ResolverExpresion(tabla, pos));
                    }
                    int dimension = 0;
                    if (nuevo.Arreglo.Arreglo.get(0).Tipo.Equals(Constante.TArreglo))
                    {
                        dimension = nuevo.Arreglo.Arreglo.get(0).Arreglo.Arreglo.size();
                    }
                    else
                    {
                        dimension = 1;
                    }

                    boolean estado = true;
                    for (FNodoExpresion exp : nuevo.Arreglo.Arreglo)
                    {
                        if (exp != null)
                        {
                            if (exp.Tipo.Equals(Constante.TArreglo))
                            {
                                if (!(exp.Arreglo.Arreglo.size() == dimension))
                                {
                                    estado = false;
                                }
                            }
                        }
                        else
                        {
                            TitusTools.InsertarError(Constante.TErrorSemanticoSemantico, "No se puede asignar un valor null a un arreglo", Fila, Columna);
                        }
                    }

                    if (estado)
                    {
                        nuevo.Arreglo.Dimensiones = nuevo.Arreglo.Arreglo.size();
                        aux = nuevo;
                    }
                    else
                    {
                        TitusTools.InsertarError(Constante.TErrorSemanticoSemantico, "El arreglo no tiene las mismas dimensiones", Fila, Columna);
                    }
                    break;

                case Constante.TAls:
                    FLlamadaObjeto llamada = (FLlamadaObjeto)this.Objeto;
                    Variable valor = llamada.Ejecutar(tabla, tabla, pos);
                    if (valor != null)
                    {
                        aux = (FNodoExpresion)valor.Valor;
                        if (valor.Rol.Equals(Constante.TVariableArreglo))
                        {
                            aux.PosArreglo = aux.ArregloResuelto.ObtenerValor(llamada.LlamadaArreglo, tabla);
                        }
                    }
                    break;

                case Constante.TVariable:
                    break;

                case Constante.TVariableArreglo:
                    aux = nodo;
                    break;

                case Constante.TNuevo:
                    aux = new FNodoExpresion(nodo);
                    break;

                case Constante.TObjeto:
                    aux = nodo;
                    break;*/
                
            }
            return aux;
        }

        /*public Objeto Nuevo(Archivo archivo)
        {
            Objeto nuevo = new Objeto();
            FAls als = nuevo.BuscarAls(this.Tipo, archivo);
            if (als != null)
            {
                return new Objeto(als);
            }
            return null;
        }*/

        public FNodoExpresion Suma(FNodoExpresion izq, FNodoExpresion der)
        {
            FNodoExpresion aux = new FNodoExpresion(null, null, Constante.TErrorSemantico, Constante.TErrorSemantico, Fila, Columna, null);

            switch (izq.Tipo)
            {
                case Constante.TEntero:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, izq.Entero + der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Entero + der.Decimal);

                            break;

                        case Constante.TCaracter:
                            aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, izq.Entero + der.Caracter);

                            break;

                        case Constante.TCadena:
                            aux = new FNodoExpresion(null, null, Constante.TCadena, Constante.TCadena, Fila, Columna, izq.Entero.ToString() + der.Cadena);

                            break;

                        case Constante.TBooleano:
                            aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, izq.Entero + der.Entero);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede +, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                case Constante.TDecimal:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Decimal + der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Decimal + der.Decimal);

                            break;

                        case Constante.TCaracter:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Decimal + der.Caracter);

                            break;

                        case Constante.TCadena:
                            aux = new FNodoExpresion(null, null, Constante.TCadena, Constante.TCadena, Fila, Columna, izq.Decimal.ToString() + der.Cadena);

                            break;

                        case Constante.TBooleano:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Decimal + der.Entero);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede +, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                case Constante.TCaracter:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, izq.Caracter + der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Caracter + der.Decimal);

                            break;

                        case Constante.TCaracter:
                            aux = new FNodoExpresion(null, null, Constante.TCadena, Constante.TCadena, Fila, Columna, izq.Caracter.ToString() + der.Caracter.ToString());

                            break;

                        case Constante.TCadena:
                            aux = new FNodoExpresion(null, null, Constante.TCadena, Constante.TCadena, Fila, Columna, izq.Caracter.ToString() + der.Cadena);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede +, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                case Constante.TCadena:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TCadena, Constante.TCadena, Fila, Columna, izq.Cadena +der.Entero.ToString());
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TCadena, Constante.TCadena, Fila, Columna, izq.Cadena + der.Decimal.ToString());
                            break;

                        case Constante.TCaracter:
                            aux = new FNodoExpresion(null, null, Constante.TCadena, Constante.TCadena, Fila, Columna, izq.Cadena + der.Caracter.ToString());

                            break;

                        case Constante.TCadena:
                            aux = new FNodoExpresion(null, null, Constante.TCadena, Constante.TCadena, Fila, Columna, izq.Cadena + der.Cadena);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede +, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                case Constante.TBooleano:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, izq.Entero + der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Entero + der.Decimal);

                            break;

                        case Constante.TBooleano:
                            aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, izq.Bool || der.Bool);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede +, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

            }
            return aux;
        }

        public FNodoExpresion Resta(FNodoExpresion izq, FNodoExpresion der)
        {
            FNodoExpresion aux = new FNodoExpresion(null, null, Constante.TErrorSemantico, Constante.TErrorSemantico, Fila, Columna, null);

            switch (izq.Tipo)
            {
                case Constante.TEntero:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, izq.Entero - der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Entero - der.Decimal);

                            break;

                        case Constante.TCaracter:
                            aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, izq.Entero - der.Caracter);

                            break;

                        case Constante.TBooleano:
                            aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, izq.Entero - der.Entero);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede -, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                case Constante.TDecimal:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Decimal - der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Decimal - der.Decimal);

                            break;

                        case Constante.TCaracter:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Decimal - der.Caracter);

                            break;

                        case Constante.TBooleano:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Decimal - der.Entero);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede -, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                case Constante.TCaracter:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, izq.Caracter - der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Caracter - der.Decimal);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede -, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                case Constante.TBooleano:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, izq.Entero - der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Entero - der.Decimal);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede -, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

            }
            return aux;
        }

        public FNodoExpresion Resta(FNodoExpresion der)
        {
            FNodoExpresion aux = new FNodoExpresion(null, null, Constante.TErrorSemantico, Constante.TErrorSemantico, Fila, Columna, null);

            switch (der.Tipo)
            {
                case Constante.TEntero:
                    aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, -der.Entero);
                    break;

                case Constante.TDecimal:
                    aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, -der.Decimal);

                    break;

                case Constante.TCaracter:
                    aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, -der.Caracter);

                    break;

                case Constante.TBooleano:
                    aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, -der.Entero);
                    break;

                default:
                    TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede -, " + der.Tipo, "Aqui va ruta", Fila, Columna);
                    break;
            }
            return aux;
        }

        public FNodoExpresion Multiplicacion(FNodoExpresion izq, FNodoExpresion der)
        {
            FNodoExpresion aux = new FNodoExpresion(null, null, Constante.TErrorSemantico, Constante.TErrorSemantico, Fila, Columna, null);

            switch (izq.Tipo)
            {
                case Constante.TEntero:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, izq.Entero * der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Entero * der.Decimal);

                            break;

                        case Constante.TCaracter:
                            aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, izq.Entero * der.Caracter);

                            break;

                        case Constante.TBooleano:
                            aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, izq.Entero * der.Entero);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede *, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                case Constante.TDecimal:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Decimal * der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Decimal * der.Decimal);

                            break;

                        case Constante.TCaracter:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Decimal * der.Caracter);

                            break;

                        case Constante.TBooleano:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Decimal * der.Entero);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede *, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                case Constante.TCaracter:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, izq.Caracter * der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Caracter * der.Decimal);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede *, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                case Constante.TBooleano:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, izq.Entero * der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Entero * der.Decimal);

                            break;

                        case Constante.TBooleano:
                            aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, izq.Bool && der.Bool);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede *, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

            }
            return aux;
        }

        public FNodoExpresion Division(FNodoExpresion izq, FNodoExpresion der)
        {
            FNodoExpresion aux = new FNodoExpresion(null, null, Constante.TErrorSemantico, Constante.TErrorSemantico, Fila, Columna, null);

            switch (izq.Tipo)
            {
                case Constante.TEntero:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Entero / der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Entero / der.Decimal);

                            break;

                        case Constante.TCaracter:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Entero / der.Caracter);

                            break;

                        case Constante.TBooleano:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Entero / der.Entero);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede /, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                case Constante.TDecimal:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Decimal / der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Decimal / der.Decimal);

                            break;

                        case Constante.TCaracter:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Decimal / der.Caracter);

                            break;

                        case Constante.TBooleano:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Decimal / der.Entero);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede /, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                case Constante.TCaracter:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Caracter / der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Caracter / der.Decimal);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede /, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                case Constante.TBooleano:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Entero / der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Entero / der.Decimal);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede /, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

            }
            return aux;
        }

        public FNodoExpresion Potencia(FNodoExpresion izq, FNodoExpresion der)
        {
            FNodoExpresion aux = new FNodoExpresion(null, null, Constante.TErrorSemantico, Constante.TErrorSemantico, Fila, Columna, null);

            switch (izq.Tipo)
            {
                case Constante.TEntero:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            double p = Math.Pow(izq.Entero, der.Entero);
                            int v = (int)p;
                            aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, v);

                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, Math.Pow(izq.Entero, der.Decimal));

                            break;

                        case Constante.TCaracter:
                            double pc = Math.Pow(izq.Entero, der.Entero);
                            int vc = (int)pc;
                            aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, vc);

                            break;

                        case Constante.TBooleano:
                            double pb = Math.Pow(izq.Entero, der.Entero);
                            int vb = (int)pb;
                            aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, vb);
                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede ^, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                case Constante.TDecimal:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, Math.Pow(izq.Decimal, der.Entero));
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, Math.Pow(izq.Decimal, der.Decimal));

                            break;

                        case Constante.TCaracter:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, Math.Pow(izq.Decimal, der.Caracter));

                            break;

                        case Constante.TBooleano:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, Math.Pow(izq.Decimal, der.Entero));

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede ^, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                case Constante.TCaracter:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            double pe = Math.Pow(izq.Entero, der.Entero);
                            int ve = (int)pe;
                            aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, ve);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, Math.Pow(izq.Caracter, der.Decimal));

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede ^, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                case Constante.TBooleano:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            double p = Math.Pow(izq.Entero, der.Entero);
                            int v = (int)p;
                            aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, v);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, Math.Pow(izq.Entero, der.Decimal));

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede ^, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

            }
            return aux;
        }

        public FNodoExpresion Aumento(FNodoExpresion izq)
        {
            FNodoExpresion aux = new FNodoExpresion(null, null, Constante.TErrorSemantico, Constante.TErrorSemantico, Fila, Columna, null);

            switch (izq.Tipo)
            {
                case Constante.TEntero:
                    aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, izq.Entero + 1);
                    break;

                case Constante.TDecimal:
                    aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Decimal + 1);
                    break;

                case Constante.TCaracter:
                    aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, izq.Caracter + 1);
                    break;

                default:
                    TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede ++, " + izq.Tipo, "Aqui va ruta", Fila, Columna);
                    break;

            }
            return aux;
        }

        public FNodoExpresion Disminucion(FNodoExpresion izq)
        {
            FNodoExpresion aux = new FNodoExpresion(null, null, Constante.TErrorSemantico, Constante.TErrorSemantico, Fila, Columna, null);

            switch (izq.Tipo)
            {
                case Constante.TEntero:
                    aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, izq.Entero - 1);
                    break;

                case Constante.TDecimal:
                    aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Decimal - 1);
                    break;

                case Constante.TCaracter:
                    aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, izq.Caracter - 1);
                    break;

                default:
                    TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede ++, " + izq.Tipo, "Aqui va ruta", Fila, Columna);
                    break;

            }
            return aux;
        }

        public FNodoExpresion Mayor(FNodoExpresion izq, FNodoExpresion der)
        {
            FNodoExpresion aux = new FNodoExpresion(null, null, Constante.TErrorSemantico, Constante.TErrorSemantico, Fila, Columna, null);

            switch (izq.Tipo)
            {
                case Constante.TEntero:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Entero > der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Entero > der.Decimal);

                            break;

                        case Constante.TCaracter:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Entero > der.Caracter);

                            break;

                        case Constante.TBooleano:
                            aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, izq.Entero > der.Entero);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede >, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                case Constante.TDecimal:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Decimal > der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Decimal > der.Decimal);

                            break;

                        case Constante.TCaracter:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Decimal > der.Caracter);

                            break;

                        case Constante.TBooleano:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Decimal > der.Entero);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede >, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                case Constante.TCaracter:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Caracter > der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Caracter > der.Decimal);

                            break;

                        case Constante.TCaracter:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Caracter > der.Caracter);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede >, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                case Constante.TBooleano:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Entero > der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Entero > der.Decimal);

                            break;

                        case Constante.TBooleano:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Entero > der.Entero);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede >, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

            }
            return aux;
        }

        public FNodoExpresion Menor(FNodoExpresion izq, FNodoExpresion der)
        {
            FNodoExpresion aux = new FNodoExpresion(null, null, Constante.TErrorSemantico, Constante.TErrorSemantico, Fila, Columna, null);

            switch (izq.Tipo)
            {
                case Constante.TEntero:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Entero < der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Entero < der.Decimal);

                            break;

                        case Constante.TCaracter:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Entero < der.Caracter);

                            break;

                        case Constante.TBooleano:
                            aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, izq.Entero < der.Entero);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede <, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                case Constante.TDecimal:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Decimal < der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Decimal < der.Decimal);

                            break;

                        case Constante.TCaracter:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Decimal < der.Caracter);

                            break;

                        case Constante.TBooleano:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Decimal < der.Entero);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede <, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                case Constante.TCaracter:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Caracter < der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Caracter < der.Decimal);

                            break;

                        case Constante.TCaracter:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Caracter < der.Caracter);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede >, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                case Constante.TBooleano:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Entero < der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Entero < der.Decimal);

                            break;

                        case Constante.TBooleano:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Entero < der.Entero);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede <, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

            }
            return aux;
        }

        public FNodoExpresion Igual(FNodoExpresion izq, FNodoExpresion der)
        {
            FNodoExpresion aux = new FNodoExpresion(null, null, Constante.TErrorSemantico, Constante.TErrorSemantico, Fila, Columna, null);

            switch (izq.Tipo)
            {
                case Constante.TEntero:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Entero == der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Entero == der.Decimal);

                            break;

                        case Constante.TCaracter:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Entero == der.Caracter);

                            break;

                        case Constante.TBooleano:
                            aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, izq.Entero == der.Entero);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede ==, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                case Constante.TDecimal:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Decimal == der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Decimal == der.Decimal);

                            break;

                        case Constante.TCaracter:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Decimal == der.Caracter);

                            break;

                        case Constante.TBooleano:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Decimal == der.Entero);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede ==, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                case Constante.TCaracter:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Caracter == der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Caracter == der.Decimal);

                            break;

                        case Constante.TCaracter:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Caracter == der.Caracter);

                            break;

                        case Constante.TCadena:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Caracter.ToString().Equals(der.Cadena));

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede ==, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                case Constante.TCadena:
                    switch (der.Tipo)
                    {
                        case Constante.TCadena:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Cadena.Equals(der.Cadena));
                            break;

                        case Constante.TCaracter:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Cadena.Equals(der.Caracter.ToString()));

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede ==, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                case Constante.TBooleano:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Entero == der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Entero == der.Decimal);

                            break;

                        case Constante.TBooleano:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Bool == der.Bool);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede ==, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

            }
            return aux;
        }

        public FNodoExpresion Diferente(FNodoExpresion izq, FNodoExpresion der)
        {
            FNodoExpresion aux = new FNodoExpresion(null, null, Constante.TErrorSemantico, Constante.TErrorSemantico, Fila, Columna, null);

            switch (izq.Tipo)
            {
                case Constante.TEntero:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Entero != der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Entero != der.Decimal);

                            break;

                        case Constante.TCaracter:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Entero != der.Caracter);

                            break;

                        case Constante.TBooleano:
                            aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, izq.Entero != der.Entero);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede !=, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                case Constante.TDecimal:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Decimal != der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Decimal != der.Decimal);

                            break;

                        case Constante.TCaracter:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Decimal != der.Caracter);

                            break;

                        case Constante.TBooleano:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Decimal != der.Entero);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede !=, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                case Constante.TCaracter:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Caracter != der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Caracter != der.Decimal);

                            break;

                        case Constante.TCaracter:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Caracter != der.Caracter);

                            break;

                        case Constante.TCadena:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, !izq.Caracter.ToString().Equals(der.Cadena));

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede ==, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                case Constante.TCadena:
                    switch (der.Tipo)
                    {
                        case Constante.TCadena:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, !izq.Cadena.Equals(der.Cadena));
                            break;

                        case Constante.TCaracter:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, !izq.Cadena.Equals(der.Caracter.ToString()));

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede !=, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                case Constante.TBooleano:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Entero != der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Entero != der.Decimal);

                            break;

                        case Constante.TBooleano:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Bool != der.Bool);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede !=, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

            }
            return aux;
        }

        public FNodoExpresion MayorIgual(FNodoExpresion izq, FNodoExpresion der)
        {
            FNodoExpresion aux = new FNodoExpresion(null, null, Constante.TErrorSemantico, Constante.TErrorSemantico, Fila, Columna, null);
            FNodoExpresion mayor = Mayor(izq, der);
            FNodoExpresion igual = Igual(izq, der);

            switch (mayor.Tipo)
            {
                case Constante.TBooleano:
                    switch (igual.Tipo)
                    {
                        case Constante.TBooleano:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, mayor.Bool || igual.Bool);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede >=, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;
            }
            return aux;
        }

        public FNodoExpresion MenorIgual(FNodoExpresion izq, FNodoExpresion der)
        {
            FNodoExpresion aux = new FNodoExpresion(null, null, Constante.TErrorSemantico, Constante.TErrorSemantico, Fila, Columna, null);
            FNodoExpresion menor = Menor(izq, der);
            FNodoExpresion igual = Igual(izq, der);

            switch (menor.Tipo)
            {
                case Constante.TBooleano:
                    switch (igual.Tipo)
                    {
                        case Constante.TBooleano:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, menor.Bool || igual.Bool);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede <=, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;
            }
            return aux;
        }

        public FNodoExpresion Or(FNodoExpresion izq, FNodoExpresion der)
        {
            FNodoExpresion aux = new FNodoExpresion(null, null, Constante.TErrorSemantico, Constante.TErrorSemantico, Fila, Columna, null);

            switch (izq.Tipo)
            {
                case Constante.TBooleano:
                    switch (der.Tipo)
                    {
                        case Constante.TBooleano:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Bool || der.Bool);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede ||, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                default:
                    TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede ||, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                    break;
            }
            return aux;
        }

        public FNodoExpresion And(FNodoExpresion izq, FNodoExpresion der)
        {
            FNodoExpresion aux = new FNodoExpresion(null, null, Constante.TErrorSemantico, Constante.TErrorSemantico, Fila, Columna, null);

            switch (izq.Tipo)
            {
                case Constante.TBooleano:
                    switch (der.Tipo)
                    {
                        case Constante.TBooleano:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Bool && der.Bool);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede &&, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                default:
                    TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede &&, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                    break;
            }
            return aux;
        }

        public FNodoExpresion Not(FNodoExpresion der)
        {
            FNodoExpresion aux = new FNodoExpresion(null, null, Constante.TErrorSemantico, Constante.TErrorSemantico, Fila, Columna, null);

            switch (der.Tipo)
            {
                case Constante.TBooleano:
                    aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, !der.Bool);

                    break;

                default:
                    TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede !, " + der.Tipo, "Aqui va ruta", Fila, Columna);
                    break;
            }

            return aux;
        }

        public FNodoExpresion Xor(FNodoExpresion izq, FNodoExpresion der)
        {
            FNodoExpresion aux = new FNodoExpresion(null, null, Constante.TErrorSemantico, Constante.TErrorSemantico, Fila, Columna, null);

            switch (izq.Tipo)
            {
                case Constante.TBooleano:
                    switch (der.Tipo)
                    {
                        case Constante.TBooleano:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, (!izq.Bool && der.Bool) || (izq.Bool && !der.Bool));

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede XOR/??, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                default:
                    TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede XOR/??, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                    break;
            }
            return aux;
        }
    }
}
