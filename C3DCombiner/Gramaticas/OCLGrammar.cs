using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3DCombiner
{
    [Language("OCL++", "1.0", "OCL++ Grammar")]
    class OCLGrammar : Grammar
    {

        private readonly TerminalSet mSkipTokensInPreview = new TerminalSet(); //used in token preview for conflict resolution

        public OCLGrammar() : base(caseSensitive: false)
        {

            //Comentarios
            CommentTerminal DelimitedComment = new CommentTerminal("DelimitedComment", "{/-", "-/}");
            CommentTerminal SingleLineComment = new CommentTerminal("SingleLineComment", "//", "\r", "\n", "\u2085", "\u2028", "\u2029");


            NonGrammarTerminals.Add(DelimitedComment);
            NonGrammarTerminals.Add(SingleLineComment);

            //Expresiones Regulares y Datos          
            var Entero = new NumberLiteral(Constante.Entero, NumberOptions.IntOnly);
            var Decimal = new NumberLiteral(Constante.Decimal);
            var Caracter = new StringLiteral(Constante.Caracter, "'", StringOptions.IsChar);
            var Cadena = new StringLiteral(Constante.Cadena, "\"");
            var TTrue = ToTerm(Constante.TTrue);
            var TFalse = ToTerm(Constante.TFalse);
            var Id = new IdentifierTerminal(Constante.Id);

            //Terminales                        
            //Tipo de datos
            var TEntero = ToTerm(Constante.TEntero);
            var TDecimal = ToTerm(Constante.TDecimal);
            var TCaracter = ToTerm(Constante.TCaracter);
            var TCadena = ToTerm(Constante.TCadena);
            var TBooleano = ToTerm(Constante.TBooleano);
            var TVoid = ToTerm(Constante.TVoid);

            var TPublico = ToTerm(Constante.TPublico);
            var TPrivado = ToTerm(Constante.TPrivado);
            var TProtegido = ToTerm(Constante.TProtegido);

            var TMas = ToTerm(Constante.TMas);
            var TMenos = ToTerm(Constante.TMenos);
            var TPor = ToTerm(Constante.TPor);
            var TDivision = ToTerm(Constante.TDivision);
            var TPotencia = ToTerm(Constante.TPotenciaOCL);
            var TAumento = ToTerm(Constante.TAumento);
            var TDecremento = ToTerm(Constante.TDecremento);
            var TMayor = ToTerm(Constante.TMayor);
            var TMenor = ToTerm(Constante.TMenor);
            var TMayorIgual = ToTerm(Constante.TMayorIgual);
            var TMenorIgual = ToTerm(Constante.TMenorIgual);
            var TIgualacion = ToTerm(Constante.TIgualacion);
            var TDiferenciacion = ToTerm(Constante.TDiferenciacion);
            var TAnd = ToTerm(Constante.TAndOCL);
            var TOr = ToTerm(Constante.TOrOCL);
            var TXor = ToTerm(Constante.TXorOCL);
            var TNot = ToTerm(Constante.TNotOCL);

            var TParentesis_Izq = ToTerm(Constante.TParetesis_Izq);
            var TParentesis_Der = ToTerm(Constante.TParentesis_Der);
            var TCorchete_Izq = ToTerm(Constante.TCorchete_Izq);
            var TCorchete_Der = ToTerm(Constante.TCorchete_Der);
            var TLlave_Izq = ToTerm(Constante.TLlave_Izq);
            var TLlave_Der = ToTerm(Constante.TLlave_Der);
            var TDosPuntos = ToTerm(Constante.TDosPuntos);
            var TComa = ToTerm(Constante.TComa);
            var TPuntoComa = ToTerm(Constante.TPuntoComa);
            var TPunto = ToTerm(Constante.TPunto);
            var TIgual = ToTerm(Constante.TIgual);
            var TAsignacion = ToTerm(Constante.TAsignacion);

            var THereda = ToTerm(Constante.THereda);
            var TImportar = ToTerm(Constante.TImportar);
            var TLlamar = ToTerm(Constante.TLlamar);
            var TClase = ToTerm(Constante.TClase);
            var TConstructor = ToTerm(Constante.TConstructor);
            var TMetodo = ToTerm(Constante.TMetodo);
            var TFuncion = ToTerm(Constante.TFuncion);
            var TRetorno = ToTerm(Constante.TRetorno);
            var TSuper = ToTerm(Constante.TSuper);
            var TSobrescribirOLC = ToTerm(Constante.TSobrescribirOLC);
            var TEste = ToTerm(Constante.TEste);
            var TSi = ToTerm(Constante.TSi);
            var TSino = ToTerm(Constante.TSino);
            var TSinoSi = ToTerm(Constante.TSinoSi);
            var TSalir = ToTerm(Constante.TSalir);
            var TElegir = ToTerm(Constante.TElegir);
            var TDefecto = ToTerm(Constante.TDefecto);
            var TContinuar = ToTerm(Constante.TContinuar);
            var TMientras = ToTerm(Constante.TMientras);
            var THacer = ToTerm(Constante.THacer);
            var TRepetir = ToTerm(Constante.TRepetir);
            var THasta = ToTerm(Constante.THasta);
            var TPara = ToTerm(Constante.TPara);
            var TLoop = ToTerm(Constante.TLoop);
            var TOutString = ToTerm(Constante.TOutString);
            var TParseInt = ToTerm(Constante.TParseInt);
            var TParseDouble = ToTerm(Constante.TParseDouble);
            var TIntToStr = ToTerm(Constante.TIntToStr);
            var TDoubleToStr = ToTerm(Constante.TDoubleToStr);
            var TDoubleToInt = ToTerm(Constante.TDoubleToInt);



            var TPrincipal = ToTerm(Constante.TPrincipal);
            var TNew = ToTerm(Constante.TNew);
            var TX = ToTerm(Constante.TX);
            var TUntil = ToTerm(Constante.TUntil);
            var TImprimir = ToTerm(Constante.TImprimir);




            MarkReservedWords(Constante.TEntero);
            MarkReservedWords(Constante.TDecimal);
            MarkReservedWords(Constante.TCaracter);
            MarkReservedWords(Constante.TCadena);
            MarkReservedWords(Constante.TBooleano);
            MarkReservedWords(Constante.TVoid);

            MarkReservedWords(Constante.TPublico);
            MarkReservedWords(Constante.TPrivado);
            MarkReservedWords(Constante.TProtegido);

            MarkReservedWords(Constante.TImportar);
            MarkReservedWords(Constante.TClase);
            MarkReservedWords(Constante.TConstructor);
            MarkReservedWords(Constante.TMetodo);
            MarkReservedWords(Constante.TFuncion);
            MarkReservedWords(Constante.TRetorno);
            MarkReservedWords(Constante.TNuevo);
            MarkReservedWords(Constante.TSuper);
            MarkReservedWords(Constante.TSobrescribirTree);
            MarkReservedWords(Constante.TEste);
            MarkReservedWords(Constante.TSi);
            MarkReservedWords(Constante.TSino);
            MarkReservedWords(Constante.TSinoSi);
            MarkReservedWords(Constante.TSalir);
            MarkReservedWords(Constante.TElegir);
            MarkReservedWords(Constante.TDefecto);
            MarkReservedWords(Constante.TContinuar);
            MarkReservedWords(Constante.TMientras);
            MarkReservedWords(Constante.THacer);
            MarkReservedWords(Constante.TRepetir);
            MarkReservedWords(Constante.THasta);
            MarkReservedWords(Constante.TPara);
            MarkReservedWords(Constante.TLoop);
            MarkReservedWords(Constante.TOutString);
            MarkReservedWords(Constante.TParseInt);
            MarkReservedWords(Constante.TParseDouble);
            MarkReservedWords(Constante.TIntToStr);
            MarkReservedWords(Constante.TDoubleToStr);
            MarkReservedWords(Constante.TDoubleToInt);

            //No terminales
            var INICIO = new NonTerminal(Constante.INICIO);
            var LISTA_CLASE = new NonTerminal(Constante.LISTA_CLASE);
            var CLASE = new NonTerminal(Constante.CLASE);
            var LISTA_SENTENCIAS = new NonTerminal(Constante.LISTA_SENTENCIAS);
            var LISTA_SENTENCIA = new NonTerminal(Constante.LISTA_SENTENCIA);
            var SENTENCIA = new NonTerminal(Constante.SENTENCIA);
            var FUNCION = new NonTerminal(Constante.FUNCION);
            var DECLARACION = new NonTerminal(Constante.DECLARACION);
            var ASIGNACION = new NonTerminal(Constante.ASIGNACION);
            var TIPO = new NonTerminal(Constante.TIPO);
            var TIPO_METODO = new NonTerminal(Constante.TIPO_METODO);
            var LISTA_EXPS = new NonTerminal(Constante.LISTA_EXPS);
            var LISTA_EXP = new NonTerminal(Constante.LISTA_EXP);
            var EXP = new NonTerminal(Constante.EXP);
            var LISTA_ID = new NonTerminal(Constante.LISTA_ID);
            var LISTA_DIMENSIONES = new NonTerminal(Constante.LISTA_DIMENSIONES);
            var DIMENSION = new NonTerminal(Constante.DIMENSION);
            var OBJETO = new NonTerminal(Constante.OBJETO);
            var HIJO = new NonTerminal(Constante.HIJO);
            var LLAMAD_FUNCION = new NonTerminal(Constante.LLAMADA_FUNCION);
            var VISIBILIDAD = new NonTerminal(Constante.VISIBILIDAD);
            var LISTA_PARAMETROS = new NonTerminal(Constante.LISTA_PARAMETROS);
            var LISTA_PARAMETRO = new NonTerminal(Constante.LISTA_PARAMETRO);
            var PARAMETRO = new NonTerminal(Constante.PARAMETRO);
            var LISTA_INSTRUCCIONES = new NonTerminal(Constante.LISTA_INSTRUCCIONES);
            var LISTA_INSTRUCCION = new NonTerminal(Constante.LISTA_INSTRUCCION);
            var INSTRUCCION = new NonTerminal(Constante.INSTRUCCION);
            var DIMENSIONES_METODO = new NonTerminal(Constante.DIMENSIONES_METODO);
            var LISTA_DIMENSION_METODO = new NonTerminal(Constante.LISTA_DIMENSION_METODO);
            var DIMENSION_METODO = new NonTerminal(Constante.DIMENSION_METODO);
            var LLAMADA = new NonTerminal(Constante.LLAMADA);
            var SI = new NonTerminal(Constante.SI);
            var SINO = new NonTerminal(Constante.SINO);
            var LISTA_SINOSIS = new NonTerminal(Constante.LISTA_SINOSIS);
            var LISTA_SINOSI = new NonTerminal(Constante.LISTA_SINOSI);
            var SINOSI = new NonTerminal(Constante.SINOSI);
            var ELEGIR = new NonTerminal(Constante.ELEGIR);
            var LISTA_CASOS = new NonTerminal(Constante.LISTA_CASOS);
            var CASO = new NonTerminal(Constante.CASO);
            var DEFECTO = new NonTerminal(Constante.DEFECTO);
            var MIENTRAS = new NonTerminal(Constante.MIENTRAS);
            var HACER = new NonTerminal(Constante.HACER);
            var REPETIR = new NonTerminal(Constante.REPETIR);
            var PARA = new NonTerminal(Constante.PARA);
            var LOOP = new NonTerminal(Constante.LOOP);
            var LITERALES = new NonTerminal(Constante.LITERALES);
            var LISTA_IMPORTAR = new NonTerminal(Constante.LISTA_IMPORTAR);
            var IMPORTAR = new NonTerminal(Constante.IMPORTAR);
            var LISTA_ARCHIVO = new NonTerminal(Constante.LISTA_ARCHIVO);
            var ARCHIVO = new NonTerminal(Constante.ARCHIVO);

            INICIO.Rule = LISTA_IMPORTAR + LISTA_CLASE;

            LISTA_IMPORTAR.Rule = MakePlusRule(LISTA_IMPORTAR, IMPORTAR);

            IMPORTAR.Rule = TImportar + TParentesis_Izq + Cadena + TParentesis_Der + TPuntoComa
                | TLlamar + TParentesis_Izq + Cadena + TParentesis_Der + TPuntoComa
                ;

            LISTA_CLASE.Rule = MakePlusRule(LISTA_CLASE, CLASE);

            CLASE.Rule = TClase + Id + TLlave_Izq + LISTA_SENTENCIAS + TLlave_Der
                | TClase + Id + THereda + Id + TLlave_Izq + LISTA_SENTENCIAS + TLlave_Der
                ;

            LISTA_SENTENCIAS.Rule = LISTA_SENTENCIA
                | Empty
                ;

            LISTA_SENTENCIA.Rule = this.MakePlusRule(LISTA_SENTENCIA, SENTENCIA);

            SENTENCIA.Rule = FUNCION
                | VISIBILIDAD + DECLARACION
                ;

            FUNCION.Rule = TSobrescribirOLC + VISIBILIDAD + TIPO_METODO + Id + TParentesis_Izq + LISTA_PARAMETROS + TParentesis_Der + TLlave_Izq + LISTA_INSTRUCCIONES + TLlave_Der
                | VISIBILIDAD + TIPO_METODO + Id + TParentesis_Izq + LISTA_PARAMETROS + TParentesis_Der + TLlave_Izq + LISTA_INSTRUCCIONES + TLlave_Der
                | TPrincipal + TParentesis_Izq + TParentesis_Der + TLlave_Izq + LISTA_INSTRUCCIONES + TLlave_Der
                | Id + TParentesis_Izq + LISTA_PARAMETROS + TParentesis_Der + TLlave_Izq + LISTA_INSTRUCCIONES + TLlave_Der
                ;


            VISIBILIDAD.Rule = TPublico
                | TPrivado
                | TProtegido
                | Empty;

            DIMENSIONES_METODO.Rule = LISTA_DIMENSION_METODO
                | Empty
                ;

            LISTA_DIMENSION_METODO.Rule = MakePlusRule(LISTA_DIMENSION_METODO, DIMENSION_METODO);

            DIMENSION_METODO.Rule = TCorchete_Izq + TCorchete_Der;

            LISTA_PARAMETROS.Rule = LISTA_PARAMETRO
                | Empty
                ;

            LISTA_PARAMETRO.Rule = MakeListRule(LISTA_PARAMETRO, TComa, PARAMETRO);

            PARAMETRO.Rule = TIPO + DIMENSIONES_METODO + Id;


            LISTA_INSTRUCCIONES.Rule = LISTA_INSTRUCCION
                | Empty
                ;

            LISTA_INSTRUCCION.Rule = MakePlusRule(LISTA_INSTRUCCION, INSTRUCCION);

            INSTRUCCION.Rule = ASIGNACION
                | DECLARACION + TPuntoComa
                | TRetorno + EXP + TPuntoComa
                | TContinuar + TPuntoComa
                | TSalir + TPuntoComa
                | TContinuar + TPuntoComa
                | LLAMADA
                | SI
                | ELEGIR
                | MIENTRAS
                | HACER
                | REPETIR
                | PARA
                | LOOP
                ;


            TIPO.Rule = TEntero
                | TDecimal
                | TCaracter
                | TCadena
                | TBooleano
                | Id
                ;

            TIPO_METODO.Rule = TEntero
                | TDecimal
                | TCaracter
                | TCadena
                | TBooleano
                | Id
                | TVoid
                ;

            LISTA_ID.Rule = this.MakeListRule(LISTA_ID, TComa, Id);

            LISTA_DIMENSIONES.Rule = this.MakePlusRule(LISTA_DIMENSIONES, DIMENSION);

            DIMENSION.Rule = TCorchete_Izq + EXP + TCorchete_Der;

            DECLARACION.Rule = TIPO + LISTA_ID + TIgual + EXP 
                | TIPO + LISTA_ID 
                | TIPO + Id + LISTA_DIMENSIONES 
                | TIPO + Id + LISTA_DIMENSIONES + TIgual + EXP 
                ;


            ASIGNACION.Rule = OBJETO + Id + LISTA_DIMENSIONES + TAsignacion + EXP
                | Id + LISTA_DIMENSIONES + TAsignacion + EXP
                | OBJETO + Id + TAsignacion + EXP
                | Id + TAsignacion + EXP
                | EXP + TAumento
                | EXP + TDecremento
                ;

            LLAMADA.Rule = OBJETO + Id + TParentesis_Izq + LISTA_EXPS + TParentesis_Der
                | Id + TParentesis_Izq + LISTA_EXPS + TParentesis_Der
                | TOutString + TCorchete_Izq + EXP + TCorchete_Der
                ;

            SI.Rule = TSi + EXP + TDosPuntos + Eos + Indent + LISTA_INSTRUCCIONES + Dedent + LISTA_SINOSIS + SINO;

            LISTA_SINOSIS.Rule = LISTA_SINOSI
                | Empty
                ;

            LISTA_SINOSI.Rule = MakePlusRule(LISTA_SINOSI, SINOSI);

            SINOSI.Rule = TSinoSi + EXP + TDosPuntos + Eos + Indent + LISTA_INSTRUCCIONES + Dedent;

            SINO.Rule = TSino + TDosPuntos + Eos + Indent + LISTA_INSTRUCCIONES + Dedent
                | Empty
                ;

            ELEGIR.Rule = TElegir + EXP + TDosPuntos + Eos + Indent + LISTA_CASOS + DEFECTO + Dedent;

            LISTA_CASOS.Rule = MakePlusRule(LISTA_CASOS, CASO);

            CASO.Rule = LITERALES + TDosPuntos + Eos + Indent + LISTA_INSTRUCCIONES + Dedent;

            DEFECTO.Rule = TDefecto + TDosPuntos + Eos + Indent + LISTA_INSTRUCCIONES + Dedent
                | Empty;

            MIENTRAS.Rule = TMientras + EXP + TDosPuntos + Eos + Indent + LISTA_INSTRUCCIONES + Dedent;

            HACER.Rule = THacer + TDosPuntos + Eos + Indent + LISTA_INSTRUCCIONES + Dedent + TMientras + EXP + Eos;

            REPETIR.Rule = TRepetir + TDosPuntos + Eos + Indent + LISTA_INSTRUCCIONES + Dedent + THasta + EXP + Eos;

            PARA.Rule = TPara + TCorchete_Izq + ASIGNACION + TDosPuntos + EXP + TDosPuntos + EXP + TCorchete_Der + TDosPuntos + Eos + Indent + LISTA_INSTRUCCIONES + Dedent
                | TPara + TCorchete_Izq + DECLARACION + TDosPuntos + EXP + TDosPuntos + EXP + TCorchete_Der + TDosPuntos + Eos + Indent + LISTA_INSTRUCCIONES + Dedent
                ;

            LOOP.Rule = TLoop + TDosPuntos + Eos + Indent + LISTA_INSTRUCCIONES + Dedent;

            LITERALES.Rule = Entero
                | Decimal
                | Caracter
                | Cadena
                | TTrue
                | TFalse
                ;

            LISTA_EXPS.Rule = LISTA_EXP
                | Empty;

            LISTA_EXP.Rule = MakeListRule(LISTA_EXP, TComa, EXP);

            EXP.Rule = EXP + TMas + EXP
                | EXP + TMenos + EXP
                | TMenos + EXP
                | EXP + TAumento
                | EXP + TDecremento
                | EXP + TPor + EXP
                | EXP + TDivision + EXP
                | EXP + TPotencia + EXP
                | EXP + TMayor + EXP
                | EXP + TMenor + EXP
                | EXP + TMayorIgual + EXP
                | EXP + TMenorIgual + EXP
                | EXP + TIgualacion + EXP
                | EXP + TDiferenciacion + EXP
                | EXP + TOr + EXP
                | EXP + TAnd + EXP
                | EXP + TXor + EXP
                | TNot + EXP
                | TParentesis_Izq + EXP + TParentesis_Der
                | TLlave_Izq + LISTA_EXP + TLlave_Der //arreglonativo
                | Decimal
                | Entero
                | Caracter
                | Cadena
                | TTrue
                | TFalse
                | TEste
                | OBJETO + Id
                | OBJETO + Id + TParentesis_Izq + LISTA_EXPS + TCorchete_Der
                | OBJETO + Id + LISTA_DIMENSIONES
                | Id
                | Id + TParentesis_Izq + LISTA_EXPS + TParentesis_Der
                | Id + LISTA_DIMENSIONES
                | TNew + Id + TParentesis_Izq + LISTA_EXPS + TParentesis_Der
                ;

            OBJETO.Rule = MakePlusRule(OBJETO, HIJO);

            HIJO.Rule = Id + TPunto
                | Id + TCorchete_Izq + LISTA_EXPS + TCorchete_Der + TPunto
                | Id + LISTA_DIMENSIONES + TPunto
                | TEste + TPunto
                ;


            RegisterOperators(1, Associativity.Left, TOr.ToString());
            RegisterOperators(2, Associativity.Left, TXor.ToString());
            RegisterOperators(3, Associativity.Left, TAnd.ToString());
            RegisterOperators(4, Associativity.Left, TNot.ToString());
            RegisterOperators(5, TIgualacion.ToString(), TDiferenciacion.ToString(), TMenor.ToString(), TMayor.ToString(), TMenorIgual.ToString(), TMayorIgual.ToString());
            RegisterOperators(6, Associativity.Left, TMas.ToString(), TMenos.ToString());
            RegisterOperators(7, Associativity.Left, TPor.ToString(), TDivision.ToString());
            RegisterOperators(8, Associativity.Right, TPotencia.ToString());
            RegisterOperators(9, Associativity.Left, TAumento.ToString(), TDecremento.ToString());


            MarkPunctuation(TParentesis_Izq, TParentesis_Der, TCorchete_Izq, TCorchete_Der, TLlave_Izq, TLlave_Der, TDosPuntos, TComa, TPuntoComa, TPunto, TIgual, TAsignacion);
            //MarkTransient(TIPO, SIMPLIFICADA);
            //No terminal de inicio
            this.Root = INICIO;

            //Para generar el AST
            //LanguageFlags = LanguageFlags.CreateAst;
        }
    }
}
