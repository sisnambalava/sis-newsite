Imports System
Imports System.Data
Imports System.Xml
Imports System.Text
Imports System.IO
Partial Class _default
    Inherits System.Web.UI.Page
    Dim clsRefdata As New clsRefdata
    Dim strOtherEvents As String = ""
    Dim strXMLNewsletterPath As String = Server.MapPath("/XML/newsletter/")
    Dim strXMLTirupaavaiPath As String = Server.MapPath("/XML/thirupaavai/")
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ltBanners.Text = clsRefdata.ReadBanners("\XML\banner\")
            ltAdverts.Text = clsRefdata.ReadXmlImages("\XML\sponsor\")
            ltPopularArticles.Text = clsRefdata.LoadPopularArticles()
            ltLatestArticles.Text = clsRefdata.LoadLatestArticles()
            ltUpcomingEvents.Text = clsRefdata.LoadUpcomingEvents()
            strOtherEvents = clsRefdata.LoadOtherEvents()
            If strOtherEvents.Contains("No Events Found") Then
                pnlOtherEvents.Visible = False
            Else
                Me.ltOtherEvents.Text = strOtherEvents
            End If

            LoadNewsletter()
            LoadThiruPaavai()
        End If
    End Sub

    Protected Sub lnkSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSubmit.Click
        clsRefdata.EmailSignup(txtEmail.Text)
        Response.Redirect("signup-thanks.aspx")
    End Sub

    Private Sub LoadNewsletter()
        Dim Dss As New DataSet
        Dim dt As New DataTable
        Dim strURL As String = ""
        Dim strNewsData As String = ""
        Dim ExpiryDate As New DataColumn("ExpiryDate", GetType(String))
        Dim NMonth As New DataColumn("NMonth", GetType(String))
        Dim NYear As New DataColumn("NYear", GetType(String))
        Dim Heading As New DataColumn("Heading", GetType(String))

        dt.Columns.Add(ExpiryDate)
        dt.Columns.Add(NMonth)
        dt.Columns.Add(NYear)
        dt.Columns.Add(Heading)

        If Directory.Exists(strXMLNewsletterPath) Then
            Dim i As Integer = 0
            For Each strNewsData In Directory.GetFiles(strXMLNewsletterPath)
                If Path.GetExtension(strNewsData) = ".xml" Then
                    Dim DS As New DataSet
                    DS.ReadXml(strNewsData)
                    If DS.Tables(0).Rows.Count > 0 Then
                        Dim tdr As DataRow
                        tdr = dt.NewRow()
                        For Each dr As DataRow In DS.Tables(0).Rows()
                            Dim strDate As String = dr("ExpiryDate").ToString()
                            Dim dt_Todate As Date = CDate(Date.Today)
                            Dim dt_selectedDate As Date = CDate(strDate)
                            Dim DateState As Integer = Date.Compare(dt_selectedDate, dt_Todate)
                            If DateState < 0 Then
                            ElseIf DateState >= 0 Then
                                i += 1
                                tdr("Heading") = dr("Heading").ToString()
                                tdr("NMonth") = dr("NMonth").ToString()
                                tdr("NYear") = dr("NYear").ToString()
                                tdr("ExpiryDate") = dr("ExpiryDate").ToString()
                                dt.Rows.Add(tdr)
                            End If
                        Next
                    Else
                        pnlNewletter.Visible = False
                    End If
                End If
            Next
            Dss.Tables.Add(dt)

            If Dss.Tables(0).Rows.Count > 0 Then
                For Each dr As DataRow In Dss.Tables(0).Rows
                    'ltNewsletter.Text = dr("Heading").ToString()
                    strURL = ConfigurationManager.AppSettings.Get("website") & "newsletter/" & LCase(dr("NMonth")) & "-" & dr("NYear") & ".aspx"
                    hrefnewsletter.HRef = strURL
                Next
            Else
                pnlNewletter.Visible = False
            End If
        Else
            pnlNewletter.Visible = False
        End If

    End Sub

    Private Sub LoadThiruPaavai()
        Dim Dss As New DataSet
        Dim dt As New DataTable
        Dim strTData As String = ""
        Dim strResults As String = ""
        Dim strTLabel As String = ""

        Dim TDate As String = ""

        Dim pday As New DataColumn("pday", GetType(String))
        Dim edate As New DataColumn("edate", GetType(String))
        Dim text As New DataColumn("text", GetType(String))
        Dim url As New DataColumn("url", GetType(String))
        Dim EventDate As New DataColumn("EventDate", GetType(String))
        Dim image As New DataColumn("image", GetType(String))

        dt.Columns.Add(pday)
        dt.Columns.Add(edate)
        dt.Columns.Add(text)
        dt.Columns.Add(EventDate)
        dt.Columns.Add(url)
        dt.Columns.Add(image)

        If Directory.Exists(strXMLTirupaavaiPath) Then
            For Each strTData In Directory.GetFiles(strXMLTirupaavaiPath)
                If Path.GetExtension(strTData) = ".xml" Then
                    Dim DS As New DataSet
                    DS.ReadXml(strTData)
                    If DS.Tables(0).Rows.Count > 0 Then
                        For Each dr As DataRow In DS.Tables(0).Rows()
                            Dim strDate As String = dr("date").ToString()
                            Dim dt_Todate As Date = CDate(Date.Today)
                            Dim dt_selectedDate As Date = CDate(strDate)
                            Dim DateState As Integer = Date.Compare(dt_selectedDate, dt_Todate)

                            If DateState = 0 Then
                                Dim tdr As DataRow
                                tdr = dt.NewRow()
                                tdr("edate") = dr("date").ToString()
                                tdr("text") = dr("text").ToString()
                                tdr("EventDate") = dr("date").ToString()
                                tdr("url") = dr("url").ToString()
                                tdr("image") = dr("image").ToString()
                                tdr("pday") = dr("day").ToString()
                                dt.Rows.Add(tdr)
                            End If
                        Next
                    Else
                        ltThiruPaavai.Text = ""
                    End If
                End If
            Next
            Dss.Tables.Add(dt)

            If Dss.Tables(0).Rows.Count > 0 Then
                strResults &= "<div style=""margin-left:150px; float:left;""> "
                For Each dr As DataRow In Dss.Tables(0).Rows
                    If Day(dr("EventDate")) < 10 Then
                        TDate = "0" & Day(dr("EventDate")) & " " & Left(MonthName(Month(dr("EventDate")), False), 3) & " " & Year(dr("EventDate"))
                    Else
                        TDate = Day(dr("EventDate")) & " " & Left(MonthName(Month(dr("EventDate")), False), 3) & " " & Year(dr("EventDate"))
                    End If

                    strResults &= "<div class=""header"">" & vbCrLf
                    strResults &= "<u>Today <strong>" & TDate & "</strong> - THIRUPAAVAI PASURAM - " & dr("pday") & ":"
                    strResults &= "</u></div>"

                    strResults &= "<div class=""box-txt"">"
                    strResults &= "<img src=""" & dr("image").ToString() & """  /><br /><br />" & vbCrLf
                    strResults &= "Listen to <br />" & vbCrLf
                    strResults &= dr("text").ToString()
                    strResults &= "</div><br /><br />"
                    strResults &= "<iframe width=""420"" height=""315"" src=""" & dr("url").ToString() & """ frameborder=""0"" allowfullscreen></iframe>"
                Next
                strResults &= " </div>"
                ltThiruPaavai.Text = strResults
            Else
                ltThiruPaavai.Text = ""
            End If
        Else
            ltThiruPaavai.Text = ""
        End If

    End Sub

    Protected Overloads Overrides Sub Render(ByVal writer As System.Web.UI.HtmlTextWriter)
        Dim stringWriter As New System.IO.StringWriter()
        Dim htmlWriter As New HtmlTextWriter(stringWriter)
        MyBase.Render(htmlWriter)
        Dim html As String = stringWriter.ToString()
        Dim StartPoint As Integer = html.IndexOf("<input type=""hidden"" name=""__VIEWSTATE""")
        If StartPoint >= 0 Then
            Dim EndPoint As Integer = html.IndexOf("/>", StartPoint) + 2
            Dim viewstateInput As String = html.Substring(StartPoint, EndPoint - StartPoint)
            html = html.Remove(StartPoint, EndPoint - StartPoint)
            Dim FormEndStart As Integer = html.IndexOf("</form>")
            If FormEndStart >= 0 Then
                html = html.Insert(FormEndStart, "<div>" & viewstateInput & "</div>" & vbCrLf)
            End If
        End If
        writer.Write(html)
    End Sub
End Class
