Imports System
Imports System.Data
Imports System.IO
Partial Class useful_info_buckinghamshire_grammar_Schools
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

        'page title
        Page.Header.Title = "South Indian Society | Buckinghamshire Grammar Schools"

        'meta keywords
        metaTagKeywords = DirectCast(Page.Master.FindControl("keywords"), HtmlMeta)
        metaTagKeywords.Attributes.Add("content", "sis uk, south indian society london, UK grammar schools, Buckinghamshire grammar schools")

        'meta description
        metaTagDescription = DirectCast(Page.Master.FindControl("description"), HtmlMeta)
        metaTagDescription.Attributes.Add("content", "South Indian Society compiled a list of Grammar schools in Buckinghamshire, United Kingdom.")

        'meta robots
        metaRobots = DirectCast(Page.Master.FindControl("robots"), HtmlMeta)
        metaRobots.Attributes.Add("content", "index, follow")

    End Sub

End Class
