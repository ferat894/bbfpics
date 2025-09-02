<%@ Page Language="VB" ClientIDMode="Static" %>

<%@ Register TagPrefix="mrk" Assembly="mrk.Web.UI.ImageExplorer" Namespace="mrk.Web.UI.ImageExplorer" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta http-equiv="content-type" content="text/html; charset=utf-8" />
	<title>Image eXplorer</title>
    <style type="text/css">

        
            
    </style>
</head>

<body>
    <form id="form1" runat="server">
        <mrk:mrkImageExplorer runat="server" ID="EXPLORER" />
   </form>
</body>
</html>
