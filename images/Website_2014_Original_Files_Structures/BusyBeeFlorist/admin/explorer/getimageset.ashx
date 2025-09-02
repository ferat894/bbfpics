<%@ WebHandler Language="VB" Class="GetImageSet" %>

Imports System
Imports System.Web
Imports System.IO
Imports System.Net
Imports System.Net.Mail




Public Class GetImageSet : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        context.Response.Clear()
        context.Response.ContentType = "text"
        Call GetImages(context)
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property
    
    
    Private Sub GetImages(ByVal context As HttpContext)

        Dim categoryPath As String = "/images/flowers/"
        Dim sFor As String = context.Request.QueryString("for")
        
        
        '//for extras
        If sFor = "extras" Then categoryPath = categoryPath & "extras/"
        
        If Not String.IsNullOrWhiteSpace(categoryPath) Then
        
            Dim sb As Text.StringBuilder = New Text.StringBuilder
            Dim filenameURL As String = String.Empty
            Dim filenameHtml As String = String.Empty
            Dim fileNameWrap As String = String.Empty
            
                       
            
            For Each f As String In IO.Directory.GetFiles(context.Server.MapPath(categoryPath))
                filenameURL = context.Server.UrlEncode(IO.Path.GetFileName(f))
                filenameHtml = HttpUtility.HtmlDecode(IO.Path.GetFileName(f))
                
                fileNameWrap = filenameHtml
                If (fileNameWrap.Length > 13) Then fileNameWrap = fileNameWrap.Substring(0, 13) & "..."
                
                sb.AppendFormat("<div class=""thumb-images""><a href=""#"" class=""xplorer-image"" onclick=""javascript:select(this)"" rel=""{2}{0}"" title=""{0}"" ><img src=""getimage.ashx?for={3}&filename={1}&width=100&height=100"" width=""100px"" height=""100px"" alt="""" /><div class=""image-title"">{4}</div></a></div>" & vbNewLine, filenameHtml, filenameURL, categoryPath, sFor, fileNameWrap)
            Next
        
            context.Response.Write(sb.ToString)
        Else
            context.Response.Write("Error: no category")
        End If
        
        
    End Sub
    
End Class
