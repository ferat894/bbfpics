<%@ Application Language="VB" %>
<%@ Import Namespace="System.Web.Routing" %>

<script runat="server">

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        RouteConfig.RegisterRoutes(RouteTable.Routes)
        
        '<add key="ValidationSettings:UnobtrusiveValidationMode" value="WebForms" />
        'Dim def As New ScriptResourceDefinition()
        'With def
        '    .Path = "~/scripts/jquery-1.9.1.min.js"
        '    .DebugPath = "~/scripts/jquery-1.9.1.js"
        '    .CdnPath = "http://ajax.microsoft.com/ajax/jQuery/jquery-1.9.1.min.js"
        '    .CdnDebugPath = "http://ajax.microsoft.com/ajax/jQuery/jquery-1.9.1.js"
        'End With
        'ScriptManager.ScriptResourceMapping.AddDefinition("jquery", def)
        
    End Sub
    
    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application shutdown
    End Sub
        
    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when an unhandled error occurs
    End Sub

    
    
    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a new session is started

        
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a session ends. 
        ' Note: The Session_End event is raised only when the sessionstate mode
        ' is set to InProc in the Web.config file. If session mode is set to StateServer 
        ' or SQLServer, the event is not raised.
    End Sub
    
        
    
    Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
        
        'If HttpContext.Current.Response.Headers.AllKeys.Contains("Server") Then HttpContext.Current.Response.Headers.Remove("Server")
        'If HttpContext.Current.Response.Headers.AllKeys.Contains("X-Powered-By") Then HttpContext.Current.Response.Headers.Remove("X-Powered-By")
        'If HttpContext.Current.Response.Headers.AllKeys.Contains("X-AspNet-Version") Then HttpContext.Current.Response.Headers.Remove("X-AspNet-Version")
            
        Dim sn As String = HttpContext.Current.Request.ServerVariables("SCRIPT_NAME")
        If Not String.IsNullOrWhiteSpace(sn) Then
            
            Select Case sn.ToString.ToLower
                Case "/admin/explorer/uploader", "/admin/explorer/uploader.aspx"
                    Exit Sub
                    
                Case "/admin/errors", "/admin/errors.aspx", "/admin/elmah.axd", "/elmah.axd"
                    Exit Sub
            End Select

        End If
        
        
        HttpContext.Current.Response.AddHeader("X-Frame-Options", "DENY")
        
    End Sub
       
</script>