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
            if (button1.Text == "开始游戏")
            {
                IsStart = true;
                timer1.Enabled = true;
                position = new Point(0, 0);
            }
            else
                return;
        }

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

        Element element = new Element();
        public void Drawsnake(Form form, List<Point> points)
        {
            for (int i = 0; i < points.Count; i++)
            {
                element.SnakeElement(form, points[i].X, points[i].Y);
            }
        }

        void drawsnake()
        {
            //将旧蛇删除
            element = new Element();
            element.ClearSnake(this);
            //判断方向
            switch (Directionindex)
            {
                case 1:
                    position.Y -= offset;
                    break;
                case 2:
                    position.Y += offset;
                    break;
                case 3:
                    position.X -= offset;
                    break;
                case 4:
                    position.X += offset;
                    break;
            }
            //length是蛇的长度
            if (positions.Count < length)
                positions.Add(position);
            else
            {
                positions.RemoveAt(0);
                positions.Add(position);
            }
            snake.Drawsnake(this, positions);
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    if (Directionindex == 1 || Directionindex == 2)
                        return;
                    else
                        Directionindex = 1;
                    break;
                case Keys.Down:
                    if (Directionindex == 2 || Directionindex == 1)
                        return;
                    else
                        Directionindex = 2;
                    break;
                case Keys.Left:
                    if (Directionindex == 3 || Directionindex == 4)
                        return;
                    else
                        Directionindex = 3;
                    break;
                case Keys.Right:
                    if (Directionindex == 4 || Directionindex == 3)
                        return;
                    else
                        Directionindex = 4;
                    break;
            }

        }
        //没有这段程序无法识别方向键
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Up || keyData == Keys.Down ||
                keyData == Keys.Left || keyData == Keys.Right)
                return false;
            else
                return base.ProcessDialogKey(keyData);
        }
        public class Food
        {
            Graphics g;
            public void DrawFood(Form form, int x, int y)
            {
                g = form.CreateGraphics();
                Pen mypen = new Pen(Color.Black, 2);
                g.DrawEllipse(mypen, x, y, 10, 10);
                g.FillEllipse(new SolidBrush(Color.Red), x, y, 10, 10);
                g.Dispose();
                mypen.Dispose();
                GC.Collect();
            }
        }

        void drawfood(Point point)
        {
            food.DrawFood(this, point.X, point.Y);
        }

        void changefood()
        {
            Random random = new Random();
            int x = random.Next(0, 30);
            int y = random.Next(0, 30);
            x = x * 10;
            y = y * 10;
            foodposition = new Point(x, y);
        }
        /// <summary>
        /// 记录分数
        /// </summary>
        int score = 0;

        /// <summary>
        /// 蛇的长度
        /// </summary>
        int length = 10;

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Interval = 400;
            label2.Text = score.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "开始游戏")
            {
                IsStart = true;
                timer1.Enabled = true;
                position = new Point(0, 0);
            }
            else
                return;
        }

        void checkFood()
        {
            if (positions[positions.Count - 1] == foodposition)
            {
                changefood();
                length += 1;
                score += 1;
                label2.Text = score.ToString();
            }
            else
                return;
        }

        void restart()
        {         
            positions.Clear();
            position = new Point(0, 0);
            Directionindex = 4;
            timer1.Enabled = true;
            timer1.Interval = 400;
            length = 10;
            score = 0;
            label2.Text = score.ToString() ;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (timer1.Interval > 50)
                timer1.Interval -= 50;
            else
                return;
        }

        void checkAlive()
        {
            if (positions.Count > 3)
            {
                for (int i = 0; i < positions.Count - 1; i++)
                {
                    if (positions[positions.Count - 1] == positions[i])
                    {
                        timer1.Enabled = false;
                        if (MessageBox.Show("你死了，是：退出游戏，否：重新开始", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            this.Close();
                        }
                        else
                        {
                            if (score > Convert.ToInt32(label4.Text))
                                label4.Text = score.ToString();
                            restart();
                        }
                    }
                }

                if (positions[positions.Count - 1].X < 0 || positions[positions.Count - 1].X > this.Width
                || positions[positions.Count - 1].Y < 0 || positions[positions.Count - 1].Y > this.Height)
                {
                    timer1.Enabled = false;
                    if (MessageBox.Show("你死了，是：退出游戏，否：重新开始", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        this.Close();
                    }
                    else
                    {
                        //记录最高分
                        if (score > Convert.ToInt32(label4.Text))
                            label4.Text = score.ToString();
                        restart();
                    }
                }
                else
                    return;
            }
        }
    }

