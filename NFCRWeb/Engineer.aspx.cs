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
    public partial class WebForm3 : System.Web.UI.Page
    {
        DataSet Engineers = null;
        DataSet Engineer = null;
        int engineerId =0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {  //when not postback, means hitting page first time.
                GetDropDownList();
                //MessageLBL.Text = "Not postback.";
                Page.Session["oppID"] = 0;
                DisableTextboxes();
                AddNew.Enabled = true;
                //edit and print are available only when data in database. initial entry of first record only AddNew
                Edit.Enabled = true;
                Print.Enabled = true;
                int.TryParse(EngineerDDL.SelectedValue.ToString(), out engineerId);
                GetEngineerForListing(engineerId);

            }
            else { }
        }

        protected void DeleteBN_Click(object sender, EventArgs e)
        {
            int.TryParse(EngineerDDL.SelectedValue.ToString(), out engineerId);
            bool engineerDelete = NCRClass.NCRController.DeleteEngineer(engineerId);
            if (engineerDelete == false)
            {
                MessageLBL.Text = "The Engineer Delete Failed. Please contact your administrator.";
            }
            else
            {
                MessageLBL.Text = "The Engineer " + initiatorname.Text + " was deleted succesfully.";
                //once successfull should go back to main view mode again.
                GetDropDownList();
                ClearTextboxes();
                //all textbox Items are Disabled
                DisableTextboxes();
                int.TryParse(EngineerDDL.SelectedValue.ToString(), out engineerId);
                GetEngineerForListing(engineerId);
                //initial entry of first record only AddNew
                AddNew.Enabled = true;
                //edit and print are available only when data in database. 
                Edit.Enabled = true;
                Print.Enabled = true;
                DeleteBN.Enabled = false;
                Cancel.Enabled = false;
                Save.Enabled = false;
            }
        }

        protected void AddNew_Click(object sender, EventArgs e)
        {//when adding a new record, the save cancel buttons are enabled no others 
            //must clear textboxes
            Session["oppID"] = 0;
            ClearTextboxes();
            EnableTextboxes();
            EngineerDDL.SelectedIndex = 0;
            //change buttons
            AddNew.Enabled = false;
            Save.Enabled = true;
            Cancel.Enabled = true;
            Edit.Enabled = false;
        }

        protected void Edit_Click(object sender, EventArgs e)
        {//Save cancel Print delete enabled
            //this is to enable all text fields so as to edit the content of the customer not actually update?
            int.TryParse(EngineerDDL.SelectedValue.ToString(), out engineerId);
            Session["oppID"] = engineerId;
            EnableTextboxes();
            Save.Enabled = true;
            AddNew.Enabled = false;
            Edit.Enabled = false;
            DeleteBN.Enabled = true;
            Cancel.Enabled = true;
            Print.Enabled = true;
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            int.TryParse(EngineerDDL.SelectedValue.ToString(), out engineerId);

            if ( Session["oppID"].ToString() == "0")
            { //adding a new engineer
                try
                {
                    //should we have a try catch here to adjust buttons/textboxes if fails
                    engineerId = NCRClass.NCRController.AddEngineer(initiatorname.Text, initiatorphone.Text, initiatorExt.Text, initiatorFax.Text, initiatoremail.Text);
                    if (engineerId > 0)
                    {
                        MessageLBL.Text = "Your Engineer " + initiatorname.Text + " has been added check the dropdown.";
                        //buttons should be same as on initial pageload on success
                        ClearTextboxes();
                        DisableTextboxes();
                        GetDropDownList();
                        EngineerDDL.SelectedValue = engineerId.ToString();
                        GetEngineerForListing(engineerId);
                        //once successfull buttons change
                        AddNew.Enabled = true;
                        //edit and print are available only when data in database. initial entry of first record only AddNew
                        Edit.Enabled = true;
                        Print.Enabled = true;
                        DeleteBN.Enabled = false;
                        Save.Enabled = false;
                        Cancel.Enabled = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageLBL.Text = ex.Message.ToString();
                    Edit.Enabled = false;
                    DeleteBN.Enabled = false;
                    Save.Enabled = false;
                }
            }
            else
            {
                //updating engineer
                try
                {

                    NCRClass.NCRController.UpdateEngineer(engineerId, initiatorname.Text, initiatorphone.Text, initiatorExt.Text, initiatorFax.Text, initiatoremail.Text);
                    MessageLBL.Text = initiatorname.Text + " has been updated.";
                    //buttons should be same as on initial pageload on success
                    //we updated it was successful but delete button is avail and save is avail 
                    //when we should be back to view customers instance
                    DisableTextboxes();
                    GetDropDownList();
                    EngineerDDL.SelectedValue = engineerId.ToString();
                    GetEngineerForListing(engineerId);
                    //once successfull buttons change
                    AddNew.Enabled = true;
                    //edit and print are available only when data in database. initial entry of first record only AddNew
                    Edit.Enabled = true;
                    Print.Enabled = true;
                    DeleteBN.Enabled = false;
                    Save.Enabled = false;
                    Cancel.Enabled = false;

                }
                catch (Exception ex)
                {//update failed
                    MessageLBL.Text = ex.Message.ToString();
                    Edit.Enabled = false;
                    Save.Enabled = false;
                    DeleteBN.Enabled = false;
                }
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            ClearTextboxes();
            GetDropDownList();
            //all textbox Items are Disabled
            DisableTextboxes();
            AddNew.Enabled = true;
            //edit and print are available only when data in database. initial entry of first record only AddNew
            Edit.Enabled = true;
            Print.Enabled = true;
            DeleteBN.Enabled = false;
            Save.Enabled = false;
            Cancel.Enabled = false;
            int.TryParse(EngineerDDL.SelectedValue.ToString(), out engineerId);
            GetEngineerForListing(engineerId);
        }

        protected void Print_Click(object sender, EventArgs e)
        {
            try
            {
                //hierarchicalGroupingReport.PrintToPrinter(1, false, 1, 99);
                Session["ctrl"] = EngineerPanel;
                ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('Print.aspx','PrintMe','height=300px,width=300px,scrollbars=1');</script>");

            }
            catch (Exception ex)
            {
                MessageLBL.Text = ex.Message.ToString();
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
                    Edit.Enabled = true;
                    AddNew.Enabled = false;
                    Save.Enabled = false;
                    DeleteBN.Enabled = false;
                    //all textbox Items are Disabled
                    DisableTextboxes();
                }
                catch (Exception ex)
                {

                    MessageLBL.Text = "There is no record for that Engineer." + ex.Message.ToString();
                }
            }
            else
            {
                MessageLBL.Text = "You must either select a Engineer or click the add Button to add a new Engineer.";
                AddNew.Enabled = true;
            }
            MessageLBL.Text = EngineerDDL.SelectedValue.ToString();
        }

        protected void GetDropDownList() 
        {
            Engineers = NCRClass.NCRController.LookupAllEngineers();
            EngineerDDL.DataSource = Engineers.Tables["Engineers"];
            EngineerDDL.DataTextField = "InitiatorName";
            EngineerDDL.DataValueField = "InitiatorID";
            EngineerDDL.DataBind();
            
        }
        protected void GetEngineerForListing(int engineerId)
        {
            Engineer = NCRClass.NCRController.LookupEngineer(engineerId);
            initiatorname.Text = Engineer.Tables["Engineer"].Rows[0]["InitiatorName"].ToString();
            initiatorphone.Text = Engineer.Tables["Engineer"].Rows[0]["InitiatorPhone"].ToString();
            initiatorExt.Text = Engineer.Tables["Engineer"].Rows[0]["InitiatorExt"].ToString();
            initiatorFax.Text = Engineer.Tables["Engineer"].Rows[0]["InitiatorFax"].ToString();
            initiatoremail.Text = Engineer.Tables["Engineer"].Rows[0]["InitiatorEmail"].ToString();

        }
       protected void DisableTextboxes()
       {
           initiatorname.Enabled= false;
           initiatorphone.Enabled = false;
           initiatorExt.Enabled = false;
           initiatorFax.Enabled = false;
           initiatoremail.Enabled = false;
       }
       protected void EnableTextboxes()
       {
           initiatorname.Enabled = true;
           initiatorphone.Enabled = true; 
           initiatorExt.Enabled = true; 
           initiatorFax.Enabled = true;
           initiatoremail.Enabled = true; 
       }

       protected void ClearTextboxes()
       {
           initiatorname.Text = "";
           initiatorphone.Text = "";
           initiatorExt.Text = "";
           initiatorFax.Text = "";
           initiatoremail.Text = "";
       }

    }
}
