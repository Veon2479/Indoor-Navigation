using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;

namespace Server
{
    static class OnlineView
    {
        static SolidBrush brush = new SolidBrush(Color.BlueViolet);
        static Pen pen = new Pen(Color.Black, 2);

        internal static int UPDATE_ONLINE_VIEW_INTERVAL 
            = int.Parse(ConfigurationManager.AppSettings.Get("UPDATE_ONLINE_VIEW_INTERVAL"));
        const int USER_POINT_SIZE = 6;
        // coordinates in pixels
        private struct UserData
        {
            public int ID;
            public float x, y;
        }
        // coordinates in meters
        internal struct RealUserData
        {
            public int ID;
            public double x, y;
        }

        private static RealUserData GetRealUserData(UserData userData)
        {
            RealUserData rud;
            rud.ID = userData.ID;
            rud.x = (userData.x - MapInfo.PointX1) * MapInfo.SizeCoefficient;
            rud.y = (userData.y - MapInfo.PointY1) * MapInfo.SizeCoefficient;
            return rud;
        }

        private static List<UserData> UserList = new List<UserData>(Server.DEFAULT_TABLE_CAPACITY);

        internal static void UpdateOnlineView(PictureBox pictureBox)
        {
            GetUserList();
            DrawMap(pictureBox);
        }

        private static void GetUserList()
        {
            if (Server.userModel == null)
                return;
            UserData userData;
            // long time = DateTimeOffset.Now.ToUnixTimeSeconds();
            UserList.Clear();
            for (int i = 1; i < Server.userModel.userModelTempStorage.Length; i++)
            {
                if (Server.userModel.userModelTempStorage[i].Count > 0)
                {
                    int count = Server.userModel.userModelTempStorage[i].Count;
                    userData.ID = i;
                    userData.x = (float)(Server.userModel.userModelTempStorage[i].AccumData[count - 1].X
                        / MapInfo.SizeCoefficient + MapInfo.PointX1);
                    userData.y = (float)(Server.userModel.userModelTempStorage[i].AccumData[count - 1].Y
                        / MapInfo.SizeCoefficient + MapInfo.PointY1);
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

        internal static RealUserData GetUserInfo(int x, int y)
        {
            for (int i = 0; i < UserList.Count; i++)
            {
                if (UserList[i].x - USER_POINT_SIZE < x
                    && UserList[i].x + USER_POINT_SIZE > x
                    && UserList[i].y - USER_POINT_SIZE < y
                    && UserList[i].y + USER_POINT_SIZE > y
                    )
                {
                    return GetRealUserData(UserList[i]);
                }
            }
            return new RealUserData { ID = 0 };
        }
    }
}
