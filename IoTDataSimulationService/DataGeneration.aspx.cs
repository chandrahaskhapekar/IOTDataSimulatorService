using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Activation;
using System.ServiceModel;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Text;
using System.IO;
using IoTDataSimulationService.Helper;
using System.Threading;
using IoTDataSimulationService.Constant;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.Security;

namespace IoTDataSimulationService
{
    public partial class DataGeneration : System.Web.UI.Page
    {
        private static bool connectionStatus = false;
        private Hashtable listOfURLs;
        private static Thread thread;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session[Constants.ListOfURLsSession] != null)
                {
                    listOfURLs = new Hashtable();
                    listOfURLs = (Hashtable)Session[Constants.ListOfURLsSession];
                }
                if (Session[Constants.SimulatorName] != null)
                {
                    simulatorName.InnerText = Convert.ToString(Session[Constants.SimulatorName]);
                }

                UpdateStatus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void DoWork(object sender, EventArgs e)
        {
            try
            {
                DataGenerationHelper helper = new DataGenerationHelper();
                helper.StartSendingData(connectionStatus, listOfURLs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnStartStopProcess_Click(object sender, EventArgs e)
        {
            try
            {
                if (connectionStatus.Equals(false))
                {
                    connectionStatus = true;
                    UpdateStatus();
                    DataGeneration dataGeneration = new DataGeneration();
                    thread = new Thread(() => DoWork(sender,e));
                    thread.Name = Constants.BackgroundThread;
                    thread.IsBackground = true;
                    thread.Start();
                }
                else
                {
                    connectionStatus = false;
                    UpdateStatus();
                    string name = thread.Name;
                    if (name.Equals(Constants.BackgroundThread))
                    {
                        thread.Abort();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void UpdateStatus()
        {
            if (connectionStatus)
            {
                btnStartStopProcess.Text = Constants.StopButtonText;
                btnStartStopProcess.CssClass = Constants.StopButtonCssClass;
            }
            else
            {
                btnStartStopProcess.Text = Constants.StartButtonText;
                btnStartStopProcess.CssClass = Constants.StartButtonCssClass;
            }
        }
    }
}