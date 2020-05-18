using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace NCRClass
{
    class Customers
    {
        internal static DataSet LookupAllCustomers()
        {
            DataSet AllCustomers = null;
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlDataAdapter GetAllCustomersAdapter = new SqlDataAdapter("Exec GetAllCustomers", IncidentsConnection);
            AllCustomers = new DataSet();
            GetAllCustomersAdapter.Fill(AllCustomers, "Customers");
            return AllCustomers;
        }
        internal static DataSet LookupCustomer(int customerId)
        {
            DataSet Customer = null;
            using (SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString))
            {
                SqlDataAdapter LookupCustomerAdapter =
                    new SqlDataAdapter("Exec GetCustomerByID @CustomerID", IncidentsConnection);
                LookupCustomerAdapter.SelectCommand.Parameters.Add
                    ("@CustomerID", SqlDbType.Int).Value = customerId;
                Customer = new DataSet();
                LookupCustomerAdapter.Fill(Customer, "Customer");
            }
            return Customer;
        }
        internal static DataSet LookupCustomerByCustomerName(string CustomerName)
        {
            DataSet Customer = null;
            using (SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString))
            {
                SqlDataAdapter LookupCustomerAdapter =
                    new SqlDataAdapter("Exec LookupCustomerByCustomerName @CustomerName", IncidentsConnection);
                LookupCustomerAdapter.SelectCommand.Parameters.Add
                    ("@CustomerName", SqlDbType.VarChar).Value = CustomerName;
                Customer = new DataSet();
                LookupCustomerAdapter.Fill(Customer, "Customer");
            }
            return Customer;

        }
        internal static int AddCustomer(string company, string contactname, string contactphone, string contactext, string contactfax, string Email)
        {
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlCommand AddCustomer = new SqlCommand("AddCustomer", IncidentsConnection);
            AddCustomer.CommandType = CommandType.StoredProcedure;
            AddCustomer.Parameters.Add("@company", SqlDbType.VarChar).Value = company;
            AddCustomer.Parameters.Add("@contactname", SqlDbType.VarChar).Value = contactname;
            AddCustomer.Parameters.Add("@contactphone", SqlDbType.VarChar).Value = contactphone;
            AddCustomer.Parameters.Add("@contactext", SqlDbType.VarChar).Value = contactext;
            AddCustomer.Parameters.Add("@contactfax", SqlDbType.VarChar).Value = contactfax;
            AddCustomer.Parameters.Add("@contactemail", SqlDbType.VarChar).Value = Email;
            int customerId = 0;
            try
            {
                IncidentsConnection.Open();
                customerId = int.Parse(AddCustomer.ExecuteScalar().ToString());

            }
            finally
            {
                IncidentsConnection.Close();
            }
            return customerId;

        }

        ////Update
        internal static void UpdateCustomer(int customerId, string company, string contactname, string contactphone, string contactext, string contactfax, string Email)
        {
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlCommand AddCustomer = new SqlCommand("UpdateCustomer", IncidentsConnection);
            AddCustomer.CommandType = CommandType.StoredProcedure;
            AddCustomer.Parameters.Add("@customerid", SqlDbType.Int).Value = customerId;
            AddCustomer.Parameters.Add("@company", SqlDbType.VarChar).Value = company;
            AddCustomer.Parameters.Add("@contactname", SqlDbType.VarChar).Value = contactname;
            AddCustomer.Parameters.Add("@contactphone", SqlDbType.VarChar).Value = contactphone;
            AddCustomer.Parameters.Add("@contactext", SqlDbType.VarChar).Value = contactext;
            AddCustomer.Parameters.Add("@contactfax", SqlDbType.VarChar).Value = contactfax;
            AddCustomer.Parameters.Add("@contactemail", SqlDbType.VarChar).Value = Email;
            try
            {
                IncidentsConnection.Open();
                AddCustomer.ExecuteNonQuery();
               
            }
            finally
            {
                IncidentsConnection.Close();
            }
          
        }
        ////Delete
        internal static bool DeleteCustomer(int customerId)
        {
            bool CustomerIdDel = false;
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlCommand AddCustomer = new SqlCommand("DeleteCustomer", IncidentsConnection);
            AddCustomer.CommandType = CommandType.StoredProcedure;
            AddCustomer.Parameters.Add("@customerId", SqlDbType.Int).Value = customerId;
            try
            {
                IncidentsConnection.Open();
                CustomerIdDel = int.Parse(AddCustomer.ExecuteScalar().ToString()) == 0;
            }
            finally
            {
                IncidentsConnection.Close();
            }
            return CustomerIdDel;
        }

    }
}
