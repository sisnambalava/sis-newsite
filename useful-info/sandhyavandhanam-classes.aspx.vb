Imports System
Imports System.Data
Partial Class useful_info_sandhyavandhanam_classes
    Inherits System.Web.UI.Page
    Dim clsRefdata As New clsRefdata
    Dim ltArticleCategories As Literal
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadPageDetails()
            ltArticleCategories = DirectCast(Page.Master.FindControl("ltArticleCategories"), Literal)
            ltArticleCategories.Text = clsRefdata.LoadUsefulInfo()
        End If
    End Sub
    Private Sub LoadPageDetails()
        Dim metaTagKeywords As HtmlMeta
        Dim metaTagDescription As HtmlMeta
        Dim metaRobots As HtmlMeta
        Dim masterlblBread As Literal
        Dim strBread As String = ""

        'page title
        Page.Header.Title = "Sandhyavandhanam Classes in London, United Kingdom"

        'meta keywords
        metaTagKeywords = DirectCast(Page.Master.FindControl("keywords"), HtmlMeta)
        metaTagKeywords.Attributes.Add("content", "sis uk, sandhyavandhanam classes")

        'meta description
        metaTagDescription = DirectCast(Page.Master.FindControl("description"), HtmlMeta)
        metaTagDescription.Attributes.Add("content", "Free Sandhyavandhanam Classes in and around London, UK. Contact Mr. Subbu for more details.")

        'meta robots
        metaRobots = DirectCast(Page.Master.FindControl("robots"), HtmlMeta)
        metaRobots.Attributes.Add("content", "index, follow")

    End Sub

End Class
