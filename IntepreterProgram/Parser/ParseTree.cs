using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntepreterProgram.Parser
{
    public class ParseTree
    {
        public ParseNode Root;
    }
    public class ParseNode
    {
        public Token thisTok;
        public ParseNode[] geneTok;
        public Production mProduction;
        //generate node through the production
        public ParseNode(Production Production)
        {
            this.mProduction = Production;
            thisTok = Token.GetToken(Production.Nonterminal());
        }


        public ParseNode(Token mytok)
        {
            thisTok = mytok;
        }

        public override string ToString()
        {
            return thisTok.ToString();
        }

    }
}
