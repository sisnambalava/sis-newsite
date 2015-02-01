<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="admin_default" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>SIS ADMIN</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table align="center">
    <tr>
        <td>User name</td>
        <td><asp:TextBox ID="txtusername" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Password</td>
        <td><asp:TextBox TextMode="Password" ID="txtpassword" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
        <td colspan="2" align="center"><asp:Button ID="btnSubmit" Text="Submit" runat="server" /></td>
    </tr>
    <tr>
        <td colspan="2"><asp:Label ID="lblMsg" runat="server"></asp:Label></td>
    </tr>
    </table>
    </div>
    </form>
</body>
</html>
