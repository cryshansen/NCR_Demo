using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

namespace NCRFTRWeb
{
    public partial class WebForm7 : System.Web.UI.Page
    {
        DataSet WellTypes = null;
        DataSet WellType = null;
        int WellTypeId = 0;
        DataSet CasingSizes = null;
        DataSet CasingSize = null;
        int CasingSizeId = 0;
        DataSet LinerSizes = null;
        DataSet LinerSize = null;
        int LinerSizeId = 0;
        DataSet SystemTypes = null;
        DataSet SystemType = null;
        int SystemTypeId = 0;
        DataSet ToolLists = null;
        DataSet ToolList = null;
        int ToolListId = 0;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            //loads all 5 dropdowns
            if (!IsPostBack)
            {  //when not postback, means hitting page first time.
                GetWellTypeDropDownList();
                GetCasingSizeDropDownList();
                GetLinerSizeDropDownList();
                GetSystemTypeDropDownList();
                GetToolListDropDownList();
                //MessageLBL.Text = "Not postback.";
                //DisableTextboxes();
                //initial entry of first record only AddNew
                WTAddNew.Enabled = true;
                CSAddNewBTN.Enabled = true;
                LSAddNewBTN.Enabled = true;
                STAddNewBTN.Enabled = true;
                TLAddNewBTN.Enabled = true;
                //edit and print are available only when data in database. 
                WTEdit.Enabled = true;
                WTPrint.Enabled = true;
                CSEditBTN.Enabled = true;
                CSPrintBTN.Enabled = true;
                LSEditBTN.Enabled = true;
                LSPrintBTN.Enabled = true;
                STEditBTN.Enabled = true;
                STPrintBTN.Enabled = true;
                TLEditBTN.Enabled = true;
                TLPrintBTN.Enabled = true;
            }
        }

        protected void WellTypeDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            //get selected Well Type from table
            WTMessageLBL.Text = WellTypeDDL.SelectedValue.ToString();
            int.TryParse(WellTypeDDL.SelectedValue.ToString(), out WellTypeId );
            if (WellTypeId != 0)
            {
                try
                {
                    WTMessageLBL.Text = WellTypeId.ToString();
                    WellType = NCRClass.NCRController.LookupWellType(WellTypeId);
                    WTName.Text= WellType.Tables["WellType"].Rows[0]["WellTypeName"].ToString();
                    WTEdit.Enabled = true;
                    WTAddNew.Enabled = false;
                    WTSave.Enabled = false;
                    WTDeleteBN.Enabled = false;
                    //all textbox Items are Disabled
                    WTName.Enabled = false;
                }
                catch (Exception ex)
                {
                    WTMessageLBL.Text = "There is no record for that Well Type." + ex.Message.ToString();
                }
            }
            else
            {
                WTMessageLBL.Text = "You must either select a Well Type or click the add Button to add a new Well Type.";
                WTAddNew.Enabled = true;
            }
            WTMessageLBL.Text = WellTypeDDL.SelectedValue.ToString();
        }

        protected void CasingSizeDDL_SelectedIndexChanged(object sender, EventArgs e)
        {//get selected Casing Size from table
            CSMessageLBL.Text= CasingSizeDDL.SelectedValue.ToString();
            int.TryParse(CasingSizeDDL.SelectedValue.ToString(), out CasingSizeId);
            if (CasingSizeId != 0)
            {
                try
                {
                    CSMessageLBL.Text = CasingSizeId.ToString();
                    CasingSize = NCRClass.NCRController.LookupCasingSize(CasingSizeId);
                    CSName.Text = CasingSize.Tables["CasingSize"].Rows[0]["CasingSizeName"].ToString();
                    CSEditBTN.Enabled = true;
                    CSAddNewBTN.Enabled = false;
                    CSSaveBTN.Enabled = false;
                    CSDeleteBTN.Enabled = false;
                    //all textbox Items are Disabled
                    CSName.Enabled = false;
                }
                catch (Exception ex)
                {

                    CSMessageLBL.Text = "There is no record for that Casing Size." + ex.Message.ToString();
                }
            }
            else
            {
                CSMessageLBL.Text = "You must either select a Casing Size or click the add Button to add a new Casing Size.";
                CSAddNewBTN.Enabled = true;
            }
            CSMessageLBL.Text = CasingSizeDDL.SelectedValue.ToString();
        }

        protected void LinerSizeDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            //get selected Liner Size from table
            LSMessageLBL.Text = LinerSizeDDL.SelectedValue.ToString();
            int.TryParse(LinerSizeDDL.SelectedValue.ToString(), out LinerSizeId );
            if (LinerSizeId != 0)
            {
                try
                {

                    LSMessageLBL.Text = LinerSizeId.ToString();
                    LinerSize = NCRClass.NCRController.LookupLinerSize(LinerSizeId);
                    LSName.Text = LinerSize.Tables["LinerSize"].Rows[0]["LinerSizeName"].ToString();
                    LSEditBTN.Enabled = true;
                    LSAddNewBTN.Enabled = false;
                    LSSaveBTN.Enabled = false;
                    LSDeleteBTN.Enabled = false;
                    //all textbox Items are Disabled
                    LSName.Enabled = false;
                }
                catch (Exception ex)
                {

                    LSMessageLBL.Text = "There is no record for that Liner Size." + ex.Message.ToString();
                }
            }
            else
            {
                LSMessageLBL.Text = "You must either select a Liner Size or click the add Button to add a new Liner Size.";
                LSAddNewBTN.Enabled = true;
            }
            LSMessageLBL.Text = LinerSizeDDL.SelectedValue.ToString();
        }

        protected void SystemTypeDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            //get selected System Type from table
            STMessageLBL.Text = SystemTypeDDL.SelectedValue.ToString();
            int.TryParse(SystemTypeDDL.SelectedValue.ToString(), out SystemTypeId);
            if (SystemTypeId != 0)
            {
                try
                {
                    STMessageLBL.Text= SystemTypeId.ToString();
                    SystemType = NCRClass.NCRController.LookupSystemType(SystemTypeId);
                    STName.Text = SystemType.Tables["SystemType"].Rows[0]["SystemTypeName"].ToString();
                    STEditBTN.Enabled = true;
                    STAddNewBTN.Enabled = false;
                    STSaveBTN.Enabled = false;
                    STDeleteBTN.Enabled = false;
                    //all textbox Items are Disabled
                    STName.Enabled = false;
                }
                catch (Exception ex)
                {

                    STMessageLBL.Text = "There is no record for that System Type." + ex.Message.ToString();
                }
            }
            else
            {
                STMessageLBL.Text = "You must either select a System Type or click the add Button to add a new System Type.";
                STAddNewBTN.Enabled = true;
            }
            STMessageLBL.Text = SystemTypeDDL.SelectedValue.ToString();
        }

        protected void ToolListDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            //get selected Tool List from table
            TLMessageLBL.Text= SystemTypeDDL.SelectedValue.ToString();
            int.TryParse(ToolListDDL.SelectedValue.ToString(), out ToolListId);
            if (ToolListId != 0)
            {
                try
                {
                    TLMessageLBL.Text = ToolListId.ToString();
                    ToolList = NCRClass.NCRController.LookupToolList(ToolListId);
                    TLName.Text = ToolList.Tables["ToolList"].Rows[0]["ToolListName"].ToString();
                    TLEditBTN.Enabled = true;
                    TLAddNewBTN.Enabled = false;
                    TLSaveBTN.Enabled = false;
                    TLDeleteBTN.Enabled = false;
                    //all textbox Items are Disabled
                    TLName.Enabled = false;
                }
                catch (Exception ex)
                {

                    TLMessageLBL.Text = "There is no record for that Tool List." + ex.Message.ToString();
                }
            }
            else
            {
                TLMessageLBL.Text = "You must either select a Tool List or click the add Button to add a new Tool List.";
                TLAddNewBTN.Enabled = true;
            }
            TLMessageLBL.Text = ToolListDDL.SelectedValue.ToString();
        }

        protected void GetWellTypeDropDownList()
        {
            WellTypes   = NCRClass.NCRController.LookupAllWellTypes();
            WellTypeDDL.DataSource = WellTypes.Tables["WellTypes"];
            WellTypeDDL.DataTextField = "WellTypeName";
            WellTypeDDL.DataValueField = "WellID";
            WellTypeDDL.DataBind();
            WellTypeDDL.Items.Insert(0, " ");
        }
        protected void GetCasingSizeDropDownList()
        {
            CasingSizes = NCRClass.NCRController.LookupAllCasingSizes();
            CasingSizeDDL.DataSource = CasingSizes.Tables["CasingSizes"];
            CasingSizeDDL.DataTextField = "CasingSizeName";
            CasingSizeDDL.DataValueField = "CasingID";
            CasingSizeDDL.DataBind();
            CasingSizeDDL.Items.Insert(0,"");

        }
        protected void GetLinerSizeDropDownList()
        {
            LinerSizes = NCRClass.NCRController.LookupAllLinerSizes();
            LinerSizeDDL.DataSource = LinerSizes.Tables["LinerSizes"];
            LinerSizeDDL.DataTextField = "LinerSizeName";
            LinerSizeDDL.DataValueField = "LinerID";
            LinerSizeDDL.DataBind();
            LinerSizeDDL.Items.Insert(0, "");
        }
        protected void GetSystemTypeDropDownList()
        {
            SystemTypes = NCRClass.NCRController.LookupAllSystemTypes();
            SystemTypeDDL.DataSource = SystemTypes.Tables["SystemTypes"];
            SystemTypeDDL.DataTextField = "SystemTypeName";
            SystemTypeDDL.DataValueField = "SystemTypeID";
            SystemTypeDDL.DataBind();
            SystemTypeDDL.Items.Insert(0, "");
        }
        protected void GetToolListDropDownList()
        {
            ToolLists = NCRClass.NCRController.LookupAllToolLists();
            ToolListDDL.DataSource = ToolLists.Tables["ToolLists"];
            ToolListDDL.DataTextField = "ToolListName";
            ToolListDDL.DataValueField = "ToolListID";
            ToolListDDL.DataBind();
            ToolListDDL.Items.Insert(0,"");
        }

        protected void WTDeleteBN_Click(object sender, EventArgs e)
        {
            int.TryParse(WellTypeDDL.SelectedValue.ToString(), out WellTypeId);
            bool wellDelete = NCRClass.NCRController.DeleteWellType(WellTypeId);
            if (wellDelete == false)
            {
                WTMessageLBL.Text = "The Well Type Delete Failed. Please contact your administrator.";
            }
            else
            {
                WTMessageLBL.Text = "The Well Type " + WTName.Text + " was deleted succesfully.";
                //once successfull should go back to main view mode again.
                GetWellTypeDropDownList();
                WTName.Text = "";
                //all textbox Items are Disabled
                WTName.Enabled = false;
                //initial entry of first record only AddNew
                WTAddNew.Enabled = true;
                //edit and print are available only when data in database. 
                WTEdit.Enabled = true;
                WTPrint.Enabled = true;
                WTDeleteBN.Enabled = false;
                WTCancel.Enabled = false;
                WTSave.Enabled = false;
            }
        }

        protected void WTAddNew_Click(object sender, EventArgs e)
        {
            //when adding a new record, the save cancel buttons are enabled no others 
            //must clear textboxes
            WTName.Text = "";
            if (WellTypeId == 0)
            {
                WTName.Enabled = true;
                WellTypeDDL.SelectedIndex = 0;
                //change buttons
                WTAddNew.Enabled = false;
                WTSave.Enabled = true;
                WTCancel.Enabled = true;
                WTEdit.Enabled = false;
                //this will need to clear textboxes bc sometimes there will be some in there from dropdown select?
                WTMessageLBL.Text = "AddNew? First or second.";
            }
            else
            {
                //get the dropdown again 
                GetWellTypeDropDownList();
                WellTypeDDL.SelectedIndex = 0;
                //change buttons
                WTAddNew.Enabled = false;
                WTSave.Enabled = true;
                WTCancel.Enabled = true;
                WTEdit.Enabled = false;
                WTName.Enabled = true;
                //this will need to clear textboxes bc sometimes there will be some in there from dropdown select?
                WTMessageLBL.Text = "You must select the empty value from the dropdown first.";

            }
        }

        protected void WTEdit_Click(object sender, EventArgs e)
        {
            //Save cancel Print delete enabled
            //this is to enable all text fields so as to edit the content of the customer not actually update?
            WTName.Enabled = true;
            WTSave.Enabled = true;
            WTAddNew.Enabled = false;
            WTEdit.Enabled = false; 
            WTDeleteBN.Enabled = true;
            WTCancel.Enabled = true;
            WTPrint.Enabled = true;
            WTMessageLBL.Text= "Edit First or second.";
        }

        protected void WTSave_Click(object sender, EventArgs e)
        {
            int.TryParse(WellTypeDDL.SelectedValue.ToString(), out WellTypeId);

            if (WellTypeId == 0)
            { //adding a new Well Type
                //should we have a try catch here to adjust buttons/textboxes if fails?
                WellTypeId = NCRClass.NCRController.AddWellType(WTName.Text);
                if (WellTypeId > 0)
                {
                    WTMessageLBL.Text = "Your Well Type " + WTName.Text + " has been added check the dropdown.";
                    //buttons should be same as on initial pageload on success
                    WTName.Text = "";
                    WTName.Enabled = false;
                    GetWellTypeDropDownList();
                    WellTypeDDL.SelectedValue = WellTypeId.ToString();
                    //once successfull buttons change
                    WTAddNew.Enabled = true;
                    //edit and print are available only when data in database. initial entry of first record only AddNew
                    WTEdit.Enabled = true;
                    WTPrint.Enabled = true;
                    WTDeleteBN.Enabled = false;
                    WTSave.Enabled = false;
                    WTCancel.Enabled = false;
                }
            }
            else
            {
                //updating Well Type
                try
                {

                    NCRClass.NCRController.UpdateWellType(WellTypeId, WTName.Text);
                    WTMessageLBL.Text = WellTypeId + " Your Well Type " + WTName.Text + " has been updated.";
                    //buttons should be same as on initial pageload on success
                    //we updated it was successful but delete button is avail and save is avail 
                    //when we should be back to view well types instance
                    WTName.Enabled = false;
                    GetWellTypeDropDownList();
                    WellTypeDDL.SelectedValue = WellTypeId.ToString();
                    //once successfull buttons change
                    WTAddNew.Enabled = true;
                    //edit and print are available only when data in database. initial entry of first record only AddNew
                    WTEdit.Enabled = true;
                    WTPrint.Enabled = true;
                    WTDeleteBN.Enabled = false;
                    WTSave.Enabled = false;
                    WTCancel.Enabled = false;
                }
                catch (Exception ex)
                {//update failed
                    WTMessageLBL.Text = ex.ToString();
                    WTEdit.Enabled = false;
                    WTSave.Enabled = false;
                    WTDeleteBN.Enabled = false;
                }
            }
        }

        protected void WTCancel_Click(object sender, EventArgs e)
        {
            //all textbox Items are Emptied
            WTName.Text = "";
            //all textbox Items are Disabled
            WTName.Enabled = false;
            //Cancel.Attributes.Add("onClick","javascript:history.back();");
            GetWellTypeDropDownList();
            WTAddNew.Enabled = true;
            //edit and print are available only when data in database. initial entry of first record only AddNew
            WTEdit.Enabled = true;
            WTPrint.Enabled = true;
            WTDeleteBN.Enabled = false;
            WTCancel.Enabled = false;
            WTSave.Enabled = false;
        }

        protected void WTPrint_Click(object sender, EventArgs e)
        {
            try
            {
                //hierarchicalGroupingReport.PrintToPrinter(1, false, 1, 99);
                Session["ctrl"] = MiscPanel;
                ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('Print.aspx','PrintMe','height=300px,width=300px,scrollbars=1');</script>");

            }
            catch (Exception ex)
            {
                WTMessageLBL.Text= ex.Message;
            }

        }

        protected void CSDeleteBN_Click(object sender, EventArgs e)
        {
            int.TryParse(CasingSizeDDL.SelectedValue.ToString(), out CasingSizeId );
            bool casingDelete = NCRClass.NCRController.DeleteCasingSize(CasingSizeId);
            if (casingDelete == false)
            {
                
                CSMessageLBL.Text = "The Casing Size Delete Failed. Please contact your administrator.";
            }
            else
            {
                CSMessageLBL.Text = "The Casing Size " + CSName.Text + " was deleted succesfully.";
                //once successfull should go back to main view mode again.
                GetCasingSizeDropDownList();
                CSName.Text = "";
                //all textbox Items are Disabled
                CSName.Enabled = false;
                //initial entry of first record only AddNew
                CSAddNewBTN.Enabled = true;
                //edit and print are available only when data in database. 
                CSEditBTN.Enabled = true;
                CSPrintBTN.Enabled = true;
                CSDeleteBTN.Enabled = false;
                CSCancelBTN.Enabled = false;
                CSSaveBTN.Enabled = false;
            }
        }

        protected void CSAddNew_Click(object sender, EventArgs e)
        {
            //when adding a new record, the save cancel buttons are enabled no others 
            //must clear textboxes
            CSName.Text = "";
            if (CasingSizeId == 0)
            {
                CSName.Enabled = true;
                CasingSizeDDL.SelectedIndex = 0;
                //change buttons
                CSAddNewBTN.Enabled = false;
                CSSaveBTN.Enabled = true;
                CSCancelBTN.Enabled = true;
                CSEditBTN.Enabled = false;
                //this will need to clear textboxes bc sometimes there will be some in there from dropdown select?
                CSMessageLBL.Text = "AddNew? First or second.";
            }
            else
            {
                //get the dropdown again 
                GetCasingSizeDropDownList();
                CasingSizeDDL.SelectedIndex = 0;
                //change buttons
                CSAddNewBTN.Enabled = false;
                CSSaveBTN.Enabled = true;
                CSCancelBTN.Enabled = true;
                CSEditBTN.Enabled = false;
                CSName.Enabled = true;
                //this will need to clear textboxes bc sometimes there will be some in there from dropdown select?
                CSMessageLBL.Text = "You must select the empty value from the dropdown first.";

            }
        }

        protected void CSEdit_Click(object sender, EventArgs e)
        {
            //Save cancel Print delete enabled
            //this is to enable all text fields so as to edit the content of the customer not actually update?
            CSName.Enabled=true;
            CSSaveBTN.Enabled = true;
            CSAddNewBTN.Enabled = false;
            CSEditBTN.Enabled = false;
            CSDeleteBTN.Enabled = true;
            CSCancelBTN.Enabled = true;
            CSPrintBTN.Enabled = true;
            CSMessageLBL.Text= "Edit First or second.";
        }

        protected void CSSave_Click(object sender, EventArgs e)
        {
            int.TryParse(CasingSizeDDL.SelectedValue.ToString(), out CasingSizeId);

            if (CasingSizeId == 0)
            { //adding a new Casing Size
                //should we have a try catch here to adjust buttons/textboxes if fails?
                CasingSizeId = NCRClass.NCRController.AddCasingSize(CSName.Text);
                if (CasingSizeId > 0)
                {
                    CSMessageLBL.Text = "Your Casing Size " + CSName.Text + " has been added check the dropdown.";
                    //buttons should be same as on initial pageload on success
                    CSName.Text = "";
                    CSName.Enabled = false;
                    GetCasingSizeDropDownList();
                    CasingSizeDDL.SelectedValue = CasingSizeId.ToString();
                    //once successfull buttons change
                    CSAddNewBTN.Enabled = true;
                    //edit and print are available only when data in database. initial entry of first record only AddNew
                    CSEditBTN.Enabled = true;
                    CSPrintBTN.Enabled = true;
                    CSDeleteBTN.Enabled = false;
                    CSSaveBTN.Enabled = false;
                    CSCancelBTN.Enabled = false;
                }
            }
            else
            {
                //updating Casing Size
                try
                {

                    NCRClass.NCRController.UpdateCasingSize(CasingSizeId, CSName.Text);
                    CSMessageLBL.Text = CasingSizeId + " Your Casing Size " + CSName.Text + " has been updated.";
                    //buttons should be same as on initial pageload on success
                    //we updated it was successful but delete button is avail and save is avail 
                    //when we should be back to view well types instance
                    CSName.Enabled = false;
                    GetCasingSizeDropDownList();
                    CasingSizeDDL.SelectedValue = CasingSizeId.ToString();
                    //once successfull buttons change
                    CSAddNewBTN.Enabled = true;
                    //edit and print are available only when data in database. initial entry of first record only AddNew
                    CSEditBTN.Enabled = true;
                    CSPrintBTN.Enabled = true;
                    CSDeleteBTN.Enabled = false;
                    CSSaveBTN.Enabled = false;
                    CSCancelBTN.Enabled = false;
                }
                catch (Exception ex)
                {//update failed
                    CSMessageLBL.Text = ex.ToString();
                    CSEditBTN.Enabled = false;
                    CSSaveBTN.Enabled = false;
                    CSDeleteBTN.Enabled = false;
                }
            }
        }

        protected void CSCancel_Click(object sender, EventArgs e)
        {//all textbox Items are Emptied
            CSName.Text = "";
            //all textbox Items are Disabled
            CSName.Enabled = false;
            //Cancel.Attributes.Add("onClick","javascript:history.back();");
            GetCasingSizeDropDownList();
            CSAddNewBTN.Enabled = true;
            //edit and print are available only when data in database. initial entry of first record only AddNew
            CSEditBTN.Enabled = true;
            CSPrintBTN.Enabled = true;
            CSDeleteBTN.Enabled = false;
            CSCancelBTN.Enabled = false;
            CSSaveBTN.Enabled = false;
        }

        protected void CSPrint_Click(object sender, EventArgs e)
        {
            try
            {
                //hierarchicalGroupingReport.PrintToPrinter(1, false, 1, 99);
                Session["ctrl"] = MiscPanel;
                ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('Print.aspx','PrintMe','height=300px,width=300px,scrollbars=1');</script>");

            }
            catch (Exception ex)
            {
                CSMessageLBL.Text= ex.Message;
            }
        }
        protected void LSDeleteBN_Click(object sender, EventArgs e)
        {
            int.TryParse(LinerSizeDDL.SelectedValue.ToString(), out LinerSizeId );
            bool linerDelete = NCRClass.NCRController.DeleteLinerSize(LinerSizeId);
            if (linerDelete == false)
            {

                LSMessageLBL.Text = "The Liner Size Delete Failed. Please contact your administrator.";
            }
            else
            {
                LSMessageLBL.Text = "The Liner Size " + LSName.Text + " was deleted succesfully.";
                //once successfull should go back to main view mode again.
                GetLinerSizeDropDownList();
                LSName.Text = "";
                //all textbox Items are Disabled
                LSName.Enabled = false;
                //initial entry of first record only AddNew
                LSAddNewBTN.Enabled = true;
                //edit and print are available only when data in database. 
                LSEditBTN.Enabled = true;
                LSPrintBTN.Enabled = true;
                LSDeleteBTN.Enabled = false;
                LSCancelBTN.Enabled = false;
                LSSaveBTN.Enabled = false;
            }

        }
        protected void LSAddNew_Click(object sender, EventArgs e)
        {
            //when adding a new record, the save cancel buttons are enabled no others 
            //must clear textboxes
            LSName.Text = "";
            if (LinerSizeId == 0)
            {
                LSName.Enabled = true;
                LinerSizeDDL.SelectedIndex = 0;
                //change buttons
                LSAddNewBTN.Enabled = false;
                LSSaveBTN.Enabled = true;
                LSCancelBTN.Enabled = true;
                LSEditBTN.Enabled = false;
                //this will need to clear textboxes bc sometimes there will be some in there from dropdown select?
                LSMessageLBL.Text = "AddNew? First or second.";
            }
            else
            {
                //get the dropdown again 
                GetLinerSizeDropDownList();
                LinerSizeDDL.SelectedIndex = 0;
                //change buttons
                LSAddNewBTN.Enabled = false;
                LSSaveBTN.Enabled = true;
                LSCancelBTN.Enabled = true;
                LSEditBTN.Enabled = false;
                LSName.Enabled = true;
                //this will need to clear textboxes bc sometimes there will be some in there from dropdown select?
                LSMessageLBL.Text = "You must select the empty value from the dropdown first.";

            }
        }

        protected void LSEdit_Click(object sender, EventArgs e)
        {
            //Save cancel Print delete enabled
            //this is to enable all text fields so as to edit the content of the customer not actually update?
            LSName.Enabled = true;
            LSSaveBTN.Enabled = true;
            LSAddNewBTN.Enabled = false;
            LSEditBTN.Enabled = false;
            LSDeleteBTN.Enabled = true;
            LSCancelBTN.Enabled = true;
            LSPrintBTN.Enabled = true;
            LSMessageLBL.Text = "Edit First or second.";
        }

        protected void LSSave_Click(object sender, EventArgs e)
        {
            int.TryParse(LinerSizeDDL.SelectedValue.ToString(), out LinerSizeId);

            if (LinerSizeId == 0)
            { //adding a new Liner Size
                //should we have a try catch here to adjust buttons/textboxes if fails?
                LinerSizeId = NCRClass.NCRController.AddLinerSize(LSName.Text);
                if (LinerSizeId > 0)
                {
                    LSMessageLBL.Text = "Your Liner Size " + LSName.Text + " has been added check the dropdown.";
                    //buttons should be same as on initial pageload on success
                    LSName.Text = "";
                    LSName.Enabled = false;
                    GetLinerSizeDropDownList();
                    LinerSizeDDL.SelectedValue = LinerSizeId.ToString();
                    //once successfull buttons change
                    LSAddNewBTN.Enabled = true;
                    //edit and print are available only when data in database. initial entry of first record only AddNew
                    LSEditBTN.Enabled = true;
                    LSPrintBTN.Enabled = true;
                    LSDeleteBTN.Enabled = false;
                    LSSaveBTN.Enabled = false;
                    LSCancelBTN.Enabled = false;
                }
            }
            else
            {
                //updating Liner Size
                try
                {

                    NCRClass.NCRController.UpdateLinerSize(LinerSizeId, LSName.Text);
                    LSMessageLBL.Text = LinerSizeId + " Your Liner Size " + LSName.Text + " has been updated.";
                    //buttons should be same as on initial pageload on success
                    //we updated it was successful but delete button is avail and save is avail 
                    //when we should be back to view well types instance
                    LSName.Enabled = false;
                    GetLinerSizeDropDownList();
                    LinerSizeDDL.SelectedValue = LinerSizeId.ToString();
                    //once successfull buttons change
                    LSAddNewBTN.Enabled = true;
                    //edit and print are available only when data in database. initial entry of first record only AddNew
                    LSEditBTN.Enabled = true;
                    LSPrintBTN.Enabled = true;
                    LSDeleteBTN.Enabled = false;
                    LSSaveBTN.Enabled = false;
                    LSCancelBTN.Enabled = false;
                }
                catch (Exception ex)
                {//update failed
                    LSMessageLBL.Text = ex.ToString();
                    LSEditBTN.Enabled = false;
                    LSSaveBTN.Enabled = false;
                    LSDeleteBTN.Enabled = false;
                }
            }
        }

        protected void LSCancel_Click(object sender, EventArgs e)
        {//all textbox Items are Emptied
            LSName.Text = "";
            //all textbox Items are Disabled
            LSName.Enabled = false;
            //Cancel.Attributes.Add("onClick","javascript:history.back();");
            GetLinerSizeDropDownList();
            LSAddNewBTN.Enabled = true;
            //edit and print are available only when data in database. initial entry of first record only AddNew
            LSEditBTN.Enabled = true;
            LSPrintBTN.Enabled = true;
            LSDeleteBTN.Enabled = false;
            LSCancelBTN.Enabled = false;
            LSSaveBTN.Enabled = false;
        }

        protected void LSPrint_Click(object sender, EventArgs e)
        {
            try
            {
                //hierarchicalGroupingReport.PrintToPrinter(1, false, 1, 99);
                Session["ctrl"] = MiscPanel;
                ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('Print.aspx','PrintMe','height=300px,width=300px,scrollbars=1');</script>");

            }
            catch (Exception ex)
            {
                LSMessageLBL.Text= ex.Message;
            }
        }

        protected void STDeleteBN_Click(object sender, EventArgs e)
        {
            int.TryParse(SystemTypeDDL.SelectedValue.ToString(), out SystemTypeId  );
            bool systemDelete = NCRClass.NCRController.DeleteSystemType(SystemTypeId);
            if (systemDelete == false)
            {
                
                STMessageLBL.Text = "The System Type Delete Failed. Please contact your administrator.";
            }
            else
            {
                STMessageLBL.Text = "The System Type " + STName.Text + " was deleted succesfully.";
                //once successfull should go back to main view mode again.
                GetSystemTypeDropDownList();
                STName.Text = "";
                //all textbox Items are Disabled
                STName.Enabled = false;
                //initial entry of first record only AddNew
                STAddNewBTN.Enabled = true;
                //edit and print are available only when data in database. 
                STEditBTN.Enabled = true;
                STPrintBTN.Enabled = true;
                STDeleteBTN.Enabled = false;
                STCancelBTN.Enabled = false;
                STSaveBTN.Enabled = false;
            }

        }

        protected void STAddNew_Click(object sender, EventArgs e)
        {
            //when adding a new record, the save cancel buttons are enabled no others 
            //must clear textboxes
            STName.Text = "";
            if (SystemTypeId == 0)
            {
               STName.Enabled = true;
               SystemTypeDDL.SelectedIndex = 0;
                //change buttons
                STAddNewBTN.Enabled = false;
                STSaveBTN.Enabled = true;
                STCancelBTN.Enabled = true;
                STEditBTN.Enabled = false;
                //this will need to clear textboxes bc sometimes there will be some in there from dropdown select?
                STMessageLBL.Text = "AddNew? First or second.";
            }
            else
            {
                //get the dropdown again 
                GetSystemTypeDropDownList();
                SystemTypeDDL.SelectedIndex = 0;
                //change buttons
                STAddNewBTN.Enabled = false;
                STSaveBTN.Enabled = true;
                STCancelBTN.Enabled = true;
                STEditBTN.Enabled = false;
                //this will need to clear textboxes bc sometimes there will be some in there from dropdown select?
                STMessageLBL.Text = "You must select the empty value from the dropdown first.";
            }
        }

        protected void STEdit_Click(object sender, EventArgs e)
        {
            //Save cancel Print delete enabled
            //this is to enable all text fields so as to edit the content of the customer not actually update?
            STName.Enabled=true;
            STSaveBTN.Enabled=true;
            STAddNewBTN.Enabled=false;
            STEditBTN.Enabled=false;
            STDeleteBTN.Enabled=true;
            STCancelBTN.Enabled=true;
            STPrintBTN.Enabled=true;
            STMessageLBL.Text= "Edit First or second.";
        }

        protected void STSave_Click(object sender, EventArgs e)
        {
            int.TryParse(SystemTypeDDL.SelectedValue.ToString(), out SystemTypeId);

            if (SystemTypeId == 0)
            { //adding a new System Type
                //should we have a try catch here to adjust buttons/textboxes if fails?
                SystemTypeId = NCRClass.NCRController.AddSystemType(STName.Text);
                if (SystemTypeId > 0)
                {
                    STMessageLBL.Text = "Your System Type " + STName.Text + " has been added check the dropdown.";
                    //buttons should be same as on initial pageload on success
                    STName.Text = "";
                    STName.Enabled = false;
                    GetSystemTypeDropDownList();
                    SystemTypeDDL.SelectedValue = SystemTypeId.ToString();
                    //once successfull buttons change
                    STAddNewBTN.Enabled = true;
                    //edit and print are available only when data in database. initial entry of first record only AddNew
                    STEditBTN.Enabled = true;
                    STPrintBTN.Enabled = true;
                    STDeleteBTN.Enabled = false;
                    STSaveBTN.Enabled = false;
                    STCancelBTN.Enabled = false;
                }
            }
            else
            {
                //updating System Type
                try
                {

                    NCRClass.NCRController.UpdateSystemType(SystemTypeId, STName.Text);
                    STMessageLBL.Text = SystemTypeId + " Your System Type " + STName.Text + " has been updated.";
                    //buttons should be same as on initial pageload on success
                    //we updated it was successful but delete button is avail and save is avail 
                    //when we should be back to view well types instance
                    STName.Enabled = false;
                    GetSystemTypeDropDownList();
                    SystemTypeDDL.SelectedValue = SystemTypeId.ToString();
                    //once successfull buttons change
                    STAddNewBTN.Enabled = true;
                    //edit and print are available only when data in database. initial entry of first record only AddNew
                   STEditBTN.Enabled = true;
                    STPrintBTN.Enabled = true;
                    STDeleteBTN.Enabled = false;
                    STSaveBTN.Enabled = false;
                    STCancelBTN.Enabled = false;
                }
                catch (Exception ex)
                {//update failed
                    STMessageLBL.Text = ex.ToString();
                    STEditBTN.Enabled = false;
                    STSaveBTN.Enabled = false;
                    STDeleteBTN.Enabled = false;
                }
            }
        }

        protected void STCancel_Click(object sender, EventArgs e)
        {   //all textbox Items are Emptied
            STName.Text = "";
            //all textbox Items are Disabled
            STName.Enabled = false;
            //Cancel.Attributes.Add("onClick","javascript:history.back();");
            GetSystemTypeDropDownList();
            STAddNewBTN.Enabled = true;

            //edit and print are available only when data in database. initial entry of first record only AddNew
            STEditBTN.Enabled = true;
            STPrintBTN.Enabled = true;
            STDeleteBTN.Enabled = false;
            STCancelBTN.Enabled = false;
            STSaveBTN.Enabled = false;
        }

        protected void STPrint_Click(object sender, EventArgs e)
        {
            try
            {
                //hierarchicalGroupingReport.PrintToPrinter(1, false, 1, 99);
                Session["ctrl"] = MiscPanel;
                ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('Print.aspx','PrintMe','height=300px,width=300px,scrollbars=1');</script>");

            }
            catch (Exception ex)
            {
                STMessageLBL.Text = ex.Message;
            }
        }

        protected void TLDeleteBN_Click(object sender, EventArgs e)
        {
            int.TryParse(ToolListDDL.SelectedValue.ToString(), out ToolListId);
            bool toolDelete = NCRClass.NCRController.DeleteToolList(ToolListId);
            if (toolDelete == false)
            {
                
                TLMessageLBL.Text = "The Tool List Delete Failed. Please contact your administrator.";
            }
            else
            {
                TLMessageLBL.Text = "The Tool List " + TLName.Text + " was deleted succesfully.";
                //once successfull should go back to main view mode again.
                GetToolListDropDownList();
                TLName.Text = "";
                //all textbox Items are Disabled
                TLName.Enabled = false;
                //initial entry of first record only AddNew
                TLAddNewBTN.Enabled = true;
                //edit and print are available only when data in database. 
                TLEditBTN.Enabled = true;
                TLPrintBTN.Enabled = true;
                TLDeleteBTN.Enabled = false;
                TLCancelBTN.Enabled = false;
                TLSaveBTN.Enabled = false;
            }

        }

        protected void TLAddNew_Click(object sender, EventArgs e)
        {//when adding a new record, the save cancel buttons are enabled no others 
            //must clear textboxes
            TLName.Text = "";
            if (ToolListId== 0)
            {
                TLName.Enabled = true;
                ToolListDDL.SelectedIndex = 0;
                //change buttons
                TLAddNewBTN.Enabled = false;
                TLSaveBTN.Enabled = true;
                TLCancelBTN.Enabled = true;
                TLEditBTN.Enabled = false;
                //this will need to clear textboxes bc sometimes there will be some in there from dropdown select?
                TLMessageLBL.Text = "AddNew? First or second.";
            }
            else
            {
                //get the dropdown again 
                GetToolListDropDownList();
                ToolListDDL.SelectedIndex = 0;
                //change buttons
                TLAddNewBTN.Enabled = false;
                TLSaveBTN.Enabled = true;
                TLCancelBTN.Enabled = true;
                TLEditBTN.Enabled = false;
                //this will need to clear textboxes bc sometimes there will be some in there from dropdown select?
                TLMessageLBL.Text = "You must select the empty value from the dropdown first.";
            }

        }

        protected void TLEdit_Click(object sender, EventArgs e)
        {
            //Save cancel Print delete enabled
            //this is to enable all text fields so as to edit the content of the customer not actually update?
            TLName.Enabled = true;
            TLSaveBTN.Enabled = true;
            TLAddNewBTN.Enabled = false;
            TLEditBTN.Enabled = false;
            TLDeleteBTN.Enabled = true;
            TLCancelBTN.Enabled = true;
            TLPrintBTN.Enabled=true;
            TLMessageLBL.Text = "Edit First or second.";
        }

        protected void TLSave_Click(object sender, EventArgs e)
        {
            int.TryParse(ToolListDDL.SelectedValue.ToString(), out ToolListId);

            if (ToolListId == 0)
            { //adding a new Tool List
                //should we have a try catch here to adjust buttons/textboxes if fails?
                ToolListId = NCRClass.NCRController.AddToolList(TLName.Text);
                if (ToolListId > 0)
                {
                    TLMessageLBL.Text = "Your Tool List " + TLName.Text + " has been added check the dropdown.";
                    //buttons should be same as on initial pageload on success
                    TLName.Text = "";
                    TLName.Enabled = false;
                    GetToolListDropDownList();
                    ToolListDDL.SelectedValue = ToolListId.ToString();
                    //once successfull buttons change
                    TLAddNewBTN.Enabled = true;
                    //edit and print are available only when data in database. initial entry of first record only AddNew
                    TLEditBTN.Enabled = true;
                    TLPrintBTN.Enabled = true;
                    TLDeleteBTN.Enabled = false;
                    TLSaveBTN.Enabled = false;
                    TLCancelBTN.Enabled = false;
                }
            }
            else
            {
                //updating System Type
                try
                {

                    NCRClass.NCRController.UpdateToolList(ToolListId, TLName.Text);
                    TLMessageLBL.Text = ToolListId + " Your Tool List " + TLName.Text + " has been updated.";
                    //buttons should be same as on initial pageload on success
                    //we updated it was successful but delete button is avail and save is avail 
                    //when we should be back to view well types instance
                    TLName.Enabled = false;
                    GetToolListDropDownList();
                    ToolListDDL.SelectedValue = ToolListId.ToString();
                    //once successfull buttons change
                    TLAddNewBTN.Enabled = true;
                    //edit and print are available only when data in database. initial entry of first record only AddNew
                    TLEditBTN.Enabled = true;
                    TLPrintBTN.Enabled = true;
                    TLDeleteBTN.Enabled = false;
                    TLSaveBTN.Enabled = false;
                    TLCancelBTN.Enabled = false;
                }
                catch (Exception ex)
                {//update failed
                    TLMessageLBL.Text = ex.ToString();
                    TLEditBTN.Enabled = false;
                    TLSaveBTN.Enabled = false;
                    TLDeleteBTN.Enabled = false;
                }
            }
        }

        protected void TLCancel_Click(object sender, EventArgs e)
        {
            TLName.Text = "";
            //Cancel.Attributes.Add("onClick","javascript:history.back();");
            GetToolListDropDownList();
            //all textbox Items are Disabled
            TLName.Enabled = false;
            TLAddNewBTN.Enabled = true;
            //edit and print are available only when data in database. initial entry of first record only AddNew
            TLEditBTN.Enabled = true;
            TLPrintBTN.Enabled = true;
            TLDeleteBTN.Enabled = false;
            TLSaveBTN.Enabled = false;
            TLCancelBTN.Enabled = false;
        }

        protected void TLPrint_Click(object sender, EventArgs e)
        {
            try
            {
                //hierarchicalGroupingReport.PrintToPrinter(1, false, 1, 99);
                Session["ctrl"] = MiscPanel;
                ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('Print.aspx','PrintMe','height=300px,width=300px,scrollbars=1');</script>");

            }
            catch (Exception ex)
            {
                TLMessageLBL.Text= ex.Message;
            }
        }

        

        
    }
}
