Imports System
Imports System.Data
Imports System.IO
Imports System.Text
Imports System.Web
Imports System.Xml
Imports Microsoft.VisualBasic

Public Class clsRefdata

    Public Function CreateItem(ByVal strValue As String, ByVal strDisplayText As String) As ListItem
        Dim lvItem As New ListItem
        lvItem.Value = strValue
        lvItem.Text = strDisplayText
        CreateItem = lvItem
    End Function

    Public Sub PopulateArticleCategory(ByRef cboCArticle As DropDownList)
        cboCArticle.Items.Clear()
        cboCArticle.Items.Add(CreateItem("", "--Select Category--"))
        cboCArticle.Items.Add(CreateItem("art", "Art"))
        cboCArticle.Items.Add(CreateItem("culture", "Culture"))
        cboCArticle.Items.Add(CreateItem("recipes", "Recipes"))
        cboCArticle.Items.Add(CreateItem("religion", "Religion"))
        cboCArticle.Items.Add(CreateItem("temples", "Temples"))
        cboCArticle.Items.Add(CreateItem("others", "Others"))
    End Sub

    Public Function fn_foldercount(ByVal folderpath As String) As Integer
        Dim count As Integer = 0
        For Each d As String In Directory.GetFiles(folderpath)
            If Path.GetExtension(d) = ".xml" Then
                count += 1
            End If
        Next
        Return count
    End Function

    Public Function fn_number() As String
        Dim dateTimeInfo As DateTime = DateTime.Now
        Dim strInt As Integer = 0
        Dim strValue As String = ""
        Try
            strValue = dateTimeInfo.ToString("yyyy-MM-dd hh:mm:ss")
            strValue = strValue.Replace(" ", "")
            strValue = strValue.Replace(":", "")
            strValue = strValue.Replace("-", "")
        Catch ex As Exception
        End Try
        Return strValue
    End Function

    Public Sub PopulateYears(ByVal intStartTolerance As Integer, ByVal intEndTolerance As Integer, ByVal cboYear As DropDownList)
        Dim intYear As Integer = 0
        For intYear = (Now.Year - intStartTolerance) To (Now.Year - intEndTolerance) Step -1
            cboYear.Items.Add(CreateItem(intYear, intYear))
        Next
    End Sub

    Public Sub PopulateMonths(ByRef cboMonth As DropDownList)
        cboMonth.Items.Clear()
        cboMonth.Items.Add(CreateItem("", "-Month-"))
        cboMonth.Items.Add(CreateItem("January", "January"))
        cboMonth.Items.Add(CreateItem("February", "February"))
        cboMonth.Items.Add(CreateItem("March", "March"))
        cboMonth.Items.Add(CreateItem("April", "April"))
        cboMonth.Items.Add(CreateItem("May", "May"))
        cboMonth.Items.Add(CreateItem("June", "June"))
        cboMonth.Items.Add(CreateItem("July", "July"))
        cboMonth.Items.Add(CreateItem("August", "August"))
        cboMonth.Items.Add(CreateItem("September", "September"))
        cboMonth.Items.Add(CreateItem("October", "October"))
        cboMonth.Items.Add(CreateItem("November", "November"))
        cboMonth.Items.Add(CreateItem("December", "December"))
    End Sub


    Public Function LoadArticles() As String
        Dim strXMLArticles As String = System.Web.HttpContext.Current.Server.MapPath("/Xml/articles/")
        Dim DS As New DataSet
        Dim Dss As New DataSet
        Dim dt As New DataTable
        Dim strData As String = ""
        Dim strResults As String = ""
        Dim strURL As String = ""
        Dim ArticleId As New DataColumn("ArticleId", GetType(String))
        Dim ArticleHeading As New DataColumn("ArticleHeading", GetType(String))
        Dim ArticleDate As New DataColumn("ArticleDate", GetType(String))
        Dim category As New DataColumn("category", GetType(String))
        Dim ArticleSort As New DataColumn("ArticleSort", GetType(String))

        dt.Columns.Add(ArticleId)
        dt.Columns.Add(ArticleDate)
        dt.Columns.Add(category)
        dt.Columns.Add(ArticleSort)
        dt.Columns.Add(ArticleHeading)

        If Directory.Exists(strXMLArticles) Then
            For Each strData In Directory.GetFiles(strXMLArticles)
                If Path.GetExtension(strData) = ".xml" Then
                    DS.ReadXml(strData)
                    If DS.Tables(0).Rows.Count > 0 Then

                        Dim tdr As DataRow
                        tdr = dt.NewRow()
                        For Each dr As DataRow In DS.Tables(0).Rows()
                            tdr("ArticleId") = dr("ArticleId").ToString()
                            tdr("ArticleHeading") = dr("ArticleHeading").ToString()
                            tdr("ArticleDate") = dr("ArticleDate").ToString()
                            tdr("category") = dr("category").ToString()

                            Dim strDate As String = dr("ArticleDate").ToString()
                            Dim dt_ArticleDate As Date = CDate(strDate)
                            tdr("ArticleSort") = dr("ArticleId").ToString() 'dt_ArticleDate
                        Next
                        dt.Rows.Add(tdr)
                    Else
                        strResults = "<span class=""box-txt"">No Articles Found</span><br /><br />"
                        'ltMessages.Text = "<span style=""color:Red;"">No Record Found...</span>"
                    End If
                End If
            Next

            Dss.Tables.Add(dt)

            Dim dstArticle As New DataSet
            dstArticle = GetTopFive(Dss)

            If dstArticle.Tables(0).Rows.Count > 0 Then
                For Each dr As DataRow In dstArticle.Tables(0).Rows
                    strURL = GenerateURL(dr("ArticleHeading").ToString(), dr("articleid"), "articles/" & LCase(dr("category")) & "/" & "", "-")
                    strResults = strResults & "<span class=""box-txt""><a href=""" & strURL & """>" & dr("ArticleHeading").ToString() & "</a></span><br /><br />" & vbCrLf
                Next
            Else
                strResults = "<span class=""box-txt"">No Articles Found</span><br /><br />"
            End If

            If strData = "" Then
                strResults = "<span class=""box-txt"">No Articles Found</span><br /><br />"
            End If
        Else
            strResults = "<span class=""box-txt"">No Articles Found</span><br /><br />"
        End If
        Return strResults
    End Function
    Public Function GetTopTwo(ByVal ds As DataSet) As DataSet
        Dim dwDs As DataView = ds.Tables(0).DefaultView
        dwDs.Sort = " ArticleSort Desc "

        Dim dss As DataSet = New DataSet()
        Dim dt As DataTable = dwDs.Table.Clone()
        dss.Tables.Add(dt)

        Dim counter As Integer = 0
        If dwDs.Count >= 2 Then
            counter = 2
        Else
            counter = dwDs.Count
        End If

        Dim i As Integer
        For i = 0 To counter - 1 Step i + 1
            Dim dv As DataRow = dwDs(i).Row
            dss.Tables(0).ImportRow(dv)
        Next
        dss.AcceptChanges()
        Return (dss)
    End Function

    Public Function GetTopFive(ByVal ds As DataSet) As DataSet
        Dim dwDs As DataView = ds.Tables(0).DefaultView
        dwDs.Sort = " ArticleSort Desc "

        Dim dss As DataSet = New DataSet()
        Dim dt As DataTable = dwDs.Table.Clone()
        dss.Tables.Add(dt)

        Dim counter As Integer = 0
        If dwDs.Count >= 5 Then
            counter = 5
        Else
            counter = dwDs.Count
        End If

        Dim i As Integer
        For i = 0 To counter - 1 Step i + 1
            Dim dv As DataRow = dwDs(i).Row
            dss.Tables(0).ImportRow(dv)
        Next
        dss.AcceptChanges()
        Return (dss)
    End Function

    Public Function LoadEvents() As String
        Dim strXMLArticles As String = System.Web.HttpContext.Current.Server.MapPath("/Xml/events/")

        Dim Dss As New DataSet
        Dim dt As New DataTable
        Dim strData As String = ""
        Dim strResults As String = ""
        Dim strURL As String = ""

        Dim EventDate As New DataColumn("EventDate", GetType(String))
        Dim EventID As New DataColumn("EventID", GetType(String))
        Dim EventHeading As New DataColumn("EventHeading", GetType(String))
        Dim EventDatesort As New DataColumn("EventDatesort", GetType(Date))

        dt.Columns.Add(EventDate)
        dt.Columns.Add(EventID)
        dt.Columns.Add(EventHeading)
        dt.Columns.Add(EventDatesort)

        If Directory.Exists(strXMLArticles) Then
            For Each strData In Directory.GetFiles(strXMLArticles)
                If Path.GetExtension(strData) = ".xml" Then
                    Dim DS As New DataSet
                    DS.ReadXml(strData)
                    If DS.Tables(0).Rows.Count > 0 Then
                        For Each dr As DataRow In DS.Tables(0).Rows()
                            Dim dt_Todate As Date = CDate(Date.Today)
                            Dim dt_selectedDate As Date = CDate(dr("EventDate"))
                            Dim DateState As Integer = Date.Compare(dt_selectedDate, dt_Todate)
                            If DateState >= 0 Then
                                Dim tdr As DataRow
                                tdr = dt.NewRow()
                                Dim dt_ArticleDate As Date = CDate(dr("EventDate").ToString())
                                tdr("EventDate") = dr("EventDate").ToString()
                                tdr("EventID") = dr("EventID").ToString()
                                tdr("EventHeading") = dr("EventHeading").ToString()
                                tdr("EventDatesort") = dt_ArticleDate
                                dt.Rows.Add(tdr)
                            End If
                        Next
                    Else
                        strResults = "<span class=""box-txt"">No events found</span><br /><br />"
                    End If
                End If
            Next
            Dss.Tables.Add(dt)
            Dim dstArticle As New DataSet
            dstArticle = GetTopThree(Dss)
            If dstArticle.Tables(0).Rows.Count > 0 Then
                For Each dr As DataRow In dstArticle.Tables(0).Rows
                    strURL = GenerateURL(dr("EventHeading").ToString(), dr("EventID"), "sis-events/" & "", "-")
                    strResults = strResults & "<span class=""box-txt""><a href=""" & strURL & """>" & dr("EventHeading").ToString() & "</a></span><br /><br />" & vbCrLf
                Next
            Else
                strResults = "<span class=""box-txt"">No events found</span><br /><br />"

            End If
            If strData = "" Then
                strResults = "<span class=""box-txt"">No events found</span><br /><br />"
            End If
        Else
            strResults = "<span class=""box-txt"">No events found</span><br /><br />"
        End If
        Return strResults

    End Function

    Public Function GetTopThree(ByVal ds As DataSet) As DataSet
        Dim dwDs As DataView = ds.Tables(0).DefaultView
        dwDs.Sort = " EventDate ASC "
        Dim dss As DataSet = New DataSet()
        Dim dt As DataTable = dwDs.Table.Clone()
        dss.Tables.Add(dt)
        Dim counter As Integer = 0
        If dwDs.Count >= 3 Then
            counter = 3
        Else
            counter = dwDs.Count
        End If
        Dim i As Integer
        For i = 0 To counter - 1 Step i + 1
            Dim dv As DataRow = dwDs(i).Row
            dss.Tables(0).ImportRow(dv)
        Next
        dss.AcceptChanges()
        Return (dss)
    End Function

    Public Shared Function GenerateURL(ByVal Title As String, ByVal strId As String, ByVal strFolder As String, Optional ByVal StrSymbol As String = "", Optional ByVal strExtension As String = "") As String
        Dim strTitle As String = Title.ToString().ToLower()

        '#Region "Generate SEO Friendly URL based on Title"
        'Trim Start and End Spaces.
        strTitle = strTitle.Trim()

        'Trim "-" Hyphen
        strTitle = strTitle.Trim("-"c)

        strTitle = strTitle.ToLower()
        Dim chars As Char() = "£$%#@!*?;:~`+=()[]{}|\'<>,/^&"".".ToCharArray()
        strTitle = strTitle.Replace("c#", "C-Sharp")
        strTitle = strTitle.Replace("vb.net", "VB-Net")
        strTitle = strTitle.Replace("asp.net", "Asp-Net")

        'Replace . with - hyphen
        strTitle = strTitle.Replace(".", "-")

        'Replace Special-Characters
        For i As Integer = 0 To chars.Length - 1
            Dim strChar As String = chars.GetValue(i).ToString()
            If strTitle.Contains(strChar) Then
                strTitle = strTitle.Replace(strChar, String.Empty)
            End If
        Next

        'Replace all spaces with one "-" hyphen
        strTitle = strTitle.Replace(" ", "-")

        'Replace multiple "-" hyphen with single "-" hyphen.
        strTitle = strTitle.Replace("--", "-")
        strTitle = strTitle.Replace("---", "-")
        strTitle = strTitle.Replace("----", "-")
        strTitle = strTitle.Replace("-----", "-")
        strTitle = strTitle.Replace("----", "-")
        strTitle = strTitle.Replace("---", "-")
        strTitle = strTitle.Replace("--", "-")

        'Run the code again...
        'Trim Start and End Spaces.
        strTitle = strTitle.Trim()

        'Trim "-" Hyphen
        strTitle = strTitle.Trim("-"c)
        '#End Region

        'Append ID at the end of SEO Friendly URL
        If strExtension = "" Then
            strTitle = (ConfigurationManager.AppSettings.Get("website") & strFolder & strTitle & StrSymbol) + strId & ".aspx"
        Else
            strTitle = (ConfigurationManager.AppSettings.Get("website") & strFolder & strTitle & StrSymbol) + strId & "." & strExtension
        End If
        Return strTitle
    End Function

    Public Shared Sub SendMail(ByVal sHost As String, ByVal nPort As Integer, ByVal sUserName As String, ByVal sPassword As String, ByVal sFromName As String, ByVal sFromEmail As String, _
    ByVal sToName As String, ByVal sToEmail As String, ByVal strCCEmail As String, ByVal sHeader As String, ByVal sMessage As String, ByVal fSSL As Boolean)
        If sToName.Length = 0 Then
            sToName = sToEmail
        End If
        If sFromName.Length = 0 Then
            sFromName = sFromEmail
        End If

        Dim Mail As New System.Web.Mail.MailMessage()
        Mail.Fields("http://schemas.microsoft.com/cdo/configuration/smtpserver") = sHost
        Mail.Fields("http://schemas.microsoft.com/cdo/configuration/sendusing") = 2

        Mail.Fields("http://schemas.microsoft.com/cdo/configuration/smtpserverport") = nPort.ToString()
        If fSSL Then
            Mail.Fields("http://schemas.microsoft.com/cdo/configuration/smtpusessl") = "true"
        End If

        'Ingen auth 
        If sUserName.Length = 0 Then
        Else
            Mail.Fields("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate") = 1
            Mail.Fields("http://schemas.microsoft.com/cdo/configuration/sendusername") = sUserName
            Mail.Fields("http://schemas.microsoft.com/cdo/configuration/sendpassword") = sPassword
        End If

        Mail.[To] = sToEmail
        Mail.Cc = strCCEmail
        Mail.From = sFromEmail
        Mail.Subject = sHeader
        Mail.Body = sMessage
        Mail.BodyFormat = System.Web.Mail.MailFormat.Html

        System.Web.Mail.SmtpMail.SmtpServer = sHost
        System.Web.Mail.SmtpMail.Send(Mail)
    End Sub

    Public Sub EmailSignup(ByVal strEmail As String)
        Dim strResults As String = ""
        Try
            strResults &= "Newsletter signup received at " & Now() & vbCrLf
            strResults &= "<br /><hr />"
            strResults &= "<table border =""0"">" & vbCrLf
            strResults &= "<tr><td>" & vbCrLf
            strResults &= "Email :" & vbCrLf
            strResults &= "</td>" & vbCrLf
            strResults &= "<td>" & vbCrLf
            strResults &= strEmail & vbCrLf
            strResults &= "</td></tr>" & vbCrLf
            strResults &= "</table>" & vbCrLf
            SendMail("smtp.gmail.com", 465, "sisnambalava@gmail.com", "ind1ans0ciety27", "SIS London", "sisnambalava@gmail.com", "SIS Trustee", "sisnambalava@gmail.com", "karthikraghav@gmail.com, shankar@vshank77.com", "Newsletter Signup Received", strResults, True)
        Catch ex As Exception

        End Try

    End Sub

    Public Function ReadXmlImages(ByVal strP As String) As String
        Dim strXmlPath As String = System.Web.HttpContext.Current.Server.MapPath(strP)
        Dim strDescription As String = ""
        Dim strData As String = String.Empty
        If Directory.Exists(strXmlPath) Then
            For Each strData In Directory.GetFiles(strXmlPath)
                If Path.GetExtension(strData) = ".xml" Then
                    Dim xmlDoc As New XmlDocument
                    xmlDoc.Load(strData)
                    If xmlDoc.InnerXml.Contains("displayorder") Then
                        Dim DS As New DataSet
                        DS.ReadXml(strData)
                        Dim dv As New DataView(DS.Tables("sponsor"))
                        dv.Sort = "displayorder ASC"
                        If dv.ToTable.Rows.Count > 0 Then
                            For Each dr As DataRow In dv.ToTable.Rows
                                Dim strDate As String = dr("date").ToString()
                                Dim dt_Todate As Date = CDate(Date.Today)
                                Dim dt_selectedDate As Date = CDate(strDate)
                                Dim DateState As Integer = Date.Compare(dt_selectedDate, dt_Todate)
                                If DateState >= 0 Then
                                    strDescription &= "<div class=""advert"" >" & vbCrLf
                                    strDescription &= "<a href='" & dr("url").ToString() & "' target=""_blank""><img src='" & dr("image").ToString() & "' style=""width: 252px; height: 150px; border:0px;"" alt='" & dr("text").ToString() & "' title='" & dr("text").ToString() & "' /></a>" & vbCrLf
                                    strDescription &= "</div>" & vbCrLf
                                End If
                            Next
                        Else
                        End If
                    End If
                Else
                End If
            Next
        Else
        End If

        Return strDescription
    End Function

    Public Function LoadPopularArticles() As String
        Dim strXMLFolderPath As String = System.Web.HttpContext.Current.Server.MapPath("/XML/popular/")
        Dim strData As String = String.Empty
        Dim strResults As String = ""
        If Directory.Exists(strXMLFolderPath) Then
            For Each strData In Directory.GetFiles(strXMLFolderPath)
                If Path.GetExtension(strData) = ".xml" Then
                    Dim DS As New DataSet
                    DS.ReadXml(strData)
                    If DS.Tables(0).Rows.Count > 0 Then
                        strResults &= "<ul class=""nav"">" & vbCrLf
                        For Each dr As DataRow In DS.Tables(0).Rows()
                            If dr("Description1") <> "" Then
                                strResults &= "<li><a href=""" & dr("url1").ToString() & """>" & dr("Description1").ToString & "</a></li>" & vbCrLf
                            End If
                            If dr("Description2") <> "" Then
                                strResults &= "<li><a href=""" & dr("url2").ToString() & """>" & dr("Description2").ToString & "</a></li>" & vbCrLf
                            End If
                            If dr("Description3") <> "" Then
                                strResults &= "<li><a href=""" & dr("url3").ToString() & """>" & dr("Description3").ToString & "</a></li>" & vbCrLf
                            End If
                            If dr("Description4") <> "" Then
                                strResults &= "<li><a href=""" & dr("url4").ToString() & """>" & dr("Description4").ToString & "</a></li>" & vbCrLf
                            End If
                            If dr("Description5") <> "" Then
                                strResults &= "<li><a href=""" & dr("url5").ToString() & """>" & dr("Description5").ToString & "</a></li>" & vbCrLf
                            End If
                            If dr("Description6") <> "" Then
                                strResults &= "<li><a href=""" & dr("url6").ToString() & """>" & dr("Description6").ToString & "</a></li>" & vbCrLf
                            End If
                            If dr("Description7") <> "" Then
                                strResults &= "<li><a href=""" & dr("url7").ToString() & """>" & dr("Description7").ToString & "</a></li>" & vbCrLf
                            End If
                            If dr("Description8") <> "" Then
                                strResults &= "<li><a href=""" & dr("url8").ToString() & """>" & dr("Description8").ToString & "</a></li>" & vbCrLf
                            End If
                            If dr("Description9") <> "" Then
                                strResults &= "<li><a href=""" & dr("url9").ToString() & """>" & dr("Description9").ToString & "</a></li>" & vbCrLf
                            End If
                            If dr("Description10") <> "" Then
                                strResults &= "<li><a href=""" & dr("url10").ToString() & """>" & dr("Description10").ToString & "</a></li>" & vbCrLf
                            End If
                        Next
                        strResults &= "</ul>" & vbCrLf
                    Else
                    End If
                End If
            Next
        Else
        End If
        Return strResults
    End Function

    Public Function LoadLatestArticles() As String
        Dim strXMLArticles As String = System.Web.HttpContext.Current.Server.MapPath("/Xml/articles/")
        Dim DS As New DataSet
        Dim Dss As New DataSet
        Dim dt As New DataTable
        Dim strData As String = ""
        Dim strResults As String = ""
        Dim strURL As String = ""
        Dim ArticleId As New DataColumn("ArticleId", GetType(String))
        Dim ArticleHeading As New DataColumn("ArticleHeading", GetType(String))
        Dim ArticleDate As New DataColumn("ArticleDate", GetType(String))
        Dim category As New DataColumn("category", GetType(String))
        Dim ArticleSort As New DataColumn("ArticleSort", GetType(String))

        dt.Columns.Add(ArticleId)
        dt.Columns.Add(ArticleDate)
        dt.Columns.Add(category)
        dt.Columns.Add(ArticleSort)
        dt.Columns.Add(ArticleHeading)

        If Directory.Exists(strXMLArticles) Then
            For Each strData In Directory.GetFiles(strXMLArticles)
                If Path.GetExtension(strData) = ".xml" Then
                    DS.ReadXml(strData)
                    If DS.Tables(0).Rows.Count > 0 Then

                        Dim tdr As DataRow
                        tdr = dt.NewRow()
                        For Each dr As DataRow In DS.Tables(0).Rows()
                            tdr("ArticleId") = dr("ArticleId").ToString()
                            tdr("ArticleHeading") = dr("ArticleHeading").ToString()
                            tdr("ArticleDate") = dr("ArticleDate").ToString()
                            tdr("category") = dr("category").ToString()

                            Dim strDate As String = dr("ArticleDate").ToString()
                            Dim dt_ArticleDate As Date = CDate(strDate)
                            tdr("ArticleSort") = dr("ArticleId").ToString() 'dt_ArticleDate
                        Next
                        dt.Rows.Add(tdr)
                    Else
                    End If
                End If
            Next

            Dss.Tables.Add(dt)

            Dim dstArticle As New DataSet
            dstArticle = GetTopFive(Dss)

            If dstArticle.Tables(0).Rows.Count > 0 Then
                strResults &= "<div class=""boxheader"">Latest Articles</div>" & vbCrLf
                strResults &= "<ul class=""nav"">" & vbCrLf
                For Each dr As DataRow In dstArticle.Tables(0).Rows
                    strURL = GenerateURL(dr("ArticleHeading").ToString(), dr("articleid"), "articles/" & LCase(dr("category")) & "/" & "", "-")
                    strResults &= "<li><a href=""" & strURL & """>" & dr("ArticleHeading").ToString() & "</a></li>" & vbCrLf
                Next
                strResults &= "</ul>" & vbCrLf
            Else
            End If
        Else
            'strResults = "<span class=""box-txt"">No Articles Found</span><br /><br />"
        End If
        Return strResults
    End Function

    Public Function LoadUpcomingEvents() As String
        Dim strXMLArticles As String = System.Web.HttpContext.Current.Server.MapPath("/Xml/events/")

        Dim Dss As New DataSet
        Dim dt As New DataTable
        Dim strData As String = ""
        Dim strResults As String = ""
        Dim strURL As String = ""

        Dim EventDate As New DataColumn("EventDate", GetType(String))
        Dim EventID As New DataColumn("EventID", GetType(String))
        Dim EventHeading As New DataColumn("EventHeading", GetType(String))
        Dim EventDatesort As New DataColumn("EventDatesort", GetType(Date))

        dt.Columns.Add(EventDate)
        dt.Columns.Add(EventID)
        dt.Columns.Add(EventHeading)
        dt.Columns.Add(EventDatesort)

        If Directory.Exists(strXMLArticles) Then
            For Each strData In Directory.GetFiles(strXMLArticles)
                If Path.GetExtension(strData) = ".xml" Then
                    Dim DS As New DataSet
                    DS.ReadXml(strData)
                    If DS.Tables(0).Rows.Count > 0 Then
                        For Each dr As DataRow In DS.Tables(0).Rows()
                            Dim dt_Todate As Date = CDate(Date.Today)
                            Dim dt_selectedDate As Date = CDate(dr("EventDate"))
                            Dim DateState As Integer = Date.Compare(dt_selectedDate, dt_Todate)
                            If DateState >= 0 Then
                                Dim tdr As DataRow
                                tdr = dt.NewRow()
                                Dim dt_ArticleDate As Date = CDate(dr("EventDate").ToString())
                                tdr("EventDate") = dr("EventDate").ToString()
                                tdr("EventID") = dr("EventID").ToString()
                                tdr("EventHeading") = dr("EventHeading").ToString()
                                tdr("EventDatesort") = dt_ArticleDate
                                dt.Rows.Add(tdr)
                            End If
                        Next
                    Else
                    End If
                End If
            Next
            Dss.Tables.Add(dt)
            Dim dstArticle As New DataSet
            dstArticle = GetTopThree(Dss)
            If dstArticle.Tables(0).Rows.Count > 0 Then
                strResults &= "<div class=""boxheader"">Upcoming SIS Events</div>" & vbCrLf
                strResults &= "<ul class=""nav"">" & vbCrLf
                For Each dr As DataRow In dstArticle.Tables(0).Rows
                    strURL = GenerateURL(dr("EventHeading").ToString(), dr("EventID"), "sis-events/" & "", "-")
                    strResults &= "<li><a href=""" & strURL & """>" & dr("EventHeading").ToString() & "</a></li>" & vbCrLf
                Next
                strResults &= "</ul>" & vbCrLf
            Else
            End If
        Else
        End If
        Return strResults
    End Function

    Public Function LoadOtherEvents() As String
        Dim strXMLArticles As String = System.Web.HttpContext.Current.Server.MapPath("/XML/otherevents/")
        Dim Dss As New DataSet
        Dim dt As New DataTable
        Dim strData As String = ""
        Dim strResults As String = ""
        Dim strURL As String = ""

        Dim EventDate As New DataColumn("EventDate", GetType(String))
        Dim EventID As New DataColumn("EventID", GetType(String))
        Dim EventHeading As New DataColumn("EventHeading", GetType(String))
        Dim EventDatesort As New DataColumn("EventDatesort", GetType(Date))

        dt.Columns.Add(EventDate)
        dt.Columns.Add(EventID)
        dt.Columns.Add(EventHeading)
        dt.Columns.Add(EventDatesort)

        If Directory.Exists(strXMLArticles) Then
            For Each strData In Directory.GetFiles(strXMLArticles)
                If Path.GetExtension(strData) = ".xml" Then
                    Dim DS As New DataSet
                    DS.ReadXml(strData)
                    If DS.Tables(0).Rows.Count > 0 Then
                        For Each dr As DataRow In DS.Tables(0).Rows()
                            Dim dt_Todate As Date = CDate(Date.Today)
                            Dim dt_selectedDate As Date = CDate(dr("EventDate"))
                            Dim DateState As Integer = Date.Compare(dt_selectedDate, dt_Todate)
                            If DateState >= 0 Then
                                Dim tdr As DataRow
                                tdr = dt.NewRow()
                                Dim dt_ArticleDate As Date = CDate(dr("EventDate").ToString())
                                tdr("EventDate") = dr("EventDate").ToString()
                                tdr("EventID") = dr("EventID").ToString()
                                tdr("EventHeading") = dr("EventHeading").ToString()
                                tdr("EventDatesort") = dt_ArticleDate
                                dt.Rows.Add(tdr)
                            End If
                        Next
                    Else
                    End If
                End If
            Next
            Dss.Tables.Add(dt)
            Dim dstArticle As New DataSet
            dstArticle = GetTopThree(Dss)
            If dstArticle.Tables(0).Rows.Count > 0 Then
                strResults &= "<div class=""boxheader"">Other Events</div>" & vbCrLf
                strResults &= "<ul class=""nav"">" & vbCrLf
                For Each dr As DataRow In dstArticle.Tables(0).Rows
                    strURL = GenerateURL(dr("EventHeading").ToString(), dr("EventID"), "other-events/" & "", "-")
                    strResults &= "<li><a href=""" & strURL & """>" & dr("EventHeading").ToString() & "</a></li>" & vbCrLf
                Next
                strResults &= "</ul>" & vbCrLf
            Else
                strResults = "No Events Found"
            End If
        Else
        End If
        Return strResults
    End Function

    Public Function ReadBanners(ByVal strP As String) As String
        Dim strXmlPath As String = System.Web.HttpContext.Current.Server.MapPath(strP)
        Dim strDescription As String = ""
        Dim strData As String = String.Empty
        Dim strImage As String = "/images/banners/"
        If Directory.Exists(strXmlPath) Then
            For Each strData In Directory.GetFiles(strXmlPath)
                If Path.GetExtension(strData) = ".xml" Then
                    Dim xmlDoc As New XmlDocument
                    xmlDoc.Load(strData)
                    If xmlDoc.InnerXml.Contains("no") Then
                        Dim DS As New DataSet
                        DS.ReadXml(strData)
                        Dim dv As New DataView(DS.Tables("Order"))
                        dv.Sort = "no ASC"
                        If dv.ToTable.Rows.Count > 0 Then
                            strDescription &= "<div class=""box_skitter box_skitter_large"">" & vbCrLf
                            strDescription &= "<ul>"
                            For Each dr As DataRow In dv.ToTable.Rows
                                strDescription &= "<li><a href='" & dr("link").ToString() & "'><img src='" & strImage & dr("image").ToString() & "' style= ""width: 668px; height: 346px; border:0px;""  alt='" & dr("alt").ToString() & "' title='" & dr("alt").ToString() & "' class=""block""/></a></li>" & vbCrLf
                            Next
                            strDescription &= "</ul>" & vbCrLf
                            strDescription &= "</div>" & vbCrLf
                        Else
                        End If
                    End If
                Else
                End If
            Next
        Else
        End If

        Return strDescription
    End Function

    Public Function LoadArticleCategories() As String
        Dim strResults As String = ""
        Try
            strResults &= "<div class=""article-panel art-categories"">"
            strResults &= "<h2>Article Categories</h2>"
            strResults &= "<ul>"
            strResults &= "<li><a href=""/articles/art/"">Arts</a></li>"
            strResults &= "<li><a href=""/articles/culture/"">Culture</a></li>"
            strResults &= "<li><a href=""/articles/recipes/"">Recipes</a></li>"
            strResults &= "<li><a href=""/articles/religion/"">Religion</a></li>"
            strResults &= "<li><a href=""/articles/temples/"">Temples</a></li>"
            strResults &= "<li style=""border:0px;""><a href=""/articles/others/"">Others</a></li>"
            strResults &= "</ul>"
            strResults &= "</div>"

        Catch ex As Exception

        End Try
        Return strResults
    End Function

    Public Function LoadUsefulInfo() As String
        Dim strResults As String = ""
        Try
            strResults &= "<div class=""article-panel art-categories"">"
            strResults &= "<h2>Useful Information</h2>"
            strResults &= "<ul>"
            strResults &= "<li><a href=""/useful-info/tamil-panchangam.aspx"">Tamil Panchangam Calendar</a></li>"
            strResults &= "<li><a href=""/useful-info/online-panchangam.aspx"">Online Tamil Panchangam</a></li>"
            strResults &= "<li><a href=""/useful-info/tamil-new-year-names.aspx"">Tamil New year Names</a></li>"
            strResults &= "<li><a href=""/useful-info/uk-hindu-temples.aspx"">UK Hindu Temple List</a></li>"
            strResults &= "<li><a href=""/useful-info/uk-hindu-priests.aspx"">UK Hindu Priests(Gurukal) List</a></li>"
            strResults &= "<li><a href=""/useful-info/eleven-plus-exam-tips.aspx"">11 Plus Exam Tips</a></li>"
            strResults &= "<li><a href=""/useful-info/buckinghamshire-grammar-schools.aspx"">Buckinghamshire<br />Grammar Schools</a></li>"
            strResults &= "<li style=""border:0px;""><a href=""/useful-info/sandhyavandhanam-classes.aspx"">London Sandhyavandhanam<br />Classes</a></li>"
            strResults &= "</ul>"
            strResults &= "</div>"

        Catch ex As Exception
            strResults = ex.Message
        End Try
        Return strResults
    End Function

End Class
