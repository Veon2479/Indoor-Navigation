using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Server
{
    public class IDModel
    {
        // max time of waiting 
        public static long MAX_TIME = int.Parse(ConfigurationManager.AppSettings.Get("MAX_ALIVE_TIME"));
        public const long DEFAULT_TIME = -1;
        public const int GET_ID_ERROR = -1;
        public const int ADD_ID_ERROR = -1;

        public List<long> UserTable = new List<long>();

        /// <summary>
        ///     class constructor
        /// </summary>
        /// <param name="tableCapasity">
        ///     initial table capacity
        /// </param>
        public IDModel(int tableCapasity)
        {
            AddUsers(tableCapasity + 1);
        }

        private int AddUser(long time)
        {
            // generate new id
            int newID;
            try
            {
                // creating new record
                UserTable.Add(time);
                newID = UserTable.Count - 1;
            }
            catch (Exception)
            {
                newID = ADD_ID_ERROR;
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
        private int AddUsers(int userCount)
        {
            int count = 0;
            for (int i = 0; i < userCount; i++)
            {
                if (AddUser(DEFAULT_TIME) != ADD_ID_ERROR)
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
        ///     new ID for user, 
        ///     if wrong long time = GET_ID_ERROR, 
        ///     if error = GET_ID_ERROR, 
        /// </returns>
        internal int GetUserID(long time, UserModel userModel)
        {
            int ID = GET_ID_ERROR;
            bool isFound = false;
            try
            {
                // last package arrived before this time => free ID
                long maxTimeNow = DateTimeOffset.Now.ToUnixTimeSeconds() - MAX_TIME;
                // if [long time] is wrong
                if (time < maxTimeNow || time > DateTimeOffset.Now.ToUnixTimeSeconds())
                {
                    ID = GET_ID_ERROR;
                    isFound = true;
                }
                for (int i = 1; i < UserTable.Count; i++)
                {
                    // user is not online for a long time
                    if (UserTable[i] < maxTimeNow && UserTable[i] != DEFAULT_TIME)
                    {
                        userModel.CloseUserID(i);
                        if (isFound)
                        {
                            UserTable[i] = DEFAULT_TIME;
                        }
                        else
                        {
                            ID = i;
                            // set new time (of new user)
                            isFound = true;
                            UserTable[ID] = time;
                        }
                    }
                    // if ID is free
                    else if (UserTable[i] == DEFAULT_TIME && !isFound)
                    {
                        ID = i;
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
                return GET_ID_ERROR;
            }
        }

        /// <summary>
        /// for updating table with each new package
        /// </summary>
        public void UpdateUserTime(int ID, long lastTime)
        {
            if (ID > 0 && ID < UserTable.Count)
            {
                UserTable[ID] = lastTime;
            }
        }

        /// <summary>
        ///     checking user ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>         
        ///     true if ID is taken, 
        ///     false if ID is free or timeout has expired
        /// </returns>
        internal bool ExistUserID(int ID, UserModel userModel)
        {
            long maxTimeNow = DateTimeOffset.Now.ToUnixTimeSeconds() - MAX_TIME;
            if (ID > 0 && ID < UserTable.Count)
            {
                if (UserTable[ID] < maxTimeNow && UserTable[ID] != DEFAULT_TIME)
                {
                    UserTable[ID] = DEFAULT_TIME;
                    userModel.CloseUserID(ID);
                }
                return UserTable[ID] != DEFAULT_TIME;
            }
            else
            {
                return false;
            }
        }
    }
}