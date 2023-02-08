<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Async="true" Inherits="GoogleFirstPage.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="txtkwds1" runat="server"></asp:TextBox><br />
            <asp:Button ID="btnq1" runat="server" Text="Submit" OnClick="btnq1_Click"/>
        </div>
    </form>
</body>
</html>
