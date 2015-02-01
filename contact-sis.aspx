<%@ Page Language="VB" MasterPageFile="~/sis.master" AutoEventWireup="false" CodeFile="contact-sis.aspx.vb" Inherits="contact_sis"%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h1>South Indian Society</h1>
<p>South Indian Society is a registered charity in the UK. Charity No. 1053076<br /><br />
<u>Write to us:</u><br /><br />
South Indian Society<br />
7 Erroll Road<br />
Romford<br />
Essex<br />
RM1 3DJ<br />
United Kingdom.</p>
<div class="content" style="width:400px;">
Please send your comments using the following form: <br /><br />
<div class="div">
	<div class="login_txt1">Name:<br /></div>
	<asp:TextBox ID="txtName" runat="server" CssClass="textbox1"></asp:TextBox><br /><br />
	<asp:RequiredFieldValidator ID="revname" runat="server" ControlToValidate="txtName" ValidationGroup="contact" ErrorMessage="Name" Display="none"></asp:RequiredFieldValidator>
</div>
<div class="div">
	<div class="login_txt1">Phone:<br /></div>
	<asp:TextBox ID="txtPhone" runat="server" CssClass="textbox1"></asp:TextBox><br /><br />
	<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtPhone" ValidationGroup="contact" ErrorMessage="Phone" Display="none"></asp:RequiredFieldValidator>
</div>
<div class="div">
	<div class="login_txt1">Email:<br /></div>
	<asp:TextBox ID="txtEmail" runat="server" CssClass="textbox2"></asp:TextBox><br /><br />
	<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtEmail" ValidationGroup="contact" ErrorMessage="Email" Display="none"></asp:RequiredFieldValidator>
    <%--<asp:RegularExpressionValidator ID="rgetxtEmail" ControlToValidate="txtEmail" Display="none" ErrorMessage="Email Address" ValidationExpression="^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$" runat="server" ValidationGroup="contact" ></asp:RegularExpressionValidator>--%>
</div>
<div class="div">
	<div class="login_txt1">Comments:<br /></div>
	<textarea name="txtComments" id="txtComments" cols="60" runat="server" class="txtarea" rows="6" /><br /><br />
</div>

<div class="div" style="float:left; width:400px;">
	<asp:LinkButton Text="<img style='border:0px' src='/images/submit-button.png' alt='' title='' />" runat="server" ID="lnkSubmit" ValidationGroup="contact"></asp:LinkButton>
	<br /><br /></div>
</div>
<asp:ValidationSummary ID="val" runat="server" HeaderText="You failed to correctly fill in your:" ValidationGroup="contact"
ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="false" />
</asp:Content>