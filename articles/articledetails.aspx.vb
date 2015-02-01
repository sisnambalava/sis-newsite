Imports System
Imports System.Data
Imports System.Xml
Partial Class articles_articledetails
    Inherits System.Web.UI.Page
    Dim clsRefData As New clsRefdata
    Dim strArticleId As String = ""
    Dim xmlSortOrder As New XmlDocument
    Dim xmlSortOrderNode As XmlNode

    Dim FileName As String
    Dim strBread As String = ""
    Dim strTitle As String = ""
    Dim strHeading As String = ""
    Dim strKeywords As String = ""
    Dim strDescription As String = ""
    Dim strDate As String = ""
    Dim strContent As String = ""
    Dim strContent1 As String = ""
    Dim ltArticleCategories As Literal

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadArticle()
            ltArticleCategories = DirectCast(Page.Master.FindControl("ltArticleCategories"), Literal)
            ltArticleCategories.Text = clsRefData.LoadArticleCategories()
        End If
    End Sub

    Private Sub LoadArticle()
        Dim metaTagKeywords As HtmlMeta
        Dim metaTagDescription As HtmlMeta
        Dim metaRobots As HtmlMeta
        Dim strCategory As String = ""
        Dim ADate As String
        strArticleId = Request.QueryString("NewsID")
        If Not IsPostBack Then
            FileName = Server.MapPath("\XML\articles\") & strArticleId & ".xml"
            xmlSortOrder.Load(FileName)
            For Each xmlSortOrderNode In xmlSortOrder.SelectNodes("//Article")
                strTitle = xmlSortOrderNode.SelectSingleNode("PageTitle").InnerText
                strKeywords = xmlSortOrderNode.SelectSingleNode("MetaKeywords").InnerText
                strDescription = xmlSortOrderNode.SelectSingleNode("MetaDescription").InnerText
                strHeading = xmlSortOrderNode.SelectSingleNode("ArticleHeading").InnerText & ""
                'ADate = Day(xmlSortOrderNode.SelectSingleNode("ArticleDate").InnerText) & "/" & Left(MonthName(Month(xmlSortOrderNode.SelectSingleNode("ArticleDate").InnerText), False), 3) & "/" & Year(xmlSortOrderNode.SelectSingleNode("ArticleDate").InnerText)
                If Day(xmlSortOrderNode.SelectSingleNode("ArticleDate").InnerText) < 10 Then
                    ADate = "0" & Day(xmlSortOrderNode.SelectSingleNode("ArticleDate").InnerText) & " " & Left(MonthName(Month(xmlSortOrderNode.SelectSingleNode("ArticleDate").InnerText), False), 3) & " " & Year(xmlSortOrderNode.SelectSingleNode("ArticleDate").InnerText)
                Else
                    ADate = Day(xmlSortOrderNode.SelectSingleNode("ArticleDate").InnerText) & " " & Left(MonthName(Month(xmlSortOrderNode.SelectSingleNode("ArticleDate").InnerText), False), 3) & " " & Year(xmlSortOrderNode.SelectSingleNode("ArticleDate").InnerText)
                End If
                strCategory = xmlSortOrderNode.SelectSingleNode("category").InnerText
                strDate &= "<div class=""content"">Posted by <span style=""font-weight:bold;color:#8F9498;"">" & xmlSortOrderNode.SelectSingleNode("Author").InnerText & "</span>&nbsp;on&nbsp;<span style=""font-weight:bold;color:#8F9498;"">" & ADate & "</span></div>" & vbCrLf
                strContent = "<div>" & xmlSortOrderNode.SelectSingleNode("Maindescription").InnerXml.Replace("<![CDATA[", "").Replace("]]>", "").Replace("]]&gt;", "").Replace("&lt;![CDATA[", "").Replace("&gt;", ">").Replace("&lt;", "<") & "</div>"
            Next

            'page title
            Page.Header.Title = strTitle

            'meta keywords
            metaTagKeywords = DirectCast(Page.Master.FindControl("keywords"), HtmlMeta)
            metaTagKeywords.Attributes.Add("content", strKeywords)

            'meta description
            metaTagDescription = DirectCast(Page.Master.FindControl("description"), HtmlMeta)
            metaTagDescription.Attributes.Add("content", strDescription)

            'meta robots
            metaRobots = DirectCast(Page.Master.FindControl("robots"), HtmlMeta)
            metaRobots.Attributes.Add("content", "INDEX, FOLLOW")

            'PAGE CONTENT
            lblH2.Text = strHeading
            lblDate.Text = strDate
            lblContent.Text = strContent
        End If
    End Sub

End Class
