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
        internal static WIFISpotModel.WIFISpotModelXmlContent selectedPoint = new WIFISpotModel.WIFISpotModelXmlContent();

        internal static int WIFIRadius = 6;

        //update list view of QR location
        public static void UpdateWIFIView(WIFISpotModel spotModel, ref ListView view, PictureBox pb)
        {
            //update list view
            view.BeginUpdate();
            view.Items.Clear();

            WIFISpotModel.WIFISpotModelXmlContent[] contents = null;
            Server.wifiSpotModel.GetWIFISpotRecordList(ref contents);
            foreach (var item in contents)
            {
                ListViewItem lvItem = new ListViewItem(item.WIFISpotID);
                lvItem.SubItems.Add(item.WIFISpotName);
                lvItem.SubItems.Add(item.WIFISpotMAC);
                lvItem.SubItems.Add(item.WIFISpotPower);
                lvItem.SubItems.Add(item.X);
                lvItem.SubItems.Add(item.Y);

                view.Items.Add(lvItem);
            }

            view.EndUpdate();

            //update picture box
            WIFILocation.DrawWIFIMap(pb);
        }

        //open Wi-Fi config
        public static void OpenWIFIConfig(string filename, ref ListView list, PictureBox pb)
        {
            if (Server.Run)
                return;

            Server.wifiSpotModel = new WIFISpotModel(filename);

            //update view 
            WIFILocation.UpdateWIFIView(Server.wifiSpotModel, ref list, pb);
        }

        //draw Wi-Fi map
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

        //draw all Wi-Fi spots
        public static void DrawPoints(PictureBox pb)
        {
            //get content
            WIFISpotModel.WIFISpotModelXmlContent[] content = null;
            Server.wifiSpotModel.GetWIFISpotRecordList(ref content);
            if (content == null)
                return;

            //drawing all points
            foreach (var point in content)
            {
                double x = double.Parse(point.X, System.Globalization.CultureInfo.InvariantCulture);
                double y = double.Parse(point.Y, System.Globalization.CultureInfo.InvariantCulture);
                DrawWIFIPoint(pb, Color.Blue, x, y);
            }
        }

        //draw Wi-Fi point
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

        //add Wi-Fi to config
        public static int AddWIFI(string WIFIID, string WIFIName, string MAC, string Power, string X, string Y, ref ListView list, PictureBox pb)
        {
            int addResult = Server.wifiSpotModel.AddWIFISpotRecord(WIFIID, WIFIName, X, Y, Power, MAC);

            if (addResult >= 0)
            {
                WIFILocation.UpdateWIFIView(Server.wifiSpotModel, ref list, pb);
            }
            return addResult;
        }

        //edit Wi-Fi in config
        public static int EditWIFI(string OldWIFIID, string WIFIID, string WIFIName, string MAC, string Power, string X, string Y, ref ListView list, PictureBox pb)
        {
            int editResult = Server.wifiSpotModel.ChangeWIFISpotRecord(OldWIFIID, WIFIID, WIFIName, X, Y, Power, MAC);

            if (editResult >= 0)
            {
                WIFILocation.UpdateWIFIView(Server.wifiSpotModel, ref list, pb);
            }
            return editResult;
        }

        //delete Wi-Fi from config
        public static int DeleteWIFI(string WIFIID, ref ListView list, PictureBox pb)
        {
            int delResult = Server.wifiSpotModel.DeleteWIFISpotRecord(WIFIID);

            if (delResult >= 0)
            {
                WIFILocation.UpdateWIFIView(Server.wifiSpotModel, ref list, pb);
            }
            return delResult;
        }
        
        //check hitting in point
        public static WIFISpotModel.WIFISpotModelXmlContent HitPoint(int X, int Y)
        {
            WIFISpotModel.WIFISpotModelXmlContent[] content = null;
            Server.wifiSpotModel.GetWIFISpotRecordList(ref content);
            if (content != null)
            {
                foreach (var point in content)
                {
                    //meters 
                    double xCircle = double.Parse(point.X, System.Globalization.CultureInfo.InvariantCulture);
                    double yCircle = double.Parse(point.Y, System.Globalization.CultureInfo.InvariantCulture);

                    if (Math.Pow(((X - MapInfo.PointX1) * MapInfo.SizeCoefficient - xCircle), 2) + Math.Pow(((Y - MapInfo.PointY1) * MapInfo.SizeCoefficient - yCircle), 2) <= Math.Pow(WIFIRadius * MapInfo.SizeCoefficient, 2))
                    {
                        return point;
                    }
                }
            }
            WIFISpotModel.WIFISpotModelXmlContent ret = new WIFISpotModel.WIFISpotModelXmlContent();
            return ret;
        }

        //on select point
        public static void SelectPoint(WIFISpotModel.WIFISpotModelXmlContent point, ToolTip ttWIFI, PictureBox pb)
        {
            //show tip, select point
            WIFILocation.DrawWIFIMap(pb);
            string content = $"Wi-Fi ID: {point.WIFISpotID}\nWi-Fi Name: {point.WIFISpotName}\nMAC: {point.WIFISpotMAC}\nPower: {point.WIFISpotPower}\nX: {point.X}\nY: {point.Y}";
            ttWIFI.SetToolTip(pb, content);
            double x = double.Parse(point.X, System.Globalization.CultureInfo.InvariantCulture);
            double y = double.Parse(point.Y, System.Globalization.CultureInfo.InvariantCulture);
            WIFILocation.DrawWIFIPoint(pb, Color.Orange, x, y);
            WIFILocation.selecting = true;
        }
    }
}
