using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Server
{
    internal class QRLocation
    {

        public static bool adding { get; set; } = false;
        private static Bitmap QRMap = null;

        //drawing settings
        private static int QRPointRadius = 5;

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
            }

            /*switch ((QRModel.AddQRRecordErrorCode)addResult)
            {
                case QRModel.AddQRRecordErrorCode.INCORRECT_PARAMETER:
                    return "Incorrect QR ID or QR X or QR Y";
                case QRModel.AddQRRecordErrorCode.CORRUPTED_FILE:
                    return "Xml file not exists or was corrupted";
                case QRModel.AddQRRecordErrorCode.QRID_INCORRECT:
                    return "QR ID incorrect (QRID < 0) or alredy exist";
                case QRModel.AddQRRecordErrorCode.NAME_OCCUPIED:
                    return " Name of QR record already occupied";
                default:
                    {
                        //update view
                        QRLocation.UpdateQRView(ref view, Server.qrModel);
                        return "QR added";
                    }
            }*/
            return addResult;
        }

        //edit QR in config file
        public static string EditQR(string oldQRID, string QRID, string QRName, string x, string y, ref ListView view, PictureBox pb)
        {
            if (Server.Run)
                return "The server is running. File modification is not possible";

            //edit QR in a file
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
                        return "QR edited";
                    }
            }
        }

        //delete QR from config file
        public static string DeleteQR(string QRID, ref ListView view, PictureBox pb)
        {
            if (Server.Run)
                return "The server is running. File modification is not possible";

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
                        return "QR deleted";
                    }
            }
        }

        //paint QR Map
        public static void PaintQRMap(PictureBox pb)
        {
            //paint map
            if (SettingsModel.bitmap == null)
                return;

            QRMap = (Bitmap)SettingsModel.bitmap.Clone();
            pb.Image = QRMap;

            //paint dash rectangle
            Graphics g = Graphics.FromImage(pb.Image);
            g.DrawRectangle(SettingsModel.pen, new Rectangle(
                Math.Min(SettingsModel.PointX1, SettingsModel.PointX2),
                Math.Min(SettingsModel.PointY1, SettingsModel.PointY2),
                Math.Abs(SettingsModel.PointX2 - SettingsModel.PointX1),
                Math.Abs(SettingsModel.PointY2 - SettingsModel.PointY1)));

            //draw existing points
            DrawPoints(pb);
        }

        //draw QR point on QR map
        public static void DrawQRPoint(PictureBox pb, Color color, double x, double y)
        {
            //pb.Image = (Bitmap)QRMap.Clone();
            Graphics g = Graphics.FromImage(pb.Image);
            g.FillEllipse(new SolidBrush(color), (float)x - QRPointRadius, (float)y - QRPointRadius, 2 * QRPointRadius, 2 * QRPointRadius);
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
    }
}
