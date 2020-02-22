using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IntepreterProgram
{
    class Analysis
    {
        public static List<Result> ResultTable;
        public static List<Result> WrongTable;
        public static int Analyze(string[] inputProgram)
        {
            ResultTable = new List<Result>();
            WrongTable = new List<Result>();
            string[] inputProgs = ProcessUtil.RemoveUseless(inputProgram);

            //linenumber started with 0, be notice +1 before output so that human readable
            for (int linenumber = 0; linenumber < inputProgs.Length; linenumber++)
            {
                string inputString = inputProgs[linenumber];
                while(inputString.Length != 0)
                {
                    ReturnCode ret = ProcessUtil.FA_Analasis(State.Start, inputString,0);
                    if(ret.result == State.Error)
                    {
                        WrongTable.Add(new Result(State.Error, "line number "+(linenumber+1).ToString(), linenumber));
                        break;
                    }
                    if (inputString.Length - ret.Number < 0)
                    {
                        WrongTable.Add(new Result(State.Error, "line number " + (linenumber+1).ToString(),linenumber));
                        break;
                    }
                    string existString = inputString.Substring(0, ret.Number);
                    inputString = inputString.Substring(ret.Number);
                    if (existString == " ") continue;
                    ResultTable.Add(new Result(ret.result, existString,linenumber+1));
                }
            }
            if(WrongTable.Count != 0)
            {
                UIHelper.Log("Error on Lexical Analysis");
                for(int i=0;i<WrongTable.Count;i++)
                {
                    UIHelper.Log(WrongTable[i].ToString());
                }
                UIHelper.Log(GeneticStringClass.ITTerminate);
                Thread.CurrentThread.Abort();
            }
            UIHelper.Log("Lexical analyze finished");
            return -1;
        }
    }

}
