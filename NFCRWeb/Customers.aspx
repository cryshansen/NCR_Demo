<%@ Page Language="C#" MasterPageFile="~/PackersPlus.Master" AutoEventWireup="true" CodeBehind="Customers.aspx.cs" Inherits="NCRFTRWeb.WebForm2" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style2
        {
            height: 66px;
        }
    </style>
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

      <asp:Panel ID="CustomerPanel" runat="server">
          <table>
              <tr>
                  <td colspan="3">
                  <asp:Label ID="MessageLBL" runat="server" ForeColor="#990000"></asp:Label>
                  </td>
              </tr>
              <tr style="background-color:Menu;">
                  <td class="style2">
                      <strong>Customer:</strong></td>
                  <td class="style2">
                      <asp:DropDownList ID="CustomerDDL" runat="server" AutoPostBack="true" 
                          onselectedindexchanged="CustomerDDL_SelectedIndexChanged">
                      </asp:DropDownList>

                  
                  </td>
                  <td class="style2">
                      &nbsp;</td>
              </tr>
              <tr>
                  <td>
                      Company:</td>
                  <td>
                      <asp:TextBox ID="customer_company" runat="server"></asp:TextBox>
                  </td>
                  <td>
                      &nbsp;</td>
              </tr>
              <tr>
                  <td>
                      Contact Name:</td>
                  <td>
                      <asp:TextBox ID="customer_contact" runat="server"></asp:TextBox>
                  </td>
                  <td>
                      &nbsp;</td>
              </tr>
              <tr>
                  <td>
                      Contact Phone:</td>
                  <td>
                      <asp:TextBox ID="customer_contactphone" runat="server"></asp:TextBox>
                  </td>
                  <td>
                      &nbsp;</td>
              </tr>
              <tr>
                  <td>
                      Contact Extension:</td>
                  <td>
                      <asp:TextBox ID="customerExtension" runat="server"></asp:TextBox>
                  </td>
                  <td>
                      &nbsp;</td>
              </tr>
              <tr>
                  <td>
                      Contact Fax:</td>
                  <td>
                      <asp:TextBox ID="customerFax" runat="server"></asp:TextBox>
                  </td>
                  <td>
                      &nbsp;</td>
              </tr>
              <tr>
                  <td>
                      Contact Email:</td>
                  <td>
                      <asp:TextBox ID="customer_contactemail" runat="server"></asp:TextBox>
                  </td>
                  <td>
                  </td>
              </tr>
          </table>
      </asp:Panel>

 

</asp:Content>
