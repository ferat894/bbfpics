<%@ Page Title="Contact Us at Busy Bee Florist" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="contact.aspx.vb" Inherits="contact" %>

<%@ Register assembly="mrk.Web.Controls.NoBot" namespace="mrk.Web.Controls" tagprefix="cc1" %>

<asp:Content ContentPlaceHolderID="head" Runat="Server">
    
    <link href="css/bootstrap.css" rel="stylesheet" />
</asp:Content>
<asp:Content ContentPlaceHolderID="content" Runat="Server">

    <form id="form1" runat="server">

    <div class="header">
        <h1><span class="glyphicon glyphicon-home"></span> CONTACT US</h1>
    </div>
    <div class="content-holder"  style="padding-left:20px; padding-right:20px; padding-top:30px;">

        <asp:Literal EnableViewState="false" ID="l" runat="server" />

        <div>
            <p>Please use the form below to send us an email, or feel free to pop in and say hello at our Clayton store at: <strong>166-168 Clayton Road, Clayton Victoria 3168</strong>.</p>
            <div>&nbsp;</div>
        </div>
        
        <div style="float:left; width: 47%;">
            
                    <h3 class="color-purple">Contact Us</h3>
                    <hr />
                    <div class="form-group">
                        <label class="col-md-4 control-label">Your Name</label>
                        <div class="col-md-8">
                            <asp:TextBox runat="server" CssClass="form-control" id="txtName" MaxLength="20" placeholder="Your Name" />
                            <asp:RequiredFieldValidator ErrorMessage="Please enter your name" ControlToValidate="txtName" runat="server" CssClass="text-danger" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-4 control-label">Your Email</label>
                        <div class="col-md-8">
                            <asp:TextBox runat="server" CssClass="form-control" id="txtEmail" MaxLength="50" placeholder="Your Email" />
                            <asp:RequiredFieldValidator ErrorMessage="Please enter your email" ControlToValidate="txtEmail" runat="server" CssClass="text-danger" />
                            <asp:RegularExpressionValidator ID="re" runat="server" ErrorMessage="Invalid email format" ControlToValidate="txtEmail" ValidationExpression="\w+([-.]?\w?)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" CssClass="text-danger" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-4 control-label">Your Message</label>
                        <div class="col-md-8">
                            <asp:TextBox runat="server" ID="txtArea" TextMode="MultiLine" CssClass="form-control" />
                            <asp:RequiredFieldValidator ErrorMessage="Please enter a comment" ControlToValidate="txtArea" runat="server" CssClass="text-danger" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-4 control-label">Verify Code</label> 
                        <div class="input-group col-md-8">
                            <span class="input-group-addon"><cc1:NoBot ID="NoBot1" runat="server" /></span>
                            <asp:TextBox runat="server" CssClass="form-control" MaxLength="7" placeholder="The security code on your left" ID="tCode" Width="200px" />
                        </div>
                         <div class="row clear" style="text-indent:30px;" >
                            <asp:RequiredFieldValidator ErrorMessage="Please enter the code" ControlToValidate="tCode" runat="server" CssClass="text-danger" />

                        </div>
                    </div>



                    <div class="form-group">
                        <label class="col-md-4 control-label">&nbsp;</label>
                        <div class="col-md-8">
                            <input type="button" name="bCancel" value=" Clear " class="btn btn-default" />    
                            <asp:Button Text=" Send Email " runat="server" CssClass="btn btn-primary" id="cSubmit" />
                        </div>
                    </div>
        
        </div>

        <div style="float:right; width:47%;">
            
                    <h3 class="color-purple">Location</h3>
                    <hr />
                        <iframe width="425" height="350" frameborder="0" scrolling="no" marginheight="0" marginwidth="0" src="https://maps.google.com.au/maps?f=q&amp;source=s_q&amp;hl=en&amp;geocode=&amp;q=166-168+Clayton+Road,+Clayton,+Victoria&amp;aq=0&amp;oq=166-168+Clayto&amp;sll=-37.914701,145.12145&amp;sspn=0.00394,0.006968&amp;ie=UTF8&amp;hq=&amp;hnear=166%2F168+Clayton+Rd,+Clayton+Victoria+3168&amp;t=m&amp;z=14&amp;ll=-37.914714,145.122048&amp;output=embed"></iframe><br /><small><a href="https://maps.google.com.au/maps?f=q&amp;source=embed&amp;hl=en&amp;geocode=&amp;q=166-168+Clayton+Road,+Clayton,+Victoria&amp;aq=0&amp;oq=166-168+Clayto&amp;sll=-37.914701,145.12145&amp;sspn=0.00394,0.006968&amp;ie=UTF8&amp;hq=&amp;hnear=166%2F168+Clayton+Rd,+Clayton+Victoria+3168&amp;t=m&amp;z=14&amp;ll=-37.914714,145.122048" style="color:#0000FF;text-align:left" target="_blank">View Larger Map</a></small>

        </div>

        
        <div class="clear">&nbsp;</div>

    </div>

    </form>

</asp:Content>

