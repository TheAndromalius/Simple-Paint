using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Drawing.Drawing2D;

namespace сердешко
{
    public partial class Paint_Form : Form
    {
        public Paint_Form()
        {
            InitializeComponent();
            SetSize();
        }
        private bool doDraw = false;
        private ArrayPoints arrayPoints = new ArrayPoints(2);
        Bitmap map = new Bitmap(100, 100);
        Graphics graphics;
        Pen pen = new Pen(Color.Black, 3f);
        SolidBrush brush = new SolidBrush(Color.Black);
        private void SetSize()
        {
            Rectangle rectangle = Screen.PrimaryScreen.Bounds;
            map = new Bitmap(rectangle.Width, rectangle.Height);
            graphics = Graphics.FromImage(map);

            pen.StartCap = LineCap.Round; 
            pen.EndCap = LineCap.Round;
        }
        private class ArrayPoints
        {
            private int index = 0;
            private Point[] points;

            public ArrayPoints(int size)
            {
                if(size <= 0) { size = 2; }
                points = new Point[size];
            }

            public void SetPoint(int x, int y)
            {
                if (index >= points.Length)
                {
                    index = 0;
                }
                points[index] = new Point(x, y);
                index++;
            }

            public void Resetpoints()
            {
                index = 0;
            }

            public int GetCountPoints()
            {
                return index;
            }

            public Point[] GetPoints()
            {
                return points;
            }
        }

        private void paint_MouseDown(object sender,
        System.Windows.Forms.MouseEventArgs e)
        {
            doDraw = true;
            arrayPoints.Resetpoints();
        }

        private void paint_MouseUp(object sender,
          System.Windows.Forms.MouseEventArgs e)
        {
            doDraw = false;
            
        }

        private void paint_MouseMove(object sender,
        System.Windows.Forms.MouseEventArgs e)
        {
            if (!doDraw) { return; }
            {
                arrayPoints.SetPoint(e.X, e.Y);
                if(arrayPoints.GetCountPoints() >=2)
                {
                    graphics.DrawLines(pen,arrayPoints.GetPoints());
                    paint.Image = map;
                    arrayPoints.SetPoint(e.X,e.Y);
                }
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            pen.Color = ((Button)sender).BackColor;
        }

        private void button23_Click(object sender, EventArgs e)
        {
            if(colorDialog1.ShowDialog() == DialogResult.OK)
            {
                pen.Color = colorDialog1.Color; // цвет кисти
                ((Button)sender).BackColor = colorDialog1.Color; // цвет кнопки на интерфейсе
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            graphics.Clear(Color.White); // цвет который был по умолчанию у нашего PictureBox
            paint.Image = map; // присваем изоборжожению снова значение map
        }

        private void button2_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "JPG(*.JPG)|*.jpg"; // сохранение файла в формате jpg
            if(saveFileDialog1.ShowDialog() == DialogResult.OK) 
            {
                if(paint.Image != null) // проверка на наличие изображения
                {
                    paint.Image.Save(saveFileDialog1.FileName); // сохранение файла
                }
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            pen.Width = trackBar1.Value;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            pen.Color = Color.White;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            graphics.Clear(Color.White);
        }

        
    }
}
