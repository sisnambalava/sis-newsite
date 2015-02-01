Imports System.Xml
Imports System
Imports System.Data
Imports System.IO
Partial Class panchangam
    Inherits System.Web.UI.Page
    Dim strXMLFolderPath As String = Server.MapPath("/XML/panchangam-1/")
    Dim xmlMessage As New XmlDocument
    Dim xmlMessageNode As XmlNode
    Dim strFileName As String = ""
    Dim strMessageId As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("UserID") = "" Then
            Response.Redirect("default.aspx")
        End If
        strMessageId = "landing"
        If Not IsPostBack Then
            If strMessageId <> "" Then
                strFileName = strXMLFolderPath & strMessageId & ".xml"
                If File.Exists(strFileName) Then
                    xmlMessage.Load(strFileName)
                    For Each xmlMessageNode In xmlMessage.SelectNodes("//Message")
                        txtPagetitle.Text = xmlMessageNode.SelectSingleNode("Title").InnerText
                        txtKeywords.Value = xmlMessageNode.SelectSingleNode("Keywords").InnerText
                        txtDescriptions.Value = xmlMessageNode.SelectSingleNode("MetaDesc").InnerText
                        txtH1.Text = xmlMessageNode.SelectSingleNode("H1").InnerText
                        txtContent.Value = xmlMessageNode.SelectSingleNode("Desc").InnerText
                    Next
                End If
            End If
        End If
    End Sub
    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        If Not Directory.Exists(strXMLFolderPath) Then
            Directory.CreateDirectory(strXMLFolderPath)
        End If
        Dim enc As Encoding
        Dim objXMLTW As New XmlTextWriter(strXMLFolderPath & "landing.xml", enc)
        objXMLTW.WriteStartDocument()
        objXMLTW.Formatting = Formatting.Indented
        objXMLTW.WriteStartElement("Message")

        objXMLTW.WriteStartElement("MessageID")
        objXMLTW.WriteString("landing")
        objXMLTW.WriteEndElement()

        objXMLTW.WriteStartElement("Title")
        objXMLTW.WriteString(txtPagetitle.Text)
        objXMLTW.WriteEndElement()

        objXMLTW.WriteStartElement("Keywords")
        objXMLTW.WriteString(txtKeywords.Value)
        objXMLTW.WriteEndElement()

        objXMLTW.WriteStartElement("MetaDesc")
        objXMLTW.WriteString(txtDescriptions.Value)
        objXMLTW.WriteEndElement()

        objXMLTW.WriteStartElement("H1")
        objXMLTW.WriteString(txtH1.Text)
        objXMLTW.WriteEndElement()

        objXMLTW.WriteStartElement("Desc")
        objXMLTW.WriteString(txtContent.Value)
        objXMLTW.WriteEndElement()

        objXMLTW.WriteEndElement()
        objXMLTW.WriteEndDocument()
        objXMLTW.Flush()
        objXMLTW.Close()
    End Sub
End Class
