using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntepreterProgram
{
    class ProcessUtil
    {
        public static string[] RemoveUseless(string[] Input)
        {
            List<string> Output = new List<string>();
            //check if multiline comment is enabled
            bool multiline = false;
            foreach (string line in Input)
            {
                string outline = "";
                //remove multiple space
                int space_num = 0;
                //iterative through lines, remove useless symbols
                for (int i = 0; i<line.Length; i++)
                {

                    if (multiline)
                    {
                        if (i + 2 == line.Length)
                            continue;
                        if (line[i] == '*' && i+1<line.Length && line[i + 1] == '/')
                        {
                            multiline = false;
                            i += 2;
                        }
                        else
                            continue;
                    }
                    if(line[i] == '/')
                    {
                        if (i + 1 >= line.Length)
                            continue;
                        if(line[i+1] == '/')
                            break;
                        if (line[i + 1] == '*')
                        {
                            multiline = true;
                            i += 1;
                        }
                    }
                    //simple trick to remove multiple space
                    if (line[i] == ' ')
                    {
                        space_num++;
                        continue;
                    }
                    else
                    {
                        if (space_num != 0)
                        {
                            space_num = 0;
                            outline = outline + ' ';
                        }
                    }
                    //check if there is any tab or changeline
                    if ("\n\r\t".Contains(line[i]))
                        continue;
                    outline = outline + line[i];
                }
                //filter lines with no valid characters
               // if(outline.Length>0)
                Output.Add(outline);
            }
            return Output.ToArray();
            
        }

        // c++ style helper function,decided whether 'a' is an alpha
        private static bool isalpha(char a)
        {
            return ((a >= 'A' && a <= 'Z') || (a>='a' && a<='z'));
        }
        
        private static bool isdigit(char a)
        {
            return (a >= '0' && a <= '9');
        }
        //using finite automata to analyze the string.
        //It will recursively read character in the string, so it is not necessarily begin in the first character.
        public static ReturnCode FA_Analasis(State state, string input_string, int number)
        {
            if (number == input_string.Length)
            {
                if (state == State.eEnd || state == State.DotEnd) return  new ReturnCode(-1, State.Error);
                else
                {
                    return new ReturnCode(number, state);
                }
            }
            char now_char = input_string[number];

            switch (state)
            {
                case State.Start:
                    {
                        int op;
                        if (LexTable.Operator.Contains(now_char))
                        {
                            if (now_char == ' ')
                                return new ReturnCode(1, State.Operator);
                            state = State.Equal;
                        }
                        else if (isalpha(now_char)) state = State.Identifier;
                        else if (isdigit(now_char)) state = State.Number;
                        else state = State.Error;
                        return FA_Analasis(state, input_string, number + 1);
                    }
                case State.Equal:
                    {
                        if (now_char != '=')
                            return new ReturnCode(1, State.Operator);
                        else
                            return new ReturnCode(2, State.Operator);
                    }
                case State.RealNum:
                    {
                        //if next one is a digit, then expand the real number, otherwise it should return immediately.
                        if (isdigit(now_char)) return FA_Analasis(State.RealNum, input_string, number + 1);
                        else return new ReturnCode(number, State.RealNum);
                    }
                case State.DotEnd:
                    {
                        //end with dot, this is not valid in C.
                        if (isdigit(now_char)) return FA_Analasis(State.DotNum, input_string, number + 1);
                        else return new ReturnCode(number, State.Error);
                    }
                case State.Number:
                    {
                        if (isdigit(now_char)) return FA_Analasis(State.Number, input_string, number + 1);
                        else if (now_char == '.') return FA_Analasis(State.DotEnd, input_string, number + 1);
                        else if (now_char == 'e') return FA_Analasis(State.eEnd, input_string, number + 1);
                        else return new ReturnCode(number, State.Number);
                    }
                case State.DotNum:
                    {
                        if (isdigit(now_char)) return FA_Analasis(State.DotNum, input_string, number + 1);
                        else if (now_char == 'e') return FA_Analasis(State.eEnd, input_string, number + 1);
                        else return new ReturnCode(number , State.DotNum);
                    }
                case State.eEnd:
                    {
                        if (isdigit(now_char)) return FA_Analasis(State.RealNum, input_string, number + 1);
                        else return new ReturnCode(-1,State.Error);
                    }
                case State.Identifier:
                    {
                        if (isdigit(now_char) || isalpha(now_char) || now_char == '_') return FA_Analasis(State.Identifier, input_string, number + 1);
                        else if (input_string[number - 1] == '_')
                            return new ReturnCode(number, State.Error);
                        else
                            return new ReturnCode(number, State.Identifier);
                    }
                default:
                    return new ReturnCode(-1, State.Error);
            }
        }
    }
}
