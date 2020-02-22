using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntepreterProgram.Semantics
{
    /* Variables and array
     */
    class Variables : IOperator
    {
        private string val;
        private int intval;
        private float floatval;
        string type;
        public string name;
        public virtual string getValue()
        {
            return this.val;
        }
        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        public virtual void setValue(string val)
        {
            if (this.type == null)
            {
                ;
            }
            this.val = val;
            if (this.type == "float")
            {
                floatval = float.Parse(val);
            }
            else
            {
                intval = int.Parse(val);
            }
        }

        public override string ToString()
        {
            return this.name + this.val;
        }


    }

    class CMArray : Variables
    {
        private Variables[] arrayvals;
        public int currIndex = 0;
        private bool initialized = false;
        private int arrayLength = -1;

        public int Maxindex{ get{return this.arrayLength;}}

        public bool Initialized
        {
            get { return this.initialized; }
        }
        public void initArray(int i)
        {
            arrayvals = new Variables[i];
            int index = 0;
            for(int j= 0;j<arrayvals.Length;j++)
            {
                Variables member = new Variables();
                member.Type = this.Type;
                member.name = this.name+(index++).ToString();
                arrayvals[j] = member;
            }
            arrayLength = i;
            initialized = true;
        }
        public Variables currVari
        {
            get { return arrayvals[currIndex]; }
        }

        public override string getValue()
        {
            if (initialized)
                return arrayvals[currIndex].getValue();
            else return "-1";
        }

        public override void setValue(string val)
        {
            this.arrayvals[currIndex].setValue(val);
        }

        public void Lookup(int i)
        {
            this.currIndex = i;
        }
    }
}
