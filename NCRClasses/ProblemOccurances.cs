using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace NCRClass
{
    class ProblemOccurances
    {

        internal static DataSet LookupAllProblems()
        {
            DataSet AllProblems = null;
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlDataAdapter GetAllProblemsAdapter = new SqlDataAdapter("Exec GetAllProblemOccurances", IncidentsConnection);
            AllProblems = new DataSet();
            GetAllProblemsAdapter.Fill(AllProblems, "Problems");
            return AllProblems;
        }
        internal static DataSet LookupProblem(int problemId)
        {
            DataSet Problem = null;
            using (SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString))
            {
                SqlDataAdapter LookupProblemAdapter =
                    new SqlDataAdapter("Exec GetProblemOccuranceByID @id", IncidentsConnection);
                LookupProblemAdapter.SelectCommand.Parameters.Add
                    ("@id", SqlDbType.Int).Value = problemId;
                Problem = new DataSet();
                LookupProblemAdapter.Fill(Problem, "Problem");
            }
            return Problem;
        }
        internal static int AddProblemOccurance(string Name, string Def)
        {

            int ProblemId = 0;
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlCommand AddProblemOccurance = new SqlCommand("AddProblemOccurance", IncidentsConnection);
            AddProblemOccurance.CommandType = CommandType.StoredProcedure;
            AddProblemOccurance.Parameters.Add("@Name", SqlDbType.VarChar).Value = Name;
            AddProblemOccurance.Parameters.Add("@Def", SqlDbType.Text).Value = Def;
            try
            {
                IncidentsConnection.Open();
                ProblemId = int.Parse(AddProblemOccurance.ExecuteScalar().ToString());
            }
            finally
            {
                IncidentsConnection.Close();
            }
            return ProblemId;
        }
        internal static void UpdateProblemOccurance(int Id, string Name, string Def)
        {
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlCommand UpdateProblemOccurance = new SqlCommand("UpdateProblemOccurance", IncidentsConnection);
            UpdateProblemOccurance.CommandType = CommandType.StoredProcedure;
            UpdateProblemOccurance.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
            UpdateProblemOccurance.Parameters.Add("@Name", SqlDbType.VarChar).Value = Name;
            UpdateProblemOccurance.Parameters.Add("@Def", SqlDbType.Text).Value = Def;
            try
            {
                IncidentsConnection.Open();
                UpdateProblemOccurance.ExecuteNonQuery();
            }
            finally
            {
                IncidentsConnection.Close();
            }
        }

        ////Delete
        internal static bool DeleteProblemOccurance(int problemId)
        {
            bool ProblemIdDel = false;
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlCommand AddCustomer = new SqlCommand("DeleteProblemOccurance", IncidentsConnection);
            AddCustomer.CommandType = CommandType.StoredProcedure;
            AddCustomer.Parameters.Add("@Id", SqlDbType.Int).Value = problemId;
            try
            {
                IncidentsConnection.Open();
                ProblemIdDel = int.Parse(AddCustomer.ExecuteScalar().ToString()) == 0;
            }
            finally
            {
                IncidentsConnection.Close();
            }
            return ProblemIdDel;
        }

    }
}
