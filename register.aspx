<%@ Page Language="VB" MasterPageFile="~/sis.master" AutoEventWireup="false" CodeFile="register.aspx.vb" Inherits="register"%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h1>Register your Details</h1>
<p>Please use this form to register your details with South Indian Society, United Kingdom.</p>
<div class="content" style="width:400px;">
<div class="div">
	<div class="login_txt1">Name:<br /></div>
	<asp:TextBox ID="txtName" runat="server" CssClass="textbox1"></asp:TextBox><br /><br />
	<asp:RequiredFieldValidator ID="revname" runat="server" ControlToValidate="txtName" ValidationGroup="register" ErrorMessage="Name" Display="none"></asp:RequiredFieldValidator>
</div>
<div class="div">
	<div class="login_txt1">Address:<br /></div>
	<asp:TextBox ID="txtAddress1" runat="server" CssClass="textbox2"></asp:TextBox><br /><br />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAddress1" ValidationGroup="register" ErrorMessage="Address1" Display="none"></asp:RequiredFieldValidator>
</div>
<div class="div">
	<asp:TextBox ID="txtAddress2" runat="server" CssClass="textbox2"></asp:TextBox><br /><br />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAddress2" ValidationGroup="register" ErrorMessage="Address2" Display="none"></asp:RequiredFieldValidator>
</div>
<div class="div">
	<asp:TextBox ID="txtAddress3" runat="server" CssClass="textbox2"></asp:TextBox><br /><br />
	<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtAddress3" ValidationGroup="register" ErrorMessage="Address3" Display="none"></asp:RequiredFieldValidator>
</div>
<div class="div">
	<div class="login_txt1">Postcode:<br /></div>
	<asp:TextBox ID="txtPostcode" runat="server" CssClass="textbox1"></asp:TextBox><br /><br />
	<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPostcode" ValidationGroup="register" ErrorMessage="Postcode" Display="none"></asp:RequiredFieldValidator>
</div>
<div class="div">
	<div class="login_txt1">Phone:<br /></div>
	<asp:TextBox ID="txtPhone" runat="server" CssClass="textbox1"></asp:TextBox><br /><br />
	<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtPhone" ValidationGroup="register" ErrorMessage="Phone" Display="none"></asp:RequiredFieldValidator>
</div>
<div class="div">
	<div class="login_txt1">Mobile:<br /></div>
	<asp:TextBox ID="txtMobile" runat="server" CssClass="textbox1"></asp:TextBox><br /><br />
</div>
<div class="div">
	<div class="login_txt1">Email:<br /></div>
	<asp:TextBox ID="txtEmail" runat="server" CssClass="textbox2"></asp:TextBox><br /><br />
	<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtEmail" ValidationGroup="register" ErrorMessage="Email" Display="none"></asp:RequiredFieldValidator>
    <%--<asp:RegularExpressionValidator ID="rgetxtEmail" ControlToValidate="txtEmail" Display="none" ErrorMessage="Email Address" ValidationExpression="^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$" runat="server" ValidationGroup="register" ></asp:RegularExpressionValidator>--%>
</div>
<div class="div">
	<div class="login_txt1">Comments:<br /></div>
	<textarea name="txtComments" id="txtComments" cols="60" runat="server" class="txtarea" rows="6" /><br /><br />
</div>

<div class="div" style="float:left; width:400px;">
	<asp:LinkButton Text="<img style='border:0px' src='/images/submit-button.png' alt='' title='' />" runat="server" ValidationGroup="register" ID="lnkSubmit"></asp:LinkButton>
	<br /><br /></div>
</div>
<asp:ValidationSummary ID="val" runat="server" HeaderText="You failed to correctly fill in your:" ValidationGroup="register"
ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="false" />
</asp:Content>

