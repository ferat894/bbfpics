<%@ Page Title="" Language="VB" MasterPageFile="~/admin/admin.master" AutoEventWireup="false" CodeFile="Orders.aspx.vb" Inherits="admin_Orders" %>

<%@ Register Assembly="mrkPopupCalendar" Namespace="mrkPopupCalendar" TagPrefix="cc1" %>

<asp:Content  ContentPlaceHolderID="head" Runat="Server">

    <script type="text/javascript">


        function fetchItem(id) {

            var $container = $('#xxx');
            //$("#loading").show()

            $.ajax({
                type: "GET",
                url: "fetchWebOrder.ashx?anticache=" + Math.random(),
                data: { id: id },
                async: true,
                success: function (data) {
                    //alert(data)
                    //document.getElementById("flower-item").innerHTML = data;

                    $container.hide().html(data).fadeIn()
                    //$("#loading").fadeOut()
                    //$("#loading").fadeOut(function () {
                    // without this the DOM will contain multiple elements
                    // with the same ID, which is bad.
                    //$(this).remove();

                    //$container.hide().html(data).fadeIn();
                    //});
                },
                error: function (data) {
                    alert("Error loading the selected flower item (" + id + ")")
                }
            });


        }

    </script>
    <link href="/css/order.css" rel="stylesheet" />
    <style type="text/css">
        .order-container-list { padding:2px 0px 2px 0px; }
        .order-container-list:hover {background-color:white;  padding-left:5px; }

        #xxx table {
            width:100%;
        }

        .row-section h2 {margin:0; padding:0;}
    </style>

</asp:Content>
<asp:Content  ContentPlaceHolderID="content" Runat="Server">

    <h2><span class="glyphicon glyphicon-shopping-cart"></span> Orders</h2>

    <hr />

    <div>

        <div class="row">

            <div class="col-md-6">

            <div class="form-group">
                <label for="inputEmail" class="col-lg-3 control-label">Date From</label>
                <div class="col-lg-9">
                    <div class="input-group">
                        <asp:TextBox runat="server" ID="tFrom" CssClass="form-control" MaxLength="10" />
                        <span class="input-group-btn">
                            <cc1:mrkPopupCalendar ID="cal1" runat="server" ControlToValidate="tFrom" CssClass="btn btn-default" />
                        </span>
                    </div>
                </div>
            </div>
            <div class="clearfix">&nbsp;</div>
            <div class="form-group">
                <label for="inputEmail" class="col-lg-3 control-label">Date To</label>
                <div class="col-lg-9">
                    <div class="input-group">
                        <asp:TextBox runat="server" ID="tTo"  CssClass="form-control" MaxLength="10"   />
                        <span class="input-group-btn">
                            <cc1:mrkPopupCalendar ID="cal2" runat="server" ControlToValidate="tTo" CssClass="btn btn-default" />    
                        </span>                
                    </div>
                </div>
            </div>
            <div class="clearfix">&nbsp;</div>
            <div class="form-group">
              <div class="col-lg-9 col-lg-offset-3">
                <asp:Button Text=" View Orders By Date " runat="server" ID="cGetOrders" CssClass="btn btn-primary" />
              </div>
            </div>
        

            </div>


            <div class="col-md-6">

                <div class="form-group">
                    <label for="tOrderRef" class="col-lg-3 control-label">Order Ref.</label>
                    <div class="col-lg-9">
                        <asp:TextBox runat="server" ID="tOrderRef" CssClass="form-control" MaxLength="10" placeholder="The number portion of the reference. ie: 325" />
                    </div>
                </div>
                <div class="clearfix">&nbsp;</div>
                <div class="form-group">
                    <label for="tSenderName" class="col-lg-3 control-label">Sender Name</label>
                    <div class="col-lg-9">
                        <asp:TextBox runat="server" ID="tSenderName" CssClass="form-control" MaxLength="50" placeholder="Sender's name" />
                    </div>
                </div>


                <div class="clearfix">&nbsp;</div>
                <div class="form-group">
                  <div class="col-lg-9 col-lg-offset-3">
                    
                    <asp:Button Text=" View Order " runat="server" ID="cGetSingleOrder" CssClass="btn btn-primary" />
                  </div>
                </div>


            </div>

        </div>

        <hr />

    <div class="row">
        <div class="col-md-4">

            <div style="height:600px; overflow-y:auto" class="well">
                <asp:Repeater runat="server" ID="r">
                    <HeaderTemplate>
                        <ol>
                            
                        
                    </HeaderTemplate>
                    <ItemTemplate>
                        <li class="order-container-list">
                            <a href="javascript:void(<%#Eval("OrderID")%>)" onclick="fetchItem(<%#Eval("OrderID")%>)"><%#RenderOrderRef(Eval("OrderID"), Eval("OrderDateTime"))%></a> ( <%#Eval("OrderDateTime")%>)
                        </li>               
                    </ItemTemplate>
                    <FooterTemplate>
                        </ol>

                    </FooterTemplate>
                </asp:Repeater>
            </div>

        </div>

        <div class="col-md-8">
            <asp:Literal EnableViewState="false" ID="l" runat="server" />
                <div id="xxx"></div>
        </div>
    </div>
                 




        




    </div>

</asp:Content>

