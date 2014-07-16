<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="IoTDataSimulationService.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home</title>
    <link href="Contents/css/bootstrap-theme.min.css" rel="stylesheet" />
    <link href="Contents/css/bootstrap.min.css" rel="stylesheet" />
    <link href="Contents/css/PageStyle.css" rel="stylesheet" />
    <script src="Contents/js/jquery-1.7.1.js"></script>
    <script src="Contents/js/bootstrap.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#FileUploadControl').change(function () {
                var filenameWithoutPath = $(this).val().replace("C:\\fakepath\\", "");
                $('#FileNameTextbox').val(filenameWithoutPath);
            })
        });
    </script>
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
                <li class="active-step">
                    <span class="step-name">Upload XML</span>
                    <span class="visuallyhidden"></span>
                    <span class="step-num">&nbsp;</span>
                </li>
                <li>
                    <span class="step-name">Review XSD</span>
                    <span class="visuallyhidden"></span>
                    <span class="step-num">&nbsp;</span>
                </li>
                <li>
                    <span class="step-name">Retrieval Loaction</span>
                    <span class="visuallyhidden"></span>
                    <span class="step-num">&nbsp;</span>
                </li>
            </ol>
        </div>
        <div class="jumbotron mrg pd-left30 pd-right30">
            <ul class="media-list">
                <li class="media pd-left30 pd-right30">
                    <a class="pull-left" href="#">
                        <div class="circle">1</div>
                    </a>
                    <div class="media-body">
                        <h4 class="media-heading"><strong>Download the XSD and XML Templates</strong></h4>
                        <div class="col-md-9 pd-left0">
                            Cras sit amet nibh libero, in gravida nulla. Nulla vel metus scelerisque ante sollicitudin commodo. 
                            Cras purus odio, vestibulum in vulputate at, tempus viverra turpis.Cras sit amet nibh libero, 
                            in gravida nulla. Nulla vel metus scelerisque ante sollicitudin commodo. 
                            Cras purus odio, vestibulum in vulputate at, tempus viverra turpis.
                        </div>
                        <div class="col-md-3">
                            <button type="button" class="btn btn-default btn-custom btn-dark-gray">Download</button>
                        </div>
                    </div>
                </li>
                <li class="media pd-left30 pd-right30">
                    <a class="pull-left" href="#">
                        <div class="circle">2</div>
                    </a>
                    <div class="media-body">
                        <h4 class="media-heading"><strong>Customize XML</strong></h4>
                        <div class="col-md-12 pd-left0">
                            Cras sit amet nibh libero, in gravida nulla. Nulla vel metus scelerisque ante sollicitudin commodo. 
                            Cras purus odio, vestibulum in vulputate at, tempus viverra turpis.Cras sit amet nibh libero, 
                            in gravida nulla. Nulla vel metus scelerisque ante sollicitudin commodo. 
                            Cras purus odio, vestibulum in vulputate at, tempus viverra turpis.
                        </div>
                    </div>
                </li>
                <li class="media pd-left30 pd-right30 upload-box">
                    <a class="pull-left" href="#">
                        <div class="circle">3</div>
                    </a>
                    <div class="media-body">
                        <h4 class="media-heading"><strong>Upload customized XML</strong></h4>
                        <div class="col-md-12 pd-left0">
                            Cras sit amet nibh libero, in gravida nulla. Nulla vel metus scelerisque ante sollicitudin commodo. 
                            Cras purus odio, vestibulum in vulputate at, tempus viverra turpis.Cras sit amet nibh libero, 
                            in gravida nulla. Nulla vel metus scelerisque ante sollicitudin commodo. 
                            Cras purus odio, vestibulum in vulputate at, tempus viverra turpis.
                        </div>
                        <div style="padding: 50px 0;">
                            <form id="form1" role="form" runat="server" style="padding: 50px 0;">
                                <div>
                                    Username:
                                    <asp:Label ID="_username" runat="server"></asp:Label>
                                </div>
                                <div class="form-group">
                                    <asp:Label runat="server" ID="lblMessage" Visible="false" Text=""></asp:Label>
                                </div>
                                <div class="form-group">
                                    <label for="txtNameOfSimulator">NAME OF SIMULATOR</label>
                                    <div class="container pd-left0">
                                        <div class="col-lg-9 pd-left0">
                                            <asp:TextBox runat="server" ID="txtNameOfSimulator" CssClass="form-control pd-left0"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="uploadXMLFile">UPLOAD XML FILE</label>
                                    <div class="container pd-left0">
                                        <div class="col-lg-9 pd-left0">
                                            <input type="text" id="FileNameTextbox" runat="server" class="form-control" readonly="readonly"/>
                                        </div>
                                        <div class="fileContainer">
                                            <asp:FileUpload ID="FileUploadControl" CssClass="col-sm-12 col-xs-12 btn btn-default text-align-left" runat="server"/>
                                            <asp:Button CssClass="btn btn-default browseButton" runat="server" ID="BrowseButton" Text="Browse" OnClick="UploadButton_Click" />
                                        </div>

                                    </div>
                                </div>
                                <div class="form-group pull-right pd-top20 mr7dot5">
                                    <button class="btn btn-lg btn-link lnk-cancel-gray-btn" onclick="location.reload();" type="button">Cancel</button>
                                    | &nbsp;&nbsp;&nbsp; 
                                    <asp:Button CssClass="btn btn-default btn-custom" runat="server" ID="UploadButton" Text="Submit" OnClick="UploadButton_Click" />
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

