using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntepreterProgram
{
    class ReturnCode
    {
        public int Number;
        public State result;
        public ReturnCode(int num, State state)
        {
            this.Number = num;
            this.result = state;
        }
        
    }
    class Predefined
    {
        public int id;
        public string value;
    }

    class Result:Predefined
    {
        public ResultType type;

        public int lineNum;

        public Result(ResultType t, string v, int i)
        {
            type = t;
            value = v;
            id = i;
        }

        public override string ToString()
        {
            return type.ToString() + " " + value.ToString(); 
        }

        public Result(State s,string stv,int line)
        {
            value = stv;
            lineNum = line;
            switch (s)
            {
                // Possible Identifer could be reserved word, type descriptor, or user identifer
                case State.Identifier:
                    {
                        int resid = LexTable.IsReserved(stv);
                        if ((resid) != -1)
                        {
                            if (stv == "int" || stv == "float")
                            {
                                type = ResultType.TypeName;
                            }
                            else if(stv == "true" || stv == "false")
                            {
                                type = ResultType.ResBool;
                                id = resid;
                            }
                            else if(stv == "printf")
                            {
                                type = ResultType.Output;
                            }
                            else if(stv == "scanf")
                            {
                                type = ResultType.Input;
                            }
                            else if (stv == "return")
                            {
                                type = ResultType.Return;
                                id = resid;
                            }
                            else
                            {
                                type = ResultType.If + resid;
                                id = resid;
                            }
                        }
                        else
                        {
                            type = ResultType.Identifier;
                            id = LexTable.getID(stv, LexTable.idCounter);
                        }
                        break;
                    }
                //real number have the same type
                case State.RealNum:
                case State.DotNum:
                    {
                        type = ResultType.Constant;
                        id = LexTable.getID(stv, LexTable.constantCounter);
                        break;
                    }
                case State.Number:
                    {
                        type = ResultType.Constant;
                        id = LexTable.getID(stv, LexTable.constantCounter);
                        break;
                    }
                case State.Error:
                    {
                        type = ResultType.Error;
                        id = -1;
                        value = stv;
                        break;
                    }
                case State.Operator:
                case State.Equal:
                    {
                        switch (stv)
                        {
                            case ";":
                                type = ResultType.colon;break;
                            case "{":
                                type = ResultType.lBrace;break;
                            case "}":
                                type = ResultType.rBrace;break;
                            case "(":
                                type = ResultType.lBrack; break;
                            case ")":
                                type = ResultType.rBrack; break;
                            case "[":
                                type = ResultType.lsBrack; break;
                            case "]":
                                type = ResultType.rsBrack; break;
                            case "=":
                                type = ResultType.Assigner; break;
                            case ",":
                                type = ResultType.Comma;break;
                            default:
                                type = ResultType.Operator;break;
                        }
                        id = LexTable.Operator.LastIndexOf(stv[0]);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
    }
    public enum State
    {
        Start,
        Operator,
        Equal,
        Error,
        DotEnd,
        Number,
        DotNum,
        RealNum,
        Identifier,
        eEnd,
        End
    }
    public enum ResultType
    {
        If,
        Else,
        For,
        While,
        Identifier,
        Operator,
        Assigner,
        TypeName,
        Input,
        Output,
        lBrace,// {
        rBrace,// }
        colon,// ;
        lBrack,// (
        rBrack, // )
        lsBrack,// [
        rsBrack, // ]
        Comma, // ,
        ResBool, //reserved bool, including true and false
        Constant,
        Return,
        Error
    }
}
