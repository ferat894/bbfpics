Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class BBF

    Public Const WEBSITE_NAME As String = "Busy Bee Florist"

    Public MustInherit Class AdminBasePage
        Inherits System.Web.UI.Page


        ''' <summary>
        ''' locates the querystring for the url
        ''' </summary>
        ''' <param name="keyName"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function LocateQueryStringInURL(keyName As String) As String
            Dim x As String = String.Empty


            If Not IsNothing(Page.RouteData.RouteHandler) Then
                If TypeOf Page.RouteData.RouteHandler Is System.Web.Routing.PageRouteHandler Then
                    Dim vp As String = CType(Page.RouteData.RouteHandler, System.Web.Routing.PageRouteHandler).VirtualPath
                    Dim pos As Integer = vp.LastIndexOf("?")
                    If pos > 0 Then
                        vp = vp.Substring(pos).Replace("?", String.Empty)



                        Dim keys As New Dictionary(Of String, String)

                        If vp.Contains("&") Then
                            For Each s As String In vp.Split("&")
                                If s.Contains("=") Then
                                    Dim t As String() = s.Split("=")
                                    keys.Add(t(0), t(1))
                                End If
                            Next
                        Else
                            If vp.Contains("=") Then
                                Dim t As String() = vp.Split("=")
                                keys.Add(t(0), t(1))
                            End If
                        End If


                        If keys.ContainsKey(keyName) Then x = keys(keyName)

                    End If

                End If
            End If



            Return x
        End Function


        ''' <summary>
        ''' gets the catid from the url
        ''' </summary>
        ''' <param name="TryFromRouteDataFirst">When True, will try to get it from the Page.RouteData first. (for pages that have MapPageRoute)</param>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property CategoryID(Optional TryFromRouteDataFirst As Boolean = False) As Integer
            Get
                Dim ID As Integer = 0

                If TryFromRouteDataFirst Then
                    Dim res As String = LocateQueryStringInURL("catid")
                    If Not String.IsNullOrEmpty(res) AndAlso IsNumeric(res) Then ID = CInt(res)
                End If


                If ID = 0 AndAlso IsNumeric(Request.QueryString("catid")) Then
                    ID = CInt(Request.QueryString("catid"))
                End If

                Return ID
            End Get
        End Property


        ''' <summary>
        ''' gets the id from the url
        ''' </summary>
        ''' <param name="TryFromRouteDataFirst">When True, will try to get it from the Page.RouteData first. (for pages that have MapPageRoute)</param>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ItemId(Optional TryFromRouteDataFirst As Boolean = False) As Integer
            Get
                Dim ID As Integer = 0

                If TryFromRouteDataFirst Then
                    If Page.RouteData.Values.ContainsKey("ItemId") Then
                        Dim x As String = Page.RouteData.Values("ItemId").ToString
                        If Not String.IsNullOrEmpty(x) AndAlso IsNumeric(x) Then ID = CInt(x)
                    End If
                End If


                If ID = 0 AndAlso IsNumeric(Request.QueryString("id")) Then
                    ID = CInt(Request.QueryString("id"))
                End If

                Return ID
            End Get
        End Property

    End Class


    Public Class Category

        Public Shared Function GetCategoryName(CategoryID As Integer) As String
            If Not CategoryID > 0 Then Return String.Empty
            Dim ds As DataSet = SqlHelper.ExecuteDataset(WebConfig.SQL_CONNECTION, CommandType.Text, "SELECT BBF_CategoryName FROM Category WHERE CategoryID=@CategoryID;", New SqlParameter("@CategoryID", CategoryID))

            If ds.Tables(0).Rows.Count > 0 Then
                Return ds.Tables(0).Rows(0).Item("CategoryName").ToString
            Else
                Return String.Empty
            End If
        End Function

        ''' <summary>
        ''' returns the relative URL of the folder ie: /images/flowers/romance/
        ''' </summary>
        ''' <param name="CategoryID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetCategoryFlowerFolder(CategoryID As Integer) As String
            'Dim cn As String = GetCategoryName(CategoryID)
            'If Not String.IsNullOrWhiteSpace(cn) Then Return String.Format("/images/flowers/{0}/", cn.ToString.Replace(" ", String.Empty))
            'Return String.Empty
            Return "/images/flowers/"
        End Function

        Public Shared Function GetCategoryFlowerFolder(CategoryName As String) As String
            'If Not String.IsNullOrWhiteSpace(CategoryName) Then Return String.Format("/images/flowers/{0}/", CategoryName.ToString.Replace(" ", String.Empty))
            'Return String.Empty
            Return "/images/flowers/"
        End Function



        Public Shared Function GetAll() As DataSet
            Return SqlHelper.ExecuteDataset(WebConfig.SQL_CONNECTION, CommandType.Text, "SELECT * FROM BBF_Category ORDER BY CategoryName;")
        End Function

        Public Shared Function GetAllWithItemCount() As DataSet
            Return SqlHelper.ExecuteDataset(WebConfig.SQL_CONNECTION, CommandType.Text, "SELECT C.*, (SELECT COUNT(I.ItemId) FROM BBF_ItemInCategory I WHERE I.CategoryId = C.CategoryId) ItemCount  FROM BBF_Category C ORDER BY C.CategoryName;")
        End Function

        '

    End Class


    Public Class Item

        ''' <summary>
        ''' sets the enabled/disabled
        ''' </summary>
        ''' <param name="ItemId"></param>
        ''' <param name="ItemDisabled"></param>
        ''' <remarks></remarks>
        Public Shared Sub SetItemDisabled(ItemId As Integer, ItemDisabled As Boolean)
            Call SqlHelper.ExecuteNonQuery(WebConfig.SQL_CONNECTION, CommandType.Text, "UPDATE BBF_Flowers SET ItemDisabled = @ItemDisabled  WHERE ItemId=@ItemId;", New SqlParameter("@ItemId", ItemId), New SqlParameter("@ItemDisabled", ItemDisabled))
        End Sub


        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="ItemId"></param>
        ''' <param name="ShowAllItems">When False, will not show items that are disabled</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetItem(ItemId As Integer, Optional ShowAllItems As Boolean = True) As DataSet
            Dim SQL As String = "SELECT * FROM BBF_Flowers WHERE ItemId=@ItemId;"
            If ShowAllItems = False Then SQL = "SELECT * FROM BBF_Flowers WHERE ItemId=@ItemId AND ItemDisabled=0;"
            Return SqlHelper.ExecuteDataset(WebConfig.SQL_CONNECTION, CommandType.Text, SQL, New SqlParameter("@ItemId", ItemId))
        End Function



        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="ItemId"></param>
        ''' <param name="SiteID"></param>
        ''' <param name="ShowAllItems">When False, will not show items that are disabled</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetItem(ItemId As Integer, SiteID As Integer, Optional ShowAllItems As Boolean = True) As DataSet
            Dim SQL As String = "SELECT *, (SELECT FlatDeliveryFee FROM BBF_SiteConfig WHERE SiteID = @SiteID) FlatDeliveryFee FROM BBF_Flowers WHERE ItemId=@ItemId;"
            If ShowAllItems = False Then SQL = "SELECT *, (SELECT FlatDeliveryFee FROM BBF_SiteConfig WHERE SiteID = @SiteID) FlatDeliveryFee FROM BBF_Flowers WHERE ItemDisabled=0 AND ItemId=@ItemId;"
            Return SqlHelper.ExecuteDataset(WebConfig.SQL_CONNECTION, CommandType.Text, SQL, New SqlParameter("@ItemId", ItemId), New SqlParameter("@SiteID", SiteID))
        End Function


        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="CategoryID"></param>
        ''' <param name="ShowAllItems">When False, will not show items that are disabled</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetItemsForCategory(CategoryID As Integer, Optional ShowAllItems As Boolean = True) As DataTable

            Dim SQL As String = "SELECT * FROM BBF_Flowers WHERE CategoryID=@CategoryID ORDER BY ItemName;"
            If ShowAllItems = False Then SQL = "SELECT * FROM BBF_Flowers WHERE CategoryID=@CategoryID AND ItemDisabled=0 ORDER BY ItemName;"



            Dim ds As DataSet = SqlHelper.ExecuteDataset(WebConfig.SQL_CONNECTION, CommandType.Text, SQL, New SqlParameter("@CategoryID", CategoryID))

            If ds.Tables.Count > 0 Then
                Dim dt As DataTable = ds.Tables(0).Clone
                Dim dtWreaths As DataTable = ds.Tables(0).Clone

                '//client wants all the wreaths in category 7 to display last
                If CategoryID = 7 Then
                    For Each dr As Data.DataRow In ds.Tables(0).Rows
                        If dr("ItemName").ToString.ToLower.Contains("wreath") Or dr("ItemDescription").ToString.ToLower.Contains("wreath") Then
                            dtWreaths.ImportRow(dr)
                        Else
                            dt.ImportRow(dr)
                        End If
                    Next
                    For Each dr As Data.DataRow In dtWreaths.Rows
                        dt.ImportRow(dr)
                    Next

                    Return dt
                Else
                    '//not category 7 so return all
                    Return ds.Tables(0)
                End If

            Else
                Return Nothing
            End If


        End Function


        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="ShowAllItems">When False, will not show items that are disabled</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetAllItems(Optional ShowAllItems As Boolean = True) As DataSet
            Dim SQL As String = "SELECT *, '' CategoryName FROM BBF_Item ORDER BY ItemName;"
            If ShowAllItems = False Then SQL = "SELECT *, '' CategoryName FROM BBF_Item WHERE ItemDisabled = 0 ORDER BY ItemName;"
            Return SqlHelper.ExecuteDataset(WebConfig.SQL_CONNECTION, CommandType.Text, SQL)
        End Function


    End Class


    Public Class ItemInCategory


        ''' <summary>
        ''' Replaces the categories for a given item
        ''' </summary>
        ''' <param name="ItemID"></param>
        ''' <param name="Categories"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ItemReplaceCategories(ItemID As Integer, Categories As List(Of Integer)) As Integer

            Dim rowsAffected As Integer = 0
            Dim SQL As String = String.Empty
            Dim conn As SqlClient.SqlConnection = New SqlClient.SqlConnection(WebConfig.SQL_CONNECTION)
            Dim trans As SqlClient.SqlTransaction

            conn.Open()
            trans = conn.BeginTransaction

            Try
                Dim cmd As SqlClient.SqlCommand = Nothing


                Dim sb As Text.StringBuilder = New Text.StringBuilder


                sb.AppendLine("IF EXISTS (SELECT * FROM BBF_Category WHERE CategoryId IN (@Categories)) AND EXISTS (SELECT * FROM BBF_Item WHERE ItemID = @ItemId)")
                sb.AppendLine("BEGIN ")
                sb.AppendLine("     DELETE BBF_ItemInCategory WHERE ItemId=@ItemId; ")
                sb.AppendLine("     INSERT INTO BBF_ItemInCategory  (ItemId, CategoryId) ")
                sb.AppendLine("         SELECT @ItemId, C.CategoryId FROM BBF_Category C WHERE C.CategoryId IN (@Categories)")
                sb.AppendLine("END ")

                SQL = sb.ToString
                SQL = SQL.Replace("@ItemId", ItemID)
                SQL = SQL.Replace("@Categories", String.Join(",", Categories.ToArray).Trim(","))

                cmd = New SqlCommand(SQL, conn, trans)
                rowsAffected = cmd.ExecuteNonQuery()


                trans.Commit()
            Catch ex As Exception
                trans.Rollback()
                rowsAffected = 0

            Finally
                conn.Close()
            End Try


            Return rowsAffected


        End Function


        ''' <summary>
        ''' Replaces the items for a given category
        ''' </summary>
        ''' <param name="CategoryID"></param>
        ''' <param name="Items"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function CategoryReplaceItems(CategoryID As Integer, Items As List(Of Integer)) As Integer

            Dim rowsAffected As Integer = 0
            Dim SQL As String = String.Empty
            Dim conn As SqlClient.SqlConnection = New SqlClient.SqlConnection(WebConfig.SQL_CONNECTION)
            Dim trans As SqlClient.SqlTransaction

            conn.Open()
            trans = conn.BeginTransaction

            Try
                Dim cmd As SqlClient.SqlCommand = Nothing


                Dim sb As Text.StringBuilder = New Text.StringBuilder


                sb.AppendLine("IF EXISTS (SELECT * FROM BBF_Category WHERE CategoryId = @CategoryId) AND EXISTS (SELECT * FROM BBF_Item WHERE ItemID IN (@Items))")
                sb.AppendLine("BEGIN ")
                sb.AppendLine("     DELETE BBF_ItemInCategory WHERE CategoryId=@CategoryId; ")
                sb.AppendLine("     INSERT INTO BBF_ItemInCategory  (CategoryId,ItemId) ")
                sb.AppendLine("         SELECT @CategoryId, I.ItemId FROM BBF_Item I WHERE I.ItemId IN (@Items)")
                sb.AppendLine("END ")

                SQL = sb.ToString
                SQL = SQL.Replace("@CategoryId", CategoryID)
                SQL = SQL.Replace("@Items", String.Join(",", Items.ToArray).Trim(","))

                cmd = New SqlCommand(SQL, conn, trans)
                rowsAffected = cmd.ExecuteNonQuery()


                trans.Commit()
            Catch ex As Exception
                trans.Rollback()
                rowsAffected = 0

            Finally
                conn.Close()
            End Try


            Return rowsAffected


        End Function


        ''' <summary>
        ''' Removes the category and item
        ''' </summary>
        ''' <param name="ItemId"></param>
        ''' <param name="CategoryID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RemoveItemAndCategory(ItemId As Integer, CategoryID As Integer) As Integer

            Dim SQL As String = "IF EXISTS (SELECT * FROM BBF_ItemInCategory WHERE ItemID=@ItemID AND CategoryId=@CategoryId) DELETE BBF_ItemInCategory WHERE ItemID=@ItemID AND CategoryId=@CategoryId"

            Dim p(2) As Data.SqlClient.SqlParameter
            p(0) = New Data.SqlClient.SqlParameter("@ItemID", ItemId)
            p(1) = New Data.SqlClient.SqlParameter("@CategoryId", CategoryID)

            Return SqlHelper.ExecuteNonQuery(WebConfig.SQL_CONNECTION, CommandType.Text, SQL, p)
        End Function


        ''' <summary>
        ''' adds the item and category to the table
        ''' </summary>
        ''' <param name="ItemId"></param>
        ''' <param name="CategoryID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function AddItemAndCategory(ItemId As Integer, CategoryID As Integer) As Integer

            Dim sb As Text.StringBuilder = New Text.StringBuilder
            sb.Append("IF EXISTS (SELECT * FROM BBF_Category WHERE CategoryId = @CategoryID) ")
            sb.Append("AND EXISTS (SELECT * FROM BBF_Item WHERE ItemID = @ItemId) ")
            sb.Append("AND NOT EXISTS (SELECT * FROM BBF_ItemInCategory WHERE CategoryID=@CategoryID AND ItemID=@ItemID) ")
            sb.Append(" INSERT INTO BBF_ItemInCategory (CategoryID, ItemID) VALUES (@CategoryID, @ItemID)")



            Dim p(2) As Data.SqlClient.SqlParameter
            p(0) = New Data.SqlClient.SqlParameter("@ItemID", ItemId)
            p(1) = New Data.SqlClient.SqlParameter("@CategoryId", CategoryID)

            Return SqlHelper.ExecuteNonQuery(WebConfig.SQL_CONNECTION, CommandType.Text, sb.ToString, p)

        End Function


    End Class


    Public Class OptionalExtras

        Property OptionaExtraId As Integer
        Property Title As String
        Property Price As Decimal = 0
        Property PriceTitle As String
        Property Quantity As Integer = 0

        Public Sub New()

        End Sub

        Public Sub New(OptionalID As Integer, Title As String, price As Decimal, priceTitle As String, Quantity As Integer)
            With Me
                .OptionaExtraId = OptionalID
                .Title = Title
                .Price = price
                .PriceTitle = priceTitle
                .Quantity = Quantity
            End With

        End Sub


        ''' <summary>
        ''' sets the enabled/disabled
        ''' </summary>
        ''' <param name="ExtraId"></param>
        ''' <param name="ItemDisabled"></param>
        ''' <remarks></remarks>
        Public Shared Sub SetItemDisabled(ExtraId As Integer, ItemDisabled As Boolean)
            Call SqlHelper.ExecuteNonQuery(WebConfig.SQL_CONNECTION, CommandType.Text, "UPDATE BBF_OptionalExtra SET Disabled = @ItemDisabled  WHERE OptionalExtraId=@ExtraId;", New SqlParameter("@ExtraId", ExtraId), New SqlParameter("@ItemDisabled", ItemDisabled))
        End Sub


        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="ShowAllItems">When False, will not show items that are disabled</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetAll(Optional ShowAllItems As Boolean = True) As Data.DataSet
            Dim SQL As String = "SELECT * FROM BBF_OptionalExtra;"
            If ShowAllItems = False Then SQL = "SELECT * FROM BBF_OptionalExtra WHERE Disabled=0;"
            Return SqlHelper.ExecuteDataset(WebConfig.SQL_CONNECTION, CommandType.Text, SQL)

        End Function

        Public Shared Function GetAllForAdmin() As Data.DataSet

            Return SqlHelper.ExecuteDataset(WebConfig.SQL_CONNECTION, CommandType.Text, "SELECT * FROM BBF_OptionalExtra ORDER BY Title;")

        End Function


        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="OptionalExtraId"></param>
        ''' <param name="ShowAllItems">When False, will not show items that are disabled</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetItem(OptionalExtraId As Integer, Optional ShowAllItems As Boolean = True) As DataSet
            Dim SQL As String = "SELECT * FROM BBF_OptionalExtra WHERE OptionalExtraId=@OptionalExtraId;"
            If ShowAllItems = False Then SQL = "SELECT * FROM BBF_OptionalExtra WHERE OptionalExtraId=@OptionalExtraId AND Disabled=0;"
            Return SqlHelper.ExecuteDataset(WebConfig.SQL_CONNECTION, CommandType.Text, Sql, New SqlParameter("@OptionalExtraId", OptionalExtraId))

        End Function


    End Class



    Public Class WebOrder


        ''' <summary>
        ''' Updates the EmailSentForOrder status for an order
        ''' </summary>
        ''' <param name="OrderID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function UpdateOrderEmailSent(OrderID As Integer) As Boolean
            Return SqlHelper.ExecuteNonQuery(WebConfig.SQL_CONNECTION, CommandType.Text, "UPDATE BBF_WebOrder SET EmailSentForOrder=1 WHERE OrderID=@OrderID;", New SqlParameter("@OrderID", OrderID)) > 0
        End Function

        ''' <summary>
        ''' returns the new order id
        ''' </summary>
        ''' <param name="UserIP"></param>
        ''' <param name="OrderHtml"></param>
        ''' <param name="OrderSignature">SHA256 of the html string</param>
        ''' <param name="TimeOut">In mintues, the time to wait for allow the same submission</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function AddOrder(UserIP As String, OrderHtml As String, OrderSignature As String, TimeOut As Integer, ItemID As Integer, ItemTitle As String, ItemPrice As Decimal, ItemPriceTitle As String, _
                                        Sender As BBF.WebOrder.SenderDetails, Recipient As BBF.WebOrder.RecipientDetails, _
                                        ExtraItemsList As List(Of OptionalExtras), ShippingCostTotal As Decimal) As Integer


            Dim p(27) As Data.SqlClient.SqlParameter

            p(0) = New SqlParameter("@UserIP", UserIP)
            p(1) = New SqlParameter("@OrderHtml", OrderHtml)
            p(2) = New SqlParameter("@OrderSignature", OrderSignature)
            p(3) = New SqlParameter("@TimeOut", TimeOut)
            p(4) = New SqlParameter("@ItemID", ItemID)
            p(5) = New SqlParameter("@ItemTitle", ItemTitle)
            p(6) = New SqlParameter("@ItemPrice", ItemPrice)
            p(7) = New SqlParameter("@ItemPriceTitle", ItemPriceTitle)
            p(8) = New SqlParameter("@SenderName", Sender.Name)
            p(9) = New SqlParameter("@SenderAddress", Sender.Address)
            p(10) = New SqlParameter("@SenderSuburb", Sender.Suburb)
            p(11) = New SqlParameter("@SenderPostcode", Sender.Postcode)
            p(12) = New SqlParameter("@SenderState", Sender.State)
            p(13) = New SqlParameter("@SenderNotesToRecipient", String.Empty)
            p(14) = New SqlParameter("@DeliveryNotes", Sender.DeliveryInstructions)
            p(15) = New SqlParameter("@DeliveryPreferredTime", Sender.DeliveryPreferredTime)
            p(16) = New SqlParameter("@RecipientCompanyName", Recipient.CompanyName)
            p(17) = New SqlParameter("@RecipientName", Recipient.Name)
            p(18) = New SqlParameter("@RecipientAddress", Recipient.Address)
            p(19) = New SqlParameter("@RecipientSuburb", Recipient.Suburb)
            p(20) = New SqlParameter("@RecipientPostcode", Recipient.Postcode)
            p(21) = New SqlParameter("@RecipientState", Recipient.State)
            p(22) = New Data.SqlClient.SqlParameter("@rtn", SqlDbType.Int) : p(22).Direction = ParameterDirection.ReturnValue
            p(23) = New SqlParameter("@RecipientEmail", Recipient.Email)
            p(24) = New SqlParameter("@RecipientContact", Recipient.ContactNumber)
            p(25) = New SqlParameter("@SenderEmail", Sender.Email)
            p(26) = New SqlParameter("@SenderContact", Sender.ContactNumber)
            p(27) = New SqlParameter("@ShippingTotalCost", ShippingCostTotal)


            If SqlHelper.ExecuteNonQuery(WebConfig.SQL_CONNECTION, CommandType.StoredProcedure, "BBF_OrderAdd", p) > 0 Then


                Dim WebOrderID As Integer = p(22).Value
                If WebOrderID > 0 Then
                    For Each x As OptionalExtras In ExtraItemsList
                        Dim px(5) As Data.SqlClient.SqlParameter
                        px(0) = New SqlParameter("@WebOrderID", WebOrderID)
                        px(1) = New SqlParameter("@OptionalExtraId", x.OptionaExtraId)
                        px(2) = New SqlParameter("@Title", x.Title)
                        px(3) = New SqlParameter("@Price", x.Price)
                        px(4) = New SqlParameter("@PriceTitle", x.PriceTitle)
                        px(5) = New SqlParameter("@Quantity", x.Quantity)
                        Call SqlHelper.ExecuteNonQuery(WebConfig.SQL_CONNECTION, CommandType.StoredProcedure, "BBF_OrderExtraItemAdd", px)
                    Next
                End If



                Return WebOrderID
            Else
                If p(22).Value = -11 Then
                    Return -11
                Else
                    Return -500
                End If

            End If

        End Function


        Public Shared Function ReportOnOrders(datefrom As Date, dateto As Date) As Data.DataSet

            Dim p(2) As Data.SqlClient.SqlParameter
            p(0) = New Data.SqlClient.SqlParameter("@datefrom", datefrom)
            p(1) = New Data.SqlClient.SqlParameter("@dateto", dateto)

            Return SqlHelper.ExecuteDataset(WebConfig.SQL_CONNECTION, CommandType.Text, "SELECT * FROM BBF_WebOrder WHERE OrderDateTime BETWEEN (@datefrom -1) and (@dateto + 1) ORDER BY OrderDateTime DESC", p)

        End Function


        ''' <summary>
        ''' search the weborders
        ''' </summary>
        ''' <param name="OrderId">Put zero to search by SenderName</param>
        ''' <param name="SenderName">The send like name</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function SearchWebOrders(OrderId As Integer, Optional SenderName As String = Nothing) As DataSet
            Dim p(2) As Data.SqlClient.SqlParameter
            p(0) = New Data.SqlClient.SqlParameter("@OrderId", OrderId)
            p(1) = New Data.SqlClient.SqlParameter("@SenderName", SenderName)

            Return SqlHelper.ExecuteDataset(WebConfig.SQL_CONNECTION, CommandType.StoredProcedure, "BBF_SearchWebOrders", p)


        End Function

        ''' <summary>
        ''' gets the order from the database
        ''' </summary>
        ''' <param name="OrderID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetOrderDetails(OrderID As Integer) As DataSet
            Return SqlHelper.ExecuteDataset(WebConfig.SQL_CONNECTION, CommandType.Text, "SELECT * FROM BBF_WebOrder WHERE OrderId=@OrderId", New SqlParameter("@OrderId", OrderId))
        End Function

        ''' <summary>
        ''' renders the order html
        ''' </summary>
        ''' <param name="orderHtml"></param>
        ''' <param name="OrderDateTime"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RenderOrderDetails(orderHtml As String, OrderDateTime As DateTime) As String
            Dim DateTimeRow As String = String.Format("<tr><td><b>Date/Time of Order:</b></td><td>{0}</td></tr>", DateTimeRow)
            orderHtml = orderHtml.Replace("<!--DATETIME-->", DateTimeRow)
            Return orderHtml
        End Function

        Public Shared Function GenerateOrderRef(OrderId As Integer, OrderDate As Date) As String
            Return "BBF" & mrk.Common.Strings.PadZeros(OrderId, 4) & Left(OrderDate.ToString("ddd").ToUpper, 2)
        End Function



        Public Shared Function AES_Encrypt(ByVal input As String, ByVal pass As String) As String
            Dim AES As New System.Security.Cryptography.RijndaelManaged
            Dim Hash_AES As New System.Security.Cryptography.MD5CryptoServiceProvider
            Dim encrypted As String = ""
            Try
                Dim hash(31) As Byte
                Dim temp As Byte() = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(pass))
                Array.Copy(temp, 0, hash, 0, 16)
                Array.Copy(temp, 0, hash, 15, 16)
                AES.Key = hash
                AES.Mode = System.Security.Cryptography.CipherMode.ECB
                Dim DESEncrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateEncryptor
                Dim Buffer As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(input)
                encrypted = Convert.ToBase64String(DESEncrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))

            Catch ex As Exception
            End Try
            Return encrypted
        End Function

        Public Shared Function AES_Decrypt(ByVal input As String, ByVal pass As String) As String
            Dim AES As New System.Security.Cryptography.RijndaelManaged
            Dim Hash_AES As New System.Security.Cryptography.MD5CryptoServiceProvider
            Dim decrypted As String = ""
            Try
                Dim hash(31) As Byte
                Dim temp As Byte() = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(pass))
                Array.Copy(temp, 0, hash, 0, 16)
                Array.Copy(temp, 0, hash, 15, 16)
                AES.Key = hash
                AES.Mode = System.Security.Cryptography.CipherMode.ECB
                Dim DESDecrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateDecryptor
                Dim Buffer As Byte() = Convert.FromBase64String(input)
                decrypted = System.Text.ASCIIEncoding.ASCII.GetString(DESDecrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))

            Catch ex As Exception
            End Try
            Return decrypted
        End Function

        Private Const _SALT As String = "||_#%$12;934hgghsdfKIUYIU45hv"



#Region " ORDER WORKERS  AND INTERFACES "



        Private Interface IPerson

            Property Name As String
            Property Address As String
            Property Email As String
            Property ContactNumber As String
            Property Suburb As String
            Property State As String
            Property Postcode As String
        End Interface

        Private Interface ICreditCard
            Property CardType As String
            Property CardHolderName As String
            Property CardNumber As String '//usually 14 digits
            Property CardExpiry As String 'MM/YY
            Property CVC As String
        End Interface

        Public Class RecipientDetails
            Implements IPerson


            ''' <summary>
            ''' Returns an errors
            ''' </summary>
            ''' <returns></returns>
            ''' <remarks></remarks>
            Public Function IsValidDetails() As String

                Dim sb As Text.StringBuilder = New Text.StringBuilder

                If String.IsNullOrWhiteSpace(Me.Name) Then sb.AppendLine("Name is required.")
                If String.IsNullOrWhiteSpace(Me.ContactNumber) Then sb.AppendLine("Contact number is required.")
                If String.IsNullOrWhiteSpace(Me.Address) Then sb.AppendLine("Address is required.")
                If String.IsNullOrWhiteSpace(Me.Suburb) Then sb.AppendLine("Suburb is required.")
                If String.IsNullOrWhiteSpace(Me.Postcode) Then sb.AppendLine("Postcode is required.")
                If String.IsNullOrWhiteSpace(Me.State) Then sb.AppendLine("State is required.")

                Return sb.ToString
            End Function

            Public Property CompanyName As String

            Public Property GiftMessage As String

            Public Property Address As String Implements IPerson.Address

            Public Property ContactNumber As String Implements IPerson.ContactNumber

            Public Property Email As String Implements IPerson.Email

            Public Property Name As String Implements IPerson.Name

            Public Property Postcode As String Implements IPerson.Postcode

            Public Property State As String Implements IPerson.State

            Public Property Suburb As String Implements IPerson.Suburb
        End Class

        Public Class SenderDetails
            Implements IPerson, ICreditCard

            ''' <summary>
            ''' Returns any errors
            ''' </summary>
            ''' <param name="CreditCardUltValidation">When True will perform Mod 10 and Luhn_algorithm checks</param>
            ''' <returns></returns>
            ''' <remarks></remarks>
            Public Function IsValidCreditCard(Optional CreditCardUltValidation As Boolean = True) As String

                Dim sb As Text.StringBuilder = New Text.StringBuilder

                If String.IsNullOrWhiteSpace(Me.CardHolderName) Then sb.AppendLine("NameOnCard is missing.")

                If Not String.IsNullOrWhiteSpace(Me.CardExpiry) Then
                    If IsNumeric(Me.CardExpiry) AndAlso Me.CardExpiry.Length = 4 Then
                        If CreditCardUltValidation Then
                            If Not CreditCardUtility.IsValidExpiry(Me.CardExpiry) Then
                                sb.AppendLine("Card Expiry is not valid.")
                            End If
                        End If

                    Else
                        sb.AppendLine("Card Expiry must be a number and in the format MMYY.")
                    End If
                Else
                    sb.AppendLine("CardExpiry is missing.")
                End If


                If String.IsNullOrWhiteSpace(Me.CardType) Then sb.AppendLine("CardType is missing.")

                If Not String.IsNullOrWhiteSpace(Me.CardNumber) Then
                    Dim s As String = Me.CardNumber
                    s = s.Replace("-", String.Empty).Replace(" ", String.Empty)
                    If IsNumeric(s) Then

                        If CreditCardUltValidation Then
                            Dim CardTypeInt As CreditCardTypeType = CreditCardTypeType.Visa
                            If Me.CardType = "Visa" Then CardTypeInt = CreditCardTypeType.Visa
                            If Me.CardType = "MasterCard" Then CardTypeInt = CreditCardTypeType.MasterCard
                            If Me.CardType = "Amex" Then CardTypeInt = CreditCardTypeType.Amex
                            If Me.CardType = "BankCard" Then CardTypeInt = CreditCardTypeType.BankCard
                            If Me.CardType = "DinersClub" Then CardTypeInt = CreditCardTypeType.DinersClub
                            If Not CreditCardUtility.IsValidNumber(Me.CardNumber, CardTypeInt) Then
                                sb.AppendLine("Card Number is not a valid number.")
                            End If
                        End If

                    Else
                        sb.AppendLine("Card Number needs to be a number.")
                    End If
                Else
                    sb.AppendLine("CardNumber is missing.")
                End If


                Return sb.ToString

            End Function


            ''' <summary>
            ''' Returns an errors
            ''' </summary>
            ''' <returns></returns>
            ''' <remarks></remarks>
            Public Function IsValidDetails() As String

                Dim sb As Text.StringBuilder = New Text.StringBuilder

                If String.IsNullOrWhiteSpace(Me.Name) Then sb.AppendLine("Name is required.")
                If String.IsNullOrWhiteSpace(Me.ContactNumber) Then sb.AppendLine("Contact number is required.")
                If String.IsNullOrWhiteSpace(Me.Email) Then sb.AppendLine("Email is required.")

                Return sb.ToString
            End Function


            ''' <summary>
            ''' AM or PM
            ''' </summary>
            ''' <value></value>
            ''' <returns></returns>
            ''' <remarks></remarks>
            Public Property DeliveryPreferredTime As String

            Public Property DeliveryInstructions As String

            Public Property Address As String Implements IPerson.Address

            Public Property ContactNumber As String Implements IPerson.ContactNumber

            Public Property Email As String Implements IPerson.Email

            Public Property Name As String Implements IPerson.Name

            Public Property Postcode As String Implements IPerson.Postcode

            Public Property State As String Implements IPerson.State

            Public Property Suburb As String Implements IPerson.Suburb

            Public Property CardExpiry As String Implements ICreditCard.CardExpiry

            Public Property CardNumber As String Implements ICreditCard.CardNumber

            Public Property CardType As String Implements ICreditCard.CardType

            Public Property CVC As String Implements ICreditCard.CVC

            Public Property CardHolderName As String Implements ICreditCard.CardHolderName



            ''' <summary>
            ''' Masks the Credit Card Number
            ''' </summary>
            ''' <returns></returns>
            ''' <remarks></remarks>
            Public Function GetMaskedCardNumber() As String
                Return Left(Me.CardNumber, 1) & New String("*", 11) & Right(Me.CardNumber, 2)
            End Function



        End Class


#End Region


    End Class


    Public Class SiteConfig

        Private _ValidateOrderUsingHash As Boolean = True
        Private _ReceiveEmailAfterOrder As Boolean = True
        Private _ReceiveEmailAfterOrderEmailAddress As String = String.Empty '200
        Private _ValidateCCDetails As Boolean = True
        Public Sub New(SiteID As Integer)
            Dim ds As DataSet = SqlHelper.ExecuteDataset(WebConfig.SQL_CONNECTION, CommandType.Text, "SELECT * FROM BBF_SiteConfig WHERE SiteID=@SiteID", New SqlParameter("@SiteID", SiteID))

            If ds.Tables(0).Rows.Count > 0 Then
                Dim r As DataRow = ds.Tables(0).Rows(0)
                _ValidateOrderUsingHash = CBool(r("ValidateOrderUsingHash"))
                _ReceiveEmailAfterOrder = CBool(r("ReceiveEmailAfterOrder"))

                _ReceiveEmailAfterOrderEmailAddress = r("ReceiveEmailAfterOrderEmailAddress").ToString
                _ValidateCCDetails = CBool(r("CreditCardValidation"))
            End If

        End Sub

        Public ReadOnly Property FlatDeliveryFee As Decimal
            Get
                Return 10
            End Get
        End Property


        ''' <summary>
        ''' do hash validation on order submissions
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ValidateOrderUsingHash As Boolean
            Get
                Return _ValidateOrderUsingHash
            End Get
        End Property


        ''' <summary>
        ''' Send the email back to base when true
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ReceiveEmailAfterOrder As Boolean
            Get
                Return _ReceiveEmailAfterOrder
            End Get
        End Property


        ''' <summary>
        ''' The email address to process the order
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ReceiveEmailAfterOrderEmailAddress As String
            Get
                Return _ReceiveEmailAfterOrderEmailAddress
            End Get
        End Property

        ''' <summary>
        ''' Website property to perform validation on Credit Card details
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ValidateCreditCardDetails As Boolean
            Get
                Return _ValidateCCDetails
            End Get
        End Property


        ''' <summary>
        ''' Updates the website configuration
        ''' </summary>
        ''' <param name="SiteID"></param>
        ''' <param name="hashValidation"></param>
        ''' <param name="RecieveOrderEmails"></param>
        ''' <param name="OrderEmailToAddress"></param>
        ''' <param name="CreditCardValidation"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function UpdateConfig(SiteID As Integer, hashValidation As Boolean, _
                                            RecieveOrderEmails As Boolean, OrderEmailToAddress As String, _
                                            CreditCardValidation As Boolean) As Boolean

            Dim SQL As String = "UPDATE BBF_SiteConfig SET ValidateOrderUsingHash=@ValidateOrderUsingHash, " & _
                " ReceiveEmailAfterOrder=@ReceiveEmailAfterOrder, ReceiveEmailAfterOrderEmailAddress=@ReceiveEmailAfterOrderEmailAddress, " & _
                " CreditCardValidation=@CreditCardValidation " & _
                " WHERE SiteID=@SiteID"



            Dim p(4) As Data.SqlClient.SqlParameter
            p(0) = New Data.SqlClient.SqlParameter("@ValidateOrderUsingHash", hashValidation)
            p(1) = New Data.SqlClient.SqlParameter("@ReceiveEmailAfterOrder", RecieveOrderEmails)
            p(2) = New Data.SqlClient.SqlParameter("@ReceiveEmailAfterOrderEmailAddress", Left(OrderEmailToAddress, 200))
            p(3) = New Data.SqlClient.SqlParameter("@SiteID", SiteID)
            p(4) = New Data.SqlClient.SqlParameter("@CreditCardValidation", CreditCardValidation)



            Return SqlHelper.ExecuteNonQuery(WebConfig.SQL_CONNECTION, CommandType.Text, SQL, p) > 0

        End Function



    End Class


End Class



