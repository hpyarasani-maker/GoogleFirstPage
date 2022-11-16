<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="datewise.aspx.cs" Inherits="GoogleFirstPage.Xmltracker.datewise" %>

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
        <div class="header" style="width:4000px;">
            <div class="header-menu">
                <div style="margin-left: -80px;">
                    <img src="images/pilogo.jpg" alt="pi-datametrics" class="imagedimensions" height="51" width="54" />
                    <label class="labeltext1">Elementary by Pi</label>
                </div>
            </div>
        </div>
        <div class="displaydata">
            <div style="margin-top: 1px; background-color: #e4e8ef; height: 120px; border-radius: 10px 10px;">
                <br />
                <div>
                    <table style="margin-left: auto; margin-right: auto;">
                        <tr>
                            <td>
                                <strong><span>Select Date :</span></strong>
                            </td>
                            <td>
                                <asp:TextBox ID="txtenddate" runat="server" placeholder="start dateate" Height="30px" Width="90px" Font-Size="Large" CssClass="text1"></asp:TextBox>
                                <asp:ImageButton runat="Server" ID="EndDateImage" ImageUrl="~/images/calendarnew.png" Height="30px" Width="40px" />
                                <ajaxToolkit:CalendarExtender ID="endDate" runat="server" PopupButtonID="EndDateImage" TargetControlID="txtenddate" Format="yyyy-MM-dd" StartDate="2022-11-02" />
                            </td>
                            <td></td>
                            <td>
                                <asp:Button ID="btndatewise" runat="server" OnClick="btndatewise_Click" CssClass="buttonleft" Height="27px" Width="120px" Text="Submit" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>

        </div>
        <div style="margin-top:150px;">
             <div class="wrapper">
            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            </asp:UpdateProgress>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btndatewise" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        <asp:PlaceHolder ID="phr" runat="server"></asp:PlaceHolder>
                        &nbsp;
                    </p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        </div>
       
        <br />
        <br />
        <br />
        <br />
        <br />
        <div class="footer">
            &copy; <%= DateTime.Now.Year %> Pi Datametrics | XML Tracker.
        </div>
    </form>
</body>
</html>
