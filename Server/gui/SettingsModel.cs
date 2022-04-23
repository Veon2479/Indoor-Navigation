using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace Server
{
    public static class SettingsModel
    {
        const int POINT1 = 1;
        const int POINT2 = 2;
        public const int NO_POINT = 0;

        public const int USER_SETTINGS_SAVED_SUCCESSFULLY = 4;
        public const int IMAGE_DOWNLOAD_ERROR = -1;
        public const int DROW_FRAME_POINTS_ERROR = -2;
        public const int DOWNLOAD_USER_SETTINGS_ERROR = -3;
        public const int SAVE_USER_SETTINGS_ERROR = -4;

        const double POINT1_START_LOCATION = 0.1;
        const double POINT2_START_LOCATION = 0.9;
        static int PointSize = 5;

        static int SelectedPoint;
        static bool isDownloadImage;

        // the ratio of the size in meters to the size in pixels
        public static double SizeCoefficient;
        static int WidthBorder;
        static int HeightBorder;

        static string SettingsFilename = "ServerSettings.xml";

        static string MapImageLocalDir = "MapImages";
        static string MapImageLocalFilename = "MapImage1";
        static string MapImageLocalPath;

        static Bitmap bitmap;
        static readonly Color MapFrameColor = Color.Black;
        static readonly SolidBrush brush = new SolidBrush(MapFrameColor);
        static readonly Pen pen = new Pen(MapFrameColor, 2)
        {
            DashStyle = DashStyle.Dash
        };

        public static int PointX1;
        public static int PointY1;
        public static int PointX2;
        public static int PointY2;

        public static double RealLength;
        public static double RealWidth;

        static SettingsModel()
        {
            SizeCoefficient = 1;
            SelectedPoint = NO_POINT;
            isDownloadImage = false;
            MapImageLocalPath = $"{MapImageLocalDir}/{MapImageLocalFilename}";
        }

        public static void InitDrowSettings(PictureBox pictureBox)
        {
            PointX1 = Convert.ToInt32(bitmap.Width * POINT1_START_LOCATION);
            PointY1 = Convert.ToInt32(bitmap.Height * POINT1_START_LOCATION);
            PointX2 = Convert.ToInt32(bitmap.Width * POINT2_START_LOCATION);
            PointY2 = Convert.ToInt32(bitmap.Height * POINT2_START_LOCATION);
            WidthBorder = (pictureBox.Width - bitmap.Width) / 2;
            HeightBorder = (pictureBox.Height - bitmap.Height) / 2;
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

        public static int DownloadImage(PictureBox pictureBox, string filename)
        {
            try
            {
                int width;
                int height;
                using (FileStream fs = File.OpenRead(filename))
                {
                    var b = new Bitmap(fs).Clone();
                    bitmap = (Bitmap)b;
                    fs.Close();
                }
                if (bitmap.Width / Convert.ToDouble(bitmap.Height)
                    > pictureBox.Width / Convert.ToDouble(pictureBox.Height))
                {
                    width = pictureBox.Width;
                    height = Convert.ToInt32(Convert.ToDouble(bitmap.Height) / bitmap.Width * pictureBox.Width);
                }
                else
                {
                    height = pictureBox.Height;
                    width = Convert.ToInt32(Convert.ToDouble(bitmap.Width) / bitmap.Height * pictureBox.Height);
                }
                bitmap = new Bitmap(bitmap, new Size(width, height));
                isDownloadImage = true;
                InitDrowSettings(pictureBox);
                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return IMAGE_DOWNLOAD_ERROR;
            }
        }

        public static int FramePointsView(PictureBox pictureBox)
        {
            if (isDownloadImage)
            {
                try
                {
                    Bitmap b = (Bitmap)bitmap.Clone();
                    pictureBox.Image = b;
                }
                catch (Exception)
                {
                    return IMAGE_DOWNLOAD_ERROR;
                }
                try
                {
                    Graphics g = Graphics.FromImage(pictureBox.Image);
                    g.FillRectangle(brush,
                        PointX1 - PointSize,
                        PointY1 - PointSize,
                        2 * PointSize,
                        2 * PointSize);

                    g.FillRectangle(brush,
                        PointX2 - PointSize,
                        PointY2 - PointSize,
                        2 * PointSize,
                        2 * PointSize);

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
            }
            return 0;
        }

        public static void SelectFramePoint(int pointX, int pointY)
        {
            if (isDownloadImage)
            {
                if (SelectedPoint == NO_POINT)
                {
                    pointX -= WidthBorder;
                    pointY -= HeightBorder;
                    if (pointX < PointX1 + PointSize
                        && pointX > PointX1 - PointSize
                        && pointY < PointY1 + PointSize
                        && pointY > PointY1 - PointSize)
                    {
                        SelectedPoint = POINT1;
                    }
                    else if (pointX < PointX2 + PointSize
                        && pointX > PointX2 - PointSize
                        && pointY < PointY2 + PointSize
                        && pointY > PointY2 - PointSize)
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
                pointX -= WidthBorder;
                pointY -= HeightBorder;
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

        public static int SaveSettings()
        {
            try
            {
                if (!Directory.Exists(MapImageLocalDir))
                {
                    Directory.CreateDirectory(MapImageLocalDir);
                }
                if (File.Exists(MapImageLocalPath))
                {
                    File.Delete(MapImageLocalPath);
                }
                bitmap.Save(MapImageLocalPath);
                XDocument xdoc = new XDocument();
                XElement settings = new XElement("settings");

                settings.Add(new XElement("PointX1", PointX1));
                settings.Add(new XElement("PointX2", PointX2));
                settings.Add(new XElement("PointY1", PointY1));
                settings.Add(new XElement("PointY2", PointY2));
                settings.Add(new XElement("SizeCoefficient", SizeCoefficient.ToString("N12")));
                xdoc.Add(settings);
                xdoc.Save(SettingsFilename);
                return USER_SETTINGS_SAVED_SUCCESSFULLY;
            }
            catch (Exception)
            {
                return SAVE_USER_SETTINGS_ERROR;
            }
        }

        public static int DownloadSettings(PictureBox pictureBox)
        {
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                if (DownloadImage(pictureBox, MapImageLocalPath) != IMAGE_DOWNLOAD_ERROR)
                {
                    xmlDoc.Load(SettingsFilename);
                    XmlElement xmlEl = xmlDoc.DocumentElement;
                    foreach (XmlElement xel in xmlEl.ChildNodes)
                    {
                        if (xel.Name == "PointX1"
                            && !int.TryParse(xel.InnerText, out PointX1))
                        {
                            return DOWNLOAD_USER_SETTINGS_ERROR;
                        }
                        else if (xel.Name == "PointX2"
                            && !int.TryParse(xel.InnerText, out PointX2))
                        {
                            return DOWNLOAD_USER_SETTINGS_ERROR;
                        }
                        else if (xel.Name == "PointY1"
                            && !int.TryParse(xel.InnerText, out PointY1))
                        {
                            return DOWNLOAD_USER_SETTINGS_ERROR;
                        }
                        else if (xel.Name == "PointY2"
                            && !int.TryParse(xel.InnerText, out PointY2))
                        {
                            return DOWNLOAD_USER_SETTINGS_ERROR;
                        }
                        else if (xel.Name == "SizeCoefficient"
                            && (!double.TryParse(xel.InnerText, out SizeCoefficient)))
                        {
                            return DOWNLOAD_USER_SETTINGS_ERROR;
                        }
                    }
                    SetRealLength(SizeCoefficient * Math.Abs(PointX1 - PointX2));
                    return 0;
                }
                else
                {
                    return IMAGE_DOWNLOAD_ERROR;
                }
            }
            catch (Exception)
            {
                return DOWNLOAD_USER_SETTINGS_ERROR;
            }
        }
    }
}