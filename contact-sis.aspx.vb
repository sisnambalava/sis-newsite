Imports System
Imports System.Data
Imports System.Xml
Imports System.IO
Partial Class contact_sis
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
        Page.Header.Title = "Contact South Indian Society, United Kingdom"

        'meta keywords
        metaTagKeywords = DirectCast(Page.Master.FindControl("keywords"), HtmlMeta)
        metaTagKeywords.Attributes.Add("content", "sis uk, south indian society london, contact sis")

        'meta description
        metaTagDescription = DirectCast(Page.Master.FindControl("description"), HtmlMeta)
        metaTagDescription.Attributes.Add("content", "South Indian Society, London. Write to us your comments or feedback.")

        'meta robots
        metaRobots = DirectCast(Page.Master.FindControl("robots"), HtmlMeta)
        metaRobots.Attributes.Add("content", "index, follow")
       

    End Sub

    Protected Sub lnkSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSubmit.Click
        SISContact()
        'StoreContact()
        Response.Redirect("thank-you.aspx")
    End Sub

    Private Sub SISContact()
        Dim strResults As String = ""
        Try
            strResults &= " New Contact form received at " & Now() & vbCrLf
            strResults &= "<br /><hr />"
            strResults &= "<table border =""0"">" & vbCrLf
            strResults &= "<tr><td>" & vbCrLf
            strResults &= "Name :" & vbCrLf
            strResults &= "</td>" & vbCrLf
            strResults &= "<td>" & vbCrLf
            strResults &= txtName.Text & vbCrLf
            strResults &= "</td></tr>" & vbCrLf
            strResults &= "<tr><td>" & vbCrLf
            strResults &= "Phone :" & vbCrLf
            strResults &= "</td>" & vbCrLf
            strResults &= "<td>" & vbCrLf
            strResults &= txtPhone.Text & vbCrLf
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
            clsrefdata.SendMail("smtp.gmail.com", 465, "sisnambalava@gmail.com", "ind1ans0ciety27", "SIS London", "sisnambalava@gmail.com", "SIS Trustee", "sisnambalava@gmail.com", "karthikraghav@gmail.com, shankar@vshank77.com", "New Contact Form Received", strResults, True)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub StoreContact()
        Dim enc As Encoding
        Dim strXMLSignup As String = Server.MapPath("XML/contact/")
        Dim strXMLSignupFile As String = "contact.xml"
        Try
            If Not Directory.Exists(strXMLSignup) Then
                Directory.CreateDirectory(strXMLSignup)
            End If

            If Not File.Exists(strXMLSignup & strXMLSignupFile) Then
                Dim objXMLTW As New XmlTextWriter(strXMLSignup & strXMLSignupFile, enc)
                With objXMLTW
                    .WriteStartDocument()
                    .Formatting = Formatting.Indented
                    .WriteStartElement("ContactDetails")
                    .WriteStartElement("Contact")

                    .WriteStartElement("Name")
                    .WriteString(txtName.Text)
                    .WriteEndElement()

                    .WriteStartElement("Email")
                    .WriteString(txtEmail.Text)
                    .WriteEndElement()

                    .WriteStartElement("Phone")
                    .WriteString(txtPhone.Text)
                    .WriteEndElement()

                    .WriteStartElement("Date")
                    .WriteString(Now())
                    .WriteEndElement()

                    .WriteStartElement("IPAddress")
                    .WriteString(Request.ServerVariables("REMOTE_ADDR"))
                    .WriteEndElement()

                    .WriteEndElement()
                    .WriteEndElement()

                    .WriteEndDocument()
                End With
                objXMLTW.Flush()
                objXMLTW.Close()
            Else
                Dim xmlDoc As New XmlDocument()
                xmlDoc.Load(strXMLSignup & strXMLSignupFile)
                Dim itemMain As XmlElement = xmlDoc.CreateElement("Contact")

                Dim ItemName As XmlElement = xmlDoc.CreateElement("Name")
                ItemName.InnerXml = txtName.Text
                itemMain.AppendChild(ItemName)

                Dim ItemEmail As XmlElement = xmlDoc.CreateElement("Email")
                ItemEmail.InnerXml = txtEmail.Text
                itemMain.AppendChild(ItemEmail)

                Dim Itemphone As XmlElement = xmlDoc.CreateElement("Phone")
                Itemphone.InnerXml = txtPhone.Text
                itemMain.AppendChild(Itemphone)

                Dim ItemDate As XmlElement = xmlDoc.CreateElement("Date")
                ItemDate.InnerXml = Now()
                itemMain.AppendChild(ItemDate)

                Dim ItemIP As XmlElement = xmlDoc.CreateElement("IPAddress")
                ItemIP.InnerXml = Request.ServerVariables("REMOTE_ADDR")
                itemMain.AppendChild(ItemIP)

                xmlDoc.DocumentElement.AppendChild(itemMain)
                Dim xtw As XmlTextWriter
                xtw = New XmlTextWriter(strXMLSignup & strXMLSignupFile, Nothing)
                xtw.Formatting = Formatting.Indented
                xmlDoc.WriteContentTo(xtw)
                xtw.Close()
            End If
        Catch ex As Exception
        End Try
    End Sub

End Class
