<%@ Page Title="" Language="VB" MasterPageFile="~/admin/admin.master" AutoEventWireup="false" CodeFile="ItemList.aspx.vb" Inherits="admin_ItemList" %>

<asp:Content ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">

        .item-row {

            width:100%; border-bottom:1px solid #333;
            cursor:pointer;
                           
        }
        .item-row:hover {
            background-color:#333;  
            filter: alpha(opacity=50);
            -moz-opacity: 0.5;
            -khtml-opacity: 0.5;
            opacity: 0.5;
	        filter: progid:DXImageTransform.Microsoft.Alpha(opacity=50);
	        /* above line works in IE6, IE7, and IE8 */
	        -ms-filter: "progid:DXImageTransform.Microsoft.Alpha(opacity=50)";

            color:#fff;
        }

        .item-row:hover img {
            background-color:#333;  

            filter: alpha(opacity=50);
            -moz-opacity: 0.5;
            -khtml-opacity: 0.5;
            opacity: 0.5;
	        filter: progid:DXImageTransform.Microsoft.Alpha(opacity=50);
	        /* above line works in IE6, IE7, and IE8 */
	        -ms-filter: "progid:DXImageTransform.Microsoft.Alpha(opacity=50)";
        }

        span.price {margin-right:10px; font-size:90%}

    </style>
</asp:Content>
<asp:Content ContentPlaceHolderID="content" Runat="Server">

    
    <%--<i class="glyphicon glyphicons-phone"></i>--%>


    <asp:Panel runat="server" ID="panelFlowers" Visible="false" >
    <h2><span class="glyphicon glyphicon-leaf"></span> Flowers</h2>
     <hr />
        <div class="row">


            <div class="form-group">

                <label for="select" class="col-lg-2 control-label">Category</label>
                <div class="col-lg-10">
                    <div class="input-group">
                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlCategory" DataTextField="CategoryName" DataValueField="CategoryId" AutoPostBack="true" />
                        <span class="input-group-btn">
                            <a class="btn btn-default btn-primary" href="item?addnew=1">NEW FLOWER</a>
                            
                        </span>
                    </div>
                </div>

            </div>


        </div>

        <hr />

        <div>

                <asp:Repeater runat="server" id="r">
                    <ItemTemplate>
                        <div class="item-row" onclick="window.location.assign('item?id=<%#Eval("ItemId")%>')">
                            <div style="float:left; width:150px;">
                                <img src="<%#RenderThumbNail(Eval("ItemImageName").ToString, Eval("CategoryName").ToString)%>" alt="<%#Eval("ItemName")%>" />
                            </div>
                            <div style="float:left;">
                                <b><a href="item?id=<%#Eval("ItemId")%>"><%#Eval("ItemName")%> : <%#Eval("ItemImageName").ToString.Replace (".jpg",String.Empty )%></a> <%#RenderOffDisabled(Eval("ItemDisabled"))%> </b>
                                <div><%#Eval("ItemDescription")%></div>
                                <div>&nbsp;</div>
                                <div><span class="price"><%#CDec(Eval("ItemPrice1")).ToString("c")%></span><span><%#Eval("ItemPriceTitle1")%></span></div>
                                <div><span class="price"><%#CDec(Eval("ItemPrice2")).ToString("c")%></span><span><%#Eval("ItemPriceTitle2")%></span></div>
                            </div>
                            <div style="clear:both;"></div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>

        </div>
    </asp:Panel>


    <asp:Panel runat="server" ID="panelExtras" Visible="false" >
        <h2><span class="glyphicon glyphicon-th-list"></span> Extra Items</h2>
         <hr />

        <div class="row">


            <div class="form-group">

                <label for="select" class="col-lg-2 control-label">Optional Extras</label>
                <div class="col-lg-10">
                    <div class="input-group11">
                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlExtras" DataTextField="Title" DataValueField="OptionalExtraId" AutoPostBack="true" />
                       <%-- <span class="input-group-btn">
                            <a class="btn btn-default btn-primary" href="OptionalItem?addnew=1">NEW ITEM</a>
                        </span>--%>
                    </div>
                </div>

            </div>


        </div>

        <hr />

        <div>

                <asp:Repeater runat="server" id="r1">
                    <ItemTemplate>
                        <div class="item-row" onclick="window.location.assign('optionalitem?id=<%#Eval("OptionalExtraId")%>')">
                            <div style="float:left; width:150px;">
                                <img src="/images/flowers/extras/<%#Eval("ImageName").ToString%>" alt="<%#Eval("Title")%>" />
                            </div>
                            <div style="float:left;">
                                <b><a href="optionalitem?id=<%#Eval("OptionalExtraId")%>"><%#Eval("Title")%></a></b> <%#RenderOffDisabled(Eval("Disabled"))%>
                                <div><%#Eval("Description")%></div>
                                <div>&nbsp;</div>
                                <div><span class="price"><%#CDec(Eval("Price1")).ToString("c")%></span>&nbsp;<span><%#Eval("PriceTitle1")%></span></div>
                                <div><span class="price"><%#CDec(Eval("Price2")).ToString("c")%></span>&nbsp;<span><%#Eval("PriceTitle2")%></span></div>
                            </div>
                            <div style="clear:both;"></div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>





        </div>

    </asp:Panel>

</asp:Content>

