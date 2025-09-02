Imports System.Data
Imports System.Net
Imports System.IO
Imports System.Web.Routing

Partial Class category
    Inherits BBF.AdminBasePage

    Dim WebURLPathForFlowerFolder As String = String.Empty
    Property CategoryName As String
    Property PageCategoryID As Integer = 0

    Protected Friend Function RenderCategorySelectedCss(compareCategoryId As Integer) As String
        Dim catID As Integer = Me.PageCategoryID
        If compareCategoryId = catID Then
            Return "selected"
        Else
            Return String.Empty
        End If
    End Function


    Protected Friend Function RenderThumbNail(CateImageFileName As String) As String
        Return WebURLPathForFlowerFolder & CateImageFileName
    End Function

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.PageCategoryID = Me.CategoryID(True)



        If Not Page.IsPostBack Then

            If Me.PageCategoryID > 0 Then
                Dim dt As DataTable = BBF.Item.GetItemsForCategory(Me.PageCategoryID, False)

                If dt.Rows.Count > 0 Then
                    CategoryName = dt.Rows(0).Item("CategoryName").ToString
                    Dim ItemID As Integer = CInt(dt.Rows(0).Item("ItemId"))
                    WebURLPathForFlowerFolder = BBF.Category.GetCategoryFlowerFolder(CategoryName) & "tn/"

                    r.DataSource = dt
                    r.DataBind()

                    f.Text = RenderLoadFlower(ItemID)


                    '//set the page title
                    Me.Page.Title = CategoryName & " Flowers at " & BBF.WEBSITE_NAME
                End If
            End If
        End If

    End Sub


    Private Function RenderLoadFlower(ItemID As Integer) As String


        Dim URL As String = mrk.Web.Common.URL.CurrentURL(True) & "/fetchItem.ashx?id=" & ItemID & "&cache=" & Rnd(Now.Ticks)
        Dim strNewValue As String
        Dim strResponse As String
        ' Create the request obj
        Dim req As HttpWebRequest = DirectCast(WebRequest.Create(URL), HttpWebRequest)
        ' Set values for the request back
        req.Method = "POST"
        req.ContentType = "application/x-www-form-urlencoded"
        strNewValue = "id=" & ItemID
        req.ContentLength = strNewValue.Length
        ' Write the request
        Dim stOut As New StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII)
        stOut.Write(strNewValue)
        stOut.Close()
        ' Do the request to get the response
        Dim stIn As New StreamReader(req.GetResponse().GetResponseStream())
        strResponse = stIn.ReadToEnd()
        stIn.Close()

        Return strResponse


    End Function

End Class
