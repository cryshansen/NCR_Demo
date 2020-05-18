using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace NCRClass
{
    class Locations
    {
        internal static DataSet LookupAllLocations()
        {
            DataSet AllLocations = null;
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlDataAdapter GetAllLocationsAdapter = new SqlDataAdapter("Exec GetAllLocations", IncidentsConnection);
            AllLocations = new DataSet();
            GetAllLocationsAdapter.Fill(AllLocations, "Locations");
            return AllLocations;
        }
        internal static DataSet LookupLocation(int locationId)
        {
            DataSet Location = null;
            using (SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString))
            {
                SqlDataAdapter LookupLocationAdapter =
                    new SqlDataAdapter("Exec GetLocationById @locId", IncidentsConnection);
                LookupLocationAdapter.SelectCommand.Parameters.Add("@locId", SqlDbType.Int).Value = locationId;
                Location = new DataSet();
                LookupLocationAdapter.Fill(Location, "Location");
            }
            return Location;
        }

        internal static int AddLocation(string district, string mgrName, string address, string city, string provstate)
        {
            int locationId;
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlCommand AddLocation = new SqlCommand("AddLocation", IncidentsConnection);
            AddLocation.CommandType = CommandType.StoredProcedure;
            AddLocation.Parameters.Add("@district", SqlDbType.VarChar).Value = district;
            AddLocation.Parameters.Add("@mgrName", SqlDbType.VarChar).Value = mgrName;
            AddLocation.Parameters.Add("@address", SqlDbType.VarChar).Value = address;
            AddLocation.Parameters.Add("@city", SqlDbType.VarChar).Value = city;
            AddLocation.Parameters.Add("@prov_state", SqlDbType.VarChar).Value = provstate;
            try
            {
                IncidentsConnection.Open();
                locationId = int.Parse(AddLocation.ExecuteScalar().ToString());
                
            }
            finally
            {
                IncidentsConnection.Close();
            }
           
            return locationId;

        }
        internal static void UpdateLocation(int locationId, string district, string mgrName, string address, string city, string provstate)
        {
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlCommand UpdateLocation = new SqlCommand("UpdateLocation", IncidentsConnection);
            UpdateLocation.CommandType = CommandType.StoredProcedure;
            UpdateLocation.Parameters.Add("@locationId", SqlDbType.VarChar).Value = locationId;
            UpdateLocation.Parameters.Add("@district", SqlDbType.VarChar).Value = district;
            UpdateLocation.Parameters.Add("@mgrName", SqlDbType.VarChar).Value = mgrName;
            UpdateLocation.Parameters.Add("@address", SqlDbType.VarChar).Value = address;
            UpdateLocation.Parameters.Add("@city", SqlDbType.VarChar).Value = city;
            UpdateLocation.Parameters.Add("@prov_state", SqlDbType.VarChar).Value = provstate;
            try
            {
                IncidentsConnection.Open();
                UpdateLocation.ExecuteNonQuery();
                
            }
            finally
            {
                IncidentsConnection.Close();
            }
        }

        ////Delete
        internal static bool DeleteLocation(int locationId)
        {
            bool LocationIdDel = false;
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlCommand AddLocation = new SqlCommand("DeleteLocation", IncidentsConnection);
            AddLocation.CommandType = CommandType.StoredProcedure;
            AddLocation.Parameters.Add("@locationId", SqlDbType.Int).Value = locationId;
            try
            {
                IncidentsConnection.Open();
                LocationIdDel = int.Parse(AddLocation.ExecuteScalar().ToString()) == 0;
            }
            finally
            {
                IncidentsConnection.Close();
            }
            return LocationIdDel;
        }

    }
}
