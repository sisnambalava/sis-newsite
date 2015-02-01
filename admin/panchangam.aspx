<%@ Page Language="VB" MasterPageFile="~/admin/Menu.master" AutoEventWireup="false" CodeFile="panchangam.aspx.vb" Inherits="panchangam" title="Panchangam" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table cellpadding="2" cellspacing="0" border="0" width="100%">
    <tr>
    <td>
    <fieldset>
        <legend><b>Panchangam - Landing Page</b></legend>
        <div style="margin-top:10px; margin-bottom:10px; margin-left:10px; margin-right:10px">
        <table>
            <tr>
                <td><b>Page title</b><br />
                <asp:TextBox ID="txtPagetitle" runat="server" Width="500"></asp:TextBox>
                <asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ControlToValidate="txtPagetitle" ErrorMessage="Page title is missing"  Display="Dynamic" Text="*" />
                </td>
            </tr>
            <tr>
                <td><b>Meta Keywords</b><br />
                <textarea id="txtKeywords" runat="server" cols="80" rows="4"></textarea>
                <asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" ControlToValidate="txtKeywords" ErrorMessage="Page meta keywords is missing"  Display="Dynamic" Text="*" />
                </td>
            </tr>

            <tr>
                <td><b>Meta description</b><br />
                <textarea id="txtDescriptions" runat="server" cols="80" rows="4"></textarea>
                <asp:RequiredFieldValidator id="RequiredFieldValidator3" runat="server" ControlToValidate="txtDescriptions" ErrorMessage="Page meta descriptions is missing"  Display="Dynamic" Text="*" />
                <p></p>
                </td>
            </tr>
            <tr>
                <td><b>H1 Text</b><br />
                    <asp:TextBox ID="txtH1" Width="500" MaxLength="250" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td><b>Content</b><br />
                    <textarea id="txtContent" runat="server" cols="100" rows="10"></textarea>
                </td>
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

