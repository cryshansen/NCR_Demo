<%@ Page Language="C#" MasterPageFile="~/PackersPlus.Master" AutoEventWireup="true" CodeBehind="Operators.aspx.cs" Inherits="NCRFTRWeb.WebForm5" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">
      <div style="background-color:Menu; text-align:center">
      <table>
       <tr>
              <td>&nbsp;&nbsp;&nbsp;</td>
              <td><p style="color:Navy;">&nbsp;</p></td>
              <td>&nbsp;&nbsp;&nbsp;</td>
              <td><asp:Button ID="DeleteBN" runat="server" Text="Delete" Enabled="false" onclick="DeleteBN_Click"/></td><td>
                    <asp:Button ID="AddNew" runat="server" Text="Add" onclick="AddNew_Click"  /></td><td>
                    <asp:Button ID="Edit"  runat="server" Text="Edit" Enabled="false" onclick="Edit_Click"  /></td><td>
                    <asp:Button ID="Save" runat="server" Text="Save" Enabled="False" onclick="Save_Click"   /></td><td>
                    <asp:Button ID="Cancel" runat="server" Text="Cancel" Enabled="False" onclick="Cancel_Click"  /></td><td>
                    <asp:Button ID="Print" runat="server" Text="Print" Enabled="False" onclick="Print_Click"  /></td>
       </tr>
    </table>
    </div>
<br />


<asp:Panel ID="OperatorPanel" runat="server">
    
<table>
     <tr>
        <td colspan="3"><asp:Label ID="MessageLBL" runat="server" Text=""></asp:Label></td>
     </tr>
     <tr>
            <td><strong>Operator:</strong></td>
            <td>
                <asp:DropDownList ID="OperatorDDL" runat="server" AutoPostBack="true" 
                    onselectedindexchanged="OperatorDDL_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td></td>
        </tr>
        <tr>
            <td>Operator Name:</td>
            <td><asp:TextBox ID="operatorName" runat="server"></asp:TextBox></td>
            <td></td>
        </tr>
        <tr>
            <td>Operator Email:</td>
            <td><asp:TextBox ID="operatorEmail" runat="server"></asp:TextBox></td>
            <td></td>
        </tr>
        <tr>
            <td>Operator Phone:</td>
            <td><asp:TextBox ID="operatorPhone" runat="server"></asp:TextBox></td>
            <td></td>
        </tr>
        <tr>
            <td>Operator Extention:</td>
            <td><asp:TextBox ID="operatorExt" runat="server"></asp:TextBox></td>
            <td></td>
        </tr>
        <tr>
            <td>Operator Fax:</td>
            <td><asp:TextBox ID="operatorFax" runat="server"></asp:TextBox></td>
            <td></td>
        </tr>
        </table>
    </asp:Panel>
</asp:Content>
