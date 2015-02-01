Imports System.Xml
Imports System
Imports System.Data
Imports System.IO
Partial Class admin_othereventlist
    Inherits System.Web.UI.Page
    Dim strXMLFolderPath As String = Server.MapPath("/XML/otherevents/")
    Dim strData As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("UserID") = "" Then
            Response.Redirect("default.aspx")
        End If
        If Not IsPostBack Then
            LoadEvents()
        End If
    End Sub
    Private Sub LoadEvents()

        Dim DS As New DataSet
        Dim Dss As New DataSet
        Dim dt As New DataTable
        Dim Dv As New DataView

        Dim EventID As New DataColumn("EventID", GetType(String))
        Dim EventHeading As New DataColumn("EventHeading", GetType(String))
        Dim EventDate As New DataColumn("EventDate", GetType(String))
        Dim Address As New DataColumn("Address", GetType(String))

        dt.Columns.Add(EventID)
        dt.Columns.Add(EventHeading)
        dt.Columns.Add(EventDate)
        dt.Columns.Add(Address)

        If Directory.Exists(strXMLFolderPath) Then
            For Each strData In Directory.GetFiles(strXMLFolderPath)
                If Path.GetExtension(strData) = ".xml" Then

                    DS.ReadXml(strData)
                    If DS.Tables(0).Rows.Count > 0 Then

                        Dim tdr As DataRow
                        tdr = dt.NewRow()
                        For Each dr As DataRow In DS.Tables(0).Rows()
                            tdr("EventID") = dr("EventID").ToString()
                            tdr("EventHeading") = dr("EventHeading").ToString()
                            tdr("EventDate") = dr("EventDate").ToString()
                            tdr("Address") = dr("Address").ToString()
                        Next
                        dt.Rows.Add(tdr)
                    Else
                        lblError.Text = "No Records Found"
                    End If
                End If
            Next

            Dss.Tables.Add(dt)
            Dv = Dss.Tables(0).DefaultView
            Dv.Sort = "EventID DESC"

            gvwMessages.DataSource = Dv 'Dss
            gvwMessages.DataBind()

            If strData = "" Then
                lblError.Text = "No Records Found"
            End If
        Else
            lblError.Text = "No Records Found"
        End If
    End Sub

    Protected Sub gvwMessages_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvwMessages.RowDataBound
        Dim lblEventDate As Label
        If e.Row.RowType = DataControlRowType.DataRow Then
            lblEventDate = CType(e.Row.FindControl("lblEventDate"), Label)
            If Date.Parse(lblEventDate.Text) < Date.Now.Date Then
                e.Row.BackColor = Drawing.Color.Red
            End If
        End If
    End Sub

End Class
