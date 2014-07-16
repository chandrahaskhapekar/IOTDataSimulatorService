using IoTDataSimulationService.Constant;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Schema;

namespace IoTDataSimulationService.Helper
{
    public class XSDValidator
    {
        public bool isValid = false;
        public static List<string> listOfErrors = new List<string>(); 
        public bool ValidateSchema(String infilename)
        {
            //this function will validate the schema file (xsd)
            XmlSchema schema;
            isValid = true; //make sure to reset the success var
            StreamReader streamReader = new StreamReader(infilename);
            try
            {
                schema = XmlSchema.Read(streamReader,
                    new ValidationEventHandler(ValidationCallBack));
                //This compile statement is what ususally catches the errors
                schema.Compile(new ValidationEventHandler(ValidationCallBack));
            }
            finally
            {
                streamReader.Close();
            }
            return isValid;
        }

        private void ValidationCallBack(Object sender, ValidationEventArgs args)
        {
            //This is only called on error.
            isValid = false; //Validation failed
            listOfErrors.Add(args.Exception.Message + Constants.ErrorLineNumber + args.Exception.LineNumber + Constants.ErrorPosition + args.Exception.LinePosition.ToString() + ".");
        }
    }
}