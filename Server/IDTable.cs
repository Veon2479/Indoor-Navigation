using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    public class IDTable
    {
        // dev by Pablo
        // here just function prototype
        public void CloseID(int ID) { }

        // max time of waiting 
        // 5 min
        public const long MAX_TIME = 5 * 60;
        public const long DEFAULT_TIME = -1;

        // increment
        public const int INC_MODE = 0;
        // generating 
        public const int GEN_MODE = 1;

        public int Mode;
        public Dictionary<int, long> UserTable = new();

        public IDTable(int startCount, int mode)
        {
            Mode = mode;
            AddUsers(startCount);
        }

        public int AddUser(long time)
        {
            // generate new id
            int newID;
            if (Mode == GEN_MODE)
            {
                StringBuilder builder = new();
                Enumerable.Range(65, 6).Select(e => ((char)e).ToString())
                    .Concat(Enumerable.Range(65, 6).Select(e => ((char)e).ToString()))
                    .Concat(Enumerable.Range(0, 10).Select(e => e.ToString()))
                    .OrderBy(e => Guid.NewGuid())
                    .Take(8)
                    .ToList().ForEach(e => builder.Append(e));

                newID = BitConverter.ToInt32(Convert.FromHexString(builder.ToString()), 0);
                // creating new record
                UserTable.Add(newID, time);
            }
            else if (Mode == INC_MODE)
            {
                newID = UserTable.Count;
                // creating new record
                UserTable.Add(newID, time);
            }
            // if wrong mode
            else
            {
                newID = -1;
            }
            return newID;
        }

        /// <summary>
        /// add IDs with DEFAULT_TIME
        /// </summary>
        /// <param name="userCount"></param>
        /// <returns>
        ///     count of created users
        /// </returns>
        public int AddUsers(int userCount)
        {
            int count = 0;
            for (int i = 0; i < userCount; i++)
            {
                if (AddUser(DEFAULT_TIME) != -1)
                {
                    count++;
                }
            }
            return count;
        }

        /// <summary>
        ///     add new ID in table
        /// </summary>
        /// <param name="time">
        ///     time of registration request from usen
        /// </param>
        /// <returns>
        ///     new ID for user 
        ///     if error = -1 
        /// </returns>
        public int GetUserID(long time)
        {
            int ID = -1;
            bool isFound = false;
            try
            {
                // last package arrived before this time => free ID
                long maxTimeNow = DateTimeOffset.Now.ToUnixTimeSeconds() - MAX_TIME;
                foreach (int key in UserTable.Keys)
                {
                    // user is not online for a long time
                    if (UserTable[key] < maxTimeNow && UserTable[key] != DEFAULT_TIME)
                    {
                        CloseID(key);
                        if (isFound)
                        {
                            UserTable[key] = DEFAULT_TIME;
                        }
                        else
                        {
                            ID = key;
                            // set new time (of new user)
                            isFound = true;
                            UserTable[ID] = time;
                        }
                    }
                    // if ID is free
                    else if (UserTable[key] == DEFAULT_TIME && !isFound)
                    {
                        ID = key;
                        // set new time (of new user)
                        isFound = true;
                        UserTable[ID] = time;
                    }
                }
                // create new ID, if all IDs is taken
                if (!isFound)
                {
                    // create new ID
                    ID = AddUser(time);
                }
                return ID;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        /// <summary>
        /// for updating table with each new package
        /// </summary>
        public void UpdateTable(int ID, long lastTime)
        {
            UserTable[ID] = lastTime;
        }

        /// <summary>
        ///     checking user ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>         
        ///     true if ID is taken
        ///     false if ID is free
        /// </returns>
        public bool CheckUserID(int ID)
        {
            long maxTimeNow = DateTimeOffset.Now.ToUnixTimeSeconds() - MAX_TIME;
            if (UserTable[ID] < maxTimeNow)
            {
                UserTable[ID] = DEFAULT_TIME;
            }
            return UserTable[ID] != DEFAULT_TIME;
        }
    }
}
