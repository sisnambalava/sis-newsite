Imports System.Xml
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Partial Class admin_newsletter
    Inherits System.Web.UI.Page
    Dim intReturn As String = "0"
    Dim clsRefdata As New clsRefdata
    Dim strFolderPath As String = Server.MapPath("/XML/newsletter/")
    Dim strAId As String = ""
    Dim strFileName As String = ""
    Dim xmlMessage As New XmlDocument
    Dim xmlMessageNode As XmlNode

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("UserID") = "" Then
            Response.Redirect("default.aspx")
        End If
        strAId = Request.QueryString("aid")
        If Not IsPostBack Then
            clsRefdata.PopulateMonths(Me.ddlMonth)
            clsRefdata.PopulateYears(0, 0, ddlYear)
            If strAId <> "" Then
                strFileName = strFolderPath & strAId & ".xml"
                If File.Exists(strFileName) Then
                    xmlMessage.Load(strFileName)
                    For Each xmlMessageNode In xmlMessage.SelectNodes("//Newsletter")
                        txtPagetitle.Text = xmlMessageNode.SelectSingleNode("PageTitle").InnerText
                        txtDescriptions.Value = xmlMessageNode.SelectSingleNode("MetaDescription").InnerText
                        ADate.Date = xmlMessageNode.SelectSingleNode("ExpiryDate").InnerText
                        ddlMonth.SelectedValue = xmlMessageNode.SelectSingleNode("NMonth").InnerText
                        ddlYear.SelectedValue = xmlMessageNode.SelectSingleNode("NYear").InnerText
                        txtContent2.Value = xmlMessageNode.SelectSingleNode("Maindescription").InnerText.Replace("<![CDATA[", "").Replace("]]>", "")
                    Next
                End If
                btnSubmit.Visible = False
            End If
        End If
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Try
            If Not Directory.Exists(strFolderPath) Then
                Directory.CreateDirectory(strFolderPath)
            End If
            Dim enc As Encoding
            Dim strADate As String
            Dim NMonth As String = ""
            Dim NYear As String = ""
            Try
                If ADate.Date.Month < 10 Then
                    strADate = ADate.Date.Year & "-0" & ADate.Date.Month & "-" & ADate.Date.Day
                Else
                    strADate = ADate.Date.Year & "-" & ADate.Date.Month & "-" & ADate.Date.Day
                End If

                intReturn = clsRefdata.fn_number()

                NMonth = LCase(ddlMonth.SelectedItem.Text)
                NYear = ddlYear.SelectedItem.Text

                Dim objXMLTW As New XmlTextWriter(Server.MapPath("/XML/newsletter/") & NMonth & "-" & NYear & ".xml", enc)
                objXMLTW.WriteStartDocument()
                objXMLTW.Formatting = Formatting.Indented
                'Top level (Parent element)
                objXMLTW.WriteStartElement("Newsletter")

                'Child elements, from request form
                objXMLTW.WriteStartElement("NletterID")
                objXMLTW.WriteString(intReturn)
                objXMLTW.WriteEndElement()

                objXMLTW.WriteStartElement("PageTitle")
                objXMLTW.WriteString(txtPagetitle.Text)
                objXMLTW.WriteEndElement()

                objXMLTW.WriteStartElement("MetaDescription")
                objXMLTW.WriteString(txtDescriptions.Value)
                objXMLTW.WriteEndElement()

                objXMLTW.WriteStartElement("Heading")
                objXMLTW.WriteString("Newsletter " & ddlMonth.SelectedItem.Text & " " & ddlYear.SelectedItem.Text)
                objXMLTW.WriteEndElement()

                objXMLTW.WriteStartElement("ExpiryDate")
                objXMLTW.WriteString(strADate)
                objXMLTW.WriteEndElement()

                objXMLTW.WriteStartElement("NMonth")
                objXMLTW.WriteString(ddlMonth.SelectedItem.Text)
                objXMLTW.WriteEndElement()

                objXMLTW.WriteStartElement("NYear")
                objXMLTW.WriteString(ddlYear.SelectedItem.Text)
                objXMLTW.WriteEndElement()

                objXMLTW.WriteStartElement("Maindescription")
                objXMLTW.WriteString("<![CDATA[" & txtContent2.Value & "]]>")
                objXMLTW.WriteEndElement()

                objXMLTW.WriteEndElement() 'End top level element
                objXMLTW.WriteEndDocument() 'End Document
                objXMLTW.Flush() 'Write to file
                objXMLTW.Close()
                Me.lblSuccess.Text = "Sucessfully Updated"
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try

        Catch Ex As Exception
            'lblXMLFile.Text = "The following error occurred: " & Ex.Message
        End Try
    End Sub

End Class
