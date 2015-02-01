Imports System.Xml
Imports System
Imports System.Data
Imports System.IO
Partial Class sis_events_default
    Inherits System.Web.UI.Page

    Dim clsRefdata As New clsRefdata
    Dim strXMLFolderPath As String = Server.MapPath("/XML/events/")
    Dim strData As String = ""
    Dim strResults As String = ""
    Dim strResults1 As String = ""
    Dim strURL As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadPageDetails()
            LoadSISEvents()
        End If
    End Sub
    Private Sub LoadSISEvents()
        Dim Dss As New DataSet
        Dim dt As New DataTable
        Dim dtt As New DataTable
        Dim dst As New DataSet
        Dim EDate As String = ""
        Dim EDate1 As String = ""

        Dim EventID1 As New DataColumn("EventID", GetType(String))
        Dim EventHeading1 As New DataColumn("EventHeading", GetType(String))
        Dim EventDate1 As New DataColumn("EventDate", GetType(String))
        Dim EventSort1 As New DataColumn("EventSort", GetType(Date))

        Dim EventID2 As New DataColumn("EventID", GetType(String))
        Dim EventHeading2 As New DataColumn("EventHeading", GetType(String))
        Dim EventDate2 As New DataColumn("EventDate", GetType(String))
        Dim EventSort2 As New DataColumn("EventSort", GetType(Date))

        dt.Columns.Add(EventID1)
        dt.Columns.Add(EventHeading1)
        dt.Columns.Add(EventDate1)
        dt.Columns.Add(EventSort1)

        dtt.Columns.Add(EventID2)
        dtt.Columns.Add(EventHeading2)
        dtt.Columns.Add(EventDate2)
        dtt.Columns.Add(EventSort2)

        If Directory.Exists(strXMLFolderPath) Then
            For Each strData In Directory.GetFiles(strXMLFolderPath)
                Dim DS As New DataSet
                If Path.GetExtension(strData) = ".xml" Then
                    DS.ReadXml(strData)
                    If DS.Tables(0).Rows.Count > 0 Then
                        Dim tdr, tdr1 As DataRow
                        tdr = dt.NewRow()
                        tdr1 = dtt.NewRow()
                        For Each dr As DataRow In DS.Tables(0).Rows()
                            Dim strDate As String = dr("EventDate").ToString()
                            Dim dt_Todate As Date = CDate(Date.Today)
                            Dim dt_selectedDate As Date = CDate(strDate)
                            Dim DateState As Integer = Date.Compare(dt_selectedDate, dt_Todate)
                            If DateState < 0 Then
                                tdr1("EventID") = dr("EventID").ToString()
                                tdr1("EventHeading") = dr("EventHeading").ToString()
                                tdr1("EventDate") = dr("EventDate").ToString()
                                tdr1("EventSort") = dt_selectedDate
                                dtt.Rows.Add(tdr1)
                            ElseIf DateState >= 0 Then
                                tdr("EventID") = dr("EventID").ToString()
                                tdr("EventHeading") = dr("EventHeading").ToString()
                                tdr("EventDate") = dr("EventDate").ToString()
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

            'Dim dstArticle As New DataSet

            If dstArticle1.Tables(0).Rows.Count > 0 Then
                strResults &= " <table cellpadding=""0"" cellspacing=""0"" border=""0"" width=""600px""> "
                For Each dr As DataRow In dstArticle1.Tables(0).Rows
                    'EDate = Day(dr("EventDate")) & " " & Left(MonthName(Month(dr("EventDate")), False), 3) & " " & Year(dr("EventDate"))
                    If Day(dr("EventDate")) < 10 Then
                        EDate = "0" & Day(dr("EventDate")) & " " & Left(MonthName(Month(dr("EventDate")), False), 3) & " " & Year(dr("EventDate"))
                    Else
                        EDate = Day(dr("EventDate")) & " " & Left(MonthName(Month(dr("EventDate")), False), 3) & " " & Year(dr("EventDate"))
                    End If
                    strURL = clsRefdata.GenerateURL(dr("EventHeading").ToString(), dr("EventID"), "sis-events/" & "", "-")
                    strResults &= "<tr><td align=""left"" width=""320px"">" & vbCrLf
                    strResults &= dr("EventHeading").ToString() & "</td>" & vbCrLf
                    strResults &= "<td>" & EDate & "</td>" & vbCrLf
                    strResults &= "<td><a href=""" & strURL & """>View Event</a></td>" & vbCrLf
                    strResults &= "</tr> " & vbCrLf
                    strResults &= "<tr><td align=""right"" colspan=""3"">&nbsp;</td></tr>" & vbCrLf
                Next
                strResults &= " </table>"
                ltdabove.Text = strResults
            Else
                ltdabove.Text = "<span class=""box-txt"" style=""color:Red;"">No Events Found</span>"
            End If

            If strData = "" Then
                ltdabove.Text = "<span class=""box-txt"" style=""color:Red;"">No Events Found</span>"
            End If
        Else
            ltdabove.Text = "<span class=""box-txt"" style=""color:Red;"">No Events Found</span>"
        End If

        Dim dstArticle As New DataSet
        dst.Tables.Add(dtt)
        dstArticle = fn_desc(dst)

        If dstArticle.Tables(0).Rows.Count > 0 Then
            strResults1 &= " <table cellpadding=""0"" cellspacing=""0"" border=""0"" width=""600px""> "
            For Each dr As DataRow In dstArticle.Tables(0).Rows
                'EDate1 = Day(dr("EventDate")) & " " & Left(MonthName(Month(dr("EventDate")), False), 3) & " " & Year(dr("EventDate"))
                If Day(dr("EventDate")) < 10 Then
                    EDate1 = "0" & Day(dr("EventDate")) & " " & Left(MonthName(Month(dr("EventDate")), False), 3) & " " & Year(dr("EventDate"))
                Else
                    EDate1 = Day(dr("EventDate")) & " " & Left(MonthName(Month(dr("EventDate")), False), 3) & " " & Year(dr("EventDate"))
                End If

                strURL = clsRefdata.GenerateURL(dr("EventHeading").ToString(), dr("EventID"), "sis-events/" & "", "-")
                strResults1 &= "<tr><td align=""left"" width=""320px"">" & vbCrLf
                strResults1 &= dr("EventHeading").ToString() & "</td>" & vbCrLf
                strResults1 &= "<td>" & EDate1 & "</td>" & vbCrLf
                strResults1 &= "<td><a href=""" & strURL & """>View Event</a></td>" & vbCrLf
                strResults1 &= "</tr> " & vbCrLf
                strResults1 &= "<tr><td align=""right"" colspan=""3"">&nbsp;</td></tr>" & vbCrLf
            Next
            strResults1 &= " </table>"
            ltdbelow.Text = strResults1
        Else
            ltdbelow.Text = "<span class=""box-txt"" style=""color:Red;"">No Events Found</span>"
        End If

        If strData = "" Then
            ltdbelow.Text = "<span class=""box-txt"" style=""color:Red;"">No Events Found</span>"
        End If
    End Sub

    Private Function fn_desc(ByVal ds As DataSet) As DataSet

        Dim dwDs As DataView = ds.Tables(0).DefaultView
        dwDs.Sort = " EventSort Desc "

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

        'page title
        Page.Header.Title = "South Indian Events, Tamil Events, London, United Kingdom"

        'meta keywords
        metaTagKeywords = DirectCast(Page.Master.FindControl("keywords"), HtmlMeta)
        metaTagKeywords.Attributes.Add("content", "sis uk, south indian society london, sis events, indian events London")

        'meta description
        metaTagDescription = DirectCast(Page.Master.FindControl("description"), HtmlMeta)
        metaTagDescription.Attributes.Add("content", "A section to view Indian events & Tamil events like Deepavali, Tamil New Year, Pongal, Bollywood dances, Carnatic festivals, Vedic and many other in and around London, Harrow, Ilford, United Kingdom.")

        'meta robots
        metaRobots = DirectCast(Page.Master.FindControl("robots"), HtmlMeta)
        metaRobots.Attributes.Add("content", "index, follow")

    End Sub

End Class
