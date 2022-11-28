<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="xmlchanges.aspx.cs" Inherits="GoogleFirstPage.Xmltracker.xmlchanges" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <title>Elementary by Pi</title>
    <link href="css/main1.css" rel="stylesheet" />

    <link rel="icon" href="images/default-avatar-logo.png" type="image/x-icon" />
 
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scriptmgr" runat="server" EnablePageMethods="false"></asp:ScriptManager>
        <div class="header" style="width:4100px;">
            <div class="header-menu">
                <div style="margin-left: -80px;">
                    <img src="images/pilogo.jpg" alt="pi-datametrics" class="imagedimensions" height="51" width="54" />
                    <label class="labeltext1">Elementary by Pi</label>
                </div>
            </div>
        </div>  
        <h1 class="headerofh1">Elementary Changes Report</h1>
        <div class="wrapper">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                </asp:UpdateProgress>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <p>
                            <asp:PlaceHolder ID="phr" runat="server"></asp:PlaceHolder>
                            &nbsp;
                        </p>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        <br />
        <br />
        <br />
        <br />
        <br />
         <div class="footer">
                &copy; <%= DateTime.Now.Year %> Pi Datametrics | Elementary by Pi.
            </div>
    </form>
</body>
</html>
