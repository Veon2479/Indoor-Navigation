using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    static class HeatMap
    {
        const int SQ_SIZE = 10;
        static private int MaxCount = 0;
        static List<List<int>> Matrix;

        internal static void Generate(PictureBox pictureBox, long beginTime, long endTime)
        {
            GetMatrix(pictureBox.Image.Width / SQ_SIZE, pictureBox.Image.Height / SQ_SIZE);
            GetUserPoints(beginTime, endTime);
            GetMax();
            if (MaxCount == 0)
                return;
            DrowMap(pictureBox);
        }

        private static void GetMax()
        {
            for (int x = 0; x < Matrix.Count; x++)
            {
                for (int y = 0; y < Matrix[0].Count; y++)
                {
                    if (Matrix[x][y] > MaxCount) MaxCount = Matrix[x][y];
                }
            }
        }

        private static void GetMatrix(int x, int y)
        {
            Matrix = new List<List<int>>();
            List<int> row = new List<int>(x);
            for (int i = 0; i < x; i++)
            {
                Matrix.Add(new List<int>());
                for (int j = 0; j < y; j++)
                {
                    Matrix[i].Add(0);
                }
            }
        }

        private static void GetUserPoints(long beginTime, long endTime)
        {
            MaxCount = 0;
            long sessionTime;
            int x, y;
            long t;
            try
            {
                if (Server.userModel != null && Server.userModel.userModelTempStorage != null)
                {
                    for (int i = 0; i < Server.userModel.userModelTempStorage.Length; i++)
                    {
                        for (int j = 0; j < Server.userModel.userModelTempStorage[i].Count; j++)
                        {
                            t = Server.userModel.userModelTempStorage[i].AccumData[j].Time;
                            if (t >= beginTime)
                            {
                                if (t <= endTime)
                                {
                                    x = Convert.ToInt32(Server.userModel.userModelTempStorage[i].AccumData[j].X / MapInfo.SizeCoefficient + MapInfo.PointX1);
                                    y = Convert.ToInt32(Server.userModel.userModelTempStorage[i].AccumData[j].Y / MapInfo.SizeCoefficient + MapInfo.PointY1);
                                    if (x / SQ_SIZE > -1 && x / SQ_SIZE < Matrix.Count && y / SQ_SIZE > -1 && y / SQ_SIZE < Matrix[0].Count)
                                        Matrix[x / SQ_SIZE][y / SQ_SIZE]++;
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception) { }
            try
            {
                string[] sessionDirectories = Directory.GetDirectories(UserModel._sessionsDir);
                List<string> foundedDirectories = new List<string>(sessionDirectories.Length);
                Array.Sort(sessionDirectories, 0, sessionDirectories.Length, null);
                for (int i = 0; i < sessionDirectories.Length; i++)
                {
                    if (long.TryParse(sessionDirectories[i].Substring(sessionDirectories[i].Length-10), out sessionTime))
                    {
                        if (sessionTime >= beginTime && sessionTime <= endTime)
                        {
                            foundedDirectories.Add(sessionDirectories[i]);
                        }
                    }
                }
                foreach (string foundDirectory in foundedDirectories)
                {
                    string[] userFiles = Directory.GetFiles(foundDirectory);
                    foreach (string userFile in userFiles)
                    {
                        try
                        {
                            using (BinaryReader bReader = new BinaryReader(File.Open(userFile, FileMode.Open)))
                            {
                                byte[] buf = bReader.ReadBytes(24);
                                while (buf.Length > 0)
                                {
                                    t = BitConverter.ToInt64(buf, 16);
                                    if (t >= beginTime)
                                    {
                                        if (t <= endTime)
                                        {
                                            x = Convert.ToInt32(BitConverter.ToDouble(buf, 0) / MapInfo.SizeCoefficient + MapInfo.PointX1);
                                            y = Convert.ToInt32(BitConverter.ToDouble(buf, 8) / MapInfo.SizeCoefficient + MapInfo.PointY1);
                                            if (x / SQ_SIZE > -1 && x / SQ_SIZE < Matrix.Count && y / SQ_SIZE > -1 && y / SQ_SIZE < Matrix[0].Count)
                                                Matrix[x / SQ_SIZE][y / SQ_SIZE]++;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    buf = bReader.ReadBytes(24);
                                }
                            }
                        }
                        catch (Exception) { }
                    }
                }
            } catch (Exception) { }
        }

        private static void DrowMap(PictureBox pictureBox)
        {
            Graphics g = Graphics.FromImage(pictureBox.Image);
            for (int x = 0; x < pictureBox.Image.Width / SQ_SIZE; x++)
            {
                for (int y = 0; y < pictureBox.Image.Height / SQ_SIZE; y++)
                {
                    Color c = GetColor(Matrix[x][y]);
                    g.FillRectangle(
                        new SolidBrush(c),
                       x * SQ_SIZE, y * SQ_SIZE, SQ_SIZE, SQ_SIZE);
                }
            }
        }

        // *magic*
        private static Color GetColor(int n)
        {
            int c = Convert.ToInt32(1024 / MaxCount * n * 1.0);
            {
                if (c > 255 * 3)
                {
                    c %= 255 ;
                    return Color.FromArgb(c / 4 + 64*3, 255, 255 - c, 0);
                }
                else if (c > 255 * 2)
                {
                    c %= 255;
                    return Color.FromArgb(c / 4 + 64*2, c, 255, 0);
                }
                else if (c > 255)
                {
                    c %= 255;
                    return Color.FromArgb(c / 4 + 64*1, 0, 255, 255 - c);
                }
                else
                {
                    c %= 255;
                    return Color.FromArgb(c / 4+64*0, 0, c, 255);
                }
            }
        }
    }
}
