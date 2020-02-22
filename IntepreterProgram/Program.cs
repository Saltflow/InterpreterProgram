using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IntepreterProgram
{
    static class Program
    {
        public static bool AutoDebugTesting = false;
        static void Init()
        {
            Parsergenerator.ParserGenerate.GenerateParser();
            Parser.StrType.BuildMapping();
        }

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
             Application.EnableVisualStyles();
             Application.SetCompatibleTextRenderingDefault(false);
             Application.Run(new Mainform());
           // TestProgram.BatchTest.OpenTest();

           // Application.Run(new Parser.ShowParserTree());
          //  Semantics.SDTranslator.ParseTree.SemanticAnalysis();
        }
    }
}
