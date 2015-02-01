Imports System
Imports System.Data
Imports System.Xml
Imports System.IO
Partial Class newsletter_news
    Inherits System.Web.UI.Page
    Dim clsRefData As New clsRefdata
    Dim strNewsId As String = ""
    Dim xmlNews As New XmlDocument
    Dim xmlNewsNode As XmlNode
    Dim xmlDailyNode As XmlNode
    Dim FileName As String
    Dim strTitle As String = ""
    Dim strMetaDesc As String = ""
    Dim strHeading As String = ""
    Dim strContent As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadNewsletter()
        End If
    End Sub

    Private Sub LoadNewsletter()
        Dim metaTagKeywords As HtmlMeta
        Dim metaTagDescription As HtmlMeta
        Dim metaRobots As HtmlMeta
        Dim NMonth As String = ""
        Dim NYear As String = ""
        strNewsId = Request.QueryString("id")
        If Not IsPostBack Then
            FileName = Server.MapPath("\XML\newsletter\") & strNewsId & ".xml"
            xmlNews.Load(FileName)
            For Each xmlNewsNode In xmlNews.SelectNodes("//Newsletter")
                strTitle = xmlNewsNode.SelectSingleNode("PageTitle").InnerText
                strMetaDesc = xmlNewsNode.SelectSingleNode("MetaDescription").InnerText
                strHeading = xmlNewsNode.SelectSingleNode("Heading").InnerText
                NMonth = xmlNewsNode.SelectSingleNode("NMonth").InnerText
                NYear = xmlNewsNode.SelectSingleNode("NYear").InnerText
                strContent = xmlNewsNode.SelectSingleNode("Maindescription").InnerXml.Replace("<![CDATA[", "").Replace("]]>", "").Replace("]]&gt;", "").Replace("&lt;![CDATA[", "").Replace("&gt;", ">").Replace("&lt;", "<")
            Next

            Page.Header.Title = strTitle

            'meta keywords
            metaTagKeywords = DirectCast(Page.Master.FindControl("keywords"), HtmlMeta)
            metaTagKeywords.Attributes.Add("content", "sis, sis newsletter " & NMonth & " " & NYear)

            'meta description
            metaTagDescription = DirectCast(Page.Master.FindControl("description"), HtmlMeta)
            metaTagDescription.Attributes.Add("content", strMetaDesc)

            'meta robots
            metaRobots = DirectCast(Page.Master.FindControl("robots"), HtmlMeta)
            metaRobots.Attributes.Add("content", "INDEX, FOLLOW")

            ltH1.Text = strTitle
            lblContent.Text = strContent

        End If
    End Sub

End Class
