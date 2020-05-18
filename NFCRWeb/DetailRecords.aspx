<%@ Page Language="C#" MasterPageFile="~/PackersPlus.Master" AutoEventWireup="true"
    CodeBehind="DetailRecords.aspx.cs" Inherits="NCRFTRWeb.WebForm8" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        td
        {
            vertical-align: top;
        }
        .style3
        {
            width: 404px;
        }
        .style5
        {
            width: 223px;
        }
        .style6
        {
            width: 215px;
        }
        .style7
        {
            width: 90px;
        }
        .style8
        {
            width: 52px;
        }
        .style9
        {
            width: 38px;
        }
        .style10
        {
            width: 28px;
        }
        .style11
        {
            width: 51px;
        }
        #Notes
        {
            margin-top: 0px;
            width: 259px;
            height: 110px;
        }
        #RObservations
        {
            height: 239px;
            width: 247px;
        }
        #rDescription
        {
            height: 198px;
            width: 250px;
        }
        .style12
        {
            height: 68px;
        }
        #PrefaceTA
        {
            height: 35px;
            width: 423px;
        }
        #notes2
        {
            width: 425px;
        }
        #rExecSummary
        {
            width: 252px;
            height: 209px;
        }
        .button
    {
        border:none;
        background-color: Transparent;
    }
    </style>

    <script type="text/javascript">
         var id = null;      
         function movePanel(id)      
         {           
             var pnl = $get(id);           
             if (pnl != null)           
             {                
                 pnl.style.left = "250px";                
                 pnl.style.top = "50px";                
                 id = setTimeout("movePanel("+ id +");", 100);           
             }      
         }
         function stopMoving()      
         {           
            clearTimeout(id);      
         } 
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">
    <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server">
    </asp:ScriptManager>
   

    <asp:Panel ID="DetailPanel" runat="server">

    <table style="width: 1125px">
        <tr>
            <td valign="top">
                Report No: &nbsp;<asp:DropDownList ID="DRreportNumberDDL" runat="server" AutoPostBack="true"
                    OnSelectedIndexChanged="DRreportNumberDDL_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td valign="top" class="style6">
                Report Type:&nbsp;<asp:DropDownList ID="DRReportType" runat="server" Width="126px"
                    Enabled="true">
                    <asp:ListItem>NCR</asp:ListItem>
                    <asp:ListItem>FTR</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style7">
                &nbsp;
            </td>
            <td class="style8">
                <asp:Button ID="DRDeleteBTN" runat="server" Text="Delete" Enabled="false" OnClick="DRDeleteBTN_Click" />
            </td>
            <td class="style9">
                <asp:Button ID="DRAddBTN" runat="server" Text="Add" Enabled="true" OnClick="DRAddBTN_Click" />
            </td>
            <td class="style10">
                <asp:Button ID="DREditBTN" runat="server" Text="Edit" Enabled="False" OnClick="DREditBTN_Click" />
            </td>
            <td class="style9">
                <asp:Button ID="DRSaveBTN" runat="server" Text="Save" Enabled="False" OnClick="DRSaveBTN_Click" />
            </td>
            <td class="style11">
                <asp:Button ID="DRCancelBTN" runat="server" Text="Cancel" Enabled="false" OnClick="DRCancelBTN_Click" />
            </td>
            <td class="style9">
                <asp:Button ID="DRPrintBTN" runat="server" Text="Print" Enabled="false" Style="margin-left: 0px"
                    OnClick="DRPrintBTN_Click" />
            </td>
            <td>
            </td>
            <td>
            </td>
            <td colspan="4">
                <div id="Navigation" class="navigation" runat="server">
                    <div id="leftnav">
                       <asp:Button CssClass="button" ID="First" Text="<< First" runat="server" 
            onclick="First_Click" />
        <asp:Button CssClass="button" ID="Previous" Text="<< Previous" runat="server" 
            onclick="Previous_Click" />
        <asp:Button CssClass="button" ID="Next" Text="Next >>" runat="server" 
            onclick="Next_Click" />
        <asp:Button CssClass="button" ID="last" Text="Last >>" runat="server" 
            onclick="last_Click" />
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td valign="top" class="style5">
                <asp:Label ID="MessageLBL" runat="server" ForeColor="#990000"></asp:Label>
            </td>
            <td class="style6">
            </td>
            <td class="style7">
            </td>
            <td class="style8">
            </td>
            <td class="style9">
            </td>
            <td class="style10">
            </td>
            <td class="style9">
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
    </table>
    <br />
 
    <table>
        <tr style="background-color: Menu;">
            <td>
                <strong>Customer:</strong>
            </td>
            <td>
                <asp:DropDownList ID="CustomerDDL" runat="server" Width="126px" AutoPostBack="true"
                    OnSelectedIndexChanged="CustomerDDL_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Button ID="NewCustomerBTN" runat="server" Text="Add New" />
                <asp:Panel ID="CustomerPNL" runat="server" Width="500px" BackColor="LightGray" Height="269px"
                    Style="display: none">
                    <table>
                        <tr>
                            <td>
                                <strong>Customer:</strong>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Company:
                            </td>
                            <td>
                                <asp:TextBox ID="customer_company" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Contact Name:
                            </td>
                            <td>
                                <asp:TextBox ID="customer_contact" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Contact Phone:
                            </td>
                            <td>
                                <asp:TextBox ID="customer_contactphone" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Contact Extension:
                            </td>
                            <td>
                                <asp:TextBox ID="customerExtension" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Contact Fax:
                            </td>
                            <td>
                                <asp:TextBox ID="customerFax" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Contact Email:
                            </td>
                            <td>
                                <asp:TextBox ID="customer_contactemail" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="Accept4" runat="server" CssClass="buttonClass" Text="Save" OnClick="SaveCustomer_Click"
                                    CommandArgument='' />
                                <asp:Button ID="OKButton4" runat="server" Text="Close" />
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <cc1:ModalPopupExtender ID="ModalPopupExtender4" runat="server" PopupControlID="CustomerPNL"
                    TargetControlID="NewCustomerBTN" OkControlID="OKButton4" OnOkScript="stopMoving();">
                </cc1:ModalPopupExtender>
            </td>
            <td>
                <strong>Operator:</strong>
            </td>
            <td>
                <asp:DropDownList ID="OperatorDDL" runat="server" Width="126px" AutoPostBack="true"
                    OnSelectedIndexChanged="OperatorDDL_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Button ID="NewOperatorBTN" runat="server" Text="Add New" />
                <asp:Panel ID="OperatorPNL" runat="server" Width="500px" BackColor="LightGray" Height="269px"
                    Style="display: none">
                    <table>
                        <tr>
                            <td>
                                <strong>Operator:</strong>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Operator Name:
                            </td>
                            <td>
                                <asp:TextBox ID="operatorName" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Operator Email:
                            </td>
                            <td>
                                <asp:TextBox ID="operatorEmail" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Operator Phone:
                            </td>
                            <td>
                                <asp:TextBox ID="operatorPhone" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Operator Extention:
                            </td>
                            <td>
                                <asp:TextBox ID="operatorExt" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Operator Fax:
                            </td>
                            <td>
                                <asp:TextBox ID="operatorFax" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="Accept6" runat="server" CssClass="buttonClass" Text="Save" OnClick="SaveNewOperatorBTN_Click"
                                    CommandArgument='' />
                                <asp:Button ID="OKButton6" runat="server" Text="Close" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <cc1:ModalPopupExtender ID="ModalPopupExtender6" runat="server" PopupControlID="OperatorPNL"
                    TargetControlID="NewOperatorBTN" OkControlID="OKButton6" OnOkScript="stopMoving();">
                </cc1:ModalPopupExtender>
            </td>
            <td>
                <strong>Engineer:</strong>
            </td>
            <td>
                <asp:DropDownList ID="EngineerDDL" runat="server" Width="126px" AutoPostBack="true"
                    OnSelectedIndexChanged="EngineerDDL_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Button ID="NewEngineerBTN" runat="server" Text="Add New" />
                <asp:Panel ID="EngineerPNL" runat="server" Width="500px" BackColor="LightGray" Height="269px"
                    Style="display: none">
                    <table>
                        <tr>
                            <td>
                                <strong>Engineer:</strong>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Name:
                            </td>
                            <td>
                                <asp:TextBox ID="initiatorname" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Phone:
                            </td>
                            <td>
                                <asp:TextBox ID="initiatorphone" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Ext:
                            </td>
                            <td>
                                <asp:TextBox ID="initiatorExt" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Fax:
                            </td>
                            <td>
                                <asp:TextBox ID="initiatorFax" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Email:
                            </td>
                            <td>
                                <asp:TextBox ID="initiatoremail" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="Accept5" runat="server" CssClass="buttonClass" Text="Save" OnClick="SaveEngineerBTN_Click"
                                    CommandArgument='' />
                                <asp:Button ID="OKButton5" runat="server" Text="Close" />
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <cc1:ModalPopupExtender ID="ModalPopupExtender5" runat="server" PopupControlID="EngineerPNL"
                    TargetControlID="NewEngineerBTN" OkControlID="OKButton5" OnOkScript="stopMoving();">
                </cc1:ModalPopupExtender>
            </td>
            <td>
                Location:
            </td>
            <td>
                <asp:DropDownList ID="LocationDDL" runat="server" Width="126px" AutoPostBack="true"
                    OnSelectedIndexChanged="LocationDDL_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Button ID="NewLocationBTN" runat="server" Text="Add New" />
                <asp:Panel ID="LocationPNL" runat="server" Width="500px" BackColor="LightGray" Height="269px"
                    Style="display: none">
                    <table>
                        <tr>
                            <td>
                                <strong>Location:</strong>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                District
                            </td>
                            <td>
                                <asp:TextBox ID="district" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Manager:
                            </td>
                            <td>
                                <asp:TextBox ID="managerName" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Address
                            </td>
                            <td>
                                <asp:TextBox ID="address" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                City
                            </td>
                            <td>
                                <asp:TextBox ID="City" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Province / State:
                            </td>
                            <td>
                                <asp:TextBox ID="Province" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="Accept7" runat="server" CssClass="buttonClass" Text="Save" OnClick="SaveNewLocationBTN_Click"
                                    CommandArgument='' />
                                <asp:Button ID="OKButton7" runat="server" Text="Close" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <cc1:ModalPopupExtender ID="ModalPopupExtender7" runat="server" PopupControlID="LocationPNL"
                    TargetControlID="NewLocationBTN" OkControlID="OKButton7" OnOkScript="stopMoving();">
                </cc1:ModalPopupExtender>
            </td>
        </tr>
        <tr>
            <td>
                Company:
            </td>
            <td>
                <asp:TextBox ID="rcompany" runat="server"></asp:TextBox>
            </td>
            <td>
            </td>
            <td>
                Operator Name:
            </td>
            <td>
                <asp:TextBox ID="rOperatorName" runat="server"></asp:TextBox>
            </td>
            <td>
            </td>
            <td>
                Initiator Name:
            </td>
            <td>
                <asp:TextBox ID="rInitiatorName" runat="server"></asp:TextBox>
            </td>
            <td>
            </td>
            <td>
                District
            </td>
            <td>
                <asp:TextBox ID="rDistrict" runat="server"></asp:TextBox>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                Customer Contact:
            </td>
            <td>
                <asp:TextBox ID="rcustomercontact" runat="server"></asp:TextBox>
            </td>
            <td>
            </td>
            <td>
                Operator Email:
            </td>
            <td>
                <asp:TextBox ID="rOperatorEmail" runat="server"></asp:TextBox>
            </td>
            <td>
            </td>
            <td>
                Initiator Email:
            </td>
            <td>
                <asp:TextBox ID="rInitiatorEmail" runat="server"></asp:TextBox>
            </td>
            <td>
            </td>
            <td>
                Manager Name:
            </td>
            <td>
                <asp:TextBox ID="rManagerName" runat="server"></asp:TextBox>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                Contact Phone:
            </td>
            <td>
                <asp:TextBox ID="rcustomerphone" runat="server"></asp:TextBox>
            </td>
            <td>
            </td>
            <td>
                Operator Phone:
            </td>
            <td>
                <asp:TextBox ID="rOperatorPhone" runat="server"></asp:TextBox>
            </td>
            <td>
            </td>
            <td>
                Initiator Phone:
            </td>
            <td>
                <asp:TextBox ID="rInitiatorPhone" runat="server"></asp:TextBox>
            </td>
            <td>
            </td>
            <td>
                Address:
            </td>
            <td>
                <asp:TextBox ID="rmanagerAddress" runat="server"></asp:TextBox>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                Contact Email:
            </td>
            <td>
                <asp:TextBox ID="rCustomerEmail" runat="server"></asp:TextBox>
            </td>
            <td>
            </td>
            <td>
                Operator Extension:
            </td>
            <td>
                <asp:TextBox ID="rOperatorExt" runat="server"></asp:TextBox>
            </td>
            <td>
            </td>
            <td>
                Initiator Extension:
            </td>
            <td>
                <asp:TextBox ID="rInitiatorExt" runat="server"></asp:TextBox>
            </td>
            <td>
            </td>
            <td>
                City:
            </td>
            <td>
                <asp:TextBox ID="rLocCityAddress" runat="server"></asp:TextBox>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                Contact Fax:
            </td>
            <td>
                <asp:TextBox ID="rContactFax" runat="server"></asp:TextBox>
            </td>
            <td>
            </td>
            <td>
                Operator Fax:
            </td>
            <td>
                <asp:TextBox ID="rOperatorFax" runat="server"></asp:TextBox>
            </td>
            <td>
            </td>
            <td>
                Initiator Fax:
            </td>
            <td>
                <asp:TextBox ID="rInitiatorFax" runat="server"></asp:TextBox>
            </td>
            <td>
            </td>
            <td>
                Prov / State
            </td>
            <td>
                <asp:TextBox ID="rProvState" runat="server"></asp:TextBox>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td colspan="9">
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
    </table>
    <br />
    <hr />
    <table>
        <tr>
            <td>
                <!-- Left Table -->
                <table>
                    <tr>
                        <td>
                            Completed:
                        </td>
                        <td>
                            <asp:TextBox ID="DRDateTB" runat="server" Enabled="false"></asp:TextBox>
                            <cc1:CalendarExtender ID="DRCalExtender" TargetControlID="DRDateTB" Format="yyyy/MM/dd"
                                runat="server">
                            </cc1:CalendarExtender>
                        </td>
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
                            <asp:DropDownList ID="WellTypeDDL" runat="server">
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
                    <tr>
                        <td>
                            Latitude:
                        </td>
                        <td>
                            <asp:TextBox ID="DRLatitudeTB" runat="server" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Longitude
                        </td>
                        <td>
                            <asp:TextBox ID="DRLongitudeTB" runat="server" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:CheckBox ID="DecimalCB" runat="Server" Text="Decimal" Enabled="false" /><asp:CheckBox
                                ID="DegreesCB" runat="Server" Text="Degrees" Enabled="false" /><asp:CheckBox ID="GPSCB"
                                    runat="Server" Text="GPS" Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            UTM &nbsp;&nbsp;(N)
                        </td>
                        <td>
                            <asp:TextBox ID="DRUTMNTB" runat="server" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            (X)
                        </td>
                        <td>
                            <asp:TextBox ID="XTB" runat="server" Enabled="false"></asp:TextBox>
                        </td>
                        <tr>
                            <td align="right">
                                (Y)
                            </td>
                            <td>
                                <asp:TextBox ID="DRYTB" runat="server" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
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
                            <td colspan="2" class="style12">
                                <asp:CheckBox ID="FeetCB" runat="server" Text="Feet" />
                                <asp:CheckBox ID="MetersCB" runat="server" Text="Meters" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                System Type:
                            </td>
                            <td>
                                <asp:DropDownList ID="SystemTypeDDL" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Tool List:
                            </td>
                            <td>
                                <asp:DropDownList ID="ToolListDDL" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                </table>
            </td>
            <td>
                <!-- Middle Table -->
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
                        <td valign="top" colspan="2">
                            Notes:<br />
                            <textarea id="Notes" runat="server" rows="6" cols="40"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" valign="top">
                            Executive Summary:<br />
                            <textarea id="DRExecSummary" rows="6" cols="40" runat="server"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" valign="top">
                            Description:<br />
                            <textarea id="DRDescription" rows="6" cols="40" runat="server"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" valign="top">
                            Observations:<br />
                            <textarea id="DRObservations" rows="6" cols="40" runat="server"></textarea>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <!-- Right Table -->
                <table>
                    <tr>
                        <td colspan="2">
                            Notes:<br />
                            <textarea runat="server" id="DRnotes2" rows="6" cols="40"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            Events:<br />
                            <div style="text-align: right">
                                <asp:Button ID="AddEvent" runat="server" Text="Add New Row" Enabled="true" OnClientClick="movePanel('EventsPNL');" />
                            </div>
                            <asp:Label ID="EventLBL" runat="server"></asp:Label>
                            <asp:Panel ID="EventsPNL" runat="server" Width="500px" BackColor="LightGray" Height="269px"
                                Style="display: none">
                                <table>
                                    <tr>
                                        <td style="text-align: right">
                                            Event Title:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtEventTitle" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            Event Date:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtEventDate" runat="server" Enabled="true" Text="" />
                                            <cc1:CalendarExtender ID="EventCalExtender" runat="server" TargetControlID="txtEventDate"
                                                Format="dd/MM/yyyy">
                                            </cc1:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            Event Content:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtEventContent" runat="server" TextMode="MultiLine" Rows="6" Columns="40" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <asp:Button ID="Accept" runat="server" CssClass="buttonClass" Text="Save" OnClick="SaveEvent_Click"
                                                CommandArgument='' />
                                            <asp:Button ID="OKButton" runat="server" Text="Close" />
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            
                            
                            
                            
                            <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Conditional">                    
                                <ContentTemplate>
                                    <asp:GridView ID="gdvEvents" runat="server" AllowPaging="true" AutoGenerateColumns="false"
                                        OnRowCancelingEdit="gdvEvents_EditCancel" OnRowDeleting="gdvEvents_RowDeleting"
                                        OnRowEditing="gdvEvents_RowEditing" OnRowUpdating="gdvEvents_RowUpdating" OnPageIndexChanging="gdvEvents_Paging"
                                        PageSize="5">
                                        <Columns>
                                            <asp:TemplateField Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="EventId" runat="server" Visible="false" Text='<%# Eval("EventId") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Event Title">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEventTitle" runat="server" Text='<%# Eval("EventTitle") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Label ID="lblEventID" runat="server" Visible="false" Text='<%# Eval("EventId") %>' />
                                                    <asp:TextBox ID="edtEventTitle" runat="server" Text='<%# Eval("EventTitle") %>' />
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Event Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEventDate" runat="server" Text='<%# Convert.ToDateTime(Eval("EventDate")).ToShortDateString() %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="edtEventDate" runat="server" Enabled="true" Text='<%# Convert.ToDateTime(Eval("EventDate")).ToShortDateString() %>'></asp:TextBox>
                                                    <cc1:CalendarExtender ID="EventCalExtender" TargetControlID="edtEventDate" Format="dd/MM/yyyy"
                                                        runat="server">
                                                    </cc1:CalendarExtender>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Event Content">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEventContent" runat="server" Text='<%# Bind("EventText") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="edtEventContent" runat="server" Text='<%# Bind("EventText") %>'
                                                        TextMode="MultiLine" Rows="4" />
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:CommandField ShowEditButton="true" ButtonType="Button" />
                                            <asp:CommandField ShowDeleteButton="true" ButtonType="Button" />
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="Accept" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="EventsPNL"
                                TargetControlID="AddEvent" OkControlID="OKButton" OnOkScript="stopMoving();">
                            </cc1:ModalPopupExtender>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" class="style3">
                            Preface:<br />
                            <textarea runat="server" id="DRPrefaceTA" rows="6" cols="40"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Possible Causes:
                            <div style="text-align: right">
                                <asp:Button ID="AddCause" runat="server" Text="Add New Row" Enabled="true" OnClientClick="movePanel('CausesPNL');" />
                            </div>
                            <asp:Label ID="CauseLBL" runat="server"></asp:Label>
                            <asp:Panel ID="CausesPNL" runat="server" Width="500px" BackColor="LightGray" Height="269px"
                                Style="display: none">
                                <table>
                                    <tr>
                                        <td style="text-align: right">
                                            Cause Title:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCauseTitle" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            Cause Content:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCauseContent" runat="server" TextMode="MultiLine" Rows="6" Columns="40" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <asp:Button ID="Accept1" runat="server" CssClass="buttonClass" Text="Save" OnClick="SaveCause_Click"
                                                CommandArgument='' />
                                            <asp:Button ID="OKButton1" runat="server" Text="Close" />
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="gvCause" runat="server" AllowPaging="true" AutoGenerateColumns="false"
                                        OnRowCancelingEdit="gdvCauses_EditCancel" OnRowDeleting="gdvCauses_RowDeleting"
                                        OnRowEditing="gdvCauses_RowEditing" OnRowUpdating="gdvCauses_RowUpdating" OnPageIndexChanging="gdvCauses_Paging"
                                        PageSize="5">
                                        <Columns>
                                            <asp:TemplateField Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="CauseId" runat="server" Visible="false" Text='<%# Eval("CauseId") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cause Title">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCauseTitle" runat="server" Text='<%# Eval("CauseTitle") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Label ID="lblCauseID" runat="server" Visible="false" Text='<%# Eval("CauseId") %>' />
                                                    <asp:TextBox ID="edtCauseTitle" runat="server" Text='<%# Eval("CauseTitle") %>' />
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cause Content">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCauseContent" runat="server" Text='<%# Bind("CauseText") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="edtCauseContent" runat="server" Text='<%# Bind("CauseText") %>'
                                                        TextMode="MultiLine" Rows="4" />
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:CommandField ShowEditButton="true" ButtonType="Button" />
                                            <asp:CommandField ShowDeleteButton="true" ButtonType="Button" />
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="Accept1" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="CausesPNL"
                                TargetControlID="AddCause" OkControlID="OKButton1" OnOkScript="stopMoving();">
                            </cc1:ModalPopupExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Cause Analysis:<br />
                            <textarea id="DRCauseAnalysisTA" runat="server" rows="6" cols="40"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Remediation:
                            <div style="text-align: right">
                                <asp:Button ID="AddCorrection" runat="server" Text="Add New Row" Enabled="true" OnClientClick="movePanel('CorrectionPNL');" />
                            </div>
                            <asp:Label ID="CorrectionLBL" runat="server"></asp:Label>                            
                            <asp:Panel ID="CorrectionPNL" runat="server" Width="500px" BackColor="LightGray"
                                Height="269px" Style="display: none">
                                <table>
                                    <tr>
                                        <td style="text-align: right">
                                            Correction Title:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCorrectionTitle" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            Correction Content:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCorrectionContent" runat="server" TextMode="MultiLine" Rows="6"
                                                Columns="40" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <asp:Button ID="Accept2" runat="server" CssClass="buttonClass" Text="Save" OnClick="SaveCorrection_Click"
                                                CommandArgument='' />
                                            <asp:Button ID="OKButton2" runat="server" Text="Close" />
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="gdvCorrections" runat="server" AllowPaging="true" AutoGenerateColumns="false"
                                        OnRowCancelingEdit="gdvCorrections_EditCancel" OnRowDeleting="gdvCorrections_RowDeleting"
                                        OnRowEditing="gdvCorrections_RowEditing" OnRowUpdating="gdvCorrections_RowUpdating"
                                        OnPageIndexChanging="gdvCorrections_Paging" PageSize="5">
                                        <Columns>
                                            <asp:TemplateField Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCorrectionActID" runat="server" Visible="false" Text='<%# Eval("CorrectiveActId") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Corrections Title">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCorrectionTitle" runat="server" Text='<%# Eval("CorrectiveActTitle") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Label ID="lblCorrectionID" runat="server" Visible="false" Text='<%# Eval("CorrectiveActId") %>' />
                                                    <asp:TextBox ID="edtCorrectionTitle" runat="server" Text='<%# Eval("CorrectiveActTitle") %>' />
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Correction Content">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCorrectionContent" runat="server" Text='<%# Bind("CorrectiveActText") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="edtCorrectionContent" runat="server" Text='<%# Bind("CorrectiveActText") %>'
                                                        TextMode="MultiLine" Rows="4" />
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:CommandField ShowEditButton="true" ButtonType="Button" />
                                            <asp:CommandField ShowDeleteButton="true" ButtonType="Button" />
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="Accept2" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <cc1:ModalPopupExtender ID="ModalPopupExtender3" runat="server" PopupControlID="CorrectionPNL"
                                TargetControlID="AddCorrection" OkControlID="OKButton2" OnOkScript="stopMoving();">
                            </cc1:ModalPopupExtender>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            Attachments:
                     <%--  <div style="text-align: right">
                                <asp:Button ID="AddAttachment" runat="server" Text="Add New Row" Enabled="true" OnClientClick="movePanel('AttachmentPNL');" />
                            </div>--%>
                            <asp:Label ID="uxFileInfo" runat="server"></asp:Label>
                            <asp:UpdatePanel ID="FilePanel" runat="server">
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="Button1" />
                                </Triggers>
                                <ContentTemplate>
                                    <div style="text-align: right">
                                        <asp:Button ID="btnShow" runat="server" Text="Add New Row" OnClick="btnShow_Click" />
                                    </div>
                                    <asp:Panel ID="pnlEdit" runat="server" Style="display: none">
                                        <table>
                                            <tr>
                                                <td style="text-align: right">
                                                    Attachment Title:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtAttachmentTitle" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right">
                                                    Attachment File:
                                                </td>
                                                <td>
                                                    <asp:FileUpload ID="fileUpload" runat="server"></asp:FileUpload>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right">
                                                    Attachment Comments:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtAttachmentComments" runat="server" TextMode="MultiLine" Rows="6"
                                                        Columns="40" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right">
                                                    <asp:Button ID="Button1" runat="server" Text="File Upload" OnClick="btnFileUpload_Click"
                                                        Width="81px" /><asp:Button ID="Button2" runat="server" Text="Close" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <%-- <cc1:ModalPopupExtender ID="ModalPopupExtender4" runat="server" PopupControlID="pnlEdit"
                    TargetControlID="AddAttachment" OkControlID="OKButton3" OnOkScript="stopMoving();">
                </cc1:ModalPopupExtender>--%>
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="gdvAttachments" runat="server" AllowPaging="true" AutoGenerateColumns="false"
                                        OnRowCancelingEdit="gdvAttachments_EditCancel" OnRowDeleting="gdvAttachments_RowDeleting"
                                        OnRowEditing="gdvAttachments_RowEditing" OnRowUpdating="gdvAttachments_RowUpdating"
                                        OnPageIndexChanging="gdvAttachments_Paging" PageSize="5">
                                        <Columns>
                                            <asp:TemplateField Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAttachmentID" runat="server" Visible="false" Text='<%# Eval("AttachmentId") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Attachment Title">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAttachmentTitle" runat="server" Text='<%# Eval("AttachmentTitle") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Label ID="lblCorrectionID" runat="server" Visible="false" Text='<%# Eval("AttachmentId") %>' />
                                                    <asp:TextBox ID="edtAttachmentTitle" runat="server" Text='<%# Eval("AttachmentTitle") %>' />
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Filename">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFilename" runat="server" Text='<%# Eval("AttachmentFilename") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Correction Content">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAttachmentContent" runat="server" Text='<%# Bind("AttachmentExplaination") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="edtAttachmentContent" runat="server" Text='<%# Bind("AttachmentExplaination") %>'
                                                        TextMode="MultiLine" Rows="4" />
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:CommandField ShowEditButton="true" ButtonType="Button" />
                                            <asp:CommandField ShowDeleteButton="true" ButtonType="Button" />
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </asp:Panel>
</asp:Content>
