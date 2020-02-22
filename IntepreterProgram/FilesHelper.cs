using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IntepreterProgram
{
    class FilesHelper
    {
        static string getFilename(string path)
        {
            string []names = path.Split('\\');
            return names[names.Length - 1];
        }

        public static string OpenFile(ref TabControl tabControl)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Multiselect = false;
            openFile.Filter = "CMM程序|*.cmm";
            string allFiles;
            if (DialogResult.OK == openFile.ShowDialog())
            {
                allFiles = openFile.FileName;
            }
            else
            {
                return "";
            }

            StreamReader reader = new StreamReader(allFiles);
            string Source = "";
            while (!reader.EndOfStream)
            {
                Source += reader.ReadLine() + "\r\n";
            }
            TextBox textBox = new TextBox();
            TabPage newtab = new TabPage();
            newtab.Controls.Add(textBox);
            newtab.Size = new System.Drawing.Size(tabControl.Size.Width - 9, tabControl.Size.Height - 24);
            newtab.Text = getFilename(allFiles);
            textBox.Name = "CMtext";
            textBox.Size = newtab.Size;
            textBox.Multiline = true;
            textBox.Text = Source;
            tabControl.Controls.Add(newtab);
            textBox.ScrollBars = ScrollBars.Both;
            return allFiles;
        }
    }
}
