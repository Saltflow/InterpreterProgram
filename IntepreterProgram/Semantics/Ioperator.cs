using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntepreterProgram.Semantics
{
    /* Ioperator defines an operator type that can be used in midCode
     * mainly divided into 3 parts: variables, constant and temporary value*/
    interface IOperator
    {
        string getValue();
        void setValue(string val);
    }
}
