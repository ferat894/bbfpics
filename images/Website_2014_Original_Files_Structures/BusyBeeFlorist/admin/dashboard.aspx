<%@ Page Title="" Language="VB" MasterPageFile="~/admin/admin.master" AutoEventWireup="false" CodeFile="dashboard.aspx.vb" Inherits="admin_dashboard" %>

<asp:Content ContentPlaceHolderID="head" Runat="Server">

    <style type="text/css">


        h3.h3 {
            width:90%;
        }

        h3.h3 span.glyphicon {
            margin-right:20px;
        }

        h3.h3 span.badge {
            padding: 10px;
            float: right;
            min-width: 50px;
            text-align:center;
        }

        h3.h3 span.green {
            text-align:center;
            background-color:green !important; 
        }

        div.row div.well:hover {
            background-color:#555;
            cursor:pointer;
        }

    </style>

    <script type="text/javascript">
        $(function () {
            $("div.row div.well").each(function () {
                $(this).click(function () {
                    var href = $(this).find("a").attr("href");
                    window.location.href = href;
                });
            });
        });
    </script>

</asp:Content>
<asp:Content ContentPlaceHolderID="content" Runat="Server">
    
    <h2>Dashboard</h2>
    <p><a href="BusyBeeFlorist.pdf" target="_blank"><i class="fa fa-info-circle"></i> User Documentation</a></p>
    <div class="jumbotron">
        

        <div class="row">

            <div class="col-lg-5 well"><h3 class="h3"><span class="glyphicon glyphicon-cog"></span><a href="SiteConfig">Site Settings</a> <span class="badge green"> OK </span></h3></div>

            <div class="col-lg-5 col-lg-offset-2 well"><h3 class="h3"><span class="glyphicon glyphicon-shopping-cart"></span><a href="orders">Orders</a> <asp:Literal runat="server" ID="OrdersCount" />&nbsp;</h3></div>

            <div class="col-lg-5 well"><h3 class="h3"><span class="glyphicon glyphicon-leaf"></span><a href="ItemList">Flowers</a> <span class="badge"><asp:Literal runat="server" ID="FlowersCount" />&nbsp;</span></h3></div>

            <div class="col-lg-5 col-lg-offset-2 well"><h3 class="h3"><span class="glyphicon glyphicon-th-list"></span><a href="ItemList?section=extras">Extra Items</a> <span class="badge"><asp:Literal runat="server" ID="ExtraItemCount" />&nbsp;</span></h3></div>

        </div>


    </div>

</asp:Content>

