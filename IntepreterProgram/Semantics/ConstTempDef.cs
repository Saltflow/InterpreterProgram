using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntepreterProgram.Semantics
{
    /* Constant /Temperate Value/ Opearator
     */
    class Temp : IOperator
    {
        string val;
        public string getValue()
        {
            return this.val;
        }
        public void setValue(string val)
        {
            this.val = val;
        }
        public override string ToString()
        {
            return "Tempval " + this.getValue();
        }
    }

    class Const : IOperator
    {
        string val;
        public string getValue()
        {
            return this.val;
        }
        public void setValue(string val)
        {
            this.val = val;
        }
        public override string ToString()
        {
            return this.val;
        }
    }
    //definition helper
    class Def : IOperator
    {
        string val;
        public string getValue()
        {
            return this.val;
        }
        public void setValue(string val)
        {
            this.val = val;
        }
    }
}
