Imports System.Xml
Imports System
Imports System.Data
Imports System.IO
Partial Class admin_articlelist
    Inherits System.Web.UI.Page
    Dim strXMLFolderPath As String = Server.MapPath("/XML/articles/")
    Dim strData As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("UserID") = "" Then
            Response.Redirect("default.aspx")
        End If

        If Not IsPostBack Then
            Pr_LoadMessage()
        End If
    End Sub
    Private Sub Pr_LoadMessage()

        Dim DS As New DataSet
        Dim Dss As New DataSet
        Dim dt As New DataTable
        Dim Dv As New DataView

        Dim ArticleID As New DataColumn("ArticleID", GetType(String))
        Dim ArticleHeading As New DataColumn("ArticleHeading", GetType(String))
        Dim ArticleDate As New DataColumn("ArticleDate", GetType(String))
        Dim Author As New DataColumn("Author", GetType(String))

        dt.Columns.Add(ArticleID)
        dt.Columns.Add(ArticleHeading)
        dt.Columns.Add(ArticleDate)
        dt.Columns.Add(Author)

        If Directory.Exists(strXMLFolderPath) Then
            For Each strData In Directory.GetFiles(strXMLFolderPath)
                If Path.GetExtension(strData) = ".xml" Then

                    DS.ReadXml(strData)
                    If DS.Tables(0).Rows.Count > 0 Then

                        Dim tdr As DataRow
                        tdr = dt.NewRow()
                        For Each dr As DataRow In DS.Tables(0).Rows()
                            tdr("ArticleID") = dr("ArticleID").ToString()
                            tdr("ArticleHeading") = dr("ArticleHeading").ToString()
                            tdr("ArticleDate") = dr("ArticleDate").ToString()
                            tdr("Author") = dr("Author").ToString()
                        Next
                        dt.Rows.Add(tdr)
                    Else
                        lblError.Text = "No Records Found..."
                    End If
                End If
            Next

            Dss.Tables.Add(dt)
            Dv = Dss.Tables(0).DefaultView
            Dv.Sort = "ArticleID DESC"

            gvwMessages.DataSource = Dv 'Dss
            gvwMessages.DataBind()

            If strData = "" Then
                lblError.Text = "No Records Found..."
            End If
        Else
            lblError.Text = "No Records Found..."
        End If
    End Sub

End Class
