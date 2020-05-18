<%@ Page Language="C#" MasterPageFile="~/PackersPlus.Master" AutoEventWireup="true" CodeBehind="Problems.aspx.cs" Inherits="NCRFTRWeb.WebForm6" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">
          <div style="background-color:Menu; text-align:center">
      
    </div>

    <asp:Panel ID="ProblemPanel" runat="server">
    <table>
        <tr>
            <td colspan="3" align="center">
            <table>
                <tr>
              
                    <td><asp:Button ID="DeleteBN" runat="server" Text="Delete" Enabled="false" onclick="DeleteBN_Click" /></td><td>
                    <asp:Button ID="AddNew" runat="server" Text="Add" onclick="AddNew_Click"  /></td><td>
                    <asp:Button ID="Edit" runat="server" Text="Edit" Enabled="false" onclick="Edit_Click" /></td><td>
                    <asp:Button ID="Save" runat="server" Text="Save" Enabled="false" onclick="Save_Click"   /></td><td>
                    <asp:Button ID="Cancel" runat="server" Text="Cancel" Enabled="false" onclick="Cancel_Click"  /></td><td>
                    <asp:Button ID="Print" runat="server" Text="Print" Enabled="false" onclick="Print_Click" /></td>
                </tr>
            </table>
        </td>
       </tr>
        <tr>
            <td>Problem Occurred During</td>
            <td>
                <asp:DropDownList ID="ProbOccurDDL" runat="server" AutoPostBack="true" 
                    onselectedindexchanged="ProbOccurDDL_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td></td>
        </tr>
        <tr>
            <td>Name</td>
            <td>
                <asp:TextBox ID="ProblemName" runat="server"></asp:TextBox>
            </td>
            <td></td>
        </tr>
        <tr>
            <td>Definition</td>
            <td colspan="2">
                <textarea id="Definition"  height=76px width=199px runat="server" onclick="return Definition_onclick()"></textarea>
            </td>
            
        </tr>
        <tr>
            <td colspan="3">
                <asp:Label ID="MessageLBL" runat="server" Text=""></asp:Label></td>
        </tr>
        </table>
          </asp:Panel>
         <asp:Panel ID="CategoryPanel" runat="server">
         <table>
        <tr>
            <td></td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td colspan="3" align="center">      
            <table>
                <tr>
                  <td><asp:Button ID="DeleteCat" runat="server" Text="Delete" Enabled="false" onclick="DeleteCat_Click" /></td><td>
                    <asp:Button ID="AddNewCat" runat="server" Text="Add" onclick="AddNewCat_Click"  /></td><td>
                    <asp:Button ID="EditCat" runat="server" Text="Edit" Enabled="false" onclick="EditCat_Click" /></td><td>
                    <asp:Button ID="SaveCat" runat="server" Text="Save" Enabled="false" onclick="SaveCat_Click"   /></td><td>
                    <asp:Button ID="CancelCat" runat="server" Text="Cancel" Enabled="false" onclick="CancelCat_Click"  /></td><td>
                    <asp:Button ID="PrintCat" runat="server" Text="Print" Enabled="false" onclick="PrintCat_Click" /></td>
               </tr>
            </table>
            </td>
        </tr>
        <tr>
            <td>Category</td>
            <td>
                <asp:DropDownList ID="CategoryDDL" runat="server" onselectedindexchanged="CategoryDDL_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
            </td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:TextBox ID="CatName" runat="server" Enabled="false" ></asp:TextBox>
            </td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:DropDownList ID="SubCategoryDDL" runat="server" AutoPostBack="true" 
                    onselectedindexchanged="SubCategoryDDL_SelectedIndexChanged" >
                </asp:DropDownList>
            </td>
            <td>
                <asp:Button ID="AddNewSub" runat="server" Text="Add Subcategory" 
                    Enabled="false" Visible="false" onclick="AddNewSub_Click"/>
               <asp:Button ID="SaveSub" runat="server" Text="Save Subcategory" 
                    Enabled="false" Visible="false" onclick="SaveSub_Click"/>
                    </td>
        </tr>
        <tr>
            <td><asp:Label ID="SubCategoryText" runat="server" Text="SubCategory Text"></asp:Label></td>
            <td>
            <asp:TextBox ID="SubCatName" runat="server" Enabled="false" Visible="false"></asp:TextBox>
            <br />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Label ID="CatMessageLBL" runat="server" Text=""></asp:Label></td>
        </tr>

    </table>
    </asp:Panel>
</asp:Content>
