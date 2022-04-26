using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class Packet
    {
        public enum CommandCode
        {
            USER_REGISTRATION = 0,
            WIFI_REQUEST = 1,
        }

        public struct CommandPacket
        {
            public int command;
            public int parameter;
            public long time;

            public const int CMD_PACKET_SIZE = 2 * sizeof(int) + sizeof(long);

            public CommandPacket(int command, int parameter, long time)
            {
                this.command = command;
                this.parameter = parameter;
                this.time = time;
            }

            public static CommandPacket getStruct(byte[] data)
            {
                CommandPacket packet = new CommandPacket();
                try
                {
                    packet.command = BitConverter.ToInt32(data, 0);
                    packet.parameter = BitConverter.ToInt32(data, 4);
                    packet.time = BitConverter.ToInt64(data, 8);
                }
                catch (Exception e)
                {
                    packet.command = -1;
                    packet.parameter = -1;
                    packet.time = -1;
                }
                return packet;
            }

            public static byte[] getBytes(CommandPacket packet)
            {
                byte[] data = new byte[CMD_PACKET_SIZE];
                byte[] uCMD = BitConverter.GetBytes(packet.command);
                byte[] uPRM = BitConverter.GetBytes(packet.parameter);
                byte[] uTime = BitConverter.GetBytes(packet.time);
                uCMD.CopyTo(data, 0);
                uPRM.CopyTo(data, 4);
                uTime.CopyTo(data, 12);
                return data;
            }

            public override string ToString()
            {
                return $"Command: {command}, Parameter: {parameter}, Time: {time}";
            }
        }


        public struct UDPPacket
        {
            public int userID;
            public double x;
            public double y;
            public long time;

            public const int UDP_PACKET_SIZE = sizeof(int) + 2 * sizeof(double) + sizeof(long);

            public UDPPacket(int uID, double uX, double uY, long uTime)
            {
                userID = uID;
                x = uX;
                y = uY;
                time = uTime;           
            }

            public static UDPPacket getStruct(byte[] data, bool reverse = false)
            {
                if (reverse)
                {
                    Array.Reverse(data, 0, 4);
                    Array.Reverse(data, 4, 8);
                    Array.Reverse(data, 12, 8);
                    Array.Reverse(data, 20, 8);
                }

                UDPPacket packet = new UDPPacket();

                try
                {
                    packet.userID = BitConverter.ToInt32(data, 0);
                    packet.x = BitConverter.ToDouble(data, 4);
                    packet.y = BitConverter.ToDouble(data, 12);
                    packet.time = BitConverter.ToInt64(data, 20);
                }
                catch (Exception e)
                {
                    packet.userID = -1;
                    packet.x = -1;
                    packet.y = -1;
                    packet.time = -1;
                }
                return packet;
            }

            public static byte[] getBytes(UDPPacket packet)
            {
                byte[] data = new byte[28];
                byte[] uID = BitConverter.GetBytes(packet.userID);
                byte[] uX = BitConverter.GetBytes(packet.x);
                byte[] uY = BitConverter.GetBytes(packet.y);
                byte[] uTime = BitConverter.GetBytes(packet.time);
                uID.CopyTo(data, 0);
                uX.CopyTo(data, 4);
                uY.CopyTo(data, 12);
                uTime.CopyTo(data, 20);
                return data;
            }

            public override string ToString()
            {
                return $"userID: {userID}, x: {x}, y: {y}, time: {time}";
            }
        }
    }
}