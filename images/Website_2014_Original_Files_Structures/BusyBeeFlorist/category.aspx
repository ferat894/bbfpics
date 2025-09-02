<%@ Page Title="Flowers for life's occasaions at Busy Bee Florist" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="category.aspx.vb" Inherits="category" %>

<asp:Content  ContentPlaceHolderID="head" Runat="Server">

    <link href="/css/buttons.css" rel="stylesheet" />

    <style type="text/css">

        div#loading {

            position:absolute;
            height:1070px; 
            width:100%;

            background: #333 url(/images/000000-0.75.png);
            z-index:125;

            
            filter: alpha(opacity=70);
            -moz-opacity: 0.7;
            -khtml-opacity: 0.7;
            opacity: 0.7;
	        filter: progid:DXImageTransform.Microsoft.Alpha(opacity=70);
	        /* above line works in IE6, IE7, and IE8 */
	        -ms-filter: "progid:DXImageTransform.Microsoft.Alpha(opacity=70)";
            
            /* CSS3 styling for latest browsers */
            -moz-box-shadow:0 0 90px 5px #000;
            -webkit-box-shadow: 0 0 90px #000;
    
	        -webkit-border-radius: 5px;
	           -moz-border-radius: 5px;
	                border-radius: 5px;    

            display:none;
            text-align:center;
        }

        div#loading div#loading-image {
            background-color: white;
            height: 50px;
            width: 250px;
            margin: 0 auto;
            text-align: center;
            margin-top: 200px;
            line-height:50px;

	        -webkit-border-radius: 5px;
	        -moz-border-radius: 5px;
	        border-radius: 5px;    

        }


    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            //should remove this from  mobile platform as it remembers 
            getHash();
        });
    </script>


</asp:Content>
<asp:Content  ContentPlaceHolderID="content" Runat="Server">

    
                <div class="header" style="height:0px;">&nbsp;</div>
                <div class="content-holder">
                    
                    <div>&nbsp;</div>
                    <div id="category-toolbar">
                        <ul>
                            <li class="<%=RenderCategorySelectedCss(1)%>"><a href="/romance"><img src="/images/flowers/category/toolbar/cat-romance.jpg" alt="" title="Romance" /><span>Romance</span></a></li>
                            <li class="<%=RenderCategorySelectedCss(2)%>"><a href="/birthday"><img src="/images/flowers/category/toolbar/cat-birthday.jpg" alt="" title="Birthday" /><span>Birthday</span></a></li>
                            <li class="<%=RenderCategorySelectedCss(3)%>"><a href="/newbaby"><img src="/images/flowers/category/toolbar/cat-new-baby.jpg" alt="" title="New Baby" /><span>New Baby</span></a></li>
                            <li class="<%=RenderCategorySelectedCss(5)%>"><a href="/justbecause"><img src="/images/flowers/category/toolbar/cat-just-because.jpg" alt="" title="Just Because" /><span>Just Because</span></a></li>
                            <li class="<%=RenderCategorySelectedCss(6)%>"><a href="/getwell"><img src="/images/flowers/category/toolbar/cat-get-well.jpg" alt="" title="Get Well" /><span>Get Well</span></a></li>
                            <li class="<%=RenderCategorySelectedCss(4)%>"><a href="/specialoccasions"><img src="/images/flowers/category/toolbar/cat-special.jpg" alt="" title="Special Occasions" /><span>Special Occasions</span></a></li>
                            <li class="<%=RenderCategorySelectedCss(7)%>"><a href="/sympathy"><img src="/images/flowers/category/toolbar/cat-sympathy.jpg" alt="" title="Sympathy" /><span>Sympathy</span></a></li>
                        </ul>
                        
                    </div>
                    <span class="clear">&nbsp;</span>

                    
                    
                    
                    <div style="width:100%; height:6px; background-color:pink;">&nbsp;</div>
                    <div>&nbsp;</div>
                    
                    <div id="category-section-title-container">
                        <h1 class="color-purple"><span><%:Me.CategoryName%></span></h1>
                        <div style="margin-top:10px;">
                        <iframe src="http://www.facebook.com/plugins/like.php?href=http%3A%2F%2Fwww.busybeeflorist.com.au%2F&width&layout=button_count&action=like&show_faces=true&share=true&height=21" scrolling="no" frameborder="0" class="fbFrame" allowtransparency="true" style="height: 21px;"></iframe>
                        </div>
                    </div>
                    
                    <div>&nbsp;</div>
                    


                    <div>
                        <div style="width:100%;">
                            <div style="float: left; width: 58%;">
                                
                                
                                    <div id="thumbnail-container">
                                        <asp:Repeater runat="server" id="r">
                                            <ItemTemplate>
                                                <div class="tn-box">
                                                    <a href="#<%#Eval("ItemId")%>" onclick="fetchItem(<%#Eval("ItemId")%>)" class="tn-link">
                                                        <div class="tn-box-content">
                                                            <img src="<%#RenderThumbNail(Eval("ItemImageName").ToString)%>" alt="<%#Eval("ItemName")%>" title="<%#Eval("ItemName")%>" />
                                                        </div>
                                                    </a>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>

                                    
                            </div>
                            <div style="float: right; vertical-align: top; width: 42%; ">

                                
                                <div style="height:1000px; min-height:1000px; position:relative; z-index:100;">
                                    <div id="loading">
                                        <div id="loading-image"><img src="/images/ajax.gif" alt="" />&nbsp;Loading...</div>
                                    </div>
                                    <div id="flower-item">
                                        <asp:Literal EnableViewState="false" ID="f" runat="server" />
                                    </div>
                                </div>



                            </div>

                            <div class="clear">&nbsp;</div>
                            

                        </div>
                        
                    </div>
                    
                </div>
    

</asp:Content>

