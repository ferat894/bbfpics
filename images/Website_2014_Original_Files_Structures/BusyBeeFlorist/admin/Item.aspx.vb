Imports System.Data
Imports System.Data.SqlClient

Partial Class admin_Item
    Inherits BBF.AdminBasePage


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then


            ddlCategory.DataSource = BBF.Category.GetAll
            ddlCategory.DataBind()


            Dim ItemID As Integer = Me.ItemId

            If ItemID > 0 Then
                Call LoadItem(ItemID)
                cSubmit.Text = " Update Item "
            Else
                If Request.QueryString("addnew") = "1" Then
                    cSubmit.Text = " Add New Item "

                    If Me.CategoryID > 0 Then
                        ddlCategory.Items.FindByValue(Me.CategoryID).Selected = True
                    End If
                End If

                End If

        End If


    End Sub




    Private Sub UpdateItem(ItemId As Integer)

        Dim p As Data.SqlClient.SqlParameter() = Me.CollectParameters
        Dim rowsAffected As Integer = SqlHelper.ExecuteNonQuery(WebConfig.SQL_CONNECTION, CommandType.StoredProcedure, "BBF_ItemUpdate", p)

        If p(0).Value = -11 Then
            '//same item already exists in the category you selected
            l.Text = Common.Messages.DisplayMessage("info", "The category you are changing into has an item with the same name.")
        ElseIf p(0).Value = -12 Then
            '//selected an image that is assigned
            l.Text = Common.Messages.DisplayMessage("info", "The item code (flower image) has already been assigned. Please select another flower image.")
        Else
            If rowsAffected > 0 Then
                '// successfully update
                l.Text = Common.Messages.DisplayMessage("success", "Successfully updated this item.")


                '//update to the selected categories

                Dim Categories As New List(Of Integer)
                For Each li As ListItem In ddlCategory.Items
                    If li.Selected AndAlso IsNumeric(li.Value) Then
                        Categories.Add(CInt(li.Value))
                    End If
                Next
                If Categories.Count > 0 Then Call BBF.ItemInCategory.ItemReplaceCategories(ItemId, Categories)

            Else
                l.Text = Common.Messages.DisplayMessage("danger", "The update was not successful.")
            End If
        End If
    End Sub


    Private Sub AddItem()

        Dim p As Data.SqlClient.SqlParameter() = Me.CollectParameters(False)
        Dim rowsAffected As Integer = SqlHelper.ExecuteNonQuery(WebConfig.SQL_CONNECTION, CommandType.StoredProcedure, "BBF_ItemAdd", p)

        If p(0).Value = -11 Then
            '//same item already exists in the category you selected
            l.Text = Common.Messages.DisplayMessage("info", "The is an existing item with the same title and description.")
        ElseIf p(0).Value = -12 Then
            '//same item already exists in the category you selected
            l.Text = Common.Messages.DisplayMessage("info", "The item code (flower image) has already been assigned. lease select another flower image.")
        Else
            If rowsAffected > 0 Then
                '// successfully added
                l.Text = Common.Messages.DisplayMessage("success", "Successfully added this item.")

                '//add to the selected categories
                Dim NewID As Integer = p(0).Value
                If NewID > 0 Then
                    For Each li As ListItem In ddlCategory.Items
                        If li.Selected AndAlso IsNumeric(li.Value) Then
                            BBF.ItemInCategory.AddItemAndCategory(NewID, CInt(li.Value))
                        End If
                    Next
                End If

            Else
                l.Text = Common.Messages.DisplayMessage("danger", "The process was not successful.")
            End If
        End If
    End Sub


    Private Function CollectParameters(Optional ForUpdate As Boolean = True) As SqlParameter()
        Dim ItemID As Integer = Me.ItemId


        Dim p(9) As SqlParameter
        p(0) = New SqlParameter("@rtn", SqlDbType.Int) : p(0).Direction = ParameterDirection.ReturnValue
        'p(1) = New SqlParameter("@CategoryId", ddlCategory.SelectedValue)
        If ForUpdate Then p(2) = New SqlParameter("@ItemID", ItemID)
        p(3) = New SqlParameter("@ItemName", Server.HtmlEncode(tName.Text.Trim))
        p(4) = New SqlParameter("@ItemDescription", Server.HtmlEncode(tDescription.Text.Trim))
        p(5) = New SqlParameter("@ItemPrice1", CDec(tPrice1.Text))
        p(6) = New SqlParameter("@ItemPrice2", 0)
        If IsNumeric(tPrice2.Text) Then p(6).Value = CDec(tPrice2.Text)
        p(7) = New SqlParameter("@ItemPriceTitle1", Server.HtmlEncode(tPriceTitle1.Text.Trim))
        p(8) = New SqlParameter("@ItemPriceTitle2", Server.HtmlEncode(tPriceTitle2.Text.Trim))
        p(9) = New SqlParameter("@ItemImageName", tImageName.Text.Trim)

        Return p

    End Function


    Private Sub LoadItem(ItemId As Integer)


        Dim ds As DataSet = BBF.Item.GetItem(ItemId)
        Dim Categories As New List(Of Integer)
        If ds.Tables(0).Rows.Count > 0 Then




            Dim CategoryName As String = String.Empty
            With ds.Tables(0).Rows(0)
                tName.Text = .Item("ItemName")
                tDescription.Text = .Item("ItemDescription")
                ddlCategory.Items.FindByValue(.Item("CategoryId").ToString).Selected = True
                tPriceTitle1.Text = .Item("ItemPriceTitle1").ToString
                tPrice1.Text = CDec(.Item("ItemPrice1")).ToString(Common.MONEY_FORMAT)
                tPriceTitle2.Text = .Item("ItemPriceTitle2").ToString()
                tPrice2.Text = CDec(.Item("ItemPrice2")).ToString(Common.MONEY_FORMAT)
                tImageName.Text = .Item("ItemImageName").ToString
                CategoryName = .Item("CategoryName").ToString

                imgLarge.ImageUrl = BBF.Category.GetCategoryFlowerFolder(CategoryName) & tImageName.Text.Trim
                imgThumb.ImageUrl = BBF.Category.GetCategoryFlowerFolder(CategoryName) & "tn/" & tImageName.Text.Trim

                If CBool(.Item("ItemDisabled")) = True Then
                    cEnableItem.Visible = True
                    cSubmit.Enabled = False
                    panDisabled.Visible = True
                Else
                    cDisableItem.Visible = True
                End If
            End With



            '//load the categories
            For Each dr As Data.DataRow In ds.Tables(0).Rows
                ddlCategory.Items.FindByValue(dr("CategoryId")).Selected = True
            Next





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
        BBF.Item.SetItemDisabled(Me.ItemId, False)
        Response.Redirect(Common.URL.CurrentURL)
    End Sub

    Protected Sub cDisableItem_Click(sender As Object, e As EventArgs) Handles cDisableItem.Click
        BBF.Item.SetItemDisabled(Me.ItemId, True)
        Response.Redirect(Common.URL.CurrentURL)
    End Sub
End Class
