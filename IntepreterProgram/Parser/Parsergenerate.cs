using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IntepreterProgram.Parser;

namespace Parsergenerator
{
    class ParserGenerate
    {
        public static List<Closure> closures = new List<Closure>();
        public static Dictionary<Token, List<int>> first = new Dictionary<Token, List<int>>();
        public static Dictionary<Token, List<int>> follow = new Dictionary<Token, List<int>>();

        public enum Ptype
        {
            move,
            reduce,
            accept
        }

        public static Dictionary<Tuple<Closure,Token>, int> Goto = new Dictionary<Tuple<Closure, Token>, int>();
        public static Dictionary<Tuple<int, Token>, Tuple<Ptype, int>> Action = new Dictionary<Tuple<int, Token>, Tuple<Ptype, int>>();

        private static List<string> Grammar = new List<string>();
        private static string Term = "Terminal:";
        private static string NonTerm = "None Terminal:";

        private static StreamWriter writer;

        public static void GenerateParser()
        {
            init();
            formalize();
            getFirst();
            getFollow();
            generateClosures();
            buildParseTable();
            PrintOutConvertTable();
            IntepreterProgram.UIHelper.Log("Generate Parser Finished");
        }
        //output first set

        static void testSet(Dictionary<Token,List<int>> set)
        {
            foreach (Token tok in Token.tokens)
            {
                Console.WriteLine("Noneterm: {0}", tok.name);
                Console.WriteLine("------------------");
                for (int i = 0; i < set[tok].Count; i++)
                {
                    if (set[tok][i] == -1) Console.Write("#  ");
                    else Console.Write("{0}  ", Token.tokens[set[tok][i]].name);
                }
                Console.WriteLine();
                Console.WriteLine();
            }
            Console.Read();
        }

        static bool isDefinition(string line)
        {
            if (line.Length < 2) return false;
            if (line.Contains(":=")) return true;
            return false;
        }

        //read throught file, then find all useful informations,
        //including terminal, non-terminal grammar-rules
        //note that grammar-rules are in string-style
        static void init()
        {
            writer = new StreamWriter("../../parse_table.csv");
            string file = "../../../SA_State.idf";

            List<string> def = new List<string>();
            StreamReader reader = new StreamReader(file);
            while (!reader.EndOfStream)
            {
                def.Add(reader.ReadLine());
            }
            for (int i = 0; i < def.Count; i++)
            {
                if (def[i] == Term)
                {
                    string[] termtokens = def[i + 1].Split('|');
                    foreach (string termtoken in termtokens)
                    {
                        int tokid = Token.getID(Ttype.Terminal, termtoken);
                        List<int> firstset = new List<int>();
                        first.Add(Token.tokens[tokid], firstset);
                        List<int> followset = new List<int>();
                        follow.Add(Token.tokens[tokid], followset);

                        firstset.Add(Token.getID(termtoken));
                    }
                    continue;
                }
                if (def[i] == NonTerm)
                {
                    string[] nontermtokens = def[i + 1].Split('|');
                    foreach (string nontermtoken in nontermtokens)
                    {
                        int tokid = Token.getID(Ttype.Nontermi, nontermtoken);
                        List<int> firstset = new List<int>();
                        first.Add(Token.tokens[tokid], firstset);
                        List<int> followset = new List<int>();
                        follow.Add(Token.tokens[tokid], followset);
                    }

                    continue;
                }
                if (isDefinition(def[i]))
                {
                    Grammar.Add(def[i]);
                }
            }
            //by default,  -1 token(which means#) will always have -1 as first 
            List<int> endFirst = new List<int>();
            endFirst.Add(-1);
            first.Add(Token.GetToken(-1), endFirst);
        }

        //fill the first set by iteratively checking all the symbols in the grammar rules
        static void getFirst()
        {
            bool changed = false;
            while(true)
            {
                changed = false;
                for(int i=0;i<Grammar.Count;i++)
                {
                    Production Production = new Production(Grammar[i]);
                    //size of the first set for A
                    Token nowNonTerm = Token.tokens[Production.Nonterminal()];
                    List<int> FirstA = first[nowNonTerm];
                    int lsize = FirstA.Count;
                    FirstA = unionList(first[Token.tokens[Production.Rules()[0]]], FirstA);
                    first[nowNonTerm] = FirstA;
                    if (FirstA.Count > lsize)
                        changed = true;
                }
                if (changed == false)
                    break;
            }
        }

        /*helperd function for first calculation*/
        static List<int> unionList(List<int> a,List<int> b)
        {
            HashSet<int> aset = new HashSet<int>(a);
            HashSet<int> bset = new HashSet<int>(b);
            aset.UnionWith(bset);
            return  new List<int>(aset);
        }
        //get follow set via scanning Grammar
        static void getFollow()
        {
            bool changed = false;
            while(true)
            {
                changed = false;
                for(int i=0;i<Grammar.Count;i++)
                {
                    Production Production = new Production(Grammar[i]);
                    //tokpos refers to the position of token in Production,
                    //not token ID
                    for(int tokpos =0; tokpos < Production.Rules().Length; tokpos++)
                    {
                        if(Token.tokens[Production.Rules()[tokpos]].ttype == Ttype.Nontermi)
                        {
                            List<int> FollowB = follow[Token.tokens[Production.Rules()[tokpos]]];
                            int lsize = FollowB.Count;
                            if (tokpos != Production.Rules().Length - 1)
                                FollowB = unionList(FollowB, first[Token.tokens[Production.Rules()[tokpos + 1]]]);
                            else
                            {
                                if (!FollowB.Contains(-1))
                                    //we use -1 to denote #, end of Production
                                    FollowB.Add(-1);
                            }
                            if (FollowB.Count > lsize)
                            {
                    //            Console.WriteLine(Production.ToString()+lsize.ToString()+FollowB.Count.ToString());
                                follow[Token.tokens[Production.Rules()[tokpos]]] = FollowB;
                                changed = true;
                            }
                        }
                    }
                }
                if(changed == false)
                {
                    break;
                }
            }
        }

        static void formalize()
        {
            for(int i=0;i<Grammar.Count;i++)
            {
                Production.formalGrammar.Add(new Production(Grammar[i]));       
            }
        }

        static void generateClosures()
        {
            Closure G = new Closure(new Production(Grammar[0]));
            G.genClosure();
            closures.Add(G);
            while(true)
            {
                int len = closures.Count;
                for (int i = 0;i < closures.Count; i++)
                {
                    Closure I = closures[i];
                    foreach(Token tok in Token.tokens)
                    {
                        Closure closure = Closure.gotoClosure(I, tok);
                        //not null, and not on current closures
                        if (closure.entities.Count != 0)
                        {
                            if (!closures.Contains(closure))
                            {
                                closures.Add(closure);
                                Goto[Tuple.Create(I, tok)] = closures.Count - 1;
                            }
                            else
                            {
                                Goto[Tuple.Create(I, tok)] = closures.IndexOf(closure);
                            }
                        }
                    }
                }
                if (len == closures.Count)
                    break;
            }

        }

        static void PrintClosures()
        {
            int count = 0;
            foreach (Closure closure in closures)
            {
                Console.WriteLine(count++);
                Console.WriteLine(closure);
                foreach (Token tok in Token.tokens)
                {
                    if (Goto.ContainsKey(Tuple.Create(closure, tok)))
                    {
                        Console.WriteLine(tok.ToString() + "---->" + Goto[Tuple.Create(closure, tok)]);
                    }
                }
            }
            Console.ReadLine();
        }

        static void buildParseTable()
        {
            //Iterate through closures
            for(int i=0;i<closures.Count;i++)
            {
                Closure I = closures[i];
                //for each Production, check their movement
                for(int j=0;j<I.entities.Count;j++)
                {
                    Production Production = I.entities[j];
                    int nexttok = Production.GetNext();
                    //there is no nexttok
                    if(nexttok == -1)
                    {
                        //this is the beginning token
                        if (Production.Nonterminal() == 0)
                            Action[Tuple.Create(i, Token.GetToken(-1))] = Tuple.Create(Ptype.accept, 0);
                        else
                            Action[Tuple.Create(i, Token.GetToken(Production.Nexttok))] = Tuple.Create(Ptype.reduce, Grammar.IndexOf(Production.mrule));
                    }
                    //for terminal
                    else //if(Token.tokens[nexttok].ttype == Ttype.Terminal)
                    {
                        if (Goto.ContainsKey(Tuple.Create(I, Token.GetToken(nexttok))))
                            Action[Tuple.Create(i, Token.tokens[nexttok])] = Tuple.Create(Ptype.move, Goto[Tuple.Create(I, Token.tokens[nexttok])]);
                    }
                }
            }
        }

        static void PrintoutConvertLine(int i,Token token)
        {
            if (Action.ContainsKey(Tuple.Create(i,token)))
            {
                string action;
                Tuple<Ptype, int> movement = Action[Tuple.Create(i, token)];
                switch (movement.Item1)
                {
                    case Ptype.accept:
                        action = "acc";
                        break;
                    case Ptype.move:
                        action = "s";
                        break;
                    case Ptype.reduce:
                        action = "r";
                        break;
                    default:
                        action = "";
                        break;
                }
                writer.Write(action + movement.Item2.ToString() + ",");
            }
                else
                    writer.Write("n,");
        }

        //Output Parsing table
        static void PrintOutConvertTable()
        {
            writer.Write(",");
            for(int i=0;i<Token.tokens.Count;i++)
            {
                writer.Write(Token.tokens[i].ToString() + ",");
            }
            writer.WriteLine("");
            for(int i=0; i<closures.Count;i++)
            {
                writer.Write("{0},", i);
                Closure closure = closures[i];
                foreach(Token token in Token.tokens)
                {
                    PrintoutConvertLine(i, token);
                }
                PrintoutConvertLine(i, Token.GetToken(-1));
                writer.WriteLine("");
            }
            writer.Close();
            Console.ReadLine();
        }
    }

}
