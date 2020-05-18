using System;
using System.Collections;
using System.Collections.Generic;
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
using C1.Web.UI.Controls.C1GridView;
using System.IO;
using System.Reflection;

namespace NCRFTRWeb
{
    public partial class WebForm8 : System.Web.UI.Page
    {
        #region Local Variables
        //-- Local Page Variables
        DataSet Reports = null;
        DataSet Detail = null;
        DataSet Details = null;
        DataSet Customers = null;
        DataSet Customer = null;
        int customerId = 0;
        DataSet Engineers = null;
        DataSet Engineer = null;
        int engineerId = 0;
        DataSet Locations = null;
        DataSet Location = null;
        int locationId = 0;
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
        DataSet Operators = null;
        DataSet Operator = null;
        int operatorId = 0;
        DataSet Problems = null;
        DataSet Problem = null;
        int problemId = 0;
        DataSet Categories = null;
        DataSet Category = null;
        int parentId = 0;
        int subcatid = 0;
        //Report table id value
        int reportId = 0;
        //local var for dropdown Report Number
        string reportNumber = "";
        DateTime ReportDate;
        string unitmeasure = "Feet";
        DataSet EventsDS = null;
        DataSet CausesDS = null;
        DataSet Corrections = null;
        DataSet CorrectionsDS = null;
        DataSet AttachmentsDS = null;

        string Source = System.Configuration.ConfigurationManager.AppSettings["ImagesDirectoryLocation"].ToString();
        #endregion

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.UpdatePanel.Unload += new EventHandler(UpdatePanel_Unload);
            this.UpdatePanel1.Unload += new EventHandler(UpdatePanel1_Unload);
            this.UpdatePanel2.Unload += new EventHandler(UpdatePanel2_Unload);
            this.FilePanel.Unload += new EventHandler(FilePanel_Unload);
            this.UpdatePanel4.Unload += new EventHandler(UpdatePanel4_Unload);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //DRCalendar.ShowTitle = true;
            //get All dropdowns 
            if (!IsPostBack)
            {  //when not postback, means hitting page first time.
                //initial startup has first report number or dropdown filled and paging
                //page is the position of the rows[page] for button paging
                Session["page"] = 1;
                GetCustomerDropDownList();
                GetEngineerDropDownList();
                GetOperatorsDropDownList();
                GetLocationsDropDownList();
                GetProblemDropDownList();
                GetCategoryDropDownList(parentId);
                GetWellTypeDropDownList();
                GetCasingSizeDropDownList();
                GetLinerSizeDropDownList();
                GetSystemTypeDropDownList();
                GetToolListDropDownList();
                DisableTextboxes();
                DRAddBTN.Enabled = true;
                DREditBTN.Enabled = false;
                DRPrintBTN.Enabled = true;
                AddEvent.Enabled = false;
                AddCause.Enabled = false;
                AddCorrection.Enabled = false;
                btnShow.Enabled = false;
                FeetCB.Checked = true;
                //may need to call enable paging on initializing page
                enablePaging();
                //reportId onload is set to 0 for first report missing senario
                GetReportIds(reportId);
                UpdateEventView();
                UpdateCauseView();
                UpdateCorrectionView();
                UpdateAttachmentView();
                gdvEvents.Enabled = false;
                gdvCorrections.Enabled = false;
                gdvAttachments.Enabled = false;
                gvCause.Enabled = false;
                disableNewAddButtons();
            }
            else
            {

            }


        }
        #region UpdatePanelUnloads
        /// <summary>
       /// these following update panel unloads are to fix the bug in the print function for this page. 
       /// The error is a result of update panels being deleted then added again
       /// causing the scriptmanager to fail in building the hierarchy tree.
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        void UpdatePanel_Unload(object sender, EventArgs e)
        {
            this.RegisterUpdatePanel(sender as UpdatePanel);
        }
        void UpdatePanel1_Unload(object sender, EventArgs e)
        {
            this.RegisterUpdatePanel(sender as UpdatePanel);
        }
        void UpdatePanel2_Unload(object sender, EventArgs e)
        {
            this.RegisterUpdatePanel(sender as UpdatePanel);
        }
        void UpdatePanel4_Unload(object sender, EventArgs e)
        {
            this.RegisterUpdatePanel(sender as UpdatePanel);
        }
        void FilePanel_Unload(object sender, EventArgs e)
        { this.RegisterUpdatePanel(sender as UpdatePanel);}
        public void RegisterUpdatePanel(UpdatePanel panel)
        {
            foreach (System.Reflection.MethodInfo methodinfo in typeof(ScriptManager).GetMethods(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance))
            {
                if (methodinfo.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel"))
                {
                    methodinfo.Invoke(ScriptManager.GetCurrent(Page), new object[] { panel });
                }
            }
        }
        #endregion

        protected void DRreportNumberDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            reportId = int.Parse(DRreportNumberDDL.SelectedValue.ToString());
            reportNumber = DRreportNumberDDL.SelectedItem.ToString();
            //this sets paging button session states to the 
            //reportnumbers so can retrieve them when paging buttons are pushed.
            getIndexofReportId(reportId);
            checkButtonEnabled();
            getFullReportDetails(reportId);
            DisableTextboxes();
            DRAddBTN.Enabled = true;
            DREditBTN.Enabled = true;
            DRPrintBTN.Enabled = true;
            DRDeleteBTN.Enabled = false;
            DRSaveBTN.Enabled = false;
            DRCancelBTN.Enabled = false;
            disableNewAddButtons();
            FeetCB.Checked = true;
            UpdateEventView();
            UpdateCauseView();
            UpdateCorrectionView();
            UpdateAttachmentView();
            gdvEvents.Enabled = false;
            gdvCorrections.Enabled = false;
            gdvAttachments.Enabled = false;
            gvCause.Enabled = false;
        }
        #region Detail Record ButtonHandling ADD/EDIT/SAVE/DELETE/CANCEL/PRINT
        //------------   Button Handling     -------------------------//
        /// <summary>
        /// This button is mainly form handling but must retrieve the next 
        /// available Report Number from the Report Numbers Table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DRAddBTN_Click(object sender, EventArgs e)
        {
            Session["action"] = "Add";
            //when adding a new record, the save cancel buttons are enabled no others 
            //must clear textboxes
            //change the reportnumberddl so can see if new report number is added in dropdown.

            //getnewReportNumber from ReportNumber Table
            try
            {
                reportNumber = NCRClass.NCRController.GetNextReportId();
                //here we set the selected value to the string ReportNumber value...
                //because the Details Report has not yet been saved, we dont yet have the reportId primary key
                DRreportNumberDDL.Items.Insert(0, reportNumber);
                DRreportNumberDDL.SelectedValue = reportNumber.ToString();
                ClearSupportTableText();
                ClearTextBoxes();
                EnableTextboxes();
                //change buttons availability
                DRAddBTN.Enabled = false;
                DRSaveBTN.Enabled = true;
                DRCancelBTN.Enabled = true;
                DREditBTN.Enabled = false;
                DRDeleteBTN.Enabled = false;
                enableNewAddButtons();
                AddEvent.Enabled = true;
                AddCause.Enabled = true;
                AddCorrection.Enabled = true;
                btnShow.Enabled = true;
                gdvEvents.Enabled = true;
                gdvCorrections.Enabled = true;
                gdvAttachments.Enabled = true;
                gvCause.Enabled = true;
                disablePaging();
                disableReportDropdown();
            }
            catch (Exception ex)
            {
                MessageLBL.Text = ex.Message.ToString();
            }
        }

        protected void DREditBTN_Click(object sender, EventArgs e)
        {
            Session["action"] = "Edit";

            //getId from selectd Dropdown 
            int.TryParse(DRreportNumberDDL.SelectedValue.ToString(), out reportId);
            getFullReportDetails(reportId);
            if (reportId != 0)
            {
                //Enable all textfields();
                EnableTextboxes();
                DRSaveBTN.Enabled = true;
                DRAddBTN.Enabled = false;
                DREditBTN.Enabled = false;
                DRDeleteBTN.Enabled = true;
                DRCancelBTN.Enabled = true;
                DRPrintBTN.Enabled = true;
                enableNewAddButtons();
                AddEvent.Enabled = true;
                AddCause.Enabled = true;
                AddCorrection.Enabled = true;
                btnShow.Enabled = true;
                gdvEvents.Enabled = true;
                gdvCorrections.Enabled = true;
                gdvAttachments.Enabled = true;
                gvCause.Enabled = true;
                disablePaging();
                disableReportDropdown();
            }
            else
            {
                MessageLBL.Text = "Error trying to Edit Record.";

            }

        }


        #region SaveButton
        protected void DRSaveBTN_Click(object sender, EventArgs e)
        {
            //on an add I need the DRreportNumberDDL.SelectedItem.ToString() for reportnumber
            //Need all dropdown selections....will need the populated fields of 
            //Customer
            int.TryParse(CustomerDDL.SelectedValue.ToString(), out customerId);
            //Location
            int.TryParse(LocationDDL.SelectedValue.ToString(), out locationId);
            //Engineer
            int.TryParse(EngineerDDL.SelectedValue.ToString(), out engineerId);
            //Operator
            int.TryParse(OperatorDDL.SelectedValue.ToString(), out operatorId);
            //Need textual data of Selected dropdown values?? should we be adding id value too?
            //so that we can re - link it to the support table dropdown on retrieval of DR?
            //get dropdownvlaues
            int.TryParse(WellTypeDDL.SelectedValue.ToString(), out WellTypeId);
            int.TryParse(CasingSizeDDL.SelectedValue.ToString(), out CasingSizeId);
            int.TryParse(LinerSizeDDL.SelectedValue.ToString(), out LinerSizeId);
            int.TryParse(ProbOccurDDL.SelectedValue.ToString(), out problemId);
            int.TryParse(SystemTypeDDL.SelectedValue.ToString(), out SystemTypeId);
            int.TryParse(ToolListDDL.SelectedValue.ToString(), out ToolListId);
            int.TryParse(CategoryDDL.SelectedValue.ToString(), out parentId);
            int.TryParse(SubCategoryDDL.SelectedValue.ToString(), out subcatid);
            if (DRDateTB.Text.ToString().Trim() == "")
            {
                ReportDate = new DateTime();
            }
            else
            {
                DateTime.TryParse(DRDateTB.Text.ToString(), out ReportDate);
            }
            if (FeetCB.Checked == true)
            {
                unitmeasure = "Feet";
            }
            else if (MetersCB.Checked == true)
            {
                unitmeasure = "Meters";
            }
            else
            {
                //default
                unitmeasure = "Feet";
            }
            string reportIdTest = DRreportNumberDDL.SelectedValue.ToString();
            //reportId = int.Parse(DRreportNumberDDL.SelectedValue.ToString());
            reportNumber = DRreportNumberDDL.SelectedItem.ToString();
            if (Session["action"].ToString() == "Add")
            {
                //do we need this check anymore we've changed to use an action session state.
                //if (reportNumber == reportIdTest)
                //{
                //adding a new Detail Record
                try
                {
                    //these values are for later use.
                    //DRYTB //DRLongitudeTB //DRUTMNTB 
                    //DRLatitudeTB //operatorExt.Text 

                    reportId = NCRClass.NCRController.AddDetailRecord(DRreportNumberDDL.SelectedItem.ToString(), DRReportType.SelectedValue.ToString(),
                    customerId, rcompany.Text, rcustomercontact.Text, rcustomerphone.Text, rContactFax.Text, rCustomerEmail.Text,
                    operatorId, rOperatorName.Text, rOperatorPhone.Text, rOperatorFax.Text, rOperatorEmail.Text, rOperatorExt.Text,
                    engineerId, rInitiatorName.Text, rInitiatorPhone.Text, rInitiatorExt.Text, rInitiatorFax.Text, rInitiatorEmail.Text,
                    locationId, rDistrict.Text, rManagerName.Text, rmanagerAddress.Text, rLocCityAddress.Text, rProvState.Text,
                    ReportDate, DRtitle.Text, DRfieldticket.Text, DRwellname.Text, WellTypeId, DRLSDTB.Text, DRSectionTB.Text,
                    DRTownshipTB.Text, DRRangeTB.Text, DRMeridianTB.Text, CasingSizeId, LinerSizeId,
                    DRTMDTB.Text, DRTVDTB.Text, unitmeasure, SystemTypeId, ToolListId, problemId, parentId, subcatid, Notes.Value,
                    DRExecSummary.Value, DRDescription.Value, DRObservations.Value, DRnotes2.Value, DRPrefaceTA.Value, DRCauseAnalysisTA.Value);
                    if (reportId > 0)
                    {

                        //if add is successful we must reload the dropdown of reportNumbers.
                        GetReportIds(reportId);
                        //GETREPORTIDS SETS THE DROPDOWN TO 
                        MessageLBL.Text = "Your Detail Record " + DRtitle.Text + " has been added. Check the dropdown.";
                        DisableTextboxes();
                        //buttons should be same as on initial pageload on success
                        //once successfull buttons change
                        DRAddBTN.Enabled = true;
                        //edit and print are available only when data in database. initial entry of first record only AddNew
                        DREditBTN.Enabled = true;
                        DRPrintBTN.Enabled = true;
                        DRDeleteBTN.Enabled = false;
                        DRCancelBTN.Enabled = false;
                        DRSaveBTN.Enabled = false;
                        disableNewAddButtons();
                        AddEvent.Enabled = false;
                        AddCause.Enabled = false;
                        AddCorrection.Enabled = false;
                        btnShow.Enabled = false;
                        gdvEvents.Enabled = false;
                        gdvCorrections.Enabled = false;
                        gdvAttachments.Enabled = false;
                        gvCause.Enabled = false;
                        //enablePaging();

                    }
                }
                catch (Exception ex) { MessageLBL.Text = ex.Message.ToString(); }
                //} end of old if condition
            }
            else if (Session["action"].ToString() == "Edit")
            {
                reportId = int.Parse(DRreportNumberDDL.SelectedValue.ToString());
                //updating a report
                try
                {
                    NCRClass.NCRController.UpdateDetailRecord(reportId, DRreportNumberDDL.SelectedItem.ToString(), DRReportType.SelectedValue,
                    customerId, rcompany.Text, rcustomercontact.Text, rcustomerphone.Text, rContactFax.Text, rCustomerEmail.Text,
                    operatorId, rOperatorName.Text, rOperatorPhone.Text, rOperatorFax.Text, rOperatorEmail.Text, rOperatorExt.Text,
                    engineerId, rInitiatorName.Text, rInitiatorPhone.Text, rInitiatorExt.Text, rInitiatorFax.Text, rInitiatorEmail.Text,
                    locationId, rDistrict.Text, rManagerName.Text, rmanagerAddress.Text, rLocCityAddress.Text, rProvState.Text,
                    ReportDate, DRtitle.Text, DRfieldticket.Text, DRwellname.Text, WellTypeId, DRLSDTB.Text, DRSectionTB.Text,
                    DRTownshipTB.Text, DRRangeTB.Text, DRMeridianTB.Text, CasingSizeId, LinerSizeId,
                    DRTMDTB.Text, DRTVDTB.Text, unitmeasure, SystemTypeId, ToolListId, problemId, parentId, subcatid, Notes.Value,
                    DRExecSummary.Value, DRDescription.Value, DRObservations.Value, DRnotes2.Value, DRPrefaceTA.Value, DRCauseAnalysisTA.Value);
                    MessageLBL.Text = reportId + " Your detail Report " + DRtitle.Text + " has been updated.";
                    //if add is successful we must reload the dropdown of reportNumbers.
                    GetReportIds(reportId);
                    //GETREPORTIDS SETS THE DROPDOWN TO 
                    DisableTextboxes();
                    //once successfull buttons change
                    DRAddBTN.Enabled = true;
                    //edit and print are available only when data in database. initial entry of first record only AddNew
                    DREditBTN.Enabled = true;
                    DRPrintBTN.Enabled = true;
                    DRDeleteBTN.Enabled = false;
                    DRCancelBTN.Enabled = false;
                    DRSaveBTN.Enabled = false;
                    disableNewAddButtons();
                    AddEvent.Enabled = false;
                    AddCause.Enabled = false;
                    AddCorrection.Enabled = false;
                    btnShow.Enabled = false;
                    gdvEvents.Enabled = false;
                    gdvCorrections.Enabled = false;
                    gdvAttachments.Enabled = false;
                    gvCause.Enabled = false;
                    //enablePaging();
                }
                catch (Exception ex)
                {//update failed
                    MessageLBL.Text = ex.Message.ToString();
                    DREditBTN.Enabled = false;
                    DRSaveBTN.Enabled = false;
                    DRDeleteBTN.Enabled = false;
                    disableNewAddButtons();
                    AddEvent.Enabled = false;
                    AddCause.Enabled = false;
                    AddCorrection.Enabled = false;
                    btnShow.Enabled = false;
                    gdvEvents.Enabled = false;
                    gdvCorrections.Enabled = false;
                    gdvAttachments.Enabled = false;
                    gvCause.Enabled = false;
                }
            }
            else
            {
                MessageLBL.Text = "Unknown Error. Contact Administrator.";
            }
        }
        #endregion


        protected void DRCancelBTN_Click(object sender, EventArgs e)
        {
            bool flag = false;
            //grab the reportnumber from the add click
            string reportIdTest = DRreportNumberDDL.SelectedValue.ToString();
            reportId = int.Parse(DRreportNumberDDL.SelectedValue.ToString());
            reportNumber = DRreportNumberDDL.SelectedItem.ToString();
            //if add was hit prior to cancel must change back reportnumber used to 'n'
            if (Session["action"].ToString() == "Add")
            {
                //changed condition to use session state action control
                //if (reportNumber == reportIdTest)
                //{
                //must change back report number to unused. => 'n'
                flag = NCRClass.NCRController.ReportNumberUsed(reportNumber);
                reportId = 0;

                //}
            }

            GetReportIds(reportId);
            GetCustomerDropDownList();
            GetEngineerDropDownList();
            GetOperatorsDropDownList();
            GetLocationsDropDownList();
            GetProblemDropDownList();
            GetCategoryDropDownList(parentId);
            GetWellTypeDropDownList();
            GetCasingSizeDropDownList();
            GetLinerSizeDropDownList();
            GetSystemTypeDropDownList();
            GetToolListDropDownList();
            //all textbox Items are Disabled
            DisableTextboxes();
            enablePaging();

            DRAddBTN.Enabled = true;
            //edit and print are available only when data in database. initial entry of first record only AddNew
            DREditBTN.Enabled = true;
            DRPrintBTN.Enabled = true;
            DRDeleteBTN.Enabled = false;
            DRCancelBTN.Enabled = false;
            DRSaveBTN.Enabled = false;
            disableNewAddButtons();
            AddEvent.Enabled = false;
            AddCause.Enabled = false;
            AddCorrection.Enabled = false;
            btnShow.Enabled = false;
            gdvEvents.Enabled = false;
            gdvCorrections.Enabled = false;
            gdvAttachments.Enabled = false;
            gvCause.Enabled = false;
        }



        protected void DRPrintBTN_Click(object sender, EventArgs e)
        {
            try
            {
                //hierarchicalGroupingReport.PrintToPrinter(1, false, 1, 99);
                Session["ctrl"] = DetailPanel;
                ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('Print.aspx','PrintMe','height=300px,width=300px,scrollbars=1');</script>");

            }
            catch (Exception ex)
            {
                MessageLBL.Text = ex.Message.ToString();
            }
        }

        protected void DRDeleteBTN_Click(object sender, EventArgs e)
        {
            reportId = int.Parse(DRreportNumberDDL.SelectedValue.ToString());
            reportNumber = DRreportNumberDDL.SelectedItem.ToString();
            NCRClass.NCRController.DeleteDetailReport(reportId, reportNumber);
            GetReportIds(0);
            //GETREPORTIDS SETS THE DROPDOWN TO 
            DisableTextboxes();
            //once successfull buttons change
            DRAddBTN.Enabled = true;
            //edit and print are available only when data in database. initial entry of first record only AddNew
            DREditBTN.Enabled = true;
            DRPrintBTN.Enabled = true;
            DRDeleteBTN.Enabled = false;
            DRCancelBTN.Enabled = false;
            DRSaveBTN.Enabled = false;
            disableNewAddButtons();
            AddEvent.Enabled = false;
            AddCause.Enabled = false;
            AddCorrection.Enabled = false;
            btnShow.Enabled = false;
            gdvEvents.Enabled = false;
            gdvCorrections.Enabled = false;
            gdvAttachments.Enabled = false;
            gvCause.Enabled = false;

        }

        #endregion





        #region Support Table Selected IndexChanged

        protected void CustomerDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            int.TryParse(CustomerDDL.SelectedValue.ToString(), out customerId);
            try
            {
                GetCustomerForListing(customerId);

                //all textbox Items are Disabled
                // DisableTextboxes();
            }
            catch (Exception exeception)
            {

                MessageLBL.Text = "There is no record for that company." + exeception.Message.ToString();
            }
        }
        protected void EngineerDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            int.TryParse(EngineerDDL.SelectedValue.ToString(), out engineerId);
            if (engineerId != 0)
            {
                try
                {
                    GetEngineerForListing(engineerId);
                    //all textbox Items are Disabled
                    //DisableTextboxes();
                }
                catch (Exception ex)
                {

                    MessageLBL.Text = "There is no record for that Engineer." + ex.Message.ToString();
                }
            }
        }
        protected void LocationDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            int.TryParse(LocationDDL.SelectedValue.ToString(), out locationId);

            try
            {
                int.TryParse(LocationDDL.SelectedValue.ToString(), out locationId);
                GetLocationForListing(locationId);

                //all textbox Items are Disabled
                //DisableTextboxes();
            }
            catch (Exception ex)
            {

                MessageLBL.Text = "There is no record for that Location." + ex.Message.ToString();
            }

        }
        protected void OperatorDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            int.TryParse(OperatorDDL.SelectedValue.ToString(), out operatorId);
            if (operatorId != 0)
            {
                try
                {
                    int.TryParse(OperatorDDL.SelectedValue.ToString(), out operatorId);
                    GetOperatorForListing(operatorId);

                    //all textbox Items are Disabled
                    //DisableTextboxes();
                }
                catch (Exception ex)
                {

                    MessageLBL.Text = "There is no record for that Operator." + ex.Message.ToString();
                }
            }

        }
        protected void GetOperatorForListing(int operatorId)
        {
            Operator = NCRClass.NCRController.LookupOperator(operatorId);
            rOperatorName.Text = Operator.Tables["Operator"].Rows[0]["OperatorName"].ToString();
            rOperatorPhone.Text = Operator.Tables["Operator"].Rows[0]["OperatorPhone"].ToString();
            rOperatorExt.Text = Operator.Tables["Operator"].Rows[0]["OperatorExt"].ToString();
            rOperatorFax.Text = Operator.Tables["Operator"].Rows[0]["OperatorFax"].ToString();
            rOperatorEmail.Text = Operator.Tables["Operator"].Rows[0]["OperatorEmail"].ToString();

        }
        protected void GetLocationForListing(int locationId)
        {
            Location = NCRClass.NCRController.LookupLocation(locationId);
            rDistrict.Text = Location.Tables["Location"].Rows[0]["District"].ToString();
            rManagerName.Text = Location.Tables["Location"].Rows[0]["ManagerName"].ToString();
            rmanagerAddress.Text = Location.Tables["Location"].Rows[0]["Address"].ToString();
            rLocCityAddress.Text = Location.Tables["Location"].Rows[0]["City"].ToString();
            rProvState.Text = Location.Tables["Location"].Rows[0]["ProvState"].ToString();
        }
        protected void GetEngineerForListing(int engineerId)
        {
            Engineer = NCRClass.NCRController.LookupEngineer(engineerId);
            rInitiatorName.Text = Engineer.Tables["Engineer"].Rows[0]["InitiatorName"].ToString();
            rInitiatorPhone.Text = Engineer.Tables["Engineer"].Rows[0]["InitiatorPhone"].ToString();
            rInitiatorExt.Text = Engineer.Tables["Engineer"].Rows[0]["InitiatorExt"].ToString();
            rInitiatorFax.Text = Engineer.Tables["Engineer"].Rows[0]["InitiatorFax"].ToString();
            rInitiatorEmail.Text = Engineer.Tables["Engineer"].Rows[0]["InitiatorEmail"].ToString();

        }
        protected void GetCustomerForListing(int customerId)
        {
            Customer = NCRClass.NCRController.LookupCustomer(customerId);
            rcompany.Text = Customer.Tables["Customer"].Rows[0]["CompanyName"].ToString();
            rcustomercontact.Text = Customer.Tables["Customer"].Rows[0]["ContactName"].ToString();
            rcustomerphone.Text = Customer.Tables["Customer"].Rows[0]["ContactPhone"].ToString();
            //customerExtension.Text = Customer.Tables["Customer"].Rows[0]["ContactExt"].ToString();
            rContactFax.Text = Customer.Tables["Customer"].Rows[0]["ContactFax"].ToString();
            rCustomerEmail.Text = Customer.Tables["Customer"].Rows[0]["ContactEmail"].ToString();
        }

        //------------   Category Subcat Handling     -------------------------//

        protected void CategoryDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            //where parent id = 
            //get subcategories for spec parentid
            //get all info for specific category
            //when i reselect from parent dropdown, the subdropdown should populate. but doesnt.
            int.TryParse(CategoryDDL.SelectedValue.ToString(), out parentId);
            if (parentId != 0)
            {
                try
                {
                    Category = NCRClass.NCRController.LookupAllCategoriesByParentId(parentId);
                    SubCategoryDDL.DataSource = Category.Tables["Categories"];
                    SubCategoryDDL.DataTextField = "CategoryName";
                    SubCategoryDDL.DataValueField = "OCID";
                    SubCategoryDDL.DataBind();
                }
                catch (Exception ex)
                {
                    MessageLBL.Text = "There is no Subcategory for that Category." + ex.Message.ToString();
                }
            }

        }

        #endregion

        #region SupportTable Adds

        protected void SaveCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                customerId = NCRClass.NCRController.AddCustomer(customer_company.Text, customer_contact.Text, customer_contactphone.Text, customerExtension.Text, customerFax.Text, customer_contactemail.Text);
                if (customerId > 0)
                {
                    MessageLBL.Text = "Your Company " + customer_company.Text + " has been added check the dropdown.";
                    //buttons should be same as on initial pageload on success                   
                    //DisableTextboxes();
                    GetCustomerDropDownList();
                    CustomerDDL.SelectedValue = customerId.ToString();
                    //have to create a dataset to send to customerdetails function so to 
                    //change the textbox fields in detailview.
                    DataSet CustomerAdd = new DataSet();
                    DataTable CustomerTable = CustomerAdd.Tables.Add();
                    DataColumn company = new DataColumn("CompanyName", typeof(String));
                    DataColumn contact = new DataColumn("ContactName", typeof(String));
                    DataColumn phone = new DataColumn("ContactPhone", typeof(String));
                    DataColumn extension = new DataColumn("ContactExt", typeof(String));
                    DataColumn fax = new DataColumn("ContactFax", typeof(String));
                    DataColumn email = new DataColumn("ContactEmail", typeof(String));
                    CustomerTable.Columns.Add(company);
                    CustomerTable.Columns.Add(contact);
                    CustomerTable.Columns.Add(phone);
                    CustomerTable.Columns.Add(extension);
                    CustomerTable.Columns.Add(fax);
                    CustomerTable.Columns.Add(email);
                    DataRow dr = CustomerTable.NewRow();
                    dr["CompanyName"] = customer_company.Text;
                    dr["ContactName"] = customer_contact.Text;
                    dr["ContactPhone"] = customer_contactphone.Text;
                    dr["ContactExt"] = customerExtension.Text;
                    dr["ContactFax"] = customerFax.Text;
                    dr["ContactEmail"] = customer_contactemail.Text;
                    CustomerTable.Rows.Add(dr);
                    CustomerAdd.Tables[0].AcceptChanges();
                    getCustomerContent(CustomerAdd);
                    ClearCustomerModalTB();

                }
                else
                {
                    MessageLBL.Text = "Adding a customer failed.";

                }
            }
            catch (Exception ex)
            {

                MessageLBL.Text = ex.Message.ToString();
            }

        }
        protected void SaveEngineerBTN_Click(object sender, EventArgs e)
        {
            try
            {
                engineerId = NCRClass.NCRController.AddEngineer(initiatorname.Text, initiatorphone.Text, initiatorExt.Text, initiatorFax.Text, initiatoremail.Text);
                if (engineerId > 0)
                {
                    MessageLBL.Text = "Your Engineer " + initiatorname.Text + " has been added check the dropdown.";
                    //buttons should be same as on initial pageload on success
                    GetEngineerDropDownList();
                    EngineerDDL.SelectedValue = engineerId.ToString();
                    //once successfull buttons change
                    NewEngineerBTN.Enabled = true;
                    DataSet EngineerAdd = new DataSet();
                    DataTable EngineerTable = EngineerAdd.Tables.Add();
                    DataColumn company = new DataColumn("InitiatorName", typeof(String));
                    DataColumn phone = new DataColumn("InitiatorPhone", typeof(String));
                    DataColumn extension = new DataColumn("InitiatorExt", typeof(String));
                    DataColumn fax = new DataColumn("InitiatorFax", typeof(String));
                    DataColumn email = new DataColumn("InitiatorEmail", typeof(String));
                    EngineerTable.Columns.Add(company);
                    EngineerTable.Columns.Add(phone);
                    EngineerTable.Columns.Add(extension);
                    EngineerTable.Columns.Add(fax);
                    EngineerTable.Columns.Add(email);
                    DataRow dr = EngineerTable.NewRow();
                    dr["InitiatorName"] = initiatorname.Text;
                    dr["InitiatorPhone"] = initiatorphone.Text;
                    dr["InitiatorExt"] = initiatorExt.Text;
                    dr["InitiatorFax"] = initiatorFax.Text;
                    dr["InitiatorEmail"] = initiatoremail.Text;
                    EngineerTable.Rows.Add(dr);
                    EngineerAdd.Tables[0].AcceptChanges();
                    getInitiatorContent(EngineerAdd);
                    ClearEngineerModalTextboxes();

                    //edit and print are available only when data in database. initial entry of first record only AddNew

                }
                else
                {
                    MessageLBL.Text = "Adding the Engineer Failed.";
                }
            }
            catch (Exception ex)
            {

                MessageLBL.Text = ex.Message.ToString();
            }
        }
        protected void SaveNewOperatorBTN_Click(object sender, EventArgs e)
        {
            try
            {
                operatorId = NCRClass.NCRController.AddOperator(operatorName.Text, operatorPhone.Text, operatorExt.Text, operatorFax.Text, operatorEmail.Text);
                if (operatorId > 0)
                {
                    MessageLBL.Text = "Your Operator " + operatorName.Text + " has been added check the dropdown.";
                    //buttons should be same as on initial pageload on success
                    GetOperatorsDropDownList();
                    OperatorDDL.SelectedValue = operatorId.ToString();
                    DataSet OperatorAdd = new DataSet();
                    DataTable OperatorTable = OperatorAdd.Tables.Add();
                    DataColumn company = new DataColumn("OperatorName", typeof(String));
                    DataColumn phone = new DataColumn("OperatorPhone", typeof(String));
                    DataColumn extension = new DataColumn("OperatorExt", typeof(String));
                    DataColumn fax = new DataColumn("OperatorFax", typeof(String));
                    DataColumn email = new DataColumn("OperatorEmail", typeof(String));
                    OperatorTable.Columns.Add(company);
                    OperatorTable.Columns.Add(phone);
                    OperatorTable.Columns.Add(extension);
                    OperatorTable.Columns.Add(fax);
                    OperatorTable.Columns.Add(email);
                    DataRow dr = OperatorTable.NewRow();
                    dr["OperatorName"] = operatorName.Text;
                    dr["OperatorPhone"] = operatorPhone.Text;
                    dr["OperatorExt"] = operatorExt.Text;
                    dr["OperatorFax"] = operatorFax.Text;
                    dr["OperatorEmail"] = operatorEmail.Text;
                    OperatorTable.Rows.Add(dr);
                    OperatorAdd.Tables[0].AcceptChanges();
                    getOperatorContent(OperatorAdd);
                    ClearOperatorModalTB();
                    //once successfull buttons change
                    NewOperatorBTN.Enabled = true;
                    //edit and print are available only when data in database. initial entry of first record only AddNew

                }
                else
                {
                    MessageLBL.Text = "Operator Add Failed.";
                }
            }
            catch (Exception ex)
            {

                MessageLBL.Text = ex.Message.ToString();
            }

        }
        protected void SaveNewLocationBTN_Click(object sender, EventArgs e)
        {
            try
            {
                locationId = NCRClass.NCRController.AddLocation(district.Text, managerName.Text, address.Text, City.Text, Province.Text);
                if (locationId > 0)
                {
                    MessageLBL.Text = "Your Location " + district.Text + " has been added check the dropdown.";
                    //buttons should be same as on initial pageload on success
                    GetLocationsDropDownList();
                    LocationDDL.SelectedValue = locationId.ToString();
                    DataSet LocationAdd = new DataSet();
                    DataTable LocationTable = LocationAdd.Tables.Add();
                    DataColumn district1 = new DataColumn("District", typeof(String));
                    DataColumn company = new DataColumn("ManagerName", typeof(String));
                    DataColumn phone = new DataColumn("Address", typeof(String));
                    DataColumn extension = new DataColumn("City", typeof(String));
                    DataColumn fax = new DataColumn("ProvState", typeof(String));
                    LocationTable.Columns.Add(district1);
                    LocationTable.Columns.Add(company);
                    LocationTable.Columns.Add(phone);
                    LocationTable.Columns.Add(extension);
                    LocationTable.Columns.Add(fax);
                    DataRow dr = LocationTable.NewRow();
                    dr["District"] = district.Text;
                    dr["ManagerName"] = managerName.Text;
                    dr["Address"] = address.Text;
                    dr["City"] = City.Text;
                    dr["ProvState"] = Province.Text;
                    LocationTable.Rows.Add(dr);
                    LocationAdd.Tables[0].AcceptChanges();
                    getLocationContent(LocationAdd);
                    ClearLocationModalTB();
                    //once successfull buttons change
                    NewLocationBTN.Enabled = true;
                }

                else
                {
                    MessageLBL.Text = "Add Location Failed.";
                }
            }
            catch (Exception ex)
            {

                MessageLBL.Text = ex.Message.ToString();
            }
        }


        #endregion
        #region PagefunctionCalls
        //------------------------------- Functions -----------------------------------//


        protected void getFirstReport()
        {
            Reports = NCRClass.NCRController.GetFirstReport();
            if (Reports.Tables[0].Rows.Count >= 0)
            {
                DRreportNumberDDL.DataSource = Reports.Tables["Reports"];
                DRreportNumberDDL.DataTextField = "ReportId";
                //datavaluefield same so can use reportnumber in adds.
                DRreportNumberDDL.DataValueField = "ReportId";
                DRreportNumberDDL.DataBind();

                //THIS IS AN ISSUE TO REVISIT
                DRreportNumberDDL.SelectedValue = "000000001";

            }
        }
        protected void GetReportIds(int reportId)
        {
            //this gets dropdownlist of reportids from the details table for view mode.
            Details = NCRClass.NCRController.GetAllDetailReports();
            Details.Tables["Details"].PrimaryKey = new DataColumn[] { Details.Tables["Details"].Columns["ReportId"] };
            //paging setup Session variables
            Session["totalCount"] = Details.Tables["Details"].Rows.Count.ToString();
            Session["count"] = 1;
            Session["first"] = Details.Tables["Details"].Rows[0]["ReportId"].ToString();
            Session["previous"] = Details.Tables["Details"].Rows[0]["ReportId"].ToString();
            Session["next"] = Details.Tables["Details"].Rows[1]["ReportId"].ToString();
            Session["last"] = Details.Tables["Details"].Rows[int.Parse(Session["totalCount"].ToString()) - 1]["ReportId"].ToString();
            //endpaging setup
            if (Details.Tables[0].Rows.Count > 0)
            {
                //we have reports in the system.
                //report Id may have been reset due to cancel or on page load
                if (reportId == 0)
                {
                    //grab first Report Primary Key to use for retrieving complete record
                    int.TryParse(Details.Tables["Details"].Rows[0]["ReportId"].ToString(), out  reportId);
                    Session["first"] = reportId.ToString();
                }

                //get the first record in table 
                //get the last record in table
                //do an adition and subtraction based on record count etc.
                DRreportNumberDDL.DataSource = Details.Tables["Details"];
                DRreportNumberDDL.DataTextField = "ReportNumber";
                DRreportNumberDDL.DataValueField = "ReportId";
                DRreportNumberDDL.DataBind();
                Page.Session["Details"] = Details;
                checkButtonEnabled();
                getFullReportDetails(reportId);

            }
        }
        protected void getFullReportDetails(int reportId)
        {
            try
            {
                Detail = NCRClass.NCRController.GetDetailsById(reportId);
                if (Detail.Tables[0].Rows.Count > 0)
                {
                    reportId = int.Parse(Detail.Tables[0].Rows[0]["ReportId"].ToString());
                    DRreportNumberDDL.SelectedValue = reportId.ToString();
                    //dropdown have to deal with
                    DRReportType.SelectedValue = Detail.Tables[0].Rows[0]["ReportType"].ToString();
                    //functions for Customer/Initiator/Location/Operator
                    getCustomerContent(Detail);
                    getInitiatorContent(Detail);
                    getLocationContent(Detail);
                    getOperatorContent(Detail);
                    DateTime dt = DateTime.Parse(Detail.Tables[0].Rows[0]["ReportDate"].ToString());
                    DRDateTB.Text = dt.ToString("MM/dd/yyyy");
                    DRSectionTB.Text = Detail.Tables[0].Rows[0]["Section"].ToString();
                    DRtitle.Text = Detail.Tables[0].Rows[0]["Title"].ToString();
                    DRTMDTB.Text = Detail.Tables[0].Rows[0]["WellTMD"].ToString();
                    DRTownshipTB.Text = Detail.Tables[0].Rows[0]["Township"].ToString();
                    DRTVDTB.Text = Detail.Tables[0].Rows[0]["WellTVD"].ToString();
                    WellTypeDDL.SelectedValue = Detail.Tables[0].Rows[0]["WellTypeID"].ToString();
                    //DRUTMNTB = Reports.Tables[0].Rows[0]["UTMN"].ToString();
                    DRwellname.Text = Detail.Tables[0].Rows[0]["WellName"].ToString();
                    //DRYTB = Reports.Tables[0].Rows[0]["ReportType"].ToString();
                    DRRangeTB.Text = Detail.Tables[0].Rows[0]["DRRange"].ToString();
                    DRPrefaceTA.Value = Detail.Tables[0].Rows[0]["Preface"].ToString();
                    DRnotes2.Value = Detail.Tables[0].Rows[0]["EventNotes"].ToString();
                    DRMeridianTB.Text = Detail.Tables[0].Rows[0]["Meridian"].ToString();
                    DRLSDTB.Text = Detail.Tables[0].Rows[0]["LSD"].ToString();
                    //DRLongitudeTB = Reports.Tables[0].Rows[0]["ReportType"].ToString();
                    //DRLatitudeTB = Reports.Tables[0].Rows[0]["ReportType"].ToString();
                    DRfieldticket.Text = Detail.Tables[0].Rows[0]["FieldTicket"].ToString();
                    DRCauseAnalysisTA.Value = Detail.Tables[0].Rows[0]["CauseAnalysis"].ToString();
                    //dropdownvlaues
                    CasingSizeDDL.SelectedValue = Detail.Tables[0].Rows[0]["CasingSizeID"].ToString();
                    LinerSizeDDL.SelectedValue = Detail.Tables[0].Rows[0]["LinerSize"].ToString();
                    ProbOccurDDL.SelectedValue = Detail.Tables[0].Rows[0]["ProblemId"].ToString();
                    SystemTypeDDL.SelectedValue = Detail.Tables[0].Rows[0]["SystemType"].ToString();
                    ToolListDDL.SelectedValue = Detail.Tables[0].Rows[0]["ToolList"].ToString();
                    CategoryDDL.SelectedValue = Detail.Tables[0].Rows[0]["CategoryId"].ToString();
                    GetSubCategoryDropDownList(int.Parse(Detail.Tables[0].Rows[0]["CategoryId"].ToString()));
                    SubCategoryDDL.SelectedValue = Detail.Tables[0].Rows[0]["CatDescriptId"].ToString();
                    Notes.Value = Detail.Tables[0].Rows[0]["ProblemNotes"].ToString();
                    DRExecSummary.Value = Detail.Tables[0].Rows[0]["ExecSummary"].ToString();
                    DRDescription.Value = Detail.Tables[0].Rows[0]["Description"].ToString();
                    DRObservations.Value = Detail.Tables[0].Rows[0]["Observation"].ToString();
                    DRnotes2.Value = Detail.Tables[0].Rows[0]["EventNotes"].ToString();
                    DRPrefaceTA.Value = Detail.Tables[0].Rows[0]["Preface"].ToString();
                    DRCauseAnalysisTA.Value = Detail.Tables[0].Rows[0]["CauseAnalysis"].ToString();
                    CustomerDDL.SelectedValue = Detail.Tables[0].Rows[0]["CustomerID"].ToString();
                    OperatorDDL.SelectedValue = Detail.Tables[0].Rows[0]["OperatorID"].ToString();
                    EngineerDDL.SelectedValue = Detail.Tables[0].Rows[0]["InitiatorID"].ToString();
                    LocationDDL.SelectedValue = Detail.Tables[0].Rows[0]["LocationID"].ToString();
                    DREditBTN.Enabled = true;
                }
                else
                {//report number is set to first initial report number because 
                    //no records yet or we are in view mode and therefore getting first record
                    //get first id in ReportNumbers table....
                    DRreportNumberDDL.SelectedValue = "1";
                    //Disable all textboxes
                    //Clear all textboxes...must hit add or edit to enable textboxes.
                }
            }
            catch (Exception ex)
            {
                MessageLBL.Text = ex.Message.ToString();
            }
        }


        protected void GetCustomerDropDownList()
        {
            Customers = NCRClass.NCRController.LookupAllCustomers();
            CustomerDDL.DataSource = Customers.Tables["Customers"];
            CustomerDDL.DataTextField = "CompanyName";
            CustomerDDL.DataValueField = "CustomerID";
            CustomerDDL.DataBind();
        }
        protected void GetEngineerDropDownList()
        {
            Engineers = NCRClass.NCRController.LookupAllEngineers();
            EngineerDDL.DataSource = Engineers.Tables["Engineers"];
            EngineerDDL.DataTextField = "InitiatorName";
            EngineerDDL.DataValueField = "InitiatorID";
            EngineerDDL.DataBind();
        }
        protected void GetLocationsDropDownList()
        {
            Locations = NCRClass.NCRController.LookupAllLocations();
            LocationDDL.DataSource = Locations.Tables["Locations"];
            LocationDDL.DataTextField = "District";
            LocationDDL.DataValueField = "LocationID";
            LocationDDL.DataBind();
        }
        protected void GetOperatorsDropDownList()
        {
            Operators = NCRClass.NCRController.LookupAllOperators();
            OperatorDDL.DataSource = Operators.Tables["Operators"];
            OperatorDDL.DataTextField = "OperatorName";
            OperatorDDL.DataValueField = "OperatorID";
            OperatorDDL.DataBind();
        }



        protected void GetWellTypeDropDownList()
        {
            WellTypes = NCRClass.NCRController.LookupAllWellTypes();
            WellTypeDDL.DataSource = WellTypes.Tables["WellTypes"];
            WellTypeDDL.DataTextField = "WellTypeName";
            WellTypeDDL.DataValueField = "WellID";
            WellTypeDDL.DataBind();
        }
        protected void GetCasingSizeDropDownList()
        {
            CasingSizes = NCRClass.NCRController.LookupAllCasingSizes();
            CasingSizeDDL.DataSource = CasingSizes.Tables["CasingSizes"];
            CasingSizeDDL.DataTextField = "CasingSizeName";
            CasingSizeDDL.DataValueField = "CasingID";
            CasingSizeDDL.DataBind();

        }
        protected void GetLinerSizeDropDownList()
        {
            LinerSizes = NCRClass.NCRController.LookupAllLinerSizes();
            LinerSizeDDL.DataSource = LinerSizes.Tables["LinerSizes"];
            LinerSizeDDL.DataTextField = "LinerSizeName";
            LinerSizeDDL.DataValueField = "LinerID";
            LinerSizeDDL.DataBind();
        }
        protected void GetSystemTypeDropDownList()
        {
            SystemTypes = NCRClass.NCRController.LookupAllSystemTypes();
            SystemTypeDDL.DataSource = SystemTypes.Tables["SystemTypes"];
            SystemTypeDDL.DataTextField = "SystemTypeName";
            SystemTypeDDL.DataValueField = "SystemTypeID";
            SystemTypeDDL.DataBind();
        }
        protected void GetToolListDropDownList()
        {
            ToolLists = NCRClass.NCRController.LookupAllToolLists();
            ToolListDDL.DataSource = ToolLists.Tables["ToolLists"];
            ToolListDDL.DataTextField = "ToolListName";
            ToolListDDL.DataValueField = "ToolListID";
            ToolListDDL.DataBind();
        }
        protected void GetProblemDropDownList()
        {
            Problems = NCRClass.NCRController.LookupAllProblems();
            ProbOccurDDL.DataSource = Problems.Tables["Problems"];
            ProbOccurDDL.DataTextField = "ProblemName";
            ProbOccurDDL.DataValueField = "POID";
            ProbOccurDDL.DataBind();
        }
        protected void GetCategoryDropDownList(int parentId)
        {
            Categories = NCRClass.NCRController.LookupAllCategoriesByParentId(parentId);
            CategoryDDL.DataSource = Categories.Tables["Categories"];
            CategoryDDL.DataTextField = "CategoryName";
            CategoryDDL.DataValueField = "OCID";
            CategoryDDL.DataBind();
            //CategoryDDL.Items.Insert(0, " ");
            if (parentId == 0)
            {
                SubCategoryDDL.Items.Clear();
                //SubCatName.Visible = false;
                //AddNewSub.Visible = false;
            }
        }

        /// <summary>
        /// This is the subcategory retrieval when the category is selected
        /// </summary>
        /// <param name="subcatid"> is actually the parent id to gather all subcategories from</param>
        protected void GetSubCategoryDropDownList(int subcatid)
        {
            Categories = NCRClass.NCRController.LookupAllCategoriesByParentId(subcatid);
            SubCategoryDDL.DataSource = Categories.Tables["Categories"];
            SubCategoryDDL.DataTextField = "CategoryName";
            SubCategoryDDL.DataValueField = "OCID";
            SubCategoryDDL.DataBind();
        }
        protected void getCustomerContent(DataSet Reports)
        {
            rcompany.Text = Reports.Tables[0].Rows[0]["CompanyName"].ToString();
            rcustomercontact.Text = Reports.Tables[0].Rows[0]["ContactName"].ToString();
            rcustomerphone.Text = Reports.Tables[0].Rows[0]["ContactPhone"].ToString();
            // = Reports.Tables["Customer"].Rows[0]["ContactExt"].ToString();
            rContactFax.Text = Reports.Tables[0].Rows[0]["ContactFax"].ToString();
            rCustomerEmail.Text = Reports.Tables[0].Rows[0]["ContactEmail"].ToString();

        }
        protected void getInitiatorContent(DataSet Reports)
        {
            rInitiatorName.Text = Reports.Tables[0].Rows[0]["InitiatorName"].ToString();
            rInitiatorPhone.Text = Reports.Tables[0].Rows[0]["InitiatorPhone"].ToString();
            rInitiatorExt.Text = Reports.Tables[0].Rows[0]["InitiatorExt"].ToString();
            rInitiatorFax.Text = Reports.Tables[0].Rows[0]["InitiatorFax"].ToString();
            rInitiatorEmail.Text = Reports.Tables[0].Rows[0]["InitiatorEmail"].ToString();

        }
        protected void getLocationContent(DataSet Reports)
        {
            rDistrict.Text = Reports.Tables[0].Rows[0]["District"].ToString();
            rManagerName.Text = Reports.Tables[0].Rows[0]["ManagerName"].ToString();
            rmanagerAddress.Text = Reports.Tables[0].Rows[0]["Address"].ToString();
            rLocCityAddress.Text = Reports.Tables[0].Rows[0]["City"].ToString();
            rProvState.Text = Reports.Tables[0].Rows[0]["ProvState"].ToString();

        }
        protected void getOperatorContent(DataSet Reports)
        {
            rOperatorName.Text = Reports.Tables[0].Rows[0]["OperatorName"].ToString();
            rOperatorPhone.Text = Reports.Tables[0].Rows[0]["OperatorPhone"].ToString();
            //operatorExt.Text = Operator.Tables["Operator"].Rows[0]["OperatorExt"].ToString();
            rOperatorFax.Text = Reports.Tables[0].Rows[0]["OperatorFax"].ToString();
            rOperatorEmail.Text = Reports.Tables[0].Rows[0]["OperatorEmail"].ToString();

        }
        #endregion

        #region gridviews
        //------------------------ GRIDVIEWS ----------------------------------



        #region Event gridview
        /// <summary>
        /// This Binds the DataSource to the Event Gridview
        /// </summary>
        private void UpdateEventView()
        {
            DataSet ds = GetEventDataSet();
            // Bind the data
            gdvEvents.DataSource = ds.Tables["Events"];
            gdvEvents.DataBind();

        }
        /// <summary>
        /// this creates Event Dataset from the Database on initiate page
        /// and Assigns it to a Session Event Object
        /// </summary>
        /// <returns></returns>
        private DataSet GetEventDataSet()
        {
            reportId = int.Parse(DRreportNumberDDL.SelectedValue.ToString());
            EventsDS = Page.Session["Events"] as DataSet;
            if (EventsDS == null)
            {
                EventsDS = NCRClass.NCRController.getEventsByReportID(reportId);
                EventsDS.Tables["Events"].PrimaryKey = new DataColumn[] { EventsDS.Tables["Events"].Columns["EventId"] };
                Page.Session["Events"] = EventsDS;
            }
            return EventsDS;
        }

        protected void gdvEvents_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (gdvEvents.EditIndex == -1)
            {
                gdvEvents.EditIndex = e.NewEditIndex;
                gdvEvents.DataSource = GetEventDataSet().Tables["Events"];
                gdvEvents.DataBind();
            }
            else

                EventLBL.Text = "Please finish editing current row first";
        }
        protected void gdvEvents_EditCancel(object sender, GridViewCancelEditEventArgs e)
        {
            gdvEvents.EditIndex = -1;
            gdvEvents.DataSource = GetEventDataSet().Tables["Events"];
            gdvEvents.DataBind();
        }
        protected void gdvEvents_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // Implement Deleting
            // Get data from row being Deleted
            GridViewRow myRow = gdvEvents.Rows[e.RowIndex];
            String myID = ((Label)myRow.FindControl("EventId")).Text;

            // Use myID to delete from database
            try
            {
                bool success = NCRClass.NCRController.DeleteEvent(int.Parse(myID));
                if (success == true)
                { //acceptChanges on DataSet
                    EventsDS = DeleteEventDataRow(int.Parse(myID));
                }
                else
                {
                    //rejectchanges 
                    //get session dataset
                    EventsDS = Page.Session["Events"] as DataSet;// set as datasource and rebind
                }
            }
            catch (Exception ex)
            {
                EventLBL.Text = ex.Message.ToString();
            }
            // Fetch new data from database or delete from dataset

            // set as datasource and rebind
            gdvEvents.DataSource = EventsDS.Tables["Events"];//GetEventDataSet().Tables["Events"];
            gdvEvents.DataBind();
            // set as datasource and rebind

        }
        protected void gdvEvents_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            //get ReportId
            reportId = int.Parse(DRreportNumberDDL.SelectedValue.ToString());
            // Get data from row being updated
            GridViewRow myRow = gdvEvents.Rows[e.RowIndex];
            String myID = ((Label)myRow.FindControl("lblEventID")).Text;
            String eventTitle = ((TextBox)myRow.FindControl("edtEventTitle")).Text;
            String eventDate = ((TextBox)myRow.FindControl("edtEventDate")).Text;
            TextBox eventContent = (TextBox)myRow.FindControl("edtEventContent");
            EventLBL.Text = "My results: " + myID + " : " + eventTitle + " : " + eventDate + " : " + eventContent.Text;

            // Save changes to database
            try
            {

                NCRClass.NCRController.UpdateEvent(int.Parse(myID.ToString()), reportId, eventTitle, eventContent.Text, eventDate);
                EventsDS = UpdateEventDataRow(int.Parse(myID.ToString()), eventTitle, eventDate, eventContent.Text);
                gdvEvents.EditIndex = -1;
                gdvEvents.DataSource = EventsDS.Tables["Events"];//EventsDS.Tables["Events"];
                gdvEvents.DataBind();
            }
            catch (Exception ex)
            {
                EventLBL.Text = ex.Message.ToString();
                //clear EventSession variable
                Page.Session["Events"] = null;
                //bind data to database table
                gdvEvents.EditIndex = -1;
                gdvEvents.DataSource = GetEventDataSet().Tables["Events"];
                gdvEvents.DataBind();
            }
            // Fetch new dataset from database or use session dataset so must create new row

            // Set datasource and rebind

        }
        protected void gdvEvents_Paging(object sender, GridViewPageEventArgs e)
        {
            gdvEvents.PageIndex = e.NewPageIndex;
            gdvEvents.DataSource = GetEventDataSet().Tables["Events"];
            gdvEvents.DataBind();
        }
        protected void SaveEvent_Click(object sender, EventArgs e)
        {
            int eventId = 0;
            // Implement Adding new event
            reportId = int.Parse(DRreportNumberDDL.SelectedValue.ToString());

            try
            {
                eventId = NCRClass.NCRController.AddEvent(reportId, txtEventTitle.Text, txtEventContent.Text, txtEventDate.Text);
                //populateEventGrid(reportId);
                if (eventId > 0)
                {
                    //Fetch new info from dataset
                    EventsDS = CreateEventDataRow(eventId, txtEventTitle.Text, txtEventDate.Text, txtEventContent.Text);
                    gdvEvents.EditIndex = -1;
                    // Set datasource and rebind
                    gdvEvents.DataSource = EventsDS.Tables["Events"];//EventsDS.Tables["Events"];
                    gdvEvents.DataBind();
                    //clear modal text
                    ClearEventModalText();

                }
                else
                {
                    //Fetch new info from database or session state cuz add failed
                    EventsDS = Page.Session["Events"] as DataSet;
                    gdvEvents.EditIndex = -1;
                    // Set datasource and rebind
                    gdvEvents.DataSource = EventsDS.Tables["Events"];//EventsDS.Tables["Events"];
                    gdvEvents.DataBind();
                    EventLBL.Text = "Some weird error happened on insert that was not caught";
                }
            }
            catch (Exception ex)
            {
                EventLBL.Text = "Testing text values: " + reportId + " :  " + txtEventTitle.Text + " :  " + txtEventContent.Text + " :  " + txtEventDate.Text + " :  " + ex.Message.ToString();
            }



        }
        #endregion

        #region EventGridView DataSetManagement
        /// <summary>
        /// This creates a row in the dataset to add the content to so as to not hit database all the time. 
        /// </summary>
        /// <param name="myId"></param>
        /// <param name="eventTitle"></param>
        /// <param name="EventDate"></param>
        /// <param name="EventText"></param>
        /// <returns>Event Dataset for Binding</returns>
        private DataSet CreateEventDataRow(int myId, string eventTitle, string EventDate, string EventText)
        {
            //get the dataset in session
            EventsDS = Page.Session["Events"] as DataSet;
            DataRow newEventRow = EventsDS.Tables["Events"].NewRow();

            newEventRow["EventId"] = myId;
            newEventRow["EventTitle"] = eventTitle;
            newEventRow["EventDate"] = DateTime.Parse(EventDate.ToString());
            newEventRow["EventText"] = EventText;
            EventsDS.Tables["Events"].Rows.Add(newEventRow);
            EventsDS.Tables["Events"].AcceptChanges();
            Page.Session["Events"] = EventsDS;
            return EventsDS;
        }

        /// <summary>
        /// This Updates the Dataset and Session Event Object
        /// </summary>
        /// <param name="myId"></param>
        /// <param name="eventTitle"></param>
        /// <param name="EventDate"></param>
        /// <param name="EventText"></param>
        /// <returns>Event Dataset for Binding</returns>
        private DataSet UpdateEventDataRow(int myId, string eventTitle, string EventDate, string EventText)
        {
            //get the dataset in session
            EventsDS = Page.Session["Events"] as DataSet;
            //find the row I need to update
            DataRow dr;
            dr = EventsDS.Tables["Events"].Rows.Find(myId);
            if (dr == null)
            {
                EventLBL.Text = "Did not find the row";
            }
            else
            {
                dr["EventId"] = myId;
                dr["EventTitle"] = eventTitle;
                dr["EventDate"] = DateTime.Parse(EventDate.ToString());
                dr["EventText"] = EventText;
                EventsDS.Tables["Events"].AcceptChanges();
                Page.Session["Events"] = EventsDS;

            }
            return EventsDS;

        }
        /// <summary>
        /// This deletes the record in the DataSet
        /// </summary>
        /// <param name="myId"></param>
        /// <returns>Event Dataset for Binding</returns>
        private DataSet DeleteEventDataRow(int myId)
        {
            //get the dataset in session
            EventsDS = Page.Session["Events"] as DataSet;
            //find the row I need to update
            DataRow dr;
            dr = EventsDS.Tables["Events"].Rows.Find(myId);
            if (dr == null)
            {
                EventLBL.Text = "Did not find the row";
            }
            else
            {
                dr.Delete();
            }
            EventsDS.AcceptChanges();
            Page.Session["Events"] = EventsDS;
            return EventsDS;

        }

        #endregion




        #region Cause GridView
        private void UpdateCauseView()
        {
            DataSet ds = GetCauseDataSet();
            gvCause.DataSource = ds.Tables["Causes"];
            gvCause.DataBind();
        }
        /// <summary>
        /// this creates Event Dataset from the Database on initiate page
        /// and Assigns it to a Session Event Object
        /// </summary>
        /// <returns></returns>
        private DataSet GetCauseDataSet()
        {
            reportId = int.Parse(DRreportNumberDDL.SelectedValue.ToString());
            CausesDS = Page.Session["Causes"] as DataSet;
            if (CausesDS == null)
            {
                CausesDS = NCRClass.NCRController.getCausesByReportId(reportId);
                CausesDS.Tables["Causes"].PrimaryKey = new DataColumn[] { CausesDS.Tables["Causes"].Columns["CauseId"] };
                Page.Session["Causes"] = CausesDS;
            }
            return CausesDS;
        }

        protected void gdvCauses_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (gvCause.EditIndex == -1)
            {
                gvCause.EditIndex = e.NewEditIndex;
                gvCause.DataSource = GetCauseDataSet().Tables["Causes"];
                gvCause.DataBind();
            }
            else

                CauseLBL.Text = "Please finish editing current row first";
        }
        protected void gdvCauses_EditCancel(object sender, GridViewCancelEditEventArgs e)
        {
            gvCause.EditIndex = -1;
            gvCause.DataSource = GetCauseDataSet().Tables["Causes"];
            gvCause.DataBind();
        }
        protected void gdvCauses_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // Implement Deleting
            // Get data from row being Deleted
            GridViewRow myRow = gvCause.Rows[e.RowIndex];
            String myID = ((Label)myRow.FindControl("CauseId")).Text;

            // Use myID to delete from database
            try
            {
                NCRClass.NCRController.DeleteCause(int.Parse(myID));

                CausesDS = DeleteCauseDataRow(int.Parse(myID));

            }
            catch (Exception ex)
            {
                CauseLBL.Text = ex.Message.ToString();
                CausesDS = Page.Session["Causes"] as DataSet;
            }
            // Fetch new data from database or delete from dataset

            // set as datasource and rebind
            gvCause.DataSource = CausesDS.Tables["Causes"];//GetEventDataSet().Tables["Events"];
            gvCause.DataBind();
            // set as datasource and rebind

        }
        protected void gdvCauses_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            //get ReportId
            reportId = int.Parse(DRreportNumberDDL.SelectedValue.ToString());
            // Get data from row being updated
            GridViewRow myRow = gvCause.Rows[e.RowIndex];
            String myID = ((Label)myRow.FindControl("lblCauseID")).Text;
            String CauseTitle = ((TextBox)myRow.FindControl("edtCauseTitle")).Text;
            TextBox CauseContent = (TextBox)myRow.FindControl("edtCauseContent");
            CauseLBL.Text = "My results: " + myID + " : " + CauseTitle + " : " + CauseContent.Text;

            // Save changes to database
            try
            {
                NCRClass.NCRController.UpdateCause(int.Parse(myID.ToString()), reportId, CauseTitle, CauseContent.Text);
                // Fetch dataset use session dataset so must create new row
                CausesDS = UpdateCauseDataRow(int.Parse(myID.ToString()), CauseTitle, CauseContent.Text);
            }
            catch (Exception ex)
            {
                CauseLBL.Text = ex.Message.ToString();
                // Fetch new dataset from database or use session dataset so must create new row
                CausesDS = Page.Session["Causes"] as DataSet;


            }

            // Set datasource and rebind
            gvCause.EditIndex = -1;
            gvCause.DataSource = CausesDS.Tables["Causes"];
            gvCause.DataBind();
        }
        protected void gdvCauses_Paging(object sender, GridViewPageEventArgs e)
        {
            gvCause.PageIndex = e.NewPageIndex;
            gvCause.DataSource = GetCauseDataSet().Tables["Causes"];
            gvCause.DataBind();
        }
        protected void SaveCause_Click(object sender, EventArgs e)
        {
            int causeId = 0;
            // Implement Adding new event
            reportId = int.Parse(DRreportNumberDDL.SelectedValue.ToString());

            try
            {
                causeId = NCRClass.NCRController.AddCause(reportId, txtCauseTitle.Text, txtCauseContent.Text);
                //populateEventGrid(reportId);
                if (causeId > 0)
                {
                    //Fetch new info from dataset
                    CausesDS = CreateCauseDataRow(causeId, txtCauseTitle.Text, txtCauseContent.Text);
                    //clear modal text
                    ClearCauseModalText();

                }
                else
                {
                    //Fetch new info from database or session state cuz add failed
                    CausesDS = Page.Session["Causes"] as DataSet;
                    CauseLBL.Text = "Some weird error happened on insert that was not caught";
                }
            }
            catch (Exception ex)
            {
                CauseLBL.Text = "Testing text values: " + reportId + " :  " + txtCauseTitle.Text + " :  " + txtCauseContent.Text + " :  " + ex.Message.ToString();
                CausesDS = Page.Session["Causes"] as DataSet;
            }

            gvCause.EditIndex = -1;
            // Set datasource and rebind
            gvCause.DataSource = CausesDS.Tables["Causes"];
            gvCause.DataBind();

        }
        #endregion

        #region CauseGridView DataSetManagement
        /// <summary>
        /// This creates a row in the dataset to add the content to so as to not hit database all the time. 
        /// </summary>
        /// <param name="myId"></param>
        /// <param name="eventTitle"></param>
        /// <param name="EventDate"></param>
        /// <param name="EventText"></param>
        /// <returns>Event Dataset for Binding</returns>
        private DataSet CreateCauseDataRow(int myId, string eventTitle, string EventText)
        {
            //get the dataset in session
            CausesDS = Page.Session["Causes"] as DataSet;
            DataRow newEventRow = CausesDS.Tables["Causes"].NewRow();

            newEventRow["CauseId"] = myId;
            newEventRow["CauseTitle"] = eventTitle;
            newEventRow["CauseText"] = EventText;
            CausesDS.Tables["Causes"].Rows.Add(newEventRow);
            CausesDS.Tables["Causes"].AcceptChanges();
            Page.Session["Causes"] = CausesDS;
            return CausesDS;
        }

        /// <summary>
        /// This Updates the Dataset and Session Event Object
        /// </summary>
        /// <param name="myId"></param>
        /// <param name="eventTitle"></param>
        /// <param name="EventDate"></param>
        /// <param name="EventText"></param>
        /// <returns>Event Dataset for Binding</returns>
        private DataSet UpdateCauseDataRow(int myId, string eventTitle, string EventText)
        {
            //get the dataset in session
            CausesDS = Page.Session["Causes"] as DataSet;
            //find the row I need to update
            DataRow dr;
            dr = CausesDS.Tables["Causes"].Rows.Find(myId);
            if (dr == null)
            {
                CauseLBL.Text = "Did not find the row";
            }
            else
            {
                dr["CauseId"] = myId;
                dr["CauseTitle"] = eventTitle;
                dr["CauseText"] = EventText;
                CausesDS.Tables["Causes"].AcceptChanges();
                Page.Session["Causes"] = CausesDS;

            }
            return CausesDS;

        }
        /// <summary>
        /// This deletes the record in the DataSet
        /// </summary>
        /// <param name="myId"></param>
        /// <returns>Event Dataset for Binding</returns>
        private DataSet DeleteCauseDataRow(int myId)
        {
            //get the dataset in session
            CausesDS = Page.Session["Causes"] as DataSet;
            //find the row I need to update
            DataRow dr;
            dr = CausesDS.Tables["Causes"].Rows.Find(myId);
            if (dr == null)
            {
                CauseLBL.Text = "Did not find the row";
            }
            else
            {
                dr.Delete();
            }
            CausesDS.AcceptChanges();
            Page.Session["Causes"] = CausesDS;
            return CausesDS;

        }




        #endregion




        #region Correction GridView
        private void UpdateCorrectionView()
        {
            DataSet ds = GetCorrectionDataSet();

            gdvCorrections.DataSource = ds.Tables["Corrections"];
            gdvCorrections.DataBind();
        }
        /// <summary>
        /// this creates Event Dataset from the Database on initiate page
        /// and Assigns it to a Session Event Object
        /// </summary>
        /// <returns></returns>
        private DataSet GetCorrectionDataSet()
        {
            reportId = int.Parse(DRreportNumberDDL.SelectedValue.ToString());
            CorrectionsDS = Page.Session["Corrections"] as DataSet;
            if (CorrectionsDS == null)
            {
                CorrectionsDS = NCRClass.NCRController.getCorrectionsByReportId(reportId);
                CorrectionsDS.Tables["Corrections"].PrimaryKey = new DataColumn[] { CorrectionsDS.Tables["Corrections"].Columns["CorrectiveActId"] };
                Page.Session["Corrections"] = CorrectionsDS;
            }
            return CorrectionsDS;
        }

        protected void gdvCorrections_RowEditing(object sender, GridViewEditEventArgs e)
        {

            if (gdvCorrections.EditIndex == -1)
            {
                gdvCorrections.EditIndex = e.NewEditIndex;
                gdvCorrections.DataSource = GetCorrectionDataSet().Tables["Corrections"];
                gdvCorrections.DataBind();
            }
            else

                CorrectionLBL.Text = "Please finish editing current row first";
        }
        protected void gdvCorrections_EditCancel(object sender, GridViewCancelEditEventArgs e)
        {
            gdvCorrections.EditIndex = -1;
            gdvCorrections.DataSource = GetCorrectionDataSet().Tables["Corrections"];
            gdvCorrections.DataBind();
        }
        protected void gdvCorrections_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // Implement Deleting
            // Get data from row being Deleted
            GridViewRow myRow = gdvCorrections.Rows[e.RowIndex];
            String myID = ((Label)myRow.FindControl("lblCorrectionActID")).Text;

            // Use myID to delete from database
            try
            {
                NCRClass.NCRController.DeleteCorrection(int.Parse(myID));
                CorrectionsDS = DeleteCorrectionDataRow(int.Parse(myID));
            }
            catch (Exception ex)
            {
                CorrectionLBL.Text = ex.Message.ToString();
                // set as datasource and rebind
                CorrectionsDS = Page.Session["Corrections"] as DataSet;
            }
            // Fetch new data from database or delete from dataset

            // set as datasource and rebind
            gdvCorrections.DataSource = CorrectionsDS.Tables["Corrections"];
            gdvCorrections.DataBind();

        }
        protected void gdvCorrections_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            //get ReportId
            reportId = int.Parse(DRreportNumberDDL.SelectedValue.ToString());
            // Get data from row being updated
            GridViewRow myRow = gdvCorrections.Rows[e.RowIndex];
            String myID = ((Label)myRow.FindControl("lblCorrectionActID")).Text;
            String CorrectionTitle = ((TextBox)myRow.FindControl("edtCorrectionTitle")).Text;
            TextBox CorrectionContent = (TextBox)myRow.FindControl("edtCorrectionContent");
            CorrectionLBL.Text = "My results: " + myID + " : " + CorrectionTitle + " : " + CorrectionContent.Text;

            // Save changes to database
            try
            {

                NCRClass.NCRController.UpdateCorrection(int.Parse(myID.ToString()), reportId, CorrectionTitle, CorrectionContent.Text);
                //update dataset and session vars
                CorrectionsDS = UpdateCorrectionDataRow(int.Parse(myID.ToString()), CorrectionTitle, CorrectionContent.Text);
            }
            catch (Exception ex)
            {
                CorrectionLBL.Text = ex.Message.ToString();
                //clear EventSession variable
                CorrectionsDS = Page.Session["Corrections"] as DataSet;
                //bind data to database table


            }

            // Set datasource and rebind
            gdvCorrections.EditIndex = -1;
            gdvCorrections.DataSource = CorrectionsDS.Tables["Corrections"];
            gdvCorrections.DataBind();
        }
        protected void gdvCorrections_Paging(object sender, GridViewPageEventArgs e)
        {
            gdvCorrections.PageIndex = e.NewPageIndex;
            gdvCorrections.DataSource = GetCorrectionDataSet().Tables["Corrections"];
            gdvCorrections.DataBind();
        }
        protected void SaveCorrection_Click(object sender, EventArgs e)
        {
            int CorrectionId = 0;
            // Implement Adding new event
            reportId = int.Parse(DRreportNumberDDL.SelectedValue.ToString());

            try
            {
                CorrectionId = NCRClass.NCRController.AddCorrection(reportId, txtCorrectionTitle.Text, txtCorrectionContent.Text);
                //populateEventGrid(reportId);
                if (CorrectionId > 0)
                {
                    //Fetch new info from dataset
                    CorrectionsDS = CreateCorrectionDataRow(CorrectionId, txtCorrectionTitle.Text, txtCorrectionContent.Text);
                    //clear modal text
                    ClearCorrectionModalText();
                }
                else
                {
                    //Fetch new info from database or session state cuz add failed
                    CorrectionsDS = Page.Session["Corrections"] as DataSet;

                    CorrectionLBL.Text = "Some weird error happened on insert that was not caught";
                }
            }
            catch (Exception ex)
            {
                CorrectionsDS = Page.Session["Corrections"] as DataSet;
            }
            gdvCorrections.EditIndex = -1;
            if (CorrectionsDS != null)
            {
                // Set datasource and rebind
                gdvCorrections.DataSource = CorrectionsDS.Tables["Corrections"];
                gdvCorrections.DataBind();
            }


        }
        #endregion

        #region CorrectionGridView DataSetManagement
        /// <summary>
        /// This creates a row in the dataset to add the content to so as to not hit database all the time. 
        /// </summary>
        /// <param name="myId"></param>
        /// <param name="eventTitle"></param>
        /// <param name="EventDate"></param>
        /// <param name="EventText"></param>
        /// <returns>Event Dataset for Binding</returns>
        private DataSet CreateCorrectionDataRow(int myId, string eventTitle, string EventText)
        {
            //get the dataset in session
            CorrectionsDS = Page.Session["Corrections"] as DataSet;
            DataRow newEventRow = CorrectionsDS.Tables["Corrections"].NewRow();

            newEventRow["CorrectiveActId"] = myId;
            newEventRow["CorrectiveActTitle"] = eventTitle;
            newEventRow["CorrectiveActText"] = EventText;
            CorrectionsDS.Tables["Corrections"].Rows.Add(newEventRow);
            CorrectionsDS.Tables["Corrections"].AcceptChanges();
            Page.Session["Corrections"] = CorrectionsDS;
            return CorrectionsDS;
        }

        /// <summary>
        /// This Updates the Dataset and Session Event Object
        /// </summary>
        /// <param name="myId"></param>
        /// <param name="eventTitle"></param>
        /// <param name="EventDate"></param>
        /// <param name="EventText"></param>
        /// <returns>Event Dataset for Binding</returns>
        private DataSet UpdateCorrectionDataRow(int myId, string eventTitle, string EventText)
        {
            //get the dataset in session
            CorrectionsDS = Page.Session["Corrections"] as DataSet;
            //find the row I need to update
            DataRow dr;
            dr = CorrectionsDS.Tables["Corrections"].Rows.Find(myId);
            if (dr == null)
            {
                CorrectionLBL.Text = "Did not find the row";
            }
            else
            {
                dr["CorrectiveActId"] = myId;
                dr["CorrectiveActTitle"] = eventTitle;
                dr["CorrectiveActText"] = EventText;
                CorrectionsDS.Tables["Corrections"].AcceptChanges();
                Page.Session["Corrections"] = CorrectionsDS;

            }
            return CorrectionsDS;

        }
        /// <summary>
        /// This deletes the record in the DataSet
        /// </summary>
        /// <param name="myId"></param>
        /// <returns>Event Dataset for Binding</returns>
        private DataSet DeleteCorrectionDataRow(int myId)
        {
            //get the dataset in session
            CorrectionsDS = Page.Session["Corrections"] as DataSet;
            //find the row I need to update
            DataRow dr;
            dr = CorrectionsDS.Tables["Corrections"].Rows.Find(myId);
            if (dr == null)
            {
                CorrectionLBL.Text = "Did not find the row";
            }
            else
            {
                dr.Delete();
            }
            CorrectionsDS.AcceptChanges();
            Page.Session["Corrections"] = CorrectionsDS;
            return CorrectionsDS;

        }




        #endregion



        #region Attatchment Gridview

        private void UpdateAttachmentView()
        {
            DataSet ds = GetAttachmentDataSet();

            gdvAttachments.DataSource = ds.Tables["Attachments"];
            gdvAttachments.DataBind();
        }
        /// <summary>
        /// this creates Event Dataset from the Database on initiate page
        /// and Assigns it to a Session Event Object
        /// </summary>
        /// <returns></returns>
        private DataSet GetAttachmentDataSet()
        {
            reportId = int.Parse(DRreportNumberDDL.SelectedValue.ToString());
            AttachmentsDS = Page.Session["Attachments"] as DataSet;
            if (AttachmentsDS == null)
            {
                AttachmentsDS = NCRClass.NCRController.getAttachmentsByReportId(reportId);
                AttachmentsDS.Tables["Attachments"].PrimaryKey = new DataColumn[] { AttachmentsDS.Tables["Attachments"].Columns["AttachmentId"] };
                Page.Session["Attachments"] = AttachmentsDS;
            }
            return AttachmentsDS;
        }

        protected void gdvAttachments_RowEditing(object sender, GridViewEditEventArgs e)
        {

            if (gdvAttachments.EditIndex == -1)
            {
                gdvAttachments.EditIndex = e.NewEditIndex;
                gdvAttachments.DataSource = GetAttachmentDataSet().Tables["Attachments"];
                gdvAttachments.DataBind();
            }
            else

                uxFileInfo.Text = "Please finish editing current row first";
        }
        protected void gdvAttachments_EditCancel(object sender, GridViewCancelEditEventArgs e)
        {
            gdvAttachments.EditIndex = -1;
            gdvAttachments.DataSource = GetAttachmentDataSet().Tables["Attachments"];
            gdvAttachments.DataBind();
        }
        protected void gdvAttachments_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // Implement Deleting
            // Get data from row being Deleted
            GridViewRow myRow = gdvAttachments.Rows[e.RowIndex];
            String myID = ((Label)myRow.FindControl("lblAttachmentID")).Text;

            // Use myID to delete from database
            try
            {
                NCRClass.NCRController.DeleteAttachment(int.Parse(myID));
                AttachmentsDS = DeleteAttachmentDataRow(int.Parse(myID));
            }
            catch (Exception ex)
            {
                uxFileInfo.Text = ex.Message.ToString();
                // set as datasource and rebind
                AttachmentsDS = Page.Session["Attachments"] as DataSet;
            }
            // Fetch new data from database or delete from dataset

            // set as datasource and rebind
            gdvAttachments.DataSource = AttachmentsDS.Tables["Attachments"];
            gdvAttachments.DataBind();

        }
        protected void gdvAttachments_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            //get ReportId
            reportId = int.Parse(DRreportNumberDDL.SelectedValue.ToString());
            // Get data from row being updated
            GridViewRow myRow = gdvAttachments.Rows[e.RowIndex];
            String myID = ((Label)myRow.FindControl("lblAttachmentID")).Text;
            String AttachmentTitle = ((TextBox)myRow.FindControl("edtAttachmentTitle")).Text;
            String AttachmentFileName = ((Label)myRow.FindControl("lblFilename")).Text;
            TextBox AttachmentContent = (TextBox)myRow.FindControl("edtAttachmentContent");
            uxFileInfo.Text = "My results: " + myID + " : " + AttachmentTitle + " : " + AttachmentContent.Text;

            // Save changes to database
            try
            {

                NCRClass.NCRController.UpdateAttachment(int.Parse(myID.ToString()), reportId, AttachmentTitle, AttachmentFileName, Source, AttachmentContent.Text);
                //update dataset and session vars
                AttachmentsDS = UpdateAttachmentDataRow(int.Parse(myID.ToString()), AttachmentTitle, AttachmentFileName, AttachmentContent.Text);
            }
            catch (Exception ex)
            {
                uxFileInfo.Text = ex.Message.ToString();
                //clear EventSession variable
                AttachmentsDS = Page.Session["Attachments"] as DataSet;
                //bind data to database table


            }

            // Set datasource and rebind
            gdvAttachments.EditIndex = -1;
            gdvAttachments.DataSource = AttachmentsDS.Tables["Attachments"];
            gdvAttachments.DataBind();
        }
        protected void btnFileUpload_Click(object sender, EventArgs e)
        {
            int AttachmentId = 0;
            // Implement Adding new event
            reportId = int.Parse(DRreportNumberDDL.SelectedValue.ToString());
            //this is id of fileuploader

            if (fileUpload.HasFile)
            {
                //uxFileInfo.Text = "this worked";
                try
                {
                    fileUpload.SaveAs(@"c:\myimage.jpg");
                    fileUpload.SaveAs(Source.ToString() + "\\" + fileUpload.FileName);
                    //            Directory.CreateDirectory(
                    //                System.Configuration.ConfigurationManager.AppSettings["ImagesDirectoryLocation"].ToString()
                    //                + User.Identity.Name);
                    //            AttachmentFUL.SaveAs(
                    //                System.Configuration.ConfigurationManager.AppSettings["ImagesDirectoryLocation"].ToString()
                    //                //+ User.Identity.Name + "\\"
                    //                + AttachmentFUL.FileName);
                    uxFileInfo.Text = "File name: " +
                      fileUpload.PostedFile.FileName + "<br>" +
                      fileUpload.PostedFile.ContentLength + " kb<br>" +
                         "Content type: " +
                        fileUpload.PostedFile.ContentType;
                    AttachmentId = NCRClass.NCRController.AddAttachment(reportId, txtAttachmentTitle.Text, fileUpload.PostedFile.FileName, Source, txtAttachmentComments.Text);
                    if (AttachmentId > 0)
                    {
                        //Fetch new info from dataset
                        AttachmentsDS = CreateAttachmentDataRow(AttachmentId, txtAttachmentTitle.Text, fileUpload.PostedFile.FileName, txtAttachmentComments.Text);
                        //clear modal text
                        ClearAttachmentModalText();
                        pnlEdit.Attributes["style"] = "display: none;";
                    }
                    else
                    {
                        //Fetch new info from database or session state cuz add failed
                        AttachmentsDS = Page.Session["Attachments"] as DataSet;
                        uxFileInfo.Text = "Some weird error happened on insert that was not caught";
                    }
                }
                catch (Exception ex)
                {
                    uxFileInfo.Text = "ERROR: Testing text values: " + reportId + " :  " + txtAttachmentTitle.Text + " :  " + fileUpload.PostedFile.FileName + " :" + txtAttachmentComments.Text + " :  " + ex.Message.ToString();
                    AttachmentsDS = Page.Session["Attachments"] as DataSet;
                }
            }
            else
            {
                uxFileInfo.Text = "You have not specified a file.";
                AttachmentsDS = Page.Session["Attachments"] as DataSet;
            }
            gdvAttachments.EditIndex = -1;
            if (AttachmentsDS != null)
            {
                // Set datasource and rebind
                gdvAttachments.DataSource = AttachmentsDS.Tables["Attachments"];
                gdvAttachments.DataBind();
            }


        }
        protected void btnShow_Click(object sender, EventArgs e)
        {
            pnlEdit.Attributes["style"] = "display: ;";
        }

        protected void gdvAttachments_Paging(object sender, GridViewPageEventArgs e)
        {
            gdvAttachments.PageIndex = e.NewPageIndex;
            gdvAttachments.DataSource = GetAttachmentDataSet().Tables["Attachments"];
            gdvAttachments.DataBind();
        }
        //protected void SaveAttachments_Click(object sender, EventArgs e)
        //{

        //}
        #endregion

        #region AttachmentsGridView DataSetManagement
        /// <summary>
        /// This creates a row in the dataset to add the content to so as to not hit database all the time. 
        /// </summary>
        /// <param name="myId"></param>
        /// <param name="eventTitle"></param>
        /// <param name="EventDate"></param>
        /// <param name="EventText"></param>
        /// <returns>Event Dataset for Binding</returns>
        private DataSet CreateAttachmentDataRow(int myId, string eventTitle, string Filename, string EventText)
        {
            //get the dataset in session
            AttachmentsDS = Page.Session["Attachments"] as DataSet;
            DataRow newEventRow = AttachmentsDS.Tables["Attachments"].NewRow();

            newEventRow["AttachmentId"] = myId;
            newEventRow["AttachmentTitle"] = eventTitle;
            newEventRow["AttachmentFilename"] = Filename;
            newEventRow["AttachmentExplaination"] = EventText;
            AttachmentsDS.Tables["Attachments"].Rows.Add(newEventRow);
            AttachmentsDS.Tables["Attachments"].AcceptChanges();
            Page.Session["Attachments"] = AttachmentsDS;
            return AttachmentsDS;
        }

        /// <summary>
        /// This Updates the Dataset and Session Event Object
        /// </summary>
        /// <param name="myId"></param>
        /// <param name="eventTitle"></param>
        /// <param name="EventDate"></param>
        /// <param name="EventText"></param>
        /// <returns>Event Dataset for Binding</returns>
        private DataSet UpdateAttachmentDataRow(int myId, string eventTitle, string Filename, string EventText)
        {
            //get the dataset in session
            AttachmentsDS = Page.Session["Attachments"] as DataSet;
            //find the row I need to update
            DataRow dr;
            dr = AttachmentsDS.Tables["Attachments"].Rows.Find(myId);
            if (dr == null)
            {
                uxFileInfo.Text = "Did not find the row";
            }
            else
            {
                dr["AttachmentId"] = myId;
                dr["AttachmentTitle"] = eventTitle;
                dr["AttachmentFilename"] = Filename;
                dr["AttachmentExplaination"] = EventText;
                AttachmentsDS.Tables["Attachments"].AcceptChanges();
                Page.Session["Attachments"] = AttachmentsDS;

            }
            return AttachmentsDS;

        }
        /// <summary>
        /// This deletes the record in the DataSet
        /// </summary>
        /// <param name="myId"></param>
        /// <returns>Event Dataset for Binding</returns>
        private DataSet DeleteAttachmentDataRow(int myId)
        {
            //get the dataset in session
            AttachmentsDS = Page.Session["Attachments"] as DataSet;
            //find the row I need to update
            DataRow dr;
            dr = AttachmentsDS.Tables["Attachments"].Rows.Find(myId);
            if (dr == null)
            {
                uxFileInfo.Text = "Did not find the row";
            }
            else
            {
                dr.Delete();
            }
            AttachmentsDS.AcceptChanges();
            Page.Session["Attachments"] = AttachmentsDS;
            return AttachmentsDS;

        }




        #endregion


        #endregion //Gridviews


        #region Page Controls Handing Disables/Enables/Clear content of controls.
        /// <summary>
        /// Handling Textfields enable disable clearing content Items
        /// </summary>
        protected void EnableTextboxes()
        {
            //set to false bc dont want user just changing the dropdown report value;
            DRreportNumberDDL.Enabled = true;
            //can change report type
            DRReportType.Enabled = true;
            rDistrict.Enabled = true;
            rManagerName.Enabled = true;
            rmanagerAddress.Enabled = true;
            rLocCityAddress.Enabled = true;
            rProvState.Enabled = true;
            rcompany.Enabled = true;
            rcustomercontact.Enabled = true;
            rcustomerphone.Enabled = true;
            rContactFax.Enabled = true;
            rCustomerEmail.Enabled = true;
            rOperatorName.Enabled = true;
            rOperatorPhone.Enabled = true;
            rOperatorExt.Enabled = true;
            rOperatorFax.Enabled = true;
            rOperatorEmail.Enabled = true;
            rDistrict.Enabled = true;
            rManagerName.Enabled = true;
            rmanagerAddress.Enabled = true;
            rLocCityAddress.Enabled = true;
            rProvState.Enabled = true;
            rInitiatorName.Enabled = true;
            rInitiatorPhone.Enabled = true;
            rInitiatorExt.Enabled = true;
            rInitiatorFax.Enabled = true;
            rInitiatorEmail.Enabled = true;
            DRCauseAnalysisTA.Disabled = false;
            DRDateTB.Enabled = true;
            DRDescription.Disabled = false;
            DRExecSummary.Disabled = false;
            DRfieldticket.Enabled = true;
            DRLatitudeTB.Enabled = true;
            DRLongitudeTB.Enabled = true;
            DRLSDTB.Enabled = true;
            DRMeridianTB.Enabled = true;
            DRnotes2.Disabled = false;
            Notes.Disabled = false;
            DRObservations.Disabled = false;
            DRPrefaceTA.Disabled = false;
            DRRangeTB.Enabled = true;
            DRSectionTB.Enabled = true;
            DRtitle.Enabled = true;
            DRTMDTB.Enabled = true;
            DRTownshipTB.Enabled = true;
            DRTVDTB.Enabled = true;
            DRUTMNTB.Enabled = true;
            DRwellname.Enabled = true;
            DRYTB.Enabled = true;
            CustomerDDL.Enabled = true;
            OperatorDDL.Enabled = true;
            EngineerDDL.Enabled = true;
            LocationDDL.Enabled = true;
            WellTypeDDL.Enabled = true;
            CasingSizeDDL.Enabled = true;
            LinerSizeDDL.Enabled = true;
            SystemTypeDDL.Enabled = true;
            ToolListDDL.Enabled = true;
            ProbOccurDDL.Enabled = true;
            CategoryDDL.Enabled = true;
            SubCategoryDDL.Enabled = true;

        }

        protected void DisableTextboxes()
        {
            //these two are accessible for view mode
            DRreportNumberDDL.Enabled = true;
            DRReportType.Enabled = true;


            rDistrict.Enabled = false;
            rManagerName.Enabled = false;
            rmanagerAddress.Enabled = false;
            rLocCityAddress.Enabled = false;
            rProvState.Enabled = false;
            rcompany.Enabled = false;
            rcustomercontact.Enabled = false;
            rcustomerphone.Enabled = false;
            rContactFax.Enabled = false;
            rCustomerEmail.Enabled = false;
            rOperatorName.Enabled = false;
            rOperatorPhone.Enabled = false;
            rOperatorExt.Enabled = false;
            rOperatorFax.Enabled = false;
            rOperatorEmail.Enabled = false;
            rDistrict.Enabled = false;
            rManagerName.Enabled = false;
            rmanagerAddress.Enabled = false;
            rLocCityAddress.Enabled = false;
            rProvState.Enabled = false;
            rInitiatorName.Enabled = false;
            rInitiatorPhone.Enabled = false;
            rInitiatorExt.Enabled = false;
            rInitiatorFax.Enabled = false;
            rInitiatorEmail.Enabled = false;

            DRCauseAnalysisTA.Disabled = true;
            DRDateTB.Enabled = false;
            DRDescription.Disabled = true;
            DRExecSummary.Disabled = true;
            DRfieldticket.Enabled = false;
            DRLatitudeTB.Enabled = false;
            DRLongitudeTB.Enabled = false;
            DRLSDTB.Enabled = false;
            DRMeridianTB.Enabled = false;
            DRnotes2.Disabled = true;
            Notes.Disabled = true;
            DRObservations.Disabled = true;
            DRPrefaceTA.Disabled = true;
            DRRangeTB.Enabled = false;
            DRSectionTB.Enabled = false;
            DRtitle.Enabled = false;
            DRTMDTB.Enabled = false;
            DRTownshipTB.Enabled = false;
            DRTVDTB.Enabled = false;
            DRUTMNTB.Enabled = false;
            DRwellname.Enabled = false;
            DRYTB.Enabled = false;
            CustomerDDL.Enabled = false;
            OperatorDDL.Enabled = false;
            EngineerDDL.Enabled = false;
            LocationDDL.Enabled = false;
            WellTypeDDL.Enabled = false;
            CasingSizeDDL.Enabled = false;
            LinerSizeDDL.Enabled = false;
            SystemTypeDDL.Enabled = false;
            ToolListDDL.Enabled = false;
            ProbOccurDDL.Enabled = false;
            CategoryDDL.Enabled = false;
            SubCategoryDDL.Enabled = false;
        }
        protected void ClearSupportTableText()
        {
            rDistrict.Text = "";
            rManagerName.Text = "";
            rmanagerAddress.Text = "";
            rLocCityAddress.Text = "";
            rProvState.Text = "";
            rcompany.Text = "";
            rcustomercontact.Text = "";
            rcustomerphone.Text = "";
            rContactFax.Text = "";
            rCustomerEmail.Text = "";
            rOperatorName.Text = "";
            rOperatorPhone.Text = "";
            rOperatorExt.Text = "";
            rOperatorFax.Text = "";
            rOperatorEmail.Text = "";
            rDistrict.Text = "";
            rManagerName.Text = "";
            rmanagerAddress.Text = "";
            rLocCityAddress.Text = "";
            rProvState.Text = "";
            rInitiatorName.Text = "";
            rInitiatorPhone.Text = "";
            rInitiatorExt.Text = "";
            rInitiatorFax.Text = "";
            rInitiatorEmail.Text = "";
        }
        protected void ClearCustomerModalTB()
        {
            customer_company.Text = "";
            customer_contact.Text = "";
            customer_contactphone.Text = "";
            customerExtension.Text = "";
            customerFax.Text = "";
            customer_contactemail.Text = "";
        }
        protected void ClearEngineerModalTextboxes()
        {
            initiatorname.Text = "";
            initiatorphone.Text = "";
            initiatorExt.Text = "";
            initiatorFax.Text = "";
            initiatoremail.Text = "";

        }
        protected void ClearOperatorModalTB()
        {
            operatorName.Text = "";
            operatorPhone.Text = "";
            operatorExt.Text = "";
            operatorFax.Text = "";
            operatorEmail.Text = "";
        }
        protected void ClearLocationModalTB()
        {
            district.Text = "";
            managerName.Text = "";
            address.Text = "";
            City.Text = "";
            Province.Text = "";
        }

        protected void ClearTextBoxes()
        {
            DRCauseAnalysisTA.Value = "";
            DRDateTB.Text = "";
            DRDescription.Value = "";
            DRExecSummary.Value = "";
            DRfieldticket.Text = "";
            DRLatitudeTB.Text = "";
            DRLongitudeTB.Text = "";
            DRLSDTB.Text = "";
            DRMeridianTB.Text = "";
            DRnotes2.Value = "";
            Notes.Value = "";
            DRObservations.Value = "";
            DRPrefaceTA.Value = "";
            DRRangeTB.Text = "";
            DRSectionTB.Text = "";
            DRtitle.Text = "";
            DRTMDTB.Text = "";
            DRTownshipTB.Text = "";
            DRTVDTB.Text = "";
            DRUTMNTB.Text = "";
            DRwellname.Text = "";
            DRYTB.Text = "";
        }

        protected void ClearEventModalText()
        {
            txtEventContent.Text = "";
            txtEventDate.Text = "";
            txtEventTitle.Text = "";
        }
        protected void ClearCauseModalText()
        {
            txtCauseContent.Text = "";
            txtCauseTitle.Text = "";
        }
        protected void ClearCorrectionModalText()
        {
            txtCorrectionContent.Text = "";
            txtCorrectionTitle.Text = "";
        }
        protected void ClearAttachmentModalText()
        {
            txtAttachmentComments.Text = "";
            txtAttachmentTitle.Text = "";
        }
        /// <summary>
        /// This function disables the add new buttons of Support Tables.
        /// </summary>
        protected void disableNewAddButtons()
        {
            NewCustomerBTN.Enabled = false;
            NewEngineerBTN.Enabled = false;
            NewLocationBTN.Enabled = false;
            NewOperatorBTN.Enabled = false;
        }
        /// <summary>
        /// This function disables the paging buttons First/Previous/Next/Last so that 
        /// functionality is stable.
        /// </summary>
        protected void disablePaging()
        {
            First.Enabled = false;
            Next.Enabled = false;
            Previous.Enabled = false;
            last.Enabled = false;
        }
        /// <summary>
        /// This function disables dropdown on adds/edits of records so that functionality
        /// is stable.
        /// </summary>
        protected void disableReportDropdown()
        {
            DRreportNumberDDL.Enabled = false;
        }
        protected void enablePaging()
        {
            First.Enabled = true;
            Previous.Enabled = true;
            Next.Enabled = true;
            last.Enabled = true;
        }
        protected void enableReportDropdown()
        {
            DRreportNumberDDL.Enabled = true;
        }
        /// <summary>
        /// This function enables the Support Table Add New during Edit / Add processes.
        /// </summary>
        protected void enableNewAddButtons()
        {
            NewCustomerBTN.Enabled = true;
            NewEngineerBTN.Enabled = true;
            NewLocationBTN.Enabled = true;
            NewOperatorBTN.Enabled = true;
        }

        #endregion

        #region Paging Code
        ///<summary>
        ///This handles Paging Buttons through records First/Previous/Next/Last
        ///
        ///</summary>
        ///




        protected void First_Click(object sender, EventArgs e)
        {
            //here we want to get the first record to display.
            getFullReportDetails(int.Parse(Session["first"].ToString()));
            DRreportNumberDDL.SelectedValue = Session["first"].ToString();
            //we want to reset the session page to 1
            Session["page"] = 1;
            //set the previous session to ==first 
            Session["previous"] = Session["first"];
            //set the next to == page +1
            Session["next"] = int.Parse(Session["page"].ToString()) + 1;
            //leave the session last bc always the same unless add is done.

            //handle paging buttons viewability
            checkButtonEnabled();
        }

        protected void Previous_Click(object sender, EventArgs e)
        {
            Details = Session["Details"] as DataSet;
            //if previous is enabled we want to get that record
            getFullReportDetails(int.Parse(Session["previous"].ToString()));
            DRreportNumberDDL.SelectedValue = Session["previous"].ToString();
            //session page becomes page -1 bc we've moved one back.
            Session["page"] = int.Parse(Session["page"].ToString()) - 1;
            //we set the session previous = page -1
            int previous = int.Parse(Session["page"].ToString()) - 1;
            Session["previous"] = Details.Tables["Details"].Rows[previous]["ReportId"].ToString();
            //we set the session next to page +1
            int next = int.Parse(Session["page"].ToString()) + 1;
            if (next == int.Parse(Session["totalCount"].ToString()))
            {
                next = next - 1;
                Session["next"] = Details.Tables["Details"].Rows[next]["ReportId"].ToString();
            }
            //we leave the session last alone bc always same unless an add is done.
            if ((int.Parse(Details.Tables["Details"].Rows.Count.ToString()) - 1) > int.Parse(Session["totalCount"].ToString()))
            {
                int count = Details.Tables["Details"].Rows.Count - 1;
                Session["last"] = Details.Tables["Details"].Rows[count]["ReportId"].ToString();
            }

            //handle paging buttons viewability
            checkButtonEnabled();
        }

        protected void Next_Click(object sender, EventArgs e)
        {
            Details = Session["Details"] as DataSet;
            //want next id to get that record
            getFullReportDetails(int.Parse(Session["next"].ToString()));
            DRreportNumberDDL.SelectedValue = Session["next"].ToString();
            //leave the session first stays same previous must now be adjusted
            //previous becomes page-1 
            int previous = int.Parse(Session["page"].ToString()) - 1;
            Session["previous"] = Details.Tables["Details"].Rows[previous]["ReportId"].ToString();

            //page becomes page +1 because weve moved ahead one.
            Session["page"] = int.Parse(Session["page"].ToString()) + 1;
            //next becomes page +1
            int next = int.Parse(Session["page"].ToString()) + 1;
            if (!(next > int.Parse(Session["totalCount"].ToString())))
            {
                Session["next"] = Details.Tables["Details"].Rows[next]["ReportId"].ToString();
            }
            //last stays the same unless add is done then count is included.
            if ((int.Parse(Details.Tables["Details"].Rows.Count.ToString()) - 1) > int.Parse(Session["totalCount"].ToString()))
            {
                int count = Details.Tables["Details"].Rows.Count - 1;
                Session["last"] = Details.Tables["Details"].Rows[count]["ReportId"].ToString();
            }
            //handle paging buttons viewability
            checkButtonEnabled();
        }

        protected void last_Click(object sender, EventArgs e)
        {
            //last becomes current page. 
            Details = Session["Details"] as DataSet;
            getFullReportDetails(int.Parse(Session["last"].ToString()));
            DRreportNumberDDL.SelectedValue = Session["last"].ToString();
            //page becomes total records counted.
            int count = Details.Tables["Details"].Rows.Count;
            Session["page"] = count;
            //next page+1 but we dont display it because it is past the last count
            Session["next"] = Session["last"].ToString();
            //last is also not displayed
            //previous becomes page -1
            count = count - 1;
            Session["previous"] = Details.Tables["Details"].Rows[count]["ReportId"].ToString();
            //first remains the same.
            //handle paging buttons viewability
            checkButtonEnabled();
        }
        /// <summary>
        /// Sets Session Page when Dropdown is used for the collection of a record so that the button paging is still accurate.
        /// </summary>
        /// <param name="reportId"></param>
        protected void getIndexofReportId(int reportId)
        {
            Details = Session["Details"] as DataSet;

            for (int i = 0; i < Details.Tables["Details"].Rows.Count; i++)
            {
                if (int.Parse(Details.Tables["Details"].Rows[i]["ReportId"].ToString()) == reportId)
                {
                    Session["page"] = i + 1;
                    i = Details.Tables["Details"].Rows.Count;
                }
            }
        }
        /// <summary>
        /// This function checks whether next or previous buttons should be displayed.
        /// </summary>
        protected void checkButtonEnabled()
        {
            int page = int.Parse(Session["page"].ToString());
            //compare for previous button enabled
            if ((page - 1) == 0)
            {
                Previous.Visible = false;
            }
            else { Previous.Visible = true; }
            //compare next and last button for display page + 1 == totalRowCount -1;
            if (((page + 1) > int.Parse(Session["totalCount"].ToString())) || ((page + 1) == int.Parse(Session["totalCount"].ToString())))
            {
                Next.Visible = false;
                //we hit first 
                if ((page - 1) == 0)
                {
                    Next.Visible = true;
                }
            }
            else { Next.Visible = true; }
            if ((page) == int.Parse(Session["totalCount"].ToString()))
            {
                last.Visible = false;
            }
            else { last.Visible = true; }
        }
        #endregion








    }
}
