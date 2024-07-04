<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoadRpt.aspx.cs" Inherits="PerfectWebERP.Reports.LoadRpt" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"  Namespace="CrystalDecisions.Web" TagPrefix="CR" %>


<!DOCTYPE html>

<html>
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" HyperlinkTarget="_blank" AutoDataBind="true"  />
        </div>
    </form>
</body>
</html>
<script src="../Assets/js/jquery-2.1.4.min.js" type="text/javascript"></script>
<script type="text/javascript">
    
</script>

