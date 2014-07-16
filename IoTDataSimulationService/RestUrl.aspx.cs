using IoTDataSimulationService.Constant;
using IoTDataSimulationService.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace IoTDataSimulationService
{
    public partial class RestUrl : System.Web.UI.Page
    {
        public List<string> listOfEmptyUrlTextboxes = new List<string>();
        protected void CreateTextBoxForPostURL()
        {
            try
            {
                List<string> classes = InstanceCreation.GetAllParentClasses(Constants.NamespaceName);                

                foreach (string className in classes)
                {
                    Panel urlTextboxConatiner = new Panel();
                    urlTextboxConatiner.Attributes.Add(Constants.ClassAttribute, Constants.PanelStyle);
                    urlTextboxConatiner.ID = Constants.Div + className;

                    Label classNameLabel = new Label();
                    classNameLabel.Attributes.Add(Constants.ForAttribute, className);
                    classNameLabel.Text = className;

                    urlTextboxConatiner.Controls.Add(classNameLabel);

                    TextBox urlTextbox = new TextBox();
                    urlTextbox.ID = className;
                    urlTextbox.CssClass = Constants.Form_ControlClass;
                    urlTextbox.Attributes.Add(Constants.PlaceholderAttribute, Constants.HttpPlaceHolderValue);

                    urlTextboxConatiner.Controls.Add(urlTextbox);

                    Panel emptyDiv = new Panel();
                    emptyDiv.Attributes.Add(Constants.ClassAttribute, Constants.EmptyDivStyle);

                    this.panelDynamicData.Controls.Add(urlTextboxConatiner);
                    this.panelDynamicData.Controls.Add(emptyDiv);
                }

                if (!listOfEmptyUrlTextboxes.Count.Equals(0))
                {
                    foreach (string textbox in listOfEmptyUrlTextboxes)
                    {
                        TextBox emptyTextbox = (TextBox)this.panelDynamicData.FindControl(textbox);
                        emptyTextbox.CssClass = Constants.AlertDanger;
                        emptyTextbox.BorderColor = System.Drawing.Color.DarkRed;
                        emptyTextbox.Attributes.Add(Constants.PlaceholderAttribute, Constants.WrongURLError);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private Hashtable GetPostURLTextBoxValues(Control parent)
        {
            try
            {
                List<string> classes = InstanceCreation.GetAllParentClasses(Constants.NamespaceName);

                Hashtable listOfURLs = new Hashtable();

                foreach (string className in classes)
                {
                    bool isTextboxValueNotNull = CheckTextboxValue(className);
                    if (isTextboxValueNotNull)
                    {
                        string url = Convert.ToString(Request.Form[className]);
                        if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
                        {
                            listOfURLs.Add(className, url);
                        }
                    }
                    if (!isTextboxValueNotNull || !(Uri.IsWellFormedUriString(Convert.ToString(Request.Form[className]), UriKind.Absolute)))
                    {
                        listOfEmptyUrlTextboxes.Add(className);
                    }
                }
                return listOfURLs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private bool CheckTextboxValue(string className)
        {
            try
            {
                string url = Convert.ToString(Request.Form[className]);
                if (!string.IsNullOrEmpty(url))
                {
                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        protected void btnFinish_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable listOfURLs = GetPostURLTextBoxValues(this.panelDynamicData);
                if(listOfEmptyUrlTextboxes.Count.Equals(0))
                {
                    if (!listOfURLs.Count.Equals(0))
                    {
                        HttpContext.Current.Session[Constants.ListOfURLsSession] = listOfURLs;
                        HttpContext.Current.Session[Constants.SimulatorName] = Convert.ToString(Request.QueryString["simulator"]);
                        Response.Redirect(Constants.DataGenerationPage, false);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            CreateTextBoxForPostURL();
        }
    }
}