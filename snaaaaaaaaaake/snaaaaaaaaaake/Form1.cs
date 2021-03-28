using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace snaaaaaaaaaake
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
              

        void SnakeElement(Form form, float x, float y)
        {
            Graphics g;
            g = form.CreateGraphics();
            Pen mypen = new Pen(Color.Black, 2);
            g.DrawEllipse(mypen, x, y, 10, 10);
            g.FillEllipse(new SolidBrush(Color.Black), x, y, 10, 10);
            g.SmoothingMode = SmoothingMode.HighQuality;  //图片柔顺模式选择
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;//高质量
            g.CompositingQuality = CompositingQuality.HighQuality;//再加一点
            g.Dispose();
            mypen.Dispose();
            GC.Collect();
        }
    }
    }
}
