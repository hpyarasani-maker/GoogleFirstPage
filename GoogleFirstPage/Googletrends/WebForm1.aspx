<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="GoogleFirstPage.Googletrends.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <br />
            <asp:Button ID="btnsvd" runat="server" OnClick="btnsvd_Click" Text="Getdata" />
            <br />
            <br />
            <br />
            <asp:GridView ID="grdsvm" runat="server" ShowFooter="true">
            </asp:GridView>
        </div>
    </form>
</body>
</html>
