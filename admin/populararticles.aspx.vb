Imports System.Xml
Imports System
Imports System.Data
Imports System.IO
Partial Class admin_populararticles
    Inherits System.Web.UI.Page
    Dim strXMLFolderPath As String = Server.MapPath("/XML/popular/")
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
                        txtMessage5.Text = xmlMessageNode.SelectSingleNode("Description5").InnerText
                        txtURL5.Text = xmlMessageNode.SelectSingleNode("url5").InnerText
                        txtMessage6.Text = xmlMessageNode.SelectSingleNode("Description6").InnerText
                        txtURL6.Text = xmlMessageNode.SelectSingleNode("url6").InnerText
                        txtMessage7.Text = xmlMessageNode.SelectSingleNode("Description7").InnerText
                        txtURL7.Text = xmlMessageNode.SelectSingleNode("url7").InnerText
                        txtMessage8.Text = xmlMessageNode.SelectSingleNode("Description8").InnerText
                        txtURL8.Text = xmlMessageNode.SelectSingleNode("url8").InnerText
                        txtMessage9.Text = xmlMessageNode.SelectSingleNode("Description9").InnerText
                        txtURL9.Text = xmlMessageNode.SelectSingleNode("url9").InnerText
                        txtMessage10.Text = xmlMessageNode.SelectSingleNode("Description10").InnerText
                        txtURL10.Text = xmlMessageNode.SelectSingleNode("url10").InnerText
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

        objXMLTW.WriteStartElement("Description5")
        objXMLTW.WriteString(txtMessage5.Text)
        objXMLTW.WriteEndElement()

        objXMLTW.WriteStartElement("url5")
        objXMLTW.WriteString(txtURL5.Text)
        objXMLTW.WriteEndElement()

        objXMLTW.WriteStartElement("Description6")
        objXMLTW.WriteString(txtMessage6.Text)
        objXMLTW.WriteEndElement()

        objXMLTW.WriteStartElement("url6")
        objXMLTW.WriteString(txtURL6.Text)
        objXMLTW.WriteEndElement()

        objXMLTW.WriteStartElement("Description7")
        objXMLTW.WriteString(txtMessage7.Text)
        objXMLTW.WriteEndElement()

        objXMLTW.WriteStartElement("url7")
        objXMLTW.WriteString(txtURL7.Text)
        objXMLTW.WriteEndElement()

        objXMLTW.WriteStartElement("Description8")
        objXMLTW.WriteString(txtMessage8.Text)
        objXMLTW.WriteEndElement()

        objXMLTW.WriteStartElement("url8")
        objXMLTW.WriteString(txtURL8.Text)
        objXMLTW.WriteEndElement()

        objXMLTW.WriteStartElement("Description9")
        objXMLTW.WriteString(txtMessage9.Text)
        objXMLTW.WriteEndElement()

        objXMLTW.WriteStartElement("url9")
        objXMLTW.WriteString(txtURL9.Text)
        objXMLTW.WriteEndElement()

        objXMLTW.WriteStartElement("Description10")
        objXMLTW.WriteString(txtMessage10.Text)
        objXMLTW.WriteEndElement()

        objXMLTW.WriteStartElement("url10")
        objXMLTW.WriteString(txtURL10.Text)
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
