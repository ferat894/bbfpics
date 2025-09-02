Imports Microsoft.VisualBasic

Public Class WebConfig


    Public Shared ReadOnly Property SQL_CONNECTION As String
        Get
            Return ConfigurationManager.ConnectionStrings("SQL_CONNECTION").ConnectionString
        End Get
    End Property

    Public Shared ReadOnly Property EMAIL_SERVER As String
        Get
            Return ConfigurationManager.AppSettings("EMAIL_SERVER").ToString
        End Get
    End Property


    ''' <summary>
    ''' fallback email address if none is present in the site configuration
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property DEFAULT_EMAIL_ADDRESS As String
        Get
            Return ConfigurationManager.AppSettings("DEFAULT_EMAIL_ADDRESS").ToString
        End Get
    End Property



    Public Shared ReadOnly Property SiteID As Integer
        Get
            Dim x As String = ConfigurationManager.AppSettings("SiteID").ToString

            If Not String.IsNullOrWhiteSpace(x) AndAlso IsNumeric(x) Then
                Return x
            Else
                Return -1
            End If
        End Get
    End Property


End Class
