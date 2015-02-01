Imports System
Imports System.Data
Imports System.Xml
Partial Class other_events_otherevent
    Inherits System.Web.UI.Page
    Dim clsRefData As New clsRefdata
    Dim strEventId As String = ""
    Dim xmlSortOrder As New XmlDocument
    Dim xmlSortOrderNode As XmlNode

    Dim FileName As String
    Dim strTitle As String = ""
    Dim strHeading As String = ""
    Dim strKeywords As String = ""
    Dim strDescription As String = ""
    Dim strDate As String = ""
    Dim strTime As String = ""
    Dim strAddress As String = ""
    Dim strContent As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadEvents()
        End If
    End Sub

    Private Sub LoadEvents()
        Dim metaTagKeywords As HtmlMeta
        Dim metaTagDescription As HtmlMeta
        Dim metaRobots As HtmlMeta
        Dim ADate As String
        strEventId = Request.QueryString("EventID")
        If Not IsPostBack Then
            FileName = Server.MapPath("\XML\otherevents\") & strEventId & ".xml"
            xmlSortOrder.Load(FileName)
            For Each xmlSortOrderNode In xmlSortOrder.SelectNodes("//Event")
                strTitle = xmlSortOrderNode.SelectSingleNode("PageTitle").InnerText
                strKeywords = xmlSortOrderNode.SelectSingleNode("MetaKeywords").InnerText
                strDescription = xmlSortOrderNode.SelectSingleNode("MetaDescription").InnerText
                strHeading = xmlSortOrderNode.SelectSingleNode("EventHeading").InnerText & ""
                ADate = Day(xmlSortOrderNode.SelectSingleNode("EventDate").InnerText) & "/" & Left(MonthName(Month(xmlSortOrderNode.SelectSingleNode("EventDate").InnerText), False), 3) & "/" & Year(xmlSortOrderNode.SelectSingleNode("EventDate").InnerText)
                strDate &= "<div class=""content"">Venue: <span style=""font-style:italic; color:#8F9498;"">" & xmlSortOrderNode.SelectSingleNode("Address").InnerText & "</span><br />Date&nbsp;&nbsp;&nbsp;: <span style=""font-style:italic;color:#8F9498;"">" & ADate & "</span><br />Time&nbsp;&nbsp;: <span style=""font-style:italic;color:#8F9498;"">" & xmlSortOrderNode.SelectSingleNode("EventTime").InnerText & "</span></div><br />" & vbCrLf
                strContent = xmlSortOrderNode.SelectSingleNode("Maindescription").InnerXml.Replace("<![CDATA[", "").Replace("]]>", "").Replace("]]&gt;", "").Replace("&lt;![CDATA[", "").Replace("&gt;", ">").Replace("&lt;", "<")
            Next

            Page.Header.Title = strTitle

            'meta keywords
            metaTagKeywords = DirectCast(Page.Master.FindControl("keywords"), HtmlMeta)
            metaTagKeywords.Attributes.Add("content", strKeywords)

            'meta description
            metaTagDescription = DirectCast(Page.Master.FindControl("description"), HtmlMeta)
            metaTagDescription.Attributes.Add("content", strDescription)

            'meta robots
            metaRobots = DirectCast(Page.Master.FindControl("robots"), HtmlMeta)
            metaRobots.Attributes.Add("content", "index, follow")

            'PAGE CONTENT
            lblH2.Text = strHeading
            lblDate.Text = strDate
            lblContent.Text = strContent
        End If
    End Sub
End Class
