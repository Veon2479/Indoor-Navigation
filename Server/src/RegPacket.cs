using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public struct RegPacket
    {
        public int userID;
        public long QRID;
        public long reserved;
        public long time;

        public const int PACKET_SIZE = sizeof(int) + sizeof(double) * 2 + sizeof(long);

        public RegPacket(int uID, long uQRID, long uTime)
        {
            userID = uID;
            QRID = uQRID;
            reserved = 0;
            time = uTime;
        }

        public static RegPacket getStruct(byte[] data, bool reverse = false)
        {
            if (reverse)
            {
                Array.Reverse(data, 0, 4);
                Array.Reverse(data, 4, 8);
                Array.Reverse(data, 12, 8);
                Array.Reverse(data, 20, 8);
            }

            RegPacket packet = new RegPacket();

            try
            {
                packet.userID = BitConverter.ToInt32(data, 0);
                packet.QRID = BitConverter.ToInt64(data, 4);
                packet.reserved = BitConverter.ToInt64(data, 12);
                packet.time = BitConverter.ToInt64(data, 20);
            }
            catch (Exception e)
            {
                packet.userID = -1;
                packet.QRID = -1;
                packet.reserved = -1;
                packet.time = -1;
            }
            return packet;
        }

        public static byte[] getBytes(RegPacket packet)
        {
            byte[] data = new byte[28];
            byte[] uID = BitConverter.GetBytes(packet.userID);
            byte[] uQRID = BitConverter.GetBytes(packet.QRID);
            byte[] uReserved = BitConverter.GetBytes(packet.reserved);
            byte[] uTime = BitConverter.GetBytes(packet.time);
            uID.CopyTo(data, 0);
            uQRID.CopyTo(data, 4);
            uReserved.CopyTo(data, 12);
            uTime.CopyTo(data, 20);
            return data;
        }

        public override string ToString()
        {
            return $"userID: {userID}, QR ID: {QRID}, time: {time}";
        }
    }
}