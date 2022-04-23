using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    internal class QRLocation
    {

        //update list view of QR location
        public static void UpdateQRView(ref ListView view, QRModel qrModel)
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
        }

        //open config file
        public static void OpenQRConfig(string filename, ref ListView view)
        {
            if (Server.Run)
                return;

            //try to open file
            Server.qrModel = new QRModel(filename);

            //update view
            QRLocation.UpdateQRView(ref view, Server.qrModel);
        }

        //add QR to config file
        public static string AddQR(string QRID, string Name, string x, string y, ref ListView view)
        {
            if (Server.Run)
                return "The server is running. File modification is not possible";

            //add QRRecord into a file
            int addResult = Server.qrModel.AddQRRecord(QRID, Name, x, y);

            switch ((QRModel.AddQRRecordErrorCode)addResult)
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
            }
        }

        //edit QR in config file
        public static string EditQR(string oldQRID, string QRID, string QRName, string x, string y, ref ListView view)
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
                        QRLocation.UpdateQRView(ref view, Server.qrModel);
                        return "QR edited";
                    }
            }
        }

        //delete QR from config file
        public static string DeleteQR(string QRID, ref ListView view)
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
                        QRLocation.UpdateQRView(ref view, Server.qrModel);
                        return "QR deleted";
                    }
            }
        }
    }
}
