Imports System.Xml
Imports System
Imports System.Data
Imports System.IO

Partial Class admin_newsletterlist
    Inherits System.Web.UI.Page
    Dim strXMLFolderPath As String = Server.MapPath("/XML/newsletter/")
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

        Dim NletterID As New DataColumn("NletterID", GetType(String))
        Dim Heading As New DataColumn("Heading", GetType(String))
        Dim ExpiryDate As New DataColumn("ExpiryDate", GetType(String))
        Dim NMonth As New DataColumn("NMonth", GetType(String))
        Dim Nyear As New DataColumn("Nyear", GetType(String))

        dt.Columns.Add(NletterID)
        dt.Columns.Add(Heading)
        dt.Columns.Add(ExpiryDate)
        dt.Columns.Add(NMonth)
        dt.Columns.Add(Nyear)

        If Directory.Exists(strXMLFolderPath) Then
            For Each strData In Directory.GetFiles(strXMLFolderPath)
                If Path.GetExtension(strData) = ".xml" Then

                    DS.ReadXml(strData)
                    If DS.Tables(0).Rows.Count > 0 Then

                        Dim tdr As DataRow
                        tdr = dt.NewRow()
                        For Each dr As DataRow In DS.Tables(0).Rows()
                            tdr("NletterID") = dr("NletterID").ToString()
                            tdr("Heading") = dr("Heading").ToString()
                            tdr("ExpiryDate") = dr("ExpiryDate").ToString()
                            tdr("NMonth") = LCase(dr("NMonth").ToString())
                            tdr("NYear") = dr("NYear").ToString()
                        Next
                        dt.Rows.Add(tdr)
                    Else
                        lblError.Text = "No Records Found..."
                    End If
                End If
            Next

            Dss.Tables.Add(dt)
            Dv = Dss.Tables(0).DefaultView
            Dv.Sort = "NletterID DESC"

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
