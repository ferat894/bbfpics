<%@ Page Title="" Language="VB" MasterPageFile="~/admin/admin.master" AutoEventWireup="false" CodeFile="login.aspx.vb" Inherits="admin_login" %>

<asp:Content  ContentPlaceHolderID="head" Runat="Server">

    <style type="text/css">

        /* Set widths on the form inputs since otherwise they're 100% wide */
        input,
        select,
        textarea {
            max-width: 280px;
        }

    </style>
</asp:Content>
<asp:Content  ContentPlaceHolderID="content" Runat="Server">
    <h2 style="margin-bottom:50px; height:20px; line-height:20px;"><span class="glyphicon glyphicon-log-in"></span> Login </h2>


    <asp:Literal EnableViewState="false"  runat="server" ID="l" />

    <div class="row">
        <div class="col-md-8">
            <section id="loginForm">
                 <div class="form-horizontal">
                        <h4>Please enter your username and password.</h4>
                        <hr />
                        <p class="text-danger">
                            <asp:Literal ID="FailureText" runat="server"></asp:Literal>
                        </p>

                        <div class="form-group">
                            <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName"  CssClass="col-md-2 control-label">Username:</asp:Label>
                            <div class="col-md-10">
                                <asp:TextBox ID="UserName" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" CssClass="text-danger" ErrorMessage="User Name is required." ValidationGroup="LoginUserValidationGroup" />
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password" CssClass="col-md-2 control-label">Password:</asp:Label>
                            <div class="col-md-10">
                                <asp:TextBox ID="Password" runat="server" CssClass="form-control" TextMode="Password" MaxLength="20"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" CssClass="text-danger" ErrorMessage="Password is required." ValidationGroup="LoginUserValidationGroup" />
                            </div>
                        </div>
<%--                        <div class="form-group">
                            <div class="col-md-offset-2 col-md-10">
                                <div class="checkbox">
                                    <asp:CheckBox ID="RememberMe" runat="server"/>
                                    <asp:Label ID="RememberMeLabel" runat="server" AssociatedControlID="RememberMe" CssClass="inline">Keep me logged in</asp:Label>
                                </div>
                            </div>
                        </div>--%>
                
                        <div class="form-group">
                            <div class="col-md-offset-2 col-md-10">
                                <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Log In" ValidationGroup="LoginUserValidationGroup" CssClass="btn btn-default"  Width="150px"  />
                            </div>
                        </div>
                </div>


                </section>
        </div>

        <div class="col-md-4">
            <h4>&nbsp;</h4>
            <hr />
            <div>
                <p>Please fill out the details to login.</p>
            </div>
        </div>

    </div>

</asp:Content>

