<%@ Page Language="VB" MasterPageFile="~/admin/Menu.master" AutoEventWireup="false" ValidateRequest="false" CodeFile="message.aspx.vb" Inherits="admin_message" title="Message Board" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table cellpadding="2" cellspacing="0" border="0" width="100%">
    <tr>
    <td>
    <fieldset>
        <legend><b>Message Box</b></legend>
        <div style="margin-top:10px; margin-bottom:10px; margin-left:10px; margin-right:10px">
        <table>
            <tr>
                <td style="width:20%;">Message</td>
                <td align="left" style="width:80%;">
                    <asp:TextBox ID="txtMessage" Width="500" MaxLength="125" runat="server"></asp:TextBox>
                    <br /><span style="font-size:x-small; font-weight:bold;">Maximum characters allowed: 100</span>
                </td>
            </tr>
            <tr>
                <td>Expiry Date(dd/mm/yyyy)</td>
                <td><asp:TextBox ID="txtExpiryDate" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td align="left"><asp:Button ID="btnSubmit" runat="server" Text="Save" /></td>
            </tr>
        </table>
        </div>
    </fieldset>
    </td>
    </tr>
</table>    
</asp:Content>

