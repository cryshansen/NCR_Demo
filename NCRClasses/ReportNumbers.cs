using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;
namespace NCRClass
{
    class ReportNumbers
    {
        /// <summary>
        /// This class handles Report No.s generated from ReportNumbers Table for a numbering sequence 
        /// that ensures all numbers are in use in a cronological order
        /// numbering is from 000000001 to 999999999 in the ReportNumber Table
        /// if cancel is clicked the 'used' field must be reset to 'N'
        /// </summary>

        #region PrivateMembers
        private int _ID;
        private string _ReportNo;
        private char _Used;
        #endregion



        #region Report numbering table
        /// <summary>
        /// This stored proc creates the report numbering system
        /// from 000000001 to 999999999 in the ReportNumber Table
        /// one time used to populate the table
        /// </summary>
        /// <param name="sub">sub is the value of reportNumber </param>
        /// <returns>returns id from table</returns>
        internal static int AddReportNumbers(string sub)
        {
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlCommand AddCustomer = new SqlCommand("AddRecordNo", IncidentsConnection);
            AddCustomer.CommandType = CommandType.StoredProcedure;
            AddCustomer.Parameters.Add("@sub", SqlDbType.VarChar).Value = sub;
            int reportId = 0;
            try
            {
                IncidentsConnection.Open();
                reportId = int.Parse(AddCustomer.ExecuteScalar().ToString());

            }
            finally
            {
                IncidentsConnection.Close();
            }
            return reportId;

        }
        //getreportIds is for the report table.selected where 'n' exists and therefor use when adding new detail report.
        /// <summary>
        /// Get report Ids for next incremented report Number on an add button click
        /// sql proc [GetReportIds]
        /// </summary>
        /// <returns>first record with used value = 'n'
        /// with ReportNumber string
        /// </returns>
        internal static DataSet GetNextReportId()
        {
            DataSet ReportNo = new DataSet();
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            string storedprocString = "GetReportIds";
            SqlDataAdapter GetAllReportNoAdapter = new SqlDataAdapter(storedprocString, IncidentsConnection);
            GetAllReportNoAdapter.Fill(ReportNo, "ReportNo");
            return ReportNo;
        }

        internal static void UpdateReportNumbers(int Id, string ReportNumber, char used)
        { 
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlCommand AddProblemOccurance = new SqlCommand("UpdateReportNumbers", IncidentsConnection);
            AddProblemOccurance.CommandType = CommandType.StoredProcedure;
            AddProblemOccurance.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
            AddProblemOccurance.Parameters.Add("@ReportId", SqlDbType.NVarChar).Value =ReportNumber;
            AddProblemOccurance.Parameters.Add("@used", SqlDbType.NChar).Value =used;
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

        internal static int ReportNumberUsed(string ReportNumber)
        {
            int IsValid = 0;

            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlCommand AddProblemOccurance = new SqlCommand("ReportExists", IncidentsConnection);
            AddProblemOccurance.CommandType = CommandType.StoredProcedure;
            AddProblemOccurance.Parameters.Add("@reportNumber", SqlDbType.NVarChar).Value = ReportNumber;
            try
            {
                IncidentsConnection.Open();
                IsValid = int.Parse(AddProblemOccurance.ExecuteScalar().ToString());
            }
            finally
            {
                IncidentsConnection.Close();
            }
            return IsValid;
        }

        internal static DataSet getReportInfo(string ReportNumber)
        {
            DataSet Report = new DataSet();
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlDataAdapter GetAllReportNoAdapter = new SqlDataAdapter("Exec GetReportInfo @ReportNumber", IncidentsConnection);
            GetAllReportNoAdapter.SelectCommand.Parameters.Add("@ReportNumber", SqlDbType.NVarChar).Value = ReportNumber;
            GetAllReportNoAdapter.Fill(Report, "Report");
            return Report;
        }

        #endregion
    }
}
