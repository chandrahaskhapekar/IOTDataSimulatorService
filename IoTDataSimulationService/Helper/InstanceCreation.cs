using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace IoTDataSimulationService.Helper
{
    public class InstanceCreation
    {
        public InstanceCreation()
        {
        }

        public static List<string> GetClasses(string namespaceName)
        {
            try
            {
                Type[] types = Assembly.GetExecutingAssembly().GetTypes();
                List<string> classes = new List<string>();
                foreach (Type type in types)
                {
                    if (type.Namespace.Equals(namespaceName))
                        classes.Add(type.Name);
                }

                return classes;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public static List<string> GetAllParentClasses(string namespaceName)
        {
            try
            {
                List<string> listOfClasses = GetClasses(namespaceName);
                Type[] types = Assembly.GetExecutingAssembly().GetTypes();
                
                foreach (Type type in types)
                {
                    if (type.Namespace.Equals(namespaceName))
                    {
                        PropertyInfo[] properties = type.GetProperties();
                        foreach (var property in properties)
                        {
                            if (listOfClasses.Contains(property.PropertyType.Name))
                            {
                                listOfClasses.Remove(property.PropertyType.Name);
                            }
                        }

                    }

                }

                return listOfClasses;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static object GetInstance(string namespaceName, string className)
        {
            try
            {
                return Assembly.GetExecutingAssembly().CreateInstance(namespaceName + "." + className); ;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static List<PropertyInfo> GetProperties(object instance)
        {
            try
            {
                return instance.GetType().GetProperties().ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}