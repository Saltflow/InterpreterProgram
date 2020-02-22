using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IntepreterProgram.Semantics
{
    class AnnoNode : Parser.ParseNode
    {
        public IOperator mOp;
        public new AnnoNode[] geneTok;
        public new RuleProduction mProduction;
        public IdentiTable varTable;

        public static bool debugging = false;
        public static bool debug_update = true;


        private void UnifyScope()
        {
            foreach (AnnoNode node in geneTok)
            {
                node.varTable = this.varTable;
            }
        }


        public AnnoNode(Parser.Production Production) : base(Production)
        {
            mProduction = new RuleProduction(Production.mrule);
        }
        public AnnoNode(Parser.Token token) : base(token) { }

        public static int ResultID = 0;
        public void setAnotation()
        {
            Result lexres = Analysis.ResultTable[ResultID];
            if (this.thisTok.ttype == Parser.Ttype.Terminal)
            {
                switch (lexres.type)
                {
                    case ResultType.Identifier:
                        {
                            Variables vari = new Variables();
                            vari.name = lexres.value;
                            this.mOp = vari;
                            break;
                        }
                    case ResultType.ResBool:
                    case ResultType.Constant:
                        {
                            Const con = new Const();
                            con.setValue(lexres.value);
                            this.mOp = con;
                            break;
                        }
                    default:
                        {
                            Def def = new Def();
                            def.setValue(lexres.value);
                            this.mOp = def;
                            break;
                        }
                }
                ResultID++;
            }
            else
            {
                Temp temp = new Temp();
                temp.setValue("0");
                this.mOp = temp;
                for (int i = 0; i < geneTok.Length; i++)
                {
                    geneTok[i].setAnotation();
                }
            }
        }
        public static void WaitforCommand()
        {
            if (debugging == true)
            {
                while (true)
                {
                    AnnoTree.MessageLock.WaitOne();
                    Thread.Sleep(100);
                    if (debug_update) break;
                    AnnoTree.MessageLock.ReleaseMutex();
                }
                debug_update = false;
            }
        }
        //Analyze semantics in current Node.
        public void SemanticAnalysis()
        {
            if (this.thisTok.ttype == Parser.Ttype.Terminal)
            {
                return;
            }
            UnifyScope();
            AnnoTree.NodeOnExecute = this;
            WaitforCommand();
            switch (this.mProduction.SDTRule[1])
            {
                case "fetch":
                    {
                        geneTok[0].SemanticAnalysis();
                        this.mOp.setValue(geneTok[0].mOp.getValue());
                        break;
                    }
                case "Parseexpr":
                    {
                        SDTranslator.ParseExpr(this);
                        break;
                    }
                case "assign":
                    {
                        this.geneTok[0].SemanticAnalysis();
                        this.geneTok[2].SemanticAnalysis();
                        this.geneTok[0].mOp.setValue(this.geneTok[2].mOp.getValue());
                        break;
                    }
                case "getMem"://fetch an Identifier
                    {
                        this.mOp = IdentiTable.FetchVal((Variables)this.geneTok[0].mOp, this.varTable);
                        break;
                    }
                case "getPoint":
                    {
                        this.geneTok[2].SemanticAnalysis();
                        CMArray arrayname = new CMArray();
                        Variables varname = (Variables)this.geneTok[0].mOp;
                        arrayname.name = varname.name;
                        arrayname.Type = varname.Type;
                        this.geneTok[0].mOp = arrayname;
                        this.mOp = IdentiTable.FetchArray((CMArray)this.geneTok[0].mOp, this.geneTok[2].mOp.getValue(), this.varTable);
                        break;
                    }
                case "addID":
                    {
                        this.geneTok[1].SemanticAnalysis();

                        if (geneTok[1].mOp.GetType().Name == "Variables")
                        {
                            Variables newVari = new Variables();
                            newVari.Type = this.geneTok[0].mOp.getValue();
                            newVari.name = ((Variables)this.geneTok[1].mOp).name;

                            if (IdentiTable.declareVariable(newVari, ref this.varTable) == false)
                            {
                                UIHelper.Log("Semantic Error: double declare");
                                Thread.CurrentThread.Abort();
                            }
                            this.mOp = newVari;
                        }
                        else
                        {
                            CMArray newArray = new CMArray();
                            newArray.name = ((CMArray)this.geneTok[1].mOp).name;
                            newArray.Type = this.geneTok[0].mOp.getValue();
                            newArray.currIndex = ((CMArray)this.geneTok[1].mOp).currIndex;
                            newArray.initArray(newArray.currIndex);
                            if (IdentiTable.declareArray(newArray, ref this.varTable) == false)
                            {
                                UIHelper.Log("Semantic Error: double declare");
                                Thread.CurrentThread.Abort();
                            }
                            this.mOp = newArray;
                        }
                        break;
                    }
                case "for":
                    {
                        this.geneTok[2].SemanticAnalysis();
                        this.geneTok[4].SemanticAnalysis();
                        while (booleanBranch(geneTok[4].mOp.getValue()))
                        {
                            geneTok[8].SemanticAnalysis();
                            geneTok[6].SemanticAnalysis();
                            geneTok[4].SemanticAnalysis();
                        }

                        break;
                    }
                case "ifcon":
                    {
                        this.geneTok[2].SemanticAnalysis();
                        if (booleanBranch(geneTok[2].mOp.getValue()))
                        {
                            this.geneTok[4].SemanticAnalysis();
                        }
                        this.mOp.setValue("1");
                        break;
                    }
                case "elsecon":
                    {
                        geneTok[0].SemanticAnalysis();
                        if (booleanBranch(geneTok[0].mOp.getValue()))
                            break;
                        else
                            geneTok[2].SemanticAnalysis();
                        break;
                    }
                case "while":
                    {
                        geneTok[2].SemanticAnalysis();
                        while (booleanBranch(geneTok[2].mOp.getValue()))
                        {
                            this.geneTok[4].SemanticAnalysis();
                        }
                        break;
                    }
                case "pass":
                    {
                        geneTok[0].SemanticAnalysis();
                        break;
                    }
                case "addTable":
                    {
                        geneTok[1].varTable = new IdentiTable();
                        geneTok[1].varTable.Parent = this.varTable;
                        geneTok[1].SemanticAnalysis();
                        IdentiTable.allTables.RemoveAt(IdentiTable.allTables.Count - 1);
                        break;
                    }
                case "pass2":
                    {
                        geneTok[0].SemanticAnalysis();
                        geneTok[1].SemanticAnalysis();
                        break;
                    }
                case "input":
                    {
                        this.geneTok[2].SemanticAnalysis();
                        while(AnnoTree.AttribQueue.Count != 0)
                        {
                            Variables frontVari = AnnoTree.AttribQueue.Dequeue();
                            frontVari.setValue(UIHelper.getInput());
                        }
                        break;

                    }
                case "pushStack":
                    {
                        this.geneTok[0].SemanticAnalysis();
                        AnnoTree.AttribQueue.Enqueue((Variables) this.geneTok[0].mOp);
                        if (this.geneTok.Length >= 2)
                            this.geneTok[2].SemanticAnalysis();
                        break;
                    }
                case "output":
                    {
                        this.geneTok[2].SemanticAnalysis();
                        while (AnnoTree.AttribQueue.Count != 0)
                        {
                            UIHelper.Output(AnnoTree.AttribQueue.Dequeue().getValue());
                        }
                        break;
                    }
                default:
                    break;
            }
        }


        static bool booleanBranch(string val)
        {
            if (val != "0" && val != "False")
                return true;
            return false;
        }

        public override string ToString()
        {
            return this.thisTok.ToString() + this.mOp.getValue();
        }
    }
}
