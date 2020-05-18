<%@ Page Language="C#" MasterPageFile="~/PackersPlus.Master" AutoEventWireup="true" CodeBehind="Engineer.aspx.cs" Inherits="NCRFTRWeb.WebForm3" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">
      <div style="background-color:Menu; text-align:center">
      <table>
       <tr>
              <td>&nbsp;&nbsp;&nbsp;</td>
              <td><p style="color:Navy;"></p></td>
              <td>&nbsp;&nbsp;&nbsp;</td>
              <td><asp:Button ID="DeleteBN" runat="server" Text="Delete" Enabled="false" onclick="DeleteBN_Click" /></td><td>
                <asp:Button ID="AddNew" runat="server" Text="Add" onclick="AddNew_Click"  /></td><td>
                <asp:Button ID="Edit" runat="server" Text="Edit" Enabled="false" onclick="Edit_Click" /></td><td>
                <asp:Button ID="Save" runat="server" Text="Save" Enabled="false" onclick="Save_Click"   /></td><td>
                <asp:Button ID="Cancel" runat="server" Text="Cancel" Enabled="false" onclick="Cancel_Click"  /></td><td>
                <asp:Button ID="Print" runat="server" Text="Print" Enabled="false" onclick="Print_Click" /></td>
       </tr>
    </table>
    </div>
      <asp:Panel ID="EngineerPanel" runat="server">

<table>
     <tr>
        <td colspan="3">
            <asp:Label ID="MessageLBL" runat="server" Text=""></asp:Label></td>
     </tr>
     <tr>
             <td><strong>Engineer:</strong></td>
            <td>
                <asp:DropDownList ID="EngineerDDL" runat="server" Height="19px" Width="129px" 
                    onselectedindexchanged="EngineerDDL_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
                        </td>
            <td></td>
        </tr>
        <tr>
            <td>Name:</td>
            <td> <asp:TextBox ID="initiatorname" runat="server"></asp:TextBox> </td>
            <td></td>
        </tr>
        <tr>
            <td>Phone:</td>
            <td><asp:TextBox ID="initiatorphone" runat="server"></asp:TextBox> </td>
            <td></td>

        </tr>
        <tr>
        <td>Ext:</td>
            <td><asp:TextBox ID="initiatorExt" runat="server"></asp:TextBox> </td>
            <td></td>
        </tr>
        <tr>
            <td>Fax:</td>
            <td> <asp:TextBox ID="initiatorFax" runat="server"></asp:TextBox></td>
            <td></td>
        </tr>

        <tr>
            <td>Email:</td>
            <td><asp:TextBox ID="initiatoremail" runat="server"></asp:TextBox> </td>
            <td></td>
        </tr>

        </table>
        </asp:Panel>
</asp:Content>
