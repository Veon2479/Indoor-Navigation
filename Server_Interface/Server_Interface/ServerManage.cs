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

        //update online users
        public static void UpdateOnlineUsers(ref ListView view, IDModel userIDModel)
        {
            view.BeginUpdate();
            view.Items.Clear();

            for(int i = 1; i < userIDModel.UserTable.Count; i++)
            {
                if (userIDModel.UserTable[i] != IDModel.DEFAULT_TIME)
                {
                    ListViewItem lvItem = new ListViewItem(i.ToString());
                    lvItem.SubItems.Add(userIDModel.UserTable[i].ToString());
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
    }
}
