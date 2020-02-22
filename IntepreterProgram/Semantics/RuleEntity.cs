using IntepreterProgram.Parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntepreterProgram.Semantics
{
    class RuleProduction:Production
    {
        public string[] SDTRule;
        public RuleProduction(string rule):base(rule)
        {
            string[] productionandrule = rule.Split('|');
            SDTRule = productionandrule[1].Split(' ');
        }

       
    }
}
