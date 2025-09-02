Imports System.Net.Mail

Partial Class contact
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            NoBot1.GenerateNewCode()

        End If
    End Sub

    Protected Sub cSubmit_Click(sender As Object, e As EventArgs) Handles cSubmit.Click

        Page.Validate()
        If Not Page.IsValid Then
            l.Text = Common.Messages.DisplayMessage("danger", Common.Messages.PAGE_INVALID)
            Exit Sub
        End If



        Dim sb As Text.StringBuilder = New Text.StringBuilder
        If String.IsNullOrEmpty(txtName.Text) Then sb.Append("<li>Name is required.</li>")
        If String.IsNullOrEmpty(txtEmail.Text) Then sb.Append("<li>Email is required.</li>")
        If String.IsNullOrEmpty(txtArea.Text) Then sb.Append("<li>Message is required.</li>")
        If String.IsNullOrEmpty(tCode.Text) Then sb.Append("<li>Verification code is required.</li>")
        If Not String.IsNullOrEmpty(sb.ToString) Then
            l.Text = Common.Messages.DisplayMessage("danger", String.Format("<ul>{0}</ul>", sb.ToString))
            Exit Sub
        End If

        If Not NoBot1.VerifyCode(tCode.Text) Then
            l.Text = Common.Messages.DisplayMessage("danger", "The verification code was not correct, please try again.")
            Exit Sub
        End If

        If Not RegularExpressions.Regex.IsMatch(txtEmail.Text.Trim, Common.EMAIL_FORMAT_EXPRESSION) Then
            l.Text = Common.Messages.DisplayMessage("danger", "The email format is not valid, please try again")
            Exit Sub
        End If


        Call SendEmail()



    End Sub

    Sub SendEmail()

        Dim site As BBF.SiteConfig = New BBF.SiteConfig(WebConfig.SiteID)

        Dim BBFEmail As String = site.ReceiveEmailAfterOrderEmailAddress
        If String.IsNullOrWhiteSpace(BBFEmail) Then BBFEmail = WebConfig.DEFAULT_EMAIL_ADDRESS


        Dim WasError As Boolean = False
        Try
            Dim message As New MailMessage(txtEmail.Text.Trim, BBFEmail, BBF.WEBSITE_NAME & " Website Feedback", txtName.Text & " wrote," & vbNewLine & txtArea.Text)
            Dim emailClient As New SmtpClient(WebConfig.EMAIL_SERVER)
            message.IsBodyHtml = False
            emailClient.Send(message)
        Catch ex As Exception
            Dim x As New Elmah.Error(ex)
            Elmah.ErrorLog.GetDefault(HttpContext.Current).Log(x)
            WasError = True
        End Try

        If WasError Then
            l.Text = Common.Messages.DisplayMessage("danger", "Sorry, there was an error whily trying to send your message. Please try again at a later time.")
        Else
            l.Text = Common.Messages.DisplayMessage("success", "Thankyou, your message was sent successfully.")
            txtName.Text = String.Empty
            txtArea.Text = String.Empty
            txtEmail.Text = String.Empty
            tCode.Text = String.Empty
            Call NoBot1.GenerateNewCode()
        End If

    End Sub
End Class
