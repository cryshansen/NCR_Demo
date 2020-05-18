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
    public partial class WebForm2 : System.Web.UI.Page
    {
        DataSet Customers = null;
        DataSet Customer = null;
        int customerId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            //get all customers for dropdown list. If none check for initial start up
            if (!Page.IsPostBack)
            {  //when not postback, means hitting page first time.
                GetDropDownList();
                //MessageLBL.Text = "I am not posted back.";
                Page.Session["custID"] = 0;
                //all textbox Items are Disabled
                DisableTextboxes();
                AddNew.Enabled = true;
                //edit and print are available only when data in database. initial entry of first record only AddNew
                Edit.Enabled = true;
                Print.Enabled = true;
                int.TryParse(CustomerDDL.SelectedValue.ToString(), out customerId);
                GetCustomerForListing(customerId);
            }
           
        }
        protected void DeleteBN_Click(object sender, EventArgs e)
        {
            int.TryParse( CustomerDDL.SelectedValue.ToString(), out customerId);
            bool customerDelete = NCRClass.NCRController.DeleteCustomer(customerId);
            if (customerDelete == false)
            {
                MessageLBL.Text = "The Company Delete Failed. Please contact your administrator.";
            }
            else { 
                
                MessageLBL.Text = "The Company "+ customer_company.Text +" was deleted succesfully."; 

                //once successfull should go back to main view mode again.
                GetDropDownList();
                ClearTextBoxes();
                //all textbox Items are Disabled
                DisableTextboxes();
                int.TryParse(CustomerDDL.SelectedValue.ToString(), out customerId);
                GetCustomerForListing(customerId);
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
        protected void Edit_Click(object sender, EventArgs e)
        { //Save cancel Print delete enabled
            //this is to enable all text fields so as to edit the content of the customer not actually update?
            int.TryParse(CustomerDDL.SelectedValue.ToString(),out customerId);
            Page.Session["custID"] = customerId;
            EnableTextboxes();
            Save.Enabled = true;
            AddNew.Enabled = false;
            Edit.Enabled = false;
            DeleteBN.Enabled = true;
            Cancel.Enabled = true;
            Print.Enabled = true;
        }

        protected void AddNew_Click(object sender, EventArgs e)
        {
            //when adding a new record, the save cancel buttons are enabled no others 
            //must clear textboxes
            //set session customerID=0 so we can track the user movement
            Session["custID"] = 0;
            ClearTextBoxes();
            EnableTextboxes();
            //change buttons
            AddNew.Enabled = false;
            Save.Enabled = true;
            Cancel.Enabled = true;
            Edit.Enabled = false;
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            int.TryParse(CustomerDDL.SelectedValue.ToString(), out customerId);
            
            if ( Session["custID"].ToString() == "0")
            { //adding a new customer
                //should we have a try catch here to adjust buttons/textboxes if fails?
                try
                {
                    customerId = NCRClass.NCRController.AddCustomer(customer_company.Text, customer_contact.Text, customer_contactphone.Text, customerExtension.Text, customerFax.Text, customer_contactemail.Text);
                    if (customerId > 0)
                    {
                        MessageLBL.Text = customer_company.Text + " has been added.";
                        //buttons should be same as on initial pageload on success
                        ClearTextBoxes();
                        DisableTextboxes();
                        GetDropDownList();
                        CustomerDDL.SelectedValue = customerId.ToString();
                        GetCustomerForListing(customerId);
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
                    Save.Enabled = false;
                    DeleteBN.Enabled = false;
                }
            }
            else
            {
                //updating customer
                try
                {

                    NCRClass.NCRController.UpdateCustomer(customerId, customer_company.Text, customer_contact.Text, customer_contactphone.Text, customerExtension.Text, customerFax.Text, customer_contactemail.Text);
                    MessageLBL.Text = customerId +" Your Company " + customer_company.Text + " has been updated.";
                    //buttons should be same as on initial pageload on success
                    //we updated it was successful but delete button is avail and save is avail 
                    //when we should be back to view customers instance
                    DisableTextboxes();
                    GetDropDownList();
                    CustomerDDL.SelectedValue = customerId.ToString();
                    GetCustomerForListing(customerId);
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
                    MessageLBL.Text = ex.Message.ToString() ;
                    Edit.Enabled = false;
                    Save.Enabled = false;
                    DeleteBN.Enabled = false;
                }
            }
        }

 

        protected void Cancel_Click(object sender, EventArgs e)
        {
            ClearTextBoxes();
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
            int.TryParse(CustomerDDL.SelectedValue.ToString(), out customerId);
            GetCustomerForListing(customerId);
        }

        protected void Print_Click(object sender, EventArgs e)
        {
            try
            {
                //hierarchicalGroupingReport.PrintToPrinter(1, false, 1, 99);
                Session["ctrl"] = CustomerPanel;
                ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('Print.aspx','PrintMe','height=300px,width=300px,scrollbars=1');</script>");
                
            }
            catch (Exception ex)
            {
                MessageLBL.Text =  ex.Message.ToString();
            }
        }

        protected void CustomerDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            int.TryParse(CustomerDDL.SelectedValue.ToString(), out customerId);
            try
            {
                GetCustomerForListing(customerId);
                Edit.Enabled = true;
                AddNew.Enabled = true;
                Save.Enabled = false;
                DeleteBN.Enabled = false;
                Cancel.Enabled = false;
                //all textbox Items are Disabled
                DisableTextboxes();
            }
            catch (Exception exeception)
            {

                MessageLBL.Text = "There is no record for that company." + exeception.Message.ToString();
            }
        }        

        protected void GetDropDownList()
        {
            Customers = NCRClass.NCRController.LookupAllCustomers();
            //if no customers add button is enabled else edit button is enabled
            CustomerDDL.DataSource = Customers.Tables["Customers"];
            this.CustomerDDL.DataTextField = "CompanyName";
            this.CustomerDDL.DataValueField = "CustomerID";
            CustomerDDL.DataBind();
            //CustomerDDL.Items.Insert(0, " ");

        }
        protected void GetCustomerForListing(int customerId)
        {
            Customer = NCRClass.NCRController.LookupCustomer(customerId);
            customer_company.Text = Customer.Tables["Customer"].Rows[0]["CompanyName"].ToString();
            customer_contact.Text = Customer.Tables["Customer"].Rows[0]["ContactName"].ToString();
            customer_contactphone.Text = Customer.Tables["Customer"].Rows[0]["ContactPhone"].ToString();
            customerExtension.Text = Customer.Tables["Customer"].Rows[0]["ContactExt"].ToString();
            customerFax.Text = Customer.Tables["Customer"].Rows[0]["ContactFax"].ToString();
            customer_contactemail.Text = Customer.Tables["Customer"].Rows[0]["ContactEmail"].ToString();
        }

        protected void EnableTextboxes()
        {
            customer_company.Enabled = true;
            customer_contact.Enabled = true;
            customer_contactphone.Enabled = true;
            customerExtension.Enabled = true;
            customerFax.Enabled = true;
            customer_contactemail.Enabled = true;
        }

        protected void DisableTextboxes()
        {
            customer_company.Enabled = false;
            customer_contact.Enabled = false;
            customer_contactphone.Enabled = false;
            customerExtension.Enabled = false;
            customerFax.Enabled = false;
            customer_contactemail.Enabled = false;
        }

        protected void ClearTextBoxes()
        {
            customer_company.Text = "";
            customer_contact.Text = "";
            customer_contactphone.Text = "";
            customerExtension.Text = "";
            customerFax.Text = "";
            customer_contactemail.Text = "";
        }
    }
}
