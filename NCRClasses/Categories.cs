using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace NCRClass
{
    class Categories
    {

        internal static DataSet LookupAllCategoriesByParentId(int parentId)
        {
            DataSet AllCategories = null;
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlDataAdapter GetAllCategoriesAdapter = new SqlDataAdapter("Exec GetAllOccuranceCategoriesByParentID @Pid", IncidentsConnection);
            GetAllCategoriesAdapter.SelectCommand.Parameters.Add
                    ("@Pid", SqlDbType.Int).Value = parentId;
            AllCategories = new DataSet();
            GetAllCategoriesAdapter.Fill(AllCategories, "Categories");
            return AllCategories;
        }
        internal static DataSet LookupCategoryName(int parentId)
        {
            DataSet Category = null;
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlDataAdapter GetAllCategoriesAdapter = new SqlDataAdapter("Exec GetCategoryName @Pid", IncidentsConnection);
            GetAllCategoriesAdapter.SelectCommand.Parameters.Add
                    ("@Pid", SqlDbType.Int).Value = parentId;
            Category = new DataSet();
            GetAllCategoriesAdapter.Fill(Category, "Category");
            return Category;
        }
        internal static int AddCategory( int parentId, string Name)
        {
            int categoryId =0;
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlCommand AddCategory = new SqlCommand("AddOccuranceCategory", IncidentsConnection);
            AddCategory.CommandType = CommandType.StoredProcedure;
            AddCategory.Parameters.Add("@ParentId", SqlDbType.VarChar).Value = parentId;
            AddCategory.Parameters.Add("@Name", SqlDbType.VarChar).Value = Name;
            try
            {
                IncidentsConnection.Open();
                categoryId = int.Parse(AddCategory.ExecuteScalar().ToString());
                
                
            }finally
            {
                IncidentsConnection.Close();
            }
            return categoryId;
        }
        internal static void UpdateCategory(int Id, int parentId,string Name)
        {
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlCommand UpdateCategory = new SqlCommand("UpdateOccuranceCategory", IncidentsConnection);
            UpdateCategory.CommandType = CommandType.StoredProcedure;
            UpdateCategory.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
            UpdateCategory.Parameters.Add("@ParentId", SqlDbType.VarChar).Value = parentId;
            UpdateCategory.Parameters.Add("@Name", SqlDbType.Text).Value = Name;
            try
            {
                IncidentsConnection.Open();
                UpdateCategory.ExecuteNonQuery();
                
            }
            finally
            {
                IncidentsConnection.Close();
            }
        }
        internal static bool DeleteCategory(int Id)
        {
            bool CategoryIdDel = false;
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlCommand DeleteCategory = new SqlCommand("DeleteOccuranceCategory", IncidentsConnection);
            DeleteCategory.CommandType = CommandType.StoredProcedure;
            DeleteCategory.Parameters.Add("@Id", SqlDbType.Int).Value = Id;

            try
            {
                IncidentsConnection.Open();

                CategoryIdDel = int.Parse(DeleteCategory.ExecuteScalar().ToString()) == 0;
            }
            finally
            {
                IncidentsConnection.Close();
            }
            return CategoryIdDel;
        }

    }
}
