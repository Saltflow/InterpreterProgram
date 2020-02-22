using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Parsergenerator;
using static Parsergenerator.ParserGenerate;

namespace IntepreterProgram.Parser
{
    class Parser
    {
        //state ID and token
        Stack<Tuple<int,Token>> ParseStack = new Stack<Tuple<int, Token>>();

        Dictionary<Tuple<Closure, Token>, int> Goto;
        Dictionary<Tuple<int, Token>, Tuple<Ptype, int>> Action;

        
        public static List<Production> ParserResult = new List<Production>();
        public static ParseTree ParseTree = new ParseTree();
        public static bool afterPanicRecover = false;

        //build parser with a defined file
        public Parser()
        {
            if (ParserGenerate.closures.Count == 0)
            {
                ParserGenerate.GenerateParser();
                StrType.BuildMapping();
            }
            this.Goto = ParserGenerate.Goto;
            this.Action = ParserGenerate.Action;
        }
        /*initialize for parsing a new file
         * fornow there is 3 things to be initialized:
         * ParseStack
         * ParseResult
         * PanicRecover Symbol
         */
        public void init()
        {
            ParseStack = new Stack<Tuple<int, Token>>();
            ParserResult = new List<Production>();
            afterPanicRecover = false;
        }

        public virtual void AddParseResult(Production result)
        {
            ParserResult.Add(result);
        }
        

        public virtual void ShiftToken(ref int tokID)
        {
            tokID++;
        }

        public virtual void AcceptAction()
        {
            ParseTree.Root = new ParseNode(ParserResult[ResultID]);
            GenerateParseTree(ParseTree.Root);
            UIHelper.Log("Parse Complete");
            printParseResult();
        }

        protected static int ResultID;
        // recuresive throught rightmost nonterminal
        //ResultID : number in the ParseResult, which should include current reduce rule
        public ParseNode GenerateParseTree(ParseNode node)
        {
            if (node.thisTok.ttype == Ttype.Terminal)
                return node;
            Production currRule = ParserResult[ResultID];
            node.geneTok = new ParseNode[currRule.Rules().Length];
            for(int i=currRule.Rules().Length - 1;i>=0;i--)
            {
                Token token = Token.GetToken(currRule.Rules()[i]);
                if (token.ttype == Ttype.Nontermi) ResultID--;
                node.geneTok[i] = GenerateParseTree(new ParseNode(token));
            }
            return node;
        }

        /* Function for debug purpuse
         * may deprecated.
         * Printing parsing result to a file, with parse result organizing as Production
         */
        void printParseResult()
        {
            StreamWriter writer;
            writer = new StreamWriter("../../parse_result.tem");
            foreach(Production result in ParserResult)
            {
                writer.WriteLine(result.ToString());
            }
            writer.Close();
        }

        public void ParseTable()
        {
            init();
            // Index for current lexical Analyze result
            int currResultTok = 0;
            bool accepted = false;
            ParseStack.Push(Tuple.Create(0, Token.tokens[0]));
            while(!accepted)
            {
                Token nexttok;
                if (currResultTok < Analysis.ResultTable.Count)
                    nexttok = StrType.Res2Token[Analysis.ResultTable[currResultTok].type];
                else
                    nexttok = Token.GetToken(-1);
                Tuple<Ptype, int> nextact;
                Tuple<int,Token> nexttuple = Tuple.Create(ParseStack.Peek().Item1, nexttok);
                //there is no Action in next given tuple
                if (!Action.ContainsKey(nexttuple))
                {
                    Tuple<int,Token> emptyreduce = Tuple.Create(ParseStack.Peek().Item1, Token.GetToken(-1));
                    //no empty reduce
                    if (Action.ContainsKey(emptyreduce))
                    {
                        nextact = Action[emptyreduce];
                    }
                    else
                    {
                        PanicRecover(ref currResultTok);
                        break;
                    }
                }
                else
                    nextact = Action[nexttuple];
                switch(nextact.Item1)
                {
                    case Ptype.move:
                        {
                            ParseStack.Push(Tuple.Create(nextact.Item2,nexttok));
                            ShiftToken(ref currResultTok);
                            break;
                        }
                    case Ptype.reduce:
                        {
                            Production rule =  Production.formalGrammar[nextact.Item2];
                            int tokNum = rule.Rules().Length;
                            while (tokNum > 0)
                            {
                                ParseStack.Pop();
                                tokNum--;
                            }
                            //top equal to -1 will not be allowded
                            if(ParseStack.Peek().Item1 == -1)
                            {
                                //error handling
                                PanicRecover(ref currResultTok);
                            }
                            //next state
                            int nextsta = ParseStack.Peek().Item1;
                            ParseStack.Push(Tuple.Create(Goto[Tuple.Create(closures[nextsta], Token.tokens[rule.Nonterminal()])], Token.tokens[rule.Nonterminal()]));
                            AddParseResult(rule);
                            break;
                        }
                    case Ptype.accept:
                        {
                            accepted = true;
                            ResultID = ParserResult.Count - 1;
                            AcceptAction();
                            break;
                        }
                }
            }
            if (afterPanicRecover)
            {
                UIHelper.Log(GeneticStringClass.ITTerminate);
                Thread.CurrentThread.Abort();
            }
        }



        //Continue Parsing when received an Error
        public static void PanicRecover(ref int currResultTok)
        {
            //error handling
            UIHelper.Log("Error happened in parser, line "+Analysis.ResultTable[currResultTok].lineNum);
            while(currResultTok < Analysis.ResultTable.Count)
            {
                bool endPanic = false;
                switch(Analysis.ResultTable[currResultTok].type)
                {
                    case ResultType.colon:
                    case ResultType.rBrack:
                        {
                            endPanic = true;
                            break;
                        }
                    default:
                        {
                            currResultTok++;
                            break;
                        }
                }
                if (endPanic) break;
            }
            afterPanicRecover = true;
        }
        
    }

}
