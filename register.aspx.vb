Imports System
Imports System.Data
Imports System.Xml
Imports System.IO
Partial Class register
    Inherits System.Web.UI.Page
    Dim clsrefdata As New clsRefdata
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadPageDetails()
        End If
    End Sub
    Private Sub LoadPageDetails()
        Dim metaTagKeywords As HtmlMeta
        Dim metaTagDescription As HtmlMeta
        Dim metaRobots As HtmlMeta

        'page title
        Page.Header.Title = "Register yourself with South Indian Society"

        'meta keywords
        metaTagKeywords = DirectCast(Page.Master.FindControl("keywords"), HtmlMeta)
        metaTagKeywords.Attributes.Add("content", "sis uk, south indian society london, sis membership")

        'meta description
        metaTagDescription = DirectCast(Page.Master.FindControl("description"), HtmlMeta)
        metaTagDescription.Attributes.Add("content", "South Indian Society. Register your details to be a part of South Indian Society family.")

        'meta robots
        metaRobots = DirectCast(Page.Master.FindControl("robots"), HtmlMeta)
        metaRobots.Attributes.Add("content", "index, follow")

    End Sub

    Protected Sub lnkSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSubmit.Click
        SISContact()
        Response.Redirect("thank-you.aspx")
    End Sub

    Private Sub SISContact()
        Dim strResults As String = ""
        Try
            strResults &= " New Registration form received at " & Now() & vbCrLf
            strResults &= "<br /><hr />"
            strResults &= "<table border =""0"">" & vbCrLf
            strResults &= "<tr><td>" & vbCrLf
            strResults &= "Name :" & vbCrLf
            strResults &= "</td>" & vbCrLf
            strResults &= "<td>" & vbCrLf
            strResults &= txtName.Text & vbCrLf
            strResults &= "</td></tr>" & vbCrLf
            strResults &= "<tr><td valign=""top"">" & vbCrLf
            strResults &= "Address :" & vbCrLf
            strResults &= "</td>" & vbCrLf
            strResults &= "<td>" & vbCrLf
            strResults &= txtAddress1.Text & "<br />" & vbCrLf
            strResults &= txtAddress2.Text & "<br />" & vbCrLf
            strResults &= txtAddress3.Text & "<br />" & vbCrLf
            strResults &= txtPostcode.Text & vbCrLf
            strResults &= "</td></tr>" & vbCrLf
            strResults &= "<tr><td>" & vbCrLf
            strResults &= "Phone :" & vbCrLf
            strResults &= "</td>" & vbCrLf
            strResults &= "<td>" & vbCrLf
            strResults &= txtPhone.Text & vbCrLf
            strResults &= "</td></tr>" & vbCrLf
            strResults &= "<tr><td>" & vbCrLf
            strResults &= "Mobile :" & vbCrLf
            strResults &= "</td>" & vbCrLf
            strResults &= "<td>" & vbCrLf
            strResults &= txtMobile.Text & vbCrLf
            strResults &= "</td></tr>" & vbCrLf
            strResults &= "<tr><td>" & vbCrLf
            strResults &= "Email :" & vbCrLf
            strResults &= "</td>" & vbCrLf
            strResults &= "<td>" & vbCrLf
            strResults &= txtEmail.Text & vbCrLf
            strResults &= "</td></tr>" & vbCrLf
            strResults &= "<tr><td valign=""top"">" & vbCrLf
            strResults &= "Comments :" & vbCrLf
            strResults &= "</td>" & vbCrLf
            strResults &= "<td>" & vbCrLf
            strResults &= txtComments.Value & vbCrLf
            strResults &= "</td></tr>" & vbCrLf
            strResults &= "</table>" & vbCrLf
            clsrefdata.SendMail("smtp.gmail.com", 465, "sisnambalava@gmail.com", "ind1ans0ciety27", "SIS London", "sisnambalava@gmail.com", "SIS Trustee", "sisnambalava@gmail.com", "karthikraghav@gmail.com, shankar@vshank77.com", "New Registration Form Received", strResults, True)

        Catch ex As Exception

        End Try
    End Sub

End Class
