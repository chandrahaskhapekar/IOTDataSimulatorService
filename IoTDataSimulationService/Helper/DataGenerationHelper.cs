using IoTDataSimulationService.Constant;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

namespace IoTDataSimulationService.Helper
{
    public class DataGenerationHelper
    {
        public void StartSendingData(bool connectionStatus, Hashtable listOfURLs)
        {
            try
            {
                string json = string.Empty;
                while (connectionStatus.Equals(true))
                {
                    List<object> instances = GetInstances();
                    string urlForClass = string.Empty;
                    string className = string.Empty;

                    foreach (DictionaryEntry url in listOfURLs)
                    {
                        urlForClass = url.Value.ToString();
                        className = url.Key.ToString();
                        object instance = InstanceCreation.GetInstance(Constants.NamespaceName, className);
                        InsertRandomData(instance);
                        json = new JavaScriptSerializer().Serialize(instance);
                        Thread.Sleep(100);
                        HttpPostRequest(urlForClass, json);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string HttpPostRequest(string url, string jsonData)
        {
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                httpWebRequest.Method = Constants.POST;
                httpWebRequest.KeepAlive = false;
                byte[] data = Encoding.ASCII.GetBytes(jsonData);

                httpWebRequest.ContentType = Constants.JsonContentType;
                httpWebRequest.ContentLength = data.Length;

                Stream requestStream = httpWebRequest.GetRequestStream();
                requestStream.Write(data, 0, data.Length);
                requestStream.Close();

                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                Stream responseStream = httpWebResponse.GetResponseStream();

                StreamReader streamReader = new StreamReader(responseStream, Encoding.Default);

                string response = streamReader.ReadToEnd();

                streamReader.Close();
                responseStream.Close();

                httpWebResponse.Close();

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<object> GetInstances()
        {
            try
            {
                List<string> classes = InstanceCreation.GetClasses(Constants.NamespaceName);
                List<object> instanceList = new List<object>();

                foreach (string className in classes)
                {
                    if (!String.IsNullOrEmpty(className))
                    {
                        object instance = InstanceCreation.GetInstance(Constants.NamespaceName, className);

                        instanceList.Add(InsertRandomData(instance));
                    }
                }
                return instanceList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private object InsertRandomData(object instance)
        {
            try
            {
                List<PropertyInfo> properties = InstanceCreation.GetProperties(instance);

                foreach (PropertyInfo property in properties)
                {
                    string propertyName = property.Name;
                    string propertyType = property.PropertyType.ToString().Split('.')[1];
                    string className = string.Empty;

                    if (propertyType.Contains(Constants.Type))
                    {
                        className = propertyType;
                        propertyType = Constants.Class;
                    }

                    switch (propertyType)
                    {
                        case Constants.String:
                            property.SetValue(instance, RandomDataGeneration.RandomString(10));
                            break;
                        case Constants.Int32:
                            property.SetValue(instance, RandomDataGeneration.RandomNumbers());
                            break;
                        case Constants.Class:
                            object subInstance = InstanceCreation.GetInstance(Constants.NamespaceName, className);
                            property.SetValue(instance, InsertRandomData(subInstance));
                            break;
                    }
                }

                return instance;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}