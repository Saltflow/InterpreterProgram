using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntepreterProgram.Parser
{
    class StrType
    {
        public static Dictionary<ResultType, Token> Res2Token = new Dictionary<ResultType, Token>();
        public static void BuildMapping()
        {
            int Restype = 0;
            foreach(Token token in Token.tokens)
            {
                if (token.ttype == Ttype.Terminal)
                {
                    Res2Token[Restype + ResultType.If] = token;
                    Restype++;
                }
            }
        }
    }
}
