using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace NCRClass
{
    class Causes
    {
        internal static DataSet LookupCauses(int Id)
        {//Id= report id
            DataSet Causes = null;
            using (SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString))
            {
                SqlDataAdapter LookupCausesAdapter =
                    new SqlDataAdapter("Exec GetAllCausesById @Id", IncidentsConnection);
                LookupCausesAdapter.SelectCommand.Parameters.Add
                    ("@Id", SqlDbType.Int).Value = Id;
                Causes = new DataSet();
                LookupCausesAdapter.Fill(Causes, "Causes");
            }
            return Causes;
        }
        internal static DataSet LookupCauseByCauseId(int Id)
        {//Id= causeid
            DataSet Cause = null;
            using (SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString))
            {
                SqlDataAdapter LookupEventsAdapter =
                    new SqlDataAdapter("Exec GetCauseById @Id", IncidentsConnection);
                LookupEventsAdapter.SelectCommand.Parameters.Add
                    ("@Id", SqlDbType.Int).Value = Id;
                Cause = new DataSet();
                LookupEventsAdapter.Fill(Cause, "Cause");
            }
            return Cause;
        }
        internal static DataSet LookupCauseDDLById(int Id)
        {//Id= causeid
            DataSet Cause = null;
            using (SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString))
            {
                SqlDataAdapter LookupEventsAdapter =
                    new SqlDataAdapter("Exec GetCausesDDLById @Id", IncidentsConnection);
                LookupEventsAdapter.SelectCommand.Parameters.Add
                    ("@Id", SqlDbType.Int).Value = Id;
                Cause = new DataSet();
                LookupEventsAdapter.Fill(Cause, "Cause");
            }
            return Cause;
        }


        internal static int AddCause(int rid, string title, string content)
        {
            int causeId;
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlCommand AddCause = new SqlCommand("AddCause", IncidentsConnection);
            AddCause.CommandType = CommandType.StoredProcedure;
            AddCause.Parameters.Add("@reportId", SqlDbType.VarChar).Value = rid;
            AddCause.Parameters.Add("@title", SqlDbType.VarChar).Value = title;
            AddCause.Parameters.Add("@Text", SqlDbType.VarChar).Value = content;
            try
            {
                IncidentsConnection.Open();
                causeId = int.Parse(AddCause.ExecuteScalar().ToString());
            }
            finally
            {
                IncidentsConnection.Close();
            }
            return causeId;
        }

        internal static void UpdateCause(int id, int rid, string title, string content)
        {
            
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlCommand AddCause = new SqlCommand("UpdateCause", IncidentsConnection);
            AddCause.CommandType = CommandType.StoredProcedure;
            AddCause.Parameters.Add("@causeId", SqlDbType.VarChar).Value = id;
            AddCause.Parameters.Add("@reportId", SqlDbType.VarChar).Value = rid;
            AddCause.Parameters.Add("@causeTitle", SqlDbType.VarChar).Value = title;
            AddCause.Parameters.Add("@cause_text", SqlDbType.VarChar).Value = content;
            try
            {
                IncidentsConnection.Open();
                AddCause.ExecuteNonQuery();
            }
            finally
            {
                IncidentsConnection.Close();
            }
            
        }
        ////Delete
        internal static void DeleteCause(int causeId)
        {
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlCommand AddCause = new SqlCommand("DeleteCause", IncidentsConnection);
            AddCause.CommandType = CommandType.StoredProcedure;
            AddCause.Parameters.Add("@Id", SqlDbType.Int).Value = causeId;
            try
            {
                IncidentsConnection.Open();
                AddCause.ExecuteNonQuery();
            }
            finally
            {
                IncidentsConnection.Close();
            }
        }

    }
}
