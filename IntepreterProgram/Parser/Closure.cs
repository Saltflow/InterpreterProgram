using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parsergenerator;
namespace IntepreterProgram.Parser
{
    class Closure
    {
        //main Production 
        //derived from groundProduction;
        public List<Production> entities;

        //given Production A := <alpha> ~B<beta>,a
        //return first(<beta>a)
        private static List<int> getFirst(Production e)
        {
            List<int> first = new List<int>();
            //where B in Rules
            int place = e.Place;
            //B is the last one,<beta> is empty
            if(place == e.Rules().Length - 1)
            {
                //end with #
                if(e.Nexttok == -1)
                {
                    //return #
                    first.Add(-1);
                    return first;
                }
                else
                {
                    //return a
                    return ParserGenerate.first[Token.GetToken(e.Nexttok)];
                }
            }
            else
            {
                // return first(<beta>)
                return ParserGenerate.first[Token.GetToken(e.Rules()[place+1])];
            }
        }

        // get a set of closures that derived from this one
        public void genClosure()
        {
            while (true)
            {
                int len = entities.Count;
                //init next first every time, for # cases
                List<int> nextFirst = null;
                //iterative through each Production A := <alpha>B<beta>,a
                for (int i = 0; i <len; i++)
                {

                    //TokID referes to ID of B
                    int TokID = entities[i].GetNext();
                    if (TokID == -1) continue;
                    nextFirst = getFirst(entities[i]);
                    //Every production B -> <gamma>
                    for (int j = 0; j < Production.formalGrammar.Count; j++)
                    {
                        if (Production.formalGrammar[j].Nonterminal() != TokID)
                            continue;
                        //foreach token in nextFirst
                        foreach (int nexttok in nextFirst)
                        {
                            Production newProduction = new Production(Production.formalGrammar[j], 0, nexttok);
                            //when the Production is new to the closure
                            if (!this.entities.Contains(newProduction))
                                this.entities.Add(newProduction);

                        }
                    }
                }
                if (entities.Count == len)
                {
                    break;
                }
            }
        }

        public static Closure gotoClosure(Closure I, Token X)
        {
            Closure J = new Closure();
            foreach(Production Production in I.entities)
            {
                if(Production.GetNext() == Token.getID(X))
                {
                    J.entities.Add(Production.MoveForward());
                }
            }
            J.genClosure();
            return J;
        }

        public Closure()
        {
            this.entities = new List<Production>();
        }

        public Closure(Production ground)
        {
            this.entities = new List<Production>();
            this.entities.Add(ground);
        }

        public Closure(Production[] entities)
        {
            this.entities = new List<Production>(entities);
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Closure closure = (Closure)obj;
            //return this.groundProduction == closure.groundProduction;
            if (closure.entities.Count != entities.Count)
                return false;
            
            for(int i=0;i<closure.entities.Count; i++)
            {
                if (!closure.entities[i].Equals(entities[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public bool emplace(Production e)
        {
            if (e == null)
                return true;
            return entities.Contains(e);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            string outS = "Closure :";
            foreach(Production Production in this.entities)
            {
                outS += Production.ToString() + "\n";
            }
            return outS;
        }

    }


}
