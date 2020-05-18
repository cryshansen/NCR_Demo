using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace NCRClass
{
    class Corrections
    {
        internal static DataSet LookupCorrections(int Id)
        {//Id= report id
            DataSet Corrections = null;
            using (SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString))
            {
                SqlDataAdapter LookupCorrectionsAdapter =
                    new SqlDataAdapter("Exec GetAllCorrectiveActionsById @Id", IncidentsConnection);
                LookupCorrectionsAdapter.SelectCommand.Parameters.Add
                    ("@Id", SqlDbType.Int).Value = Id;
                Corrections = new DataSet();
                LookupCorrectionsAdapter.Fill(Corrections, "Corrections");
            }
            return Corrections;
        }
        internal static DataSet LookupCorrectionByActionId(int Id)
        {//Id= actionid
            DataSet Correction = null;
            using (SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString))
            {
                SqlDataAdapter LookupCorrectionsAdapter =
                    new SqlDataAdapter("Exec GetCorrectiveActionById @Id", IncidentsConnection);
                LookupCorrectionsAdapter.SelectCommand.Parameters.Add
                    ("@Id", SqlDbType.Int).Value = Id;
                Correction = new DataSet();
                LookupCorrectionsAdapter.Fill(Correction, "Correction");
            }
            return Correction;
        }
        internal static DataSet GetCorrectiveActionsDDLById(int Id)
        {
            DataSet Correction = null;
            using (SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString))
            {
                SqlDataAdapter LookupCorrectionsAdapter =
                    new SqlDataAdapter("Exec GetCorrectiveActionsDDLById @Id", IncidentsConnection);
                LookupCorrectionsAdapter.SelectCommand.Parameters.Add
                    ("@Id", SqlDbType.Int).Value = Id;
                Correction = new DataSet();
                LookupCorrectionsAdapter.Fill(Correction, "Correction");
            }
            return Correction;
        }
        internal static int AddCorrection(int rid, string title, string content)
        {
            int correctionId;
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlCommand AddCorrection = new SqlCommand("AddCorrectiveAction", IncidentsConnection);
            AddCorrection.CommandType = CommandType.StoredProcedure;
            AddCorrection.Parameters.Add("@reportId", SqlDbType.VarChar).Value = rid;
            AddCorrection.Parameters.Add("@title", SqlDbType.VarChar).Value = title;
            AddCorrection.Parameters.Add("@Text", SqlDbType.VarChar).Value = content;
            try
            {
                IncidentsConnection.Open();
                correctionId = int.Parse(AddCorrection.ExecuteScalar().ToString());
            }
            finally
            {
                IncidentsConnection.Close();
            }
                return correctionId;
        }

        internal static void UpdateCorrection(int id, int rid, string title, string content)
        {
            int correctionId;
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlCommand AddCorrection = new SqlCommand("UpdateCorrectiveAction", IncidentsConnection);
            AddCorrection.CommandType = CommandType.StoredProcedure;
            AddCorrection.Parameters.Add("@Id", SqlDbType.VarChar).Value = id;
            AddCorrection.Parameters.Add("@reportId", SqlDbType.VarChar).Value = rid;
            AddCorrection.Parameters.Add("@cTitle", SqlDbType.VarChar).Value = title;
            AddCorrection.Parameters.Add("@cText", SqlDbType.VarChar).Value = content;
            try
            {
                IncidentsConnection.Open();
                AddCorrection.ExecuteNonQuery();
            }
            finally
            {
                IncidentsConnection.Close();
            }
        }
        ////Delete
        internal static void DeleteCorrection(int correctionId)
        {
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlCommand AddCorrection = new SqlCommand("DeleteCorrectiveAction", IncidentsConnection);
            AddCorrection.CommandType = CommandType.StoredProcedure;
            AddCorrection.Parameters.Add("@Id", SqlDbType.Int).Value = correctionId;
            try
            {
                IncidentsConnection.Open();
                AddCorrection.ExecuteNonQuery();
            }
            finally
            {
                IncidentsConnection.Close();
            }
        }

    }
}
