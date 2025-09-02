Imports System.Data
Imports System.Data.SqlClient


Partial Class admin_OptionalItem
    Inherits BBF.AdminBasePage


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then


            Dim ItemID As Integer = Me.ItemId

            If ItemID > 0 Then
                Call LoadItem(ItemID)
                cSubmit.Text = " Update Item "
            Else
                If Request.QueryString("addnew") = "1" Then
                    cSubmit.Text = " Add New Item "

                End If

            End If

        End If


    End Sub




    Private Sub UpdateItem(ItemId As Integer)

        Dim p As Data.SqlClient.SqlParameter() = Me.CollectParameters
        Dim rowsAffected As Integer = SqlHelper.ExecuteNonQuery(WebConfig.SQL_CONNECTION, CommandType.StoredProcedure, "BBF_OptionalExtra_Update", p)

        If p(0).Value = 11 Then
            '//same item already exists in the category you selected
            l.Text = Common.Messages.DisplayMessage("info", "There is already an optional item with the same name.")
        Else
            If rowsAffected > 0 Then
                '// successfully update
                l.Text = Common.Messages.DisplayMessage("success", "Successfully updated this item.")
            Else
                l.Text = Common.Messages.DisplayMessage("danger", "The update was not successful.")
            End If
        End If
    End Sub


    Private Sub AddItem()

        Dim p As Data.SqlClient.SqlParameter() = Me.CollectParameters
        Dim rowsAffected As Integer = SqlHelper.ExecuteNonQuery(WebConfig.SQL_CONNECTION, CommandType.StoredProcedure, "BBF_OptionalExtra_ItemAdd", p)

        If p(0).Value = 11 Then
            '//same item already exists in the category you selected
            l.Text = Common.Messages.DisplayMessage("info", "The optional item already exists.")
        Else
            If rowsAffected > 0 Then
                '// successfully update
                l.Text = Common.Messages.DisplayMessage("success", "Successfully added this item.")
            Else
                l.Text = Common.Messages.DisplayMessage("danger", "The process was not successful.")
            End If
        End If
    End Sub


    Private Function CollectParameters() As SqlParameter()
        Dim ItemID As Integer = Me.ItemId


        Dim p(9) As SqlParameter
        p(0) = New SqlParameter("@rtn", SqlDbType.Int) : p(0).Direction = ParameterDirection.ReturnValue
        p(1) = New SqlParameter("@OptionalExtraId", ItemID)
        p(2) = New SqlParameter("@Title", Server.HtmlEncode(tName.Text.Trim))
        p(3) = New SqlParameter("@Description", Server.HtmlEncode(tDescription.Text.Trim))
        p(4) = New SqlParameter("@Price1", CDec(tPrice1.Text))
        p(5) = New SqlParameter("@Price2", IIf(IsNumeric(tPrice2.Text), CDec(tPrice2.Text), 0))
        p(6) = New SqlParameter("@PriceTitle1", Server.HtmlEncode(tPriceTitle1.Text.Trim))
        p(7) = New SqlParameter("@PriceTitle2", Server.HtmlEncode(tPriceTitle2.Text.Trim))
        p(8) = New SqlParameter("@ImageName", tImageName.Text.Trim)
        p(9) = New SqlParameter("@AllowMultiple", ckAllowMultiple.Checked)

        Return p

    End Function


    Private Sub LoadItem(ItemId As Integer)


        Dim ds As DataSet = BBF.OptionalExtras.GetItem(ItemId)
        If ds.Tables(0).Rows.Count > 0 Then




            With ds.Tables(0).Rows(0)
                tName.Text = .Item("Title")
                tDescription.Text = .Item("Description")
                ckAllowMultiple.Checked = CBool(.Item("AllowMultiple"))
                tPriceTitle1.Text = .Item("PriceTitle1").ToString
                tPrice1.Text = CDec(.Item("Price1")).ToString(Common.MONEY_FORMAT)
                tPriceTitle2.Text = .Item("PriceTitle2").ToString()
                tPrice2.Text = CDec(.Item("Price2")).ToString(Common.MONEY_FORMAT)
                tImageName.Text = .Item("ImageName").ToString


                If CBool(.Item("Disabled")) = True Then
                    cEnableItem.Visible = True
                    cSubmit.Enabled = False
                    panDisabled.Visible = True
                Else
                    cDisableItem.Visible = True
                End If

            End With


            imgLarge.ImageUrl = String.Format("/images/flowers/extras/{0}", tImageName.Text.Trim)

        End If



    End Sub


    Protected Sub cSubmit_Click(sender As Object, e As EventArgs) Handles cSubmit.Click
        If Me.ItemId > 0 Then
            Call UpdateItem(Me.ItemId)
        Else
            If Request.QueryString("addnew") = "1" Then
                Call AddItem()
            End If
        End If

    End Sub

    Protected Sub cEnableItem_Click(sender As Object, e As EventArgs) Handles cEnableItem.Click
        BBF.OptionalExtras.SetItemDisabled(Me.ItemId, False)
        Response.Redirect(Common.URL.CurrentURL)
    End Sub

    Protected Sub cDisableItem_Click(sender As Object, e As EventArgs) Handles cDisableItem.Click
        BBF.OptionalExtras.SetItemDisabled(Me.ItemId, True)
        Response.Redirect(Common.URL.CurrentURL)

    End Sub

End Class

