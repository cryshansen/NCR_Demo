<%@ Page Language="C#" MasterPageFile="~/PackersPlus.Master" AutoEventWireup="true" CodeBehind="Misc.aspx.cs" Inherits="NCRFTRWeb.WebForm7" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">
    <asp:Panel ID="MiscPanel" runat="server">
    <table>
        <tr>
            <td colspan="3">    <table>
       <tr>
              <td>&nbsp;&nbsp;&nbsp;</td>
              <td><p style="color:Navy;"></p></td>
              <td>&nbsp;&nbsp;&nbsp;</td>
              <td><asp:Button ID="WTDeleteBN" runat="server" Text="Delete" Enabled="false" onclick="WTDeleteBN_Click" /></td><td>
                <asp:Button ID="WTAddNew" runat="server" Text="Add" onclick="WTAddNew_Click"  /></td><td>
                <asp:Button ID="WTEdit" runat="server" Text="Edit" Enabled="false" onclick="WTEdit_Click" /></td><td>
                <asp:Button ID="WTSave" runat="server" Text="Save" Enabled="false" onclick="WTSave_Click"   /></td><td>
                <asp:Button ID="WTCancel" runat="server" Text="Cancel" Enabled="false" onclick="WTCancel_Click"  /></td><td>
                <asp:Button ID="WTPrint" runat="server" Text="Print" Enabled="false" onclick="WTPrint_Click" /></td>
       </tr>
    </table></td>
        </tr>
        <tr>
            <td>Well Type</td>
            <td>
                <asp:DropDownList ID="WellTypeDDL" runat="server" AutoPostBack="true" 
                    onselectedindexchanged="WellTypeDDL_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td>Well Type Name: </td>
            <td>
                <asp:TextBox ID="WTName" runat="server"></asp:TextBox></td>
            <td></td>
            <td></td>        
        </tr>
        <tr>
            <td colspan="4">
                <asp:Label ID="WTMessageLBL" runat="server" Text=""></asp:Label></td>        
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td colspan="3">    <table>
       <tr>
              <td>&nbsp;&nbsp;&nbsp;</td>
              <td><p style="color:Navy;"></p></td>
              <td>&nbsp;&nbsp;&nbsp;</td>
              <td><asp:Button ID="CSDeleteBTN" runat="server" Text="Delete" Enabled="false" onclick="CSDeleteBN_Click" /></td><td>
                <asp:Button ID="CSAddNewBTN" runat="server" Text="Add" onclick="CSAddNew_Click"  /></td><td>
                <asp:Button ID="CSEditBTN" runat="server" Text="Edit" Enabled="false" onclick="CSEdit_Click" /></td><td>
                <asp:Button ID="CSSaveBTN" runat="server" Text="Save" Enabled="false" onclick="CSSave_Click"   /></td><td>
                <asp:Button ID="CSCancelBTN" runat="server" Text="Cancel" Enabled="false" onclick="CSCancel_Click"  /></td><td>
                <asp:Button ID="CSPrintBTN" runat="server" Text="Print" Enabled="false" onclick="CSPrint_Click" /></td>
       </tr>
    </table></td>
        </tr>
        <tr>
            <td>Casing Size</td>
            <td>
                <asp:DropDownList ID="CasingSizeDDL" runat="server" AutoPostBack="true" 
                    onselectedindexchanged="CasingSizeDDL_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td>Casing Size Name: </td>
            <td>
                <asp:TextBox ID="CSName" runat="server"></asp:TextBox></td>
            <td></td>
            <td></td>        
        </tr>
        <tr>
            <td colspan="4">
                <asp:Label ID="CSMessageLBL" runat="server" Text=""></asp:Label></td>        
        </tr>
        <tr>
            <td></td>        
            <td></td>
            <td></td>
            <td></td>
        </tr>

        <tr>
            <td colspan="3">    <table>
       <tr>
              <td>&nbsp;&nbsp;&nbsp;</td>
              <td><p style="color:Navy;"></p></td>
              <td>&nbsp;&nbsp;&nbsp;</td>
              <td><asp:Button ID="LSDeleteBTN" runat="server" Text="Delete" Enabled="false" onclick="LSDeleteBN_Click" /></td><td>
                <asp:Button ID="LSAddNewBTN" runat="server" Text="Add" onclick="LSAddNew_Click"  /></td><td>
                <asp:Button ID="LSEditBTN" runat="server" Text="Edit" Enabled="false" onclick="LSEdit_Click" /></td><td>
                <asp:Button ID="LSSaveBTN" runat="server" Text="Save" Enabled="false" onclick="LSSave_Click"   /></td><td>
                <asp:Button ID="LSCancelBTN" runat="server" Text="Cancel" Enabled="false" onclick="LSCancel_Click"  /></td><td>
                <asp:Button ID="LSPrintBTN" runat="server" Text="Print" Enabled="false" 
                      onclick="LSPrint_Click" style="height: 26px" /></td>
       </tr>
    </table></td>
        </tr>
        <tr>
            <td>Liner Size</td>
            <td>
                <asp:DropDownList ID="LinerSizeDDL" runat="server" AutoPostBack="true" 
                    onselectedindexchanged="LinerSizeDDL_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td></td>
            <td></td>
        </tr>
                <tr>
            <td>Liner Size Name: </td>
            <td>
                <asp:TextBox ID="LSName" runat="server"></asp:TextBox></td>
            <td></td>
            <td></td>        
        </tr>
        <tr>
            <td colspan="4">
                <asp:Label ID="LSMessageLBL" runat="server" Text=""></asp:Label></td>        
        </tr>


        <tr>
            <td></td>        
            <td></td>
            <td></td>
            <td></td>
        </tr>

        <tr>
            <td colspan="3">    <table>
       <tr>
              <td>&nbsp;&nbsp;&nbsp;</td>
              <td><p style="color:Navy;"></p></td>
              <td>&nbsp;&nbsp;&nbsp;</td>
              <td><asp:Button ID="STDeleteBTN" runat="server" Text="Delete" Enabled="false" onclick="STDeleteBN_Click" /></td><td>
                <asp:Button ID="STAddNewBTN" runat="server" Text="Add" onclick="STAddNew_Click"  /></td><td>
                <asp:Button ID="STEditBTN" runat="server" Text="Edit" Enabled="false" onclick="STEdit_Click" /></td><td>
                <asp:Button ID="STSaveBTN" runat="server" Text="Save" Enabled="false" onclick="STSave_Click"   /></td><td>
                <asp:Button ID="STCancelBTN" runat="server" Text="Cancel" Enabled="false" onclick="STCancel_Click"  /></td><td>
                <asp:Button ID="STPrintBTN" runat="server" Text="Print" Enabled="false" onclick="STPrint_Click" /></td>
       </tr>
    </table></td>
        </tr>
        <tr>
            <td>System Type</td>
            <td>
                <asp:DropDownList ID="SystemTypeDDL" runat="server" AutoPostBack="true" 
                    onselectedindexchanged="SystemTypeDDL_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td></td>
            <td></td>
        </tr>
                <tr>
            <td>System Type Name: </td>
            <td>
                <asp:TextBox ID="STName" runat="server"></asp:TextBox></td>
            <td></td>
            <td></td>        
        </tr>
        <tr>
            <td colspan="4">
                <asp:Label ID="STMessageLBL" runat="server" Text=""></asp:Label></td>        
        </tr>
        <tr>
            <td></td>        
            <td></td>
            <td></td>
            <td></td>
        </tr>

        <tr>
            <td colspan="3">    <table>
       <tr>
              <td>&nbsp;&nbsp;&nbsp;</td>
              <td><p style="color:Navy;"></p></td>
              <td>&nbsp;&nbsp;&nbsp;</td>
              <td><asp:Button ID="TLDeleteBTN" runat="server" Text="Delete" Enabled="false" onclick="TLDeleteBN_Click" /></td><td>
                <asp:Button ID="TLAddNewBTN" runat="server" Text="Add" onclick="TLAddNew_Click"  /></td><td>
                <asp:Button ID="TLEditBTN" runat="server" Text="Edit" Enabled="false" onclick="TLEdit_Click" /></td><td>
                <asp:Button ID="TLSaveBTN" runat="server" Text="Save" Enabled="false" onclick="TLSave_Click"   /></td><td>
                <asp:Button ID="TLCancelBTN" runat="server" Text="Cancel" Enabled="false" onclick="TLCancel_Click"  /></td><td>
                <asp:Button ID="TLPrintBTN" runat="server" Text="Print" Enabled="false" onclick="TLPrint_Click" /></td>
       </tr>
    </table></td>
        </tr>
        <tr>
            <td>Tool List</td>
            <td>
                <asp:DropDownList ID="ToolListDDL" runat="server" AutoPostBack="true" 
                    onselectedindexchanged="ToolListDDL_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td>Tool List Name: </td>
            <td>
                <asp:TextBox ID="TLName" runat="server"></asp:TextBox></td>
            <td></td>
            <td></td>        
        </tr>
        <tr>
            <td colspan="4">
                <asp:Label ID="TLMessageLBL" runat="server" Text=""></asp:Label></td>        
        </tr>
        </table>
        </asp:Panel>

</asp:Content>
