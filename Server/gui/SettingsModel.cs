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

namespace Server
{
    public static class SettingsModel
    {
        const int POINT_SIZE = 5;

        public const int NO_POINT = 0;
        const int POINT1 = 1;
        const int POINT2 = 2;

        public const int IMAGE_DOWNLOAD_ERROR = -1;
        public const int DROW_FRAME_POINTS_ERROR = -2;
        public const int IMAGE_SAVE_ERROR = -3;

        static int SelectedPoint;
        static bool isDownloadImage;

        // the ratio of the size in meters to the size in pixels
        public static double SizeCoefficient;

        public static string MapImageFilename;

        static Color MapFrameColor = Color.Black;
        static SolidBrush brush = new SolidBrush(MapFrameColor);
        static Pen pen = new Pen(MapFrameColor)
        {
            DashStyle = DashStyle.Dash
        };

        public static int PointX1;
        public static int PointY1;
        public static int PointX2;
        public static int PointY2;

        public static double RealLength;
        public static double RealWidth;

        public static void InitSettings()
        {
            PointX1 = 20;
            PointY1 = 20;
            PointX2 = 200;
            PointY2 = 200;
            SizeCoefficient = 1;
            SelectedPoint = NO_POINT;
            isDownloadImage = false;
            MapImageFilename = "";
        }
        public static void SetRealLength(double value)
        {
            RealLength = value;
            SizeCoefficient = RealLength / Math.Abs(PointX2 - PointX1);
            RealWidth = SizeCoefficient * Math.Abs(PointY2 - PointY1);
        }
        public static void SetRealWidth(double value)
        {
            RealWidth = value;
            SizeCoefficient = RealWidth / Math.Abs(PointY2 - PointY1);
            RealLength = SizeCoefficient * Math.Abs(PointX2 - PointX1);
        }

        public static int SaveImage(string filename)
        {
            try
            {
                MapImageFilename = filename;
                // + сохранение локально
                return 0;
            }
            catch (Exception)
            {
                return IMAGE_SAVE_ERROR;
            }
        }

        public static int FramePointsView(PictureBox pictureBox)
        {
            int POINT_SIZE = SettingsModel.POINT_SIZE;
            try
            {
                pictureBox.Image = new Bitmap(MapImageFilename);
                isDownloadImage = true;
            }
            catch (Exception)
            {
                return IMAGE_DOWNLOAD_ERROR;
            }
            try
            {
                Graphics g = Graphics.FromImage(pictureBox.Image);
                g.FillRectangle(brush,
                    PointX1 - POINT_SIZE,
                    PointY1 - POINT_SIZE,
                    2 * POINT_SIZE,
                    2 * POINT_SIZE);

                g.FillRectangle(brush,
                    PointX2 - POINT_SIZE,
                    PointY2 - POINT_SIZE,
                    2 * POINT_SIZE,
                    2 * POINT_SIZE);

                g.DrawRectangle(pen, new Rectangle(
                    Math.Min(PointX1, PointX2),
                    Math.Min(PointY1, PointY2),
                    Math.Abs(PointX2 - PointX1),
                    Math.Abs(PointY2 - PointY1)));
            }
            catch (Exception)
            {
                return DROW_FRAME_POINTS_ERROR;
            }
            return 0;
        }

        public static void SelectFramePoint(PictureBox pictureBox, int pointX, int pointY)
        {
            if (isDownloadImage)
            {
                if (SelectedPoint == NO_POINT)
                {
                    // magic number
                    pointX -= (pictureBox.Width - pictureBox.Image.Width - 7) / 2;
                    pointY -= (pictureBox.Height - pictureBox.Image.Height - 7) / 2;
                    if (pointX < PointX1 + POINT_SIZE
                        && pointX > PointX1 - POINT_SIZE
                        && pointY < PointY1 + POINT_SIZE
                        && pointY > PointY1 - POINT_SIZE)
                    {
                        SelectedPoint = POINT1;
                    }
                    else if (pointX < PointX2 + POINT_SIZE
                        && pointX > PointX2 - POINT_SIZE
                        && pointY < PointY2 + POINT_SIZE
                        && pointY > PointY2 - POINT_SIZE)
                    {
                        SelectedPoint = POINT2;
                    }
                }
                else
                {
                    SelectedPoint = NO_POINT;
                }
            }
        }

        public static int MoveFramePoint(PictureBox pictureBox, int pointX, int pointY)
        {
            if (isDownloadImage)
            {
                pointX -= (pictureBox.Width - pictureBox.Image.Width) / 2;
                pointY -= (pictureBox.Height - pictureBox.Image.Height) / 2;
                if (SelectedPoint == POINT1)
                {
                    PointX1 = pointX;
                    PointY1 = pointY;
                    RealLength = SizeCoefficient * Math.Abs(PointX2 - PointX1);
                    RealWidth = SizeCoefficient * Math.Abs(PointY2 - PointY1);
                    FramePointsView(pictureBox);
                }
                else if (SelectedPoint == POINT2)
                {
                    PointX2 = pointX;
                    PointY2 = pointY;
                    RealLength = SizeCoefficient * Math.Abs(PointX2 - PointX1);
                    RealWidth = SizeCoefficient * Math.Abs(PointY2 - PointY1);
                    FramePointsView(pictureBox);
                }
            }
            return SelectedPoint;
        }
    }
}
