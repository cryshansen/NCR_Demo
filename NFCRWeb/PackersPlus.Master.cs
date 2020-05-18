using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace NCRFTRWeb
{
    public partial class PackersPlus : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
        {
            //if (e.Item.Text == "Print")
            //{
            //    try
            //    {
            //        hierarchicalGroupingReport.PrintToPrinter(1, false, 1, 99);
            //        Session["ctrl"] = MasterPanel;
            //        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('Print.aspx','PrintMe','height=300px,width=300px,scrollbars=1');</script>");

            //    }
            //    catch (Exception ex)
            //    {
            //        MessageLBL.Text = ex.Message;
            //    }

            //}
        }

    }
}
