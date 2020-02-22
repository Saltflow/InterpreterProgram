using IntepreterProgram.Semantics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IntepreterProgram.Parser
{
    public partial class ShowParserTree : Form
    {
        public ShowParserTree(ParseTree parseTree)
        {
            InitializeComponent();
            this.AutoScaleBaseSize = new Size(6, 14);
            this.ClientSize = new Size(800, 600);
            this.Paint += new PaintEventHandler(this.ShowParserTree_Paint);
            this.parseTree = parseTree;
        }

        private ParseTree parseTree;

        private Graphics graphics;

        void drawTree(ParseNode node,int x0,int y0,int bound)
        {
            Brush brush = new SolidBrush(Color.Blue);
            Font font = new Font("Arial", 24);
            graphics.DrawString(node.ToString(),Font,brush,x0,y0);
            
            if (node.geneTok != null)
            {
                int len = bound / node.geneTok.Length;
                for(int i=0; i< node.geneTok.Length;i++)
                {
                    int x1 = x0 + (i-node.geneTok.Length/2)*len;
                    int y1 = y0 + 20;
                    drawLine(x0, y0, x1, y1, 1);
                    drawTree(node.geneTok[i], x1, y1, bound);
                }
            }
        }
        void drawLine(double x0, double y0, double x1, double y1, int width)
        {
            graphics.DrawLine(
                new Pen(Color.Red, width),
                (int)x0, (int)y0, (int)x1, (int)y1);
        }

        void clear()
        {
            graphics.Clear(Color.Gray);
            this.Controls.Clear();
        }

        public override void Refresh()
        {
            drawTree(parseTree.Root, this.Width/2, 10, this.Width);
        }

        private void ShowParserTree_Paint(object sender, PaintEventArgs e)
        {
            graphics = e.Graphics;
            drawTree(parseTree.Root, this.Width/2, 30, 200);
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
        }
    }
}
