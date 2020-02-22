using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IntepreterProgram.Parser;

namespace IntepreterProgram.Semantics
{
    class SDTranslator:Parser.Parser
    {
        public int head = 10;

        public static new AnnoTree ParseTree = new AnnoTree();

        public override void AcceptAction()
        {
            ParseTree.Root = new AnnoNode(ParserResult[ResultID]);
            GenerateParseTree(ParseTree.Root);
            ParseTree.setAnnotation();
            UIHelper.Log("Set Annotation Complete");
        }

        public AnnoNode GenerateParseTree(AnnoNode node)
        {
            if (node.thisTok.ttype == Ttype.Terminal)
                return node;
            Production currRule = ParserResult[ResultID];
            node.geneTok = new AnnoNode[currRule.Rules().Length];
            node.mProduction = new RuleProduction(currRule.mrule);

            for (int i = currRule.Rules().Length - 1; i >= 0; i--)
            {
                Token token = Token.GetToken(currRule.Rules()[i]);
                if (token.ttype == Ttype.Nontermi) ResultID--;
                node.geneTok[i] = GenerateParseTree(new AnnoNode(token));
            }
            return node;
        }

        /* parse expression is one of the semantic movement
         * to place it here just for balancing code number
         * althought it increases coupling and reduce integration*/
        public static void ParseExpr(AnnoNode node)
        {
            node.geneTok[0].SemanticAnalysis();
            node.geneTok[2].SemanticAnalysis();
            Def opera = (Def)node.geneTok[1].mOp;

            IOperator a = node.geneTok[0].mOp;
            IOperator b = node.geneTok[2].mOp;

            string ans = "0";
            switch (opera.getValue())
            {
                case "+":
                    {
                        ans = ValueCalculations.Add(a,b);
                        break;
                    }
                case "-":
                    {
                        ans = ValueCalculations.Subtract(a,b);
                        break;
                    }
                case "*":
                    {
                        ans = ValueCalculations.Multiply(a,b);
                        break;
                    }
                case "/":
                    {
                        //TODO :Handling semantic error
                        if(b.getValue() == "0")
                        {
                            UIHelper.Log("Error in Semantic Analyzer: divide by 0");
                            Thread.CurrentThread.Abort();
                        }
                        ans = ValueCalculations.Diverse(a,b);
                        break;
                    }
                case ">=":
                    {
                        ans = (bool.Parse(ValueCalculations.Greater(a, b)) || bool.Parse(ValueCalculations.Equal(a, b))).ToString();
                        break;
                    }
                    
                case "<=":
                    ans = (bool.Parse(ValueCalculations.Smaller(a, b)) || bool.Parse(ValueCalculations.Equal(a, b))).ToString();
                    break;
                case ">":
                    ans = ValueCalculations.Greater(a, b);
                    break;               
                case "<":
                    ans = ValueCalculations.Smaller(a, b);
                    break;
            }
            node.mOp.setValue(ans);
        }
    }
}
