<%@ Page Language="C#" MasterPageFile="~/PackersPlus.Master" AutoEventWireup="true" CodeBehind="Locations.aspx.cs" Inherits="NCRFTRWeb.WebForm1" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">
      <div style="background-color:Menu; text-align:center">
      <table>
       <tr>
              <td>&nbsp;&nbsp;&nbsp;</td>
              <td><p style="color:Navy;"></p></td>
              <td>&nbsp;&nbsp;&nbsp;</td>
              <td><asp:Button ID="DeleteBN" runat="server" Text="Delete" Enabled="false" onclick="DeleteBN_Click" /></td>
              <td><asp:Button ID="AddNew" runat="server" Text="Add" Enabled="true" onclick="AddNew_Click" /></td>
              <td><asp:Button ID="Edit" runat="server" Text="Edit" Enabled="false" onclick="Edit_Click" /></td>
              <td><asp:Button ID="Save" runat="server" Text="Save" Enabled="false" onclick="Save_Click"  /></td>
              <td><asp:Button ID="Cancel" runat="server" Text="Cancel" Enabled="false" onclick="Cancel_Click" /></td>
              <td><asp:Button ID="Print" runat="server" Text="Print" Enabled="false" onclick="Print_Click" /></td>
       </tr>
    </table>
    </div>
<asp:Panel ID="LocationPanel" runat="server">
<table>
    <tr>
    <td colspan="3"><asp:Label ID="MessageLBL" runat="server" Text=""></asp:Label></td>
    </tr>
     <tr>
            <td><strong>Location:</strong></td>
            <td>
                <asp:DropDownList ID="LocationDDL" runat="server" Height="18px" Width="129px" 
                    onselectedindexchanged="LocationDDL_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
                        </td>
            <td></td>
        </tr>

        <tr>
            <td>District</td>
            <td>
                <asp:TextBox ID="district" runat="server"></asp:TextBox>
                    </td>
            <td></td>
        </tr>
        <tr>
            <td>Manager:</td>
            <td>
                <asp:TextBox ID="managerName" runat="server"></asp:TextBox>
                    </td>
            <td></td>
        </tr>
        <tr>
            <td>Address</td>
            <td>
                <asp:TextBox ID="address" runat="server"></asp:TextBox>
                    </td>
            <td></td>
        </tr>
        <tr>
            <td>City</td>
            <td>
                <asp:TextBox ID="City" runat="server"></asp:TextBox>
                    </td>
            <td></td>
        </tr>
        <tr>
            <td>Province / State:</td>
            <td>
                <asp:TextBox ID="Province" runat="server"></asp:TextBox>
                    </td>
            <td></td>
        </tr> 
    </table>  
</asp:Panel>
</asp:Content>
