﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Server
{
    public class IDModel
    {
        // dev by Pablo
        // here just function prototype
        public void CloseUserID(int ID) { }

        // max time of waiting 
        // 5 min
        public static long MAX_TIME = int.Parse(ConfigurationManager.AppSettings.Get("MAX_ALIVE_TIME"));
        public const long DEFAULT_TIME = -1;

        public Dictionary<int, long> UserTable = new();

        /// <summary>
        ///     class constructor
        /// </summary>
        /// <param name="tableCapasity">
        ///     initial table capacity
        /// </param>
        public IDModel(int tableCapasity)
        {
            AddUsers(tableCapasity);
        }

        private int AddUser(long time)
        {
            // generate new id
            int newID;
            try
            {
                newID = UserTable.Count;
                // creating new record
                UserTable.Add(newID, time);
            }
            catch (Exception)
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
        private int AddUsers(int userCount)
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
                        CloseUserID(key);
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
        public void UpdateUserTime(int ID, long lastTime)
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
        public bool ExistUserID(int ID)
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
