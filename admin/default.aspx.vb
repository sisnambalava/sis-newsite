Imports System
Imports System.Data
Partial Class admin_default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblMsg.Text = ""
        Session.RemoveAll()
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Try
            If txtusername.Text = "administrator" And txtpassword.Text = "adm1nS1S" Then
                Session.Add("UserID", "administrator")
                Response.Redirect("homepagebanners.aspx")
            Else
                lblMsg.Text = "Invalid user name or password"
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
        End Try
    End Sub

End Class
