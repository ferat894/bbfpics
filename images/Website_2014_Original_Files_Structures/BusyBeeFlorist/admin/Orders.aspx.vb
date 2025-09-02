
Partial Class admin_Orders
    Inherits System.Web.UI.Page


    Protected Friend Function RenderOrderRef(OrderID As Integer, OrderDateTime As Date) As String
        Return BBF.WebOrder.GenerateOrderRef(OrderID, OrderDateTime)
    End Function


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            tFrom.Text = Now.Date.AddDays(-7).ToShortDateString
            tTo.Text = Now.Date.ToShortDateString

        End If
    End Sub

    Protected Sub cGetOrders_Click(sender As Object, e As EventArgs) Handles cGetOrders.Click
        Page.Validate()
        If Page.IsValid Then


            r.DataSource = BBF.WebOrder.ReportOnOrders(CDate(tFrom.Text), CDate(tTo.Text))
            r.DataBind()

        End If
    End Sub

    Protected Sub cGetSingleOrder_Click(sender As Object, e As EventArgs) Handles cGetSingleOrder.Click
        Dim ds As Data.DataSet = BBF.WebOrder.SearchWebOrders(mrk.Common.Conversion.ToInteger(tOrderRef.Text, 0), tSenderName.Text.Trim)
        If ds.Tables.Count > 0 Then r.DataSource = ds
        r.DataBind()
    End Sub
End Class
