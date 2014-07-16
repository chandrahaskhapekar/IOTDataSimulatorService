using IoTDataSimulationService.Constant;
using IoTDataSimulationService.Helper;
using IoTDataSimulationServiceBLL;
using IoTDataSimulationServiceBLL.DataLayer;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Execution;
using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace IoTDataSimulationService
{
    public partial class Home : System.Web.UI.Page
    {
        public Hashtable listOfURLs = new Hashtable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                _username.Text = HttpContext.Current.User.Identity.Name;
            }
            else
            {
                _username.Text = "Not logged in";
                FormsAuthentication.SetAuthCookie("CookieMan", true);
            }
        }
        protected void UploadButton_Click(object sender, EventArgs e)
        {
            try
            {

                string userId = HttpContext.Current.User.Identity.Name;

                if(!string.IsNullOrEmpty(txtNameOfSimulator.Text))
                {
                    // Store simulator name  
                    Session[Constants.SimulatorName] = txtNameOfSimulator.Text.Trim();
                    txtNameOfSimulator.BorderColor = System.Drawing.Color.DarkGray;
                    txtNameOfSimulator.ForeColor = System.Drawing.Color.Black;
                    txtNameOfSimulator.CssClass = Constants.Form_ControlClass;
                }
                else
                {
                    txtNameOfSimulator.Attributes.Add(Constants.PlaceholderAttribute, Constants.EmptySimulatorNameError);
                    txtNameOfSimulator.BorderColor = System.Drawing.Color.DarkRed;
                    txtNameOfSimulator.CssClass = Constants.AlertDanger;
                }
                if (FileUploadControl.HasFile && !string.IsNullOrEmpty(txtNameOfSimulator.Text))
                {
                    string fileName = Path.GetFileName(FileUploadControl.FileName);

                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(FileUploadControl.FileName);

                    string Extension = Path.GetExtension(FileUploadControl.PostedFile.FileName); /* this will give u the extension*/

                    if (Extension != Constants.XsdExtention)
                    {
                        fileName = string.Empty;
                        fileNameWithoutExtension = string.Empty;
                    }
                    else
                    {
                        FileUploadControl.SaveAs(Server.MapPath("~/") + fileName);

                        Helper.XSDValidator xsdValidatorHelper = new Helper.XSDValidator();
                       
                        //Try to validate the schema file
                        if (xsdValidatorHelper.ValidateSchema(Server.MapPath("~/") + fileName))
                        {
                            string fileToLoad = Server.MapPath("~/" + fileName);
                            FileStream fileStream = new FileStream(fileToLoad, FileMode.Open);

                            // Load the schema to process.
                            XmlSchema xsd = XmlSchema.Read(fileStream, null);

                            // Collection of schemas for the XmlSchemaImporter
                            XmlSchemas xsds = new XmlSchemas();
                            xsds.Add(xsd);
                            XmlSchemaImporter importer = new XmlSchemaImporter(xsds);

                            // System.CodeDom namespace for the XmlCodeExporter to put classes in
                            System.CodeDom.CodeNamespace codeNamespace = new System.CodeDom.CodeNamespace(Constants.NamespaceName);
                            XmlCodeExporter exporter = new XmlCodeExporter(codeNamespace);

                            // Iterate schema items (top-level elements only) and generate code for each
                            foreach (XmlSchemaObject item in xsd.Items)
                            {
                                if (item is XmlSchemaElement)
                                {
                                    // Import the mapping first
                                    XmlTypeMapping map = importer.ImportTypeMapping(
                                      new XmlQualifiedName(((XmlSchemaElement)item).Name,
                                      xsd.TargetNamespace));
                                    // Export the code finally
                                    exporter.ExportTypeMapping(map);
                                }
                            }

                            // Code generator to build code with.
                            ICodeGenerator generator = new CSharpCodeProvider().CreateGenerator();

                            // Generate untouched version
                            using (StreamWriter streamWriter = new StreamWriter(@"" + Server.MapPath("~/Files/") + fileNameWithoutExtension + Constants.CsExtention, false))
                            {
                                generator.GenerateCodeFromNamespace(codeNamespace, streamWriter, new CodeGeneratorOptions());
                                streamWriter.Flush();
                                streamWriter.Close();
                            }

                            //Clear content of Generated.cs file
                            System.IO.File.WriteAllText(@"" + Server.MapPath("~/") + Constants.NamespaceName + ".cs", string.Empty);


                            //Copy .cs file data in Generated.cs file
                            using (FileStream stream = File.OpenRead(@"" + Server.MapPath("~/Files/") + fileNameWithoutExtension + Constants.CsExtention))
                            using (FileStream writeStream = File.OpenWrite(@"" + Server.MapPath("~/") + Constants.NamespaceName + Constants.CsExtention))
                            {
                                BinaryReader reader = new BinaryReader(stream);
                                BinaryWriter writer = new BinaryWriter(writeStream);

                                // create a buffer to hold the bytes 
                                byte[] buffer = new Byte[1024];
                                int bytesRead;

                                // while the read method returns bytes
                                // keep writing them to the output stream
                                while ((bytesRead =
                                        stream.Read(buffer, 0, 1024)) > 0)
                                {
                                    writeStream.Write(buffer, 0, bytesRead);
                                }
                                reader.Close();
                                writer.Close();
                            }
                            CodeDomProvider codeProvider = CodeDomProvider.CreateProvider(Constants.CSharp);
                            string Output = Constants.OutExe;
                            System.CodeDom.Compiler.CompilerParameters parameters = new CompilerParameters();

                            //Make sure we generate an EXE, not a DLL
                            parameters.GenerateExecutable = true;
                            parameters.OutputAssembly = Output;

                            ProjectCollection projectCollection = new ProjectCollection();
                            projectCollection.SetGlobalProperty(Constants.Configuration, Constants.Release);
                            projectCollection.SetGlobalProperty(Constants.Platform, Constants.AnyCpu);
                            BuildManager.DefaultBuildManager.Build(new BuildParameters(projectCollection), new BuildRequestData(new ProjectInstance(Server.MapPath("~/") + Constants.CsprojFileName), new string[] { Constants.Rebuild }));

                            Response.Redirect(Constants.RestUrlPage + "?simulator=" + txtNameOfSimulator.Text.Trim(), false); 
                        }
                        else
                        {
                            DeleteFile(Server.MapPath("~/") + fileName);
                        }
                    }

                }
                else
                {
                    //FileNameTextbox.Value = "Please select xsd file.";
                    FileNameTextbox.Attributes.Add(Constants.PlaceholderAttribute, Constants.FileNotUploadedError);
                    FileNameTextbox.Style.Add("border", "1px solid Darkred");
                    FileNameTextbox.Style.Add("background-color", "#f2dede");
                    FileNameTextbox.Value = string.Empty;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Visible = true;
                lblMessage.Text = Constants.FileUploadError + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
                FileNameTextbox.Value = string.Empty;
            }

        }

        protected void ShowDataButton_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> classes = InstanceCreation.GetClasses(Constants.NamespaceName);

                foreach (string className in classes)
                {
                    object instance = InstanceCreation.GetInstance(Constants.NamespaceName, className);
                    List<PropertyInfo> properties = InstanceCreation.GetProperties(instance);

                    Table table = new Table();
                    table.CssClass = Constants.TableCss;

                    TableHeaderRow tbHeaderRow = new TableHeaderRow();

                    TableHeaderCell tbHeaderCellForName = new TableHeaderCell();
                    Label headerpropertyNameLabel = new Label();

                    headerpropertyNameLabel.Text = Constants.PropertiesName;
                    tbHeaderCellForName.Controls.Add(headerpropertyNameLabel);

                    TableHeaderCell tbHeaderCellForType = new TableHeaderCell();
                    Label headerpropertyTypeLabel = new Label();

                    headerpropertyTypeLabel.Text = Constants.DataTypes;
                    tbHeaderCellForType.Controls.Add(headerpropertyTypeLabel);

                    tbHeaderRow.Controls.Add(tbHeaderCellForName);
                    tbHeaderRow.Controls.Add(tbHeaderCellForType);

                    table.Controls.Add(tbHeaderRow);

                    foreach (PropertyInfo property in properties)
                    {
                        string propertyName = property.Name;
                        string propertyType = property.PropertyType.ToString().Split('.')[1];

                        TableRow rowNew = new TableRow();
                        table.Controls.Add(rowNew);

                        TableCell propertyNameCell = new TableCell();
                        TableCell propertyTypeCell = new TableCell();

                        Label propertyNameLabel = new Label();
                        propertyNameLabel.Text = propertyName;

                        Label propertyTypeLabel = new Label();
                        propertyTypeLabel.Text = propertyType;

                        propertyNameCell.Controls.Add(propertyNameLabel);
                        propertyTypeCell.Controls.Add(propertyTypeLabel);

                        rowNew.Controls.Add(propertyNameCell);
                        rowNew.Controls.Add(propertyTypeCell);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private Hashtable GetAllTextBoxes(Control parent)
        {
            try
            {
                foreach (Control control in parent.Controls)
                {
                    if (control is TextBox)
                    {
                        TextBox textbox = control as TextBox;
                        if (!listOfURLs.Contains(textbox.ID))
                        {
                            if (!string.IsNullOrEmpty(textbox.Text))
                            {
                                if (Uri.IsWellFormedUriString(textbox.Text, UriKind.Absolute))
                                {
                                    string textboxID = textbox.ID;
                                    textboxID = textboxID.Replace(Constants.TypeTextBox, string.Empty);
                                    listOfURLs.Add(textboxID, textbox.Text);
                                }
                            }
                        }
                    }
                    GetAllTextBoxes(control);
                }
                return listOfURLs;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private void DeleteFile(string filename)
        {
            try
            {
                if ((System.IO.File.Exists(filename)))
                {
                    System.IO.File.Delete(filename);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}