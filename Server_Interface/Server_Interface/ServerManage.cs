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

            foreach(var user in userIDModel.UserTable)
            {
                if (user.Value != IDModel.DEFAULT_TIME)
                {
                    ListViewItem lvItem = new ListViewItem(user.Key.ToString());
                    lvItem.SubItems.Add(user.Value.ToString());
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
