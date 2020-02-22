using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IntepreterProgram.Semantics
{

    class IdentiTable
    {
        // int and vari defines a variable
        List<Variables> declaredVaries = new List<Variables>();
        IdentiTable parentScope = null;
        public static List<IdentiTable> allTables = new List<IdentiTable>();
        public IdentiTable Parent
        {
            set { this.parentScope = value; }
        }

        public IdentiTable()
        {
            allTables.Add(this);
        }
        public IdentiTable(int i)
        {
            this.declaredVaries = new List<Variables>();
            allTables[i] = this;
        }
        public static bool declareVariable(Variables vari,ref IdentiTable table)
        {
            foreach(Variables declared in table.declaredVaries)
            {
                if(declared.name == vari.name)
                {
                    return false;
                }
            }
            table.declaredVaries.Add(vari);
            return true;
        }

        public static bool declareArray(CMArray vari, ref IdentiTable table)
        {
            foreach (Variables declared in table.declaredVaries)
            {
                if (declared.name == vari.name)
                {
                    return false;
                }
            }
            table.declaredVaries.Add(vari);
            return true;
        }

        public static Variables FetchVal(Variables vari,IdentiTable table)
        {
            IdentiTable nowTable = table;
            while(nowTable != null)
            {
                foreach (Variables declared in nowTable.declaredVaries)
                {
                    if (declared.name == vari.name)
                    {
                        return declared;
                    }
                }
                nowTable = nowTable.parentScope;
            }
            return vari;
        }

        public static Variables FetchArray(CMArray vari, string i,IdentiTable table)
        {
            CMArray array = (CMArray)FetchVal(vari,table);
            int index = int.Parse(i);
            array.Lookup(index);
            if (array.Initialized)
            {
                if(array.currIndex >= array.Maxindex)
                {
                    UIHelper.Log("Semantic Error: array overflow");
                    Thread.CurrentThread.Abort();
                }
                return array.currVari;
            }
            else
            {
                array.currIndex = index;
                return array;
            }
        }

        //Iteratively get all variables and arrays, for output and display 
        public static string GetAllVarAndVal()
        {
            string outstring = "";
            for (int i = 0; i < allTables.Count; i++)
            {
                List<Variables> variables = allTables[i].declaredVaries;
                for (int j = 0; j < variables.Count; j++)
                {
                    outstring += variables[j].name + "\t" + variables[j].getValue() + "\t" + variables[j].Type + "\t" + i.ToString() + "\r\n";
                }
            }
            return outstring;
        }
    }
}
