using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace Server
{
    internal class QRLocation
    {

        public static bool adding { get; set; } = false;
        public static bool selecting { get; set; } = false;
        public static bool drag { get; set; } = false;
        private static Bitmap QRMap = null;

        //drawing settings
        internal static int QRPointRadius = 6;

        //update list view of QR location
        public static void UpdateQRView(QRModel qrModel, ref ListView view, PictureBox pb)
        {
            view.BeginUpdate();
            view.Items.Clear();

            QRModel.QRModelXmlContent[] QRContent = null;

            qrModel.GetQRRecordList(ref QRContent);

            foreach (QRModel.QRModelXmlContent QRItem in QRContent)
            {
                ListViewItem lvItem = new ListViewItem(QRItem.QRID);
                lvItem.SubItems.Add(QRItem.QRName);
                lvItem.SubItems.Add(QRItem.X);
                lvItem.SubItems.Add(QRItem.Y);

                view.Items.Add(lvItem);
            }

            view.EndUpdate();
            QRLocation.PaintQRMap(pb);
        }

        //open config file
        public static void OpenQRConfig(string filename, ref ListView view, PictureBox pb)
        {
            if (Server.Run)
                return;

            //try to open file
            Server.qrModel = new QRModel(filename);

            //update view
            QRLocation.UpdateQRView(Server.qrModel, ref view, pb);
            QRLocation.PaintQRMap(pb);

            //update QR Images
            QRModel.QRModelXmlContent[] contents = null;
            Server.qrModel.GetQRRecordList(ref contents);
            foreach (var item in contents)
            {
                int Result = Server.qrModel.GenerateQR(item.QRID);
            }
        }

        //add QR to config file
        public static int AddQR(string QRID, string Name, string x, string y, ref ListView view, PictureBox pb)
        {
            if (Server.Run)
                //return "The server is running. File modification is not possible";
                return -1;

            //add QRRecord into a file
            int addResult = Server.qrModel.AddQRRecord(QRID, Name, x, y);

            if (addResult == 0)
            {
                QRLocation.UpdateQRView(Server.qrModel, ref view, pb);

                //generate QR file
                Server.qrModel.GenerateQR(QRID);
            }
            return addResult;
        }

        //edit QR in config file
        public static string EditQR(string oldQRID, string QRID, string QRName, string x, string y, ref ListView view, PictureBox pb)
        {
            if (Server.Run)
                return "The server is running. File modification is not possible";

            //edit QR in a file
            string OldQRFileName = "";
            int Result = Server.qrModel.GetQRImgName(oldQRID, ref OldQRFileName);

            int editResult = Server.qrModel.ChangeQRRecord(oldQRID, QRID, QRName, x, y);

            switch ((QRModel.ChangeQRRecordErrorCode)editResult)
            {
                case QRModel.ChangeQRRecordErrorCode.INCORRECT_PARAMETR:
                    return "Incorrect QR ID or QR X or QR Y";
                case QRModel.ChangeQRRecordErrorCode.CORRUPTED_FILE:
                    return "Xml file not exist or was corrupted";
                case QRModel.ChangeQRRecordErrorCode.QRID_INCORRECT:
                    return "QR ID incorrent or already exist";
                case QRModel.ChangeQRRecordErrorCode.NAME_IS_OCCUPIED:
                    return "QR Name already occupied";
                case QRModel.ChangeQRRecordErrorCode.NAME_NOT_FOUND:
                    return "QR Name is not exist";
                default:
                    {
                        //update view
                        QRLocation.UpdateQRView(Server.qrModel, ref view, pb);

                        //delete old QR file
                        if (Result >= 0)
                        {
                            File.Delete(OldQRFileName);
                        }

                        //generate new QR file
                        Server.qrModel.GenerateQR(QRID);                        

                        return "QR edited";
                    }
            }
        }

        //delete QR from config file
        public static string DeleteQR(string QRID, ref ListView view, PictureBox pb)
        {
            if (Server.Run)
                return "The server is running. File modification is not possible";

            string OldQRFileName = "";
            int Result = Server.qrModel.GetQRImgName(QRID, ref OldQRFileName);

            int delResult = Server.qrModel.DeleteQRRecord(QRID);

            switch ((QRModel.DeleteQRRecordErrorCode)delResult)
            {
                case QRModel.DeleteQRRecordErrorCode.CORRUPTED_FILE:
                    return "Xml file not exist or was corrupted";
                case QRModel.DeleteQRRecordErrorCode.QRID_INCORRECT:
                    return "QR ID not found";
                case QRModel.DeleteQRRecordErrorCode.NAME_NOT_FOUND:
                    return "Incorrect QR name of does not exist";
                default:
                    {
                        //update view
                        QRLocation.UpdateQRView(Server.qrModel, ref view, pb);

                        //delete QR file
                        if (Result >= 0)
                        {
                            File.Delete(OldQRFileName);
                        }

                        return "QR deleted";
                    }
            }
        }

        //paint QR Map
        public static void PaintQRMap(PictureBox pb)
        {
            //paint map
            if (MapInfo.bitmap == null)
                return;

            QRMap = (Bitmap)MapInfo.bitmap.Clone();
            pb.Image = QRMap;

            //paint dash rectangle
            Graphics g = Graphics.FromImage(pb.Image);
            g.DrawRectangle(SettingsModel.pen, new Rectangle(
                Math.Min(MapInfo.PointX1, MapInfo.PointX2),
                Math.Min(MapInfo.PointY1, MapInfo.PointY2),
                Math.Abs(MapInfo.PointX2 - MapInfo.PointX1),
                Math.Abs(MapInfo.PointY2 - MapInfo.PointY1)));

            //draw existing points
            DrawPoints(pb);
        }

        //draw QR point on QR map
        public static void DrawQRPoint(PictureBox pb, Color color, double xMeters, double yMeters)
        {
            //pb.Image = (Bitmap)QRMap.Clone();
            if (QRMap == null)
                return;

            Graphics g = Graphics.FromImage(pb.Image);
            g.FillEllipse(new SolidBrush(color), (float)((xMeters / MapInfo.SizeCoefficient) + MapInfo.PointX1 - QRPointRadius), (float)(yMeters / MapInfo.SizeCoefficient) + MapInfo.PointY1 - QRPointRadius, 2 * QRPointRadius, 2 * QRPointRadius);
            Pen pen = new Pen(Color.Black, 1);
            g.DrawEllipse(pen, (float)((xMeters / MapInfo.SizeCoefficient) + MapInfo.PointX1 - QRPointRadius), (float)(yMeters / MapInfo.SizeCoefficient) + MapInfo.PointY1 - QRPointRadius, 2 * QRPointRadius, 2 * QRPointRadius);
            QRMap = (Bitmap)pb.Image.Clone();
            pb.Image = QRMap;
        }

        //draw existing points
        public static void DrawPoints(PictureBox pb)
        {
            QRModel.QRModelXmlContent[] content = null;
            Server.qrModel.GetQRRecordList(ref content);
            if (content == null)
                return;

            foreach (var point in content)
            {
                double x = double.Parse(point.X, System.Globalization.CultureInfo.InvariantCulture);
                double y = double.Parse(point.Y, System.Globalization.CultureInfo.InvariantCulture);
                DrawQRPoint(pb, Color.Green, x, y);
            }
        }

        //hitting the point
        public static QRModel.QRModelXmlContent HitPoint(int X, int Y)
        {
            QRModel.QRModelXmlContent[] content = null;
            Server.qrModel.GetQRRecordList(ref content);
            if (content != null)
            {
                foreach (var point in content)
                {
                    //meters 
                    double xCircle = double.Parse(point.X, System.Globalization.CultureInfo.InvariantCulture);
                    double yCircle = double.Parse(point.Y, System.Globalization.CultureInfo.InvariantCulture);

                    if (Math.Pow(((X - MapInfo.PointX1) * MapInfo.SizeCoefficient - xCircle), 2) + Math.Pow(((Y - MapInfo.PointY1) * MapInfo.SizeCoefficient - yCircle), 2) <= Math.Pow(QRPointRadius * MapInfo.SizeCoefficient, 2))
                    {
                        return point;
                    }
                }
            }
            QRModel.QRModelXmlContent ret = new QRModel.QRModelXmlContent();
            return ret;
        }

        //select point on QR map
        public static void SelectPoint(QRModel.QRModelXmlContent point, ToolTip ttQR, PictureBox pbMap, PictureBox pbQR)
        {
            //show tip, select point
            QRLocation.PaintQRMap(pbMap);
            string content = $"QR ID: {point.QRID}\nQR Name: {point.QRName}\nX: {point.X}\nY: {point.Y}";
            ttQR.SetToolTip(pbMap, content);
            double x = double.Parse(point.X, System.Globalization.CultureInfo.InvariantCulture);
            double y = double.Parse(point.Y, System.Globalization.CultureInfo.InvariantCulture);
            QRLocation.DrawQRPoint(pbMap, Color.Orange, x, y);
            QRLocation.selecting = true;
            ShowQRImg(point.QRID, pbQR);
        }

        //show selected QR
        public static void ShowQRImg(string QRID, PictureBox pbQR)
        {
            string QRFileName = "";
            int Result = Server.qrModel.GetQRImgName(QRID, ref QRFileName);
            switch ((QRModel.GetQRImgNameErrorCode)Result)
            {
                case QRModel.GetQRImgNameErrorCode.CORRUPTED_FILE:
                case QRModel.GetQRImgNameErrorCode.QRID_INCORRECT:
                case QRModel.GetQRImgNameErrorCode.NAME_NOT_FOUND:
                case QRModel.GetQRImgNameErrorCode.FILE_NOT_FOUND:
                    pbQR.Image = null;
                    return;
                default:
                    {
                        using (FileStream fs = File.OpenRead(QRFileName))
                        {
                            Bitmap QRMap = (Bitmap)new Bitmap(fs).Clone();
                            fs.Close();
                            pbQR.Image = QRMap;
                        }
                        return;
                    }
            }
        }
    }
}
