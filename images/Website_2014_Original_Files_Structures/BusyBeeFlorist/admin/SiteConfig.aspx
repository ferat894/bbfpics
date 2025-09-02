<%@ Page Title="" Language="VB" MasterPageFile="~/admin/admin.master" AutoEventWireup="false" CodeFile="SiteConfig.aspx.vb" Inherits="admin_SiteConfig" %>

<asp:Content ContentPlaceHolderID="head" Runat="Server">

    <style type="text/css">

        hr.hr {border-bottom:1px dashed black; width:75%; margin-bottom:30px;}

    </style>

</asp:Content>
<asp:Content ContentPlaceHolderID="content" Runat="Server">
    <h2><span class="glyphicon glyphicon-cog"></span> Site Configuration</h2>
     <hr />
    <asp:Literal  runat="server" EnableViewState="false" id="l"  />

    <div class="well bs-component">
        <div class="form-horizontal">
            <fieldset>
                <legend>Orders</legend>
                <div class="form-group" >
                    <div class="col-lg-10 col-lg-offset-1">
                        <asp:CheckBox ID="chkOrderValidation" runat="server" Text="Allow Order Submission Validation"   />
                        <span class="help-block">When allowed, will use a hashing algorithm to verify the order submission against duplicate submissions for a period of time.</span>
                    </div>
                    <div class="col-lg-1">&nbsp;</div>
                     
                </div>
                
                <hr class="hr" />
                
                <div class="form-group">
                    <div class="col-lg-10 col-lg-offset-1" >
                        <asp:CheckBox ID="chkOrderSendEmail" runat="server" Text="Receive Order Form via Email"  />
                        <asp:TextBox runat="server" CssClass="form-control" ID="tEmail" MaxLength="200" />
                        <asp:RequiredFieldValidator ID="r1" runat="server" ControlToValidate="tEmail" ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        <span class="help-block">When enabled, will send an email to the address specified upon order submission. This email will include the sender&#39;s email address.</span>
                    </div>
                    <div class="col-lg-1">&nbsp;</div>
                </div>

                <hr class="hr" />
                
                <div class="form-group">
                    <div class="col-lg-10 col-lg-offset-1" >
                        <asp:CheckBox ID="chkCCValidation" runat="server" Text="Validate Credit Card Details"  />
                        <span class="help-block">Performs server side validation for credit card input on the Expiry Date and Credit Card number using Luhn Algorithm and Mod 10 techniques.<br /> 
                                        This does not ensure that the card details are correct/valid, it only ensures they are in the correct format. Ie: contains the correct number of digits etc. 
                        </span>
                    </div>
                    <div class="col-lg-1">&nbsp;</div>
                </div>
                
                <hr class="hr" />

                <div class="form-group">
                    <div class="col-lg-10 col-lg-offset-1">
                        <asp:Button Text="Update Configuration" runat="server" ID="cUpdateConfig" CssClass="btn bg-primary" />
                    </div>
                    <div class="col-lg-1">&nbsp;</div>
                </div>



            </fieldset>
        </div>
    </div>
</asp:Content>

