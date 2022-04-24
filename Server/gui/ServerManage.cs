using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    internal class ServerManage
    {
        private static string UnixToDate(long Time, string ConvertFormat)
        {
            DateTime ConvertedUnixTime = DateTimeOffset.FromUnixTimeSeconds(Time).DateTime.ToLocalTime();
            return ConvertedUnixTime.ToString(ConvertFormat);
        }

        //update online users
        public static void UpdateOnlineUsersView(ref ListView view, IDModel userIDModel)
        {
            view.BeginUpdate();
            view.Items.Clear();

            for (int i = 1; i < userIDModel.UserTable.Count; i++)
            {
                if (userIDModel.UserTable[i] != IDModel.DEFAULT_TIME)
                {
                    ListViewItem lvItem = new ListViewItem(i.ToString());
                    lvItem.SubItems.Add(UnixToDate(userIDModel.UserTable[i], "dd:MM:yyyy HH:mm:ss"));
                    view.Items.Add(lvItem);
                }
            }
            view.EndUpdate();
        }

        //start the server
        public static void StartServer(ref Timer timer)
        {
            timer.Start();
            Server.Start();
        }

        //stop the server
        public static void StopServer(ref Timer timer)
        {
            Server.Stop();
            timer.Stop();
        }

        //set up server
        public static void SetUpServer(Server.LogMessageDelegate log, ref ListView QRView, PictureBox pb)
        {
            Server.LogMessage = log;
            Server.qrModel = new QRModel();
            QRLocation.UpdateQRView(Server.qrModel, ref QRView, pb);
        }
    }
}
