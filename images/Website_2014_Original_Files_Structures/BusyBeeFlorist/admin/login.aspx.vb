
Partial Class admin_login
    Inherits System.Web.UI.Page

    Protected Sub LoginButton_Click(sender As Object, e As EventArgs) Handles LoginButton.Click
        Page.Validate()
        If Page.IsValid Then

            'Response.Write("<hr />" & FormsAuthentication.HashPasswordForStoringInConfigFile("", "SHA1"))


            If FormsAuthentication.Authenticate(UserName.Text.Trim, Password.Text.Trim) Then
                Call FormsAuthentication.RedirectFromLoginPage(UserName.Text, False)
            Else
                l.Text = Common.Messages.DisplayMessage("danger", "The details you provided are not correct, please try again.")
            End If


        End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then


            If Request.QueryString("off") = "1" Then
                FormsAuthentication.SignOut()
                Response.Redirect("/")
            Else
                If Context.User.Identity.IsAuthenticated Then
                    Response.Redirect("/admin/")
                End If
            End If
        End If
    End Sub

    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        If Request.QueryString("ReturnURL") <> "" Then
            Response.Redirect(Request.ServerVariables("SCRIPT_NAME"))
            Exit Sub
        End If
    End Sub
End Class
