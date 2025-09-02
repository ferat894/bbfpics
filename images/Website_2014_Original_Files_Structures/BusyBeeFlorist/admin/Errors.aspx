<%@ Page Title="" Language="VB" MasterPageFile="~/admin/admin.master" AutoEventWireup="false" CodeFile="Errors.aspx.vb" Inherits="admin_Errors" %>

<asp:Content ContentPlaceHolderID="head" Runat="Server">

    <script type="text/javascript">

        //access iframe 
        $(function () {//document ready

            $("#iError").load(function () {

                
                //access the iframe doc
                var doc = document.getElementById('iError').contentWindow.document;
                doc.body.style.paddingLeft = "25px";
                doc.body.style.paddingRight = "25px";

                if (doc.getElementById("Footer") !== null){
                    doc.getElementById("Footer").style.display = "none";
                }
                if (doc.getElementById("ErrorDetail") !== null){
                    doc.getElementById("ErrorDetail").style.padding = 0;
                    doc.getElementById("ErrorDetail").style.fontSize = "100%";
                }

                var ul = doc.getElementById("SpeedList");
                if (ul !== null) {
                    var list = ul.getElementsByTagName("li");
                    for (var i = 0; i < list.length; i++) {
                        if (list[i].innerText !== "Download Log") { list[i].style.display = "none" }
                    }
                    doc.getElementById("SpeedList").style.marginBottom = "30px";
                }

                        
                var h = doc.body.scrollHeight + 100;
                $(this).css("height", h);


                

            });//.bind('load', function () {  });
            
        });

    </script>

</asp:Content>
<asp:Content ContentPlaceHolderID="content" Runat="Server">
    

    <h2><span class="fa fa-bug"></span> Errors</h2>
    <hr />

    <div class="col-lg-12">
        <iframe id="iError" src="/admin/elmah.axd" style="width:100%; height:600px;" frameborder="0" />
    </div>

    


</asp:Content>

