using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public struct userPacket
    {
        public int userID;
        public double x;
        public double y;
        public long time;

        public userPacket(int uID, double uX, double uY, long uTime)
        {
            userID = uID;
            x = uX;
            y = uY;
            time = uTime;
        }
    }
}