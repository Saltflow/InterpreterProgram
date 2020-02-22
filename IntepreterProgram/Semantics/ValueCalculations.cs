using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntepreterProgram.Semantics
{
    class ValueCalculations
    {
        /* input: value
         * output: false if it cannot be parse as integer
         */
        public static bool IsInteger(string val)
        {
            try
            {
                int.Parse(val);
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
        public static string Add(IOperator op1, IOperator op2)
        {
            string val1 = op1.getValue();
            string val2 = op2.getValue();
            if(!IsInteger(val1) || !IsInteger(val2))
            {
                return (float.Parse(val1) + float.Parse(val2)).ToString();
            }
            return (int.Parse(val1) + int.Parse(val2)).ToString();

        }
        public static string Subtract(IOperator op1, IOperator op2)
        {
            string val1 = op1.getValue();
            string val2 = op2.getValue();
            if (!IsInteger(val1) || !IsInteger(val2))
            {
                return (float.Parse(val1) - float.Parse(val2)).ToString();
            }
            return (int.Parse(val1) - int.Parse(val2)).ToString();

        }
        public static string Multiply(IOperator op1, IOperator op2)
        {
            string val1 = op1.getValue();
            string val2 = op2.getValue();
            if (!IsInteger(val1) || !IsInteger(val2))
            {
                return (float.Parse(val1) * float.Parse(val2)).ToString();
            }
            return (int.Parse(val1) * int.Parse(val2)).ToString();
        }

        /* we do sanity check in ParseExpr
         * Here we just assume op2 is diversable
         * */
        public static string Diverse(IOperator op1, IOperator op2)
        {
            string val1 = op1.getValue();
            string val2 = op2.getValue();
            if (!IsInteger(val1) || !IsInteger(val2))
            {
                return (float.Parse(val1) / float.Parse(val2)).ToString();
            }
            return (int.Parse(val1) / int.Parse(val2)).ToString();

        }

        public static string Greater(IOperator op1, IOperator op2)
        {
            string val1 = op1.getValue();
            string val2 = op2.getValue();
            if (!IsInteger(val1) || !IsInteger(val2))
            {
                return (float.Parse(val1) > float.Parse(val2)).ToString();
            }
            return (int.Parse(val1) > int.Parse(val2)).ToString();
        }

        public static string Smaller(IOperator op1, IOperator op2)
        {
            string val1 = op1.getValue();
            string val2 = op2.getValue();
            if (!IsInteger(val1) || !IsInteger(val2))
            {
                return (float.Parse(val1) < float.Parse(val2)).ToString();
            }
            return (int.Parse(val1) < int.Parse(val2)).ToString();
        }

        public static string Equal(IOperator op1, IOperator op2)
        {
            string val1 = op1.getValue();
            string val2 = op2.getValue();
            if (!IsInteger(val1) || !IsInteger(val2))
            {
                return (float.Parse(val1) == float.Parse(val2)).ToString();
            }
            return (int.Parse(val1) == int.Parse(val2)).ToString();
        }
    }
}
