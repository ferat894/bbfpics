<%@ Page Language="VB" AutoEventWireup="false" CodeFile="home.aspx.vb" Inherits="home" %>

<%@ Register Src="~/ucFooter.ascx" TagPrefix="uc1" TagName="ucFooter" %>

<!DOCTYPE html>
<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <meta content="IE=edge" http-equiv="X-UA-Compatible" />
    <meta name="description" content="Fresh Beautiful flowers for all life's occasions, located in Clayton Victoria, serving Australia nationally." />
    <meta name="keywords" content="Busy Bee Florist, Florist in VIC, Florist in Clayton VIC, Florist in Melbourne, Wedding, Silk, Funeral, Gift, Plant, Flowers, Service, Quality, Arrangement, Posy, Bouque, Clayton Flowers" />

    <link rel="shortcut icon" href="/favicon.ico" type="image/x-icon" />
    <link rel="icon" href="/favicon.ico" type="image/x-icon" />
    <title>Fresh Beautiful Flowers at Busy Bee Florist</title>
    <script src="Scripts/jquery-1.9.1.min.js" type="text/javascript"></script>
    <link href="css/bbf.css" rel="stylesheet" />
    <!--[if gte IE 9]>
      <style type="text/css">
        .gradient {
           filter: none;
        }
      </style>
    <![endif]-->
    <!--[if lte IE 7]>
    <link href="/css/ie7.css" rel="stylesheet" />
    <![endif]-->    
    <!--[if lte IE 8]>
    <link href="/css/ie8.css" rel="stylesheet" />
    <![endif]-->    
    <style type="text/css">
        
        .cat-box-content img {
            height:150px;
            width:150px;

            -webkit-border-radius: 7px;
            -moz-border-radius: 7px;
            border-radius: 7px;
        }

        ul.our-services {
            margin:0;
            margin-left:10px;
            padding:0;
            padding-left:10px;
        }

        ul.our-services li { list-style-image:url(/images/li.png); margin-bottom:10px;}
        a.cat-link, a.cat-link:hover { text-decoration:none;}

    </style>


    <script type="text/javascript">
        // A $( document ).ready() block.
        $(document).ready(function () {
            $("div.cat-box-footer").each(function () {
                $(this).hover(
                      function () {
                          $(this).prev().css("background-color", "#FFE0E3");
                      }, function () {
                          $(this).prev().css("background-color", "");
                      }
                );
            });
        });
    </script>

    </head>
<body>


    <div id="page-wrapper">
        <div class="header" style="height:0px; margin-bottom:10px;">&nbsp;</div>
        <div style="min-height:884px">
            <div id="banner-image-container"><img src="/images/bbf-header.jpg" alt="Busy Bee Florist Logo" width="900" height="133" />
                <span id="header-address">
                    166 - 168 Clayton Road, Clayton Victoria 3168 <br />
                    <strong>Tel: +61 3 9543 8588</strong>&nbsp;Fax: +61 3 9543 8599
                </span>
            </div>
            <div>&nbsp;</div>
            <div>


                <div class="header">
                    <h1>Welcome to Busy Bee Florist</h1>
                </div>
                
                <div class="content-holder">
                    <div>&nbsp;</div>
                    <div id="xxx1">
                        <div>
                            <div style="float: left; margin-right:21px;">
                                <div class="cat-box">
                                    <a href="/romance" class="cat-link">
                                        <div class="cat-box-content">
                                            <img src="/images/flowers/category/cat-romance.jpg" alt="ROMANCE" />
                                        </div>
                                        <div class="cat-box-footer">ROMANCE</div>
                                    </a>
                                    
                                </div>
                            </div>



                            <div style="float: left; margin-right:21px;">
                                <div class="cat-box">
                                    <a href="/birthday" class="cat-link">
                                        <div class="cat-box-content">
                                            <img src="/images/flowers/category/cat-birthday.jpg" alt="BIRTHDAY" />
                                        </div>
                                        <div class="cat-box-footer">BIRTHDAY</div>
                                    </a>
                                    
                                </div>
                            </div>


                            <div style="float: left; margin-right:21px">
                                <div class="cat-box">
                                    <a href="/newbaby" class="cat-link">
                                        <div class="cat-box-content">
                                            <img src="/images/flowers/category/cat-new-baby.jpg" alt="NEW BABY" />
                                        </div>
                                        <div class="cat-box-footer">NEW BABY</div>
                                    </a>
                                </div>
                            </div>

                            <div style="float: right;">
                                <div class="cat-box">
                                    <a href="/special" class="cat-link">
                                        <div class="cat-box-content">
                                            <img src="/images/flowers/category/cat-special.jpg" alt="SPECIAL OCCASIONS" />
                                        </div>
                                        <div class="cat-box-footer">SPECIAL OCCASIONS</div>
                                    </a>
                                </div>
                            </div>



                        </div>
                        <div class="clear">&nbsp;</div>
                        <div>

                            <div style="float: left; margin-right: 21px; ">
                                <div class="cat-box">
                                    <a href="/justbecause" class="cat-link">
                                        <div class="cat-box-content">
                                            <img src="/images/flowers/category/cat-just-because.jpg" alt="JUST BECAUSE" />
                                        </div>
                                        <div class="cat-box-footer">JUST BECAUSE</div>
                                    </a>
                                </div>
                            </div>

                            <div style="float: left; margin-right: 21px; ">
                                <div class="cat-box">
                                    <a href="/getwell" class="cat-link">
                                        <div class="cat-box-content">
                                            <img src="/images/flowers/category/cat-get-well.jpg" alt="GET WELL" />
                                        </div>
                                        <div class="cat-box-footer">GET WELL</div>
                                    </a>
                                </div>
                            </div>

                            <div style="float: left; margin-right: 21px; ">
                                <div class="cat-box">
                                    <a href="/sympathy" class="cat-link">
                                        <div class="cat-box-content">
                                            <img src="/images/flowers/category/cat-sympathy.jpg" alt="SYMPATHY" />
                                        </div>
                                        <div class="cat-box-footer">SYMPATHY</div>
                                    </a>
                                    
                                </div>
                            </div>

                            <div style="float:left;height:150px; position:relative; width:150px;">
                                <img src="/images/member_of1.jpg"  style="position:absolute; bottom:-20px; left:0px;" alt="" />&nbsp;
                            </div>

                        </div>
                    </div>

                    <div class="clear">&nbsp;</div>
                    
                    <div class="clear">&nbsp;</div>
                    <div style="margin:0 auto; width:700px; text-align:center;"><img src="/images/divider700.png" alt="" /></div>
                    
                    <div class="clear">&nbsp;</div>
                    <div>

                        <div style="float:left; width:45%">
                                <div style="padding:10px">
                                    <h2 class="color-purple" style="border-bottom: 2px solid #ae75aa; margin-bottom: 20px; padding-bottom: 5px; width: 100%">About Us</h2>
                                    <p style="text-align:justify">We are a small, family florist who want our customers to keep coming back. So we sell only the highest quality and freshest flowers. We know you are sending flowers to someone special and they deserve the best. We also know that if our flowers are fresh and last a long time so will our good relationship with our customers. We take great care of the flowers up to the time we deliver them to you and then we provide advice on how to keep those flowers looking fresh for at least a week. </p>
                                </div>

                        </div>
                        <div style="float:right; width:45%">
                            <div style="padding:10px">
                                <h2 class="color-purple" style="border-bottom: 2px solid #ae75aa; margin-bottom: 20px; padding-bottom: 5px; width: 80%; ">Our Services</h2>
                                <ul class="our-services">
                                    <li>Open 7 days until late</li>
                                    <li>Quality fresh cut flowers &amp; arrangements for all occasions</li>
                                    <li>Deliveries to Melbourne metro areas</li>
                                    <li>Large selection of silk flowers, pots, vases &amp; teddy bears</li>
                                    <li>EFTPOS &amp; major credit cards accepted</li>
                                </ul>
                                
                            </div>
                        </div>

                    </div>
                    
                    <div class="clear">&nbsp;</div>
                    <div>&nbsp;</div>
                    <div>&nbsp;</div>

                </div>
            </div>

        </div>
        <div>&nbsp;</div>

        <uc1:ucFooter runat="server" ID="ucFooter" />
    </div>


</body>
</html>