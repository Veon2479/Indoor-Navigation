using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class UserModel
    {
        //Data about position of 1 user in at given moment
        protected internal struct UserModelPositionData{
            internal double X, Y;
            internal long Time;        
        }

        //Data about 1 user nessesary to accumuleate PositionData and save it to file
        protected internal struct UserModelTempStorageEl{
            internal string FileName;
            internal int Count; //Alos using as flag "does ID exist"
            internal UserModelPositionData[] AccumData;
        }
        //Defaul directory to save accumulated data
        private const string DEFAULT_DIR = "ID_Data";

        //Const that define ID as "Free"
        private const int NO_ID = -1;

        //Size of accumulated storage (how much record save by time)
        private int _accumDataSize = 0;

        //Size of temporrary storage (how much users can be processing) (expands by 2 times if need)
        private int _amountOfUsers = 0;

        //Temporary Storage that accumulate data about users and save it to file
        protected internal UserModelTempStorageEl[] userModelTempStorage;
        
        /// <summary>
        ///     Create object
        /// </summary>
        /// <param name="amountOfUsers">Size of temporrary storage (how much users can be processing) (expands by 2 times if need)</param>
        /// <param name="accumDataSize">Size of accumulated storage (how much record save by time))</param>
        public UserModel(int amountOfUsers, int accumDataSize)
        {
            this._amountOfUsers = amountOfUsers;
            this._accumDataSize = accumDataSize;
            Array.Resize(ref userModelTempStorage, this._amountOfUsers);
            for (int i = 0; i < this._amountOfUsers; i++){
                Array.Resize(ref (userModelTempStorage[i].AccumData), this._accumDataSize);
                userModelTempStorage[i].Count = NO_ID;
            }
        }

        /// <summary>
        ///     Add user ID to temparary storage
        /// </summary>
        /// <param name="ID">User ID</param>
        /// <param name="x">Coordinate x</param>
        /// <param name="y">Coordinate y</param>
        /// <param name="time">UNIX time</param>
        /// <returns>
        ///     <list type="table">
        ///         <listheader>
        ///             <term>
        ///                 >= 0 - no errors, else - error
        ///             </term>
        ///         </listheader>
        ///     <item>-1: Incorrect ID (ID < 0)</item>
        ///     <item>-2: Cannot resize array (reallocate memory)</item>
        ///     <item>-3: ID is occupied</item>
        ///     </list>
        /// </returns>
        public int AddUserID(int ID, double x, double y, long time)
        {
            //Check for correct ID
            if (ID < 0){
                return -1; //Incorrect ID (ID < 0)
            }

            //Try to reallocate memory if it nessesarry
            if (this._amountOfUsers < ID){
                try{
                    _amountOfUsers *= 2;
                    Array.Resize(ref this.userModelTempStorage, _amountOfUsers);
                }catch{
                    return -2; //Cannot resize array
                }
            }

            if (this.userModelTempStorage[ID].Count >= 0){
                return -3; //ID is occupied
            }

            //Create file path
            StringBuilder FileName = new StringBuilder(DEFAULT_DIR + "/" + time.ToString() + "_" + ID.ToString() + ".uinf");
            var processedID = this.userModelTempStorage;

            //Fill new element
            processedID[ID].Count = 0;
            processedID[ID].FileName = FileName.ToString();                 
            processedID[ID].AccumData[processedID[ID].Count].X = x;
            processedID[ID].AccumData[processedID[ID].Count].Y = y;
            processedID[ID].AccumData[processedID[ID].Count].Time = time;
            processedID[ID].Count++;
            return 0;
        }

        /// <summary>
        ///     Append user data in temoarrary storage accorgind user ID
        /// </summary>
        /// <param name="ID">User ID</param>
        /// <param name="x">Coordinate x</param>
        /// <param name="y">Coordinate y</param>
        /// <param name="time">UNIX time</param>
        /// <returns>
        ///     <list type="table">
        ///         <listheader>
        ///             <term>
        ///                 >= 0 - no errors, else - error
        ///             </term>
        ///         </listheader>
        ///     <item>-1: Incorrect ID (ID < 0)</item>
        ///     <item>-2: Out of range. ID does not exist </item>
        ///     <item>-3: ID does not exist (it set to "Free")</item>
        ///     <item>-4: Error in writing to the file. See "SaveStorageEl" method  </item>
        ///     </list>
        /// </returns>
        public int AppendUserData(int ID, double x, double y, long time)
        {
            //Check for correct ID
            if (ID < 0){
                return -1; //Incorrect ID (ID < 0)
            }

            //Check for exesting ID
            if (ID >= this._amountOfUsers){
                return -2; //Out of range. ID does not exist
            }
            var processedID = this.userModelTempStorage;
            if (processedID[ID].Count < 0){
                return -3; //ID does not exist
            }

            //Fill new Element
            processedID[ID].AccumData[processedID[ID].Count].X = x;
            processedID[ID].AccumData[processedID[ID].Count].Y = y;
            processedID[ID].AccumData[processedID[ID].Count].Time = time;
            processedID[ID].Count++;

            //Check for fullness
            if (processedID[ID].Count >= this._accumDataSize){

                //Write data to file
                if (SaveStorageEl(processedID[ID]) < 0){
                    return -4; //See SaveStorageEl errors 
                }
                processedID[ID].Count = 0;
            }
            return 0;
        }   

        /// <summary>
        ///     Force save data from temporrary storage to the file according ID
        /// </summary>
        /// <param name="ID">UserID</param>
        /// <returns>
        ///     <list type="table">
        ///         <listheader>
        ///             <term>
        ///                 >= 0 - no errors, else - error
        ///             </term>
        ///         </listheader>
        ///     <item>-1: Incorrect ID (ID < 0)</item>
        ///     <item>-2: Error in writing to the file. See "SaveStorageEl" method </item>
        ///     </list>
        /// </returns>
        public int CloseUserID(int ID)
        {
            //Check for correct ID
            if (ID < 0){
                return -1; //Incorrect ID (ID < 0)
            }
            //Forced save data to file
            var processedID = this.userModelTempStorage;
            if (SaveStorageEl(processedID[ID]) < 0){
                return -2; //See SaveStorageEl errors 
            }

            //"Free" ID
            processedID[ID].Count = NO_ID; 
            return 0;
        }

        /// <summary>
        ///     Force save data from temporrary storage to the file to all current using ID
        /// </summary>
        /// <returns>
        ///     <list type="table">
        ///         <listheader>
        ///             <term>
        ///                 >= 0 - no errors, else - error
        ///             </term>
        ///         </listheader>
        ///     <item>-1: Error in writing to the file. See "SaveStorageEl" method </item>
        ///     </list>
        /// </returns>
        public int FlushTempStorage() 
        {
            int iError, iResult = 0;

            //Write all using ID's data to file
            for (int i = 0; i < this._amountOfUsers; i++){
                if (this.userModelTempStorage[i].Count != NO_ID){
                    iError = SaveStorageEl(this.userModelTempStorage[i]);
                    if (iError < 0){
                        iResult = iError; //See SaveStorageEl errors 
                    }

                    //"Free" ID
                    this.userModelTempStorage[i].Count = NO_ID;
                }
            }
            return iResult;
        }

        /// <summary>
        ///     Save accumulated data form storage to file according internal parametrs (Count, FileName) 
        /// </summary>
        /// <param name="tempStorageEl">Element of temporrary storage. Contain data about ID</param>
        /// <returns>
        ///     <list type="table">
        ///         <listheader>
        ///             <term>
        ///                 >= 0 - no errors, else - error
        ///             </term>
        ///         </listheader>
        ///     <item>-1: Error in writing file (no such directory, can't open, 
        ///     invalid name, can't write to file, can't close BinaryWriter ... )</item>
        ///     </list>
        /// </returns>
        private int SaveStorageEl(UserModelTempStorageEl tempStorageEl)
        {
            try{
                //Check if directory exists
                if (!Directory.Exists(DEFAULT_DIR)){
                    Directory.CreateDirectory(DEFAULT_DIR);
                }
                
                //Write data to file
                using(BinaryWriter bWriter = new BinaryWriter(File.Open(tempStorageEl.FileName, FileMode.Append))){
                    for(int i = 0; i < tempStorageEl.Count; i++){
                        bWriter.Write(tempStorageEl.AccumData[i].X);
                        bWriter.Write(tempStorageEl.AccumData[i].Y);
                        bWriter.Write(tempStorageEl.AccumData[i].Time);
                    }
                bWriter.Close();
                }
                }catch{
                    return -1; //Thraulble with file (no such directory, can't open,
                               //invalid name, can't write to file, can't close BinaryWriter ... )
                }
            return 0;
        }
    }
}
