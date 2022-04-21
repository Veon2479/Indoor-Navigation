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
        private string _xmlFileName = "";
        private const string _defaultDir = "XmlDocs";
        private const string _defaultName = "DefaultQRData.xml";
        List<int> _QRIDExist = new List<int>();

        public QRModel(string xmlFileName)
        {
            _xmlFileName = xmlFileName;
            XmlDocument xmlDocument = new XmlDocument();
            int iResult = CheckXmlFileContent(ref xmlDocument); 
            if (iResult == (int)CheckXmlFileContentErrorCode.READ_FILE_ERROR){
                UseDefaultXmlDoc();
            }else if (iResult < 0){
                throw new Exception("Incorrect file name or file was corrupted");
            }
        }

        public enum GetQRCoordErrorCode{
            CORRUPTED_FILE = -1,
            QRID_INCORRECT = -2,
            PARSE_TO_DOUBLE_ERROR = -3
        }
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

            xmlEl.RemoveChild(xmlEl.ChildNodes[_QRIDExist.IndexOf(QRID)]);
            xmlDoc.Save(_xmlFileName);
            return 0;
        }

        public int DeleteQRRecord(string name)
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
                if (xmlEl.ChildNodes[ID].Attributes[1].Value == name){
                    find = true;
                }
            }

            if (!find){
                return (int)DeleteQRRecordErrorCode.NAME_NOT_FOUND;
            }

            xmlEl.RemoveChild(xmlEl.ChildNodes[ID]);
            xmlDoc.Save(_xmlFileName);
            return 0;
        }

        public enum AddQRRecordErrorCode{
            INCORRECT_PARAMETER = -1,
            CORRUPTED_FILE = -2,
            QRID_INCORRECT = -3,
            NAME_OCCUPIED = -2
        }
        public int AddQRRecord(string QRID, string name, string x, string y)
        {
            int iQRID = 0;
            try{
                iQRID = Int32.Parse(QRID);
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
            
            //Check for correct QRID
            if (iQRID < 0 || _QRIDExist.Contains(iQRID)){
                return (int)AddQRRecordErrorCode.QRID_INCORRECT;
            }
            
            XmlElement xmlRoot = xmlDoc.DocumentElement;

            //Check for existing name
            Boolean isExist = false;
            int i = 0;
            while (i < xmlRoot.ChildNodes.Count && !isExist){
                if (xmlRoot.ChildNodes[i].Attributes[1].Value == name){
                    isExist = true;
                }
                i++;
            }
            if (isExist){
                return (int)AddQRRecordErrorCode.NAME_OCCUPIED;
            }

            
            XmlElement QRCode = xmlDoc.CreateElement("QRCode");
            QRCode.SetAttribute("id", QRID);
            QRCode.SetAttribute("name", name);
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

            XmlElement xmlRoot = xmlDoc.DocumentElement;
            XmlNode QRCode = xmlRoot.ChildNodes[_QRIDExist.IndexOf(QRID)]; 
            QRCode.ChildNodes[0].InnerText = x.ToString(System.Globalization.CultureInfo.InvariantCulture);
            QRCode.ChildNodes[1].InnerText = y.ToString(System.Globalization.CultureInfo.InvariantCulture);
            xmlDoc.Save(_xmlFileName);
            return 0;
        }

        public int ChangeQRRecord(string name, double x, double y)
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
                if (xmlEl.ChildNodes[ID].Attributes[1].Value == name){
                    find = true;
                }
            }
            if (!find){
                return (int)ChangeQRRecordErrorCode.NAME_INCORRECT;
            }

            XmlElement xmlRoot = xmlDoc.DocumentElement;
            XmlNode QRCode = xmlRoot.ChildNodes[ID]; 
            QRCode.ChildNodes[0].InnerText = x.ToString(System.Globalization.CultureInfo.InvariantCulture);
            QRCode.ChildNodes[1].InnerText = y.ToString(System.Globalization.CultureInfo.InvariantCulture);
            xmlDoc.Save(_xmlFileName);
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

                //Create default file if it cannot be reads
                if (!Directory.Exists(_defaultDir)){
                    Directory.CreateDirectory(_defaultDir);
                }
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
