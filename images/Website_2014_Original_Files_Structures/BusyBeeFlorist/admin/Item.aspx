<%@ Page Title="" Language="VB" MasterPageFile="~/admin/admin.master" AutoEventWireup="false" CodeFile="Item.aspx.vb" Inherits="admin_Item" %>

<asp:Content  ContentPlaceHolderID="head" runat="Server">


    <style type="text/css">
        .col-lg-9 {
            padding-right:50px;
        }

        .category-container {
            height: 150px;
            overflow: auto;
        }
        .category-container table td {
            padding-right:15px;
            font-weight:normal;
        }

        .category-container table label {
            padding-left:5px;
            font-weight:normal;
        }
    </style>

    <script type="text/javascript">

        var popup;
        function openPopup(targetId){

            popup = window.open('explorer/?targetId=' + targetId + '&for=flower&catid=1', '1402904788184', 'width=885,height=600,toolbar=0,menubar=0,location=0,status=0,scrollbars=1,resizable=1,left=0,top=0');
            popup.focus();
            return false;
        }

        function auto(el) {
            autoComplete(el, document.getElementById('bs'), 'text', false)
        }



        function updateImage(name) {
            document.getElementById("<%=imgLarge.ClientID()%>").src = "/images/flowers/" + name
            document.getElementById("<%=imgThumb.ClientID()%>").src = "/images/flowers/tn/" + name
        }
        
    </script>
    
    <script type="text/javascript" src="/Scripts/admin/autocomplete_simple.js"></script>
</asp:Content>
<asp:Content ContentPlaceHolderID="content" runat="Server">

   <h2><span class="glyphicons flower" style="padding-bottom:30px;"></span>Flower</h2>
    <hr />
    <div>
        <asp:Panel runat="server" ID="panDisabled" Visible="false" >
            <div class="alert alert-dismissable alert-info">
              <button type="button" class="close" data-dismiss="alert">×</button>
              <strong>Attention</strong> This item is disabled and is not visible to the clients. To update this item, please enable it first.
            </div>
        </asp:Panel>
        <asp:Literal EnableViewState="false" ID="l" runat="server" />


    </div>

    <div class="row">

    <div class="col-lg-6" >



     <div class="well bs-component">

    <div class="form-horizontal">
        <fieldset>
            <legend>Flower</legend>
            <div class="form-group">
                <label class="col-lg-3 control-label" for="tName">Name</label>
                <div class="col-lg-9">
                    <asp:TextBox runat="server" ID="tName" MaxLength="50" CssClass="form-control" placeholder="flower name" /> 
                    <asp:RequiredFieldValidator ErrorMessage="Name is required" ControlToValidate="tName" runat="server" Text="*"  />
                </div>
            </div>


            <div class="form-group">
                <label class="col-lg-3 control-label" for="tDescription">Description</label>
                <div class="col-lg-9">
                    <asp:TextBox runat="server" ID="tDescription" MaxLength="500" CssClass="form-control" placeholder="About the flower" />
                    <asp:RequiredFieldValidator ErrorMessage="Desciption is required" ControlToValidate="tDescription" runat="server"  Text="*"  />
                </div>
            </div>


          
      <div class="form-group">    
          <label class="col-lg-3 control-label" for="select">Category</label>
          <div class="col-lg-9">
              <div class="form-control category-container">
                <asp:CheckBoxList RepeatLayout="Table" BorderStyle="None"  RepeatColumns="2"  ID="ddlCategory" DataTextField="CategoryName" DataValueField="CategoryId" runat="server"></asp:CheckBoxList>
              </div>
          </div>
     
      </div>
            

            <div class="form-group">
                <label class="col-lg-3 control-label" for="tPriceTitle1">Price 1 Title</label>
                <div class="col-lg-9">
                    <asp:TextBox runat="server" ID="tPriceTitle1" MaxLength="50" CssClass="form-control" placeholder="(optional)" onkeyup="auto(this);" autocomplete="off"  />

                </div>
            </div>

            <div class="form-group">
                <label class="col-lg-3 control-label" for="tPrice1">Price 1</label>

                <div class="col-lg-9">
                    <div class="input-group">
                        <span class="input-group-addon">$</span>
                        <asp:TextBox runat="server" ID="tPrice1" MaxLength="10" CssClass="form-control" placeholder="59.65" />
                    </div>
                    <asp:RequiredFieldValidator ErrorMessage="Price is required" ControlToValidate="tPrice1" runat="server"  Text="*"  />
                </div>
            </div>


            <div class="form-group">
                <label class="col-lg-3 control-label" for="tPriceTitle1">Price 2 Title</label>
                <div class="col-lg-9">
                    <asp:TextBox runat="server" ID="tPriceTitle2" MaxLength="50" CssClass="form-control" placeholder="(optional)" onkeyup="auto(this);" autocomplete="off" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-lg-3 control-label" for="tPrice2">Price 2</label>
                <div class="col-lg-9">
                    <div class="input-group">
                        <span class="input-group-addon">$</span><asp:TextBox runat="server" ID="tPrice2" MaxLength="10" CssClass="form-control" placeholder="0.00" />
                    </div>
                </div>
            </div>

            <div class="form-group">
                <label class="col-lg-3 control-label" for="tPrice2">Image</label>
                <div class="col-lg-9">
                    <div class="input-group">
                        <asp:TextBox runat="server" ID="tImageName" MaxLength="300" CssClass="form-control"  />
                        <span class="input-group-btn">
                            <a onclick="javascript:openPopup('<%=tImageName.ClientID()%>');" class="btn btn-default"> ...</a>
                        </span>
                    </div>
                </div>
            </div>

            <div class="form-group">
                <div class="col-lg-9 col-lg-offset-3">
                    <button class="btn btn-default" onclick="window.location='/admin/'; return false;">Cancel</button>
                    <asp:Button Text=" Submit " ID="cSubmit" runat="server" CssClass="btn btn-primary" />
                    &nbsp; <asp:Button Text=" Disable Item " runat="server" ID="cDisableItem" Visible="false" CssClass="btn btn-warning" OnClientClick="if(!confirm('You are about to disable this item, it will no longer be visible to clients.')){return false};" />
                      <asp:Button Text=" Enable Item " runat="server" ID="cEnableItem" Visible="false" CssClass="btn btn-primary" />
                </div>
            </div>



        </fieldset>
    </div>

         </div>



    </div>


        <div class="col-lg-6">



            <div class="form-horizontal">

                <asp:Image ImageUrl="~/" runat="server" ID="imgLarge" />
                <hr />
                <asp:Image ImageUrl="~/" runat="server" ID="imgThumb" />

            </div>
        </div>

    </div>

    <select name="bs" id="bs" style="display:none">
        <script src="/Scripts/admin/priceTitles.js" type="text/javascript"></script>
    </select> 


</asp:Content>

