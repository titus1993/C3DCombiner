using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3DCombiner.Gramaticas
{
    [Language("3D", "1.0", "3D Grammar")]
    class _3DGrammar : Grammar
    {
        private readonly TerminalSet mSkipTokensInPreview = new TerminalSet(); //used in token preview for conflict resolution

        public _3DGrammar() : base(caseSensitive: false)
        {

            //Comentarios
            CommentTerminal DelimitedComment = new CommentTerminal("DelimitedComment", "/*", "*/");
            CommentTerminal SingleLineComment = new CommentTerminal("SingleLineComment", "//", "\r", "\n", "\u2085", "\u2028", "\u2029");


            NonGrammarTerminals.Add(DelimitedComment);
            NonGrammarTerminals.Add(SingleLineComment);

            //Expresiones Regulares y Datos          
            var Entero = new NumberLiteral(Constante.Entero, NumberOptions.IntOnly);
            var Decimal = new NumberLiteral(Constante.Decimal);
            var Id = new IdentifierTerminal(Constante.Id);
            var temp = new RegexBasedTerminal(Constante.Temporal, "[t][0-9]+");
            var printnum = new RegexBasedTerminal(Constante.PrintNum, "\"%d\"");
            var printchar = new RegexBasedTerminal(Constante.PrintChar, "\"%c\"");
            var printdouble = new RegexBasedTerminal(Constante.PrintDouble, "\"%f\"");
            var etq = new RegexBasedTerminal(Constante.Etiqueta, "[L][0-9]+");

            //Terminales                        
            //Tipo de datos
            var TVoid = ToTerm(Constante.TVoid);
            var TMain = ToTerm(Constante.TMain);
            var TH = ToTerm(Constante.TH);
            var TP = ToTerm(Constante.TP);
            var THeap = ToTerm(Constante.THeap);
            var TStack = ToTerm(Constante.TStack);
            var TGoto = ToTerm(Constante.TGoto);

            var TMas = ToTerm(Constante.TMas);
            var TMenos = ToTerm(Constante.TMenos);
            var TPor = ToTerm(Constante.TPor);
            var TDivision = ToTerm(Constante.TDivision);
            var TPotencia = ToTerm(Constante.TPotenciaOCL);
            var TMayor = ToTerm(Constante.TMayor);
            var TMenor = ToTerm(Constante.TMenor);
            var TMayorIgual = ToTerm(Constante.TMayorIgual);
            var TMenorIgual = ToTerm(Constante.TMenorIgual);
            var TIgualacion = ToTerm(Constante.TIgualacion);
            var TDiferenciacion = ToTerm(Constante.TDiferenciacion);

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

            var TPrint = ToTerm(Constante.TPrint);
            var TIf = ToTerm(Constante.TIf);
            var TIfFalse = ToTerm(Constante.TIfFalse);




            MarkReservedWords(Constante.TPrint);
            MarkReservedWords(Constante.TIf);
            MarkReservedWords(Constante.TIfFalse);
            MarkReservedWords(Constante.TH);
            MarkReservedWords(Constante.TP);
            MarkReservedWords(Constante.THeap);
            MarkReservedWords(Constante.TStack);

            //No terminales
            var INICIO = new NonTerminal(Constante.INICIO);
            var LISTA_SENTENCIAS = new NonTerminal(Constante.LISTA_SENTENCIAS);
            var LISTA_SENTENCIA = new NonTerminal(Constante.LISTA_SENTENCIA);
            var SENTENCIA = new NonTerminal(Constante.SENTENCIA);
            var ASIGNACION = new NonTerminal(Constante.ASIGNACION);
            var EXP = new NonTerminal(Constante.EXP);
            var LISTA_INSTRUCCIONES = new NonTerminal(Constante.LISTA_INSTRUCCIONES);
            var LISTA_INSTRUCCION = new NonTerminal(Constante.LISTA_INSTRUCCION);
            var INSTRUCCION = new NonTerminal(Constante.INSTRUCCION);
            var ARITMETICA = new NonTerminal(Constante.ARITMETICA);
            var RELACIONAL = new NonTerminal(Constante.RELACIONAL);
            var PRINT = new NonTerminal(Constante.TPrint);
            var If = new NonTerminal(Constante.TIf);
            var IfFalse = new NonTerminal(Constante.TIfFalse);


            INICIO.Rule = LISTA_SENTENCIA
                ;

            LISTA_SENTENCIAS.Rule = LISTA_SENTENCIA
                | Empty
                ;

            LISTA_SENTENCIA.Rule = this.MakePlusRule(LISTA_SENTENCIA, SENTENCIA);

            SENTENCIA.Rule = TVoid + Id + TParentesis_Izq + TParentesis_Der + TLlave_Izq + LISTA_INSTRUCCIONES + TLlave_Der
                | TMain + TParentesis_Izq + TParentesis_Der + TLlave_Izq + LISTA_INSTRUCCIONES + TLlave_Der
                ;


            LISTA_INSTRUCCIONES.Rule = LISTA_INSTRUCCION
                | Empty
                ;

            LISTA_INSTRUCCION.Rule = MakePlusRule(LISTA_INSTRUCCION, INSTRUCCION);

            INSTRUCCION.Rule = ASIGNACION + TPuntoComa//1
                | etq + TDosPuntos//1
                | TIf + RELACIONAL + TGoto + etq + TPuntoComa//4
                | TIfFalse + RELACIONAL + TGoto + etq + TPuntoComa//4
                | TGoto + etq + TPuntoComa//2
                | Id + TParentesis_Izq + TParentesis_Der + TPuntoComa//1
                | TPrint + TParentesis_Izq + PRINT + TComa + temp + TParentesis_Der + TPuntoComa//3
                ;

            PRINT.Rule = printchar
                | printdouble
                | printnum
                ;

            ASIGNACION.Rule = temp + TIgual + EXP
                | temp + TIgual + ARITMETICA
                | TH + TIgual + EXP
                | TH + TIgual + ARITMETICA
                | TP + TIgual + EXP
                | TP + TIgual + ARITMETICA
                | THeap + TCorchete_Izq + EXP + TCorchete_Der + TIgual + EXP
                | TStack + TCorchete_Izq + EXP + TCorchete_Der + TIgual + EXP
                ;
            

            RELACIONAL.Rule = EXP + TMayor + EXP
                | EXP + TMenor + EXP
                | EXP + TMayorIgual + EXP
                | EXP + TMenorIgual + EXP
                | EXP + TIgualacion + EXP
                | EXP + TDiferenciacion + EXP
                ;

            ARITMETICA.Rule = EXP + TMas + EXP
                | EXP + TMenos + EXP
                | TMenos + EXP
                | EXP + TPor + EXP
                | EXP + TDivision + EXP
                | EXP + TPotencia + EXP
                | THeap + TCorchete_Izq + EXP + TCorchete_Der
                | TStack + TCorchete_Izq + EXP + TCorchete_Der
                ;

            EXP.Rule = Decimal
                | Entero
                | temp
                | TH
                | TP
                ;


            RegisterOperators(1, TIgualacion.ToString(), TDiferenciacion.ToString(), TMenor.ToString(), TMayor.ToString(), TMenorIgual.ToString(), TMayorIgual.ToString());
            RegisterOperators(2, Associativity.Left, TMas.ToString(), TMenos.ToString());
            RegisterOperators(3, Associativity.Left, TPor.ToString(), TDivision.ToString());
            RegisterOperators(4, Associativity.Right, TPotencia.ToString());


            MarkPunctuation(TParentesis_Izq, TParentesis_Der, TCorchete_Izq, TCorchete_Der, TLlave_Izq, TLlave_Der, TDosPuntos, TComa, TPuntoComa, TPunto, TIgual, TIgual);
            //MarkTransient(TIPO, SIMPLIFICADA);
            //No terminal de inicio
            this.Root = INICIO;

            //Para generar el AST
            //LanguageFlags = LanguageFlags.CreateAst;
        }
    }
}
