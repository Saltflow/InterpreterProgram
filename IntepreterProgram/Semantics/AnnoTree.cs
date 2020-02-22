using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IntepreterProgram.Semantics
{
    partial class AnnoTree:Parser.ParseTree
    {
        public new AnnoNode Root;
        public static AnnoNode NodeOnExecute;
        public static Mutex MessageLock = new Mutex();
        public static Queue<Variables> AttribQueue = new Queue<Variables>();

        public void setAnnotation()
        {
            AnnoNode.ResultID = 0;
            Root.setAnotation();
        }
        public void SemanticAnalysis()
        {
            Root.varTable = new IdentiTable();
            Root.SemanticAnalysis();
            UIHelper.Log("Semantic Analyisis complete");
        }
    }
    

}
