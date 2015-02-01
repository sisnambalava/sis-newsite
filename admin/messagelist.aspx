<%@ Page Language="VB" MasterPageFile="~/admin/Menu.master" AutoEventWireup="false" CodeFile="messagelist.aspx.vb" Inherits="admin_messagelist" title="Message Board" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table cellpadding="0" cellspacing="0" border="0" width="100%" style="border:solid 1px blackl">
<tr>
    <td>
    <fieldset>
        <legend><b>Message Box</b></legend>
        <div style="margin-top:10px; margin-bottom:10px; margin-left:10px; margin-right:10px">
        <asp:Label ID="lblError" runat="server" ForeColor="red"></asp:Label>
        <table>    
            <tr>
                <td>
                    <asp:GridView ID="gvwMessages" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                        <Columns>
                            <asp:TemplateField HeaderText="Message ID">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hypMessage" runat="server" Text='<% #DataBinder.Eval(Container.DataItem,"MessageId") %>'
                                    NavigateUrl='<%# "message.aspx?mid=" & DataBinder.Eval(Container.DataItem,"MessageId") %>'></asp:HyperLink>
                                </ItemTemplate>
                                <HeaderStyle BorderColor="#BABEC7" />
                                <ItemStyle BorderColor="#BABEC7" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="Description" HeaderText="Message">
                                <ItemStyle HorizontalAlign="Left"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="ExpiryDate" HeaderText="Expiry Date">
                                <ItemStyle HorizontalAlign="Left"/>
                            </asp:BoundField>
                        </Columns>
                        <RowStyle ForeColor="#000066" />
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
        </div>
    </fieldset>
    </td>
    </tr>
</table>            
</asp:Content>

