using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    static class OnlineView
    {
        static SolidBrush brush = new SolidBrush(Color.Blue);
        static Pen pen = new Pen(Color.Black, 2);

        internal static int UPDATE_INTERVAL = int.Parse(ConfigurationManager.AppSettings.Get("UPDATE_INTERVAL"));
        const int USER_POINT_SIZE = 4;
        private struct UserData
        {
            public int ID, x, y;
        }

        private static List<UserData> UserList = new List<UserData>(Server.DEFAULT_TABLE_CAPACITY);

        internal static void UpdateOnlineView(PictureBox pictureBox)
        {
            GetUserList();
            DrawMap(pictureBox);
        }

        private static void GetUserList()
        {
            UserData userData;
            //long time = DateTimeOffset.Now.ToUnixTimeSeconds();
            UserList.Clear();
            for (int i = 1; i < Server.userModel.userModelTempStorage.Length; i++)
            {
                if (Server.userModel.userModelTempStorage[i].Count > 0)
                {
                    int count = Server.userModel.userModelTempStorage[i].Count;
                    userData.ID = i;
                    userData.x = Convert.ToInt32(Server.userModel.userModelTempStorage[i].AccumData[count - 1].X
                        / MapInfo.SizeCoefficient);
                    userData.y = Convert.ToInt32(Server.userModel.userModelTempStorage[i].AccumData[count - 1].Y
                        / MapInfo.SizeCoefficient);
                    UserList.Add(userData);
                }
            }
        }

        internal static void DrawMap(PictureBox pictureBox)
        {
            pictureBox.Image = (Bitmap)MapInfo.bitmap.Clone();
            Graphics g = Graphics.FromImage(pictureBox.Image);
            for (int i = 0; i < UserList.Count; i++)
            {
                g.FillEllipse(brush,
                    UserList[i].x - USER_POINT_SIZE,
                    UserList[i].y - USER_POINT_SIZE,
                    USER_POINT_SIZE * 2,
                    USER_POINT_SIZE * 2);
                g.DrawEllipse(pen,
                    UserList[i].x - USER_POINT_SIZE,
                    UserList[i].y - USER_POINT_SIZE,
                    USER_POINT_SIZE * 2,
                    USER_POINT_SIZE * 2);
            }
        }
    }
}
