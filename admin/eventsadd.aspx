<%@ Page Language="VB" MasterPageFile="~/admin/Menu.master" ValidateRequest="false" AutoEventWireup="false" CodeFile="eventsadd.aspx.vb" Inherits="admin_eventsadd" title="Events" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<%@ Import Namespace="System.XML" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table border="0" width="100%" cellpadding="0" cellspacing="0">
    <tr>
    <td>
        <fieldset>
        <legend><b>Events</b></legend>
        <div style="margin-top:10px; margin-bottom:10px; margin-left:10px; margin-right:10px">
        <asp:Label ID="lblSuccess" runat="server" ForeColor="Red" Font-Bold="true" Font-Size="Small"></asp:Label>
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
                <td><b>Event Heading</b><br />
                <asp:textbox ID="txtH2Text" runat="server" Width="500"></asp:textbox>
                <asp:RequiredFieldValidator id="RequiredFieldValidator8" runat="server" ControlToValidate="txtH2Text" ErrorMessage="Article Heading is missing"  Display="Dynamic" Text="*" />
                </td>
            </tr>

            <tr>
                <td><b>Event Date</b><br />
                <cc1:GMDatePicker ID="ADate" runat="server"></cc1:GMDatePicker>
                </td>
            </tr>

            <tr>
                <td><b>Time</b><br />
                <asp:textbox ID="txtTime" runat="server" Width="500"></asp:textbox>
                <asp:RequiredFieldValidator id="RequiredFieldValidator4" runat="server" ControlToValidate="txtTime" ErrorMessage="Time is missing"  Display="Dynamic" Text="*" />
                </td>
            </tr>
            
            <tr>
                <td><b>Address</b><br />
                <asp:textbox ID="txtAddress" runat="server" Width="500"></asp:textbox>
                <asp:RequiredFieldValidator id="RequiredFieldValidator10" runat="server" ControlToValidate="txtAddress" ErrorMessage="address is missing"  Display="Dynamic" Text="*" />
                </td>
            </tr>

            <tr>
                <td><b>Description</b><br />
                <textarea id="txtContent2" runat="server" cols="100" rows="40"></textarea>
                <asp:RequiredFieldValidator id="RequiredFieldValidator13" runat="server" ControlToValidate="txtContent2" ErrorMessage="Main description is missing"  Display="Dynamic" Text="*" />
                </td>
            </tr>

            <tr>
                <td align="center"><asp:Button ID="btnSubmit" Text="Submit" runat="server" /></td>
            </tr>
            <tr><td>
                <asp:ValidationSummary id="valSummary" runat="server"
                     DisplayMode="BulletList"
                     HeaderText="<b>The following errors were detected:</b>" />   
            </td></tr>
        </table>
        </div>
        </fieldset>
    </td>
</tr>
</table>
</asp:Content>

