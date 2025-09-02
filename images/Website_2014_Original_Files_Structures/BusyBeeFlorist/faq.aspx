<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="faq.aspx.vb" Inherits="faq" %>

<asp:Content ContentPlaceHolderID="head" Runat="Server">
    <link href="/css/glyphs.css" rel="stylesheet" />
    <style type="text/css">

        div.faq-item {
            margin-bottom:20px;
        }

        div.faq-item h3 {
            width:100%;
            padding:10px 0px 10px 0px;
            text-indent:10px;
            background-color: pink;
            cursor:pointer;

            -webkit-border-radius: 5px 20px 5px;
            -moz-border-radius: 5px 20px 5px;
            border-radius: 5px 20px 5px;
        }

            div.faq-item h3:hover {
                background-color: orange;
            }

        div.faq-item div.faq-section {
            display: none;
            padding:10px;
        }


        h4.div-question{
            font-weight:bold; background:url(/images/li.png) no-repeat 1px 25%; text-indent:20px;
        }
        div.div-answer  {
            padding-left:40px;
            width:90%;
            margin-bottom:25px;
        }



    </style>
    <script type="text/javascript">
        $(document).ready(function () {

            $("h3.faq-title").each(function () {
                $(this).click(function () {
                    $(this).next("div.faq-section").slideToggle();
                })
            })

            var hash = window.location.hash;
            if (hash !== "") {
                hash = hash.replace("#", "");

                $("div.faq-section").each(function () {
                    var faqSection = $(this);
                    
                    $(faqSection).find("a").each(function (index, el) {
                        if (el.name == hash) {
                            $(faqSection).show();

                            $('html, body').animate({
                                scrollTop: $(faqSection).offset().top - 50
                            }, 750);

                            return false;
                        }

                    })
                });


            }
            
        });

    </script>
</asp:Content>
<asp:Content ContentPlaceHolderID="content" Runat="Server">

    <div class="header">
        <h1>FAQ (Frequently Asked Questions)</h1>
    </div>
    <div class="content-holder" style="padding-left:20px; padding-right:20px;">
        <div>
            <p>Got a question? Please have a look through our frequently asked questions about payment, delivery, flowers, optional extras and anything else that is important.</p>
            <p>If you can't find what you're looking for, please don't hesitate to <a href="/contact">contact us</a> for further information. </p>
        </div>

        <div class="faq-item">
            
            <h3 class="faq-title"><span class="glyphicon glyphicon-shopping-cart"></span> Delivery / Orders</h3>
            <div class="faq-section">
                <a name="deliveries"></a>

                <div style="background:url(/images/delivery.png) no-repeat 0 5px; padding-left:40px; height:50px; line-height:45px; margin-bottom:10px; margin-top:10px;">
                    Our delivery service is available during <u>weekdays in business hours</u> only for the <u>Melbourne Metro area</u>.
                </div>

                <h4 class="div-question">How long does delivery take?</h4>
                <div class="div-answer">
                    <ul>
                        <li>Orders must be received by 8PM for next business day AM delivery.</li>
                        <li>Orders must be received by 1PM for the same business day delivery.</li>
                    </ul>
                    <p>Please note that although we strive for next day delivery, uncontrollable factors may influence the time taken for delivery to reach their destination.</p>


                </div>
                <h4 class="div-question">Can I get flowers delivered on a Weekend?</h4>
                <div class="div-answer">
                    <p>Weekend delivery services only available to limited area. Please <a href="/contact">contact us</a>  for more information.</p>
                </div>

                <h4 class="div-question">What happens to incorrect delivery address?</h4>
                <div class="div-answer">
                    <p>Should the incorrect delivery address be supplied, we will use all means available to find the correct details. It is the obligation of the customer to supply accurate details at the time of order. Re-deliveries may incur extra charge, and responsibility cannot be taken for goods left at the incorrect address.</p>
                </div>

                <h4 class="div-question">What if the Recipient is not home?</h4>
                <div class="div-answer">
                    <p><p>Should the recipient not be home when the delivery is made, discretion is left to the driver to leave flowers in a safe and protected place.</p>
                </div>

                <h4 class="div-question">Will my flowers be delivered as pictured?</h4>
                <div class="div-answer">
                    <p>The images of flowers to order, presented on this website are subject to seasonal availability and we will endeavour to fulfil your order as requested. Substitutions may be necessary. If your requirements are quite specific, you may wish to <a href="/contact">contact us</a> directly.</p>
                </div>

                <a name="refunds"></a>
                <h4 class="div-question">What if I want to cancel or alter my order?</h4>
                <div class="div-answer">
                    <p>Cancellations <b>must</b> be done by phone 24 hours prior to delivery. Alteration 24 hours prior to delivery <b>must</b> be made by phone.</p>
                    
                </div>

                <%--<h4 class="div-question"></h4>
                <div class="div-answer">
                    <p></p>
                </div>--%>

            </div>
        </div>


        <div class="faq-item">
            <h3 class="faq-title"><span class="glyphicon glyphicon-credit-card"></span> Payment</h3>
            <div class="faq-section">
                <h4 class="div-question">What payment method do you accept?</h4>
                <div class="div-answer">
                    <p>Credit Card and PayPal</p>
                </div>
            </div>
        </div>


        <div class="faq-item">
            <h3 class="faq-title"><span class="glyphicon glyphicon-leaf"></span> About the Flowers</h3>
            <div class="faq-section">
                <h4 class="div-question">Can I send an arrangement that is not pictured?</h4>
                <div class="div-answer">
                    <p>Yes, our florists are always available to help and advise you with your individual requests. Please <a href="/contact">contact us</a> for more details.</p>
                </div>
                <h4 class="div-question">Will my flowers be delivered as pictured?</h4>
                <div class="div-answer">
                    <p>The images of flowers to order, presented on this website are subject to seasonal availability and we will endeavour to fulfil your order as requested. Substitutions may be necessary. If your requirements are quite specific, you may wish to <a href="/contact">contact us</a> directly.</p>
                </div>
            </div>
        </div>


    </div>

</asp:Content>

