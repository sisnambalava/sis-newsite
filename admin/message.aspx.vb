Imports System.Xml
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Partial Class admin_message
    Inherits System.Web.UI.Page
    Dim strXMLFolderPath As String = Server.MapPath("/XML/home/")
    Dim xmlMessage As New XmlDocument
    Dim xmlMessageNode As XmlNode
    Dim strFileName As String = ""
    Dim strMessageId As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("UserID") = "" Then
            Response.Redirect("default.aspx")
        End If
        strMessageId = Request.QueryString("mid")
        If Not IsPostBack Then
            If strMessageId <> "" Then
                strFileName = strXMLFolderPath & strMessageId & ".xml"
                If File.Exists(strFileName) Then
                    xmlMessage.Load(strFileName)
                    For Each xmlMessageNode In xmlMessage.SelectNodes("//Message")
                        txtMessage.Text = xmlMessageNode.SelectSingleNode("Description").InnerText
                        txtExpiryDate.Text = xmlMessageNode.SelectSingleNode("ExpiryDate").InnerText
                    Next
                End If
            End If
        End If
    End Sub
    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        If Not Directory.Exists(strXMLFolderPath) Then
            Directory.CreateDirectory(strXMLFolderPath)
        End If
        If Request.QueryString("mid") <> "" Then
            Dim enc As Encoding
            Dim objXMLTW As New XmlTextWriter(strXMLFolderPath & Request.QueryString("mid").ToString() & ".xml", enc)
            objXMLTW.WriteStartDocument()
            objXMLTW.WriteStartElement("Message")

            objXMLTW.WriteStartElement("MessageID")
            objXMLTW.WriteString(Request.QueryString("mid").ToString())
            objXMLTW.WriteEndElement()

            objXMLTW.WriteStartElement("Description")
            objXMLTW.WriteString(txtMessage.Text)
            objXMLTW.WriteEndElement()

            objXMLTW.WriteStartElement("ExpiryDate")
            objXMLTW.WriteString(txtExpiryDate.Text)
            objXMLTW.WriteEndElement()

            objXMLTW.WriteEndElement()
            objXMLTW.WriteEndDocument()
            objXMLTW.Flush()
            objXMLTW.Close()
        Else
            Dim intXmlCount As Integer = 0
            intXmlCount = fn_foldercount(strXMLFolderPath)
            intXmlCount = intXmlCount + 1
            If intXmlCount > 0 Then
                Dim enc As Encoding
                Dim objXMLTW As New XmlTextWriter(strXMLFolderPath & intXmlCount.ToString() & ".xml", enc)
                objXMLTW.WriteStartDocument()
                objXMLTW.WriteStartElement("Message")

                objXMLTW.WriteStartElement("MessageID")
                objXMLTW.WriteString(intXmlCount.ToString())
                objXMLTW.WriteEndElement()

                objXMLTW.WriteStartElement("Description")
                objXMLTW.WriteString(txtMessage.Text)
                objXMLTW.WriteEndElement()

                objXMLTW.WriteStartElement("ExpiryDate")
                objXMLTW.WriteString(txtExpiryDate.Text)
                objXMLTW.WriteEndElement()

                objXMLTW.WriteEndElement()
                objXMLTW.WriteEndDocument()
                objXMLTW.Flush()
                objXMLTW.Close()
            End If
        End If
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
