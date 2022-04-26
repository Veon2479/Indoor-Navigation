using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Server
{
    internal class WIFISpotModel
    {
        //Struct that contain information about one record in xml file
        public struct WIFISpotModelXmlContent
        {
            public string WIFISpotID;
            public string WIFISpotName;
            public string WIFISpotPower;
            public string WIFISpotMAC;
            public string X;
            public string Y;
        }

        //Name of xml file to work with
        internal string _xmlFileName { get; private set; } = "";

        //Default directory that contains private directories
        protected const string _defaultDir = "config";

        //Default xml file name
        protected const string _defaultName = "DefaultWIFISpotData.xml";

        //WIFI MAC address length
        //MAC - 6 oct. by 2 hex values and 5 smb '-'
        protected const int _WIFISpotMACLen = 17; 

        //List that contain exicting QRID
        List<int> _WIFISpotIDExist = new List<int>();

        public WIFISpotModel(string xmlFileName = "")
        {
            _xmlFileName = xmlFileName;
            XmlDocument xmlDocument = new XmlDocument();


            //Check xml document
            int iResult = CheckXmlFileContent(ref xmlDocument);
            if (iResult == (int)CheckXmlFileContentErrorCode.READ_FILE_ERROR)
            {

                if (_xmlFileName == "")
                {
    
                    //Work with default file name if name is empty
                    _xmlFileName = _defaultDir + "\\" + _defaultName;
                }

                //Check for correct extension
                string ext = _xmlFileName.Remove(0, _xmlFileName.LastIndexOf(".")+1);
                if (ext != "xml"){
                    throw new Exception ("Incorrect file name");
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

        private enum CheckXmlFileContentErrorCode
        {
            IS_EMPTY = 1,
            READ_FILE_ERROR = -1,
            UNKNOWN_ROOT_TAG = -2,
            UNKNOWN_ELEMENT_LV1_TAG = -3,
            INCORRECT_ATRIBUTES_LV1 = -4,
            INCORRECT_ELEMENT_LV2 = -5
        }

        public enum DeleteWIFISpotRecordErrorCode
        {
            CORRUPTED_FILE = -1,
            QRID_INCORRECT = -2,
            NAME_NOT_FOUND = -3
        }
        public int DeleteWIFISpotRecord(string WIFISpotID_WIFISpotName)
        {
             XmlDocument xmlDoc = new XmlDocument();

            //Check for correct file content
            if (CheckXmlFileContent(ref xmlDoc) < 0)
            {
                return (int)DeleteWIFISpotRecordErrorCode.CORRUPTED_FILE;
            }

            //Check for correct QR ID or QR Name
            int WIFISpotID = CheckWIFISpotID_WIFISpotName(WIFISpotID_WIFISpotName, xmlDoc);
            if (WIFISpotID < 0){
                return WIFISpotID;
            }

            XmlElement xmlEl = xmlDoc.DocumentElement;

            //Delete record, save changes
            xmlEl.RemoveChild(xmlEl.ChildNodes[_WIFISpotIDExist.IndexOf(WIFISpotID)]);
            xmlDoc.Save(_xmlFileName);
            return WIFISpotID;
        }

        public enum AddWIFISpotRecordErrorCode
        {
            INCORRECT_PARAMETER = -1,
            CORRUPTED_FILE = -2,
            QRID_INCORRECT = -3,
            NAME_OCCUPIED = -4
        }
        public int AddWIFISpotRecord(string WIFISpotID, string WIFISpotName, string x, string y, string power, string MACAddress)
        {
            //Check for correct input parametrs
            double dblPower = 0;
            try
            {
                x = x.Replace(',', '.');
                y = y.Replace(',', '.');
                power = power.Replace(',', '.');
                double.Parse(x, System.Globalization.CultureInfo.InvariantCulture);
                double.Parse(y, System.Globalization.CultureInfo.InvariantCulture);
                dblPower = double.Parse(power, System.Globalization.CultureInfo.InvariantCulture);
            }
            catch
            {
                return (int)AddWIFISpotRecordErrorCode.INCORRECT_PARAMETER;
            }

            //Check power (greater then 0) and MAC address
            if (dblPower <= 0 || !CheckMACAddress(MACAddress)){
                return (int)AddWIFISpotRecordErrorCode.INCORRECT_PARAMETER;                
            }
            XmlDocument xmlDoc = new XmlDocument();

            //Check for correct file content
            if (CheckXmlFileContent(ref xmlDoc) < 0)
            {
                return (int)AddWIFISpotRecordErrorCode.CORRUPTED_FILE;
            }

            //if QRID is empty then generate it 
            int iWIFISpotID = -1;
            if (!Int32.TryParse(WIFISpotID, out iWIFISpotID))
            {
                if (WIFISpotID == "")
                {
                    iWIFISpotID = 0;
                    while (_WIFISpotIDExist.Contains(iWIFISpotID))
                    {
                        iWIFISpotID++;
                    }
                }
            }

            //Check for correct QRID
            if (iWIFISpotID < 0 || _WIFISpotIDExist.Contains(iWIFISpotID))
            {
                return (int)AddWIFISpotRecordErrorCode.QRID_INCORRECT;
            }

            XmlElement xmlRoot = xmlDoc.DocumentElement;

            //Check for existing name
            Boolean isExist = false;
            int i = 0;
            while (i < xmlRoot.ChildNodes.Count && !isExist)
            {
                if (xmlRoot.ChildNodes[i].Attributes[1].Value == WIFISpotName)
                {
                    isExist = true;
                }
                i++;
            }
            if (isExist)
            {
                return (int)AddWIFISpotRecordErrorCode.NAME_OCCUPIED;
            }
            if (!(!Int32.TryParse(WIFISpotName, out i) && WIFISpotName != null && WIFISpotName != "" )){
                return (int)AddWIFISpotRecordErrorCode.INCORRECT_PARAMETER;
            }

            //Create new xml element, fill it, save changes
            XmlElement WIFISpot = xmlDoc.CreateElement("WIFISpot");
            WIFISpot.SetAttribute("id", iWIFISpotID.ToString(System.Globalization.CultureInfo.InvariantCulture));
            WIFISpot.SetAttribute("name", WIFISpotName);
            XmlElement xmlX = xmlDoc.CreateElement("x");
            xmlX.InnerText = x;
            XmlElement xmlY = xmlDoc.CreateElement("y");
            xmlY.InnerText = y;
            XmlElement xmlPower = xmlDoc.CreateElement("power");
            xmlPower.InnerText = power;
            XmlElement xmlMac = xmlDoc.CreateElement("mac");
            xmlMac.InnerText = MACAddress;
            WIFISpot.AppendChild(xmlX);
            WIFISpot.AppendChild(xmlY);
            WIFISpot.AppendChild(xmlPower);
            WIFISpot.AppendChild(xmlMac);
            xmlRoot.AppendChild(WIFISpot);
            xmlDoc.Save(_xmlFileName);

            return 0;
        }


        public enum ChangeWIFISpotRecordErrorCode
        {
            INCORRECT_PARAMETR = -1,
            CORRUPTED_FILE = -2,
            QRID_INCORRECT = -3,
            NAME_IS_OCCUPIED = -4,
            NAME_NOT_FOUND = -5
        }
        public int ChangeWIFISpotRecord(string oldQRID_oldName, string newQRID, string newQRName, string newX, string newY, 
                                        string newPower, string newMAC)
        {
            XmlDocument xmlDoc = new XmlDocument();

            //Try to delete old QR record
            int QRID = DeleteWIFISpotRecord(oldQRID_oldName);  
            if (QRID < 0){
                return (int)ChangeWIFISpotRecordErrorCode.INCORRECT_PARAMETR;
            }

            //Try to add new QR record
            int iResult = AddWIFISpotRecord(newQRID, newQRName, newX, newY, newPower, newMAC); 
            if (iResult < 0){
                return iResult;
            }
            
            return 0;
        }

        public enum GetWIFISpotRecordListErrorCode
        {
            CORRUPTED_FILE = -1,
            PARSE_TO_DOUBLE_ERROR = -2
        }
        public int GetWIFISpotRecordList(ref WIFISpotModelXmlContent[] xmlContent)
        {
            XmlDocument xmlDoc = new XmlDocument();

            //Check for correct file content
            if (CheckXmlFileContent(ref xmlDoc) < 0)
            {
                return (int)GetWIFISpotRecordListErrorCode.CORRUPTED_FILE;
            }

            XmlElement xmlRoot = xmlDoc.DocumentElement;

            //Prepare array
            if (xmlContent == null)
            {
                xmlContent = new WIFISpotModelXmlContent[xmlRoot.ChildNodes.Count];
            }
            if (xmlContent.Length != xmlRoot.ChildNodes.Count)
            {
                Array.Resize(ref xmlContent, xmlRoot.ChildNodes.Count);
            }

            for (int i = 0; i < xmlRoot.ChildNodes.Count; i++)
            {
                XmlNode xmlNode = xmlRoot.ChildNodes[i];

                //Get atributes QRID, QRName
                xmlContent[i].WIFISpotID = xmlNode.Attributes[0].Value;
                xmlContent[i].WIFISpotName = xmlNode.Attributes[1].Value;
                xmlContent[i].WIFISpotMAC = xmlNode.ChildNodes[2].Value;

                //Get Coordinate x and y
                try
                {
                    xmlContent[i].X = double.Parse(xmlNode.ChildNodes[0].InnerText, System.Globalization.CultureInfo.InvariantCulture).ToString(System.Globalization.CultureInfo.InvariantCulture);
                    xmlContent[i].Y = double.Parse(xmlNode.ChildNodes[1].InnerText, System.Globalization.CultureInfo.InvariantCulture).ToString(System.Globalization.CultureInfo.InvariantCulture);
                    xmlContent[i].WIFISpotPower = double.Parse(xmlNode.ChildNodes[2].InnerText, System.Globalization.CultureInfo.InvariantCulture).ToString(System.Globalization.CultureInfo.InvariantCulture);
                }
                catch
                {
                    return (int)GetWIFISpotRecordListErrorCode.PARSE_TO_DOUBLE_ERROR;
                }
            }
            return 0;
        }


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
            if (xmlEl.Name != "WIFISpots")
            {
                return (int)CheckXmlFileContentErrorCode.UNKNOWN_ROOT_TAG;
            }

            //Initialize dictionnary
            _WIFISpotIDExist.Clear();

            if (xmlEl.ChildNodes.Count == 0)
            {
                return (int)CheckXmlFileContentErrorCode.IS_EMPTY;
            }

            //List for check unuque attribute name
            List<string> WIFISpotNameList = new List<string>();

            //For every lvl 1 tag:
            foreach (XmlNode xmlNode in xmlEl)
            {

                //Check tag
                if (xmlNode.Name != "WIFISpot")
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
                int WIFISpotID = 0;
                if (!Int32.TryParse(xmlNode.Attributes[0].Value, out WIFISpotID))
                {
                    return (int)CheckXmlFileContentErrorCode.INCORRECT_ATRIBUTES_LV1;
                }
                if (WIFISpotID < 0 || _WIFISpotIDExist.Contains(WIFISpotID))
                {
                    return (int)CheckXmlFileContentErrorCode.INCORRECT_ATRIBUTES_LV1;
                }
                _WIFISpotIDExist.Add(WIFISpotID);

                //Check unique name
                if (WIFISpotNameList.Contains(xmlNode.Attributes[1].Value))
                {
                    return (int)CheckXmlFileContentErrorCode.INCORRECT_ATRIBUTES_LV1;
                }

                //Check that name is not any number
                int iName;
                if (Int32.TryParse(xmlNode.Attributes[1].Value, out iName)){
                    return (int)CheckXmlFileContentErrorCode.INCORRECT_ATRIBUTES_LV1;
                }
                WIFISpotNameList.Add(xmlNode.Attributes[1].Value);
                
                //Check amount of lvl 2 tags in lvl 1 
                if (!(xmlNode.ChildNodes.Count == 4 &&
                      xmlNode.ChildNodes[0].Name == "x" &&
                      xmlNode.ChildNodes[1].Name == "y" &&
                      xmlNode.ChildNodes[2].Name == "power" &&
                      xmlNode.ChildNodes[3].Name == "mac" &&
                      !xmlNode.ChildNodes[0].InnerText.Contains(',') &&
                      !xmlNode.ChildNodes[1].InnerText.Contains(',') &&
                      !xmlNode.ChildNodes[2].InnerText.Contains(',')))
                {
                    return (int)CheckXmlFileContentErrorCode.INCORRECT_ELEMENT_LV2;
                }

                //Check "value" of lvl 2 tags
                double dblPower = 0;
                try
                {
                    double.Parse(xmlNode.ChildNodes[0].InnerText, System.Globalization.CultureInfo.InvariantCulture);
                    double.Parse(xmlNode.ChildNodes[1].InnerText, System.Globalization.CultureInfo.InvariantCulture);
                    dblPower = double.Parse(xmlNode.ChildNodes[2].InnerText, System.Globalization.CultureInfo.InvariantCulture);
                }
                catch
                {
                    return (int)CheckXmlFileContentErrorCode.INCORRECT_ELEMENT_LV2;
                }

                //Power must be greater that zero
                if (dblPower <= 0){
                    return (int)CheckXmlFileContentErrorCode.INCORRECT_ELEMENT_LV2;
                }

                //Chaeck Correct MAC address
                Boolean isCorrectMAC = CheckMACAddress(xmlNode.ChildNodes[3].InnerText);
                if (!isCorrectMAC || xmlNode.ChildNodes[3].InnerText.Length != _WIFISpotMACLen){
                    return (int)CheckXmlFileContentErrorCode.INCORRECT_ELEMENT_LV2;
                }
            }
            return 0;
        }

        private int CreateNessaryFiles()
        {
            XmlDocument xmlDoc = new XmlDocument();

            if (!Directory.Exists(_defaultDir)){
                Directory.CreateDirectory(_defaultDir);
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
                XmlComment XmlCom = xmlDoc.CreateComment("WIFISpot data here");
                xmlDoc.AppendChild(XmlCom);
                XmlElement QRCodes = xmlDoc.CreateElement("WIFISpots");
                xmlDoc.AppendChild(QRCodes);
                xmlDoc.Save(_xmlFileName);
            }
            return 0;
        }

        private enum CheckWIFISpotID_WIFISpotNameErrorCode{
            QRID_INCORRECT = -2,
            NAME_NOT_FOUND = -3
        }
        private int CheckWIFISpotID_WIFISpotName(string WIFISpotID_WIFISpotName, XmlDocument xmlDoc)
        {
            XmlElement xmlEl = xmlDoc.DocumentElement;

            //Check contains of WIFISpotID_WIFISpotName
            int QRID;
            if (Int32.TryParse(WIFISpotID_WIFISpotName, out QRID)){
            
                //WIFISpotID_WIFISpotName contain id
                //Check if WIFISpotID exists if WIFISpotID correct
                if (QRID < 0 || !_WIFISpotIDExist.Contains(QRID)){
                    return (int)CheckWIFISpotID_WIFISpotNameErrorCode.QRID_INCORRECT;
                }
            }else{
            
                //WIFISpotID_WIFISpotName contain name
                //Check is xml file contain Name
                QRID = -1;
                foreach (XmlNode xmlNode in xmlEl.ChildNodes){
                    if (xmlNode.Attributes[1].Value == WIFISpotID_WIFISpotName){
                        if (!Int32.TryParse(xmlNode.Attributes[0].Value, out QRID)){
                            return (int)CheckWIFISpotID_WIFISpotNameErrorCode.QRID_INCORRECT;
                        }
                    }
                }

                //WIFISpotID_WIFISpotName == -1 => not changed => WIFISpotName not found
                if (QRID == -1){
                    return (int)CheckWIFISpotID_WIFISpotNameErrorCode.NAME_NOT_FOUND;
                }
            }
            return QRID;
        }

        private Boolean CheckMACAddress(string MACAddress)
        {
            Boolean isCorrectMAC = true;
            int state = 0, count = 0;
            foreach (char c in MACAddress){
                switch (state){
                    case 0: 
                        if (!(c >= '0' && c <= '9' || c >= 'A' && c <= 'F' )){
                            state = -1;
                         }
                       count ++;
                        if (count == 2){
                            state = 1;
                        }
                        break;
                    case 1:
                        if (c == '-'){
                            state = 0;
                            count = 0;
                        }else{
                            state = -1;
                        }
                        break;
                    default:
                        isCorrectMAC = false;
                        break;
                }
            }  
            return isCorrectMAC;
        }

    }
}
