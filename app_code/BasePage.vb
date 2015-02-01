Imports System
Imports System.IO
Imports Microsoft.VisualBasic

Public Class BasePage
    Inherits System.Web.UI.MasterPage

    Protected Overloads Overrides Sub Render(ByVal writer As System.Web.UI.HtmlTextWriter)
        Dim stringWriter As New System.IO.StringWriter()
        Dim htmlWriter As New HtmlTextWriter(stringWriter)
        MyBase.Render(htmlWriter)
        Dim html As String = stringWriter.ToString()
        Dim StartPoint As Integer = html.IndexOf("<input type=""hidden"" name=""__VIEWSTATE""")
        If StartPoint >= 0 Then
            Dim EndPoint As Integer = html.IndexOf("/>", StartPoint) + 2
            Dim viewstateInput As String = html.Substring(StartPoint, EndPoint - StartPoint)
            html = html.Remove(StartPoint, EndPoint - StartPoint)
            Dim FormEndStart As Integer = html.IndexOf("</form>")
            If FormEndStart >= 0 Then
                html = html.Insert(FormEndStart, "<div>" & viewstateInput & "</div>" & vbCrLf)
            End If
        End If
        writer.Write(html)
    End Sub

End Class
