<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RestUrl.aspx.cs" Inherits="IoTDataSimulationService.RestUrl" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>REST Urls</title>
    <link href="Contents/css/bootstrap-theme.min.css" rel="stylesheet" />
    <link href="Contents/css/bootstrap.min.css" rel="stylesheet" />
    <link href="Contents/css/PageStyle.css" rel="stylesheet" />
    <script src="Contents/js/bootstrap.min.js"></script>
</head>
<body>
    <div class="container">
        <div class="header">
            <h3 class="text-muted mrg">JOHN DEERE</h3>
        </div>
        <div>
            <h4 class="mrg"><strong>Create a new data simulator</strong></h4>
        </div>
        <div class="mrg">
            <ol class="wizard-progress clearfix">
                <li>
                    <span class="step-name">Upload XML</span>
                    <span class="visuallyhidden"></span>
                    <span class="step-num">&nbsp;</span>
                </li>
                <li>
                    <span class="step-name">Review XSD</span>
                    <span class="visuallyhidden"></span>
                    <span class="step-num">&nbsp;</span>
                </li>
                <li class="active-step">
                    <span class="step-name">Retrieval Loaction</span>
                    <span class="visuallyhidden"></span>
                    <span class="step-num">&nbsp;</span>
                </li>
            </ol>
        </div>
        <div class="jumbotron mrg pd-left30 pd-right30">
            <ul class="media-list">
                <li class="media pd-left30 pd-right30">
                    <div class="media-body">
                        <h4 class="media-heading"><strong>Where do you wish to send the data from this simulator?</strong></h4>
                        <div class="col-md-12 pd-left0">
                            <small>Enter the rest URLs of the devices data will be sent to.</small>
                        </div>
                        <div style="padding: 50px 0;">
                            <form id="form1" role="form" runat="server">
                                <asp:Panel ID="panelDynamicData" CssClass="container" runat="server">

                                </asp:Panel>
                                <div class="form-group col-sm-12 pd-top20">
                                    <asp:Button CssClass="btn btn-default btn-custom pull-right " runat="server" ID="btnFinish" Text="FINISH" OnClick="btnFinish_Click" />
                                    <span class="pull-right" style="padding: 10px 20px 10px 10px;">|</span>
                                    <button class="btn btn-lg btn-link lnk-cancel-darkgray-btn pull-right " onclick="window.location = 'Home.aspx'" type="button">Cancel</button>
                                </div>
                            </form>
                        </div>
                    </div>
                </li>
            </ul>
        </div>
    </div>
</body>
</html>
