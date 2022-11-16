<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="keywords.aspx.cs" Inherits="GoogleFirstPage.Xmltracker.keywords" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link href="css/main.css" rel="stylesheet" />

    <title>Elementary by Pi</title>


    <link rel="icon" href="images/default-avatar-logo.png" type="image/x-icon" />

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

    <link media="screen" rel="stylesheet" href='http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/css/bootstrap.min.css' />
    <link rel="stylesheet" type="text/css" href="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/0.1.0/css/footable.min.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/0.1.0/js/footable.min.js"></script>

    <script src="js/jquery-1.10.2.js"></script>

    <script type="text/javascript">
        $(function () {
            $('[id*=gridkeywords]').footable();
        });
    </script>
</head>
<body>
    <script src="js/custom.js"></script>
    <script src="js/jquery.js"></script>

    <form id="form1" runat="server">
        <asp:ScriptManager ID="scriptmgr" runat="server" EnablePageMethods="false"></asp:ScriptManager>
        <div class="header" style="width: 2500px;">
            <div class="header-menu">
                <div style="margin-left: -80px;">
                    <img src="images/pilogo.jpg" alt="pi-datametrics" class="imagedimensions" height="51" width="54" />
                    <label class="labeltext1">Elementary by Pi</label>
                </div>
            </div>
        </div>
        <div class="displaydata">
            <div style="margin-top: 1px; background-color: #e4e8ef; height: 120px; border-radius: 10px 10px;margin-left:20px;width:105%;">
                <br />
                <div>
                    <table style="margin-left: auto; margin-right: auto;">
                        <tr>
                            <td><strong><span>Client :</span></strong>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlclients" runat="server" Height="32px" Width="200px" CssClass="control"></asp:DropDownList>
                            </td>
                            <td></td>
                            <td>
                                <asp:Button ID="btnkeywords" runat="server" OnClick="btnkeywords_Click" CssClass="buttonleft" Text="Submit" Height="27px" Width="120px" />
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>
                                <strong><span>keyword Search :</span></strong>
                            </td>
                            <td>
                                <asp:TextBox ID="txtsearch" runat="server" Height="40px" Width="200px" CssClass="control" placeholder="Enter keyword"></asp:TextBox>
                            </td>
                            <td></td>
                            <td>
                                <asp:Button ID="btnsearch" runat="server" OnClick="btnsearch_Click" CssClass="buttonright" Text="Submit" Height="27px" Width="120px" />
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
        <div style="margin-left:30px;">
            <div class="wrapper">
            <asp:Label ID="lblnodata" runat="server" ForeColor="Blue" Font-Size="Small" Font-Bold="true"></asp:Label><br />
            <div id="divgetkwds" runat="server">
                <asp:UpdatePanel ID="updatepnel1" runat="server">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnkeywords" EventName="Click" />
                    </Triggers>
                    <ContentTemplate>
                        <asp:GridView ID="gridkeywords" runat="server" AutoGenerateColumns="false" Width="80%" CssClass="footable" Font-Bold="true" HeaderStyle-Font-Bold="true" RowStyle-Height="1px" AlternatingRowStyle-BackColor="#f5f5f5" Font-Size="Small" HeaderStyle-BackColor="#e4e8ef" RowStyle-Width="1px" HeaderStyle-Height="30px" CellPadding="5" CellSpacing="0">
                            <HeaderStyle BackColor="#e4e8ef" Font-Bold="true" ForeColor="Black" />
                            <Columns>
                                <asp:TemplateField HeaderText="Keywords">
                                    <ItemTemplate>
                                        <asp:Label Text='<%#DataBinder.Eval(Container.DataItem,"KName")%>' ID="kwd" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <a href="xmlurls.aspx?kid=<%# Eval("kid") %>" target="_blank">Link</a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <div id="divsearch" runat="server">
                <asp:UpdatePanel ID="updatepnel2" runat="server">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnsearch" EventName="Click" />
                    </Triggers>
                    <ContentTemplate>
                        <asp:GridView ID="gvsearch" runat="server" AutoGenerateColumns="false" CssClass="footable" Width="80%" Font-Bold="true" HeaderStyle-Font-Bold="true" RowStyle-Height="1px" AlternatingRowStyle-BackColor="#f5f5f5" Font-Size="Small" HeaderStyle-BackColor="#e4e8ef" RowStyle-Width="1px" HeaderStyle-Height="30px" CellPadding="5" CellSpacing="0">
                            <HeaderStyle BackColor="#e4e8ef" Font-Bold="true" ForeColor="Black" />
                            <Columns>
                                <asp:TemplateField HeaderText="Keywords">
                                    <ItemTemplate>
                                        <asp:Label ID="kwdsearch" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"keyword")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <a href="xmlurls.aspx?kid=<%# Eval("kid") %>" target="_blank">Link</a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
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
