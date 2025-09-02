<%@ WebHandler Language="VB" Class="FetchWebOrder" %>

Imports System
Imports System.Web
Imports System.IO
Imports System.Net
Imports System.Net.Mail
Imports System.Data
Imports System.Data.SqlClient




Public Class FetchWebOrder : Implements IHttpHandler
    
    Private Structure Pricing
        Public Title As String
        Public Price As Decimal
    End Structure
        
    

        
    
    
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        
  
        
            
        
        Dim OrderId As Integer = 0
        If IsNumeric(context.Request.QueryString("id")) Then OrderId = CInt(context.Request.QueryString("id"))
        If OrderId > 0 Then
           
          
            
            Dim ds As DataSet = BBF.WebOrder.GetOrderDetails (OrderId)
            
            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                Dim s As String = BBF.WebOrder.RenderOrderDetails(ds.Tables(0).Rows(0).Item("OrderHtml").ToString, ds.Tables(0).Rows(0).Item("OrderDateTime"))
                context.Response.Write(s)
            End If
        
        End If
        
        
                
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property
    
 
    
    
End Class
