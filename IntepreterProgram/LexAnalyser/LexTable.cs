using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntepreterProgram
{
    class LexTable
    {
        public static string[] ReservedTable = 
        {
          "if",
          "else",
          "for",
          "while",
          "int",
          "float",
          "true",
          "false",
          "return",
          "printf",
          "scanf"
        };

        public static string[] WordType = 
        {
            "Identifier",
            "Operator",
            "Reserved"
        };

        public static string[] ValType = {
            "Real",
            "Integer"
        };

        public static string Operator = " +-*/()<>[]&|!={};,";
        public static int IsReserved(string next)
        {
            int tableid = 0;
            foreach (var i in ReservedTable)
            {
                if (i == next) return tableid;
                tableid++;
            }
            return -1;
        }

        public static List<string> idCounter = new List<string>();
        public static List<string> constantCounter = new List<string>();
        public static int getID(string sig, List<string> counter)
        {
            for (int i = 0; i < counter.Count; i++)
            {
                if (sig == counter[i]) return i;
            }
            counter.Add(sig);
            return counter.Count - 1;
        }

    }
}
