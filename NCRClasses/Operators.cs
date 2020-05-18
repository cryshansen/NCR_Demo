using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace NCRClass
{
    class Operators
    {
        internal static DataSet LookupAllOperators()
        {
            DataSet AllOperators = null;
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlDataAdapter GetAllCustomersAdapter = new SqlDataAdapter("Exec GetAllOperators", IncidentsConnection);
            AllOperators = new DataSet();
            GetAllCustomersAdapter.Fill(AllOperators, "Operators");
            return AllOperators;
        }
        internal static DataSet LookupOperator(int operatorId)
        {
            DataSet Operator = null;
            using (SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString))
            {
                SqlDataAdapter LookupCustomerAdapter =
                    new SqlDataAdapter("Exec GetOperatorById @OperatorId", IncidentsConnection);
                LookupCustomerAdapter.SelectCommand.Parameters.Add
                    ("@OperatorId", SqlDbType.Int).Value = operatorId;
                Operator = new DataSet();
                LookupCustomerAdapter.Fill(Operator, "Operator");
            }
            return Operator;
        }

        internal static int AddOperator(string operatorName, string @operatorPhone, string operatorExt, string @operatorFax, string operatorEmail)
        {
            int OperatorId;
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlCommand AddOperator = new SqlCommand("AddOperator", IncidentsConnection);
            AddOperator.CommandType = CommandType.StoredProcedure;
            AddOperator.Parameters.Add("@operatorName", SqlDbType.VarChar).Value = operatorName;
            AddOperator.Parameters.Add("@operatorPhone", SqlDbType.VarChar).Value = operatorPhone;
            AddOperator.Parameters.Add("@operatorExt", SqlDbType.VarChar).Value = operatorExt;
            AddOperator.Parameters.Add("@operatorFax", SqlDbType.VarChar).Value = operatorFax;
            AddOperator.Parameters.Add("@operatorEmail", SqlDbType.VarChar).Value = operatorEmail;
            try
            {
                IncidentsConnection.Open();
                OperatorId = int.Parse(AddOperator.ExecuteScalar().ToString());
            }
            finally
            {
                IncidentsConnection.Close();
            }
           return OperatorId;

        }
        internal static void UpdateOperator(int operatorId, string operatorName, string @operatorPhone, string operatorExt, string @operatorFax, string operatorEmail)
        {
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlCommand AddOperator = new SqlCommand("UpdateOperator", IncidentsConnection);
            AddOperator.CommandType = CommandType.StoredProcedure;
            AddOperator.Parameters.Add("@operatorId", SqlDbType.VarChar).Value = operatorId;
            AddOperator.Parameters.Add("@operatorName", SqlDbType.VarChar).Value = operatorName;
            AddOperator.Parameters.Add("@operatorPhone", SqlDbType.VarChar).Value = operatorPhone;
            AddOperator.Parameters.Add("@operatorExt", SqlDbType.VarChar).Value = operatorExt;
            AddOperator.Parameters.Add("@operatorFax", SqlDbType.VarChar).Value = operatorFax;
            AddOperator.Parameters.Add("@operatorEmail", SqlDbType.VarChar).Value = operatorEmail;
            try
            {
                IncidentsConnection.Open();
                AddOperator.ExecuteNonQuery();
            }
            finally
            {
                IncidentsConnection.Close();
            }
        }
        ////Delete
        internal static bool DeleteOperator(int operatorId)
        {
            bool OperatorIdDel = false;
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlCommand AddOperator = new SqlCommand("DeleteOperator", IncidentsConnection);
            AddOperator.CommandType = CommandType.StoredProcedure;
            AddOperator.Parameters.Add("@operatorId", SqlDbType.Int).Value = operatorId;
            try
            {
                IncidentsConnection.Open();
                OperatorIdDel = int.Parse(AddOperator.ExecuteScalar().ToString())==0;
            }
            finally
            {
                IncidentsConnection.Close();
            }
            return OperatorIdDel;
        }

    }
}
