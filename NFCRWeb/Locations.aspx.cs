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
    public partial class WebForm1 : System.Web.UI.Page
    {
        DataSet Locations = null;
        DataSet Location = null;
        int locationId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {  //when not postback, means hitting page first time.
                GetDropDownList();
                Session["locID"] = 0;
                //all textbox Items are Disabled
                DisableTextboxes();
                AddNew.Enabled = true;
                //edit and print are available only when data in database. initial entry of first record only AddNew
                Edit.Enabled = true;
                Print.Enabled = true;
                int.TryParse(LocationDDL.SelectedValue.ToString(), out locationId);
                GetLocationForListing(locationId);
            }

        }

        protected void DeleteBN_Click(object sender, EventArgs e)
        {
            int.TryParse(LocationDDL.SelectedValue.ToString(), out locationId);
            bool locDelete = NCRClass.NCRController.DeleteLocation(locationId);
            if (locDelete == false)
            {
                MessageLBL.Text = "The Location Delete Failed. Please contact your administrator.";
            }
            else
            {
                MessageLBL.Text = "The Location " +  managerName.Text + " was deleted succesfully.";
                //once successfull should go back to main view mode again.
                GetDropDownList();
                GetLocationForListing(int.Parse(LocationDDL.SelectedValue.ToString()));
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
        {//when adding a new record, the save cancel buttons are enabled no others 
            //must clear textboxes
            ClearTextboxes();
            EnableTextboxes();
            Session["locID"]=0;
            //change buttons
            AddNew.Enabled = false;
            Save.Enabled = true;
            Cancel.Enabled = true;
            Edit.Enabled = false;
            //this will need to clear textboxes bc sometimes there will be some in there from dropdown select?

        }

        protected void Edit_Click(object sender, EventArgs e)
        {
            //Save cancel Print delete enabled
            //this is to enable all text fields so as to edit the content of the customer not actually update?
            int.TryParse(LocationDDL.SelectedValue.ToString(), out locationId);
            Session["locID"] = locationId;
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
            int.TryParse(LocationDDL.SelectedValue.ToString(), out locationId);
            if (Session["locID"].ToString() == "0")
            { //adding a new location
                //should we have a try catch here to adjust buttons/textboxes if fails
                try
                {
                    locationId = NCRClass.NCRController.AddLocation(district.Text, managerName.Text, address.Text, City.Text, Province.Text);
                    if (locationId > 0)
                    {
                        MessageLBL.Text = "Your Location " + district.Text + " has been added check the dropdown.";
                        //buttons should be same as on initial pageload on success
                        ClearTextboxes();
                        DisableTextboxes();
                        GetDropDownList();
                        LocationDDL.SelectedValue = locationId.ToString();
                        GetLocationForListing(locationId);
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
                //updating location
                try
                {

                    NCRClass.NCRController.UpdateLocation(locationId, district.Text, managerName.Text, address.Text, City.Text, Province.Text);
                    MessageLBL.Text = locationId + " Your Location " + managerName.Text + " has been updated.";
                    //buttons should be same as on initial pageload on success
                    //we updated it was successful but delete button is avail and save is avail 
                    //when we should be back to view customers instance
                    DisableTextboxes();
                    GetDropDownList();
                    LocationDDL.SelectedValue = locationId.ToString();
                    GetLocationForListing(locationId);
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
            DisableTextboxes();
            AddNew.Enabled = true;
            Edit.Enabled = true;
            Print.Enabled = true;
            DeleteBN.Enabled = false;
            Save.Enabled = false;
            Cancel.Enabled = false;
            int.TryParse(LocationDDL.SelectedValue.ToString(), out locationId);
            GetLocationForListing(locationId);
        }

        protected void Print_Click(object sender, EventArgs e)
        {
            try
            {
                //hierarchicalGroupingReport.PrintToPrinter(1, false, 1, 99);
                Session["ctrl"] = LocationPanel;
                ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('Print.aspx','PrintMe','height=300px,width=300px,scrollbars=1');</script>");

            }
            catch (Exception ex)
            {
                MessageLBL.Text = ex.Message.ToString();
            }

        }

        protected void LocationDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            int.TryParse(LocationDDL.SelectedValue.ToString(), out locationId);
            
                try
                {
                    int.TryParse(LocationDDL.SelectedValue.ToString(), out locationId);
                    GetLocationForListing(locationId);
                    Edit.Enabled = true;
                    AddNew.Enabled = false;
                    Save.Enabled = false;
                    DeleteBN.Enabled = false;
                    //all textbox Items are Disabled
                    DisableTextboxes();
                }
                catch (Exception ex)
                {

                    MessageLBL.Text = "There is no record for that Location." + ex.Message.ToString();
                }

        }
 
        protected void GetLocationForListing(int locationId)
        {
            Location = NCRClass.NCRController.LookupLocation(locationId);
            district.Text = Location.Tables["Location"].Rows[0]["District"].ToString();
            managerName.Text = Location.Tables["Location"].Rows[0]["ManagerName"].ToString();
            address.Text = Location.Tables["Location"].Rows[0]["Address"].ToString();
            City.Text = Location.Tables["Location"].Rows[0]["City"].ToString();
            Province.Text = Location.Tables["Location"].Rows[0]["ProvState"].ToString();
        }
        protected void GetDropDownList()
        {
            Locations = NCRClass.NCRController.LookupAllLocations();
            LocationDDL.DataSource = Locations.Tables["Locations"];
            LocationDDL.DataTextField = "District";
            LocationDDL.DataValueField = "LocationID";
            LocationDDL.DataBind();
        }
        protected void ClearTextboxes()
        {
            district.Text = "";
            managerName.Text = "";
            address.Text = "";
            City.Text = "";
            Province.Text = "";
        }

        protected void EnableTextboxes()
        {
            district.Enabled = true;
            managerName.Enabled = true;
            address.Enabled = true;
            City.Enabled = true;
            Province.Enabled = true;
        }
        protected void DisableTextboxes()
        {
            district.Enabled = false;
            managerName.Enabled = false;
            address.Enabled = false;
            City.Enabled = false;
            Province.Enabled = false;

        }
    }
}
