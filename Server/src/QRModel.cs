using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Xml;
using QRCoder;

namespace Server
{
    internal class QRModel
    {
        //Struct that contain information about one record in xml file
        public struct QRModelXmlContent
        {
            public string QRID;
            public string QRName;
            public string X;
            public string Y;
        }

        //Name of xml file to work with
        internal string _xmlFileName { get; private set; } = "";

        //Directory that contains xml file and QR codes to work with
        private string _workQRDir = "";

        //Default directory that contais xml file and QR codes
        protected const string _defaultPrivateDir = "DefaultQRCode";

        //Default directory that contains private directories
        protected const string _defaultDir = "config";

        //Default directory that contains QRCodes image
        protected const string _defaultQRCodeDir = "QRCodes";

        //Default xml file name
        protected const string _defaultName = "DefaultQRData.xml";

        //Default data that contain QR code
        protected string _defaultQRCodeData = "Default data";

        //Amount of pixels on one module
        protected int _pixelsPerModule = 6;

        //List that contain exicting QRID
        List<int> _QRIDExist = new List<int>();

        /// <summary>
        ///     Open and check xml file. Set _workDir to work with QRCodes. If smth not exist - create it
        /// </summary>
        /// <param name="xmlFileName">Name of xml file to work with</param>
        /// <exception cref="Exception">Xml file incorrect or was corrupted</exception>
        public QRModel(string xmlFileName = "")
        {
            _xmlFileName = xmlFileName;
            if (_xmlFileName != ""){
                string ext = xmlFileName.Remove(0, xmlFileName.LastIndexOf(".")+1);
                if (ext != "xml"){
                    throw new Exception ("Incorrect file name");
                }
                _workQRDir = _xmlFileName.Remove(_xmlFileName.LastIndexOf('.')) + "_" + _defaultQRCodeDir;
            }
            XmlDocument xmlDocument = new XmlDocument();

            //Check xml document
            int iResult = CheckXmlFileContent(ref xmlDocument);
            if (iResult == (int)CheckXmlFileContentErrorCode.READ_FILE_ERROR)
            {

                if (_xmlFileName == "")
                {
    
                    //Work with default file name if name is empty
                    _xmlFileName = _defaultDir + "/" + _defaultPrivateDir + "/" + _defaultName;
                    _workQRDir = _xmlFileName.Remove(_xmlFileName.LastIndexOf('.')) + "_" + _defaultQRCodeDir;
                }else{

                    //Create new files
                    int insertInd = _xmlFileName.LastIndexOf('/');

                    //Get only name to insert
                    string onlyName = _xmlFileName.Remove(0, insertInd+1);
                    onlyName = onlyName.Remove(onlyName.LastIndexOf('.'));

                    _xmlFileName = _xmlFileName.Insert(insertInd+1, onlyName + "/");
                    _workQRDir = _xmlFileName.Remove(_xmlFileName.LastIndexOf('.')) + "_" + _defaultQRCodeDir;
                }
            }
            else if (iResult < 0)
            {
                throw new Exception("Incorrect file or file was corrupted");
            }

            //Change xml document to default or create new
            CreateNessaryFiles();

            //Check chosen
            if (CheckXmlFileContent(ref xmlDocument) < 0)
            {
                throw new Exception("Incorrect default file or file was corrupted");
            }
        }

        public enum GetQRCoordErrorCode
        {
            CORRUPTED_FILE = -1,
            QRID_INCORRECT = -2,
            PARSE_TO_DOUBLE_ERROR = -3
        }
        /// <summary>
        ///     Return QR Coordiates according recived QRID
        /// </summary>
        /// <param name="QRID">ID of QR record in xml file</param>
        /// <param name="x">Coordinate x</param>
        /// <param name="y">Coordinate y</param>
        /// <returns>
        ///     <list type="table">
        ///         <listheader>
        ///             <term>
        ///                 >= 0 - no errors, else - error
        ///             </term>
        ///         </listheader>
        ///     <item>-1 (CORRUPTED_FILE): Xml file does not exist or was corrupted</item>
        ///     <item>-2 (QRID_INCORRECT): No this ID in xml file</item>
        ///     <item>-3 (PARSE_TO_DOUBLE_ERROR): Error in parcing data from xml table to double</item>
        ///     </list>
        /// </returns>
        public int GetQRCoord(int QRID, ref double x, ref double y)
        {
            XmlDocument xmlDoc = new XmlDocument();

            //Check for correct file content
            if (CheckXmlFileContent(ref xmlDoc) < 0)
            {
                return (int)GetQRCoordErrorCode.CORRUPTED_FILE;
            }
            XmlElement xmlEl = xmlDoc.DocumentElement;

            //Check for correct QRID
            if (QRID < 0 || !_QRIDExist.Contains(QRID))
            {
                return (int)GetQRCoordErrorCode.QRID_INCORRECT;
            }

            XmlNode xmlNode = xmlEl.ChildNodes[_QRIDExist.IndexOf(QRID)];

            //Try to parce string to double
            try
            {
                x = double.Parse(xmlNode.ChildNodes[0].InnerText, System.Globalization.CultureInfo.InvariantCulture);
                y = double.Parse(xmlNode.ChildNodes[1].InnerText, System.Globalization.CultureInfo.InvariantCulture);
            }
            catch
            {
                return (int)GetQRCoordErrorCode.PARSE_TO_DOUBLE_ERROR;
            }
            return 0;
        }

        public enum DeleteQRRecordErrorCode
        {
            CORRUPTED_FILE = -1,
            QRID_INCORRECT = -2,
            NAME_NOT_FOUND = -3
        }
        /// <summary>
        ///     Delet QR Record from xml file according recived QRID or QRName
        /// </summary>
        /// <param name="QRID_QRName">ID or Name of QR record in xml file</param>
        /// <returns>
        ///     <list type="table">
        ///         <listheader>
        ///             <term>
        ///                 >= 0 - no errors, else - error
        ///             </term>
        ///         </listheader>
        ///     <item>-1 (CORRUPTED_FILE): Xml file does not exist or was corrupted</item>
        ///     <item>-2 (QRID_INCORRECT): No this ID in xml file</item>
        ///     <item>-3 (NAME_NOT_FOUD): No this Name in xml file</item>
        ///     </list>
        /// </returns>
        public int DeleteQRRecord(string QRID_QRName)
        {
             XmlDocument xmlDoc = new XmlDocument();

            //Check for correct file content
            if (CheckXmlFileContent(ref xmlDoc) < 0)
            {
                return (int)DeleteQRRecordErrorCode.CORRUPTED_FILE;
            }

            //Check for correct QR ID or QR Name
            int QRID = CheckQRID_QRName(QRID_QRName, xmlDoc);
            if (QRID < 0){
                return QRID;
            }

            XmlElement xmlEl = xmlDoc.DocumentElement;

            //Delete record, save changes
            xmlEl.RemoveChild(xmlEl.ChildNodes[_QRIDExist.IndexOf(QRID)]);
            xmlDoc.Save(_xmlFileName);
            return QRID;
        }

        public enum AddQRRecordErrorCode
        {
            INCORRECT_PARAMETER = -1,
            CORRUPTED_FILE = -2,
            QRID_INCORRECT = -3,
            NAME_OCCUPIED = -4
        }
        /// <summary>
        ///     Add QR recrod in xml file
        /// </summary>
        /// <param name="QRID">ID of QR record to add</param>
        /// <param name="QRName">Name of QR record to add</param>
        /// <param name="x">Coordinate x</param>
        /// <param name="y">Coordinate y</param>
        /// <returns>
        ///     <list type="table">
        ///         <listheader>
        ///             <term>
        ///                 >= 0 - no errors, else - error
        ///             </term>
        ///         </listheader>
        ///     <item>-1 (INCORRECT_PARAMET): Incorrect one of received parameters</item>
        ///     <item>-2 (CORRUPTED_FILE): Xml file not exists or was corrupted</item>
        ///     <item>-3 (QRID_INCORRECT): QR ID incorrect (0 > QRID) or alredy exist</item>
        ///     <item>-4 (NAME_OCCUPIED): Name of QR record already occupied</item>
        ///     </list>
        /// </returns>
        public int AddQRRecord(string QRID, string QRName, string x, string y)
        {
            //Check for correct input parametrs
            try
            {
                x = x.Replace(',', '.');
                y = y.Replace(',', '.');
                double.Parse(x, System.Globalization.CultureInfo.InvariantCulture);
                double.Parse(y, System.Globalization.CultureInfo.InvariantCulture);
            }
            catch
            {
                return (int)AddQRRecordErrorCode.INCORRECT_PARAMETER;
            }

            XmlDocument xmlDoc = new XmlDocument();

            //Check for correct file content
            if (CheckXmlFileContent(ref xmlDoc) < 0)
            {
                return (int)AddQRRecordErrorCode.CORRUPTED_FILE;
            }

            //if QRID is empty then generate it 
            int iQRID = -1;
            if (!Int32.TryParse(QRID, out iQRID))
            {
                if (QRID == "")
                {
                    iQRID = 0;
                    while (_QRIDExist.Contains(iQRID))
                    {
                        iQRID++;
                    }
                }
            }

            //Check for correct QRID
            if (iQRID < 0 || _QRIDExist.Contains(iQRID))
            {
                return (int)AddQRRecordErrorCode.QRID_INCORRECT;
            }

            XmlElement xmlRoot = xmlDoc.DocumentElement;

            //Check for existing name
            Boolean isExist = false;
            int i = 0;
            while (i < xmlRoot.ChildNodes.Count && !isExist)
            {
                if (xmlRoot.ChildNodes[i].Attributes[1].Value == QRName)
                {
                    isExist = true;
                }
                i++;
            }
            if (isExist)
            {
                return (int)AddQRRecordErrorCode.NAME_OCCUPIED;
            }
            if (!(!Int32.TryParse(QRName, out i) && QRName != null && QRName != "" )){
                return (int)AddQRRecordErrorCode.INCORRECT_PARAMETER;
            }

            //Create new xml element, fill it, save changes
            XmlElement QRCode = xmlDoc.CreateElement("QRCode");
            QRCode.SetAttribute("id", iQRID.ToString());
            QRCode.SetAttribute("name", QRName);
            XmlElement xmlX = xmlDoc.CreateElement("x");
            xmlX.InnerText = x;
            XmlElement xmlY = xmlDoc.CreateElement("y");
            xmlY.InnerText = y;
            QRCode.AppendChild(xmlX);
            QRCode.AppendChild(xmlY);
            xmlRoot.AppendChild(QRCode);
            xmlDoc.Save(_xmlFileName);

            return 0;
        }

        public enum ChangeQRRecordErrorCode
        {
            INCORRECT_PARAMETR = -1,
            CORRUPTED_FILE = -2,
            QRID_INCORRECT = -3,
            NAME_IS_OCCUPIED = -4,
            NAME_NOT_FOUND = -5
        }
        /// <summary>
        ///     Chade QR record in xml file according received ID or Name
        /// </summary>
        /// <param name="oldQRID_oldName">ID of QR record in xml file</param>
        /// <param name="newQRID">New QR ID to change</param>
        /// <param name="newQRName">New QR name to change</param>
        /// <param name="newX">Coordinate x</param>
        /// <param name="newY">Coordinate y</param>
        /// <returns>
        ///     <list type="table">
        ///         <listheader>
        ///             <term>
        ///                 >= 0 - no errors, else - error
        ///             </term>
        ///         </listheader>
        ///     <item>-1 (INCORRECT_PARAMETR): Incorrect one of received parameters</item>
        ///     <item>-2 (CORRUPTED_FILE): Xml file not exists or was corrupted</item>
        ///     <item>-3 (QRID_INCORRECT): QR ID incorrect (0 > QRID) or alredy exist</item>
        ///     <item>-4 (NAME_OCCUPIED): Name of QR record already occupied</item>
        ///     <item>-5 (NAME_NOT_FOUND): name of QR record is not exists</item>
        ///     </list>
        /// </returns>
        public int ChangeQRRecord(string oldQRID_oldName, string newQRID, string newQRName, string newX, string newY)
        {
            XmlDocument xmlDoc = new XmlDocument();

            //Try to delete old QR record
            int QRID = DeleteQRRecord(oldQRID_oldName);  
            if (QRID < 0){
                return (int)ChangeQRRecordErrorCode.INCORRECT_PARAMETR;
            }

            //Try to add new QR record
            int iResult = AddQRRecord(newQRID, newQRName, newX, newY); 
            if (iResult < 0){
                return iResult;
            }
            
            return 0;
        }

        public enum GetQrRecordListErrorCode
        {
            CORRUPTED_FILE = -1,
            PARCE_TO_DOUBLE_ERROR = -2
        }
        /// <summary>
        ///     Chade QR record in xml file according received ID or Name
        /// </summary>
        /// <param name="oldQRID_oldName">ID of QR record in xml file</param>
        /// <param name="newQRID">New QR ID to change</param>
        /// <param name="newQRName">New QR name to change</param>
        /// <param name="newX">Coordinate x</param>
        /// <param name="newY">Coordinate y</param>
        /// <returns>
        ///     <list type="table">
        ///         <listheader>
        ///             <term>
        ///                 >= 0 - no errors, else - error
        ///             </term>
        ///         </listheader>
        ///     <item>-1 (INCORRECT_PARAMETR): Incorrect one of received parameters</item>
        ///     <item>-2 (CORRUPTED_FILE): Xml file not exists or was corrupted</item>
        ///     <item>-3 (QRID_INCORRECT): QR ID incorrect (0 > QRID) or alredy exist</item>
        ///     <item>-4 (NAME_OCCUPIED): Name of QR record already occupied</item>
        ///     <item>-5 (NAME_NOT_FOUND): name of QR record is not exists</item>
        ///     </list>
        /// </returns>
        public int GetQRRecordList(ref QRModelXmlContent[] xmlContent)
        {
            XmlDocument xmlDoc = new XmlDocument();

            //Check for correct file content
            if (CheckXmlFileContent(ref xmlDoc) < 0)
            {
                return (int)GetQrRecordListErrorCode.CORRUPTED_FILE;
            }

            XmlElement xmlRoot = xmlDoc.DocumentElement;

            //Prepare array
            if (xmlContent == null)
            {
                xmlContent = new QRModelXmlContent[xmlRoot.ChildNodes.Count];
            }
            if (xmlContent.Length != xmlRoot.ChildNodes.Count)
            {
                Array.Resize(ref xmlContent, xmlRoot.ChildNodes.Count);
            }

            for (int i = 0; i < xmlRoot.ChildNodes.Count; i++)
            {
                XmlNode xmlNode = xmlRoot.ChildNodes[i];

                //Get atributes QRID, QRName
                xmlContent[i].QRID = xmlNode.Attributes[0].Value;
                xmlContent[i].QRName = xmlNode.Attributes[1].Value;

                //Get Coordinate x and y
                try
                {
                    xmlContent[i].X = double.Parse(xmlNode.ChildNodes[0].InnerText, System.Globalization.CultureInfo.InvariantCulture).ToString(System.Globalization.CultureInfo.InvariantCulture);
                    xmlContent[i].Y = double.Parse(xmlNode.ChildNodes[1].InnerText, System.Globalization.CultureInfo.InvariantCulture).ToString(System.Globalization.CultureInfo.InvariantCulture);
                }
                catch
                {
                    return (int)GetQRCoordErrorCode.PARSE_TO_DOUBLE_ERROR;
                }
            }
            return 0;
        }

        public enum GenerateQRCodeErrorCode{
            CORRUPTED_FILE = -1,
            CODEC_NOT_FOUND = -2,
            GENERATE_QR_ERROR = -3,
            WRITE_FILE_ERROR = -4
        }
        /// <summary>
        ///     Generate QR according recived QRID or QRName and dafault data
        /// </summary>
        /// <param name="QRID_QRName">ID or Name of QR record in xml file</param>
        /// <returns>
        ///     <list type="table">
        ///         <listheader>
        ///             <term>
        ///                 >= 0 - no errors, else - error
        ///             </term>
        ///         </listheader>
        ///     <item>-1 (CORRUPTED_FILE): Xml file does not exist or was corrupted</item>
        ///     <item>-2 (CODEC_NOT_FOUND): Cannot find codec to convert to jpeg</item>
        ///     <item>-3 (GENERATE_QR_ERROR): Error in "using QRCoder" or xml incoming data</item>
        ///     <item>-4 (WRITE_FILE_ERROR): Error in writing file (no such directory, can't open, 
        ///     invalid name, can't write to file, smth with bitmap or encoding... )</item>
        ///     </list>
        /// </returns>
        public int GenerateQR(string QRID_QRName)
        {
            XmlDocument xmlDoc = new XmlDocument();

            //Check for correct file content
            if (CheckXmlFileContent(ref xmlDoc) < 0){
                return (int)GenerateQRCodeErrorCode.CORRUPTED_FILE;
            }            

            //Check for correct QR ID or QR Name
            int QRID = CheckQRID_QRName(QRID_QRName, xmlDoc);
            if (QRID < 0){
                return QRID;
            }

            XmlElement xmlEl = xmlDoc.DocumentElement;
            XmlNode xmlNode = xmlEl.ChildNodes[_QRIDExist.IndexOf(QRID)];

            //Create QR code data
            string QRData = _defaultQRCodeData + "\n" +
                            xmlNode.ChildNodes[0].InnerText + "\n" + 
                            xmlNode.ChildNodes[1].InnerText;

            //Create QR code file name
            string QRFileName = _workQRDir + "/" + xmlNode.Attributes[1].Value + ".jpeg";

            //cdr - coder
            Bitmap QRBmp;
            try{
    
                //Generate QR code
                QRCodeGenerator cdrQRCodeGen = new QRCodeGenerator();
                QRCodeData cdrQRData = cdrQRCodeGen.CreateQrCode(QRData, QRCodeGenerator.ECCLevel.L, true);
                QRCode cdrQRCode = new QRCode(cdrQRData);
                QRBmp = cdrQRCode.GetGraphic(_pixelsPerModule);
            }catch{
                return (int)GenerateQRCodeErrorCode.GENERATE_QR_ERROR;
            }

            //Finding jpeg codec info
            ImageCodecInfo[] imgsCodecInfo = ImageCodecInfo.GetImageEncoders(); 
            int codecID = -1;
            for (int i = 0; i < imgsCodecInfo.Length; i++){
                if (imgsCodecInfo[i].FormatDescription == "JPEG"){
                    codecID = i;
                }
            }
            
            //Codec not found
            if (codecID == -1){
                return (int)GenerateQRCodeErrorCode.CODEC_NOT_FOUND;
            }

            try{

            //Save Bmp as jpeg to file
                System.Drawing.Imaging.Encoder QREncoder = System.Drawing.Imaging.Encoder.Quality;
                EncoderParameters QREncoderParameters = new EncoderParameters(1);
                QREncoderParameters.Param[0] = new EncoderParameter(QREncoder, 25L);
                QRBmp.Save(QRFileName, imgsCodecInfo[0], QREncoderParameters);
            }catch{
                return (int)GenerateQRCodeErrorCode.WRITE_FILE_ERROR;
            }
            return 0;
        }

        public enum GetQRImgNameErrorCode{
            CORRUPTED_FILE = -1,
            QRID_INCORRECT = -2,
            NAME_NOT_FOUND = -3,
            FILE_NOT_FOUND = -4
        }
        /// <summary>
        ///     Retrun name of image file accroding received QR ID or QR Name
        /// </summary>
        /// <param name="QRID_QRName">ID or Name of QR record in xml file</param>
        /// <param name="QRImgFileName">Name of image file</param>
        /// <returns>
        ///     <list type="table">
        ///         <listheader>
        ///             <term>
        ///                 >= 0 - no errors, else - error
        ///             </term>
        ///         </listheader>
        ///     <item>-1 (CORRUPTED_FILE): Xml file does not exist or was corrupted</item>
        ///     <item>-2 (QRID_INCORRECT): No this ID in xml file</item>
        ///     <item>-3 (NAME_NOT_FOUD): No this Name in xml file</item>
        ///     <item>-4 (FILE_NOT_FOUND): File with this name was not found</item>
        ///     </list>
        /// </returns>
        public int GetQRImgName(string QRID_QRName, ref string QRImgFileName)
        {
            XmlDocument xmlDoc = new XmlDocument();

            //Check for correct file content
            if (CheckXmlFileContent(ref xmlDoc) < 0){
                return (int)GetQRImgNameErrorCode.CORRUPTED_FILE;
            }            

            //Check for correct QR ID or QR Name
            int QRID = CheckQRID_QRName(QRID_QRName, xmlDoc);
            if (QRID < 0){
                return QRID;
            }

            XmlElement xmlEl = xmlDoc.DocumentElement;
            XmlNode xmlNode = xmlEl.ChildNodes[_QRIDExist.IndexOf(QRID)];

            //Check is directory exist
            if (!Directory.Exists(_workQRDir)){
                return (int)GetQRImgNameErrorCode.FILE_NOT_FOUND;
            }
            
            //Create potentional file name
            QRImgFileName = _workQRDir + "/" + xmlNode.Attributes[1].Value + ".jpeg";
            if (!File.Exists(QRImgFileName)){
                return (int)GetQRImgNameErrorCode.FILE_NOT_FOUND;
            }

            return 0;
        }

        private enum CheckXmlFileContentErrorCode
        {
            IS_EMPTY = 1,
            READ_FILE_ERROR = -1,
            UNKNOWN_ROOT_TAG = -2,
            UNKNOWN_ELEMENT_LV1_TAG = -3,
            INCORRECT_ATRIBUTES_LV1 = -4,
            INCORRECT_ELEMENT_LV2 = -5
        }
        /// <summary>
        ///     Check xml file for incrrect content
        /// </summary>
        /// <param name="xmlDoc">Xml document to open xml file</param>
        /// <returns>
        ///     <list type="table">
        ///         <listheader>
        ///             <term>
        ///                 >= 0 - no errors, else - error
        ///             </term>
        ///         </listheader>
        ///     <itme> 1 (IS_EMPTY): xml document is empty</itme>
        ///     <item>-1 (READ_FILE_ERROR): Error on read file (cannot find, incorrect name, cannot read, incorrect directory...)</item>
        ///     <item>-2 (UNKNOWN_ROOT_TAG): Xml file contain unknown root tag (Known root tags: "QRCodes")</item>
        ///     <item>-3 (UNKNOWN_ELEMENT_LV1_TAG): Xml file contain unknown element tag on lvl 1 (Known lvl 1 tags: "QRCode")</item>
        ///     <item>-4 (INCORRECT_ATRIBUTES_LV1): Xml file contain incorrect atributes on lvl 1 (Correct artributes: "id", "name")</item>
        ///     <item>-5 (INCORRECT_ELEMENT_LV2): Xml file contain incorrect element on lvl 2 (Correct lvl 2 tags: "x", "y"; Correct lvl 2 innrer text type: double)</item>
        ///     </list>
        /// </returns>
        private int CheckXmlFileContent(ref XmlDocument xmlDoc)
        {
            //Check is this file exist
            try
            {
                xmlDoc.Load(_xmlFileName);
            }
            catch
            {
                return (int)CheckXmlFileContentErrorCode.READ_FILE_ERROR;
            }

            XmlElement xmlEl = xmlDoc.DocumentElement;

            //Check for root tag
            if (xmlEl.Name != "QRCodes")
            {
                return (int)CheckXmlFileContentErrorCode.UNKNOWN_ROOT_TAG;
            }

            //Initialize dictionnary
            _QRIDExist.Clear();

            if (xmlEl.ChildNodes.Count == 0)
            {
                return (int)CheckXmlFileContentErrorCode.IS_EMPTY;
            }

            //List for check unuque attribute name
            List<string> QRNameList = new List<string>();

            //For every lvl 1 tag:
            foreach (XmlNode xmlNode in xmlEl)
            {

                //Check tag
                if (xmlNode.Name != "QRCode")
                {
                    return (int)CheckXmlFileContentErrorCode.UNKNOWN_ELEMENT_LV1_TAG;
                }

                if (!(xmlNode.Attributes.Count == 2 &&          //Chekc amount of atributes
                     xmlNode.Attributes[0].Name == "id" &&      //Check exist atribure "id"
                     xmlNode.Attributes[1].Name == "name"))     //Check exist atribute "name"
                {    
                    return (int)CheckXmlFileContentErrorCode.INCORRECT_ATRIBUTES_LV1;
                }

                //Check ID value
                int QRID = 0;
                if (!Int32.TryParse(xmlNode.Attributes[0].Value, out QRID))
                {
                    return (int)CheckXmlFileContentErrorCode.INCORRECT_ATRIBUTES_LV1;
                }
                if (QRID < 0 || _QRIDExist.Contains(QRID))
                {
                    return (int)CheckXmlFileContentErrorCode.INCORRECT_ATRIBUTES_LV1;
                }
                _QRIDExist.Add(QRID);

                //Check unique name
                if (QRNameList.Contains(xmlNode.Attributes[1].Value))
                {
                    return (int)CheckXmlFileContentErrorCode.INCORRECT_ATRIBUTES_LV1;
                }

                //Check that name is not any number
                int iName;
                if (Int32.TryParse(xmlNode.Attributes[1].Value, out iName)){
                    return (int)CheckXmlFileContentErrorCode.INCORRECT_ATRIBUTES_LV1;
                }
                QRNameList.Add(xmlNode.Attributes[1].Value);
                
                //Check amount of lvl 2 tags in lvl 1 
                if (!(xmlNode.ChildNodes.Count == 2 &&
                      xmlNode.ChildNodes[0].Name == "x" &&
                      xmlNode.ChildNodes[1].Name == "y" &&
                      !xmlNode.ChildNodes[0].InnerText.Contains(',') &&
                      !xmlNode.ChildNodes[1].InnerText.Contains(',')))
                {
                    return (int)CheckXmlFileContentErrorCode.INCORRECT_ELEMENT_LV2;
                }

                //Check "value" of lvl 2 tags
                try
                {
                    double.Parse(xmlNode.ChildNodes[0].InnerText, System.Globalization.CultureInfo.InvariantCulture);
                    double.Parse(xmlNode.ChildNodes[1].InnerText, System.Globalization.CultureInfo.InvariantCulture);
                }
                catch
                {
                    return (int)CheckXmlFileContentErrorCode.INCORRECT_ELEMENT_LV2;
                }

            }
            return 0;
        }

        /// <summary>
        ///     Change _xmlFileName to default value and check it for errors
        /// </summary>
        /// <returns>
        ///     <list type="table">
        ///         <listheader>
        ///             <term>
        ///                 >= 0 - no errors, else - error
        ///             </term>
        ///         </listheader>
        ///     </list>
        /// </returns>
        private int CreateNessaryFiles()
        {
            XmlDocument xmlDoc = new XmlDocument();

            //Check is default directory exist
            if (!Directory.Exists(_defaultDir))
            {
                Directory.CreateDirectory(_defaultDir);
            }
            if (!Directory.Exists(_workQRDir)){
                Directory.CreateDirectory(_workQRDir);
            }

            //Try to open _xmlFileName
            try
            {
                //Try to load default file
                xmlDoc.Load(_xmlFileName);
                
            }
            catch
            {

                //Create and save default file if it cannot be reads
                XmlDeclaration XmlDec = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
                xmlDoc.AppendChild(XmlDec);
                XmlComment XmlCom = xmlDoc.CreateComment("QRData here");
                xmlDoc.AppendChild(XmlCom);
                XmlElement QRCodes = xmlDoc.CreateElement("QRCodes");
                xmlDoc.AppendChild(QRCodes);
                xmlDoc.Save(_xmlFileName);
            }
            return 0;
        }

        private enum CheckQRID_QRNameErrorCode{
            QRID_INCORRECT = -2,
            NAME_NOT_FOUND = -3
        }
        /// <summary>
        ///     Check is QRID_QRName ID or Name that exest in xml file
        /// </summary>
        /// <param name="QRID_QRName">>ID or Name of QR record in xml file</param>
        /// <param name="xmlDoc">Open xml file to work with</param>
        /// <returns>
        ///     <list type="table">
        ///         <listheader>
        ///             <term>
        ///                 >= 0 - ID of element in xml file (no errors), else - error
        ///             </term>
        ///         </listheader>
        ///         <item>-2 (QRID_INCORRECT): No this ID in xml file</item>
        ///         <item>-3 (NAME_NOT_FOUD): No this Name in xml file</item>
        ///     </list>
        /// </returns>
        private int CheckQRID_QRName(string QRID_QRName, XmlDocument xmlDoc)
        {
            XmlElement xmlEl = xmlDoc.DocumentElement;

            //Check contains of QRID_QRName
            int QRID;
            if (Int32.TryParse(QRID_QRName, out QRID)){
            
                //QRID_QRName contain id
                //Check if QRID exists if QRID correct
                if (QRID < 0 || !_QRIDExist.Contains(QRID)){
                    return (int)CheckQRID_QRNameErrorCode.QRID_INCORRECT;
                }
            }else{
            
                //QRID_QRName contain name
                //Check is xml file contain Name
                QRID = -1;
                foreach (XmlNode xmlNode in xmlEl.ChildNodes){
                    if (xmlNode.Attributes[1].Value == QRID_QRName){
                        if (!Int32.TryParse(xmlNode.Attributes[0].Value, out QRID)){
                            return (int)CheckQRID_QRNameErrorCode.QRID_INCORRECT;
                        }
                    }
                }

                //QRID == -1 => not changed => QRName not found
                if (QRID == -1){
                    return (int)CheckQRID_QRNameErrorCode.NAME_NOT_FOUND;
                }
            }
            return QRID;
        }
    }
}