Imports System.Data
Imports System.IO
Partial Class articles_default
    Inherits System.Web.UI.Page
    Dim clsRefData As New clsRefdata
    Public pageSize As Integer = 10
    Public CurrentPage As Integer = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then
            CurrentPage = hdnpgVal.Value
            LoadPageDetails()
            LoadArticles("", "")
            LoadArticleCategories()
            'ClientScript.RegisterStartupScript(Page.GetType(), "Script", "call();", True)
        End If
        If Not IsPostBack Then
            LoadPageDetails()
            LoadArticles("", "")
            LoadArticleCategories()
        End If
    End Sub

    Private Sub LoadPageDetails()
        Dim metaTagKeywords As HtmlMeta
        Dim metaTagDescription As HtmlMeta
        Dim metaRobots As HtmlMeta
        
        'page title
        Page.Header.Title = "Vedic Articles, Religious Articles, Temples, Indian Recipes"

        'meta keywords
        metaTagKeywords = DirectCast(Page.Master.FindControl("keywords"), HtmlMeta)
        metaTagKeywords.Attributes.Add("content", "sis uk, south indian society london, sis airticles, Vedic Articles, Religious Articles, Temples, Indian Recipes")

        'meta description
        metaTagDescription = DirectCast(Page.Master.FindControl("description"), HtmlMeta)
        metaTagDescription.Attributes.Add("content", "A section to read Vedic articles, Religious articles, Hindu temples, Indian food recipes etc compiled by South Indian Society.")

        'meta robots
        metaRobots = DirectCast(Page.Master.FindControl("robots"), HtmlMeta)
        metaRobots.Attributes.Add("content", "index, follow")

    End Sub

    Private Sub LoadArticles(ByVal StrMonth As String, ByVal StrYear As String)
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
        Dim DvCategory As New DataView
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
            tdr("category") = DS.Tables(0).Rows(0)("category")
            xmlTable.Rows.Add(tdr)
        Next
        dss.Tables.Add(xmlTable)

        Dv = dss.Tables(0).DefaultView
        DvCategory = dss.Tables(0).DefaultView
        'Category Display
        DvCategory.Sort = "category asc"
        If DvCategory.Count > 0 Then
            Dim dtCategory As New DataTable()
            Dim dstCategory As New DataSet
            Dim CategoryCount As New DataColumn("CategoryCount", GetType(String))
            Dim DispCategory As New DataColumn("DispCategory", GetType(String))

            dtCategory.Columns.Add(CategoryCount)
            dtCategory.Columns.Add(DispCategory)

            Dim strCurrentCategory As String = ""
            Dim strPreviousCategory As String = ""
            Dim intCategoryCount As Integer = 0
            Dim strDisCategory As String = ""

            strPreviousCategory = DvCategory(0)("category").ToString.ToLower().Trim()
            For i = 0 To DvCategory.Count - 1
                strCurrentCategory = DvCategory(i)("category").ToString.ToLower().Trim()
                Dim tdrCategory As DataRow
                If strPreviousCategory = strCurrentCategory And i <> 1 Then
                    intCategoryCount += 1
                End If
                strDisCategory = strPreviousCategory
                If strPreviousCategory <> strCurrentCategory Then
                    tdrCategory = dtCategory.NewRow()
                    tdrCategory("DispCategory") = strDisCategory
                    If i <> 1 Then
                        intCategoryCount = intCategoryCount + 1
                    Else
                        intCategoryCount = intCategoryCount
                    End If
                    tdrCategory("CategoryCount") = intCategoryCount
                    dtCategory.Rows.Add(tdrCategory)
                    strPreviousCategory = strCurrentCategory
                    intCategoryCount = 0
                    strDisCategory = DvCategory(i)("category").ToString.ToLower().Trim()
                End If
            Next
            'Adding Last Record
            Dim tdrSub As DataRow
            tdrSub = dtCategory.NewRow()

            If DvCategory.Count = 1 Then
                intCategoryCount = intCategoryCount
            Else
                intCategoryCount = intCategoryCount + 1
            End If

            tdrSub("DispCategory") = strDisCategory
            tdrSub("CategoryCount") = intCategoryCount
            dtCategory.Rows.Add(tdrSub)

            dstCategory.Tables.Add(dtCategory)
            If dstCategory.Tables(0).Rows.Count > 0 Then
                Dim strCategory As String = ""
                'strCategory &= "<table border=""0"" width=""100%"">" & vbCrLf
                'strCategory &= "<h2>Article Categories</h2>"
                'strCategory &= "<div class=""article-panel art-categories"">"
                'strCategory &= "<h2>Article Categories</h2>"
                'strCategory &= "<ul>"

                For Each dr As DataRow In dstCategory.Tables(0).Rows
                    'strCategory &= "<tr>" & vbCrLf
                    'strCategory &= "<td class=""box-txt"" align=""left""><img src=""/images/folder.jpg"" alt="""">&nbsp;" & vbCrLf
                    'strCategory &= "<a href=""/articles/" & dr("DispCategory").ToString().ToLower() & "/""" & vbCrLf
                    'strCategory &= "title=""" & dr("DispCategory").ToString() & """>" & vbCrLf
                    'strCategory &= dr("DispCategory").ToString() & " (" & dr("CategoryCount").ToString() & ")</a></td> " & vbCrLf
                    'strCategory &= "</tr>" & vbCrLf
                    'strCategory &= "<li><a href=""/articles/" & dr("DispCategory").ToString().ToLower() & "/""" > dr("DispCategory").ToString() & " (" & dr("CategoryCount").ToString() & ")</a></li>"

                Next
                'strCategory &= "</table>" & vbCrLf

                'strCategory &= "</ul>"
                'strCategory &= "</div>"

                'ltArticleCategories = DirectCast(Page.Master.FindControl("ltArticleCategories"), Literal)
                'ltArticleCategories.Text = strCategory

                'ltCategories.Text = strCategory
            Else
                'Me.ltCategories.Text = "<div class=""content"">No Records Found</div>" & vbCrLf
            End If
        Else
            'Me.ltCategories.Text = "<div class=""content"">No Records Found</div>" & vbCrLf
        End If

        Dv.Sort = "ArticleId DESC"

        Dim totalRecords As Integer = Dv.Count
        Dim totalPages As Integer
        Dim recStart As Integer
        Dim recEnd As Integer

        If totalRecords Mod pageSize = 0 Then
            totalPages = totalRecords / pageSize
        Else
            totalPages = totalRecords / pageSize
            totalPages = totalPages + 1
        End If

        If CurrentPage <> 0 And CurrentPage <= totalPages Then
            recEnd = CurrentPage * pageSize
            recStart = recEnd - (pageSize - 1)
        Else
            CurrentPage = 1
            recEnd = CurrentPage * pageSize
            recStart = recEnd - (pageSize - 1)
        End If

        strResults &= "<table border=""0"">" & vbCrLf
        If StrMonth = "" And StrYear = "" Then
            If Dv.Count > 0 Then
                Dim rowcount As Integer = 0
                For i = 0 To Dv.Count - 1
                    rowcount += 1
                    If rowcount <= 10 Then
                        'If rowcount >= recStart And rowcount <= recEnd Then
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
                    End If
                Next
            End If
        End If

        strResults &= "</table>" & vbCrLf

        'Dim gen As String
        'Dim last As Integer
        'Dim first As Integer
        'Dim lnkprevcalc As Integer = CurrentPage - 10
        'Dim lnknxtcalc As Integer = CurrentPage + 10
        'strResults &= "<table style=""border:0px; color:#000;"">" & vbCrLf
        'strResults &= "<tr>" & vbCrLf
        'strResults &= "<td>" & vbCrLf

        'If lnknxtcalc >= totalPages Then
        '    last = totalPages
        'Else
        '    last = CurrentPage + 10
        'End If
        'If lnkprevcalc >= 1 Then
        '    first = lnkprevcalc
        'Else
        '    first = 1
        'End If
        'For i = first To last
        '    'gen = "default.aspx?currentpage=" & i & ""
        '    'gen = "/articles/"
        '    gen = "javascript:void(0);"

        '    If i <> CurrentPage And i = first Then
        '        strResults &= "<a href=""" & gen & """ onclick='changeVal(" & i & ")'  style='padding:5px;margin:5px;'>&laquo; Previous</a>&nbsp;" & vbCrLf
        '    ElseIf i = CurrentPage Then
        '        strResults &= "<a href=""" & gen & """ onclick='changeVal(" & i & ")' style='width:10px;padding:5px;margin:5px;background:rgba(0,0,0,0.1);'>" & i & "</a>&nbsp;" & vbCrLf
        '    ElseIf i <> CurrentPage And i = last Then
        '        strResults &= "<a href=""" & gen & """ onclick='changeVal(" & i & ")' style='padding:5px;margin:5px;'>Next &raquo;</a>&nbsp;" & vbCrLf
        '    Else
        '        strResults &= "<a href=""" & gen & """ onclick='changeVal(" & i & ")' style='width:10px;padding:5px;margin:5px;'>" & i & "</a>&nbsp;" & vbCrLf
        '    End If
        'Next
        'strResults &= "<script type='text/javascript'>function changeVal(val){document.getElementById('ctl00_ContentPlaceHolder1_hdnpgVal').value=val; document.getElementById('aspnetForm').submit();}</script>"
        ''strResults &= "<script type='text/javascript'>function call(){ urlBase = location.href.substring(0,location.href.lastIndexOf(""/"")+1); window.history.pushState('', document.title, urlBase);}</script>" & vbCrLf
        'strResults &= "</td>" & vbCrLf
        'strResults &= "</tr>" & vbCrLf

        'strResults &= "</table>" & vbCrLf
        xmlTable.DefaultView.Sort = "ArticleID DESC"
        If xmlTable.Rows.Count > 0 Then
            If Dv.Count > 0 Then
                Me.lblNewsArchieve.Text = strResults
            Else
                Me.lblNewsArchieve.Text = "<div class=""content"">No Records Found</div>"
            End If
        Else
            Me.lblNewsArchieve.Text = "<div class=""content"">No Records Found</div>"
        End If
    End Sub

    Private Sub LoadArticleCategories()
        Dim ltArticleCategories As Literal
        ltArticleCategories = DirectCast(Page.Master.FindControl("ltArticleCategories"), Literal)
        ltArticleCategories.Text = clsRefData.LoadArticleCategories()
        'Dim strResults As String = ""
        'Dim ltArticleCategories As Literal
        'Try
        '    strResults &= "<div class=""article-panel art-categories"">"
        '    strResults &= "<h2>Article Categories</h2>"
        '    strResults &= "<ul>"
        '    strResults &= "<li><a href=""/articles/art/"">Arts</a></li>"
        '    strResults &= "<li><a href=""/articles/culture/"">Culture</a></li>"
        '    strResults &= "<li><a href=""/articles/recipes/"">Recipes</a></li>"
        '    strResults &= "<li><a href=""/articles/religion/"">Religion</a></li>"
        '    strResults &= "<li><a href=""/articles/temples/"">Temples</a></li>"
        '    strResults &= "<li style=""border:0px;""><a href=""/articles/others/"">Others</a></li>"
        '    strResults &= "</ul>"
        '    strResults &= "</div>"

        '    ltArticleCategories = DirectCast(Page.Master.FindControl("ltArticleCategories"), Literal)
        '    ltArticleCategories.Text = strResults
        'Catch ex As Exception

        'End Try

    End Sub
End Class
