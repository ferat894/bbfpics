<%@ Page Title="Online Orders at Busy Bee Florist" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="order.aspx.vb" Inherits="order" %>

<asp:Content  ContentPlaceHolderID="head" Runat="Server">
    <link href="/css/bootstrap.css" rel="stylesheet" />
    <link href="/css/order.css" rel="stylesheet" />
    <script type="text/javascript" src="/Scripts/jquery.validate.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#form-order").validate();

            $("#rDeliveryInstructions").change(function () {
                if (document.getElementById("rDeliveryInstructions").value  == "fillform") {
                    fill()
                }
            })
        });
    </script>

</asp:Content>
<asp:Content  ContentPlaceHolderID="content" Runat="Server">
    

    <asp:Panel runat="server" ID="panel_order" ClientIDMode="Static">


                <div class="header">
                    <h1>ORDER FORM</h1>
                </div>
        
                <div class="content-holder">

                    <div style="width:100%;">

                        <div style="float:left; width:27%">
                            <asp:Image ImageUrl="~/" runat="server" ID="imgLarge" Width="250px" Height="250px" /></div>
                        <div style="float: right; width: 70%">
                            <div>
                                <div style="float:left; width:50%">
                                    <b><asp:Literal ID="lbName" runat="server" /></b> : <asp:Literal ID="lbPrice" runat="server" />
                                </div>
                                <div style="float:left; width:50%">
                                    <strong>Item Code: </strong> <asp:Literal  runat="server" id="lbItemCode" />
                                </div>
                                <div class="clear clearfix"></div>
                            </div>
                            <p><asp:Literal ID="lbDescription" runat="server" /></p>
                            

                            <div>
                                <b>Extra Items</b><br />
                                <asp:Repeater runat="server" ID="r">
                                    <ItemTemplate>
                                        <div style="float:left">
                                            <div style="float:left; margin-right:50px; text-align:center; font-size:90%;">
                                                <%#Eval("UnitQtyLabel")%> <%#Eval("PriceTitleLabel")%> <%#Eval("Title")%><br />
                                                <%#CDec(Eval("UnitPrice")).ToString("c")%> each<br />
                                                <div style="border-bottom:3px double black; border-top:1px solid black; margin-bottom:5px;">
                                                    <%#CDec(Eval("SubTotalPrice")).ToString("c")%>
                                                </div>
                                                <img src="/images/flowers/extras/<%#Eval("ImageName")%>" alt="" />
                                                
                                                
                                            </div>
                                        </div>
                                        
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:Literal runat="server" id="lbNoExtras" />
                                <div class="clear"></div>
                            </div>
                            
                        </div>
                        <div class="clear"></div>
                    </div>


                    <div>&nbsp;</div>
                    <div style="width:100%; height:6px; background-color:pink;">&nbsp;</div>
                    <div>&nbsp;</div>
                    <div>&nbsp;</div>

                    <form action="/order?summary=1" method="post" id="form-order">
                        <asp:Literal id="l" runat="server" />

                    
                        <div class="row" style="width:90%; margin:0 auto;">

                           

                            <div style="float:left; width:45%; margin-right:20px; margin-left:20px;">

                                <h3 class="color-purple">Sender's Details</h3>
                                <hr />

                                <div class="form-group">

                                    <label class="col-md-4 control-label">Name</label>

                                    <div class="col-md-8">
                                        <input type="text" name="sName" class="form-control" maxlength="30" placeholder="Your Name"
                                            data-rule-required="true" data-msg-required="Please enter your name" />
                                    </div>
                                </div>

                                <div class="form-group">

                                    <label class="col-md-4 control-label">Email</label>

                                    <div class="col-md-8">
                                        <input type="text" name="sEmail" class="form-control" maxlength="200" placeholder="Your Email Address" 
                                            data-rule-required="true" data-rule-email="true" data-msg-required="Please enter your email address" 
                                            data-msg-email="Please enter a valid email address" />
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-md-4 control-label">Contact Number</label>
                                    <div class="col-md-8">
                                        <input type="text" name="sPhone" class="form-control"  maxlength="10" placeholder="Your Contact Number"
                                             data-rule-required="true" data-msg-required="Please enter your contact number" />
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-md-4 control-label">Address</label>
                                    <div class="col-md-8">
                                        <input type="text" name="sAddress" class="form-control" maxlength="100" placeholder="Your Address" />
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-md-4 control-label">Suburb</label>
                                    <div class="col-md-8">
                                        <input type="text" name="sCity" class="form-control" maxlength="50" placeholder="Your Suburb" />
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-md-4 control-label">Postcode</label>
                                    <div class="col-md-8">
                                        <input type="text" name="sPostcode" class="form-control" maxlength="10" placeholder="Your Postcode" />
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-md-4 control-label">State</label>
                                    <div class="col-md-8">
                                        <input type="text" name="sState" class="form-control" maxlength="50" placeholder=" Your State" />
                                    </div>
                                </div>



                            </div>



                            <div style="float: left; width: 45%">
                                <h3 class="color-purple">Recipient's Details</h3>
                                <hr />


                                <div class="form-group">

                                    <label class="col-md-4 control-label">Name</label>

                                    <div class="col-md-8">
                                        <input type="text" name="rName" class="form-control" maxlength="30" placeholder="Recipient's Name"
                                             data-rule-required="true" data-msg-required="Please enter the recipient's name" />
                                    </div>
                                </div>


                                <div class="form-group">

                                    <label class="col-md-4 control-label">Company Name</label>

                                    <div class="col-md-8">
                                        <input type="text" name="rCompanyName" class="form-control" maxlength="100" placeholder="Recipient's Company Name" />
                                    </div>
                                </div>
                          


                                <div class="form-group">

                                    <label class="col-md-4 control-label">Email</label>

                                    <div class="col-md-8">
                                        <input type="text" name="rEmail" class="form-control" maxlength="200" placeholder="Recipient's Email Address"
                                            data-rule-email="true" data-msg-email="Please enter a valid email address" />
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-md-4 control-label">Contact Number</label>
                                    <div class="col-md-8">
                                        <input type="text" name="rPhone" class="form-control"  maxlength="10" placeholder="Recipient's Contact Number" 
                                             data-rule-required="true" data-msg-required="Recipient's contact number required" />
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-md-4 control-label">Address</label>
                                    <div class="col-md-8">
                                        <input type="text" name="rAddress" class="form-control" maxlength="100" placeholder="Recipient's Address" 
                                             data-rule-required="true" data-msg-required="Recipient's address required" />
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-md-4 control-label">Suburb</label>
                                    <div class="col-md-8">
                                        <input type="text" name="rCity" class="form-control" maxlength="50" placeholder="Recipient's Suburb" 
                                             data-rule-required="true" data-msg-required="Please enter the recipient's suburb" />
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-md-4 control-label">Postcode</label>
                                    <div class="col-md-8">
                                        <input type="text" name="rPostcode" class="form-control" maxlength="10" placeholder="Recipient's Postcode"
                                              data-rule-required="true" data-msg-required="Recipient's postcode required" />
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-md-4 control-label">State</label>
                                    <div class="col-md-8">
                                        <input type="text" name="rState" class="form-control" maxlength="50" placeholder="Recipient's State"
                                             data-rule-required="true" data-msg-required="Recipient's state required"  />
                                    </div>
                                </div>


                                

                                <div class="form-group">
                                    <label class="col-md-4 control-label">Preferred Time</label>
                                    <div class="col-md-8">
                                        <select class="form-control select-fix" name="rPreferredTime">
                                            <option value=""> -- PREFERRED TIME --</option>
                                            <option value="AM">AM</option>
                                            <option value="PM">PM</option>
                                        </select>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-md-4 control-label">Delivery Instructions</label>
                                    <div class="col-md-8">
                                        <textarea class="form-control" rows="3" id="rDeliveryInstructions" name="rDeliveryInstructions" cols="12"></textarea>
                                        <span class="help-block">Tell us a little about how to deliver the flower. For example, leave it out the back.</span>

                                    </div>
                                </div>


                            </div>

                            <div class="clear"></div>
                            <div>&nbsp;</div>
                            <div>&nbsp;</div>
                            <div>&nbsp;</div>
                            <div style="float:left; width:45%; margin-right:20px; margin-left:20px;">


                                <h3 class="color-purple"><span class="glyphicon glyphicon-credit-card"></span>Payment Details</h3>
                                <hr />
                                <div class="form-group">

                                    <label class="col-md-4 control-label">Card Type</label>
                                    <div class="col-md-8">
                                        <select class="form-control select-fix" name="pCardType">
                                            <option value="Visa">Visa</option>
                                            <option value="MasterCard">MasterCard</option>
                                            <option value="Amex">American Express</option>
                                            <option value="Bank Card<">Bank Card</option>
                                            <option value="DinersClub">Diners Club</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-4 control-label">Card Number</label>
                                    <div class="col-md-8">
                                        <input type="text" name="pCardNumber" class="form-control" maxlength="20" placeholder="Card Number"
                                            data-rule-required="true" 
                                            data-msg-required="Please enter the Credit Card number"
                                            data-rule-creditcard="true"
                                            data-msg-creditcard="Invalid Credit Card numer"  />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-4 control-label">Card Expiry</label>
                                    <div class="col-md-8">
                                        <input type="number" name="pCardExpiry" class="form-control" maxlength="4" placeholder="MMYY"
                                             data-rule-required="true" data-msg-required="Please enter the expiry date"
                                            data-rule-number="true" data-msg-number="Please enter a number"
                                             />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-4 control-label">Cardholder's Name</label>
                                    <div class="col-md-8">
                                        <input type="text" name="pCardholderName" class="form-control" maxlength="30" placeholder="Name on the Card"
                                             data-rule-required="true" data-msg-required="Please enter the name on the card" />
                                    </div>
                                </div>

                            </div>

                            <div style="float: left; width: 45%">
                                <h3 class="color-purple">Total</h3>
                                <hr />
                                <div class="price-panel" style="margin-top:40px;">
                                    <table border="0" cellpadding="4" cellspacing="4" width="100%">
                                        <tr>
                                            <td><b>Flower: </b></td>
                                            <td><asp:Literal ID="lbFinalFlowerTotal" runat="server" /></td>
                                        </tr>
                                        <tr>
                                            <td><b>Extras:</b> </td>
                                            <td><asp:Literal ID="lbFinalExtrasTotal" runat="server" /></td>
                                        </tr>
                                        <tr>
                                            <td><b>Delivery Fee:</b> </td>
                                            <td><asp:Literal ID="lbDeliveryFee" runat="server" /></td>
                                        </tr>
                                        <tr>
                                            <td><b>Order Total:</b></td>
                                            <td style="border-bottom:3px double black;border-top:1px solid black;"><asp:Literal ID="lbFinalTotalPrice" runat="server" /></td>
                                        </tr>
                                    </table>
                                    <div class="clear"></div>
                                    <hr />
                                
                                    <div class="small-text bold arial" style="background:url(/images/delivery.png) no-repeat 0 5px; padding-left:40px;">
                                        <div style="line-height:17px;">
                                        Deliveries are available on weekdays in Melbourne Metro areas<br/>during business hours only. <a href="/faq#deliveries" target="_blank">(details)</a>.<br />All Prices are GST inclusive.
                                            </div>
                                        <div>&nbsp;</div>
                                        <div style="padding-top:5px;">
                                            <input type="checkbox" name="ckConfirm" id="ckConfirm"
                                                data-rule-required="true" 
                                                data-msg-required="Please indicate that you have read the above." />
                                            <label onclick="" for="ckConfirm" style="padding-left:8px;">I have read and understood the above.</label>
                                        </div>
                                    </div>
                                    <div style="padding:15px 0px 15px 0px; width:100%; text-align:center;">
                                        <input type="submit" name="bSubmit" value=" Send Order Now >> " class="btn btn-primary" style="width:100%;" />
                                    </div>
                                </div>

                                </div>


                            <div class="clear"></div>
                        </div>


                    </form>
                </div>
    



        </asp:Panel>
    
    <asp:Panel runat="server" ID="panel_result" Visible="false" ClientIDMode="Static" >

             <div class="header">
                    <h1>ORDER FORM</h1>
                </div>

                <div class="content-holder" style="text-align:center;">
                    <asp:Literal runat="server" ID="lbHtml" />

                    <div>&nbsp;</div>
                    <a href="." class="btn btn-primary" style="width:40%;" runat="server" id="bContinue"> Continue browsing... </a>
                    <a href="javascript:void(0);" onclick="window.history.go(-1);" class="btn btn-default" style="width:40%;" runat="server" id="bGoBack" visible="false"> Go Back &amp; Retry </a>

                </div>
                

    </asp:Panel>

</asp:Content>

