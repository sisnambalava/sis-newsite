Imports System
Imports System.Data
Imports System.Configuration
Imports System.Collections
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.IO
Imports System.Drawing.Text
Imports System.Drawing.Imaging
Imports System.Drawing
Imports System.Security.Cryptography
Partial Class admin_imgdisplay
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim ServerPath As [String] = Server.MapPath("/images/banners/") + Request.QueryString.[Get]("imagename")
        Dim ServerPath As String = ""
        ServerPath = "/images/banners/" & Request.QueryString("imagename")
        'Response.Write(ServerPath)
        'Response.End()
        Dim image As System.Drawing.Image = System.Drawing.Image.FromFile(Server.MapPath(ServerPath))
        Dim thumbnailImage As System.Drawing.Image = image.GetThumbnailImage(421, 259, New System.Drawing.Image.GetThumbnailImageAbort(AddressOf ThumbnailCallback), IntPtr.Zero)
        Dim imageStream As New MemoryStream()
        thumbnailImage.Save(imageStream, System.Drawing.Imaging.ImageFormat.Jpeg)
        Dim imageContent As Byte() = New [Byte](imageStream.Length - 1) {}
        imageStream.Position = 0
        imageStream.Read(imageContent, 0, CInt(imageStream.Length))
        Response.ContentType = "image/jpg"
        Response.BinaryWrite(imageContent)
    End Sub
    Public Function ThumbnailCallback() As Boolean
        Return True
    End Function
End Class
