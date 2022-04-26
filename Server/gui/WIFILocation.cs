using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Server
{
    internal class WIFILocation
    {
        public static bool adding { get; set; } = false;
        public static bool selecting { get; set; } = false;
        private static Bitmap WIFIMap = null;

        internal static ListView.SelectedListViewItemCollection selectedItem = null;
        //internal static QRModel.QRModelXmlContent selectedPoint = new QRModel.QRModelXmlContent();

        internal static int WIFIRadius = 6;

        //update list view of QR location
        public static void UpdateWIFIView(WIFISpotModel spotModel, ref ListView view, PictureBox pb)
        {
            //update list view
            view.BeginUpdate();
            view.Items.Clear();
            //
            view.EndUpdate();

            //update picture box
            WIFILocation.DrawWIFIMap(pb);
        }

        public static void OpenWIFIConfig(string filename, ref ListView list, PictureBox pb)
        {
            if (Server.Run)
                return;

            //update view 
            //Server.wifiSpotModel = new WIFISpotModel(filename);
            WIFILocation.UpdateWIFIView(Server.wifiSpotModel, ref list, pb);
        }

        public static void DrawWIFIMap(PictureBox pb)
        {
                //paint map
                if (MapInfo.bitmap == null)
                    return;

                WIFIMap = (Bitmap)MapInfo.bitmap.Clone();
                pb.Image = WIFIMap;

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

        public static void DrawPoints(PictureBox pb)
        {
            //get content

            //drawing all points
            //DrawWIFIPoint(pb,
        }

        public static void DrawWIFIPoint(PictureBox pb, Color color, double xMeters, double yMeters)
        {
            //pb.Image = (Bitmap)QRMap.Clone();
            if (WIFIMap == null)
                return;

            Graphics g = Graphics.FromImage(pb.Image);
            g.FillEllipse(new SolidBrush(color), (float)((xMeters / MapInfo.SizeCoefficient) + MapInfo.PointX1 - WIFIRadius), (float)(yMeters / MapInfo.SizeCoefficient) + MapInfo.PointY1 - WIFIRadius, 2 * WIFIRadius, 2 * WIFIRadius);
            Pen pen = new Pen(Color.Black, 1);
            g.DrawEllipse(pen, (float)((xMeters / MapInfo.SizeCoefficient) + MapInfo.PointX1 - WIFIRadius), (float)(yMeters / MapInfo.SizeCoefficient) + MapInfo.PointY1 - WIFIRadius, 2 * WIFIRadius, 2 * WIFIRadius);
            WIFIMap = (Bitmap)pb.Image.Clone();
            pb.Image = WIFIMap;
        }

        /*
        public static int AddWIFI(ref ListView list, PictureBox pb)
        {
            int addResult = Server.wifiSpotModel.AddWIFIRecord();

            if (addResult == 0)
            {
                WIFILocation.UpdateWIFIView(Server.wifiSpotModel, ref list, pb);
            }
            return addResult;
        }

        public static int EditWIFI(ref ListView list, PictureBox pb)
        {
            int editResult = Server.wifiSpotModel.EditWIFIRecord();

            if (editResult == 0)
            {
                WIFILocation.UpdateWIFIView(Server.wifiSpotModel, ref list, pb);
            }
            return editResult;
        }

        public static int DeleteWIFI(ref ListView list, PictureBox pb) 
        {
            int delResult = Server.wifiSpotModel.DeleteWIFIRecord();

            if (delResult == 0)
            {
                WIFILocation.UpdateWIFIView(Server.wifiSpotModel, ref list, pb);
            }
            return delResult;
        }
        */
    }
}
