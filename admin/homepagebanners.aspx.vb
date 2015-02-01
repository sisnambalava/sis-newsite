Imports System
Imports System.Data
Imports System.IO
Imports System.Xml
Imports System.Xml.Linq
Imports GrayMatterSoft
Partial Class admin_homepagebanners
    Inherits System.Web.UI.Page
    Dim DtImage As New DataTable
    Public strImage1 As String = ""
    Dim strMainPath As String = "/images/banners/"
    Dim cnt As Integer = 0
    'Dim xdoc As XDocument = XDocument.Load(Server.MapPath("data.xml"))
    Dim xdoc As New XmlDocument

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If File.Exists(Server.MapPath("/XML/banner/data.xml")) Then
            xdoc.Load(Server.MapPath("/XML/banner/data.xml"))
        End If
        'xdoc.Load(Server.MapPath("data.xml"))
        If Not IsPostBack Then
            PopulateGrid()
        End If
    End Sub
    Private Sub PopulateGrid()
        'Dim dt As New DataTable()
        'Dim tdr As DataRow

        'Dim no As New DataColumn("no", GetType(Integer))
        'Dim alt As New DataColumn("alt", GetType(String))
        'Dim link As New DataColumn("link", GetType(String))
        'Dim expdate As New DataColumn("date", GetType(Date))
        'Dim image As New DataColumn("image", GetType(String))
        Dim dset As New DataSet()

        Dim dt As New DataTable()
        Dim tdr As DataRow

        Dim no As New DataColumn("no", GetType(String))
        Dim alt As New DataColumn("alt", GetType(String))
        Dim link As New DataColumn("link", GetType(String))
        Dim expdate As New DataColumn("expdate", GetType(String))
        Dim image As New DataColumn("image", GetType(String))


        dt.Columns.Add(no)
        dt.Columns.Add(alt)
        dt.Columns.Add(link)
        dt.Columns.Add(expdate)
        dt.Columns.Add(image)

        Dim filePath As String = "/XML/banner/data.xml"
        'Dim count As Integer = 1
        If File.Exists(Server.MapPath("data.xml")) Then
            dset.ReadXml(Server.MapPath(filePath))
            If dset.Tables.Count > 0 Then
                If dset.Tables(0).Rows.Count > 0 Then
                    For Each dr As DataRow In dset.Tables(0).Rows
                        tdr = dt.NewRow()
                        tdr("no") = dr("no")
                        'tdr("no") = count
                        tdr("alt") = dr("alt")
                        tdr("link") = dr("link")
                        tdr("expdate") = dr("expdate")
                        tdr("image") = "/images/banners/" + dr("image")
                        dt.Rows.Add(tdr)
                        'count = count + 1
                    Next
                End If
            End If
        End If
        If dt.Rows.Count < 6 Then
            Dim iEmptyRowCount As Integer = 6 - dt.Rows.Count
            Dim i As Integer
            For i = 1 To iEmptyRowCount
                tdr = dt.NewRow()
                tdr("no") = dt.Rows.Count + 1
                tdr("alt") = ""
                tdr("link") = ""
                tdr("expdate") = ""
                tdr("image") = ""
                dt.Rows.Add(tdr)
                'count = count + 1
            Next
        End If
        gvwBanner.DataSource = dt
        gvwBanner.DataBind()

        DtImage = LoadImageSelection()
        Session("DtImage") = ""
        Session("DtImage") = DtImage
        If DtImage.Rows.Count = 0 Then
            lblMag.Visible = True
            lblMag.Text = "Server folder does't contain image."
            BtnUpload.Enabled = False
        Else
            DtLstImgDisplay.DataSource = DtImage
            DtLstImgDisplay.DataBind()
        End If
    End Sub
    Protected Sub gvwBanner_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvwBanner.RowCommand

        For Each drow As GridViewRow In gvwBanner.Rows
            Dim img As Image = DirectCast(drow.FindControl("imgOffer"), Image)
            Dim flupload As FileUpload = DirectCast(drow.FindControl("flupload"), FileUpload)
            If flupload.HasFile Then
                If Not File.Exists(Server.MapPath(strMainPath & flupload.FileName)) Then
                    flupload.SaveAs(Server.MapPath(strMainPath & flupload.FileName))
                Else
                    flupload.SaveAs(Server.MapPath(strMainPath & "img" & cnt & "-" & flupload.FileName))
                    cnt = cnt + 1
                End If
                flupload.SaveAs(Server.MapPath(strMainPath & flupload.FileName))
                strImage1 = strMainPath & flupload.FileName
                img.ImageUrl = strImage1
            End If
        Next

        If e.CommandName = "EditImage" Or e.CommandName = "EditUploadImage" Then
            Dim gvrhome As GridViewRow = TryCast(DirectCast((e.CommandSource), Control).NamingContainer, GridViewRow)
            Dim strSno As String = gvwBanner.DataKeys(gvrhome.RowIndex).Values("no").ToString()
            hdnsno.Value = gvrhome.RowIndex
            ViewState("csno") = ""
            ViewState("csno") = gvrhome.RowIndex

            pnlimageselection.Visible = True

            'DtImage = LoadImageSelection()
            'Session("DtImage") = ""
            'Session("DtImage") = DtImage
            'If DtImage.Rows.Count = 0 Then
            '    lblMag.Visible = True
            '    lblMag.Text = "Server folder does't contain image."
            '    BtnUpload.Enabled = False
            'Else
            '    DtLstImgDisplay.DataSource = DtImage
            '    DtLstImgDisplay.DataBind()
            'End If
            'ccmpopexe.Show()
            'txtsearch.Text = ""
        End If
        If e.CommandName = "Deleterow" Then
            Dim gvrhome As GridViewRow = TryCast(DirectCast((e.CommandSource), Control).NamingContainer, GridViewRow)
            Dim strSno As String = e.CommandArgument
            Dim list As XmlNodeList
            list = xdoc.SelectNodes("Orders/Order")
            If list.Count > 0 Then
                For Each element As XmlNode In list
                    If element.SelectSingleNode("no").InnerText = strSno Then
                        'element.RemoveAll()
                        Dim xmlnode As XmlNode = element.ParentNode
                        xmlnode.RemoveChild(element)
                        'xdoc.RemoveChild(xmlnode)
                    End If
                Next
                xdoc.Save(Server.MapPath("~/XML/banner/data.xml"))
            End If
            Dim listup As XmlNodeList
            listup = xdoc.SelectNodes("Orders/Order")
            Dim i As Integer = 1
            If listup.Count > 0 Then
                For Each element As XmlElement In listup
                    element.SelectSingleNode("no").InnerText = i
                    i = i + 1
                Next
                xdoc.Save(Server.MapPath("~/XML/banner/data.xml"))
            End If

            PopulateGrid()

        End If
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click

        Dim strno As String = ""
        Dim stralt As String = ""
        Dim strlink As String = ""
        Dim strdate As String = ""
        Dim strpath As String = ""

        If Not File.Exists(Server.MapPath("data.xml")) Then
            'File.Create(Server.MapPath("data.xml"))
            'xdoc.CreateNode(XmlNodeType.Document, "Orders", String.Empty)
            Dim xmlDeclaration As XmlDeclaration = xdoc.CreateXmlDeclaration("1.0", "utf-8", String.Empty)
            Dim rootNode As XmlElement = xdoc.CreateElement("Orders")
            xdoc.InsertBefore(xmlDeclaration, xdoc.DocumentElement)
            xdoc.AppendChild(rootNode)
            xdoc.Save(Server.MapPath("/XML/banner/data.xml"))
            xdoc.Load(Server.MapPath("/XML/banner/data.xml"))
        End If

        Dim list As XmlNodeList
        list = xdoc.SelectNodes("Orders/Order")
        For Each drow As GridViewRow In gvwBanner.Rows
            strno = DirectCast(drow.FindControl("txtorderno"), TextBox).Text.ToString()
            stralt = DirectCast(drow.FindControl("txtalttxt"), TextBox).Text.ToString()
            strlink = DirectCast(drow.FindControl("txtlink"), TextBox).Text.ToString()
            strdate = DirectCast(drow.FindControl("GMDatePicker1"), GMDatePicker).DateString


            Dim img As Image = DirectCast(drow.FindControl("imgOffer"), Image)
            Dim flupload As FileUpload = DirectCast(drow.FindControl("flupload"), FileUpload)

            If flupload.HasFile Then
                If Not File.Exists(Server.MapPath(strMainPath & flupload.FileName)) Then
                    flupload.SaveAs(Server.MapPath(strMainPath & flupload.FileName))
                Else
                    flupload.SaveAs(Server.MapPath(strMainPath & "img" & cnt & "-" & flupload.FileName))
                    cnt = cnt + 1
                End If

                strImage1 = strMainPath & flupload.FileName
                img.ImageUrl = strImage1
            End If
            strpath = Path.GetFileName(img.ImageUrl)
            If strpath = "noimage.jpg" Then
                strpath = ""
            End If

            'Dim update = xdoc.Root.Elements("Order").Where(Function(t) t.Element("no").Value.Equals(strno))
            Dim flag As Integer = 0
            If list.Count > 0 Then
                For Each element As XmlElement In list
                    If element.SelectSingleNode("no").InnerText = strno Then
                        element.SelectSingleNode("alt").InnerText = stralt
                        element.SelectSingleNode("link").InnerText = strlink
                        element.SelectSingleNode("expdate").InnerText = strdate
                        element.SelectSingleNode("image").InnerText = strpath
                        flag = 1
                    End If
                Next
                If flag = 0 Then
                    Dim parentNode As XmlElement = xdoc.CreateElement("Order")
                    Dim no As XmlElement = xdoc.CreateElement("no")
                    Dim alt As XmlElement = xdoc.CreateElement("alt")
                    Dim link As XmlElement = xdoc.CreateElement("link")
                    Dim expdate As XmlElement = xdoc.CreateElement("expdate")
                    Dim image As XmlElement = xdoc.CreateElement("image")

                    Dim notxt As XmlText = xdoc.CreateTextNode(strno)
                    Dim alttxt As XmlText = xdoc.CreateTextNode(stralt)
                    Dim linktxt As XmlText = xdoc.CreateTextNode(strlink)
                    Dim expdatetxt As XmlText = xdoc.CreateTextNode(strdate)
                    Dim imagetxt As XmlText = xdoc.CreateTextNode(strpath)

                    no.AppendChild(notxt)
                    alt.AppendChild(alttxt)
                    link.AppendChild(linktxt)
                    expdate.AppendChild(expdatetxt)
                    image.AppendChild(imagetxt)

                    parentNode.AppendChild(no)
                    parentNode.AppendChild(alt)
                    parentNode.AppendChild(link)
                    parentNode.AppendChild(expdate)
                    parentNode.AppendChild(image)

                    Dim xmlele As XmlElement = xdoc.SelectSingleNode("Orders")
                    xmlele.AppendChild(parentNode)
                    xdoc.Save(Server.MapPath("/XML/banner/data.xml"))
                End If
            Else 'First Parent Creation
                Dim parentNode As XmlElement = xdoc.CreateElement("Order")
                Dim no As XmlElement = xdoc.CreateElement("no")
                Dim alt As XmlElement = xdoc.CreateElement("alt")
                Dim link As XmlElement = xdoc.CreateElement("link")
                Dim expdate As XmlElement = xdoc.CreateElement("expdate")
                Dim image As XmlElement = xdoc.CreateElement("image")

                Dim notxt As XmlText = xdoc.CreateTextNode(strno)
                Dim alttxt As XmlText = xdoc.CreateTextNode(stralt)
                Dim linktxt As XmlText = xdoc.CreateTextNode(strlink)
                Dim expdatetxt As XmlText = xdoc.CreateTextNode(strdate)
                Dim imagetxt As XmlText = xdoc.CreateTextNode(strpath)

                no.AppendChild(notxt)
                alt.AppendChild(alttxt)
                link.AppendChild(linktxt)
                expdate.AppendChild(expdatetxt)
                image.AppendChild(imagetxt)

                parentNode.AppendChild(no)
                parentNode.AppendChild(alt)
                parentNode.AppendChild(link)
                parentNode.AppendChild(expdate)
                parentNode.AppendChild(image)

                Dim xmlele As XmlElement = xdoc.SelectSingleNode("Orders")
                xmlele.AppendChild(parentNode)
                xdoc.Save(Server.MapPath("/XML/banner/data.xml"))
            End If

            'For Each up As Object In update
            '    up.Element("alt").Value = stralt
            '    up.Element("link").Value = strlink
            '    up.Element("expdate").Value = strdate
            '    up.Element("image").Value = strpath
            'Next

            'xdoc.Element("Orders").Add(New XElement("Order", New XElement("no", strno), New XElement("alt", stralt), New XElement("link", strlink), New XElement("date", strdate), New XElement("image", strpath)))
            xdoc.Save(Server.MapPath("~/XML/banner/data.xml"))
        Next
        lblmsg.ForeColor = Drawing.Color.LightSeaGreen
        lblmsg.Font.Bold = True
        lblmsg.Text = "Submitted Successfully."
    End Sub

    Protected Sub UploadImageselection()
        Dim strPPCId As String = ""
        Dim strImageType As String = ""
        Dim strImageName As String = ""
        Dim StrReturnVal As String = ""
        Dim strMode As String = ""
        Dim strImagePathUrl As String = ""
        Dim XMLFILE As String = ""
        Dim strFiles As String = ""
        strImagePathUrl = "/images/banners/"
        Try
            Dim GV As GridViewRow = gvwBanner.Rows(Integer.Parse(ViewState("csno")))
            Dim img As Image = DirectCast(GV.FindControl("imgOffer"), Image)
            Dim lblShipImage As Label = DirectCast(GV.FindControl("lblShipImage"), Label)
            If Request.Form("rdoImageName") <> "" Then
                strImageName = Request.Form("rdoImageName").ToString()
                strFiles = Server.MapPath(strImagePathUrl & strImageName)
                If File.Exists(strFiles) Then
                    Dim strPath As String = ""
                    strPath = strImagePathUrl & strImageName
                    img.ImageUrl = strPath
                    lblShipImage.Text = strPath
                End If
            End If
        Catch ex As Exception
        Finally
        End Try
        pnlimageselection.Visible = False
    End Sub

    Protected Sub BtnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnUpload.Click
        UploadImageselection()
    End Sub
    Protected Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        pnlimageselection.Visible = False

    End Sub

    Public Function LoadImageSelection() As DataTable

        Dim strFolderPath As String = "/images/banners/"
        Dim DtImage As New DataTable()
        Try
            Dim DirInfo As New System.IO.DirectoryInfo(HttpContext.Current.Server.MapPath(strFolderPath))
            Dim col1 As New DataColumn("Name")
            col1.DataType = System.Type.[GetType]("System.String")
            DtImage.Columns.Add(col1)

            For Each chDir As FileInfo In DirInfo.GetFiles()
                If Not (chDir.Extension = ".db") Then
                    Dim row As DataRow
                    row = DtImage.NewRow()
                    row("Name") = chDir.Name.ToString()
                    DtImage.Rows.Add(row)
                End If
            Next

        Catch ex As Exception
            Dim str As String = ex.Message

            ' Response.Write(ex.Message)
        End Try
        Return DtImage
    End Function
End Class
