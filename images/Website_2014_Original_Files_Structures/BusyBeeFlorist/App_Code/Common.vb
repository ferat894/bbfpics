Imports Microsoft.VisualBasic
Imports System.Net
Imports System.IO
Imports System.Threading

Public Class StringCompression


    ''' <summary>
    ''' compresses a string to ToBase64
    ''' </summary>
    ''' <param name="plainString"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CompressString(ByVal plainString As String) As String
        'compress
        Dim mem As New IO.MemoryStream
        Dim gz As New System.IO.Compression.GZipStream(mem, IO.Compression.CompressionMode.Compress)
        Dim sw As New IO.StreamWriter(gz)

        sw.WriteLine(plainString)
        sw.Close()

        'Convert the compressed byte array back to a string
        Return System.Convert.ToBase64String(mem.ToArray)
    End Function


    ''' <summary>
    ''' decompress a string from ToBase64
    ''' </summary>
    ''' <param name="compressedString"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function DecompressString(ByVal compressedString As String) As String

        ''decompress
        Dim mem2 As New IO.MemoryStream(System.Convert.FromBase64String(compressedString))
        Dim gz As System.IO.Compression.GZipStream = New System.IO.Compression.GZipStream(mem2, IO.Compression.CompressionMode.Decompress)
        Dim sr As New IO.StreamReader(gz)
        Dim uncompressed As String = sr.ReadLine
        sr.Close()

        Return uncompressed
    End Function


End Class

Public Class Common



    Public Const EMAIL_FORMAT_EXPRESSION As String = "\w+([-.]?\w?)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
    Public Const MONEY_FORMAT As String = "#,##0.00"

    Public Class URL

        Public Shared Function CurrentURL() As String
            Return HttpContext.Current.Request.ServerVariables("SCRIPT_NAME") & "?" & HttpContext.Current.Request.ServerVariables("QUERY_STRING")
        End Function
    End Class


    Public Class Messages

        Public Const RESUBMISSION As String = "The page seems to have been resubmitted with the same information, please restart the process and try again."
        Public Const PAGE_INVALID As String = "The page is not valid and contain errors, please review the page and try again."
        Public Const FILE_DOWNLOAD_NOT_EXISTS As String = "The file cannot be downloaded because it does not exists. Please contact Sportscover."

        ''' <summary>
        ''' displays the message on the page
        ''' </summary>
        ''' <param name="type">success,warning,danger,info</param>
        ''' <param name="message">the actual message</param>
        ''' <param name="FadeOutAfterSeconds">When supplied will cause to fade out</param>
        ''' <remarks></remarks>
        Public Shared Function DisplayMessage(ByVal type As String, ByVal message As String, Optional FadeOutAfterSeconds As Integer = 0) As String
            Return String.Format("<div id=""response-message-container"" class=""alert alert-dismissable alert-{0}""><button class=""close"" type=""button"" data-dismiss=""alert"">×</button>{1}</div>", type, message) & _
                IIf(FadeOutAfterSeconds > 0, "<script type=""text/javascript"">function closeMsg(){$('#response-message-container').fadeTo(1000,0)} setTimeout('closeMsg()'," & FadeOutAfterSeconds & "000);</script>", String.Empty)

        End Function

    End Class


    Public Class SimpleHash

        Public Shared Function SHA256(x As String) As String
            Return mrk.Security.Cryptography.Hash.EncryptSHA256(x, System.Text.Encoding.Unicode)
        End Function



    End Class
End Class
