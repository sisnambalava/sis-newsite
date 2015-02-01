Imports System
Imports System.Data
Imports System.Xml
Imports System.IO
Partial Class useful_info_tamil_panchangam_pandetails
    Inherits System.Web.UI.Page
    Dim clsRefData As New clsRefdata
    Dim strEventId As String = ""
    Dim xmlPan As New XmlDocument
    Dim xmlPanNode As XmlNode
    Dim xmlDailyNode As XmlNode
    Dim FileName As String
    Dim strTitle As String = ""
    Dim strHeading As String = ""
    Dim strKeywords As String = ""
    Dim strDescription As String = ""
    Dim strContent As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadPanchangam()
        End If
    End Sub

    Private Sub LoadPanchangam()
        Dim metaTagKeywords As HtmlMeta
        Dim metaTagDescription As HtmlMeta
        Dim metaRobots As HtmlMeta
        Dim strResults As String = ""
        Dim EDate As String = ""
        strEventId = Request.QueryString("id")
        If Not IsPostBack Then
            FileName = Server.MapPath("\XML\panchangam\") & strEventId & ".xml"
            xmlPan.Load(FileName)
            strResults &= " <table cellpadding=""0"" cellspacing=""0"" border=""0"" width=""580px""> "
            strResults &= "<tr><td align=""right"" colspan=""2"">&nbsp;</td></tr>" & vbCrLf
            For Each xmlPanNode In xmlPan.SelectNodes("//panchangam")
                strTitle = xmlPanNode.SelectSingleNode("EventName").InnerText
                For Each xmlDailyNode In xmlPan.SelectNodes("//EventDate/day")
                    If Day(xmlDailyNode.SelectSingleNode("date").InnerText) < 10 Then
                        EDate = "0" & Day(xmlDailyNode.SelectSingleNode("date").InnerText) & " " & Left(MonthName(Month(xmlDailyNode.SelectSingleNode("date").InnerText), False), 3) & " " & Year(xmlDailyNode.SelectSingleNode("date").InnerText)
                    Else
                        EDate = Day(xmlDailyNode.SelectSingleNode("date").InnerText) & " " & Left(MonthName(Month(xmlDailyNode.SelectSingleNode("date").InnerText), False), 3) & " " & Year(xmlDailyNode.SelectSingleNode("date").InnerText)
                    End If
                    strResults &= "<tr><td align=""left"" width=""100px"">" & vbCrLf
                    strResults &= EDate & "</td>" & vbCrLf
                    strResults &= "<td>" & xmlDailyNode.SelectSingleNode("text").InnerText & "</td>" & vbCrLf
                    strResults &= "</tr> " & vbCrLf
                    strResults &= "<tr><td align=""right"" colspan=""2"">&nbsp;</td></tr>" & vbCrLf
                Next
            Next
            strResults &= " </table>"
            ltdPanchangam.Text = strResults

            Page.Header.Title = "Tamil Panchangam " & strTitle

            'meta keywords
            metaTagKeywords = DirectCast(Page.Master.FindControl("keywords"), HtmlMeta)
            metaTagKeywords.Attributes.Add("content", "panchangam, tamil panchangam, tamil panchangam " & strTitle)

            'meta description
            metaTagDescription = DirectCast(Page.Master.FindControl("description"), HtmlMeta)
            metaTagDescription.Attributes.Add("content", "")

            'meta robots
            metaRobots = DirectCast(Page.Master.FindControl("robots"), HtmlMeta)
            metaRobots.Attributes.Add("content", "INDEX, FOLLOW")

            lblH1.Text = "Tamil Panchangam " & strTitle

        End If
    End Sub
End Class
