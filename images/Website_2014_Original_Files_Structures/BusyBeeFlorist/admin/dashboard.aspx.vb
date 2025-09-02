Imports System.Data
Imports System.Data.SqlClient

Partial Class admin_dashboard
    Inherits BBF.AdminBasePage



    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then




            Dim SQL As String = "SELECT COUNT(*) FROM BBF_Item; " & _
                "SELECT COUNT(*), (SELECT COUNT(*) FROM BBF_WebOrder WHERE OrderDateTime BETWEEN (GetDate() - 7) AND GetDate() ) FROM BBF_WebOrder;" & _
                "SELECT COUNT(*) FROM BBF_OptionalExtra; "

            Dim ds As DataSet = SqlHelper.ExecuteDataset(WebConfig.SQL_CONNECTION, CommandType.Text, SQL)
            FlowersCount.Text = ds.Tables(0).Rows(0).Item(0)

            Dim AllOrdersCount As Double = CDbl(ds.Tables(1).Rows(0).Item(0))
            Dim OrdersWithinWeek As Double = CDbl(ds.Tables(1).Rows(0).Item(1))

            OrdersCount.Text = String.Format("<span class=""badge{1}"">{0}</span>", AllOrdersCount, IIf(OrdersWithinWeek > 0, " green", String.Empty))
            ExtraItemCount.Text = ds.Tables(2).Rows(0).Item(0)


        End If
    End Sub

  
End Class
