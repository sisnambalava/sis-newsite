Imports System.Xml
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Partial Class admin_articles
    Inherits System.Web.UI.Page
    Dim intReturn As String = "0"
    Dim clsRefdata As New clsRefdata
    Dim strFolderPath As String = Server.MapPath("/XML/articles/") '"C://work/sis/xml/articles/" '
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
            clsRefdata.PopulateArticleCategory(Me.ddlCategory)
            If strAId <> "" Then
                strFileName = strFolderPath & strAId & ".xml"
                If File.Exists(strFileName) Then
                    xmlMessage.Load(strFileName)
                    For Each xmlMessageNode In xmlMessage.SelectNodes("//Article")
                        txtPagetitle.Text = xmlMessageNode.SelectSingleNode("PageTitle").InnerText
                        txtKeywords.Value = xmlMessageNode.SelectSingleNode("MetaKeywords").InnerText
                        txtDescriptions.Value = xmlMessageNode.SelectSingleNode("MetaDescription").InnerText
                        txtH2Text.Text = xmlMessageNode.SelectSingleNode("ArticleHeading").InnerText
                        ADate.Date = xmlMessageNode.SelectSingleNode("ArticleDate").InnerText
                        txtAuthor.Text = xmlMessageNode.SelectSingleNode("Author").InnerText
                        ddlCategory.SelectedValue = xmlMessageNode.SelectSingleNode("category").InnerText
                        txtContent1.Value = xmlMessageNode.SelectSingleNode("MiniDescription").InnerText
                        txtContent2.Value = xmlMessageNode.SelectSingleNode("Maindescription").InnerText.Replace("<![CDATA[", "").Replace("]]>", "")
                    Next
                End If
            End If
        End If
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Try
            If Not Directory.Exists(strFolderPath) Then
                Directory.CreateDirectory(strFolderPath)
            End If

            'Dim intXmlCount As Integer = 0
            'intXmlCount = clsRefdata.fn_foldercount(strFolderPath)
            'intXmlCount = intXmlCount + 1
            'intReturn = intXmlCount

            If strAId <> "" Then
                Dim enc As Encoding
                Dim strADate As String
                Try

                    If ADate.Date.Month < 10 Then
                        strADate = ADate.Date.Year & "-0" & ADate.Date.Month & "-" & ADate.Date.Day
                    Else
                        strADate = ADate.Date.Year & "-" & ADate.Date.Month & "-" & ADate.Date.Day
                    End If

                    Dim objXMLTW As New XmlTextWriter(strFolderPath & Request.QueryString("aid").ToString() & ".xml", enc)
                    objXMLTW.WriteStartDocument()
                    objXMLTW.Formatting = Formatting.Indented
                    objXMLTW.WriteStartElement("Article")

                    objXMLTW.WriteStartElement("ArticleID")
                    objXMLTW.WriteString(strAId)
                    objXMLTW.WriteEndElement()

                    objXMLTW.WriteStartElement("category")
                    objXMLTW.WriteString(ddlCategory.SelectedItem.Value)
                    objXMLTW.WriteEndElement()

                    objXMLTW.WriteStartElement("PageTitle")
                    objXMLTW.WriteString(txtPagetitle.Text)
                    objXMLTW.WriteEndElement()

                    objXMLTW.WriteStartElement("MetaKeywords")
                    objXMLTW.WriteString(txtKeywords.Value)
                    objXMLTW.WriteEndElement()

                    objXMLTW.WriteStartElement("MetaDescription")
                    objXMLTW.WriteString(txtDescriptions.Value)
                    objXMLTW.WriteEndElement()

                    objXMLTW.WriteStartElement("ArticleHeading")
                    objXMLTW.WriteString(txtH2Text.Text)
                    objXMLTW.WriteEndElement()

                    objXMLTW.WriteStartElement("ArticleDate")
                    objXMLTW.WriteString(strADate)
                    objXMLTW.WriteEndElement()

                    objXMLTW.WriteStartElement("Author")
                    objXMLTW.WriteString(txtAuthor.Text)
                    objXMLTW.WriteEndElement()

                    objXMLTW.WriteStartElement("MiniDescription")
                    objXMLTW.WriteString(txtContent1.Value)
                    objXMLTW.WriteEndElement()

                    objXMLTW.WriteStartElement("Maindescription")
                    objXMLTW.WriteString("<![CDATA[" & txtContent2.Value & "]]>")
                    objXMLTW.WriteEndElement()

                    objXMLTW.WriteStartElement("AMonth")
                    objXMLTW.WriteString(DateAndTime.MonthName(ADate.Date.Month))
                    objXMLTW.WriteEndElement()

                    objXMLTW.WriteStartElement("AYear")
                    objXMLTW.WriteString(ADate.Date.Year)
                    objXMLTW.WriteEndElement()

                    objXMLTW.WriteEndElement()
                    objXMLTW.WriteEndDocument()
                    objXMLTW.Flush()
                    objXMLTW.Close()
                    Me.lblSuccess.Text = "Sucessfully Updated"
                Catch ex As Exception
                    Response.Write(ex.Message)
                End Try

            Else
                intReturn = clsRefdata.fn_number()

                If intReturn <> 0 Then
                    Dim enc As Encoding
                    Dim strADate As String
                    Try
                        If ADate.Date.Month < 10 Then
                            strADate = ADate.Date.Year & "-0" & ADate.Date.Month & "-" & ADate.Date.Day
                        Else
                            strADate = ADate.Date.Year & "-" & ADate.Date.Month & "-" & ADate.Date.Day
                        End If

                        Dim objXMLTW As New XmlTextWriter(Server.MapPath("/XML/articles/") & intReturn & ".xml", enc)
                        objXMLTW.WriteStartDocument()
                        objXMLTW.Formatting = Formatting.Indented
                        'Top level (Parent element)
                        objXMLTW.WriteStartElement("Article")

                        'Child elements, from request form
                        objXMLTW.WriteStartElement("ArticleID")
                        objXMLTW.WriteString(intReturn)
                        objXMLTW.WriteEndElement()

                        objXMLTW.WriteStartElement("category")
                        objXMLTW.WriteString(ddlCategory.SelectedItem.Value)
                        objXMLTW.WriteEndElement()

                        objXMLTW.WriteStartElement("PageTitle")
                        objXMLTW.WriteString(txtPagetitle.Text)
                        objXMLTW.WriteEndElement()

                        objXMLTW.WriteStartElement("MetaKeywords")
                        objXMLTW.WriteString(txtKeywords.Value)
                        objXMLTW.WriteEndElement()

                        objXMLTW.WriteStartElement("MetaDescription")
                        objXMLTW.WriteString(txtDescriptions.Value)
                        objXMLTW.WriteEndElement()


                        objXMLTW.WriteStartElement("ArticleHeading")
                        objXMLTW.WriteString(txtH2Text.Text)
                        objXMLTW.WriteEndElement()


                        objXMLTW.WriteStartElement("ArticleDate")
                        objXMLTW.WriteString(strADate)
                        objXMLTW.WriteEndElement()

                        objXMLTW.WriteStartElement("Author")
                        objXMLTW.WriteString(txtAuthor.Text)
                        objXMLTW.WriteEndElement()

                        objXMLTW.WriteStartElement("MiniDescription")
                        objXMLTW.WriteString(txtContent1.Value)
                        objXMLTW.WriteEndElement()

                        objXMLTW.WriteStartElement("Maindescription")
                        objXMLTW.WriteString("<![CDATA[" & txtContent2.Value & "]]>")
                        objXMLTW.WriteEndElement()

                        objXMLTW.WriteStartElement("AMonth")
                        objXMLTW.WriteString(DateAndTime.MonthName(ADate.Date.Month))
                        objXMLTW.WriteEndElement()

                        objXMLTW.WriteStartElement("AYear")
                        objXMLTW.WriteString(ADate.Date.Year)
                        objXMLTW.WriteEndElement()

                        objXMLTW.WriteEndElement() 'End top level element
                        objXMLTW.WriteEndDocument() 'End Document
                        objXMLTW.Flush() 'Write to file
                        objXMLTW.Close()
                        Me.lblSuccess.Text = "Sucessfully Updated"
                    Catch ex As Exception
                        Response.Write(ex.Message)
                    End Try

                Else
                    Me.lblSuccess.Text = "Not Sucessfully Updated"
                End If
            End If
        Catch Ex As Exception
            'lblXMLFile.Text = "The following error occurred: " & Ex.Message
        End Try
    End Sub
End Class
