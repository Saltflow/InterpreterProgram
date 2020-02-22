using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IntepreterProgram.Semantics
{
    public partial class Identitables : Form
    {
        public Identitables()
        {
            InitializeComponent();
            IdentTextBox.Text += "name\tvalue\ttype\ttable\t\r\n";
            IdentTextBox.Text += IdentiTable.GetAllVarAndVal();
        }
        public Identitables (string TableTop, string TableContent)
        {
            InitializeComponent();
            IdentTextBox.Text += TableTop;
            IdentTextBox.Text += TableContent;
        }
    }
}
