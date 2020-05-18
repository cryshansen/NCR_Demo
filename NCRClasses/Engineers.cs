using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace NCRClass
{
    class Engineers
    {
        internal static DataSet LookupAllEngineers()
        {
            DataSet AllEngineers = null;
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlDataAdapter GetAllEngineersAdapter = new SqlDataAdapter("Exec GetAllInitiators", IncidentsConnection);
            AllEngineers = new DataSet();
            GetAllEngineersAdapter.Fill(AllEngineers, "Engineers");
            return AllEngineers;
        }
        internal static DataSet LookupEngineer(int EngineerId)
        {
            DataSet Engineer = null;
            using (SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString))
            {
                SqlDataAdapter LookupEngineerAdapter =
                    new SqlDataAdapter("Exec GetInitiatorById @initId", IncidentsConnection);
                LookupEngineerAdapter.SelectCommand.Parameters.Add
                    ("@initId", SqlDbType.Int).Value = EngineerId;
                Engineer = new DataSet();
                LookupEngineerAdapter.Fill(Engineer, "Engineer");
            }
            return Engineer;
        }

        internal static int AddEngineer(string EngineerName, string EngineerPhone, string EngineerExt, string EngineerFax, string EngineerEmail)
        {
            int EngineerId;
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlCommand AddEngineer = new SqlCommand("AddInitiator", IncidentsConnection);
            AddEngineer.CommandType = CommandType.StoredProcedure;
            AddEngineer.Parameters.Add("@initiatorName", SqlDbType.VarChar).Value = EngineerName;
            AddEngineer.Parameters.Add("@initiatorPhone", SqlDbType.VarChar).Value = EngineerPhone;
            AddEngineer.Parameters.Add("@initiatorExt", SqlDbType.VarChar).Value = EngineerExt;
            AddEngineer.Parameters.Add("@initiatorFax", SqlDbType.VarChar).Value = EngineerFax;
            AddEngineer.Parameters.Add("@initiatorEmail", SqlDbType.VarChar).Value = EngineerEmail;
            try
            {
                IncidentsConnection.Open();
                EngineerId = int.Parse(AddEngineer.ExecuteScalar().ToString());
            }
            finally
            {
                IncidentsConnection.Close();
            }
            return EngineerId;

        }
        internal static void UpdateEngineer(int EngineerId, string EngineerName, string EngineerPhone, string EngineerExt, string EngineerFax, string EngineerEmail)
        {
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlCommand UpdateEngineer = new SqlCommand("UpdateInitiator", IncidentsConnection);
            UpdateEngineer.CommandType = CommandType.StoredProcedure;
            UpdateEngineer.Parameters.Add("@initiatorId", SqlDbType.Int).Value = EngineerId;
            UpdateEngineer.Parameters.Add("@initiatorName", SqlDbType.VarChar).Value = EngineerName;
            UpdateEngineer.Parameters.Add("@initiatorPhone", SqlDbType.VarChar).Value = EngineerPhone;
            UpdateEngineer.Parameters.Add("@initiatorExt", SqlDbType.VarChar).Value = EngineerExt;
            UpdateEngineer.Parameters.Add("@initiatorFax", SqlDbType.VarChar).Value = EngineerFax;
            UpdateEngineer.Parameters.Add("@initiatorEmail", SqlDbType.VarChar).Value = EngineerEmail;
            try
            {

                IncidentsConnection.Open();
                UpdateEngineer.ExecuteNonQuery();
                
            }
           finally
            {
                IncidentsConnection.Close();
            }
            
        }
        ////Delete

        internal static bool DeleteEngineer(int EngineerId)
        {
            bool EngineerIdDel = false;
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlCommand AddEngineer = new SqlCommand("DeleteInitiator", IncidentsConnection);
            AddEngineer.CommandType = CommandType.StoredProcedure;
            AddEngineer.Parameters.Add("@initiatorId", SqlDbType.Int).Value = EngineerId;
            try
            {
                IncidentsConnection.Open();
                EngineerIdDel = int.Parse(AddEngineer.ExecuteScalar().ToString()) == 0;
            }
            finally
            {
                IncidentsConnection.Close();
            }
            return EngineerIdDel;
        }


    }
}
