using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntepreterProgram.Parser
{

    public enum Ttype
    {
        Terminal,
        Nontermi
    }

    public class Token
    {
        public Ttype ttype;
        public string name;
        public bool Emptyable = false;

        public Token(Ttype t, string n)
        {
            ttype = t;
            name = n;
        }
        private static Token endToken = new Token(Ttype.Terminal, "#");

        //get token by ID
        public static Token GetToken(int i)
        {
            if(i != -1)
            {
                return Token.tokens[i];
            }
            return endToken;
        }
        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Token token = (Token)obj;
            return (this.ttype == token.ttype && this.name == token.name);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public static List<Token> tokens = new List<Token>();
        public static int getID(string tok)
        {
            if (tok == "") return -1;
            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i].name == tok)
                {
                    return i;
                }
            }
            return -1;
        }
        public static int getID(Ttype t, string n)
        {
            Token newT = new Token(t, n);
            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i] == newT)
                {
                    return i;
                }
            }
            tokens.Add(newT);
            return tokens.Count - 1;
        }

        public static int getID(Token tok)
        {
            return Token.tokens.IndexOf(tok);
        }

        public override string ToString()
        {
            return this.name;
        }
    }

}
