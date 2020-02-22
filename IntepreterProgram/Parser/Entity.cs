using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntepreterProgram.Parser
{
    /* rules that includes current state
     for example if E := .aA 
     then lhandle = E
     place = 0
     rules = ['a','A']
    */
    public class Production
    {
        //All valid statements in current grammar
        public static List<Production> formalGrammar = new List<Production>();

        // where the next token suppose to be
        private int place;
        public int Place { get { return place; } }
        //token on the left of the statement
        private int lhandle;
        // direct derivations
        private int[] tokens;
       //used in LR(1), to solve ambiguous 
        private int nexttok;
        //keep a copy of rule in Production object
        public string mrule;

        public int Nonterminal()
        {
            return lhandle;
        }
        public int[] Rules()
        {
            return tokens;
        }

        public int Nexttok
        {
            get { return nexttok; }
        }
        public int GetNext()
        {
            /*using ID instead of token instance to peocess*/
            if (place == tokens.Length)
            {
                return -1;
            }
            return tokens[place];
        }
        //move forward of the place,
        //which builds a new Production
        public Production MoveForward()
        {
            if (this.place == tokens.Length)
                return null;
            Production newProduction = new Production(this,place+1,this.nexttok);
            return newProduction;
            
        }
        //construct an Production with grammar rule
        // the place should start with 0 on default
        //lhandle should be '#' on default
        public Production(string rule,int initplace = 0, int nexttok = -1)
        {
            mrule = rule;
            List<int> toks = new List<int>();
            string[] lr = rule.Split(' ');
            for (int i = 0; i < lr.Length; ++i)
            {
                //reserved for SDT
                if (lr[i] == "|")
                    break;
                if (i == 0)
                {
                    lhandle = Token.getID(lr[i]);
                    continue;
                }
                if (i == 1)
                {
                    continue;
                }

                int tokid = Token.getID(lr[i]);
                if(tokid != -1)
                    toks.Add(tokid);
            }
            tokens = toks.ToArray();
            place = initplace;
            this.nexttok = nexttok;
        }

        //generate an Production throungh a new Production
        public Production(Production former, int initplace = 0, int nexttok = -1)
        {
            this.tokens = former.Rules();
            this.nexttok = nexttok;
            this.place = initplace;
            this.lhandle = former.lhandle;
            this.mrule = former.mrule;
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Production Production = (Production)obj;
            if (Production.lhandle != lhandle || Production.place != place || Production.tokens.Length != tokens.Length || Production.nexttok != nexttok)
                return false;
            for(int i=0;i<tokens.Length;i++)
            {
                if(tokens[i] != Production.tokens[i])
                {
                    return false;
                }
            }
            return true;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            string outS = "";
            var alltokens = Token.tokens;
            outS += alltokens[lhandle].ToString() + ":= ";
            for(int i=0;i<this.tokens.Length;i++)
            {
                if (i == place)
                    outS += "~";
                outS += alltokens[tokens[i]].ToString() + " ";
            }
            if (nexttok == -1)
                outS += ",# \r\n";
            else
                outS += "," + alltokens[nexttok].ToString() + " \r\n";
            return outS;
        }
    
    }
}
