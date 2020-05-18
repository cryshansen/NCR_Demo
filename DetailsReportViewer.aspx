<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DetailsReportViewer.aspx.cs" Inherits="NCRFTRWeb.DetailsReportViewer" %>

<%@ Register Assembly="C1.Web.UI.Controls.C1Report.3" Namespace="C1.Web.UI.Controls.C1Report"
    TagPrefix="cc1" %>

<%@ Register assembly="C1.Web.C1WebReport.2" namespace="C1.Web.C1WebReport" tagprefix="cc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript">
    
      function pageLoad() {
      }
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <br />
        <br />
    <cc1:C1ReportViewer ID="C1ReportViewer1" runat="server" >
    <%--FileName="<%=this.xmlstring%>" ReportName="NCRReport">--%>
    </cc1:C1ReportViewer>
    </div>
    </form>
</body>
</html>
