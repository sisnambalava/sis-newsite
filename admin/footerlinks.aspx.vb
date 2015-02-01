Imports System.Xml
Imports System
Imports System.Data
Imports System.IO
Partial Class admin_footerlinks
    Inherits System.Web.UI.Page
    Dim strXMLFolderPath As String = Server.MapPath("/XML/footer/")
    Dim xmlMessage As New XmlDocument
    Dim xmlMessageNode As XmlNode
    Dim strFileName As String = ""
    Dim strMessageId As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("UserID") = "" Then
            Response.Redirect("default.aspx")
        End If
        strMessageId = "1"
        If Not IsPostBack Then
            If strMessageId <> "" Then
                strFileName = strXMLFolderPath & strMessageId & ".xml"
                If File.Exists(strFileName) Then
                    xmlMessage.Load(strFileName)
                    For Each xmlMessageNode In xmlMessage.SelectNodes("//Message")
                        txtMessage1.Text = xmlMessageNode.SelectSingleNode("Description1").InnerText
                        txtURL1.Text = xmlMessageNode.SelectSingleNode("url1").InnerText
                        txtMessage2.Text = xmlMessageNode.SelectSingleNode("Description2").InnerText
                        txtURL2.Text = xmlMessageNode.SelectSingleNode("url2").InnerText
                        txtMessage3.Text = xmlMessageNode.SelectSingleNode("Description3").InnerText
                        txtURL3.Text = xmlMessageNode.SelectSingleNode("url3").InnerText
                        txtMessage4.Text = xmlMessageNode.SelectSingleNode("Description4").InnerText
                        txtURL4.Text = xmlMessageNode.SelectSingleNode("url4").InnerText
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
        Dim objXMLTW As New XmlTextWriter(strXMLFolderPath & "1.xml", enc)
        objXMLTW.WriteStartDocument()
        objXMLTW.Formatting = Formatting.Indented
        objXMLTW.WriteStartElement("Message")

        objXMLTW.WriteStartElement("MessageID")
        objXMLTW.WriteString("1")
        objXMLTW.WriteEndElement()

        objXMLTW.WriteStartElement("Description1")
        objXMLTW.WriteString(txtMessage1.Text)
        objXMLTW.WriteEndElement()

        objXMLTW.WriteStartElement("url1")
        objXMLTW.WriteString(txtURL1.Text)
        objXMLTW.WriteEndElement()

        objXMLTW.WriteStartElement("Description2")
        objXMLTW.WriteString(txtMessage2.Text)
        objXMLTW.WriteEndElement()

        objXMLTW.WriteStartElement("url2")
        objXMLTW.WriteString(txtURL2.Text)
        objXMLTW.WriteEndElement()

        objXMLTW.WriteStartElement("Description3")
        objXMLTW.WriteString(txtMessage3.Text)
        objXMLTW.WriteEndElement()

        objXMLTW.WriteStartElement("url3")
        objXMLTW.WriteString(txtURL3.Text)
        objXMLTW.WriteEndElement()

        objXMLTW.WriteStartElement("Description4")
        objXMLTW.WriteString(txtMessage4.Text)
        objXMLTW.WriteEndElement()

        objXMLTW.WriteStartElement("url4")
        objXMLTW.WriteString(txtURL4.Text)
        objXMLTW.WriteEndElement()

        objXMLTW.WriteEndElement()
        objXMLTW.WriteEndDocument()
        objXMLTW.Flush()
        objXMLTW.Close()
    End Sub
    Public Function fn_foldercount(ByVal folderpath As String) As Integer
        Dim count As Integer = 0
        For Each d As String In Directory.GetFiles(folderpath)
            If Path.GetExtension(d) = ".xml" Then
                count += 1
            End If
        Next
        Return count
    End Function
End Class
