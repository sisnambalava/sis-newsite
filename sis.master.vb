Imports System
Imports System.Data
Imports System.Xml
Imports System.Text
Imports System.IO
Partial Class sis
    Inherits BasePage
    Dim clsRefdata As New clsRefdata
    Dim strOtherEvents As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form1.Action = Request.RawUrl
        If Not IsPostBack Then
            ltAdverts.Text = clsRefdata.ReadXmlImages("\XML\sponsor\")
            'ltEvents.Text = clsRefdata.LoadEvents()
            'ltArticles.Text = clsRefdata.LoadArticles()
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
        End If
    End Sub

    Protected Sub lnkSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSubmit.Click
        clsRefdata.EmailSignup(txtEmail.Text)
        Response.Redirect("/signup-thanks.aspx")
    End Sub

    Private Sub LoadNewsletter()
        Dim Dss As New DataSet
        Dim dt As New DataTable
        Dim strURL As String = ""
        Dim strXMLNewsletterPath As String = Server.MapPath("/XML/newsletter/")
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
                    strURL = "/" & "newsletter/" & LCase(dr("NMonth")) & "-" & dr("NYear") & ".aspx"
                    hrefnewsletter.HRef = strURL
                Next
            Else
                pnlNewletter.Visible = False
            End If
        Else
            pnlNewletter.Visible = False
        End If

    End Sub

   
End Class

