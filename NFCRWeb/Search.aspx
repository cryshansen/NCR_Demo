<%@ Page Language="C#" MasterPageFile="~/PackersPlus.Master" AutoEventWireup="true"
    CodeBehind="Search.aspx.cs" Inherits="NCRFTRWeb.WebForm10" Title="Untitled Page" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="C1.Web.UI.Controls.3" Namespace="C1.Web.UI.Controls.C1GridView"
    TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        td
        {
            vertical-align: top;
        }
        #Notes
        {
            height: 32px;
        }
        #Notes0
        {
            height: 106px;
        }
        #DRExecSummary
        {
            height: 178px;
        }
        #DRDescription
        {
            height: 182px;
        }
        #DRObservations
        {
            height: 184px;
        }
        #Notes2
        {
            height: 66px;
        }
        #DRPrefaceTA
        {
            height: 55px;
        }
        #DRCauseAnalysisTA
        {
            height: 43px;
        }
    </style>

    <script type="text/javascript">
         var id = null;      
         function movePanel()      
         {           
             var pnl = $get("ResultsPanel");           
             if (pnl != null)           
             {                
                 /*pnl.style.left = "250px";                
                 pnl.style.top = "50px"; */               
                 id = setTimeout("movePanel();", 100);           
             }      
         }
         function stopMoving()      
         {           
            clearTimeout(id);      
         } 
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Label ID="MessageLBL" runat="server" Text="Label"></asp:Label>
    <asp:UpdatePanel ID="udp1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="SearchPanel" runat="server">
                <asp:Button ID="Search" runat="server" Height="20px" OnClick="Search_Click" Text="Search"
                    Width="258px" />
                <hr />
                <table>
                    <tr style="background-color: Menu;">
                        <td>
                            <strong>Customer:</strong>
                        </td>
                        <td>
                            <asp:DropDownList ID="CustomerDDL" runat="server" Width="126px">
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <strong>Operator:</strong>
                        </td>
                        <td>
                            <asp:DropDownList ID="OperatorDDL" runat="server" Width="126px">
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <strong>Engineer:</strong>
                        </td>
                        <td>
                            <asp:DropDownList ID="EngineerDDL" runat="server" Width="126px">
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            Location:
                        </td>
                        <td>
                            <asp:DropDownList ID="LocationDDL" runat="server" Width="126px">
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <br />
                <hr />
                <table>
                    <!--Outer Table starts here -->
                    <tr>
                        <td>
                            <table>
                                <!--First Inner table starts here 3 columns wide each -->
                                <tr>
                                    <td>
                                        Incident Type:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="SearchReportTypeDDL" runat="server" Height="17px" Width="138px">
                                            <asp:ListItem>NCR</asp:ListItem>
                                            <asp:ListItem>FTR</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Completed:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="SearchDateTB" runat="server" Enabled="true"></asp:TextBox>
                                    </td>
                                    <cc1:CalendarExtender ID="SearchCalExtender" TargetControlID="SearchDateTB" Format="yyyy/MM/dd"
                                        runat="server">
                                    </cc1:CalendarExtender>
                                </tr>
                                <tr>
                                    <td>
                                        Incident Title:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="DRtitle" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Ticket Number:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="DRfieldticket" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Well Name:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="DRwellname" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Well Type:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="WellTypeDDL" runat="server" Height="16px" Width="122px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        Location Information:
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        LSD:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="DRLSDTB" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Section:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="DRSectionTB" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Township:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="DRTownshipTB" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Range:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="DRRangeTB" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Meridian:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="DRMeridianTB" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <%--                <tr>
                    <td>Latitude:</td>
                    <td><asp:TextBox ID="DRLatitudeTB" runat="server" Enabled="false"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Longitude:</td>
                    <td><asp:TextBox ID="DRLongitudeTB" runat="server" Enabled="false"></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="2"><asp:CheckBox ID="DecimalCB" runat="Server" Text="Decimal" Enabled="false" /><asp:CheckBox ID="DegreesCB" runat="Server" Text="Degrees" Enabled="false" /><asp:CheckBox ID="GPSCB" runat="Server" Text="GPS" Enabled="false" /></td>
                </tr>
                <tr>
                    <td align="right">UTM &nbsp;&nbsp;(N)</td>
                    <td><asp:TextBox ID="DRUTMNTB" runat="server" Enabled="false"></asp:TextBox></td>
                </tr>
                <tr>
                    <td align="right">(X)</td>
                    <td><asp:TextBox ID="XTB" runat="server" Enabled="false"></asp:TextBox></td>
                </tr>
                <tr>
                    <td align="right">(Y)</td>
                    <td><asp:TextBox ID="DRYTB" runat="server" Enabled="false"></asp:TextBox></td>
                </tr>
--%>
                                <tr>
                                    <td>
                                        Casing Size:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="CasingSizeDDL" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Liner Size:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="LinerSizeDDL" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        TMD:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="DRTMDTB" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        TVD:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="DRTVDTB" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        System Type:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="SystemTypeDDL" runat="server" Height="17px" Width="118px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Tool List:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ToolListDDL" runat="server" Height="16px" Width="115px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <!--Second table starts here -->
                            <table>
                                <tr>
                                    <td>
                                        Problem Occured During:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ProbOccurDDL" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Category:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="CategoryDDL" runat="server" AutoPostBack="true" OnSelectedIndexChanged="CategoryDDL_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Description:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="SubCategoryDDL" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Notes:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="NotesTB" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Executive Summary:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ExecSummaryTB" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Description:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="DescriptionTB" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Observations:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ObservationsTB" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <!--Third table starts here -->
                            <table>
                                <tr>
                                    <td>
                                        Notes:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="Notes2TB" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Events:
                                    </td>
                                    <td>
                                        <%--<asp:GridView ID="EventGV" runat="server"></asp:GridView>--%><asp:TextBox ID="EventsTB"
                                            runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Preface:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="PrefaceTB" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Causes:
                                    </td>
                                    <td>
                                        <%--<asp:GridView ID="CauseGV" runat="server"></asp:GridView>--%><asp:TextBox ID="CausesTB"
                                            runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Cause Analysis:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="CauseAnalysisTB" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Remediation:
                                    </td>
                                    <td>
                                        <%--<asp:GridView ID="CorrectiveActGV" runat="server"></asp:GridView>--%><asp:TextBox
                                            ID="RemediationTB" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Attachments:
                                    </td>
                                    <td>
                                        <%--<asp:GridView ID="AttachmentGV" runat="server">
                            <Columns>
                                <asp:HyperLinkField Text="Delete" />
                            </Columns>
                        </asp:GridView>--%>
                                        <asp:TextBox ID="AttachmentTB" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <!--End Complete table  -->
                </table>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ReSearch" EventName="click" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="upd2" runat="server">
        <ContentTemplate>
            <asp:Panel ID="ResultPanel" runat="server">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                               <asp:Button ID="ReportBTN" runat="server" Text="Print Report" OnClick="ReportBTN_Click" />
<br />
<hr />
                       
                        <%--<asp:Button ID="PrintBTN" runat="server" Text="Print" OnClick="PrintRecord_Click"  />
                        <asp:Panel runat="server" ID="PrintPanel">
                            <asp:GridView runat="server" ID="printGV" AutoGenerateColumns="true">
                            </asp:GridView>
                        </asp:Panel>
                        <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="PrintPNL"
                            TargetControlID="PrintBTN" OkControlID="OKButton" OnOkScript="stopMoving();">
                        </cc1:ModalPopupExtender>--%>
                        <asp:GridView runat="server" ID="ResultGV" AllowPaging="true" AutoGenerateColumns="false">
                            <Columns>
                                <asp:TemplateField HeaderText="ReportId">
                                    <ItemTemplate>
                                        <asp:Label ID="ReportId" runat="server" Text='<%# Eval("ReportId") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ReportNumber">
                                    <ItemTemplate>
                                        <asp:Label ID="ReportNumber" runat="server" Text='<%# Eval("ReportNumber") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Title">
                                    <ItemTemplate>
                                        <asp:Label ID="Title" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Company">
                                    <ItemTemplate>
                                        <asp:Label ID="Company" runat="server" Text='<%# Eval("CompanyName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Customer">
                                    <ItemTemplate>
                                        <asp:Label ID="Customer" runat="server" Text='<%# Eval("Customer") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ReportDate">
                                    <ItemTemplate>
                                        <asp:Label ID="ReportDate" runat="server" Text='<%# Convert.ToDateTime(Eval("ReportDate")).ToShortDateString() %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Select">
                                    <ItemTemplate>
                                        <asp:CheckBox Enabled="true" ID="Report" runat="server" Checked="true" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:Button ID="ReSearch" runat="server" Text="Back to Search" OnClick="ReSearch_Click" />
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ReSearch" EventName="click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
