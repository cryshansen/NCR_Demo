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
using C1.Web.UI.Controls.C1GridView;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

namespace NCRFTRWeb
{
    public partial class WebForm10 : System.Web.UI.Page
    {
        #region Local Variables
        /// <summary>
        /// Local Variables
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 

        int parentId = 0;
        int customerId = 0;
        int operatorId = 0;
        int engineerId=0;
        int locationId=0;
        //int reportId=0;
        int WellTypeId = 0;
        int CasingSizeId = 0;
        int LinerSizeId = 0;
        int SystemTypeId = 0;
        int ToolListId = 0;
        int problemId = 0;
        int subcatid = 0;
        DataSet Results = null;
        DataSet Customers = null;
        DataSet Engineers = null;
        DataSet Locations = null;
        DataSet Operators = null;
        DataSet WellTypes = null;
        DataSet CasingSizes = null;
        DataSet LinerSizes = null;
        DataSet SystemTypes = null;
        DataSet ToolLists = null;
        DataSet Problems = null;
        DataSet Categories = null;
        DataSet ds = new DataSet();
        
        //SearchReport rpReport = new SearchReport();
        DataSet ReportsDS = null;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ResultPanel.Visible = false;
                SearchPanel.Visible = true;
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
                Random random = new Random();
                int randno = random.Next(1000);
                Session["randno"] = randno;
                
            }
        }

            #region GetDropdowns
        protected void GetCustomerDropDownList()
        {
            Customers = NCRClass.NCRController.LookupAllCustomers();
            //if no customers add button is enabled else edit button is enabled
            CustomerDDL.DataSource = Customers.Tables["Customers"];
            this.CustomerDDL.DataTextField = "CompanyName";
            this.CustomerDDL.DataValueField = "CustomerID";
            CustomerDDL.DataBind();
            CustomerDDL.Items.Insert(0, " ");

        }
        protected void GetEngineerDropDownList()
        {
            Engineers = NCRClass.NCRController.LookupAllEngineers();
            EngineerDDL.DataSource = Engineers.Tables["Engineers"];
            EngineerDDL.DataTextField = "InitiatorName";
            EngineerDDL.DataValueField = "InitiatorID";
            EngineerDDL.DataBind();
            EngineerDDL.Items.Insert(0, " ");
        }
        protected void GetLocationsDropDownList()
        {
            Locations = NCRClass.NCRController.LookupAllLocations();
            LocationDDL.DataSource = Locations.Tables["Locations"];
            LocationDDL.DataTextField = "District";
            LocationDDL.DataValueField = "LocationID";
            LocationDDL.DataBind();
            LocationDDL.Items.Insert(0, " ");
        }
        protected void GetOperatorsDropDownList()
        {
            Operators = NCRClass.NCRController.LookupAllOperators();
            OperatorDDL.DataSource = Operators.Tables["Operators"];
            OperatorDDL.DataTextField = "OperatorName";
            OperatorDDL.DataValueField = "OperatorID";
            OperatorDDL.DataBind();
            OperatorDDL.Items.Insert(0, "");
        }
        protected void GetWellTypeDropDownList()
        {
            WellTypes = NCRClass.NCRController.LookupAllWellTypes();
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
            CasingSizeDDL.Items.Insert(0, "");

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
            ToolListDDL.Items.Insert(0, "");
        }
        protected void GetProblemDropDownList()
        {
            Problems = NCRClass.NCRController.LookupAllProblems();
            ProbOccurDDL.DataSource = Problems.Tables["Problems"];
            ProbOccurDDL.DataTextField = "ProblemName";
            ProbOccurDDL.DataValueField = "POID";
            ProbOccurDDL.DataBind();
            ProbOccurDDL.Items.Insert(0, " ");
        }
        protected void GetCategoryDropDownList(int parentId)
        {
            Categories = NCRClass.NCRController.LookupAllCategoriesByParentId(parentId);
            CategoryDDL.DataSource = Categories.Tables["Categories"];
            CategoryDDL.DataTextField = "CategoryName";
            CategoryDDL.DataValueField = "OCID";
            CategoryDDL.DataBind();
            CategoryDDL.Items.Insert(0, " ");
            if (parentId == 0)
            {
                SubCategoryDDL.Items.Clear();
            } if (parentId != 0)
            {
                //AddNewSub.Visible = true;
            }
        }
        protected void GetSubCategoryDropDownList(int subcatid)
        {
            Categories = NCRClass.NCRController.LookupAllCategoriesByParentId(subcatid);
            SubCategoryDDL.DataSource = Categories.Tables["Categories"];
            SubCategoryDDL.DataTextField = "CategoryName";
            SubCategoryDDL.DataValueField = "OCID";
            SubCategoryDDL.DataBind();
            SubCategoryDDL.Items.Insert(0, " ");

        }


        #endregion
        //------------   DropDown Handling     -------------------------//

        #region Selected Index Changed

        protected void CategoryDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            //where parent id = 
            //get subcategories for spec parentid
            //get all info for specific category
            //when i reselect from parent dropdown, the subdropdown should populate. but doesnt.

            //MessageLBL.Text = ProbOccurDDL.SelectedValue.ToString();
            int.TryParse(CategoryDDL.SelectedValue.ToString(), out parentId);
            if (parentId != 0)
            {
                try
                {
                    MessageLBL.Text = parentId.ToString();
                    Categories = NCRClass.NCRController.LookupAllCategoriesByParentId(parentId);
                    SubCategoryDDL.DataSource = Categories.Tables["Categories"];
                    SubCategoryDDL.DataTextField = "CategoryName";
                    SubCategoryDDL.DataValueField = "OCID";
                    SubCategoryDDL.DataBind();
                    SubCategoryDDL.Items.Insert(0, " ");
                }
                catch (Exception ex)
                {
                    MessageLBL.Text = "There is no record for that Category." + ex.Message.ToString();
                }
            }
            else
            {
                SubCategoryDDL.SelectedIndex = 0;
                MessageLBL.Text = "You must either select a Category or click the add Button to add a new Engineer.";
            }
            MessageLBL.Text = CategoryDDL.SelectedValue.ToString();
        }

        #endregion

        protected void Search_Click(object sender, EventArgs e)
        {
            SearchPanel.Visible = false;
            MessageLBL.Text = "";
            //grab all filled in items
            int.TryParse(CustomerDDL.SelectedValue.ToString(), out customerId);
            //Location
            int.TryParse(LocationDDL.SelectedValue.ToString(), out locationId);
            //Engineer
            int.TryParse(EngineerDDL.SelectedValue.ToString(), out engineerId);
            //Operator
            int.TryParse(OperatorDDL.SelectedValue.ToString(), out operatorId);
            int.TryParse(WellTypeDDL.SelectedValue.ToString(), out WellTypeId);
            int.TryParse(CasingSizeDDL.SelectedValue.ToString(), out CasingSizeId);
            int.TryParse(LinerSizeDDL.SelectedValue.ToString(), out LinerSizeId);
            int.TryParse(ProbOccurDDL.SelectedValue.ToString(), out problemId);
            int.TryParse(SystemTypeDDL.SelectedValue.ToString(), out SystemTypeId);
            int.TryParse(ToolListDDL.SelectedValue.ToString(), out ToolListId);
            int.TryParse(CategoryDDL.SelectedValue.ToString(), out parentId);
            int.TryParse(SubCategoryDDL.SelectedValue.ToString(), out subcatid);
            
            try
            {
              Results=  NCRClass.NCRController.SearchFields(customerId, operatorId, engineerId, locationId, SearchReportTypeDDL.SelectedValue.ToString(), SearchDateTB.Text, DRtitle.Text, DRfieldticket.Text, DRwellname.Text, WellTypeId, DRLSDTB.Text, DRSectionTB.Text, DRTownshipTB.Text, DRRangeTB.Text, DRMeridianTB.Text, CasingSizeId, LinerSizeId, DRTVDTB.Text, DRTMDTB.Text, ToolListId, SystemTypeId, ExecSummaryTB.Text, DescriptionTB.Text, ObservationsTB.Text, NotesTB.Text, Notes2TB.Text, problemId, parentId, subcatid, PrefaceTB.Text, EventsTB.Text, AttachmentTB.Text, CausesTB.Text, CauseAnalysisTB.Text, RemediationTB.Text);
              ResultPanel.Visible = true;
              ResultGV.DataSource = Results.Tables["Result"];
              ResultGV.DataBind();
              Session["ReportDS"] = Results;

            }catch(Exception ex)
            {
                MessageLBL.Text = ex.Message.ToString();
            }
            
        }

        protected void ReSearch_Click(object sender, EventArgs e)
        {
            ResultPanel.Visible = false;
            SearchPanel.Visible = true;
        }

        protected void ReportBTN_Click(object sender, EventArgs e)
        {
            //string strId = string.Empty;
            //foreach (GridViewRow myrow in ResultGV.Rows)
            //{

            //    if (((CheckBox)myrow.FindControl("Report")).Checked == true)
            //    {

            //        strId = ((Label)myrow.FindControl("ReportId")).Text;
            //        String ReportNo = ((Label)myrow.FindControl("ReportNumber")).Text;
            //        String Title = ((Label)myrow.FindControl("Title")).Text;
            //        String Company = ((Label)myrow.FindControl("Company")).Text;
            //        String Customer = ((Label)myrow.FindControl("Customer")).Text;
            //        String ReportDate = ((Label)myrow.FindControl("ReportDate")).Text;
            //        CreatePrintDataRow(int.Parse(strId.ToString()), ReportNo, Title, Company, Customer, ReportDate);

            //    }
            //}
            
                //this writes the selected rows of the search values to an xml recordset for printing
                   //need a filename with random number


            //randno = Session["randno"];
            string xmlstring = string.Concat(Session["randno"], "ReportSet.xml");
            xmlstring = string.Concat("~/", xmlstring);
            //need to save as session var per user
            StreamWriter xmlDoc = new StreamWriter(Server.MapPath(xmlstring), false);
            ds = Session["ReportDS"] as DataSet;

            //ds = Session["Reports"] as DataSet;
            ds.WriteXml(xmlDoc);
            xmlDoc.Close();
            Response.Redirect("DetailsReportViewer.aspx?xmlString="+string.Concat(Session["randno"],"ReportSet"));
            
            
        }
 
        private void PrintReport()
    {
        try
        {
            //hierarchicalGroupingReport.PrintToPrinter(1, false, 1, 99);
            //Session["ctrl"] = PrintPanel;
            //ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('Print.aspx','PrintMe','height=300px,width=300px,scrollbars=1');</script>");

        }
        catch (Exception ex)
        {
            MessageLBL.Text = ex.Message;
        }
    
    }
        private DataSet isDataSetExist()
        {
            ReportsDS = Page.Session["Reports"] as DataSet;
            if (ReportsDS == null)
            {
                ReportsDS = new DataSet();
                DataTable Reports = new DataTable();
                Reports.TableName = "ReportsTB";
                ReportsDS.Tables.Add(Reports);

                //Add Columns to Table
                Reports.Columns.Add("ReportId", typeof(int));
                Reports.Columns.Add("ReportNumber",typeof(string));
                Reports.Columns.Add("Title",typeof(string));
                Reports.Columns.Add("Company",typeof(string));
                Reports.Columns.Add("Customer",typeof(string));
                Reports.Columns.Add("ReportDate",typeof(DateTime));
            }
            return ReportsDS;
        
        }
        private void CreatePrintDataRow(int myId,string ReportNumber,string Title, string Company, string Customer,string ReportDate)
        {
            //get the dataset in session
            ReportsDS = isDataSetExist();
            DataRow newReportRow = ReportsDS.Tables["ReportsTB"].NewRow();
            newReportRow["ReportId"] = myId;
            newReportRow["ReportNumber"] = ReportNumber;
            newReportRow["Title"] = Title;
            newReportRow["Company"] = Company;
            newReportRow["Customer"] = Customer;
            newReportRow["ReportDate"] = DateTime.Parse(ReportDate.ToString());
            ReportsDS.Tables["ReportsTB"].Rows.Add(newReportRow);
            ReportsDS.Tables["ReportsTB"].AcceptChanges();
            Page.Session["Reports"] = ReportsDS;
            
        }

    }
    }

