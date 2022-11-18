<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="XmlDiff.aspx.cs" Inherits="GoogleFirstPage.Xmltracker.XmlDiff" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <title>Elementary by Pi</title>
    <link href="css/main1.css" rel="stylesheet" />

    <link rel="icon" href="images/default-avatar-logo.png" type="image/x-icon" />

    <style type="text/css">
        .text1 {}
        .auto-style1 {
            height: 56px;
        }
        .auto-style2 {
            height: 187px;
            width: 950px;
        }
        .auto-style3 {
            font-size: 15px;
            border-radius: 4px;
            display: inline-block;
            font-family: Calibri;
            font-weight: bold;
            font-style: normal;
            color: #FFFFFF;
            border: 1px solid #1583c2;
            margin-left: 15px;
            background: #1583c2;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scriptmgr" runat="server" EnablePageMethods="false"></asp:ScriptManager>
        <div class="header" style="width:4000px;">
            <div class="header-menu">
                <div style="margin-left: -80px;">
                    <img src="images/pilogo.jpg" alt="pi-datametrics" class="imagedimensions" height="51" width="54" />
                    <label class="labeltext1">Elementary by Pi</label>
                </div>
            </div>
        </div>
        <div class="displaydata">
            <div style="margin-top: 1px; background-color: #e4e8ef; border-radius: 10px 10px; " class="auto-style2">
                <br />
                <div>
                    <table style="margin-left: auto; margin-right: auto;">
                        <tr>
                            <td class="auto-style1">
                                <strong><span>Select Date :</span></strong>
                            </td>
                            <td class="auto-style1">
                                <asp:TextBox ID="txtDate" runat="server" placeholder="Date" Height="30px" Width="152px" Font-Size="Large" CssClass="text1"></asp:TextBox>
                                <asp:ImageButton runat="Server" ID="DateImage" ImageUrl="~/images/calendarnew.png" Height="30px" Width="40px" />
                                <ajaxToolkit:CalendarExtender ID="cDate" runat="server" PopupButtonID="DateImage" TargetControlID="txtDate" Format="yyyy-MM-dd" StartDate="2022-11-02" />
                            </td>
                            </tr>
                        <tr>
                            <td>
                                <strong><span>UId :</span></strong>
                            </td>
                            <td>
                                <asp:TextBox ID="txtUId" runat="server" placeholder="UId" Height="30px" Width="153px" Font-Size="Large" CssClass="text1"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <br />
                                <asp:Button ID="btndatewise" runat="server" OnClick="btndatewise_Click" CssClass="auto-style3" Height="27px" Width="142px" Text="Submit" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>

        </div>        
       
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />

        <div ID="XmlData" runat="server"></div>

        <div class="footer">
            &copy; <%= DateTime.Now.Year %> Pi Datametrics | XML Tracker.
        </div>
    </form>
</body>
</html>
