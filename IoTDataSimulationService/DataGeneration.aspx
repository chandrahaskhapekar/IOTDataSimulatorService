<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataGeneration.aspx.cs" Inherits="IoTDataSimulationService.DataGeneration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Data Simulators</title>
    <link href="Contents/css/bootstrap-theme.min.css" rel="stylesheet" />

    <link href="Contents/css/bootstrap.min.css" rel="stylesheet" />
    <link href="Contents/css/PageStyle.css" rel="stylesheet" />
    <script src="Contents/js/jquery-1.7.1.js"></script>
    <script src="Contents/js/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="header">
                <h3 class="text-muted mrg">JOHN DEERE</h3>
            </div>
            <div class="row mrg">
                <h4 class="col-sm-9"><strong>My Data Simulators</strong></h4>
                <div class="col-sm-3">
                    <input type="button" class="btn btn-default btn-custom" id="createNew" onclick="window.location.href = 'Home.aspx'" value="CREATE NEW" />
                </div>
            </div>
            <div class="col-md-12 pd-2" style="border: 2px solid #808080">
                <div class="col-md-9">
                    <h3 runat="server" id="simulatorName" class="mrg0 pd-top-bottom4"></h3>
                </div>
                <div class="col-md-2 outerDiv">
                    <%--<i class="glyphicon glyphicon-play"></i><asp:Button ID="btnStartStopProcess" runat="server" Text="" CssClass="btn btn-default" Style="margin-top: 15px; margin-bottom: 10px" OnClick="btnStartStopProcess_Click" />--%>
                    <asp:LinkButton ID="btnStartStopProcess" runat="server" CssClass="btn btn-default" OnClick="btnStartStopProcess_Click">
                        <i aria-hidden="true" class="glyphicon glyphicon-play"></i>
                    </asp:LinkButton>
                </div>
                <div class="col-md-1">
                    <a href="#" class="settings">
                        <h2 class="glyphicon glyphicon-cog setting-icon mrg0"></h2>
                    </a>
                    <ul class="displayNone settingsMenu">
                        <li><a href="#">Edit</a></li>
                        <li class="separator-grey"></li>
                        <li><a href="#">Remove</a></li>
                    </ul>
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript">
        $(function () {
            if ($('#btnStartStopProcess').hasClass('stopButton')) {
                $('div .outerDiv').parent().addClass('activeRow');
                $('#simulatorName').css("color", "#fff");
            }

            $('.settings').click(function () {
                $('.settings').find('h2').toggleClass('active');
                $('.settingsMenu').toggleClass('displayNone').toggleClass('displayTable');
            });
        });

    </script>

</body>

</html>
