<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="xmlurls.aspx.cs" Inherits="GoogleFirstPage.Xmltracker.xmlurls" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <title>Elementary by Pi</title>
    <link href="css/main.css" rel="stylesheet" />

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

    <link media="screen" rel="stylesheet" href='http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/css/bootstrap.min.css' />
    <link rel="stylesheet" type="text/css" href="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/0.1.0/css/footable.min.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/0.1.0/js/footable.min.js"></script>

    <link rel="icon" href="images/default-avatar-logo.png" type="image/x-icon" />

    <script src="js/jquery-1.10.2.js"></script>

    <script type="text/javascript">
        $(function () {
            $('[id*=gvxmlurls]').footable();
        });
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scriptmgr" runat="server" EnablePageMethods="false"></asp:ScriptManager>
        <div class="header" style="width:2400px;">
            <div class="header-menu">
                <div style="margin-left: -80px;">
                    <img src="images/pilogo.jpg" alt="pi-datametrics" class="imagedimensions" height="51" width="54" />
                    <label class="labeltext1">Elementary by Pi</label>
                </div>
            </div>
        </div>
        <div class="displaydata">
            <br />
            <br />
            <asp:Label ID="lbl1" runat="server" Text="Click on  '' View XML''    for Processing XML Content" ForeColor="Blue" Font-Size="Large" Font-Bold="true"></asp:Label>
        </div>

        <div class="wrapper">
            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            </asp:UpdateProgress>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="gvxmlurls" runat="server" AutoGenerateColumns="false" Width="80%" CssClass="footable" Font-Bold="true" HeaderStyle-Font-Bold="true" RowStyle-Height="1px" AlternatingRowStyle-BackColor="#f5f5f5" Font-Size="Small" HeaderStyle-BackColor="#e4e8ef" RowStyle-Width="1px" HeaderStyle-Height="30px">
                        <HeaderStyle BackColor="#e4e8ef" Font-Bold="true" ForeColor="Black" />
                        <Columns>
                            <asp:BoundField HeaderText="UID" DataField="uid" ItemStyle-HorizontalAlign="Center" Visible="false" />
                            <asp:TemplateField HeaderText="URLs">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <a href="<%#DataBinder.Eval(Container.DataItem,"url") %>" target="_blank"><%#DataBinder.Eval(Container.DataItem,"url") %></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="View XML">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <a href="viewxml.aspx?uid=<%# Eval("uid") %>&url=<%# Eval("url") %>" target="_blank">View XML</a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="XML Changes">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <a href="xmlchanges.aspx?uid=<%# Eval("uid") %>" target="_blank">XML Changes</a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date wise">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <a href="Datewise.aspx?uid=<%# Eval("uid") %>" target="_blank">Date wise</a>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
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
