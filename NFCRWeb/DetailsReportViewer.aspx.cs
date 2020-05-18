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
using C1.Web.UI.Controls.C1Report;

namespace NCRFTRWeb
{
    public partial class DetailsReportViewer : System.Web.UI.Page
    {
        string xmlstring;
        protected void Page_Load(object sender, EventArgs e)
        {
            xmlstring = Request.QueryString["xmlString"];
            xmlstring = string.Concat(xmlstring, ".xml");
            if (xmlstring.IndexOfAny(System.IO.Path.GetInvalidFileNameChars()) < 0)
            {
                Label1.Text = xmlstring;

            }
            else {
                //this.C1ReportViewer1.Load = xmlstring;
            }
        }
    }
}
