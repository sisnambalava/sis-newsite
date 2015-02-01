<%@ Page Language="VB" MasterPageFile="~/admin/Menu.master" AutoEventWireup="false" CodeFile="homepagebanners.aspx.vb" Inherits="admin_homepagebanners" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table border="0" width="100%" cellpadding="0" cellspacing="0">
<tr><td>
<fieldset>
<legend><b>Home Page Banners</b></legend>
<div style="margin-top: 10px; margin-bottom: 10px; margin-left: 10px; margin-right: 10px">
    <asp:GridView ID="gvwBanner" CellPadding="0" CellSpacing="0" runat="server"
     Width="800px" AutoGenerateColumns="false" ShowFooter="false" ShowHeader="false" DataKeyNames="no"
     GridLines="Horizontal" AllowPaging="false" EmptyDataRowStyle-HorizontalAlign="center"
     AllowSorting="false">
    <Columns>
    <asp:TemplateField>
    <ItemTemplate>
    <asp:Label ID="lblSno" runat="Server" ></asp:Label>
    <table>
        <tr>
            <td><asp:Label ID="lblorderno" runat="server">Order No:</asp:Label></td>
            <td><asp:TextBox ID="txtorderno" runat="server" ReadOnly="true" Text='<%# DataBinder.Eval(Container, "DataItem.no") %>'></asp:TextBox></td>
            <td><asp:Label ID="lblalttxt" runat="server">Alternate Text :</asp:Label></td>
            <td><asp:TextBox ID="txtalttxt" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.alt") %>'></asp:TextBox></td>
        </tr>
        <tr>
            <td><asp:Label ID="lbllink" runat="server">Deep Link:</asp:Label></td>
            <td><asp:TextBox ID="txtlink" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.link") %>'></asp:TextBox></td>
            <td><asp:Label ID="lblexpdate" runat="server">Expiry Date:</asp:Label></td>
            <td><cc1:GMDatePicker ID="GMDatePicker1" runat="server" CalendarFont-Names="Arial" DateFormat="yyyy/MM/dd" InitialValueMode="Null" ShowNoneButton="false" InitialText= '<%# DataBinder.Eval(Container, "DataItem.expdate") %>'>
            <CalendarDayStyle Font-Size="9pt" />
            <CalendarTodayDayStyle BorderWidth="1" BorderColor="darkred" Font-Bold="true" />
            <CalendarOtherMonthDayStyle BackColor="whitesmoke" />
            <CalendarTitleStyle BackColor="#E0E0E0" Font-Names="Arial" Font-Size="9pt" />
            </cc1:GMDatePicker>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblimg" runat="server">Image:</asp:Label></td>
            <td><asp:Image runat="server" ID="imgOffer" onerror="this.onerror=null;this.src='images/noimage.jpg'" Width="202" Height="146" ImageUrl='<%# DataBinder.Eval(Container, "DataItem.image") %>'></asp:Image>
            <asp:Label ID="lblShipImage" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.image") %>' Visible="false"></asp:Label></td>
            <td><asp:Label ID="lblfupimg" runat="server">(OR) Upload Image :</asp:Label></td>
            <td><asp:FileUpload ID="flupload" runat="server" /></td>
        </tr>
        <tr>
            <td><asp:LinkButton ID="lnkbutImage" runat="server" ToolTip="Image" Text="Select Image" CommandArgument='<%# Bind("no") %> ' CommandName="EditImage" CausesValidation="false"></asp:LinkButton></td>
            <td></td> 
            <td></td> 
            <td><asp:Button ID="lnkbtnDelete" runat="server" Text="Delete" CommandArgument ='<%#Bind("no") %>' CommandName="Deleterow" CausesValidation="false"></asp:Button></td>
        </tr>
    </table>
    </ItemTemplate>
    </asp:TemplateField>
    </Columns>
    </asp:GridView>
    <asp:Label ID="lblmsg" runat="server"></asp:Label><br />
    <asp:HiddenField ID="hdnsno" runat="server" />
    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClientClick="javascript:return fn_Validate();" />
    
    <table>
        <tr><td>
            <asp:Panel ID="pnlimageselection" runat="server" CssClass="modalBox" Visible="false">
            <div align="center" class="PopUpPanel">
            <table runat="server" id="tblImage" bgcolor="#D8E1F2">
            <tr><td>
                <table align="left">
                <tr><td><asp:Button ID="BtnUpload" ForeColor="Blue" runat="server" Text="Select Image" /></td>
                <td><asp:Button ID="btnclose" ForeColor="Blue" runat="server" Text="Close" /></td>
                </tr>
                </table>
            </td></tr>
            <tr><td><asp:HiddenField ID="closebtn" runat="server" /></td></tr>
            <tr>
            <td>
                <div align="center" style="height: 510px; width: 950px; overflow-y: Auto">
                <table runat="server" id="Table1">
                <tr><td>
                <asp:DataList RepeatColumns="3" ID="DtLstImgDisplay" runat="server">
                    <ItemTemplate>
                    <table runat="server" style="border-bottom-style: none;" border="1" cellpadding="0" cellspacing="0" id="tblImg">
                    <tr><td>
                        <input type="radio" value='<%# Eval("Name") %>' id="rdoImageName" name="rdoImageName" />
                    </td><td><asp:Label ID="lblImageN" Text='<%# Eval("Name") %>' Font-Size="11px" runat="server"></asp:Label><br />
                    <img alt='<%# Eval("Name") %>' width="270" height="200" id="img1" runat="server" src='<%# "imgdisplay.aspx?imagename=" + Eval("Name") %>' /></td>
                    </tr>
                    </table>
                    </ItemTemplate>
                </asp:DataList>
                </td></tr>
                </table>
                <br />
                <img visible="false" src="" alt="" runat="server" id="Imgdisp" /><br />
                <asp:Label Visible="false" ID="lblMag" runat="server"></asp:Label>
                <asp:HiddenField ID="hdnselectimg" runat="server" />
                </div>
            </td></tr>
            </table>
            </div>
            </asp:Panel>
        </td></tr>
    </table>
<script type="text/javascript">
function fn_Validate1() {
var grid = document.getElementById("<%= gvwBanner.ClientID %>");
var totalrowcount = grid.rows.length; //GETTING THE ROW COUNT IN THE GRIDVIEW
var i;
var ctl1 = '', ctl2 = '', ctl3 = '', ctl4 = '', ctl5 = '', ctl6 = '';
var ErrorOffer1 = '', ErrorOffer2 = '', ErrorOffer3 = '', ErrorOffer4 = '', ErrorOffer5 = '', ErrorOffer6 = '';
for (i = 0; i < totalrowcount; i++)//GRIDVIEW ROW STARTS FROM 02 SO THE LOOP STARTS FROM 2 TILL TOTAL ROWCOUNT-1 SINCE LAST ROW IS TOTAL 
{
    j = i + 2;
   
    if (document.getElementById("gvwBanner_ctl0" + j + "_txtorderno").value != "") {
        if (document.getElementById("gvwBanner_ctl0" + j + "_txtalttxt").value == "") {
            ErrorOffer1 = "- alt text  \n";
            if (ctl1 == '') {
                ctl1 = document.getElementById("gvwBanner_ctl0" + j + "_txtalttxt");
            }
        }
        if (document.getElementById("gvwBanner_ctl0" + j + "_txtlink").value == "") {
            ErrorOffer5 = "- link  \n";
            if (ctl5 == '') {
                ctl5 = document.getElementById("gvwBanner_ctl0" + j + "_txtlink");
            }
        }
        if (document.getElementById("gvwBanner_ctl0" + j + "_txtdate").value == "") {
            ErrorOffer6 = "- expiry date  \n";
            if (ctl6 == '') {
                ctl6 = document.getElementById("gvwBanner_ctl0" + j + "_txtdate");
            }
        }
        //                      

    }
}
ErrorMsg = ErrorOffer1 + ErrorOffer2 + ErrorOffer3 + ErrorOffer4 + ErrorOffer5 + ErrorOffer6;
if (ErrorMsg != "") {
    ErrorMsg = "___________________________\n" +
"You failed to correctly fill in your:\n\n" +
ErrorMsg + "\n___________________________" +
"\nPlease re-enter and submit again!";
    alert(ErrorMsg);
    //THIS IS TO SET FOCUS ON THE CONTROL 
    if (ctl1 != '') {
        ctl1.focus();
    } else if (ctl2 != '') {
        ctl2.focus();
    } else if (ctl3 != '') {
        ctl3.focus();
    } else if (ctl4 != '') {
        ctl4.focus();
    } else if (ctl5 != '') {
        ctl5.focus();
    } else if (ctl6 != '') {
        ctl6.focus();
    }
    return false;
}

}    
</script>
</div>
</fieldset>
</td>
</tr>
</table>
</asp:Content>

