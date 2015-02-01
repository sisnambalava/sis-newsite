Imports System.Xml
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Partial Class admin_messagelist
    Inherits System.Web.UI.Page
    Dim strXMLFolderPath As String = Server.MapPath("/XML/home/")
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

        Dim MessageId As New DataColumn("MessageId", GetType(String))
        Dim Description As New DataColumn("Description", GetType(String))
        Dim ExpiryDate As New DataColumn("ExpiryDate", GetType(String))

        dt.Columns.Add(MessageId)
        dt.Columns.Add(Description)
        dt.Columns.Add(ExpiryDate)

        If Directory.Exists(strXMLFolderPath) Then
            For Each strData In Directory.GetFiles(strXMLFolderPath)
                If Path.GetExtension(strData) = ".xml" Then

                    DS.ReadXml(strData)
                    If DS.Tables(0).Rows.Count > 0 Then

                        Dim tdr As DataRow
                        tdr = dt.NewRow()
                        For Each dr As DataRow In DS.Tables(0).Rows()
                            tdr("MessageId") = dr("MessageId").ToString()
                            tdr("Description") = dr("Description").ToString()
                            tdr("ExpiryDate") = dr("ExpiryDate").ToString()
                        Next
                        dt.Rows.Add(tdr)
                    Else
                        lblError.Text = "No Record Found..."
                    End If
                End If
            Next

            Dss.Tables.Add(dt)
            gvwMessages.DataSource = Dss
            gvwMessages.DataBind()

            If strData = "" Then
                lblError.Text = "No Record Found..."
            End If
        Else
            lblError.Text = "No Record Found..."
        End If
    End Sub

End Class
