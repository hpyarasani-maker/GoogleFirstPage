<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="GoogleFirstPage.Classiclinks.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="grd11" runat="server" AutoGenerateColumns="true">
                
            </asp:GridView>

            <asp:Button ID="bt111" runat="server" OnClick="bt111_Click" Text="submit" />
            <asp:Label ID="lblclcount" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>
