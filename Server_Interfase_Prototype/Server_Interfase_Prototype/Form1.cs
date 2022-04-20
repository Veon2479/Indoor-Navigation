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

namespace Server_Interfase_Prototype
{
    public partial class FMapSettings : Form
    {
        // координаты в пикселях
        private int X1 = 0;
        private int X2 = 0;
        private int Y1 = 0;
        private int Y2 = 0;

        private double MapRealWidth = 0;

        //static Bitmap bitmap;
        private string MapImageFilename = "";

        static readonly Color MapBorderColor = Color.Black;

        // half of dot size
        private const int DOT_SIZE = 5;
        public static double sizeCoeff = 1;

        public FMapSettings()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pbMapImage.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void TbCoordinate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(8))
            {
                e.Handled = true;
            }
        }

        private void bDownloadMap_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            MapImageFilename = openFileDialog1.FileName;
            if (MapImageFilename != "")
            {
                //bitmap = new Bitmap(MapImageFilename);
                RedrawDots();
            }
        }

        private void RedrawDots()
        {
            pbMapImage.Image = new Bitmap(MapImageFilename);
            Graphics g = Graphics.FromImage(pbMapImage.Image);
            g.FillRectangle(new SolidBrush(MapBorderColor),
                X1 - DOT_SIZE, Y1 - DOT_SIZE, 2 * DOT_SIZE, 2 * DOT_SIZE);

            g.FillRectangle(new SolidBrush(MapBorderColor),
                X2 - DOT_SIZE, Y2 - DOT_SIZE, 2 * DOT_SIZE, 2 * DOT_SIZE);

            Pen pen = new Pen(MapBorderColor);
            pen.DashStyle = DashStyle.Dash;
            g.DrawRectangle(pen, new Rectangle(
                Math.Min(X1, X2), Math.Min(Y1, Y2),
                Math.Abs(X2 - X1), Math.Abs(Y2 - Y1)));
        }

        private void tbX1_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(tbX1.Text, out X1))
            {
                RedrawDots();
            }
        }
        private void tbY1_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(tbY1.Text, out Y1))
            {
                RedrawDots();
            }
        }
        private void tbX2_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(tbX2.Text, out X2))
            {
                RedrawDots();
            }
        }
        private void tbY2_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(tbY2.Text, out Y2))
            {
                RedrawDots();
            }
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            // пока примем, что картинка пропорциональна реальности и возьмем только ширину 
            if (double.TryParse(tbRealWidth.Text, out MapRealWidth))
            {
                // пикселей на метр
                try
                {
                    sizeCoeff = Math.Abs(X2 - X1) / MapRealWidth;
                }
                catch (DivideByZeroException)
                {
                    //
                }
            }
            else
            {
                //
            }
        }

        private void tbRealWidth_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) 
                && !char.IsDigit(e.KeyChar) 
                && e.KeyChar != Convert.ToChar(8)
                && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }
    }
}
