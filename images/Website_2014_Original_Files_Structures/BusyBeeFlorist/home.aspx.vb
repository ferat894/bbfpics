
Partial Class home
    Inherits System.Web.UI.Page

    Protected Sub Page_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        'If Request.ServerVariables("SCRIPT_NAME").ToString.ToLower.Contains("testsession") Then Exit Sub

        'Dim o As Object = HttpContext.Current.Session("testonly")
        'If Not IsNothing(o) AndAlso HttpContext.Current.Session("testonly") = "1" Then
        '    Exit Sub
        'Else
        '    Response.Redirect("/TestSession.aspx")
        '    Response.End()
        '    Exit Sub
        'End If
    End Sub
End Class
