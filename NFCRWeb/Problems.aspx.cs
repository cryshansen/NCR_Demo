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
    public partial class WebForm6 : System.Web.UI.Page
    {
        DataSet Problems = null;
        DataSet Problem = null;
        public int problemId = 0;
        DataSet Categories = null;
        DataSet Category = null;
        public int parentId = 0;
        public int subcatid = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            //get 2 dropdowns ProbOccurDDL CategoryDDL
            if (!IsPostBack)
            {  //when not postback, means hitting page first time.
                GetProblemDropDownList();
                Session["probID"] = 0;
                GetProblemForListing(problemId);
                GetCategoryDropDownList(parentId);
                Session["parID"]=0;
                Session["subID"] = 0;
                
                //MessageLBL.Text = "Not postback.";
                //DisableTextboxes();
                AddNew.Enabled = true;
                //edit and print are available only when data in database. initial entry of first record only AddNew
                Edit.Enabled = true;
                Print.Enabled = true;
                AddNewCat.Enabled = true;
                EditCat.Enabled = true;
                PrintCat.Enabled = true;

            }
            
        }

        protected void DeleteBN_Click(object sender, EventArgs e)
        {
            int.TryParse(ProbOccurDDL.SelectedValue.ToString(), out problemId);
            bool ProbDelete = NCRClass.NCRController.DeleteProblemOccurance(problemId);
            if (ProbDelete == false)
            {

                MessageLBL.Text = "The Problem Occured Delete Failed. Please contact your administrator.";
            }
            else
            {
                MessageLBL.Text = "The Problem Occured  " + ProblemName.Text + " was deleted succesfully.";
                //once successfull should go back to main view mode again.
                GetProblemDropDownList();
                ProblemName.Text = "";
                Definition.Value = "";
                //all textbox Items are Disabled
                ProblemName.Enabled = false;
                Definition.Disabled = true;
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
            ClearProblems();
            EnableProblems();
            Session["probID"]=0;
            //change buttons
            AddNew.Enabled = false;
            Save.Enabled = true;
            Cancel.Enabled = true;
            Edit.Enabled = false;
            DeleteBN.Enabled = false;

        }

        protected void Edit_Click(object sender, EventArgs e)
        {
            int.TryParse(ProbOccurDDL.SelectedValue.ToString(), out problemId);
            Session["probID"] = problemId;
            EnableProblems();
            Save.Enabled = true;
            AddNew.Enabled = false;
            Edit.Enabled = false;
            DeleteBN.Enabled = true;
            Cancel.Enabled = true;
            Print.Enabled = true;
           
            
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            int.TryParse(ProbOccurDDL.SelectedValue.ToString(), out problemId);
            if (Session["probID"].ToString() == "0")
            { //adding a new problem
                //should we have a try catch here to adjust buttons/textboxes if fails?
                try
                {
                    problemId = NCRClass.NCRController.AddProblemOccurance(ProblemName.Text, Definition.Value);
                    if (problemId > 0)
                    {
                        MessageLBL.Text = "Your Problem Occurance " + ProblemName.Text + " has been added check the dropdown.";
                        //buttons should be same as on initial pageload on success
                        DisableProblems();
                        GetProblemDropDownList();
                        ProbOccurDDL.SelectedValue = problemId.ToString();
                        GetProblemForListing(problemId);
                        //once successfull buttons change
                        AddNew.Enabled = true;
                        //edit and print are available only when data in database. initial entry of first record only AddNew
                        Edit.Enabled = true;
                        Print.Enabled = true;
                        DeleteBN.Enabled = false;
                        Cancel.Enabled = false;
                        Save.Enabled = false;
                    }
                }
                catch (Exception ex) { MessageLBL.Text = ex.Message.ToString(); }
            }
            else
            {
                //updating 
                try
                {

                    NCRClass.NCRController.UpdateProblemOccurance(problemId, ProblemName.Text, Definition.Value);
                    MessageLBL.Text = " Your Problem Occurance " + ProblemName.Text + " has been updated.";
                    //buttons should be same as on initial pageload on success
                    //we updated it was successful but delete button is avail and save is avail 
                    //when we should be back to view well types instance
                    DisableProblems();
                    GetProblemDropDownList();
                    ProbOccurDDL.SelectedValue = problemId.ToString();
                    GetProblemForListing(problemId);
                    //once successfull buttons change
                    AddNew.Enabled = true;
                    //edit and print are available only when data in database. initial entry of first record only AddNew
                    Edit.Enabled = true;
                    Print.Enabled = true;
                    DeleteBN.Enabled = false;
                    Cancel.Enabled = false;
                    Save.Enabled = false;
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
            //all textbox Items are Emptied
            ClearProblems();
            //all textbox Items are Disabled
            DisableProblems();
            GetProblemDropDownList();
            GetProblemForListing(int.Parse(ProbOccurDDL.SelectedValue.ToString()));
            AddNew.Enabled = true;
            //edit and print are available only when data in database. initial entry of first record only AddNew
            Edit.Enabled = true;
            Print.Enabled = true;
            DeleteBN.Enabled = false;
            Cancel.Enabled = false;
            Save.Enabled = false;
        }

        protected void Print_Click(object sender, EventArgs e)
        {
            try
            {
                //hierarchicalGroupingReport.PrintToPrinter(1, false, 1, 99);
                Session["ctrl"] = ProblemPanel;
                ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('Print.aspx','PrintMe','height=300px,width=300px,scrollbars=1');</script>");

            }
            catch (Exception ex)
            {
                MessageLBL.Text = ex.Message.ToString();
            }

        }
        protected void ProbOccurDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            //get all info for specific problem
            int.TryParse(ProbOccurDDL.SelectedValue.ToString(), out problemId);
                try
                {
                    GetProblemForListing(problemId);
                    Edit.Enabled = true;
                    AddNew.Enabled = false;
                    Save.Enabled = false;
                    DeleteBN.Enabled = false;
                    DisableProblems();
                }
                catch (Exception ex)
                {
                    MessageLBL.Text = "There is no record for that Problem Occurance." + ex.Message.ToString();
                }
        }
 
        
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
                    MessageLBL.Text = parentId.ToString();
                    Category = NCRClass.NCRController.LookupCategoryName(parentId);
                    CatName.Text = Category.Tables["Category"].Rows[0]["CategoryName"].ToString();
                    Category  = NCRClass.NCRController.LookupAllCategoriesByParentId(parentId);
                    SubCategoryDDL.DataSource = Category.Tables["Categories"];
                    SubCategoryDDL.DataTextField = "CategoryName";
                    SubCategoryDDL.DataValueField = "OCID";
                    SubCategoryDDL.DataBind();
                    SubCategoryDDL.Items.Insert(0, " ");
                    //here we display the addsubcat button
                    AddNewSub.Visible = true;
                    AddNewSub.Enabled = true;
                    EditCat.Enabled = true;
                    AddNewCat.Enabled = true;
                    SaveCat.Enabled = false;
                    DeleteCat.Enabled = false;
                    //all textbox Items are Disabled
                    // DisableTextboxes();
                }
                catch (Exception ex)
                {
                     MessageLBL.Text = "There is no record for that Category." + ex.Message.ToString();
                }
            }
            else
            {
                SubCategoryDDL.SelectedIndex =0;
                MessageLBL.Text = "You must either select a Category or click the add Button to add a new Engineer.";
                AddNewCat.Enabled = true;
                EditCat.Enabled = false;
            }
             MessageLBL.Text = CategoryDDL.SelectedValue.ToString();

        }

        protected void SubCategoryDDL_SelectedIndexChanged(object sender, EventArgs e)
        {//this will populate the subcategory text field.
            //when i reselect from parent dropdown, the subdropdown should populate. but doesnt.

            int.TryParse(SubCategoryDDL.SelectedValue.ToString(), out parentId);
            if (parentId != 0)
            {

                SubCatName.Visible = true;
                //populate textfield with dropdown content
                Category = NCRClass.NCRController.LookupCategoryName(parentId);
                SubCatName.Text = Category.Tables["Category"].Rows[0]["CategoryName"].ToString();
                EditCat.Enabled = true;
                AddNewCat.Enabled = true;
                SaveCat.Enabled = false;
                DeleteCat.Enabled = false;
                //all textbox Items are Disabled
                // DisableTextboxes();

            }
            else
            {
                SubCategoryDDL.SelectedIndex = 0;
                MessageLBL.Text = "You must either select a Category or click the add Button to add a new Engineer.";
                AddNewCat.Enabled = true;
            }
            MessageLBL.Text = SubCategoryDDL.SelectedValue.ToString();

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
        protected void GetProblemForListing(int problemId)
        {
            Problem = NCRClass.NCRController.LookupProblem(problemId);
            ProblemName.Text = Problem.Tables["Problem"].Rows[0]["ProblemName"].ToString();
            Definition.Value = Problem.Tables["Problem"].Rows[0]["ProblemDef"].ToString();
        }
        protected void ClearProblems()
        {
            ProblemName.Text = "";
            Definition.Value = "";
        }
        protected void DisableProblems()
        {
            ProblemName.Enabled = false;
            Definition.Disabled = true;
        }
        protected void EnableProblems()
        {
            ProblemName.Enabled = true;
            Definition.Disabled = false;
        
        }


        protected void GetCategoryDropDownList(int parentId)
        {
            Categories = NCRClass.NCRController.LookupAllCategoriesByParentId(parentId);
            CategoryDDL.DataSource = Categories.Tables["Categories"];
            CategoryDDL.DataTextField = "CategoryName";
            CategoryDDL.DataValueField = "OCID";
            CategoryDDL.DataBind();
            if (parentId == 0) { 
                SubCategoryDDL.Items.Clear();
                SubCatName.Visible = false;
                AddNewSub.Visible = false;
            } 
            if (parentId != 0) 
            {
                AddNewSub.Visible = true;
            }
        }
        protected void GetSubCategoryDropDownList(int subcatid)
        {
            Categories = NCRClass.NCRController.LookupAllCategoriesByParentId(subcatid);
            SubCategoryDDL.DataSource = Categories.Tables["Categories"];
            SubCategoryDDL.DataTextField = "CategoryName";
            SubCategoryDDL.DataValueField = "OCID";
            SubCategoryDDL.DataBind();
        }

        protected void DeleteCat_Click(object sender, EventArgs e)
        {
            //if category has subcategories they must be deleted first. 
            int.TryParse(CategoryDDL.SelectedValue.ToString(), out parentId);
            int.TryParse(SubCategoryDDL.SelectedValue.ToString(), out subcatid);
            MessageLBL.Text = "The category id = "+ parentId + " The subcategory id = " + subcatid;
            if (subcatid == 0 && parentId > 0) 
            { 
            //deleting category
                //1. Check if subcats exist
                //returns a dataset
                Category = NCRClass.NCRController.LookupAllCategoriesByParentId(parentId);
                //int counter = Category.Tables["Category"].Rows.Count;
                //if subcats exists throw error
                if (Category ==null || Category.Tables.Count ==0 || Category.Tables[0].Rows.Count ==0)
                {//no data in table = no subcategories
                    //perform delete
                    bool deleteid = NCRClass.NCRController.DeleteCategory(parentId);
                    if (deleteid == false)
                    {
                        CatMessageLBL.Text = "The Category Delete Failed. Please contact your administrator.";
                    }
                    else
                    {
                        MessageLBL.Text = "The Category  " + CatName.Text + " was deleted succesfully.";
                        //once successfull should go back to main view mode again.
                        GetCategoryDropDownList(0);
                        CatName.Text = "";
                        //all textbox Items are Disabled
                        CatName.Enabled = false;
                        //initial entry of first record only AddNew
                        AddNewCat.Enabled = true;
                        //edit and print are available only when data in database. 
                        EditCat.Enabled = true;
                        PrintCat.Enabled = true;
                        DeleteCat.Enabled = true;
                        CancelCat.Enabled = false;
                        SaveCat.Enabled = false;
                    }
                    
                }
                else { 
                    MessageLBL.Text = "The category id = " + parentId + " has subcategories, you must delete them first.";

                    
                }
            }
            else if (subcatid > 0 && parentId > 0)
            {//deleting subcategory
                bool deleteid = NCRClass.NCRController.DeleteCategory(subcatid);
                if (deleteid == false)
                {
                    CatMessageLBL.Text = "The SubCategory Delete Failed. Please contact your administrator.";
                }
                else
                {
                    CatMessageLBL.Text = "The SubCategory  " + SubCatName.Text + " was deleted succesfully.";
                    //once successfull should go back to main view mode again.
                    GetCategoryDropDownList(parentId);
                    CatName.Text = "";
                    //all textbox Items are Disabled
                    CatName.Enabled = false;
                    //initial entry of first record only AddNew
                    AddNewCat.Enabled = true;
                    //edit and print are available only when data in database. 
                    EditCat.Enabled = true;
                    PrintCat.Enabled = true;
                    DeleteCat.Enabled = true;
                    CancelCat.Enabled = false;
                    SaveCat.Enabled = false;
                }

            }
            else { }

            
        }

        protected void AddNewCat_Click(object sender, EventArgs e)
        {//parentId should be 0 so reset to 0
            //this button will only drive the Parent Dropdown (CategoryDDL)
            //1. Set CategoryDropDown to 0
            parentId = 0;
            //2. Enable Category Textbox field
            CatName.Text = "";
            CatName.Enabled = true;
            //3. disable and hide subcategorydropdown if can.
            SubCategoryDDL.Items.Clear();
            SubCategoryDDL.Visible = false;
            SubCatName.Visible = false;
            AddNewSub.Visible = false;
            CategoryDDL.Items.Clear();
            CategoryDDL.Items.Insert(0," ");
            //4. enable/disable buttons 
            //if (parentId == 0)
            //{
            //    CatName.Enabled = true;
            //    CategoryDDL.SelectedIndex = 0;
                //change buttons
                AddNewCat.Enabled = false;
                DeleteCat.Enabled = true;
                SaveCat.Enabled = true;
                CancelCat.Enabled = true;
                EditCat.Enabled = false;
                //this will need to clear textboxes bc sometimes there will be some in there from dropdown select?
            //    SubCatName.Visible = true;
            //    SubCatName.Enabled = true;
            //    //SubCategoryText.Visible = true;
            //}
            //else
            //{//category dropdown has been selected therefore need to add to subcategory as well.
            //    //get the dropdown again 
            //    GetCategoryDropDownList(parentId);
            //    CategoryDDL.SelectedIndex = 0;
            //    SubCatName.Visible = true;
            //    //change buttons
            //    AddNewCat.Enabled = false;
            //    SaveCat.Enabled = true;
            //    CancelCat.Enabled = true;
            //    DeleteCat.Enabled = true;
            //    EditCat.Enabled = false;
            //    SubCatName.Enabled = true;
            //    //this will need to clear textboxes bc sometimes there will be some in there from dropdown select?
            //    //MessageLBL.Text = "You must select the empty value from the dropdown first.";
            //}
        }

        protected void EditCat_Click(object sender, EventArgs e)
        {
            int.TryParse(CategoryDDL.SelectedValue.ToString(), out parentId);
            int.TryParse(SubCategoryDDL.SelectedValue.ToString(), out subcatid);

            //Save cancel Print delete enabled
            //this is to enable all text fields so as to edit the content of the customer not actually update?
            CatName.Enabled = true;
            SubCatName.Enabled = true;
            AddNewCat.Enabled = false;
            SaveCat.Enabled = true;
            EditCat.Enabled = false;
            DeleteCat.Enabled = true;
            CancelCat.Enabled = true;
            PrintCat.Enabled = true;
            MessageLBL.Text = "Edit Cat";
        }

        protected void SaveCat_Click(object sender, EventArgs e)
        {//this no work
            //sometimes the parentId will be a value other than zero but the value represents a 
            //category for the subcategory add
            int.TryParse(CategoryDDL.SelectedValue.ToString(), out parentId);
            int.TryParse(SubCategoryDDL.SelectedValue.ToString(), out subcatid);
            if (parentId == 0)
            {// this means we're adding a category
                int categoryId = NCRClass.NCRController.AddCategory(parentId,CatName.Text);
                MessageLBL.Text = parentId + " Your Category " + CatName.Text + " has been Saved.";
                if (subcatid == 0 && SubCatName.Text != "")
                {//we are adding a subcategory as well....
                    subcatid = NCRClass.NCRController.AddCategory(categoryId, SubCatName.Text);
                    MessageLBL.Text = parentId + " Your SubCategory " + SubCatName.Text + " has been Saved.";

                }
                //buttons should be same as on initial pageload on success
                //we updated it was successful but delete button is avail and save is avail 
                //when we should be back to view well types instance
                CatName.Enabled = false;
                GetCategoryDropDownList(categoryId);
                CategoryDDL.SelectedIndex = categoryId;
                //once successfull buttons change
                AddNewCat.Enabled = true;
                //edit and print are available only when data in database. initial entry of first record only AddNew
                EditCat.Enabled = true;
                PrintCat.Enabled = true;
                DeleteCat.Enabled = false;
                CancelCat.Enabled = false;
                SaveCat.Enabled = false;
                if (subcatid != 0)
                {
                    GetSubCategoryDropDownList(subcatid);
                    SubCategoryDDL.SelectedValue = subcatid.ToString();
                }
            }
            else
            { //otherwise we're updating a category or adding subcategory
                if (subcatid == 0 && SubCatName.Text == "")
                { //and SubCatName.Text=="" we're updating a category
                    NCRClass.NCRController.UpdateCategory(parentId,0,CatName.Text);
                    MessageLBL.Text = "Your Category " + CatName.Text + " has been Updated and " + subcatid + " Your SubCategory " + SubCatName.Text + " has been Saved.";
                    //buttons should be same as on initial pageload on success
                    //we updated it was successful but delete button is avail and save is avail 
                    //when we should be back to view well types instance
                    SubCatName.Enabled = false;
                    GetCategoryDropDownList(parentId);
                    //CategoryDDL.SelectedValue = parentId.ToString();
                    //once successfull buttons change
                    AddNewCat.Enabled = true;
                    //edit and print are available only when data in database. initial entry of first record only AddNew
                    EditCat.Enabled = true;
                    PrintCat.Enabled = true;
                    DeleteCat.Enabled = false;
                    CancelCat.Enabled = false;
                    SaveCat.Enabled = false;
                }else if(subcatid > 0 && parentId >0)
                {//we're updating subcategory
                    NCRClass.NCRController.UpdateCategory(parentId, 0, CatName.Text);
                    NCRClass.NCRController.UpdateCategory(subcatid,parentId,SubCatName.Text);
                    MessageLBL.Text = "Your Category " + CatName.Text + " has been Updated and " + subcatid + " Your SubCategory " + SubCatName.Text + " has been Saved.";
                    //buttons should be same as on initial pageload on success
                    //we updated it was successful but delete button is avail and save is avail 
                    //when we should be back to view well types instance
                    SubCatName.Enabled = false;
                    GetCategoryDropDownList(parentId);
                    CategoryDDL.SelectedValue = parentId.ToString();
                    GetSubCategoryDropDownList(subcatid);
                    SubCategoryDDL.SelectedValue = subcatid.ToString();
                    //once successfull buttons change
                    AddNewCat.Enabled = true;
                    //edit and print are available only when data in database. initial entry of first record only AddNew
                    EditCat.Enabled = true;
                    PrintCat.Enabled = true;
                    DeleteCat.Enabled = false;
                    CancelCat.Enabled = false;
                    SaveCat.Enabled = false;
                }
                else { 
                    //we're adding a subcategory parentId!=0 and has been added prior to
                    subcatid=NCRClass.NCRController.AddCategory(parentId,SubCatName.Text);
                    MessageLBL.Text = "Your Category " + CatName.Text + " has been Updated and " + subcatid + " Your SubCategory " + SubCatName.Text + " has been Saved.";
                    //buttons should be same as on initial pageload on success
                    //we updated it was successful but delete button is avail and save is avail 
                    //when we should be back to view well types instance
                    SubCatName.Enabled = false;
                    GetCategoryDropDownList(parentId);
                    CategoryDDL.SelectedValue = parentId.ToString();
                    GetSubCategoryDropDownList(subcatid);
                    SubCategoryDDL.SelectedValue = subcatid.ToString();
                    //once successfull buttons change
                    AddNewCat.Enabled = true;
                    //edit and print are available only when data in database. initial entry of first record only AddNew
                    EditCat.Enabled = true;
                    PrintCat.Enabled = true;
                    DeleteCat.Enabled = false;
                    CancelCat.Enabled = false;
                    SaveCat.Enabled = false;
                }

            }
        }

        protected void CancelCat_Click(object sender, EventArgs e)
        {
            //all textbox Items are Emptied
            CatName.Text = "";
            SubCatName.Text = "";
            //all textbox Items are Disabled
            CatName.Enabled = false;
            SubCatName.Enabled= false;
            //Cancel.Attributes.Add("onClick","javascript:history.back();");
            GetCategoryDropDownList(0);
            AddNewCat.Enabled = true;
            //edit and print are available only when data in database. initial entry of first record only AddNew
            EditCat.Enabled = true;
            PrintCat.Enabled = true;
            DeleteCat.Enabled = false;
            CancelCat.Enabled = false;
            SaveCat.Enabled = false;
        }

        protected void PrintCat_Click(object sender, EventArgs e)
        {
            try
            {
                //hierarchicalGroupingReport.PrintToPrinter(1, false, 1, 99);
                Session["ctrl"] = CategoryPanel;
                ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('Print.aspx','PrintMe','height=300px,width=300px,scrollbars=1');</script>");

            }
            catch (Exception ex)
            {
                MessageLBL.Text = ex.Message;
            }
        }

        protected void AddNewSub_Click(object sender, EventArgs e)
        {
            //1. enable subcategory textbox
            //2. ensure subcategory dropdown is set to 0
            //3. Save button is enabled and visible.
            //4. dropdown is visible? - nah but set to 0
            SubCategoryDDL.Items.Clear();
            SubCategoryDDL.Items.Insert(0," ");
            SubCatName.Visible = true;
            SubCatName.Enabled = true;
            SaveSub.Visible = true;
            SaveSub.Enabled = true;
            AddNewSub.Visible = false;
        }

        protected void SaveSub_Click(object sender, EventArgs e)
        {
            //follow same precedure as saveCategory

            int.TryParse(CategoryDDL.SelectedValue.ToString(), out parentId);
            int.TryParse(SubCategoryDDL.SelectedValue.ToString(), out subcatid);
            if (parentId == 0)
            {// this means fail!! 
            }
            if (subcatid == 0 && SubCatName.Text != "")
            {//we are adding a subcategory 
                subcatid = NCRClass.NCRController.AddCategory( parentId , SubCatName.Text);
                MessageLBL.Text = parentId + " Your SubCategory " + SubCatName.Text + " has been Saved." + subcatid;

            }
            //buttons should be same as on initial pageload on success
            //we updated it was successful but delete button is avail and save is avail 
            //when we should be back to view well types instance
            CatName.Enabled = false;
            GetCategoryDropDownList(0);
            CategoryDDL.SelectedValue = parentId.ToString();
            GetSubCategoryDropDownList(parentId);
            SubCategoryDDL.SelectedValue = subcatid.ToString();
            //once successfull buttons change
            AddNewCat.Enabled = true;
            //edit and print are available only when data in database. initial entry of first record only AddNew
            EditCat.Enabled = true;
            PrintCat.Enabled = true;
            DeleteCat.Enabled = false;
            CancelCat.Enabled = false;
            SaveCat.Enabled = false;
        }

        
       
    }
}
