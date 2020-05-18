using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;

namespace NCRClass
{
    class Incidents
    {
        #region Private/Protected Members

        private int ReportID;
       /// <summary>
       /// Report No is generated from a separate table for a numbering sequence 
       /// that ensures all numbers are in use in a cronological order
       /// </summary>
        private string ReportNo;
        private string ReportType;
        private string Title;
	    private string FieldTicket;
        private int WellID;
	    private string WellName;
	    private string WellLocation ;
	    private string WellCasing ;
	    private string WellTubing;
	    private string WellDepth;
	    private string WellType ;
	    private string WellTD ;
	    private string WellTVD;
	    private string ExecSummary;
	    private string Description;
	    private string Observation;
	    private string CustomerID;
	    private string CompanyName;
	    private string ContactName ;
	    private string ContactPhone ;
	    private string ContactEmail;
	    private string ContactExt;
	    private string ContactFax;
	    private int OperatorID;
	    private string OperatorName;
	    private string OperatorPhone;
	    private string OperatorEmail;
	    private string OperatorExt;
	    private string OperatorFax;
	    private int InitiatorID;
	    private string InitiatorName ;
	    private string InitiatorPhone ;
	    private string InitiatorEmail;
	    private string InitiatorExt;
	    private string InitiatorFax;
	    private int LocationID;
	    private string District;
	    private string ManagerName; 
	    private string Address; 
	    private string City ;
	    private string ProvState;
#endregion
 

        #region Detail Reports
        /// <summary>
        /// Using  a paging system for this function
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="numTotalDetails"></param>
        /// <returns></returns>
        internal static DataSet GetAllDetailReports(int pageIndex, int PageSize, int numTotalDetails)
        {
            DataSet Details = new DataSet();

            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlDataAdapter GetAllCustomersAdapter = new SqlDataAdapter("Exec GetAllDetailRecords @pageIndex,@NumRows,@DetailsCount", IncidentsConnection);
            GetAllCustomersAdapter.SelectCommand.Parameters.Add("@pageIndex", SqlDbType.Int).Value = pageIndex;
            GetAllCustomersAdapter.SelectCommand.Parameters.Add("@NumRows", SqlDbType.Int).Value = PageSize;
            GetAllCustomersAdapter.SelectCommand.Parameters.Add("@DetailsCount",SqlDbType.Int).Value = numTotalDetails;
            try
            {
                GetAllCustomersAdapter.Fill(Details, "Details");
            }
            finally
            { }
                return Details;


        }
        /// <summary>
        /// Get Detail Reports for dropdown info
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="numTotalDetails"></param>
        /// <returns></returns>
        internal static DataSet GetAllDetailReports()
        {
            DataSet Details = new DataSet();

            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlDataAdapter GetAllCustomersAdapter = new SqlDataAdapter("Exec GetAllDetailRecordsDD", IncidentsConnection);
            try
            {
                GetAllCustomersAdapter.Fill(Details, "Details");
            }
            finally
            { }
            return Details;


        }
        internal static DataSet GetDetailsById(int reportId)
        {
            DataSet Detail = new DataSet();
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlDataAdapter GetAllCustomersAdapter = new SqlDataAdapter("Exec GetIncidentById @reportId", IncidentsConnection);
            GetAllCustomersAdapter.SelectCommand.Parameters.Add
                    ("@reportId", SqlDbType.NVarChar).Value = reportId;
            GetAllCustomersAdapter.Fill(Detail, "Detail");
            return Detail;

        }

        //do I need this?
        internal static DataSet GetFirstReport()
        {

            DataSet Reports = new DataSet();
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlDataAdapter GetAllCustomersAdapter = new SqlDataAdapter("Exec GetFirstReport", IncidentsConnection);
            
            GetAllCustomersAdapter.Fill(Reports, "Reports");
            return Reports;

        }
        internal static DataSet SearchFields(string query,string EventsTB,string CausesTB,string RemediationTB,string AttachmentTB)
        {

            DataSet Result = new DataSet();
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlDataAdapter GetAllCustomersAdapter = new SqlDataAdapter("Exec SearchIncidentsQueryString", IncidentsConnection);
            GetAllCustomersAdapter.SelectCommand.Parameters.Add("@query", SqlDbType.NVarChar).Value = query;
            GetAllCustomersAdapter.SelectCommand.Parameters.Add("@attachment", SqlDbType.NVarChar).Value = AttachmentTB;
            GetAllCustomersAdapter.SelectCommand.Parameters.Add("@events", SqlDbType.NVarChar).Value = EventsTB;
            GetAllCustomersAdapter.SelectCommand.Parameters.Add("@causes", SqlDbType.NVarChar).Value = CausesTB;
            GetAllCustomersAdapter.SelectCommand.Parameters.Add("@correctiveAction", SqlDbType.NVarChar).Value = RemediationTB;
           
            
            try
            {

                GetAllCustomersAdapter.Fill(Result, "Result");
            }
            finally
            { 
            
            }
            return Result;

        }
        #region Add
        /// <summary>
        /// This function Adds a Detail Record to the Database
        /// </summary>
        /// <param name="DRreportNumber"></param>
        /// <param name="DRReportType"></param>
        /// <param name="customerId"></param>
        /// <param name="rcompany"></param>
        /// <param name="rcustomercontact"></param>
        /// <param name="rcustomerphone"></param>
        /// <param name="rContactFax"></param>
        /// <param name="rCustomerEmail"></param>
        /// <param name="operatorId"></param>
        /// <param name="rOperatorName"></param>
        /// <param name="rOperatorPhone"></param>
        /// <param name="rOperatorFax"></param>
        /// <param name="rOperatorEmail"></param>
        /// <param name="rOperatorExt"></param>
        /// <param name="engineerId"></param>
        /// <param name="rInitiatorName"></param>
        /// <param name="rInitiatorPhone"></param>
        /// <param name="rInitiatorExt"></param>
        /// <param name="rInitiatorFax"></param>
        /// <param name="rInitiatorEmail"></param>
        /// <param name="locationId"></param>
        /// <param name="rDistrict"></param>
        /// <param name="rManagerName"></param>
        /// <param name="rmanagerAddress"></param>
        /// <param name="rLocCityAddress"></param>
        /// <param name="rProvState"></param>
        /// <param name="ReportDate"></param>
        /// <param name="DRtitle"></param>
        /// <param name="DRfieldticket"></param>
        /// <param name="DRwellname"></param>
        /// <param name="WellTypeId"></param>
        /// <param name="DRLSDTB"></param>
        /// <param name="DRSectionTB"></param>
        /// <param name="DRTownshipTB"></param>
        /// <param name="DRRangeTB"></param>
        /// <param name="DRMeridianTB"></param>
        /// <param name="CasingSizeId"></param>
        /// <param name="LinerSizeId"></param>
        /// <param name="DRTMDTB"></param>
        /// <param name="DRTVDTB"></param>
        /// <param name="SystemTypeId"></param>
        /// <param name="ToolListId"></param>
        /// <param name="problemId"></param>
        /// <param name="parentId"></param>
        /// <param name="subcatid"></param>
        /// <param name="Notes"></param>
        /// <param name="DRExecSummary"></param>
        /// <param name="DRDescription"></param>
        /// <param name="DRObservations"></param>
        /// <param name="DRnotes2"></param>
        /// <param name="DRPrefaceTA"></param>
        /// <param name="DRCauseAnalysisTA"></param>
        /// <param name="unitmeasure"></param>
        /// <returns>Returns the newly added Detail Id</returns>

        internal static int AddDetailRecord(string DRreportNumber, string DRReportType, int customerId, string rcompany, string rcustomercontact, string rcustomerphone, string rContactFax, string rCustomerEmail, int operatorId, string rOperatorName, string rOperatorPhone, string rOperatorFax, string rOperatorEmail,string rOperatorExt,
                    int engineerId, string rInitiatorName, string rInitiatorPhone, string rInitiatorExt, string rInitiatorFax, string rInitiatorEmail,
                    int locationId, string rDistrict, string rManagerName, string rmanagerAddress, string rLocCityAddress, string rProvState,
                    DateTime ReportDate, string DRtitle, string DRfieldticket, string DRwellname, int WellTypeId, string DRLSDTB, string DRSectionTB,
                    string DRTownshipTB, string DRRangeTB, string DRMeridianTB, int CasingSizeId, int LinerSizeId,
                    string DRTMDTB, string DRTVDTB,string unitmeasure, int SystemTypeId, int ToolListId, int problemId, int parentId, int subcatid, string Notes,
                    string DRExecSummary, string DRDescription, string DRObservations, string DRnotes2, string DRPrefaceTA, string DRCauseAnalysisTA)
        {

            //missing unitsmeasure - feet/meters
            int reportId = 0;
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlCommand AddProblemOccurance = new SqlCommand("AddIncident", IncidentsConnection);
            AddProblemOccurance.CommandType = CommandType.StoredProcedure;
            AddProblemOccurance.Parameters.Add("@DRreportNumber", SqlDbType.NVarChar).Value = DRreportNumber;
            AddProblemOccurance.Parameters.Add("@DRReportType", SqlDbType.NVarChar).Value =DRReportType;
            AddProblemOccurance.Parameters.Add("@customerId", SqlDbType.Int).Value =customerId;
            AddProblemOccurance.Parameters.Add("@rcompany", SqlDbType.NVarChar).Value =rcompany;
            AddProblemOccurance.Parameters.Add("@rcustomercontact", SqlDbType.NVarChar).Value =rcustomercontact;
            AddProblemOccurance.Parameters.Add("@rcustomerphone", SqlDbType.NVarChar).Value =rcustomerphone;
            AddProblemOccurance.Parameters.Add("@rContactFax", SqlDbType.NVarChar).Value =rContactFax;
            AddProblemOccurance.Parameters.Add("@rCustomerEmail", SqlDbType.NVarChar).Value =rCustomerEmail;
            AddProblemOccurance.Parameters.Add("@operatorId", SqlDbType.Int).Value =operatorId;
            AddProblemOccurance.Parameters.Add("@rOperatorName", SqlDbType.NVarChar).Value = rOperatorName;
            AddProblemOccurance.Parameters.Add("@rOperatorPhone", SqlDbType.NVarChar).Value =rOperatorPhone;
            AddProblemOccurance.Parameters.Add("@rOperatorFax", SqlDbType.NVarChar).Value =rOperatorFax;
            AddProblemOccurance.Parameters.Add("@rOperatorEmail", SqlDbType.NVarChar).Value =rOperatorEmail;
            AddProblemOccurance.Parameters.Add("@rOperatorExt", SqlDbType.NVarChar).Value =rOperatorExt;
            AddProblemOccurance.Parameters.Add("@engineerId", SqlDbType.Int).Value =engineerId;
            AddProblemOccurance.Parameters.Add("@rInitiatorName", SqlDbType.NVarChar).Value =rInitiatorName;
            AddProblemOccurance.Parameters.Add("@rInitiatorPhone", SqlDbType.NVarChar).Value =rInitiatorPhone;
            AddProblemOccurance.Parameters.Add("@rInitiatorExt", SqlDbType.NVarChar).Value =rInitiatorExt;
            AddProblemOccurance.Parameters.Add("@rInitiatorFax", SqlDbType.NVarChar).Value =rInitiatorFax;
            AddProblemOccurance.Parameters.Add("@rInitiatorEmail", SqlDbType.NVarChar).Value =rInitiatorEmail;
            AddProblemOccurance.Parameters.Add("@locationId", SqlDbType.Int).Value =locationId;
            AddProblemOccurance.Parameters.Add("@rDistrict", SqlDbType.NVarChar).Value =rDistrict;
            AddProblemOccurance.Parameters.Add("@rManagerName", SqlDbType.NVarChar).Value =rManagerName;
            AddProblemOccurance.Parameters.Add("@rmanagerAddress", SqlDbType.NVarChar).Value = rmanagerAddress;
            AddProblemOccurance.Parameters.Add("@rLocCityAddress", SqlDbType.NVarChar).Value = rLocCityAddress;
            AddProblemOccurance.Parameters.Add("@rProvState", SqlDbType.NVarChar).Value =rProvState;
            AddProblemOccurance.Parameters.Add("@DRDateTB", SqlDbType.DateTime).Value = ReportDate;
            AddProblemOccurance.Parameters.Add("@DRtitle", SqlDbType.NVarChar).Value = DRtitle;
            AddProblemOccurance.Parameters.Add("@DRfieldticket", SqlDbType.NVarChar).Value =DRfieldticket;
            AddProblemOccurance.Parameters.Add("@DRwellname", SqlDbType.NVarChar).Value = DRwellname;
            AddProblemOccurance.Parameters.Add("@WellTypeId", SqlDbType.Int).Value =WellTypeId;
            AddProblemOccurance.Parameters.Add("@DRLSDTB", SqlDbType.NVarChar).Value =DRLSDTB;
            AddProblemOccurance.Parameters.Add("@DRSectionTB", SqlDbType.NVarChar).Value =DRSectionTB;               
            AddProblemOccurance.Parameters.Add("@DRTownshipTB", SqlDbType.NVarChar).Value = DRTownshipTB;
            AddProblemOccurance.Parameters.Add("@DRRangeTB", SqlDbType.NVarChar).Value = DRRangeTB;
            AddProblemOccurance.Parameters.Add("@DRMeridianTB", SqlDbType.NVarChar).Value = DRMeridianTB;
            AddProblemOccurance.Parameters.Add("@CasingSizeId", SqlDbType.Int).Value =CasingSizeId;
            AddProblemOccurance.Parameters.Add("@LinerSizeId", SqlDbType.Int).Value = LinerSizeId ;             
            AddProblemOccurance.Parameters.Add("@DRTMDTB", SqlDbType.NVarChar).Value =DRTMDTB;
            AddProblemOccurance.Parameters.Add("@DRTVDTB", SqlDbType.NVarChar).Value =DRTVDTB;
            AddProblemOccurance.Parameters.Add("@SystemTypeId", SqlDbType.Int).Value =SystemTypeId;
            AddProblemOccurance.Parameters.Add("@ToolListId", SqlDbType.Int).Value =ToolListId;
            AddProblemOccurance.Parameters.Add("@problemId", SqlDbType.Int).Value =problemId;
            AddProblemOccurance.Parameters.Add("@parentId", SqlDbType.Int).Value = parentId;
            AddProblemOccurance.Parameters.Add("@subcatid", SqlDbType.Int).Value =subcatid;
            AddProblemOccurance.Parameters.Add("@Notes", SqlDbType.Text).Value =Notes;           
            AddProblemOccurance.Parameters.Add("@DRExecSummary", SqlDbType.Text).Value =DRExecSummary;
            AddProblemOccurance.Parameters.Add("@DRDescription", SqlDbType.Text).Value = DRDescription;
            AddProblemOccurance.Parameters.Add("@DRObservations", SqlDbType.Text).Value = DRObservations;
            AddProblemOccurance.Parameters.Add("@DRnotes2", SqlDbType.Text).Value = DRnotes2;
            AddProblemOccurance.Parameters.Add("@DRPrefaceTA", SqlDbType.Text).Value =DRPrefaceTA;
            AddProblemOccurance.Parameters.Add("@DRCauseAnalysisTA", SqlDbType.Text).Value =DRCauseAnalysisTA;
            AddProblemOccurance.Parameters.Add("@unitmeasure", SqlDbType.NVarChar).Value = unitmeasure;
            try
            {
                IncidentsConnection.Open();
                reportId =int.Parse(AddProblemOccurance.ExecuteScalar().ToString());
            }
            finally
            {
                IncidentsConnection.Close();
            }
            return reportId;


        }
        internal static void UpdateDetailRecord(int ReportId,string DRreportNumber, string DRReportType, int customerId, string rcompany, string rcustomercontact, string rcustomerphone, string rContactFax, string rCustomerEmail, int operatorId, string rOperatorName, string rOperatorPhone, string rOperatorFax, string rOperatorEmail, string rOperatorExt,
            int engineerId, string rInitiatorName, string rInitiatorPhone, string rInitiatorExt, string rInitiatorFax, string rInitiatorEmail,
            int locationId, string rDistrict, string rManagerName, string rmanagerAddress, string rLocCityAddress, string rProvState,
            DateTime ReportDate, string DRtitle, string DRfieldticket, string DRwellname, int WellTypeId, string DRLSDTB, string DRSectionTB,
            string DRTownshipTB, string DRRangeTB, string DRMeridianTB, int CasingSizeId, int LinerSizeId,
            string DRTMDTB, string DRTVDTB,string unitmeasure, int SystemTypeId, int ToolListId, int problemId, int parentId, int subcatid, string Notes,
            string DRExecSummary, string DRDescription, string DRObservations, string DRnotes2, string DRPrefaceTA, string DRCauseAnalysisTA)
        {

            //missing unitsmeasure - feet/meters
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlCommand AddProblemOccurance = new SqlCommand("UpdateIncident", IncidentsConnection);
            AddProblemOccurance.CommandType = CommandType.StoredProcedure;
            AddProblemOccurance.Parameters.Add("@DRreportNumber", SqlDbType.NVarChar).Value = DRreportNumber;
            AddProblemOccurance.Parameters.Add("@DRReportType", SqlDbType.NVarChar).Value = DRReportType;
            AddProblemOccurance.Parameters.Add("@customerId", SqlDbType.Int).Value = customerId;
            AddProblemOccurance.Parameters.Add("@rcompany", SqlDbType.NVarChar).Value = rcompany;
            AddProblemOccurance.Parameters.Add("@rcustomercontact", SqlDbType.NVarChar).Value = rcustomercontact;
            AddProblemOccurance.Parameters.Add("@rcustomerphone", SqlDbType.NVarChar).Value = rcustomerphone;
            AddProblemOccurance.Parameters.Add("@rContactFax", SqlDbType.NVarChar).Value = rContactFax;
            AddProblemOccurance.Parameters.Add("@rCustomerEmail", SqlDbType.NVarChar).Value = rCustomerEmail;
            AddProblemOccurance.Parameters.Add("@operatorId", SqlDbType.Int).Value = operatorId;
            AddProblemOccurance.Parameters.Add("@rOperatorName", SqlDbType.NVarChar).Value = rOperatorName;
            AddProblemOccurance.Parameters.Add("@rOperatorPhone", SqlDbType.NVarChar).Value = rOperatorPhone;
            AddProblemOccurance.Parameters.Add("@rOperatorFax", SqlDbType.NVarChar).Value = rOperatorFax;
            AddProblemOccurance.Parameters.Add("@rOperatorEmail", SqlDbType.NVarChar).Value = rOperatorEmail;
            AddProblemOccurance.Parameters.Add("@rOperatorExt", SqlDbType.NVarChar).Value = rOperatorExt;
            AddProblemOccurance.Parameters.Add("@engineerId", SqlDbType.Int).Value = engineerId;
            AddProblemOccurance.Parameters.Add("@rInitiatorName", SqlDbType.NVarChar).Value = rInitiatorName;
            AddProblemOccurance.Parameters.Add("@rInitiatorPhone", SqlDbType.NVarChar).Value = rInitiatorPhone;
            AddProblemOccurance.Parameters.Add("@rInitiatorExt", SqlDbType.NVarChar).Value = rInitiatorExt;
            AddProblemOccurance.Parameters.Add("@rInitiatorFax", SqlDbType.NVarChar).Value = rInitiatorFax;
            AddProblemOccurance.Parameters.Add("@rInitiatorEmail", SqlDbType.NVarChar).Value = rInitiatorEmail;
            AddProblemOccurance.Parameters.Add("@locationId", SqlDbType.Int).Value = locationId;
            AddProblemOccurance.Parameters.Add("@rDistrict", SqlDbType.NVarChar).Value = rDistrict;
            AddProblemOccurance.Parameters.Add("@rManagerName", SqlDbType.NVarChar).Value = rManagerName;
            AddProblemOccurance.Parameters.Add("@rmanagerAddress", SqlDbType.NVarChar).Value = rmanagerAddress;
            AddProblemOccurance.Parameters.Add("@rLocCityAddress", SqlDbType.NVarChar).Value = rLocCityAddress;
            AddProblemOccurance.Parameters.Add("@rProvState", SqlDbType.NVarChar).Value = rProvState;
            AddProblemOccurance.Parameters.Add("@DRDateTB", SqlDbType.DateTime).Value = ReportDate;
            AddProblemOccurance.Parameters.Add("@DRtitle", SqlDbType.NVarChar).Value = DRtitle;
            AddProblemOccurance.Parameters.Add("@DRfieldticket", SqlDbType.NVarChar).Value = DRfieldticket;
            AddProblemOccurance.Parameters.Add("@DRwellname", SqlDbType.NVarChar).Value = DRwellname;
            AddProblemOccurance.Parameters.Add("@WellTypeId", SqlDbType.Int).Value = WellTypeId;
            AddProblemOccurance.Parameters.Add("@DRLSDTB", SqlDbType.NVarChar).Value = DRLSDTB;
            AddProblemOccurance.Parameters.Add("@DRSectionTB", SqlDbType.NVarChar).Value = DRSectionTB;
            AddProblemOccurance.Parameters.Add("@DRTownshipTB", SqlDbType.NVarChar).Value = DRTownshipTB;
            AddProblemOccurance.Parameters.Add("@DRRangeTB", SqlDbType.NVarChar).Value = DRRangeTB;
            AddProblemOccurance.Parameters.Add("@DRMeridianTB", SqlDbType.NVarChar).Value = DRMeridianTB;
            AddProblemOccurance.Parameters.Add("@CasingSizeId", SqlDbType.Int).Value = CasingSizeId;
            AddProblemOccurance.Parameters.Add("@LinerSizeId", SqlDbType.Int).Value = LinerSizeId;
            AddProblemOccurance.Parameters.Add("@DRTMDTB", SqlDbType.NVarChar).Value = DRTMDTB;
            AddProblemOccurance.Parameters.Add("@DRTVDTB", SqlDbType.NVarChar).Value = DRTVDTB;
            AddProblemOccurance.Parameters.Add("@SystemTypeId", SqlDbType.Int).Value = SystemTypeId;
            AddProblemOccurance.Parameters.Add("@ToolListId", SqlDbType.Int).Value = ToolListId;
            AddProblemOccurance.Parameters.Add("@problemId", SqlDbType.Int).Value = problemId;
            AddProblemOccurance.Parameters.Add("@parentId", SqlDbType.Int).Value = parentId;
            AddProblemOccurance.Parameters.Add("@subcatid", SqlDbType.Int).Value = subcatid;
            AddProblemOccurance.Parameters.Add("@Notes", SqlDbType.Text).Value = Notes;
            AddProblemOccurance.Parameters.Add("@DRExecSummary", SqlDbType.Text).Value = DRExecSummary;
            AddProblemOccurance.Parameters.Add("@DRDescription", SqlDbType.Text).Value = DRDescription;
            AddProblemOccurance.Parameters.Add("@DRObservations", SqlDbType.Text).Value = DRObservations;
            AddProblemOccurance.Parameters.Add("@DRnotes2", SqlDbType.Text).Value = DRnotes2;
            AddProblemOccurance.Parameters.Add("@DRPrefaceTA", SqlDbType.Text).Value = DRPrefaceTA;
            AddProblemOccurance.Parameters.Add("@DRCauseAnalysisTA", SqlDbType.Text).Value = DRCauseAnalysisTA;
            AddProblemOccurance.Parameters.Add("@unitmeasure", SqlDbType.NVarChar).Value = unitmeasure;
            AddProblemOccurance.Parameters.Add("@reportId", SqlDbType.Int).Value = ReportId;
            try
            {
                IncidentsConnection.Open();
                AddProblemOccurance.ExecuteNonQuery();
            }
            finally
            {
                IncidentsConnection.Close();
            }
        }

        internal static bool DeleteDetailReport(int reportId)
        {
            bool detailReportIdDel = false;
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlCommand AddCustomer = new SqlCommand("DeleteDetailReport", IncidentsConnection);
            AddCustomer.CommandType = CommandType.StoredProcedure;
            AddCustomer.Parameters.Add("@reportId", SqlDbType.Int).Value = reportId;
            try
            {
                IncidentsConnection.Open();
                detailReportIdDel = int.Parse(AddCustomer.ExecuteScalar().ToString()) == 0;
            }
            finally
            {
                IncidentsConnection.Close();
            }
            return detailReportIdDel;

        }

        #endregion 



        #endregion
    }
}
