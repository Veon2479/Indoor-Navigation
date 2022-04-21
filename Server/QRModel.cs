using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Server
{
    internal class QRModel
    {
        private string _xmlDocName = "";

        public QRModel(string xmlDocName)
        {
            _xmlDocName = xmlDocName;
            //CheckFileContent
            //throw new Exception();
        }

        public enum GetQRCoordErrorCode{
            READ_FILE_ERROR = -1,
            QRID_INCORRECT = -2,
            PARSE_TO_DOUBLE_ERROR = -3
        }
        public int GetQRCoord(int QRID, ref double x, ref double y)
        {
            XmlDocument xmlDoc = new XmlDocument();
            
            //Check is this file exist
            try{
                xmlDoc.Load(this._xmlDocName);
            }catch{
                return (int)GetQRCoordErrorCode.READ_FILE_ERROR;
            }
            
            XmlElement xEl = xmlDoc.DocumentElement;
            
            //Check for correct QRID
            if (QRID < 0 || QRID > xEl.ChildNodes.Count-1){
                return (int)GetQRCoordErrorCode.QRID_INCORRECT;
            }

            XmlNode xNode = xEl.ChildNodes[QRID];

            //Try to parce string to double
            try{
                x = double.Parse(xNode.ChildNodes[0].InnerText, System.Globalization.CultureInfo.InvariantCulture);
                y = double.Parse(xNode.ChildNodes[1].InnerText, System.Globalization.CultureInfo.InvariantCulture);
            }catch{
                return (int)GetQRCoordErrorCode.PARSE_TO_DOUBLE_ERROR; 
            }
            return 0;
        }
    }
}
