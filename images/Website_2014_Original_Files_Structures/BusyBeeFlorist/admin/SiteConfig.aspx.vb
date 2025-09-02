
Partial Class admin_SiteConfig
    Inherits System.Web.UI.Page



    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then


            Dim Config As BBF.SiteConfig = New BBF.SiteConfig(WebConfig.SiteID)

            chkOrderSendEmail.Checked = Config.ReceiveEmailAfterOrder
            chkOrderValidation.Checked = Config.ValidateOrderUsingHash
            tEmail.Text = Config.ReceiveEmailAfterOrderEmailAddress
            chkCCValidation.Checked = Config.ValidateCreditCardDetails


        End If
    End Sub

    Protected Sub cUpdateConfig_Click(sender As Object, e As EventArgs) Handles cUpdateConfig.Click
        Page.Validate()
        If Page.IsValid Then
            If BBF.SiteConfig.UpdateConfig(WebConfig.SiteID, chkOrderValidation.Checked, _
                         chkOrderSendEmail.Checked, tEmail.Text.Trim, _
                         chkCCValidation.Checked) Then

                l.Text = Common.Messages.DisplayMessage("success", "Site configuration updated successfully.")
            Else
                l.Text = Common.Messages.DisplayMessage("danger", "Update failed.")
            End If
        Else
            l.Text = Common.Messages.DisplayMessage("warning", Common.Messages.PAGE_INVALID)
        End If

    End Sub
End Class
