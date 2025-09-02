<%@ WebHandler Language="VB" Class="GetImageFetcher" %>

Imports System
Imports System.Web
Imports System.IO
Imports System.Net
Imports System.Net.Mail




Public Class GetImageFetcher : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Call GetSingleImage(context)
                
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property
    
    
    Private Sub GetSingleImage(ByVal context As HttpContext)
        

        Dim categoryPath As String = "/images/flowers/"

        '//for extras
        If context.Request.QueryString("for") = "extras" Then categoryPath = categoryPath & "extras/"
        
        If String.IsNullOrWhiteSpace(categoryPath) Then Exit Sub
        
        
        ' Initialize objects
        Dim objImage As System.Drawing.Image, objThumbnail As System.Drawing.Image
        Dim strServerPath As String, strFilename As String
        Dim shtWidth As Short, shtHeight As Short
        ' Get image folder path on server - use "\" string if root
        strServerPath = context.Server.MapPath(categoryPath)
        ' Retrieve name of file to resize from query string
        strFilename = strServerPath & context.Request.QueryString("filename")
        ' Retrieve file, or error.gif if not available
        
        Try
            objImage = System.Drawing.Image.FromFile(strFilename)
        Catch ex As Exception
            objImage = System.Drawing.Image.FromFile(strServerPath & "error.gif")
        End Try

        ' Retrieve width from query string
        If context.Request.QueryString("width") = Nothing Then
            shtWidth = objImage.Width
        ElseIf context.Request.QueryString("width") < 1 Then
            shtWidth = 100
        Else
            shtWidth = context.Request.QueryString("width")
        End If
        

        If context.Request.QueryString("height") = Nothing Then
            
            ' Work out a proportionate height from width if no height specified
            shtHeight = objImage.Height / (objImage.Width / shtWidth)
            
        ElseIf context.Request.QueryString("height") < 1 Then
            shtHeight = 100
        Else
            shtHeight = context.Request.QueryString("height")
        End If
        
        

        ' Create thumbnail
        objThumbnail = objImage.GetThumbnailImage(shtWidth, shtHeight, Nothing, System.IntPtr.Zero)
        ' Send down to client
        context.Response.ContentType = "image/jpeg"
        objThumbnail.Save(context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg)
        ' Tidy up
        objImage.Dispose()
        objThumbnail.Dispose()
        
        
    End Sub
    
End Class
