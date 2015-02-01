Imports System
Imports System.Data
Imports System.Xml
Imports System.IO
Partial Class useful_info_tamil_panchangam
    Inherits System.Web.UI.Page
    Dim clsRefdata As New clsRefdata
    Dim strXMLFolderPath As String = Server.MapPath("/XML/panchangam/")
    Dim strXMLFolderPath1 As String = Server.MapPath("/XML/panchangam-1/")
    Dim strData As String = ""
    Dim strResults As String = ""
    Dim strResults1 As String = ""
    Dim strURL As String = ""
    Dim ltArticleCategories As Literal
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadPageDetails()
            LoadOtherEvents()
            ltArticleCategories = DirectCast(Page.Master.FindControl("ltArticleCategories"), Literal)
            ltArticleCategories.Text = clsRefdata.LoadUsefulInfo()
        End If
    End Sub
    Private Sub LoadOtherEvents()
        Dim Dss As New DataSet
        Dim dt As New DataTable
        Dim dtt As New DataTable
        Dim dst As New DataSet
        Dim EDate As String = ""
        Dim EDate1 As String = ""

        Dim EventID1 As New DataColumn("EventID", GetType(String))
        Dim EventName1 As New DataColumn("EventName", GetType(String))
        Dim ExpiryDate1 As New DataColumn("ExpiryDate", GetType(String))
        Dim EventSort1 As New DataColumn("EventSort", GetType(Date))

        dt.Columns.Add(EventID1)
        dt.Columns.Add(EventName1)
        dt.Columns.Add(ExpiryDate1)
        dt.Columns.Add(EventSort1)

        If Directory.Exists(strXMLFolderPath) Then
            Dim i As Integer = 0
            For Each strData In Directory.GetFiles(strXMLFolderPath)
                Dim DS As New DataSet
                If Path.GetExtension(strData) = ".xml" Then
                    DS.ReadXml(strData)
                    If DS.Tables(0).Rows.Count > 0 Then
                        Dim tdr, tdr1 As DataRow
                        tdr = dt.NewRow()
                        tdr1 = dtt.NewRow()
                        For Each dr As DataRow In DS.Tables(0).Rows()
                            Dim strDate As String = dr("ExpiryDate").ToString()
                            Dim dt_Todate As Date = CDate(Date.Today)
                            Dim dt_selectedDate As Date = CDate(strDate)
                            Dim DateState As Integer = Date.Compare(dt_selectedDate, dt_Todate)
                            If DateState < 0 Then
                            ElseIf DateState >= 0 Then
                                i += 1
                                tdr("EventID") = dr("EventID").ToString()
                                tdr("EventName") = dr("EventName").ToString()
                                tdr("ExpiryDate") = dr("ExpiryDate").ToString()
                                tdr("EventSort") = dt_selectedDate
                                dt.Rows.Add(tdr)
                            End If
                        Next
                    Else
                        'lblError.Text = "No Record Found..."
                    End If
                End If
            Next
            Dss.Tables.Add(dt)
            Dim dstArticle1 As New DataSet
            dstArticle1 = fn_desc(Dss)

            If dstArticle1.Tables(0).Rows.Count > 0 Then
                'strResults &= " <table cellpadding=""0"" cellspacing=""0"" border=""0"" width=""380px""> "
                'strResults &= "<tr><td align=""right"" colspan=""2"">&nbsp;</td></tr>" & vbCrLf
                strResults &= "<ul>" & vbCrLf
                For Each dr As DataRow In dstArticle1.Tables(0).Rows
                    If Day(dr("ExpiryDate")) < 10 Then
                        EDate = "0" & Day(dr("ExpiryDate")) & " " & Left(MonthName(Month(dr("ExpiryDate")), False), 3) & " " & Year(dr("ExpiryDate"))
                    Else
                        EDate = Day(dr("ExpiryDate")) & " " & Left(MonthName(Month(dr("ExpiryDate")), False), 3) & " " & Year(dr("ExpiryDate"))
                    End If
                    strURL = ConfigurationManager.AppSettings.Get("website") & "useful-info/tamil-panchangam/" & LCase(dr("EventID")) & ".aspx"
                    'strResults &= "<tr><td align=""left"" class=""content"">" & vbCrLf
                    strResults &= "<li><a href=""" & strURL & """>" & dr("EventName").ToString() & "</li>" & vbCrLf
                    'strResults &= "</tr> " & vbCrLf
                    'strResults &= "<tr><td align=""right"" colspan=""2"">&nbsp;</td></tr>" & vbCrLf
                Next
                'strResults &= " </table>"
                strResults &= " </ul>"
                ltdPanchangam.Text = strResults
            Else
                'ltdPanchangam.Text = "<span class=""content"" style=""color:Red;"">No Records Found</span>"
            End If

            If i = 0 Then
                'ltdPanchangam.Text = "<span class=""content"" style=""color:Red;"">No Records Found</span>"
            End If
            If strData = "" Then
                'ltdPanchangam.Text = "<span class=""content"" style=""color:Red;"">No Records Found</span>"
            End If
        Else
            'ltdPanchangam.Text = "<span class=""content"" style=""color:Red;"">No Records Found</span>"
        End If

    End Sub

    Private Function fn_desc(ByVal ds As DataSet) As DataSet

        Dim dwDs As DataView = ds.Tables(0).DefaultView
        dwDs.Sort = " EventSort asc "

        Dim dss As DataSet = New DataSet()
        Dim dt As DataTable = dwDs.Table.Clone()
        dss.Tables.Add(dt)
        Dim i As Integer
        For i = 0 To dwDs.Count - 1 Step i + 1
            Dim dv As DataRow = dwDs(i).Row
            dss.Tables(0).ImportRow(dv)
        Next

        dss.AcceptChanges()
        Return (dss)
    End Function

    Private Sub LoadPageDetails()
        Dim metaTagKeywords As HtmlMeta
        Dim metaTagDescription As HtmlMeta
        Dim metaRobots As HtmlMeta
        Dim strData As String = String.Empty
        Dim dt As New DataTable
        Dim DS As New DataSet
        Try

            metaTagKeywords = DirectCast(Page.Master.FindControl("keywords"), HtmlMeta)
            metaTagDescription = DirectCast(Page.Master.FindControl("description"), HtmlMeta)
            metaRobots = DirectCast(Page.Master.FindControl("robots"), HtmlMeta)
            metaRobots.Attributes.Add("content", "index, follow")

            Dim Title As New DataColumn("Title", GetType(String))
            Dim Keywords As New DataColumn("Keywords", GetType(String))
            Dim MetaDesc As New DataColumn("MetaDesc", GetType(String))
            Dim H1 As New DataColumn("H1", GetType(String))
            Dim Desc As New DataColumn("Desc", GetType(String))

            dt.Columns.Add(Title)
            dt.Columns.Add(Keywords)
            dt.Columns.Add(MetaDesc)
            dt.Columns.Add(H1)
            dt.Columns.Add(Desc)

            If Directory.Exists(strXMLFolderPath1) Then
                For Each strData In Directory.GetFiles(strXMLFolderPath1)
                    If Path.GetExtension(strData) = ".xml" Then
                        Dim xmlDoc As New XmlDocument
                        xmlDoc.Load(strData)

                        DS.ReadXml(strData)
                        If DS.Tables(0).Rows.Count > 0 Then
                            Dim tdr As DataRow
                            tdr = dt.NewRow()
                            For Each dr As DataRow In DS.Tables(0).Rows()
                                Page.Header.Title = dr("Title").ToString()
                                metaTagKeywords.Attributes.Add("content", dr("Keywords").ToString())
                                metaTagDescription.Attributes.Add("content", dr("MetaDesc").ToString())
                                ltH1.Text = dr("H1").ToString()
                                ltContent.Text = dr("Desc").ToString()
                            Next
                            dt.Rows.Add(tdr)
                        Else
                        End If
                    Else
                    End If
                Next
            Else

            End If
        Catch ex As Exception
        End Try
    End Sub

End Class
