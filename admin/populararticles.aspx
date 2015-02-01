<%@ Page Language="VB" MasterPageFile="~/admin/Menu.master" AutoEventWireup="false" CodeFile="populararticles.aspx.vb" Inherits="admin_populararticles" title="Popular Articles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table cellpadding="2" cellspacing="0" border="0" width="100%">
    <tr>
    <td>
    <fieldset>
        <legend><b>Popular Articles</b></legend>
        <div style="margin-top:10px; margin-bottom:10px; margin-left:10px; margin-right:10px">
        <table>
            <tr>
                <td style="width:20%;">article 1</td>
                <td align="left" style="width:80%;">
                    <asp:TextBox ID="txtMessage1" Width="500" MaxLength="250" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width:20%;">URL 1</td>
                <td align="left" style="width:80%;">
                    <asp:TextBox ID="txtURL1" Width="500" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width:20%;">article 2</td>
                <td align="left" style="width:80%;">
                    <asp:TextBox ID="txtMessage2" Width="500" MaxLength="250" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width:20%;">URL 2</td>
                <td align="left" style="width:80%;">
                    <asp:TextBox ID="txtURL2" Width="500" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width:20%;">article 3</td>
                <td align="left" style="width:80%;">
                    <asp:TextBox ID="txtMessage3" Width="500" MaxLength="250" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width:20%;">URL 3</td>
                <td align="left" style="width:80%;">
                    <asp:TextBox ID="txtURL3" Width="500" runat="server"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td style="width:20%;">article 4</td>
                <td align="left" style="width:80%;">
                    <asp:TextBox ID="txtMessage4" Width="500" MaxLength="250" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width:20%;">URL 4</td>
                <td align="left" style="width:80%;">
                    <asp:TextBox ID="txtURL4" Width="500" runat="server"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td style="width:20%;">article 5</td>
                <td align="left" style="width:80%;">
                    <asp:TextBox ID="txtMessage5" Width="500" MaxLength="250" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width:20%;">URL 5</td>
                <td align="left" style="width:80%;">
                    <asp:TextBox ID="txtURL5" Width="500" runat="server"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td style="width:20%;">article 6</td>
                <td align="left" style="width:80%;">
                    <asp:TextBox ID="txtMessage6" Width="500" MaxLength="250" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width:20%;">URL 6</td>
                <td align="left" style="width:80%;">
                    <asp:TextBox ID="txtURL6" Width="500" runat="server"></asp:TextBox>
                </td>
            </tr>
            
            <tr>
                <td style="width:20%;">article 7</td>
                <td align="left" style="width:80%;">
                    <asp:TextBox ID="txtMessage7" Width="500" MaxLength="250" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width:20%;">URL 7</td>
                <td align="left" style="width:80%;">
                    <asp:TextBox ID="txtURL7" Width="500" runat="server"></asp:TextBox>
                </td>
            </tr>
            
            <tr>
                <td style="width:20%;">article 8</td>
                <td align="left" style="width:80%;">
                    <asp:TextBox ID="txtMessage8" Width="500" MaxLength="250" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width:20%;">URL 8</td>
                <td align="left" style="width:80%;">
                    <asp:TextBox ID="txtURL8" Width="500" runat="server"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td style="width:20%;">article 9</td>
                <td align="left" style="width:80%;">
                    <asp:TextBox ID="txtMessage9" Width="500" MaxLength="250" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width:20%;">URL 9</td>
                <td align="left" style="width:80%;">
                    <asp:TextBox ID="txtURL9" Width="500" runat="server"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td style="width:20%;">article 10</td>
                <td align="left" style="width:80%;">
                    <asp:TextBox ID="txtMessage10" Width="500" MaxLength="250" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width:20%;">URL 10</td>
                <td align="left" style="width:80%;">
                    <asp:TextBox ID="txtURL10" Width="500" runat="server"></asp:TextBox>
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

