Imports Microsoft.VisualBasic

Public Class XHeaderSecurityModule
    Implements IHttpModule



    Public Sub Dispose() Implements IHttpModule.Dispose

    End Sub

    Public Sub Init(context As HttpApplication) Implements IHttpModule.Init

        AddHandler context.BeginRequest, AddressOf Me.BeginRequest

    End Sub


    Public Sub BeginRequest(ByVal source As Object, ByVal e As EventArgs)
        Dim application As HttpApplication = DirectCast(source, HttpApplication)
        application.Context.Response.AddHeader("X-Frame-Options", "DENY")
    End Sub

End Class
