<%@ Page Language="VB" MasterPageFile="~/sis.master" AutoEventWireup="false" CodeFile="articledetails.aspx.vb" Inherits="articles_articledetails"%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h1><asp:Literal ID="lblH2" runat="server"></asp:Literal></h1>
<!-- AddThis Button BEGIN -->
<div class="addthis_toolbox addthis_default_style " style="width:150px;float:right;">
<a class="addthis_button_preferred_1"></a>
<a class="addthis_button_preferred_2"></a>
<a class="addthis_button_preferred_3"></a>
<a class="addthis_button_preferred_4"></a>
<a class="addthis_button_compact"></a>
<a class="addthis_counter addthis_bubble_style"></a>
</div>
<script type="text/javascript" src="http://s7.addthis.com/js/250/addthis_widget.js#pubid=sisnambalava"></script>
<!-- AddThis Button END -->
<asp:Literal ID="lblDate" runat="server"></asp:Literal><br />
<asp:Literal  ID="lblContent" runat="server"></asp:Literal>
<br />
<div class="more_txt" style="float:right; font-size:1.4em;"><a href="javascript:history.go(-1)">&laquo; Previous page</a></div>
</asp:Content>

