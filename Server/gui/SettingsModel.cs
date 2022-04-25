using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace Server
{
    public static class SettingsModel
    {
        const int POINT1 = 1;
        const int POINT2 = 2;
        public const int NO_POINT = 0;

        // message and error codes
        public struct MESSAGE
        {
            public const int IMAGE_DOWNLOAD_ERROR = -1;
            public const int DROW_FRAME_POINTS_ERROR = -2;
            public const int DOWNLOAD_USER_SETTINGS_ERROR = -3;
            public const int SAVE_USER_SETTINGS_ERROR = -4;

            public const int USER_SETTINGS_SAVED_SUCCESSFULLY = 1;
            public const int DOWNLOAD_USER_SETTINGS = 2;
            public const int DOWNLOAD_DEFOULT_SETTINGS = 3;
        }

        const double POINT1_START_LOCATION = 0.1;
        const double POINT2_START_LOCATION = 0.9;

        // size of POINT1 and POINT2
        const int POINT_SIZE = 5;

        static int SelectedPoint = NO_POINT;

        static int WidthBorder;
        static int HeightBorder;
        static readonly Color MapFrameColor = Color.Black;
        static readonly SolidBrush brush = new SolidBrush(MapFrameColor);
        internal static readonly Pen pen = new Pen(MapFrameColor, 2)
        {
            DashStyle = DashStyle.Dash
        };

        /// <summary>
        /// Initialise settings for drowing frame with points
        /// </summary>
        /// <param name="pictureBox">PictureBox for drowing</param>
        static void InitDrowSettings(PictureBox pictureBox)
        {
            WidthBorder = (pictureBox.Width - pictureBox.Image.Width) / 2;
            HeightBorder = (pictureBox.Height - pictureBox.Image.Height) / 2;
        }

        static void SetDefaultPoints(PictureBox pictureBox)
        {
            MapInfo.SetX1(Convert.ToInt32(pictureBox.Image.Width * POINT1_START_LOCATION));
            MapInfo.SetY1(Convert.ToInt32(pictureBox.Image.Height * POINT1_START_LOCATION));
            MapInfo.SetX2(Convert.ToInt32(pictureBox.Image.Width * POINT2_START_LOCATION));
            MapInfo.SetY2(Convert.ToInt32(pictureBox.Image.Height * POINT2_START_LOCATION));
        }

        /// <summary>
        /// Download image from file to PictureBox
        /// </summary>
        /// <param name="pictureBox">PictureBox on witch the picture is loaded</param>
        /// <param name="filename">Name of file, from picture is loaded</param>
        /// <returns>Error code</returns>
        public static int DownloadMap(PictureBox pictureBox, string filename)
        {
            try
            {
                int width;
                int height;
                int returnMsg;
                // download map from image file
                if (TryGetMap(filename) == MESSAGE.DOWNLOAD_USER_SETTINGS)
                {
                    returnMsg = MESSAGE.DOWNLOAD_USER_SETTINGS;
                    pictureBox.Image = MapInfo.bitmap;
                }
                else
                {
                    using (FileStream fs = File.OpenRead(filename))
                    {
                        MapInfo.bitmap = (Bitmap)new Bitmap(fs).Clone();
                        fs.Close();
                    }
                    if (MapInfo.bitmap.Width / Convert.ToDouble(MapInfo.bitmap.Height)
                        > pictureBox.Width / Convert.ToDouble(pictureBox.Height))
                    {
                        width = pictureBox.Width;
                        height = Convert.ToInt32(Convert.ToDouble(MapInfo.bitmap.Height)
                            / MapInfo.bitmap.Width * pictureBox.Width);
                    }
                    else
                    {
                        height = pictureBox.Height;
                        width = Convert.ToInt32(Convert.ToDouble(MapInfo.bitmap.Width)
                            / MapInfo.bitmap.Height * pictureBox.Height);
                    }
                    MapInfo.bitmap = new Bitmap(MapInfo.bitmap, new Size(width, height));
                    MapInfo.Azimuth = 0;
                    MapInfo.RealLength = Math.Abs(MapInfo.PointX1 - MapInfo.PointX2);
                    pictureBox.Image = MapInfo.bitmap;
                    SetDefaultPoints(pictureBox);
                    MapInfo.SetDefoult();
                    returnMsg = MESSAGE.DOWNLOAD_DEFOULT_SETTINGS;
                }
                InitDrowSettings(pictureBox);
                MapInfo.isDownloadMap = true;
                FramePointsView(pictureBox);
                return returnMsg;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return MESSAGE.DOWNLOAD_USER_SETTINGS_ERROR;
            }
        }

        /// <summary>
        /// Trying get map from file
        /// </summary>
        /// <param name="filename"></param>
        /// <returns>Error code</returns>
        private static int TryGetMap(string filename)
        {
            try
            {
                using (FileStream fs = File.OpenRead(filename))
                {
                    int bitmapSize = 0;
                    byte[] arr = new byte[sizeof(double)];

                    fs.Read(arr, 0, sizeof(int));
                    MapInfo.PointX1 = BitConverter.ToInt32(arr, 0);

                    fs.Read(arr, 0, sizeof(int));
                    MapInfo.PointX2 = BitConverter.ToInt32(arr, 0);

                    fs.Read(arr, 0, sizeof(int));
                    MapInfo.PointY1 = BitConverter.ToInt32(arr, 0);

                    fs.Read(arr, 0, sizeof(int));
                    MapInfo.PointY2 = BitConverter.ToInt32(arr, 0);

                    fs.Read(arr, 0, sizeof(double));
                    MapInfo.SizeCoefficient = BitConverter.ToDouble(arr, 0);

                    fs.Read(arr, 0, sizeof(double));
                    MapInfo.Azimuth = BitConverter.ToDouble(arr, 0);

                    MapInfo.SetRealLength(MapInfo.SizeCoefficient * Math.Abs(MapInfo.PointX1 - MapInfo.PointX2));

                    fs.Read(arr, 0, sizeof(int));
                    bitmapSize = BitConverter.ToInt32(arr, 0);

                    arr = new byte[bitmapSize];
                    fs.Read(arr, 0, bitmapSize);

                    MemoryStream ms = new MemoryStream(arr);

                    Bitmap b = (Bitmap)Image.FromStream(ms);
                    MapInfo.bitmap = (Bitmap)b.Clone();
                }
                return MESSAGE.DOWNLOAD_USER_SETTINGS;
            }
            catch (Exception)
            {
                return MESSAGE.DOWNLOAD_USER_SETTINGS_ERROR;
            }
        }

        /// <summary>
        /// Drow frame with points on PaintBox
        /// </summary>
        /// <param name="pictureBox">PaintBox on whith the picture is drown</param>
        /// <returns>Error code</returns>
        public static int FramePointsView(PictureBox pictureBox)
        {
            if (MapInfo.isDownloadMap)
            {
                try
                {
                    // reset PictureBox.Image with new Bitmap
                    Bitmap b = (Bitmap)MapInfo.bitmap.Clone();
                    pictureBox.Image = b;
                }
                catch (Exception)
                {
                    return MESSAGE.IMAGE_DOWNLOAD_ERROR;
                }
                try
                {
                    Graphics g = Graphics.FromImage(pictureBox.Image);
                    // drow point 1
                    g.FillRectangle(brush,
                        MapInfo.PointX1 - POINT_SIZE,
                        MapInfo.PointY1 - POINT_SIZE,
                        2 * POINT_SIZE,
                        2 * POINT_SIZE);
                    // drow point 2
                    g.FillRectangle(brush,
                        MapInfo.PointX2 - POINT_SIZE,
                        MapInfo.PointY2 - POINT_SIZE,
                        2 * POINT_SIZE,
                        2 * POINT_SIZE);
                    // drow frame
                    g.DrawRectangle(pen, new Rectangle(
                        Math.Min(MapInfo.PointX1, MapInfo.PointX2),
                        Math.Min(MapInfo.PointY1, MapInfo.PointY2),
                        Math.Abs(MapInfo.PointX2 - MapInfo.PointX1),
                        Math.Abs(MapInfo.PointY2 - MapInfo.PointY1)));
                    MapInfo.isMapChanged = true;
                }
                catch (Exception)
                {
                    return MESSAGE.DROW_FRAME_POINTS_ERROR;
                }
            }
            return 0;
        }

        internal static void RedrowAzimuth()
        {
            //
        }

        /// <summary>
        /// Changing real length and changing real width according to new value of real length
        /// </summary>
        /// <param name="realLengthText"></param>
        /// <returns></returns>
        public static string RealLengthChanged(string realLengthText)
        {
            if (double.TryParse(realLengthText, out double realLength))
            {
                MapInfo.SetRealLength(realLength);
            }
            return MapInfo.RealWidth.ToString("N3");
        }

        /// <summary>
        /// Stores the selected point at the given coordinates
        /// </summary>
        /// <param name="pointX">Coordinate x on control</param>
        /// <param name="pointY">Coordinate y on control</param>
        public static void SelectFramePoint(int pointX, int pointY)
        {
            if (MapInfo.isDownloadMap)
            {
                // save selected point
                if (SelectedPoint == NO_POINT)
                {
                    pointX -= WidthBorder;
                    pointY -= HeightBorder;
                    if (pointX < MapInfo.PointX1 + POINT_SIZE
                        && pointX > MapInfo.PointX1 - POINT_SIZE
                        && pointY < MapInfo.PointY1 + POINT_SIZE
                        && pointY > MapInfo.PointY1 - POINT_SIZE)
                    {
                        SelectedPoint = POINT1;
                    }
                    else if (pointX < MapInfo.PointX2 + POINT_SIZE
                        && pointX > MapInfo.PointX2 - POINT_SIZE
                        && pointY < MapInfo.PointY2 + POINT_SIZE
                        && pointY > MapInfo.PointY2 - POINT_SIZE)
                    {
                        SelectedPoint = POINT2;
                    }
                }
                // reset selected point
                else
                {
                    SelectedPoint = NO_POINT;
                }
            }
        }

        /// <summary>
        /// Moves the selected point at the given coordinates 
        /// and redraw PaintBox with new frame
        /// </summary>
        /// <param name="pictureBox"></param>
        /// <param name="pointX"></param>
        /// <param name="pointY"></param>
        /// <returns></returns>
        public static int MoveFramePoint(PictureBox pictureBox, int pointX, int pointY)
        {
            if (MapInfo.isDownloadMap)
            {
                pointX -= WidthBorder;
                pointY -= HeightBorder;
                if (SelectedPoint == POINT1)
                {
                    MapInfo.SetX1(pointX);
                    MapInfo.SetY1(pointY);
                    FramePointsView(pictureBox);
                }
                else if (SelectedPoint == POINT2)
                {
                    MapInfo.SetX2(pointX);
                    MapInfo.SetY2(pointY);
                    FramePointsView(pictureBox);
                }
            }
            return SelectedPoint;
        }

        /// <summary>
        /// Saving settings to a file
        /// </summary>
        /// <returns>Error code</returns>
        public static int SaveSettings()
        {
            try
            {
                List<byte> MapInBytes = new List<byte>(sizeof(int) * 4 + sizeof(double));

                int point1 = MapInfo.PointX1;
                int point2 = MapInfo.PointX2;
                MapInfo.PointX1 = Math.Min(point1, point2);
                MapInfo.PointX2 = Math.Max(point1, point2);
                point1 = MapInfo.PointY1;
                point2 = MapInfo.PointY2;
                MapInfo.PointY1 = Math.Min(point1, point2);
                MapInfo.PointY2 = Math.Max(point1, point2);

                MapInBytes.AddRange(BitConverter.GetBytes(MapInfo.PointX1));
                MapInBytes.AddRange(BitConverter.GetBytes(MapInfo.PointX2));
                MapInBytes.AddRange(BitConverter.GetBytes(MapInfo.PointY1));
                MapInBytes.AddRange(BitConverter.GetBytes(MapInfo.PointY2));
                MapInBytes.AddRange(BitConverter.GetBytes(MapInfo.SizeCoefficient));
                MapInBytes.AddRange(BitConverter.GetBytes(MapInfo.Azimuth));
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Indoor-navigation map files (*.inm)|*.inm";
                    saveFileDialog.AddExtension = true;
                    saveFileDialog.DefaultExt = "inm";
                    if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                    {
                        return 0;
                    }
                    using (FileStream fs = File.OpenWrite(saveFileDialog.FileName))
                    {
                        fs.Write(MapInBytes.ToArray(), 0, MapInBytes.Count);
                        using (MemoryStream ms = new MemoryStream())
                        {
                            MapInfo.bitmap.Save(ms, ImageFormat.Bmp);
                            byte[] bitmapInBytes = new byte[ms.Length];
                            byte[] bitmapSize = BitConverter.GetBytes((int)ms.Length);
                            ms.Position = 0;
                            ms.Read(bitmapInBytes, 0, (int)ms.Length);
                            fs.Write(bitmapSize, 0, sizeof(int));
                            fs.Write(bitmapInBytes, 0, (int)ms.Length);
                            ms.Close();
                        }
                        fs.Close();
                    }
                };
                MapInfo.isMapChanged = false;
                return MESSAGE.USER_SETTINGS_SAVED_SUCCESSFULLY;
            }
            catch (Exception)
            {
                return MESSAGE.SAVE_USER_SETTINGS_ERROR;
            }
        }

        public static int CoordTextChanged(string text, int point)
        {
            if (int.TryParse(text, out int pointCoord))
            {
                return pointCoord;
            }
            return point;
        }

        public static void CheckSaving(TabControl tabControl)
        {
            if (tabControl.SelectedIndex != 0)
            {
                if (MapInfo.isMapChanged)
                {
                    DialogResult dialogResult =
                        MessageBox.Show("Please, save new settings", "Warning!", MessageBoxButtons.OKCancel);
                    if (dialogResult == DialogResult.OK)
                    {
                        if (MessageView(SaveSettings())
                            != MESSAGE.USER_SETTINGS_SAVED_SUCCESSFULLY)
                        {
                            tabControl.SelectedIndex = 0;
                        }
                    }
                    else
                    {
                        tabControl.SelectedIndex = 0;
                    }
                }
            }
        }

        //  display message according to message or error code
        public static int MessageView(int errorCode)
        {
            switch (errorCode)
            {
                case MESSAGE.IMAGE_DOWNLOAD_ERROR:
                    {
                        MessageBox.Show("Image loading error", "Error", MessageBoxButtons.OK);
                    }
                    break;
                case MESSAGE.DROW_FRAME_POINTS_ERROR:
                    {
                        MessageBox.Show("Error drawing additional elements", "Error", MessageBoxButtons.OK);
                    }
                    break;
                case MESSAGE.DOWNLOAD_USER_SETTINGS_ERROR:
                    {
                        MessageBox.Show("Error loading user settings", "Error", MessageBoxButtons.OK);
                    }
                    break;
                case MESSAGE.USER_SETTINGS_SAVED_SUCCESSFULLY:
                    {
                        MessageBox.Show("Settings saved successfully", "Message", MessageBoxButtons.OK);
                    }
                    break;
                case MESSAGE.SAVE_USER_SETTINGS_ERROR:
                    {
                        MessageBox.Show("Error saving user settings", "Error", MessageBoxButtons.OK);
                    }
                    break;
            }
            return errorCode;
        }

    }
}