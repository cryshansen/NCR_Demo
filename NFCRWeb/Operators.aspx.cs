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
    public partial class WebForm5 : System.Web.UI.Page
    {
        DataSet Operators = null;
        DataSet Operator = null;
        int operatorId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {  //when not postback, means hitting page first time.
                GetDropDownList();
                MessageLBL.Text = "Not postback.";
                DisableTextboxes();
                AddNew.Enabled = true;
                //edit and print are available only when data in database. initial entry of first record only AddNew
                Edit.Enabled = true;
                Print.Enabled = true;

            }
            else { MessageLBL.Text = "is postback."; }

        }

        protected void DeleteBN_Click(object sender, EventArgs e)
        {
            int.TryParse(OperatorDDL.SelectedValue.ToString(), out operatorId);
            bool operatorDelete = NCRClass.NCRController.DeleteOperator(operatorId);
            if (operatorDelete == false)
            {
                MessageLBL.Text = "The Operator Delete Failed. Please contact your administrator.";
            }
            else
            {
                MessageLBL.Text = "The Operator " + operatorName.Text + " was deleted succesfully.";
                //once successfull should go back to main view mode again.
                GetDropDownList();
                ClearTextboxes();
                //all textbox Items are Disabled
                DisableTextboxes();
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
        {
            //when adding a new record, the save cancel buttons are enabled no others 
            //must clear textboxes
            ClearTextboxes();
            if (operatorId == 0)
            {
                EnableTextboxes();
                OperatorDDL.SelectedIndex = 0;
                //change buttons
                AddNew.Enabled = false;
                Save.Enabled = true;
                Cancel.Enabled = true;
                Edit.Enabled = false;
                //this will need to clear textboxes bc sometimes there will be some in there from dropdown select?
                MessageLBL.Text = "AddNew? First or second.";

            }
            else
            {
                //get the dropdown again 
                GetDropDownList();
                OperatorDDL.SelectedIndex = 0;
                AddNew.Enabled = false;
                Save.Enabled = true;
                Cancel.Enabled = true;
                Edit.Enabled = false;
                EnableTextboxes();
                MessageLBL.Text = "You must select the empty value from the dropdown first.";

            }
        }

        protected void Edit_Click(object sender, EventArgs e)
        {
            //Save cancel Print delete enabled
            //this is to enable all text fields so as to edit the content of the customer not actually update?
            EnableTextboxes();
            Save.Enabled = true;
            AddNew.Enabled = false;
            Edit.Enabled = false;
            DeleteBN.Enabled = true;
            Cancel.Enabled = true;
            Print.Enabled = true;
            MessageLBL.Text = "Edit First or second.";

        }

        protected void Save_Click(object sender, EventArgs e)
        {
            int.TryParse(OperatorDDL.SelectedValue.ToString(), out operatorId);

            if (operatorId == 0)
            { //adding a new customer
                //should we have a try catch here to adjust buttons/textboxes if fails?
                operatorId = NCRClass.NCRController.AddOperator(operatorName.Text,operatorPhone.Text,operatorExt.Text,operatorFax.Text,operatorEmail.Text);
                if (operatorId > 0)
                {
                    MessageLBL.Text = "Your Operator " + operatorName.Text + " has been added check the dropdown.";
                    //buttons should be same as on initial pageload on success
                    ClearTextboxes();
                    DisableTextboxes();
                    GetDropDownList();
                    OperatorDDL.SelectedValue = operatorId.ToString();
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
            else
            {
                //updating engineer
                try
                {

                    NCRClass.NCRController.UpdateOperator(operatorId, operatorName.Text, operatorPhone.Text, operatorExt.Text, operatorFax.Text, operatorEmail.Text);
                    MessageLBL.Text = operatorId + " Your Operator " + operatorName.Text + " has been updated.";
                    //buttons should be same as on initial pageload on success
                    //we updated it was successful but delete button is avail and save is avail 
                    //when we should be back to view customers instance
                    DisableTextboxes();
                    GetDropDownList();
                    OperatorDDL.SelectedValue = operatorId.ToString();
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
                    MessageLBL.Text = ex.ToString();
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
            DisableTextboxes();
            AddNew.Enabled = true;
            Edit.Enabled = true;
            Print.Enabled = true;
            DeleteBN.Enabled = false;
            Save.Enabled = false;
            Cancel.Enabled = false;
        }

        protected void Print_Click(object sender, EventArgs e)
        {
            try
            {
                //hierarchicalGroupingReport.PrintToPrinter(1, false, 1, 99);
                Session["ctrl"] = OperatorPanel;
                ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('Print.aspx','PrintMe','height=300px,width=300px,scrollbars=1');</script>");

            }
            catch (Exception ex)
            {
                MessageLBL.Text = ex.Message;
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
                    Operator = NCRClass.NCRController.LookupOperator(operatorId);
                    operatorName.Text = Operator.Tables["Operator"].Rows[0]["OperatorName"].ToString();
                    operatorPhone.Text = Operator.Tables["Operator"].Rows[0]["OperatorPhone"].ToString();
                    operatorExt.Text= Operator.Tables["Operator"].Rows[0]["OperatorExt"].ToString();
                    operatorFax.Text = Operator.Tables["Operator"].Rows[0]["OperatorFax"].ToString();
                    operatorEmail.Text = Operator.Tables["Operator"].Rows[0]["OperatorEmail"].ToString();
                    Edit.Enabled = true;
                    AddNew.Enabled = false;
                    Save.Enabled = false;
                    DeleteBN.Enabled = false;
                    //all textbox Items are Disabled
                    DisableTextboxes();
                }
                catch (Exception ex)
                {

                    MessageLBL.Text = "There is no record for that Operator." + ex.Message.ToString();
                }
            }
            else
            {
                AddNew.Enabled = true;
            }
        }
        protected void GetDropDownList()
        {
            Operators = NCRClass.NCRController.LookupAllOperators();
            OperatorDDL.DataSource = Operators.Tables["Operators"];
            OperatorDDL.DataTextField = "OperatorName";
            OperatorDDL.DataValueField = "OperatorID";
            OperatorDDL.DataBind();
            OperatorDDL.Items.Insert(0, "");
        }
        protected void DisableTextboxes()
        {
            operatorName.Enabled = false;
            operatorPhone.Enabled = false;
            operatorExt.Enabled = false;
            operatorFax.Enabled = false;
            operatorEmail.Enabled = false;
        }
        protected void EnableTextboxes()
        {
            operatorName.Enabled = true;
            operatorPhone.Enabled = true;
            operatorExt.Enabled = true;
            operatorFax.Enabled = true;
            operatorEmail.Enabled = true;
        }
        protected void ClearTextboxes()
        {
            operatorName.Text = "";
            operatorPhone.Text = "";
            operatorExt.Text = "";
            operatorFax.Text = "";
            operatorEmail.Text = "";
        }

    }
}
