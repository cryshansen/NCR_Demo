using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NCRClass
{
    class NCRClasses
    {
/// <summary>
/// Each NCR FTR class goes in here.....
/// </summary>
class asdf
{
void dos()
{
Customers c = new Customers(1);
c.company;

}
}
public class Utility
{
public static void WriteToErrorLog(System.Exception error)
{
System.Diagnostics.EventLog.WriteEntry("NCRFTR Application", error.Message);
}
}
class Customers
{
#region Private Fields
private int id;
/// <summary>
/// Company Name
/// </summary>
public string company;
private string contactname;
private string contactphone;
#endregion
#region Public Props
public int ID
{
get
{

return id;
}
set
{
id = value;
}
}
public string ASDF
{
get
{
return asdfadsf;
}
set
{
asdfadsf = value;
}
}
#endregion
public Customers(int _ID)
{
this.id = _ID;
DataRow dr = Customers.Customers_DAL.LookupCustomer(this.id).Tables[0].Rows[0];
this.company = dr["company"].ToString();
}
#region DAL
public class Customers_DAL
{

internal static DataSet LookupAllCustomers
{
get
{
DataSet AllCustomers = null;
SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
SqlDataAdapter GetAllCustomersAdapter = new SqlDataAdapter("Exec GetAllCustomers", IncidentsConnection);
AllCustomers = new DataSet();
GetAllCustomersAdapter.Fill(AllCustomers, "Customers");
return AllCustomers;
}
}
internal static DataSet LookupCustomer(int customerId)
{
try
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
catch (Exception err)
{
Utility.WriteToErrorLog(err);
throw err;
}
}
internal static DataSet LookupCustomerByCustomerName(string CustomerName)
{
DataSet Customer = null;
using (SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString))
{
SqlCommand cmd = new SqlCommand();
cmd.CommandType = CommandType.StoredProcedure;
cmd.CommandText = "LookupCustomerByCustomerName";
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
try
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
IncidentsConnection.Close();
}
catch (System.Data.SqlClient.SqlException ex)
{
throw ex;
}
return customerId;
}
catch (Exception err)
{
throw err;
}
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
IncidentsConnection.Close();
}
catch (System.Data.SqlClient.SqlException ex)
{
throw ex;
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
IncidentsConnection.Open();
CustomerIdDel = int.Parse(AddCustomer.ExecuteScalar().ToString()) == 0;
IncidentsConnection.Close();
return CustomerIdDel;
}
#endregion
}
}
}
    }

