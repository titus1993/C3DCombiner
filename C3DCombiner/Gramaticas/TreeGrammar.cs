using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Parsing;
using Irony.Ast;

namespace C3DCombiner
{
    [Language("Tree", "1.0", "Tree Grammar")]
    class TreeGrammar : Grammar
    {

        private readonly TerminalSet mSkipTokensInPreview = new TerminalSet(); //used in token preview for conflict resolution

        public TreeGrammar() : base(caseSensitive: false)
        {

            //Comentarios
            CommentTerminal DelimitedComment = new CommentTerminal("DelimitedComment", "{--", "--}");
            CommentTerminal SingleLineComment = new CommentTerminal("SingleLineComment", "##", "\r", "\n", "\u2085", "\u2028", "\u2029");


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
            var TPotencia = ToTerm(Constante.TPotencia);
            var TAumento = ToTerm(Constante.TAumento);
            var TDecremento = ToTerm(Constante.TDecremento);
            var TMayor = ToTerm(Constante.TMayor);
            var TMenor = ToTerm(Constante.TMenor);
            var TMayorIgual = ToTerm(Constante.TMayorIgual);
            var TMenorIgual = ToTerm(Constante.TMenorIgual);
            var TIgualacion = ToTerm(Constante.TIgualacion);
            var TDiferenciacion = ToTerm(Constante.TDiferenciacion);
            var TAnd = ToTerm(Constante.TAnd);
            var TOr = ToTerm(Constante.TOr);
            var TXor = ToTerm(Constante.TXor);
            var TNot = ToTerm(Constante.TNot);

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

            var TImportar = ToTerm(Constante.TImportar);
            var TClase = ToTerm(Constante.TClase);
            var TConstructor = ToTerm(Constante.TConstructor);
            var TMetodo = ToTerm(Constante.TMetodo);
            var TFuncion = ToTerm(Constante.TFuncion);
            var TRetorno = ToTerm(Constante.TRetorno);
            var TNuevo = ToTerm(Constante.TNuevo);
            var TSuper = ToTerm(Constante.TSuper);
            var TSobrescribirTree = ToTerm(Constante.TSobrescribirTree);
            var TSelf = ToTerm(Constante.TSelf);
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
            MarkReservedWords(Constante.TSelf);
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
            var LISTA_DIMENSIONES = new NonTerminal(Constante.LISTA_DIMENSIONES);
            var LISTA_EXPS = new NonTerminal(Constante.LISTA_EXPS);
            var LISTA_EXP = new NonTerminal(Constante.LISTA_EXP);
            var EXP = new NonTerminal(Constante.EXP);
            var LISTA_ID = new NonTerminal(Constante.LISTA_ID);
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
            var LLAMADA_EXP = new NonTerminal(Constante.LLAMADA_EXP);

            INICIO.Rule = LISTA_IMPORTAR + LISTA_CLASE;

            LISTA_IMPORTAR.Rule = MakePlusRule(LISTA_IMPORTAR, IMPORTAR);

            IMPORTAR.Rule = TImportar + LISTA_ARCHIVO + Eos
                | Empty;

            LISTA_ARCHIVO.Rule = MakeListRule(LISTA_ARCHIVO, TComa, ARCHIVO);

            ARCHIVO.Rule = Id + ".tree"
                | Id + ".olc"
                | Cadena
                ;

            LISTA_CLASE.Rule = MakePlusRule(LISTA_CLASE, CLASE);

            CLASE.Rule = TClase + Id + TCorchete_Izq + TCorchete_Der + TDosPuntos + Eos + Indent + LISTA_SENTENCIAS + Dedent
                | TClase + Id + TCorchete_Izq + Id + TCorchete_Der + TDosPuntos + Eos + Indent + LISTA_SENTENCIAS + Dedent
                ;

            LISTA_SENTENCIAS.Rule = LISTA_SENTENCIA
                | Empty
                ;

            LISTA_SENTENCIA.Rule = this.MakePlusRule(LISTA_SENTENCIA, SENTENCIA);

            SENTENCIA.Rule = FUNCION
                | VISIBILIDAD + DECLARACION + Eos;

          FUNCION.Rule = TSobrescribirTree + Eos + VISIBILIDAD + TMetodo + Id + TCorchete_Izq + LISTA_PARAMETROS + TCorchete_Der + TDosPuntos + Eos + Indent + LISTA_INSTRUCCIONES + Dedent
                | VISIBILIDAD + TMetodo + Id + TCorchete_Izq + LISTA_PARAMETROS + TCorchete_Der + TDosPuntos + Eos + Indent + LISTA_INSTRUCCIONES + Dedent
                | TSobrescribirTree + Eos + VISIBILIDAD + TFuncion + TIPO + DIMENSIONES_METODO + Id + TCorchete_Izq + LISTA_PARAMETROS + TCorchete_Der + TDosPuntos + Eos + Indent + LISTA_INSTRUCCIONES + Dedent
                | VISIBILIDAD + TFuncion + TIPO + DIMENSIONES_METODO + Id + TCorchete_Izq + LISTA_PARAMETROS + TCorchete_Der + TDosPuntos + Eos + Indent + LISTA_INSTRUCCIONES + Dedent
                | TConstructor + TCorchete_Izq + LISTA_PARAMETROS + TCorchete_Der + TDosPuntos + Eos + Indent + LISTA_INSTRUCCIONES + Dedent
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

            INSTRUCCION.Rule = LLAMADA + Eos
                | DECLARACION + Eos
                | ASIGNACION + Eos
                | TRetorno + EXP + Eos
                | TSalir + Eos
                | TContinuar + Eos
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

            LISTA_ID.Rule = this.MakeListRule(LISTA_ID, TComa, Id);

            LISTA_DIMENSIONES.Rule = this.MakePlusRule(LISTA_DIMENSIONES, DIMENSION);

            DIMENSION.Rule = TCorchete_Izq + EXP + TCorchete_Der;

            DECLARACION.Rule = TIPO + LISTA_ID + TAsignacion + EXP
                | TIPO + LISTA_ID
                | TIPO + Id + LISTA_DIMENSIONES;


            ASIGNACION.Rule = OBJETO + Id + LISTA_DIMENSIONES + TAsignacion + EXP
                | Id + LISTA_DIMENSIONES + TAsignacion + EXP
                | OBJETO + Id + TAsignacion + EXP
                | Id + TAsignacion + EXP
                | TParentesis_Izq + EXP + TAumento + TParentesis_Der
                | TParentesis_Izq + EXP + TDecremento + TParentesis_Der
                ;

            LLAMADA.Rule = OBJETO + Id + TCorchete_Izq + LISTA_EXPS + TCorchete_Der//3
                | Id + TCorchete_Izq + LISTA_EXPS + TCorchete_Der//2
                | OBJETO + Id + LISTA_DIMENSIONES//3
                | Id + LISTA_DIMENSIONES//2
                | TOutString + TCorchete_Izq + EXP + TCorchete_Der//2
                | TSuper + TCorchete_Izq + LISTA_EXPS + TCorchete_Der//2
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

            PARA.Rule = TPara + TCorchete_Izq + ASIGNACION + TDosPuntos + EXP + TDosPuntos + ASIGNACION + TCorchete_Der + TDosPuntos + Eos + Indent + LISTA_INSTRUCCIONES + Dedent
                | TPara + TCorchete_Izq + DECLARACION + TDosPuntos + EXP + TDosPuntos + ASIGNACION + TCorchete_Der + TDosPuntos + Eos + Indent + LISTA_INSTRUCCIONES + Dedent
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

            EXP.Rule = TParentesis_Izq + EXP + EXP + TMas + TParentesis_Der
                | TParentesis_Izq + EXP + EXP + TMenos + TParentesis_Der
                | TParentesis_Izq + EXP + TMenos + TParentesis_Der
                | TParentesis_Izq + EXP + TAumento + TParentesis_Der
                | TParentesis_Izq + EXP + TDecremento + TParentesis_Der
                | TParentesis_Izq + EXP + EXP + TPor + TParentesis_Der
                | TParentesis_Izq + EXP + EXP + TDivision + TParentesis_Der
                | TParentesis_Izq + EXP + EXP + TPotencia + TParentesis_Der
                | TCorchete_Izq + EXP + EXP + TMayor + TCorchete_Der
                | TCorchete_Izq + EXP + EXP + TMenor + TCorchete_Der
                | TCorchete_Izq + EXP + EXP + TMayorIgual + TCorchete_Der
                | TCorchete_Izq + EXP + EXP + TMenorIgual + TCorchete_Der
                | TCorchete_Izq + EXP + EXP + TIgualacion + TCorchete_Der
                | TCorchete_Izq + EXP + EXP + TDiferenciacion + TCorchete_Der
                | TLlave_Izq + EXP + EXP + TOr + TLlave_Der
                | TLlave_Izq + EXP + EXP + TAnd + TLlave_Der
                | TLlave_Izq + EXP + EXP + TXor + TLlave_Der
                | TLlave_Izq + EXP + TNot + TLlave_Der
                | Decimal
                | Entero
                | Caracter
                | Cadena
                | TTrue
                | TFalse
                | TSelf
                | LLAMADA_EXP
                | TNuevo + Id + TCorchete_Izq + LISTA_EXPS + TCorchete_Der
                | TParseInt + TCorchete_Izq + EXP + TCorchete_Der
                | TParseDouble + TCorchete_Izq + EXP + TCorchete_Der
                | TIntToStr + TCorchete_Izq + EXP + TCorchete_Der
                | TDoubleToStr + TCorchete_Izq + EXP + TCorchete_Der
                | TDoubleToInt + TCorchete_Izq + EXP + TCorchete_Der
                ;

            LLAMADA_EXP.Rule = OBJETO + Id
                | OBJETO + Id + TCorchete_Izq + LISTA_EXPS + TCorchete_Der
                | OBJETO + Id + LISTA_DIMENSIONES
                | Id
                | Id + TCorchete_Izq + LISTA_EXPS + TCorchete_Der
                | Id + LISTA_DIMENSIONES
                ;

            OBJETO.Rule = MakePlusRule(OBJETO, HIJO);

            HIJO.Rule = Id + TPunto
                | Id + TCorchete_Izq + LISTA_EXPS + TCorchete_Der + TPunto
                | Id + LISTA_DIMENSIONES + TPunto
                | TSelf + TPunto
                | TSuper + TPunto
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

        public override void CreateTokenFilters(LanguageData language, TokenFilterList filters)
        {
            var outlineFilter = new CodeOutlineFilter(language.GrammarData,
              OutlineOptions.ProduceIndents | OutlineOptions.CheckBraces, ToTerm(@"\")); // "\" is continuation symbol
            filters.Add(outlineFilter);
        }
    }
}
