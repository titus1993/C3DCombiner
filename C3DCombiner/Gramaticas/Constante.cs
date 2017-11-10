using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3DCombiner
{
    static class Constante
    {
        public const String Entero = "enteroliteral";
        public const String Decimal = "decimalliteral";
        public const String Caracter = "caracterliteral";
        public const String Cadena = "cadenaliteral";
        public const String Booleano = "booleanoliteral";
        public const String TTrue = "true";
        public const String TFalse = "false";

        public const String Id = "id";

        public const String TEntero = "entero";
        public const String TDecimal = "decimal";
        public const String TCaracter = "caracter";
        public const String TCadena = "cadena";
        public const String TBooleano = "booleano";
        public const String TVoid = "void";

        public const String TPublico = "publico";
        public const String TPrivado = "privado";
        public const String TProtegido = "protegido";

        public const String TMas = "+";
        public const String TMenos = "-";
        public const String TPor = "*";
        public const String TDivision = "/";
        public const String TPotencia = "pow";
        public const String TPotenciaOCL = "^";
        public const String TAumento = "++";
        public const String TDecremento = "--";
        public const String TMayor = ">";
        public const String TMenor = "<";
        public const String TMayorIgual = ">=";
        public const String TMenorIgual = "<=";
        public const String TIgualacion = "==";
        public const String TDiferenciacion = "!=";
        public const String TAnd = "and";
        public const String TOr = "or";
        public const String TXor = "xor";
        public const String TNot = "not";

        public const String TAndOCL = "&&";
        public const String TOrOCL = "||";
        public const String TXorOCL = "??";
        public const String TNotOCL = "!";

        public const String TParetesis_Izq = "(";
        public const String TParentesis_Der = ")";
        public const String TCorchete_Izq = "[";
        public const String TCorchete_Der = "]";
        public const String TLlave_Izq = "{";
        public const String TLlave_Der = "}";
        public const String TDosPuntos = ":";
        public const String TComa = ",";
        public const String TPuntoComa = ";";
        public const String TPunto = ".";
        public const String TIgual = "=";
        public const String TAsignacion = "=>";

        public const String TLlamar = "llamar";
        public const String TImportar = "importar";
        public const String TClase = "clase";
        public const String TConstructor = "__constructor";
        public const String TMetodo = "metodo";
        public const String TFuncion = "funcion";
        public const String TRetorno = "retornar";
        public const String TNuevo = "nuevo";
        public const String TSuper = "super";
        public const String TSobrescribirTree = "/**Sobreescribir**/";
        public const String TSelf = "self";
        public const String TSi = "si";
        public const String TSino = "si_no";
        public const String TSinoSi = "si_no_si";
        public const String TSalir = "salir";
        public const String TElegir = "elegir caso";
        public const String TDefecto = "defecto";
        public const String TContinuar = "continuar";
        public const String TMientras = "mientras";
        public const String THacer = "hacer";
        public const String TRepetir = "repetir";
        public const String THasta = "hasta";
        public const String TPara = "para";
        public const String TLoop = "loop";
        public const String TOutString = "out_string";
        public const String TParseInt = "parseint";
        public const String TParseDouble = "parsedouble";
        public const String TIntToStr = "inttostr";
        public const String TDoubleToStr = "doubletostr";
        public const String TDoubleToInt = "doubletoint";

        public const String TSobrescribirOLC = "@Sobrescribir";
        public const String THereda = "hereda de";
        public const String TEste = "este";
        public const String TPrincipal = "principal";
        public const String TNew = "new";
        public const String TX = "X";
        public const String TUntil = "until";
        public const String TImprimir = "imprimir";



        public const String INICIO = "inicio";
        public const String LISTA_SENTENCIA = "lista_sentencia";
        public const String LISTA_SENTENCIAS = "lista_sentencias";
        public const String SENTENCIA = "sentencia";
        public const String LISTA_EXPS = "lista_exps";
        public const String LISTA_EXP = "lista_exp";
        public const String EXP = "exp";
        public const String LLAMADA_EXP = "llamada_exp";
        public const String DECLARACION = "declaracion";
        public const String ASIGNACION = "asignacion";
        public const String TIPO = "tipo";
        public const String LISTA_ID = "lista_id";
        public const String LISTA_DIMENSIONES = "lista_dimensiones";
        public const String DIMENSION = "dimension";
        public const String OBJETO = "objeto";
        public const String LLAMADA_FUNCION = "llamada_funcion";
        public const String HIJO = "hijo";
        public const String FUNCION = "funcion";
        public const String VISIBILIDAD = "visibilidad";
        public const String LISTA_PARAMETROS = "lista_parametros";
        public const String LISTA_PARAMETRO = "lista_parametro";
        public const String PARAMETRO = "parametro";
        public const String LISTA_INSTRUCCIONES = "lista_instrucciones";
        public const String LISTA_INSTRUCCION = "lista_instruccion";
        public const String INSTRUCCION = "instruccion";
        public const String DIMENSIONES_METODO = "dimensiones_metodo";
        public const String LISTA_DIMENSION_METODO = "lista_dimension_metodo";
        public const String DIMENSION_METODO = "dimension_metodo";
        public const String LISTA_CLASE = "lista_clase";
        public const String CLASE = "clase";
        public const String LLAMADA = "llamada";
        public const String SI = "si";
        public const String SINO = "sino";
        public const String LISTA_SINOSIS = "lista_sinosis";
        public const String LISTA_SINOSI = "lista_sinosi";
        public const String SINOSI = "sinosi";
        public const String ELEGIR = "elegir";
        public const String LISTA_CASOS = "lista_casos";
        public const String CASO = "caso";
        public const String DEFECTO = "defecto";
        public const String MIENTRAS = "mientras";
        public const String HACER = "hacer";
        public const String REPETIR = "repetir";
        public const String PARA = "para";
        public const String LOOP = "loop";
        public const String LITERALES = "literales";
        public const String LISTA_IMPORTAR = "lista_importar";
        public const String IMPORTAR = "importar";
        public const String LISTA_ARCHIVO = "lista_archivo";
        public const String ARCHIVO = "archivo";
        public const String X = "ciclo x";
        public const String TSinoOCL = "sino";
        public const String TSinoSiOCL = "sino si";


        public const String TErrorSemantico = "Semantico";
        public const String TErrorLexico = "Lexico";
        public const String TErrorSintactico = "Sintactico";
        public const String LLAMADA_ARREGLO = "llamada_arreglo";
        public const String LLAMADA_METODO = "llamada_metodo";
        public const String LLAMADA_OBJETO = "llamada_objeto";
        public const String LLAMADA_METODO_ARREGLO = "llamada_metodo_arreglo";


        public const String RELACIONAL = "relacional";
        public const String ARITMETICA = "aritmetica";
        public const String Etiqueta = "etiqueta";
        public const String Temporal = "temporal";
        public const String PrintNum = "printnum";
        public const String PrintChar = "printchar";
        public const String PrintDouble = "printdouble";
        public const String TPrint = "print";
        public const String TIf = "if";
        public const String TIfFalse = "ifFalse";
        public const String TGoto = "goto";
        public const String TMain = "main";
        public const String THeap = "heap";
        public const String TStack = "stack";
        public const String TH = "h";
        public const String TP = "p";


        public const String Tope = "tope";
    }
}
