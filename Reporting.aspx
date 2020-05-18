<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reporting.aspx.cs" Inherits="NCRFTRWeb.Reporting" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="C1.Web.UI.Controls.C1Report.3" Namespace="C1.Web.UI.Controls.C1Report"
    TagPrefix="cc1" %>
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
            
            <cc1:C1ReportViewer ID="C1ReportViewer1" runat="server" FileName="ResultReport.xml" 
            ReportName="Result Report">
            </cc1:C1ReportViewer>
    </div>
    </form>
</body>
</html>
