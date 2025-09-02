
Partial Class admin_ItemList
    Inherits BBF.AdminBasePage


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then


            panelExtras.Visible = False
            panelFlowers.Visible = False
            If Request.QueryString("section") = "extras" Then
                ddlExtras.DataSource = BBF.OptionalExtras.GetAllForAdmin
                ddlExtras.DataBind()
                ddlExtras.Items.Insert(0, New ListItem(" -- PLEASE SELECT -- ", 0))
                ddlExtras.Items.Add(New ListItem(" -- VIEW ALL -- ", -1))

                panelExtras.Visible = True
            Else

                For Each r As Data.DataRow In BBF.Category.GetAllWithItemCount.Tables(0).Rows
                    ddlCategory.Items.Add(New ListItem(r("CategoryName") & " (" & r("ItemCount") & ")", r("CategoryId")))
                Next
                ddlCategory.Items.Insert(0, New ListItem(" -- PLEASE SELECT -- ", 0))
                ddlCategory.Items.Add(New ListItem(" ALL FLOWERS ", -1))

                panelFlowers.Visible = True
            End If


        End If
    End Sub

    Protected Friend Function RenderThumbNail(CateImageFileName As String, CategoryName As String) As String
        Return BBF.Category.GetCategoryFlowerFolder(CategoryName) & "tn/" & CateImageFileName
    End Function

    Protected Friend Function RenderOffDisabled(ItemDisabled As Boolean) As String
        If ItemDisabled Then
            Return "&nbsp;<span class=""item-disabled"">(Disabled)</span>"
        Else
            Return String.Empty
        End If
    End Function

    Protected Sub ddlCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCategory.SelectedIndexChanged
        If ddlCategory.SelectedValue > 0 Then
            r.DataSource = BBF.Item.GetItemsForCategory(ddlCategory.SelectedValue)
        Else
            If ddlCategory.SelectedValue = "-1" Then r.DataSource = BBF.Item.GetAllItems()
        End If

        r.DataBind()

    End Sub


    Protected Sub ddlExtras_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlExtras.SelectedIndexChanged
        If CInt(ddlExtras.SelectedItem.Value) > 0 Then
            r1.DataSource = BBF.OptionalExtras.GetItem(ddlExtras.SelectedItem.Value)
        ElseIf ddlExtras.SelectedItem.Value = "-1" Then
            r1.DataSource = BBF.OptionalExtras.GetAllForAdmin
        End If

        r1.DataBind()



    End Sub



End Class
