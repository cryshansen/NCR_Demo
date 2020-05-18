using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace NCRClass
{
    class Misc
    {
        internal static DataSet LookupAllWellTypes()
        {
            DataSet AllWellTypes = null;
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlDataAdapter GetAllProblemsAdapter = new SqlDataAdapter("Exec GetAllWellTypes", IncidentsConnection);
            AllWellTypes = new DataSet();
            GetAllProblemsAdapter.Fill(AllWellTypes, "WellTypes");
            return AllWellTypes;
        }
        internal static DataSet LookupWellType(int wellTypeId)
        {
            DataSet WellType = null;
            using (SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString))
            {
                SqlDataAdapter LookupProblemAdapter =
                    new SqlDataAdapter("Exec GetWellTypeByID @id", IncidentsConnection);
                LookupProblemAdapter.SelectCommand.Parameters.Add
                    ("@id", SqlDbType.Int).Value = wellTypeId;
                WellType = new DataSet();
                LookupProblemAdapter.Fill(WellType, "WellType");
            }
            return WellType;
        }
        internal static int AddWellType(string Name)
        {
            int WellTypeId;
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlCommand AddWellType = new SqlCommand("AddWellType", IncidentsConnection);
            AddWellType.CommandType = CommandType.StoredProcedure;
            AddWellType.Parameters.Add("@Name", SqlDbType.VarChar).Value = Name;
            try
            {
                IncidentsConnection.Open();
                WellTypeId = int.Parse(AddWellType.ExecuteScalar().ToString());
                
            }
            finally
            {
                IncidentsConnection.Close();
            }
            return WellTypeId;
        }
        internal static void UpdateWellType(int Id,string Name)
        {
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlCommand UpdateWellType = new SqlCommand("UpdateWellType", IncidentsConnection);
            UpdateWellType.CommandType = CommandType.StoredProcedure;
            UpdateWellType.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
            UpdateWellType.Parameters.Add("@Name", SqlDbType.VarChar).Value = Name;
            try
            {
                IncidentsConnection.Open();
                UpdateWellType.ExecuteNonQuery();
                
            }
           finally
            {
                IncidentsConnection.Close();
            }
        }
        ////Delete
        internal static bool DeleteWellType(int WellTypeId)
        {
            bool IdDel = false;
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlCommand AddWellType = new SqlCommand("DeleteWellType", IncidentsConnection);
            AddWellType.CommandType = CommandType.StoredProcedure;
            AddWellType.Parameters.Add("@Id", SqlDbType.Int).Value = WellTypeId;
            try
            {
                IncidentsConnection.Open();
                IdDel = int.Parse(AddWellType.ExecuteScalar().ToString()) == 0;
            }
            finally
            {
                IncidentsConnection.Close();
            }
            return IdDel;
        }
        //----------------------------------  Casing Sizes  -----------------------------  

        internal static DataSet LookupAllCasingSizes()
        {
            DataSet AllCasingSizes = null;
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlDataAdapter GetAllProblemsAdapter = new SqlDataAdapter("Exec GetAllCasingSize", IncidentsConnection);
            AllCasingSizes = new DataSet();
            GetAllProblemsAdapter.Fill(AllCasingSizes, "CasingSizes");
            return AllCasingSizes;
        }
        internal static DataSet LookupCasingSize(int casingSizeId)
        {
            DataSet CasingSize = null;
            using (SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString))
            {
                SqlDataAdapter LookupProblemAdapter =
                    new SqlDataAdapter("Exec GetCasingSizeByID @id", IncidentsConnection);
                LookupProblemAdapter.SelectCommand.Parameters.Add
                    ("@id", SqlDbType.Int).Value = casingSizeId;
                CasingSize = new DataSet();
                LookupProblemAdapter.Fill(CasingSize, "CasingSize");
            }
            return CasingSize;
        }

        internal static int AddCasingSize(string Name )
        {
            int CasingSizeId;
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlCommand AddCasingSize = new SqlCommand("AddCasingSize", IncidentsConnection);
            AddCasingSize.CommandType = CommandType.StoredProcedure;
            AddCasingSize.Parameters.Add("@Name", SqlDbType.VarChar).Value = Name;
            try
            {
                IncidentsConnection.Open();
                CasingSizeId = int.Parse(AddCasingSize.ExecuteScalar().ToString());
            }
            finally
            {
                IncidentsConnection.Close();
            }
            return CasingSizeId;
        }
        internal static void UpdateCasingSize(int Id,string Name)
        {
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlCommand AddCasingSize = new SqlCommand("UpdateCasingSize", IncidentsConnection);
            AddCasingSize.CommandType = CommandType.StoredProcedure;
            AddCasingSize.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
            AddCasingSize.Parameters.Add("@Name", SqlDbType.VarChar).Value = Name;
            try
            {
                IncidentsConnection.Open();
                AddCasingSize.ExecuteNonQuery();
            }
            finally
            {
                IncidentsConnection.Close();
            }
        }

        internal static bool DeleteCasingSize(int CasingId)
        {
            bool IdDel = false;
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlCommand AddCasingSize = new SqlCommand("DeleteCasingSize", IncidentsConnection);
            AddCasingSize.CommandType = CommandType.StoredProcedure;
            AddCasingSize.Parameters.Add("@Id", SqlDbType.Int).Value = CasingId;
            try
            {
                IncidentsConnection.Open();
                IdDel = int.Parse(AddCasingSize.ExecuteScalar().ToString()) == 0;
            }
            finally
            {
                IncidentsConnection.Close();
            }
            return IdDel;
        }
        //---------------------------------- Liner Sizes  -----------------------------  

        internal static DataSet LookupAllLinerSizes()
        {
            DataSet AllLinerSizes = null;
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlDataAdapter GetAlldapter = new SqlDataAdapter("Exec GetAllLinerSize", IncidentsConnection);
            AllLinerSizes = new DataSet();
            GetAlldapter.Fill(AllLinerSizes, "LinerSizes");
            return AllLinerSizes;
        }
        internal static DataSet LookupLinerSize(int linerSizeId)
        {
            DataSet LinerSize = null;
            using (SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString))
            {
                SqlDataAdapter LookupAdapter =
                    new SqlDataAdapter("Exec GetLinerSizeByID @id", IncidentsConnection);
                LookupAdapter.SelectCommand.Parameters.Add
                    ("@id", SqlDbType.Int).Value = linerSizeId;
                LinerSize = new DataSet();
                LookupAdapter.Fill(LinerSize, "LinerSize");
            }
            return LinerSize;
        }
        internal static int AddLinerSize(string Name)
        {
            int LinerSizeId;
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlCommand AddLinerSize = new SqlCommand("AddLinerSize", IncidentsConnection);
            AddLinerSize.CommandType = CommandType.StoredProcedure;
            AddLinerSize.Parameters.Add("@Name", SqlDbType.VarChar).Value = Name;
            try
            {
                IncidentsConnection.Open();
                LinerSizeId = int.Parse(AddLinerSize.ExecuteScalar().ToString());
            }
            finally
            {
                IncidentsConnection.Close();
            }
            return LinerSizeId;
        }
        internal static void UpdateLinerSize(int Id, string Name)
        {
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlCommand UpdateLinerSize = new SqlCommand("UpdateLinerSize", IncidentsConnection);
            UpdateLinerSize.CommandType = CommandType.StoredProcedure;
            UpdateLinerSize.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
            UpdateLinerSize.Parameters.Add("@Name", SqlDbType.VarChar).Value = Name;
            try
            {
                IncidentsConnection.Open();
                UpdateLinerSize.ExecuteNonQuery();
            }
            finally            
            {
                IncidentsConnection.Close();
            }
        }

        internal static bool DeleteLinerSize(int LinerId)
        {
            bool IdDel = false;
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlCommand AddLinerSize = new SqlCommand("DeleteLinerSize", IncidentsConnection);
            AddLinerSize.CommandType = CommandType.StoredProcedure;
            AddLinerSize.Parameters.Add("@Id", SqlDbType.Int).Value = LinerId;
            try
            {
                IncidentsConnection.Open();
                IdDel = int.Parse(AddLinerSize.ExecuteScalar().ToString()) == 0;
            }
            finally
            {
                IncidentsConnection.Close();
            }
            return IdDel;
        }
        //----------------------------------  System Types  -----------------------------  

        internal static DataSet LookupAllSystemTypes()
        {
            DataSet AllSystemTypes = null;
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlDataAdapter GetAllProblemsAdapter = new SqlDataAdapter("Exec GetAllSystemType", IncidentsConnection);
            AllSystemTypes = new DataSet();
            GetAllProblemsAdapter.Fill(AllSystemTypes, "SystemTypes");
            return AllSystemTypes;
        }
        internal static DataSet LookupSystemType(int SystemTypeId)
        {
            DataSet SystemType = null;
            using (SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString))
            {
                SqlDataAdapter LookupProblemAdapter =
                    new SqlDataAdapter("Exec GetSystemTypeByID @id", IncidentsConnection);
                LookupProblemAdapter.SelectCommand.Parameters.Add
                    ("@id", SqlDbType.Int).Value = SystemTypeId;
                SystemType = new DataSet();
                LookupProblemAdapter.Fill(SystemType, "SystemType");
            }
            return SystemType;
        }
        internal static int AddSystemType(string Name)
        {
            int SystemTypeId;
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlCommand AddSystemType = new SqlCommand("AddSystemType", IncidentsConnection);
            AddSystemType.CommandType = CommandType.StoredProcedure;
            AddSystemType.Parameters.Add("@Name", SqlDbType.VarChar).Value = Name;
            try
            {
                IncidentsConnection.Open();
                SystemTypeId = int.Parse(AddSystemType.ExecuteScalar().ToString());
            }
            finally
            {
                IncidentsConnection.Close();
            }
            return SystemTypeId;
        }
        internal static void UpdateSystemType(int Id, string Name)
        {
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlCommand UpdateSystemType = new SqlCommand("UpdateSystemType", IncidentsConnection);
            UpdateSystemType.CommandType = CommandType.StoredProcedure;
            UpdateSystemType.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
            UpdateSystemType.Parameters.Add("@Name", SqlDbType.VarChar).Value = Name;
            try
            {
                IncidentsConnection.Open();
                UpdateSystemType.ExecuteNonQuery();
            }
            finally
            {
                IncidentsConnection.Close();
            }
        }

        internal static bool DeleteSystemType(int SystemTypeId)
        {
            bool IdDel = false;
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlCommand AddSystemType = new SqlCommand("DeleteSystemType", IncidentsConnection);
            AddSystemType.CommandType = CommandType.StoredProcedure;
            AddSystemType.Parameters.Add("@Id", SqlDbType.Int).Value = SystemTypeId;
            try
            {
                IncidentsConnection.Open();
                IdDel = int.Parse(AddSystemType.ExecuteScalar().ToString()) == 0;
            }
            finally
            {
                IncidentsConnection.Close();
            }
            return IdDel;
        }
        //----------------------------------  Tool List  -----------------------------  

        internal static DataSet LookupAllToolLists()
        {
            DataSet AllToolLists = null;
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlDataAdapter GetAllProblemsAdapter = new SqlDataAdapter("Exec GetAllToolList", IncidentsConnection);
            AllToolLists = new DataSet();
            GetAllProblemsAdapter.Fill(AllToolLists, "ToolLists");
            return AllToolLists;
        }
        internal static DataSet LookupToolList(int ToolListId)
        {
            DataSet ToolList = null;
            using (SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString))
            {
                SqlDataAdapter LookupProblemAdapter =
                    new SqlDataAdapter("Exec GetToolListByID @id", IncidentsConnection);
                LookupProblemAdapter.SelectCommand.Parameters.Add
                    ("@id", SqlDbType.Int).Value = ToolListId;
                ToolList = new DataSet();
                LookupProblemAdapter.Fill(ToolList, "ToolList");
            }
            return ToolList;
        }
        internal static int AddToolList(string Name)
        {
            int ToolListId;
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlCommand AddToolList = new SqlCommand("AddToolList", IncidentsConnection);
            AddToolList.CommandType = CommandType.StoredProcedure;
            AddToolList.Parameters.Add("@Name", SqlDbType.VarChar).Value = Name;
            try
            {
                IncidentsConnection.Open();
                ToolListId = int.Parse(AddToolList.ExecuteScalar().ToString());
            }
            finally            
            {
                IncidentsConnection.Close();
            }
            return ToolListId;
        }
        internal static void UpdateToolList(int Id, string Name)
        {
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlCommand UpdateToolList = new SqlCommand("UpdateToolList", IncidentsConnection);
            UpdateToolList.CommandType = CommandType.StoredProcedure;
            UpdateToolList.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
            UpdateToolList.Parameters.Add("@Name", SqlDbType.VarChar).Value = Name;
            try
            {
                IncidentsConnection.Open();
                UpdateToolList.ExecuteNonQuery();
            }
            finally            
            {
                IncidentsConnection.Close();
            }
        }

        internal static bool DeleteToolList(int ToolListId)
        {
            bool IdDel = false;
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlCommand AddToolList = new SqlCommand("DeleteToolList", IncidentsConnection);
            AddToolList.CommandType = CommandType.StoredProcedure;
            AddToolList.Parameters.Add("@Id", SqlDbType.Int).Value = ToolListId;
            try
            {
                IncidentsConnection.Open();
                IdDel = int.Parse(AddToolList.ExecuteScalar().ToString()) == 0;
            }
            finally
            {
                IncidentsConnection.Close();

            }
            return IdDel;
        }

    }
}
