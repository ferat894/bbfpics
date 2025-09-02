
Partial Class _default
    Inherits System.Web.UI.Page

    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        If Request.QueryString("generate") = "error" Then
            Dim x As Integer = 0
            x = 1 / x
        End If
        Server.Transfer("home.aspx")
    End Sub


    Private Sub CopyImages()

        Dim s As String() = {"200.jpg",
       "201.jpg", "202.jpg", "203.jpg", "204.jpg",
       "206.jpg", "207.jpg", "216.jpg", "232.jpg", "240.jpg",
       "241.jpg", "244.jpg", "252.jpg", "264.jpg", "265.jpg", "266.jpg",
       "267.jpg", "272.jpg", "273.jpg", "274.jpg", "275.jpg", "279.jpg",
       "281.jpg", "283.jpg", "288.jpg", "291.jpg",
       "292.jpg", "293.jpg", "297.jpg", "299.jpg",
       "301.jpg", "302.jpg", "303.jpg", "304.jpg",
       "305.jpg", "309.jpg", "310.jpg", "314.jpg",
       "315.jpg", "316.jpg", "317.jpg", "318.jpg",
       "319.jpg", "320.jpg", "321.jpg", "329.jpg",
       "331.jpg", "337.jpg", "338.jpg", "339.jpg",
       "340.jpg", "341.jpg", "342.jpg", "343.jpg",
       "345.jpg", "347.jpg", "348.jpg", "349.jpg",
       "350.jpg", "356.jpg", "359.jpg", "363.jpg",
       "364.jpg", "370.jpg", "371.jpg", "372.jpg",
       "378.jpg", "379.jpg", "394.jpg", "395.jpg",
       "399.jpg", "400.jpg", "405.jpg", "407.jpg",
       "408.jpg", "410.jpg", "411.jpg", "412.jpg", "708.jpg", "723.jpg", "725.jpg",
       "729.jpg", "736.jpg", "742.jpg", "750.jpg",
       "751.jpg", "752.jpg", "767.jpg", "768.jpg",
       "784.jpg"}



        Dim path As String = Server.MapPath("500")
        Dim f As String = String.Empty
        Dim newF As String = String.Empty
        For Each x As String In s
            f = IO.Path.Combine(path, x)
            newF = IO.Path.Combine(path, "copied", x)

            If IO.File.Exists(f) Then
                IO.File.Copy(f, newF)
            Else
                IO.File.Copy(IO.Path.Combine(path, "no.jpg"), newF)
            End If

        Next

    End Sub

End Class
