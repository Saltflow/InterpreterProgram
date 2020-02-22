using IntepreterProgram.TestProgram;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IntepreterProgram
{
    public partial class Mainform : Form
    {
        public Mainform()
        {
            InitializeComponent();
            UIHelper.UIForm = this;
            UIHelper.inTextbox = (TextBox)UIHelper.UIForm.Controls.Find("inputTextBox", true)[0];
            this.inputTextBox.Enabled = false;
            UIHelper.outTextBox = RealtimeResultBox;
            // BatchTest.Test();
        }


        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FilesHelper.OpenFile(ref this.MainTab);
            if(this.MainTab.Controls.Count!= 0)
                UIHelper.ExtractCode(this.MainTab);
        }


        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            UIHelper.ExtractCode(this.MainTab);
            UIHelper.OutInterpreteResult();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            UIHelper.OutParseResult();
        }

        private void contButton_Click(object sender, EventArgs e)
        {
            UIHelper.PressContinue(this.RealtimeResultBox);
        }

        private void toggleStepDebugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Semantics.AnnoNode.debugging = true;
        }

        private void compileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UIHelper.ExtractCode(this.MainTab);
            UIHelper.SematicDebugging();
        }
        /* syncronization
         */
        private void inputTextBox_TextChanged(object sender, EventArgs e)
        {
            /* flushing input textbox will trigger this event, 
             * while input textbox may have no character*/
            if (inputTextBox.TextLength == 0)
                return;
            int asciih = Convert.ToInt32(inputTextBox.Text[inputTextBox.TextLength - 1]);
            /* input equal to backspace, then load the input to buffer*/
            if(asciih == 10)
            {
                UIHelper.ReadtoBuffer();
            }

        }

        private void MainTab_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                this.Controls.Find("CMtext", true)[0].Size = this.Size;
            }
            catch(Exception)
            {

            }
        }

        private void inputTextBox_Leave(object sender, EventArgs e)
        {
            //UIHelper.IntepreteThread.Resume();
        }

        private void ShowAnalyzeButton_Click(object sender, EventArgs e)
        {
            if (Analysis.ResultTable.Count == 0)
                return;
            string TableTop = "type\tvalue\t\r\n";
            string TableContent = ""; 
            foreach(Result result in Analysis.ResultTable)
            {
                TableContent += result.ToString() + "\r\n";
            }
            Semantics.Identitables identitables = new Semantics.Identitables(TableTop, TableContent);
            identitables.Show();
        }

        private void identiTableButton_Click(object sender, EventArgs e)
        {
            Semantics.Identitables identitables = new Semantics.Identitables();
            identitables.Show();
        }
    }
}
