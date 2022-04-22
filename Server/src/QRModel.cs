using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Server
{
    internal class QRModel
    {
        //Struct that contain information about one record in xml file
        public struct QRModelXmlContent{
            public string QRID;
            public string QRName;
            public string X;
            public string Y;
        }

        //Name of xml file to work with
        private string _xmlFileName = "";

        //Default directory that contains 
        protected const string _defaultDir = "XmlDocs";
        
        //Default xml file name
        protected const string _defaultName = "DefaultQRData.xml";
        
        //List that contain exicting QRID
        List<int> _QRIDExist = new List<int>();

        /// <summary>
        ///     Open and check xml file. If file does not exist use standart file in standart directory
        /// </summary>
        /// <param name="xmlFileName">Name of xml file to work with</param>
        /// <exception cref="Exception">Xml file incorrect or was corrupted</exception>
        public QRModel(string xmlFileName)
        {
            _xmlFileName = xmlFileName;
            XmlDocument xmlDocument = new XmlDocument();

            //Check xml document
            int iResult = CheckXmlFileContent(ref xmlDocument); 
            if (iResult == (int)CheckXmlFileContentErrorCode.READ_FILE_ERROR){
                
                //Change xml document to default
                UseDefaultXmlDoc();

                //Check default xml document 
                if (CheckXmlFileContent(ref xmlDocument) < 0){
                    throw new Exception("Incorrect default file or file was corrupted");    
            }
            }else if (iResult < 0){
                throw new Exception("Incorrect file or file was corrupted");
            }
        }

        public enum GetQRCoordErrorCode{
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
            if (CheckXmlFileContent(ref xmlDoc) < 0){
                return (int)GetQRCoordErrorCode.CORRUPTED_FILE;    
            }
            XmlElement xmlEl = xmlDoc.DocumentElement;
            
            //Check for correct QRID
            if (QRID < 0 || !_QRIDExist.Contains(QRID)){
                return (int)GetQRCoordErrorCode.QRID_INCORRECT;
            }

            XmlNode xmlNode = xmlEl.ChildNodes[QRID];

            //Try to parce string to double
            try{
                x = double.Parse(xmlNode.ChildNodes[0].InnerText, System.Globalization.CultureInfo.InvariantCulture);
                y = double.Parse(xmlNode.ChildNodes[1].InnerText, System.Globalization.CultureInfo.InvariantCulture);
            }catch{
                return (int)GetQRCoordErrorCode.PARSE_TO_DOUBLE_ERROR; 
            }
            return 0;
        }

        public enum DeleteQRRecordErrorCode{
            CORRUPTED_FILE = -1,
            QRID_INCORRECT = -2,
            NAME_NOT_FOUND = -3
        }
        /// <summary>
        ///     Delet QR Record from xml file according recived QRID
        /// </summary>
        /// <param name="QRID">ID of QR record in xml file</param>
        /// <returns>
        ///     <list type="table">
        ///         <listheader>
        ///             <term>
        ///                 >= 0 - no errors, else - error
        ///             </term>
        ///         </listheader>
        ///     <item>-1 (CORRUPTED_FILE): Xml file does not exist or was corrupted</item>
        ///     <item>-2 (QRID_INCORRECT): No this ID in xml file</item>
        ///     </list>
        /// </returns>
        public int DeleteQRRecord(int QRID)
        {
            XmlDocument xmlDoc = new XmlDocument();

            //Check for correct file content
            if (CheckXmlFileContent(ref xmlDoc) < 0){
                return (int)DeleteQRRecordErrorCode.CORRUPTED_FILE;    
            }

            XmlElement xmlEl = xmlDoc.DocumentElement;

            //Check for correct QRID
            if (QRID < 0 || !_QRIDExist.Contains(QRID)){
                return (int)DeleteQRRecordErrorCode.QRID_INCORRECT;
            }

            //Delete record, save changes
            xmlEl.RemoveChild(xmlEl.ChildNodes[_QRIDExist.IndexOf(QRID)]);
            xmlDoc.Save(_xmlFileName);
            return 0;
        }
        /// <summary>
        ///     Deler QR record from xlm file according recived record name
        /// </summary>
        /// <param name="QRname">Name of record in xml file</param>
        /// <returns>        
        ///     <list type="table">
        ///         <listheader>
        ///             <term>
        ///                 >= 0 - no errors, else - error
        ///             </term>
        ///         </listheader>
        ///     <item>-1 (CORRUPTED_FILE): Xml file does not exist or was corrupted</item>
        ///     <item>-3 (NAME_NOT_FOUND): Record name not found</item>
        ///     </list>
        /// </returns>
        public int DeleteQRRecord(string QRname)
        {
            XmlDocument xmlDoc = new XmlDocument();

            //Check for correct file content
            if (CheckXmlFileContent(ref xmlDoc) < 0){
                return (int)DeleteQRRecordErrorCode.CORRUPTED_FILE;    
            }

            XmlElement xmlEl = xmlDoc.DocumentElement;

            //Check is name exist
            int ID = -1;
            Boolean find = false;
            while (ID+1 < xmlEl.ChildNodes.Count && !find){
                ID++;
                if (xmlEl.ChildNodes[ID].Attributes[1].Value == QRname){
                    find = true;
                }
            }
            if (!find){
                return (int)DeleteQRRecordErrorCode.NAME_NOT_FOUND;
            }

            //Delete record, save changes
            xmlEl.RemoveChild(xmlEl.ChildNodes[ID]);
            xmlDoc.Save(_xmlFileName);
            return 0;
        }

        public enum AddQRRecordErrorCode{
            INCORRECT_PARAMETER = -1,
            CORRUPTED_FILE = -2,
            QRID_INCORRECT = -3,
            NAME_OCCUPIED = -4
        }
        /// <summary>
        ///     Add QR recrod in xml file
        /// </summary>
        /// <param name="QRID">ID of QR record to add</param>
        /// <param name="QRname">Name of QR record to add</param>
        /// <param name="x">Coordinate x</param>
        /// <param name="y">Coordinate y</param>
        /// <returns>
        ///     <list type="table">
        ///         <listheader>
        ///             <term>
        ///                 >= 0 - no errors, else - error
        ///             </term>
        ///         </listheader>
        ///     <item>-1 (INCORRECT_PARAMET): Incorrect QRID or x or y</item>
        ///     <item>-2 (CORRUPTED_FILE): Xml file not exists or was corrupted</item>
        ///     <item>-3 (QRID_INCORRECT): QR ID incerrect (QRID < 0) or alredy exist</item>
        ///     <item>-4 (NAME_OCCUPIED): Name of QR record already occupied</item>
        ///     </list>
        /// </returns>
        public int AddQRRecord(string QRID, string QRname, string x, string y)
        {
            //Check for correct input parametrs
            try{
                double.Parse(x, System.Globalization.CultureInfo.InvariantCulture);
                double.Parse(y, System.Globalization.CultureInfo.InvariantCulture);
            }catch{
                return (int)AddQRRecordErrorCode.INCORRECT_PARAMETER;
            }

            XmlDocument xmlDoc = new XmlDocument();

            //Check for correct file content
            if (CheckXmlFileContent(ref xmlDoc) < 0){
                return (int)AddQRRecordErrorCode.CORRUPTED_FILE;    
            }
            
            //if QRID is empty then generate it 
            int iQRID = -1;
            if (!Int32.TryParse(QRID, out iQRID)){
                if (QRID == ""){
                    iQRID = 0;
                    while(_QRIDExist.Contains(iQRID)){
                        iQRID++;
                    }
                }
            }

            //Check for correct QRID
            if (iQRID < 0 || _QRIDExist.Contains(iQRID)){
                return (int)AddQRRecordErrorCode.QRID_INCORRECT;
            }
            
            XmlElement xmlRoot = xmlDoc.DocumentElement;

            //Check for existing name
            Boolean isExist = false;
            int i = 0;
            while (i < xmlRoot.ChildNodes.Count && !isExist){
                if (xmlRoot.ChildNodes[i].Attributes[1].Value == QRname){
                    isExist = true;
                }
                i++;
            }
            if (isExist){
                return (int)AddQRRecordErrorCode.NAME_OCCUPIED;
            }

            //Create new xml element, fill it, save changes
            XmlElement QRCode = xmlDoc.CreateElement("QRCode");
            QRCode.SetAttribute("id", iQRID.ToString());
            QRCode.SetAttribute("name", QRname);
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

        public enum ChangeQRRecordErrorCode{
            CORRUPTED_FILE = -1,
            QRID_INCORRECT = -2,
            NAME_INCORRECT = -3
        }
        
        /// <summary>
        ///     Chade QR record in xml file according received QRID
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
        ///     <item>-1 (CORRUPTED_FILE): Xml file not exists or was corrupted</item>
        ///     <item>-2 (INCORRECT_PARAMET): Incorrect QRID or x or y</item>
        ///     </list>
        /// </returns>
        public int ChangeQRRecord(int QRID, double x, double y)
        {
            XmlDocument xmlDoc = new XmlDocument();

            //Check for correct file content
            if (CheckXmlFileContent(ref xmlDoc) < 0){
                return (int)ChangeQRRecordErrorCode.CORRUPTED_FILE;    
            }
            
            //Check for correct QRID
            if (QRID < 0 || !_QRIDExist.Contains(QRID)){
                return (int)ChangeQRRecordErrorCode.QRID_INCORRECT;
            }

            //Get chosen xml element, change it data, save changes
            XmlElement xmlRoot = xmlDoc.DocumentElement;
            XmlNode QRCode = xmlRoot.ChildNodes[_QRIDExist.IndexOf(QRID)]; 
            QRCode.ChildNodes[0].InnerText = x.ToString(System.Globalization.CultureInfo.InvariantCulture);
            QRCode.ChildNodes[1].InnerText = y.ToString(System.Globalization.CultureInfo.InvariantCulture);
            xmlDoc.Save(_xmlFileName);
            return 0;
        }
        /// <summary>
        ///     Change QR record in xml file according received QR record name
        /// </summary>
        /// <param name="QRname">Name of QR recrod</param>
        /// <param name="x">Coordinate x</param>
        /// <param name="y">Coordinate y</param>
        /// <returns>
        ///     <list type="table">
        ///         <listheader>
        ///             <term>
        ///                 >= 0 - no errors, else - error
        ///             </term>
        ///         </listheader>
        ///     <item>-1 (CORRUPTED_FILE): Xml file not exists or was corrupted</item>
        ///     <item>-3 (NAME_INCORRECT): Incorrect QR name, or does not exist</item>
        ///     </list>
        ///</returns>
        public int ChangeQRRecord(string QRname, double x, double y)
        {
            XmlDocument xmlDoc = new XmlDocument();

            //Check for correct file content
            if (CheckXmlFileContent(ref xmlDoc) < 0){
                return (int)ChangeQRRecordErrorCode.CORRUPTED_FILE;    
            }

            XmlElement xmlEl = xmlDoc.DocumentElement;

            //Check is name exist
            int ID = -1;
            Boolean find = false;
            while (ID+1 < xmlEl.ChildNodes.Count && !find){
                ID++;
                if (xmlEl.ChildNodes[ID].Attributes[1].Value == QRname){
                    find = true;
                }
            }
            if (!find){
                return (int)ChangeQRRecordErrorCode.NAME_INCORRECT;
            }

            //Get chosen xml element, change it data, save changes
            XmlElement xmlRoot = xmlDoc.DocumentElement;
            XmlNode QRCode = xmlRoot.ChildNodes[ID]; 
            QRCode.ChildNodes[0].InnerText = x.ToString(System.Globalization.CultureInfo.InvariantCulture);
            QRCode.ChildNodes[1].InnerText = y.ToString(System.Globalization.CultureInfo.InvariantCulture);
            xmlDoc.Save(_xmlFileName);
            return 0;
        }

        public enum GetQrRecordListErrorCode{
            CORRUPTED_FILE = -1,
            PARCE_TO_DOUBLE_ERROR = -2
        }
        /// <summary>
        ///     Return array of records, that contain data from xml file
        /// </summary>
        /// <param name="xmlContent">Array to fill it with data</param>
        /// <returns>
        ///     <list type="table">
        ///         <listheader>
        ///             <term>
        ///                 >= 0 - no errors, else - error
        ///             </term>
        ///         </listheader>
        ///     <item>-1 (CORRUPTED_FILE): Xml file not exists or was corrupted</item>
        ///     <item>-2 (PARSE_TO_DOUBLE_ERROR): Error in parcing data from xml table to double</item>
        ///     </list>
        /// </returns>
        public int GetQRRecordList(ref QRModelXmlContent[] xmlContent)
        {
            XmlDocument xmlDoc = new XmlDocument();

            //Check for correct file content
            if (CheckXmlFileContent(ref xmlDoc) < 0){
                return (int)GetQrRecordListErrorCode.CORRUPTED_FILE;    
            }

            XmlElement xmlRoot = xmlDoc.DocumentElement;
            
            //Prepare array
            if (xmlContent == null){
                xmlContent = new QRModelXmlContent[xmlRoot.ChildNodes.Count];
            }
            if (xmlContent.Length != xmlRoot.ChildNodes.Count){
                Array.Resize(ref xmlContent, xmlRoot.ChildNodes.Count);
            }

            for (int i = 0; i < xmlRoot.ChildNodes.Count; i++){
                XmlNode xmlNode = xmlRoot.ChildNodes[i];
                
                //Get atributes QRID, QRName
                xmlContent[i].QRID = xmlNode.Attributes[0].Value;
                xmlContent[i].QRName = xmlNode.Attributes[1].Value;
                
                //Get Coordinate x and y
                try{
                    xmlContent[i].X = double.Parse(xmlNode.ChildNodes[0].InnerText, System.Globalization.CultureInfo.InvariantCulture).ToString();
                    xmlContent[i].Y = double.Parse(xmlNode.ChildNodes[1].InnerText, System.Globalization.CultureInfo.InvariantCulture).ToString();
                }catch{
                    return (int)GetQRCoordErrorCode.PARSE_TO_DOUBLE_ERROR; 
                }
            }
            return 0;
        }
        private enum CheckXmlFileContentErrorCode{
            IS_EMPTY = 1,
            READ_FILE_ERROR = -1,
            UNKNOWN_ROOT_TAG = -2,
            UNKNOWN_ELEMENT_LV1_TAG = -3,
            INCORRECT_ATRIBUTES_LV1 = -4,
            INCORRECT_NODE_LV2 = -5
        }
        private int CheckXmlFileContent(ref XmlDocument xmlDoc)
        {            
            //Check is this file exist
            try{
                xmlDoc.Load(_xmlFileName);
            }catch{
                return (int)CheckXmlFileContentErrorCode.READ_FILE_ERROR;
            }
            
            XmlElement xmlEl = xmlDoc.DocumentElement;

            //Check for root tag
            if (xmlEl.Name != "QRCodes"){
                return (int)CheckXmlFileContentErrorCode.UNKNOWN_ROOT_TAG;
            }

            //Initialize dictionnary
            _QRIDExist.Clear();

            if (xmlEl.ChildNodes.Count == 0){
                return (int)CheckXmlFileContentErrorCode.IS_EMPTY;
            }

            //For every lvl 1 tag:
            foreach(XmlNode xmlNode in xmlEl){
                
                //Check tag
                if(xmlNode.Name != "QRCode"){
                    return (int)CheckXmlFileContentErrorCode.UNKNOWN_ELEMENT_LV1_TAG; 
                }


                if(!(xmlNode.Attributes.Count == 2 &&           //Chekc amount of atributes
                     xmlNode.Attributes[0].Name == "id" &&      //Check exist atribure "id"
                     xmlNode.Attributes[1].Name == "name")){    //Check exist atribute "name"
                    return (int)CheckXmlFileContentErrorCode.INCORRECT_ATRIBUTES_LV1;
                }

                //Check ID value
                int QRID = 0;
                if (!Int32.TryParse(xmlNode.Attributes[0].Value, out QRID)){
                    return (int)CheckXmlFileContentErrorCode.INCORRECT_ATRIBUTES_LV1;
                }
                if (QRID < 0 || _QRIDExist.Contains(QRID)){
                    return (int)CheckXmlFileContentErrorCode.INCORRECT_ATRIBUTES_LV1;
                }
                _QRIDExist.Add(QRID);

                //Check amount of lvl 2 tags in lvl 1 tag
                if (!(xmlNode.ChildNodes.Count == 2) && 
                      xmlNode.ChildNodes[0].Name == "x" && 
                      xmlNode.ChildNodes[1].Name == "y"){
                    return (int)CheckXmlFileContentErrorCode.INCORRECT_NODE_LV2;
                }

                //Check "value" of lvl 2 tags
                try{
                    double.Parse(xmlNode.ChildNodes[0].InnerText, System.Globalization.CultureInfo.InvariantCulture);
                    double.Parse(xmlNode.ChildNodes[1].InnerText, System.Globalization.CultureInfo.InvariantCulture);
                }catch{
                    return (int)CheckXmlFileContentErrorCode.INCORRECT_NODE_LV2;
                }

            }
            return 0;
        }

        private int UseDefaultXmlDoc()
        {
            XmlDocument xmlDoc = new XmlDocument();

            //Change xmlFile name to default
            _xmlFileName = _defaultDir + "/" + _defaultName;
            try{

                //Try to load default file
                xmlDoc.Load(_xmlFileName);
            }catch{ 

                //Check is default directory exist
                if (!Directory.Exists(_defaultDir)){
                    Directory.CreateDirectory(_defaultDir);
                }

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
    }
}
