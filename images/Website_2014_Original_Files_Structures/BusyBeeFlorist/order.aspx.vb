Imports System.Data
Imports System.Data.SqlClient
Imports System.Net.Mail


''' <summary>
''' Collects the from the Request.Form
''' </summary>
''' <remarks></remarks>
Public Class FlowerCollector

    ''' <summary>
    ''' optional extra items class
    ''' </summary>
    ''' <remarks></remarks>
    Public Class OptionalItem

        ''' <summary>
        ''' the id of the optional item
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property IncludeOptionID As Integer = 0 '
        ''' <summary>
        ''' level of pricing (1 or 2)
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PricingOptionID As Integer = 1 'either 1 or 2 : default 1
        ''' <summary>
        ''' the qty of the optional item selected
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property QtyIncludeOptionID As Integer = 0 'qty count if present


        Public Property ImageName As String = String.Empty
        Public Property Title As String = String.Empty
        Public Property PriceTitle1 As String = String.Empty
        Public Property PriceTitle2 As String = String.Empty

        '//business logic labels
        Public Property PriceTitleLabel As String = String.Empty ''price title after calculation
        Public Property UnitQtyLabel As String = String.Empty
        Public Property UnitPrice As Decimal = 0
        Public Property SubTotalPrice As Decimal = 0


        Private _InternalOptionalExtraPrice1 As Decimal = 0
        ''' <summary>
        ''' the price of the extra item from the database
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property InternalOptionalExtraPrice1 As Decimal
            Get
                Return _InternalOptionalExtraPrice1
            End Get
        End Property

        Private _InternalOptionalExtraPrice2 As Decimal = 0
        ''' <summary>
        ''' the price of the extra item from the database
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property InternalOptionalExtraPrice2 As Decimal
            Get
                Return _InternalOptionalExtraPrice2
            End Get
        End Property

        Private _Disabled As Boolean = False
        Public ReadOnly Property Disabled As Boolean
            Get
                Return _Disabled
            End Get
        End Property




        Public Sub FillInteralItemInfo()



            Dim ds As DataSet = BBF.OptionalExtras.GetItem(Me.IncludeOptionID, False)
            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                Dim r As DataRow = ds.Tables(0).Rows(0)

                _InternalOptionalExtraPrice1 = CDec(r("Price1"))
                _InternalOptionalExtraPrice2 = CDec(r("Price2"))
                _Disabled = CBool(r("Disabled"))

                Me.ImageName = r("ImageName").ToString
                Me.Title = r("Title").ToString
                Me.PriceTitle1 = r("PriceTitle1").ToString
                Me.PriceTitle2 = r("PriceTitle2").ToString
                Dim AllowMultiple As Boolean = CBool(r("AllowMultiple"))


                '//get the pricing label
                If Me.PricingOptionID = 1 Then
                    Me.PriceTitleLabel = Me.PriceTitle1
                    Me.UnitPrice = Me.InternalOptionalExtraPrice1
                End If

                If Me.PricingOptionID = 2 Then
                    Me.UnitPrice = Me.InternalOptionalExtraPrice2
                    Me.PriceTitleLabel = Me.PriceTitle2
                End If

                If Me.QtyIncludeOptionID > 0 Then
                    Me.SubTotalPrice = (Me.UnitPrice * Me.QtyIncludeOptionID)
                    Me.UnitQtyLabel = Me.QtyIncludeOptionID & " x "
                Else
                    Me.SubTotalPrice = Me.UnitPrice
                End If




            End If

        End Sub



    End Class


    ''' <summary>
    ''' list of optional extras
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property OptionalItems As List(Of OptionalItem) = New List(Of OptionalItem)
    ''' <summary>
    ''' flower item id
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ItemId As Integer = 0
    ''' <summary>
    ''' level of item pricing
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ItemPricing As Integer = 1 'default to 1, either 1 or 2

    ''' <summary>
    ''' calculates the total  of the optional extras
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CalculateSubTotalOptionalItems() As Decimal
        Dim totalExtraPrice As Decimal = 0
        If Me.OptionalItems.Count > 0 Then
            For Each f As FlowerCollector.OptionalItem In Me.OptionalItems
                totalExtraPrice += f.SubTotalPrice
            Next
        End If

        Return totalExtraPrice
    End Function

    Private Const MAX_ITEMS As Integer = 100

    Public Sub New()

    End Sub

    Public Sub New(Request As HttpRequest)
        Call Loadkeys(Request)
    End Sub

    Public Sub Loadkeys(Request As HttpRequest)
        If Not IsNothing(Request.Form("ItemId")) Then
            If IsNumeric(Request.Form("ItemId")) Then Me.ItemId = CInt(Request.Form("ItemId")) 'the id of the item
        End If

        If Not IsNothing(Request.Form("ItemPricing")) Then
            If IsNumeric(Request.Form("ItemPricing")) Then Me.ItemPricing = CInt(Request.Form("ItemPricing")) 'the level  of the item pricing
        End If


        Dim x As Integer = 0
        Dim NamedID As String = String.Empty
        Dim PricingID As String = String.Empty
        Dim QtyID As String = String.Empty

        For x = 1 To MAX_ITEMS
            NamedID = "IncludeOptionID_" & x
            PricingID = "PricingOptionID_" & x
            QtyID = "QtyIncludeOptionID_" & x

            Dim item As OptionalItem = New OptionalItem

            If Not IsNothing(Request.Form(NamedID)) Then
                If Request.Form(NamedID) = "1" Then
                    item.IncludeOptionID = x 'the id of the optional item



                    If Not IsNothing(Request.Form(PricingID)) Then
                        If IsNumeric(Request.Form(PricingID)) Then item.PricingOptionID = CInt(Request.Form(PricingID)) 'the selection of the pricing either 1 or 2, default 1
                    End If



                    If Not IsNothing(Request.Form(QtyID)) Then '/default is zero
                        If IsNumeric(Request.Form(QtyID)) Then item.QtyIncludeOptionID = CInt(Request.Form(QtyID))
                    End If



                    ''add to collection
                    If item.IncludeOptionID > 0 Then
                        Call item.FillInteralItemInfo()
                        Me.OptionalItems.Add(item)
                    End If


                End If

            End If



        Next
    End Sub

End Class

Partial Class order
    Inherits BBF.AdminBasePage


    Private Property PageItemId As Integer


    Private Structure OrderProperty
        Public Flower As FlowerCollector

        ''' <summary>
        ''' category
        ''' </summary>
        ''' <remarks></remarks>
        Public CategoryName As String
        ''' <summary>
        ''' flower image name
        ''' </summary>
        ''' <remarks></remarks>
        Public ImageName As String
        ''' <summary>
        ''' Flower price
        ''' </summary>
        ''' <remarks></remarks>
        Public Price As Decimal
        ''' <summary>
        ''' flower price title
        ''' </summary>
        ''' <remarks></remarks>
        Public PriceTitle As String

        Public ItemId As String
        Public ItemTitle As String


    End Structure



    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.PageItemId = Me.ItemId(True)

        If Not Page.IsPostBack Then

            If Request.QueryString("summary") = "1" Then
                Dim SummaryItemID As Integer = 0
                If Not IsNothing(Request.Form("ItemId")) AndAlso IsNumeric(Request.Form("ItemId")) Then SummaryItemID = CInt(Request.Form("ItemId"))
                Call GetOrderDetails(SummaryItemID)
                Exit Sub
            End If

            ''//repost the form values
            Dim sb As Text.StringBuilder = New Text.StringBuilder
            For Each k As String In Request.Form.Keys
                sb.AppendFormat("<input type=""hidden"" name=""{0}"" value=""{1}"" />", k, Request.Form(k))
            Next
            l.Text = sb.ToString


            Dim ItemId As Integer = Me.PageItemId
            If ItemId > 0 Then
                Dim x As OrderProperty = LoadItem(ItemId)

                If Not x.ItemId > 0 Or String.IsNullOrWhiteSpace(x.ItemTitle) Then
                    panel_order.Visible = False
                    panel_result.Visible = True
                    lbHtml.Text = "<h2 style=""margin-bottom:20px; margin-top:20px;"">This flower is no longer available</h2>" & Common.Messages.DisplayMessage("danger", "The flower you have selected is no longer available as we do not have stock at the moment. We are sorry for the inconvenience, please try selecting another flower. <a href=""home"">Please click here start again.</a>")
                End If
            Else
                panel_order.Visible = False
                panel_result.Visible = True
                lbHtml.Text = "<h2 style=""margin-bottom:20px; margin-top:20px;"">No Information</h2>" & Common.Messages.DisplayMessage("danger", "This process could not fetch the necessary information to display the page. <a href=""home"">Please click here start again.</a>")
            End If
        End If

    End Sub


    Private Function LoadItem(ItemId As Integer) As OrderProperty



        Dim flower As FlowerCollector = New FlowerCollector(Request)
        Dim ds As DataSet = BBF.Item.GetItem(ItemId, WebConfig.SiteID, False)
        Dim x As New OrderProperty


        If ds.Tables(0).Rows.Count > 0 Then


            If Not CBool(ds.Tables(0).Rows(0).Item("ItemDisabled")) Then
                Dim FlatDeliveryFee As Decimal = 0
                Dim CategoryName As String = String.Empty
                Dim ImageName As String = String.Empty
                Dim Price As Decimal = 0
                Dim PriceTitle As String = String.Empty

                With ds.Tables(0).Rows(0)
                    lbName.Text = .Item("ItemName")
                    lbDescription.Text = .Item("ItemDescription")

                    Price = CDec(.Item("ItemPrice1"))
                    PriceTitle = .Item("ItemPriceTitle1").ToString()



                    If flower.ItemPricing = 2 Then
                        Price = CDec(.Item("ItemPrice2"))
                        PriceTitle = .Item("ItemPriceTitle2").ToString()
                    End If

                    If Not String.IsNullOrEmpty(PriceTitle) Then PriceTitle = " (" & PriceTitle & ")"

                    lbPrice.Text = Price.ToString("c") & PriceTitle

                    ImageName = .Item("ItemImageName").ToString
                    CategoryName = .Item("CategoryName").ToString
                    FlatDeliveryFee = CDec(.Item("FlatDeliveryFee").ToString)

                    lbItemCode.Text = Left(ImageName, ImageName.IndexOf("."))

                End With

                '//the flower image
                imgLarge.ImageUrl = BBF.Category.GetCategoryFlowerFolder(CategoryName) & ImageName



                '//the optional items
                If flower.OptionalItems.Count > 0 Then
                    r.DataSource = flower.OptionalItems
                Else
                    lbNoExtras.Text = "No extra items."
                End If
                r.DataBind()


                '//add the total pricing at the bottom of the form
                lbFinalFlowerTotal.Text = Price.ToString("c")
                Dim totalExtraPrice As Decimal = 0
                If flower.OptionalItems.Count > 0 Then
                    totalExtraPrice = flower.CalculateSubTotalOptionalItems()
                    lbFinalExtrasTotal.Text = totalExtraPrice.ToString("c")
                Else
                    lbFinalExtrasTotal.Text = "N/A"
                End If
                lbDeliveryFee.Text = FlatDeliveryFee.ToString("c")
                lbFinalTotalPrice.Text = (FlatDeliveryFee + Price + totalExtraPrice).ToString("c")


                '//collect information

                With x
                    .ItemTitle = lbName.Text
                    .ItemId = ItemId
                    .Flower = flower
                    .CategoryName = CategoryName
                    .ImageName = ImageName
                    .Price = Price
                    .PriceTitle = PriceTitle
                End With

                Return x



            End If
        End If
        Return x


    End Function







    Private Sub GetOrderDetails(ItemID As Integer)

        If Not ItemID > 0 Then
            l.Text = Common.Messages.DisplayMessage("danger", "There was an error while trying to process this error. Please try again and if the problem persits then please contact us.")
            Exit Sub
        End If

        '//get configurations
        Dim Config As BBF.SiteConfig = New BBF.SiteConfig(WebConfig.SiteID)


        '************************************************************************************
        '// collect sender and recipient info
        '************************************************************************************
        Dim Recipient As New BBF.WebOrder.RecipientDetails
        With Recipient
            .Name = Server.HtmlEncode(Request.Form("rName").ToString.Trim)
            .Email = Server.HtmlEncode(Request.Form("rEmail").ToString.Trim)
            .ContactNumber = Server.HtmlEncode(Request.Form("rPhone").ToString.Trim)
            .Address = Server.HtmlEncode(Request.Form("rAddress").ToString.Trim)
            .Suburb = Server.HtmlEncode(Request.Form("rCity").ToString.Trim)
            .State = Server.HtmlEncode(Request.Form("rState").ToString.Trim)
            .Postcode = Server.HtmlEncode(Request.Form("rPostcode").ToString.Trim)
            .CompanyName = Server.HtmlEncode(Request.Form("rCompanyName").ToString.Trim)
        End With
        Dim Sender As New BBF.WebOrder.SenderDetails
        With Sender
            .Name = Server.HtmlEncode(Request.Form("sName").ToString.Trim)
            .Email = Server.HtmlEncode(Request.Form("sEmail").ToString.Trim)
            .ContactNumber = Server.HtmlEncode(Request.Form("sPhone").ToString.Trim)
            .Address = Server.HtmlEncode(Request.Form("sAddress").ToString.Trim)
            .Suburb = Server.HtmlEncode(Request.Form("sCity").ToString.Trim)
            .State = Server.HtmlEncode(Request.Form("sState").ToString.Trim)
            .Postcode = Server.HtmlEncode(Request.Form("sPostcode").ToString.Trim)
            .DeliveryInstructions = Server.HtmlEncode(Request.Form("rDeliveryInstructions").ToString.Trim)
            .DeliveryPreferredTime = Server.HtmlEncode(Request.Form("rPreferredTime").ToString.Trim)

            .CardExpiry = Server.HtmlEncode(Request.Form("pCardExpiry").ToString.Trim)
            .CardType = Server.HtmlEncode(Request.Form("pCardType").ToString.Trim)
            .CardHolderName = Server.HtmlEncode(Request.Form("pCardholderName").ToString.Trim)
            .CardNumber = Server.HtmlEncode(Request.Form("pCardNumber").ToString.Trim)
            .CVC = String.Empty

        End With

        '************************************************************************************
        '//internal validation
        '************************************************************************************

        Dim sbError As Text.StringBuilder = New Text.StringBuilder
        Dim err As String = String.Empty

        err = Sender.IsValidDetails()
        If Not String.IsNullOrWhiteSpace(err) Then sbError.AppendFormat("<p><b>Sender's Details are not valid</b></p><p>{0}</p>", err.Replace(vbNewLine, "<br />"))

        err = Sender.IsValidCreditCard(Config.ValidateCreditCardDetails)
        If Not String.IsNullOrWhiteSpace(err) Then sbError.AppendFormat("<p><b>Credit Card Details are not valid</b></p><p>{0}</p>", err.Replace(vbNewLine, "<br />"))

        err = Recipient.IsValidDetails()
        If Not String.IsNullOrWhiteSpace(err) Then sbError.AppendFormat("<p><b>Recipient's Details are not valid</b></p><p>{0}</p>", err.Replace(vbNewLine, "<br />"))

        If Not String.IsNullOrWhiteSpace(sbError.ToString) Then
            lbHtml.Text = Common.Messages.DisplayMessage("danger", "The page has missing information, please enter the required details and try again. (<a href=""javascript:void(0);"" onclick=""window.history.go(-1);"">GO BACK</a>)<hr />" & sbError.ToString)
            panel_order.Visible = False
            panel_result.Visible = True
            bContinue.Visible = False
            bGoBack.Visible = True
            Exit Sub
        End If



        '************************************************************************************
        '//get the order items and extra items
        '************************************************************************************

        Dim order As OrderProperty = LoadItem(ItemID)
        Dim extraOrderItems As New List(Of BBF.OptionalExtras)


        Dim extraItems As String = String.Empty
        extraItems &= "<span style=""float:left"">"
        extraItems &= "     <span style=""float:left; margin-right:50px; text-align:center;"">"
        extraItems &= "         {0} {1} {2}<br />"
        extraItems &= "         {3} each<br />"
        extraItems &= "         <span style=""border-bottom:3px double black; border-top:1px solid black; margin-bottom:5px;"">"
        extraItems &= "         {4}</span><br />"
        extraItems &= "         <img src=""{5}{6}"" />"
        extraItems &= "     </span>"
        extraItems &= "</span>"


        Dim sbExtras As Text.StringBuilder = New Text.StringBuilder
        Dim t As String = String.Empty
        If order.Flower.OptionalItems.Count > 0 Then
            For Each x As FlowerCollector.OptionalItem In order.Flower.OptionalItems
                sbExtras.AppendFormat(extraItems, x.UnitQtyLabel, x.PriceTitleLabel, _
                                      x.Title, x.UnitPrice.ToString("c"), x.SubTotalPrice.ToString("c"), _
                                      mrk.Web.Common.URL.CurrentURL(True) & "/images/flowers/extras/", x.ImageName)


                '//adds to the list for insert into data store
                extraOrderItems.Add(New BBF.OptionalExtras(x.IncludeOptionID, x.Title, x.UnitPrice, _
                                                           x.PriceTitleLabel, _
                                                           IIf(x.QtyIncludeOptionID = 0, 1, x.QtyIncludeOptionID)))
            Next

        Else
            sbExtras.Append("No extras.")
        End If


        '************************************************************************************
        '//render to display confirmation
        '************************************************************************************
        Dim sb As Text.StringBuilder = New Text.StringBuilder
        ''if we use the hash of the the html with the date time, we then cant use it to do a compare before
        ''injecting into database cuz it will awalys be different.
        ''we added the datetime marker in the html string later
        ''and when we fetch it from the database, we insert the database datetime field into the marker
        Dim DateTimeRow As String = String.Format("<tr><td><b>Date/Time of Order:</b></td><td>{0}</td></tr>", Now.ToString)

        Dim OrderSumTotal As Decimal = (Config.FlatDeliveryFee + order.Price + order.Flower.CalculateSubTotalOptionalItems)

        sb.AppendFormat("<table class=""order-html-table"" border=""0"" cellpadding=""4"" cellspacing=""4"">")

        'order details
        sb.AppendFormat("<tr class=""row-section""><td colspan=""2""><h2>Order Details</h2></td></tr>")
        sb.AppendFormat("<tr class=""row-alt""><td><b>Item:</b></td><td>{0}<br /><img src=""{1}{2}"" title=""{0}"" /></td></tr>", order.ItemTitle, mrk.Web.Common.URL.CurrentURL(True) & "/images/flowers/tn/", order.ImageName)
        sb.AppendFormat("<tr><td><b>Item Code:</b></td><td>{0}</td></tr>", Left(order.ImageName, order.ImageName.IndexOf(".")))
        sb.AppendFormat("<tr class=""row-alt""><td><b>Item Id:</b></td><td>{0}</td></tr>", order.ItemId)
        sb.AppendFormat("<tr><td><b>Item Price:</b></td><td>{0}</td></tr>", order.Price.ToString("c"))
        sb.AppendFormat("<tr class=""row-alt""><td><b>Extras:</b></td><td>{0}<span style=""clear:both;""></span></td></tr>", sbExtras.ToString)
        sb.AppendFormat("<!--DATETIME-->") 'date time marker
        sb.AppendFormat("<tr><td><b>Delivery Fee:</b></td><td>{0}</td></tr>", Config.FlatDeliveryFee.ToString("c"))
        sb.AppendFormat("<tr class=""row-alt""><td><b>Total Order Price:</b></td><td>Items: {0}<br />Extras: {1}<br />Delivery: {2}<br />Total: <span style=""border-bottom:3px double black; border-top:1px solid black; margin-bottom:5px;font-weight:bold;"">{3}</span></td></tr>", order.Price.ToString("c"), order.Flower.CalculateSubTotalOptionalItems.ToString("c"), Config.FlatDeliveryFee.ToString("c"), OrderSumTotal.ToString("c"))

        'sender
        sb.AppendFormat("<tr class=""row-section""><td colspan=""2""><h2>Sender's Details</h2></td></tr>")
        sb.AppendFormat("<tr><td><b>Name:</b></td><td>{0}</td></tr>", Server.HtmlEncode(Sender.Name))
        sb.AppendFormat("<tr class=""row-alt""><td><b>Email:</b></td><td>{0}</td></tr>", Server.HtmlEncode(Sender.Email))
        sb.AppendFormat("<tr><td><b>Contact Number:</b></td><td>{0}</td></tr>", Server.HtmlEncode(Sender.ContactNumber))
        sb.AppendFormat("<tr class=""row-alt""><td><b>Address:</b></td><td>{0}</td></tr>", Server.HtmlEncode(Sender.Address))
        sb.AppendFormat("<tr><td><b>Suburb:</b></td><td>{0}</td></tr>", Server.HtmlEncode(Sender.Suburb))
        sb.AppendFormat("<tr class=""row-alt""><td><b>Postcode:</b></td><td>{0}</td></tr>", Server.HtmlEncode(Sender.Postcode))
        sb.AppendFormat("<tr><td><b>State:</b></td><td>{0}</td></tr>", Server.HtmlEncode(Sender.State))


        'recipent
        sb.AppendFormat("<tr class=""row-section""><td colspan=""2""><h2>Recipient's Details</h2></td></tr>")
        sb.AppendFormat("<tr><td><b>Name:</b></td><td>{0}</td></tr>", Server.HtmlEncode(Recipient.Name))
        sb.AppendFormat("<tr class=""row-alt""><td><b>Company Name:</b></td><td>{0}</td></tr>", Server.HtmlEncode(Recipient.CompanyName))
        sb.AppendFormat("<tr><td><b>Email:</b></td><td>{0}</td></tr>", Server.HtmlEncode(Recipient.Email))
        sb.AppendFormat("<tr class=""row-alt""><td><b>Contact Number:</b></td><td>{0}</td></tr>", Server.HtmlEncode(Recipient.ContactNumber))
        sb.AppendFormat("<tr><td><b>Address:</b></td><td>{0}</td></tr>", Server.HtmlEncode(Recipient.Address))
        sb.AppendFormat("<tr class=""row-alt""><td><b>Suburb:</b></td><td>{0}</td></tr>", Server.HtmlEncode(Recipient.Suburb))
        sb.AppendFormat("<tr><td><b>Postcode:</b></td><td>{0}</td></tr>", Server.HtmlEncode(Recipient.Postcode))
        sb.AppendFormat("<tr class=""row-alt""><td><b>State:</b></td><td>{0}</td></tr>", Server.HtmlEncode(Recipient.State))
        sb.AppendFormat("<tr><td><b>Preferred Time:</b></td><td>{0}</td></tr>", Server.HtmlEncode(Sender.DeliveryPreferredTime))
        sb.AppendFormat("<tr class=""row-alt""><td><b>Delivery Instructions:</b></td><td>{0}</td></tr>", Server.HtmlEncode(Sender.DeliveryInstructions))
        'Payment
        Dim CCNumberMasked As String = Server.HtmlEncode(Sender.GetMaskedCardNumber)
        sb.AppendFormat("<tr class=""row-section""><td colspan=""2""><h2>Payment Details</h2></td></tr>")
        sb.AppendFormat("<tr><td><b>Card Type:</b></td><td>{0}</td></tr>", Server.HtmlEncode(Sender.CardType))
        sb.AppendFormat("<tr class=""row-alt""><td><b>Card Number:</b></td><td>{0}</td></tr>", CCNumberMasked)
        sb.AppendFormat("<tr><td><b>Card Expiry:</b></td><td>{0}</td></tr>", Server.HtmlEncode(Sender.CardExpiry))
        sb.AppendFormat("<tr class=""row-alt""><td><b>Cardholder's Name:</b></td><td>{0}</td></tr>", Server.HtmlEncode(Sender.CardHolderName))

        sb.AppendFormat("<tr class=""row-section""><td colspan=""2""><h2>Signature</h2></td></tr>")
        sb.AppendFormat("<tr><td><b>IP Address</b></td><td>{0}</td></tr>", Request.UserHostAddress)
        sb.AppendFormat("</table>")


        '//this is the unmasked info to send to BBF
        Dim sbUnmasked As String = sb.ToString
        sbUnmasked = Replace(sbUnmasked, CCNumberMasked, Server.HtmlEncode(Sender.CardNumber))





        Dim TimeOut As Integer = IIf(Config.ValidateOrderUsingHash, 30, -1) '//when time out value is 0 or less, there is no internal comparison 

        'Response.Clear()
        'Response.Write(BBF.WebOrder.AES_Decrypt("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", "sdfsdf"))
        'Response.End()

        Dim OrderId As Integer = BBF.WebOrder.AddOrder(Request.UserHostAddress, sb.ToString, Common.SimpleHash.SHA256(sb.ToString), _
                                                       TimeOut, order.ItemId, order.ItemTitle, order.Price, order.PriceTitle, _
                                                       Sender, Recipient, extraOrderItems, Config.FlatDeliveryFee)


        panel_order.Visible = False
        panel_result.Visible = True
        Select Case OrderId
            Case Is > 0

                Dim orderRef As String = BBF.WebOrder.GenerateOrderRef(OrderId, Now.Date)
                Dim emailErrorHtml As String = String.Empty


                '//sends email
                If Config.ReceiveEmailAfterOrder Then

                    Dim ToAddress As String = Sender.Email
                    Dim BBFEmail As String = Config.ReceiveEmailAfterOrderEmailAddress

                    If String.IsNullOrWhiteSpace(BBFEmail) Then BBFEmail = WebConfig.DEFAULT_EMAIL_ADDRESS

                    Dim EmailCss As String = GetEmailCss()


                    '//masked version of email
                    Dim messageToClient As New MailMessage(BBFEmail, ToAddress, _
                                                   "Busy Bee Florist Order Confirmation: " & orderRef, _
                                                   EmailCss & String.Format("<h3>Dear {0}</h3><p>Thank you for your order, this is an automatic email confirming your order details." & _
                                                                 " Please carefully revise your order and contact us if you have any further queries.</p><hr />{1}", _
                                                                  Sender.Name, sb.ToString))

                    '//unmasked version
                    Dim messageBBF As New MailMessage(WebConfig.DEFAULT_EMAIL_ADDRESS, BBFEmail, _
                                                   "Busy Bee Florist Order Confirmation: " & orderRef, _
                                                   EmailCss & String.Format("<h3>Dear {0}</h3><p>Thank you for your order, this is an automatic email confirming your order details." & _
                                                                 " Please carefully revise your order and contact us if you have any further queries.</p><hr />{1}", _
                                                                  Sender.Name, sbUnmasked))

                    Dim WasError As Boolean = False
                    Try
                        Dim emailClient As New SmtpClient(WebConfig.EMAIL_SERVER)
                        messageToClient.IsBodyHtml = True
                        messageBBF.IsBodyHtml = True
                        emailClient.Send(messageBBF)
                        emailClient.Send(messageToClient)

                        'update order status
                        Call BBF.WebOrder.UpdateOrderEmailSent(OrderId)
                    Catch ex As Exception
                        Dim x As New Elmah.Error(ex)
                        x.Message &= " Order Id: " & OrderId
                        Elmah.ErrorLog.GetDefault(HttpContext.Current).Log(x)
                        WasError = True

                    End Try

                    'TODO: have to accomodate for paypal if present
                    If WasError Then emailErrorHtml = Common.Messages.DisplayMessage("warning", "<h1 class=""glyphicon glyphicon-exclamation-sign"">Wait, one last step.</h1><p>Your order was successful, however, our emailing systems are down at the moment and was not able to send out a confirmation email, please contact us to confirm  your order: <b>" & orderRef & "</b> </p>.")


                End If

                '/render the message
                lbHtml.Text = Common.Messages.DisplayMessage("success", "<h1 class=""glyphicon glyphicon-ok-sign""></h1> Thank you, your order (<b>" & orderRef & "</b>) has been placed.") & emailErrorHtml & "<hr />" & sb.ToString.Replace("<!--DATETIME-->", DateTimeRow)


                If Request.QueryString("pp") = "1" Then
                    'TODO: have to deal with case when the try statement results in error
                    '//do paypal
                    Session("payment_amt") = OrderSumTotal.ToString(Common.MONEY_FORMAT)
                    Session("order_id") = OrderId
                    Response.Redirect("/checkout/start")
                End If

            Case -11
                lbHtml.Text = Common.Messages.DisplayMessage("info", "<h1 class=""glyphicon glyphicon-exclamation-sign""></h1> There is already another order with the same details, please alter your order or wait " & TimeOut & " mintues before trying again.<br/><br/><a href=""javascript:void(0);"" onclick=""window.history.go(-1);"">Back to Order Page</a>.")
            Case -500
                lbHtml.Text = Common.Messages.DisplayMessage("danger", "<h1 class=""glyphicon glyphicon-exclamation-sign""></h1> Could not process your order, no rows returned.<br/><br/><a href=""javascript:void(0);"" onclick=""window.history.go(-1);"">Back to Order Page</a>.")
            Case Else
                lbHtml.Text = Common.Messages.DisplayMessage("danger", "<h1 class=""glyphicon glyphicon-exclamation-sign""></h1> Could not process your order. (Error: " & OrderId & ")<br/><br/><a href=""javascript:void(0);"" onclick=""window.history.go(-1);"">Back to Order Page</a>.")
        End Select






    End Sub



    Private Function GetEmailCss() As String

        Dim sb As Text.StringBuilder = New Text.StringBuilder
        sb.AppendLine("    <style type=""text/css"">")
        sb.AppendLine("        table.order-html-table { border-spacing: 10px; border-collapse: separate; border: 1px solid #c0c0c0; -webkit-border-radius: 7px !important; -moz-border-radius: 7px !important; border-radius: 7px !important; width: 80%; margin: 0 auto; text-align: left; }")
        sb.AppendLine("        tr.row-section td {color: #fff !important; border: none !important; background-color: #ae75aa !important; -webkit-border-radius: 7px !important;  -moz-border-radius: 7px !important; border-radius: 7px !important; }")
        sb.AppendLine("        table.order-html-table td { background-color: #f5f5f5; padding: 10px;}")
        sb.AppendLine("        table.order-html-table td:last-child { background-color: #f5f5f5; }")
        sb.AppendLine("    </style>")

        Return sb.ToString

    End Function

End Class
