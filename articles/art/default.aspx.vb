Imports System.IO
Imports System.Data
Partial Class articles_art_default
    Inherits System.Web.UI.Page
    Dim clsRefData As New clsRefdata
    Dim strCategoryName As String = "art"
    Dim ltArticleCategories As Literal
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadPageDetails()
            LoadArticles()
            ltArticleCategories = DirectCast(Page.Master.FindControl("ltArticleCategories"), Literal)
            ltArticleCategories.Text = clsRefData.LoadArticleCategories()
        End If
    End Sub

    Private Sub LoadPageDetails()
        Dim metaTagKeywords As HtmlMeta
        Dim metaTagDescription As HtmlMeta
        Dim metaRobots As HtmlMeta
        Dim pTitle As String = ""
        Dim pMetaKeywords As String = ""
        Dim pMetaDesc As String = ""

        pTitle = "Indian Arts | South Indian Society"
        pMetaKeywords = "sis uk, south indian society, useful articles about various arts"
        pMetaDesc = "Useful articles about Yoga, meditation, Carnatic Music, Bharatanatyam, Kuchipudi and other various Indian arts compiled by South Indian Society."

        'page title
        Page.Header.Title = pTitle

        'meta keywords
        metaTagKeywords = DirectCast(Page.Master.FindControl("keywords"), HtmlMeta)
        metaTagKeywords.Attributes.Add("content", pMetaKeywords)

        'meta description
        metaTagDescription = DirectCast(Page.Master.FindControl("description"), HtmlMeta)
        metaTagDescription.Attributes.Add("content", pMetaDesc)

        'meta robots
        metaRobots = DirectCast(Page.Master.FindControl("robots"), HtmlMeta)
        metaRobots.Attributes.Add("content", "index, follow")

        ltH1.Text = "Indian Art Articles"

    End Sub

    Private Sub LoadArticles()
        Dim files As String()
        Dim File As String
        Dim strQry As String = ""
        Dim FilterQry As String = ""
        Dim strResults As String = ""
        Dim strURL As String = ""
        Dim URL As String = ""
        Dim i As Integer
        Dim ADate As String

        Dim dss As New Data.DataSet
        Dim Dv As New DataView
        Dim xmlTable As New DataTable()

        Dim ArticleHeading As New DataColumn("ArticleHeading", GetType(String))
        Dim ArticleDate As New DataColumn("ArticleDate", GetType(String))
        Dim ArticleID As New DataColumn("ArticleID", GetType(String))
        Dim MiniDescription As New DataColumn("MiniDescription", GetType(String))
        Dim Author As New DataColumn("Author", GetType(String))
        Dim Category As New DataColumn("category", GetType(String))

        xmlTable.Columns.Add(ArticleHeading)
        xmlTable.Columns.Add(ArticleDate)
        xmlTable.Columns.Add(Author)
        xmlTable.Columns.Add(ArticleID)
        xmlTable.Columns.Add(MiniDescription)
        xmlTable.Columns.Add(Category)

        files = IO.Directory.GetFiles(Server.MapPath("\XML\articles\"), "*")
        For Each File In files
            Dim tdr As DataRow
            Dim DS As New DataSet
            DS.ReadXml(File)
            tdr = xmlTable.NewRow()
            tdr("ArticleHeading") = DS.Tables(0).Rows(0)("ArticleHeading")
            tdr("ArticleDate") = DS.Tables(0).Rows(0)("ArticleDate")
            tdr("Author") = DS.Tables(0).Rows(0)("Author")
            tdr("ArticleID") = DS.Tables(0).Rows(0)("ArticleID")
            tdr("MiniDescription") = DS.Tables(0).Rows(0)("MiniDescription")
            tdr("category") = DS.Tables(0).Rows(0)("category").ToString.ToLower().Trim()
            xmlTable.Rows.Add(tdr)
        Next
        dss.Tables.Add(xmlTable)

        Dv = dss.Tables(0).DefaultView
        Dv.RowFilter = "category='" & strCategoryName & "'"
        Dv.Sort = "ArticleId DESC"
        strResults &= "<table border=""0"">" & vbCrLf
        If Dv.Count > 0 Then
            For i = 0 To Dv.Count - 1
                URL = ""
                If Day(Dv(i)("ArticleDate")) < 10 Then
                    ADate = "0" & Day(Dv(i)("ArticleDate")) & " " & Left(MonthName(Month(Dv(i)("ArticleDate")), False), 3) & " " & Year(Dv(i)("ArticleDate"))
                Else
                    ADate = Day(Dv(i)("ArticleDate")) & " " & Left(MonthName(Month(Dv(i)("ArticleDate")), False), 3) & " " & Year(Dv(i)("ArticleDate"))
                End If
                strURL = clsRefData.GenerateURL(Dv(i)("ArticleHeading"), Dv(i)("ArticleID"), "articles/" & LCase(Dv(i)("category")) & "/" & URL, "-") 'DS.Tables(0).Rows(i)("url"), "-")
                strResults &= "<tr>" & vbCrLf
                strResults &= "<td align=""left"" valign=""top"">" & vbCrLf
                strResults &= "<div class=""boxheader"" style=""font-size:1.4em;"">" & Dv(i)("ArticleHeading") & "</div>" & vbCrLf
                strResults &= "<div>Posted by <span style=""font-weight:bold; color:#8F9498;"">" & Dv(i)("Author") & "</span>&nbsp;on&nbsp;<span style=""font-weight:bold; color:#8F9498"">" & ADate & "</span></div>" & vbCrLf
                strResults &= "<div>" & Dv(i)("MiniDescription") & "</div>" & vbCrLf
                strResults &= "</td>" & vbCrLf
                strResults &= "</tr>" & vbCrLf
                strResults &= "<tr>" & vbCrLf
                strResults &= "<td>" & vbCrLf
                strResults &= "<div class=""more_txt"" style=""float:right;""><a href=""" & LCase(strURL) & """ title=""" & Dv(i)("ArticleHeading") & """ >Read full article&nbsp;&raquo;</a></div>" & vbCrLf
                strResults &= "</td>" & vbCrLf
                strResults &= "</tr>" & vbCrLf
            Next
        End If
        strResults &= "</table>" & vbCrLf
        xmlTable.DefaultView.Sort = "ArticleID DESC"
        If xmlTable.Rows.Count > 0 Then
            If Dv.Count > 0 Then
                Me.lblArticleCategory.Text = strResults
            Else
                Me.lblArticleCategory.Text = "<div style=""color:red;"" class=""content"">No Records Found</div>"
            End If
        Else
            Me.lblArticleCategory.Text = "<div style=""color:red;"" class=""content"">No Records Found</div>"
        End If
    End Sub

End Class
