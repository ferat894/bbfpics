Imports Microsoft.VisualBasic
Imports System
Imports System.Web
Imports System.IO
Imports System.Net
Imports System.Net.Mail
Imports System.Data
Imports System.Data.SqlClient




Public Class FetchItemHandler : Implements IHttpHandler

    Private Structure Pricing
        Public Title As String
        Public Price As Decimal
    End Structure


    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        '   System.Threading.Thread.Sleep(5000)
        'For x As Double = 1 To 99999909
        '    'do
        '    Dim sb As Text.StringBuilder = New Text.StringBuilder
        'Next



        Dim ItemID As Integer = 0
        If IsNumeric(context.Request.QueryString("id")) Then ItemID = CInt(context.Request.QueryString("id"))
        If ItemID > 0 Then
            Dim ds As DataSet = BBF.Item.GetItem(ItemID, False)


            Dim CategoryName As String = String.Empty

            Dim Price1 As New Pricing
            Dim Price2 As New Pricing
            Dim ImageName As String = String.Empty

            Dim s As String = String.Empty
            With ds.Tables(0).Rows(0)

                ImageName = .Item("ItemImageName").ToString

                Price1.Price = CDec(.Item("ItemPrice1").ToString)
                Price1.Title = .Item("ItemPriceTitle1").ToString

                Price2.Price = CDec(.Item("ItemPrice2").ToString)
                Price2.Title = .Item("ItemPriceTitle2").ToString

                s = GetItemTemplate(ItemID, Price1, Price2)

                CategoryName = .Item("CategoryName").ToString
                s = Replace(s, "@ItemName@", .Item("ItemName").ToString)
                s = Replace(s, "@ItemDescription@", .Item("ItemDescription").ToString)
                s = Replace(s, "@ItemImageName@", BBF.Category.GetCategoryFlowerFolder(CategoryName) & ImageName)
                s = Replace(s, "@ItemCode@", Left(ImageName, ImageName.IndexOf(".")))




            End With

            context.Response.Write(s)



        End If



    End Sub

    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property


    Private Function GetItemTemplate(ItemID As Integer, Price1 As Pricing, Price2 As Pricing) As String

        'http://stackoverflow.com/questions/12162576/block-style-radio-buttons-working-on-everything-but-ipad
        'http://forums.macrumors.com/showthread.php?t=785632

        Dim sb As Text.StringBuilder = New Text.StringBuilder
        sb.AppendLine("<div style=""min-height:410px; display:block;""><img src=""@ItemImageName@"" alt=""@ItemName@"" style=""border-bottom: 10px solid red; width:410px; height:410px;"" /></div>")
        sb.AppendFormat("<form action=""/order/{0}"" method=""post""><input type=""hidden"" name=""ItemId"" value=""{0}"" />", ItemID)
        sb.AppendLine("    <div class=""price-panel"">")
        sb.AppendLine("    <strong>@ItemName@</strong>")
        sb.AppendLine("    <p>@ItemDescription@</p>")
        sb.AppendLine("    <div style=""margin-bottom:15px;""><strong>Item Code:</strong> @ItemCode@</div>")
        sb.AppendLine("    <div><strong>Price</strong>")
        sb.AppendLine("    <p>")
        sb.AppendFormat("        <input type=""radio"" name=""ItemPricing"" value=""1"" id=""priceP1"" checked=""checked"" />")
        sb.AppendFormat("        <label onclick="""" for=""priceP1"" class=""price"">{0}{1}</label>", Price1.Price.ToString("c"), IIf(String.IsNullOrEmpty(Price1.Title), "", " (" & Price1.Title & ")"))
        If Price2.Price > 0 Then
            sb.AppendFormat("        <input type=""radio"" name=""ItemPricing"" value=""2"" id=""priceP2"" />")
            sb.AppendFormat("        <label onclick=""""  for=""priceP2"" class=""price"">{0}{1}</label>", Price2.Price.ToString("c"), IIf(String.IsNullOrEmpty(Price2.Title), "", " (" & Price2.Title & ")"))
        End If

        sb.AppendLine("    </p></div>")
        sb.AppendLine("    <div>&nbsp;</div>")
        sb.AppendLine("    <strong>Include Extras</strong>")
        sb.AppendLine("    <ul class=""extra-goodies"">")



        For Each dr As Data.DataRow In BBF.OptionalExtras.GetAll(False).Tables(0).Rows
            sb.AppendLine(CreateExtraHtml(dr))
        Next



        'sb.AppendLine("<li><input type=""checkbox"" name=""cTeddy"" id=""cTeddy"" value=""1"" />        <label for=""cTeddy"">       <img src=""images/flowers/extras/teddy.jpg"" />       <span style=""padding-left:20px"">Teddy <select name=""selTeddy"" id=""selTeddy""><option>Small</option><option>Large</option></select></span></label></li>")
        'sb.AppendLine("<li><input type=""checkbox"" name=""cBalloon"" id=""cBalloon"" value=""1"" />    <label for=""cBalloon"">     <img src=""images/flowers/extras/balloons.jpg"" />    <span style=""padding-left:20px"">Balloon <select name=""selBalloon"" id=""selBalloon""><option>1</option><option>2</option><option>3</option><option>4</option><option>5</option><option>6</option><option>7</option><option>8</option><option>9</option><option>10</option></select></span></label></li>")
        'sb.AppendLine("<li><input type=""checkbox"" name=""cChocolate"" id=""cChocolate"" value=""1"" /><label for=""cChocolate"">   <img src=""images/flowers/extras/chocolates.jpg"" />  <span style=""padding-left:20px"">Chocolate</span></label> </li>")

        sb.AppendLine("    </ul>")
        sb.AppendLine("    <div class=""clear""></div>")
        sb.AppendLine("    <hr />")
        sb.AppendLine("    <div style=""padding:15px 0px 15px 0px;"">")
        sb.AppendLine("     <div class=""small-text bold arial"" style=""background:url(/images/delivery.png) no-repeat 0 5px; padding-left:40px; line-height:17px;"">")
        sb.AppendLine("         Deliveries are available on weekdays in Melbourne Metro areas<br/>during business hours only. <a href=""/faq#deliveries"" target=""_blank"">(details)</a>.<br />All Prices are GST inclusive.")
        sb.AppendLine("         <input type=""submit"" name=""name"" value="" Place Order Now >> "" class=""btn btn-primary"" style=""margin-top:20px; margin-left:-1px; width:100% !important;"" />")
        sb.AppendLine("     </div>")

        sb.AppendLine("     </div>")
        sb.AppendLine("    </div></form>")



        Return sb.ToString

    End Function



    ''' <summary>
    ''' returns an html li element
    ''' </summary>
    ''' <param name="row"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateExtraHtml(row As DataRow) As String

        Dim Price1 As New Pricing
        Dim Price2 As New Pricing

        Price1.Price = CDec(row.Item("Price1").ToString)
        Price1.Title = row.Item("PriceTitle1").ToString

        Price2.Price = CDec(row.Item("Price2").ToString)
        Price2.Title = row.Item("PriceTitle2").ToString

        Dim OptionTitle As String = row.Item("Title").ToString
        Dim ImageName As String = row.Item("ImageName").ToString

        Dim OptionID As Integer = row.Item("OptionalExtraId")
        Dim AllowMultiple As Boolean = CBool(row.Item("AllowMultiple"))

        Dim IsDisabled As Boolean = CBool(row.Item("Disabled"))


        Dim s As String = String.Empty
        Select Case True
            Case Price1.Price > 0 AndAlso Price2.Price > 0
                s = "<li><input type=""checkbox"" name=""IncludeOptionID_{0}"" id=""IncludeOptionID_{0}"" value=""1"" /><label onclick="""" for=""IncludeOptionID_{0}"">{1}<br /><img src=""/images/flowers/extras/{2}"" /></label><br /><span style=""font-size:smaller"">{5} or {6}</span><select class=""selExtraPricing"" name=""PricingOptionID_{0}"" id=""PricingOptionID_{0}""><option value=""1"">{3}</option><option value=""2"">{4}</option></select><!----></li>"
                s = String.Format(s, OptionID, OptionTitle, ImageName, Price1.Title & " (" & Price1.Price.ToString("$#,##") & ")", Price2.Title & " (" & Price2.Price.ToString("$#,##") & ")", _
                                  Price1.Price.ToString("c"), Price2.Price.ToString("c"))
            Case Price1.Price > 0
                s = "<li><input type=""checkbox"" name=""IncludeOptionID_{0}"" id=""IncludeOptionID_{0}"" value=""1"" /><label onclick="""" for=""IncludeOptionID_{0}"">{1}<br /><img src=""/images/flowers/extras/{2}"" /></label><br /><span style=""font-size:smaller"">{3}</span><!----><input type=""hidden""  name=""PricingOptionID_{0}"" value=""1"" /></li>"
                s = String.Format(s, OptionID, OptionTitle, ImageName, Price1.Price.ToString("c") & IIf(AllowMultiple, " each", ""))
        End Select

        If AllowMultiple Then
            Dim sb As Text.StringBuilder = New Text.StringBuilder
            For x As Integer = 1 To 10
                sb.AppendFormat("<option value=""{0}"">{0}</option>", x)
            Next
            s = Replace(s, "<!---->", "<br /><select class=""selExtraQty"" name=""QtyIncludeOptionID_{0}"" id=""QtyIncludeOptionID_{0}"">" & sb.ToString & "</select>")
            s = String.Format(s, OptionID)
        Else
            s = Replace(s, "<!---->", String.Empty)
        End If

        Return s


    End Function


End Class
