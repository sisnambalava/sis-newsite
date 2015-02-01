<%@ Page Language="VB" MasterPageFile="~/sis.master" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="sis_events_default"%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h1>South Indian Society Events</h1>
<p>Please find below South Indian Society forthcoming events happening in and around London, United Kingdom.</p>
<div>
    <div class="box" style="width:600px;float:left; margin-bottom:30px;">    
    <div class="boxheader" style="margin-top:5px;">Upcoming Events</div>
    <div style=" font-size: 0.9em;">
    <asp:Literal ID="ltdabove" runat="server"></asp:Literal>
    </div>
    </div>
    <div class="box" style="width:600px;float:left;">    
    <div class="boxheader" style="margin-top:5px;">Previous Events</div>
    <div style=" font-size: 0.9em;"><asp:Literal ID="ltdbelow" runat="server"></asp:Literal></div>
    </div>
</div>

</asp:Content>

