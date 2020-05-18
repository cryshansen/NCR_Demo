using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace NCRClass
{
    class Events
    {

        internal static DataSet LookupEvents(int Id)
        {//Id= report id
            DataSet Events = null;
            using (SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString))
            {
                //gets events by reportId****
                SqlDataAdapter LookupEventsAdapter =
                    new SqlDataAdapter("Exec GetAllEventsById @Id", IncidentsConnection);
                LookupEventsAdapter.SelectCommand.Parameters.Add
                    ("@Id", SqlDbType.Int).Value = Id;
                Events = new DataSet();
                LookupEventsAdapter.Fill(Events, "Events");
            }
            return Events;
        }
        internal static DataSet LookupEventByEventId(int Id)
        {//Id= eventid
            DataSet Event = null;
            using (SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString))
            {
                SqlDataAdapter LookupEventsAdapter =
                    new SqlDataAdapter("Exec GetEventById @Id", IncidentsConnection);
                LookupEventsAdapter.SelectCommand.Parameters.Add
                    ("@Id", SqlDbType.Int).Value = Id;
                Event = new DataSet();
                LookupEventsAdapter.Fill(Event, "Event");
            }
            return Event;
        }

        internal static DataSet GetEventDDLById(int Id)
        {
            DataSet Event = null;
            using (SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString))
            {
                SqlDataAdapter LookupEventsAdapter =
                    new SqlDataAdapter("Exec GetEventDDLById @Id", IncidentsConnection);
                LookupEventsAdapter.SelectCommand.Parameters.Add
                    ("@Id", SqlDbType.Int).Value = Id;
                Event = new DataSet();
                LookupEventsAdapter.Fill(Event, "Event");
            }
            return Event;

        }
        internal static int AddEvent(int rid, string title, string content, string edate)
        {
            int eventId;
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlCommand AddEvent = new SqlCommand("AddEvent", IncidentsConnection);
            AddEvent.CommandType = CommandType.StoredProcedure;
            AddEvent.Parameters.Add("@reportId", SqlDbType.VarChar).Value = rid;
            AddEvent.Parameters.Add("@title", SqlDbType.VarChar).Value = title;
            AddEvent.Parameters.Add("@eventdate", SqlDbType.VarChar).Value = edate;
            AddEvent.Parameters.Add("@eventText", SqlDbType.VarChar).Value = content;
            try
            {
                IncidentsConnection.Open();
                eventId = int.Parse(AddEvent.ExecuteScalar().ToString());
            }
            finally
            {
                IncidentsConnection.Close();
            }
            return eventId;
        }

        internal static void UpdateEvent(int id, int rid, string title, string content, string edate)
        {
            
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlCommand AddEvent = new SqlCommand("UpdateEvent", IncidentsConnection);
            AddEvent.CommandType = CommandType.StoredProcedure;
            AddEvent.Parameters.Add("@eventId", SqlDbType.VarChar).Value = id;
            AddEvent.Parameters.Add("@reportId", SqlDbType.VarChar).Value = rid;
            AddEvent.Parameters.Add("@title", SqlDbType.VarChar).Value = title;
            AddEvent.Parameters.Add("@eventdate", SqlDbType.VarChar).Value = edate;
            AddEvent.Parameters.Add("@eventText", SqlDbType.VarChar).Value = content;
            try
            {
                IncidentsConnection.Open();
                AddEvent.ExecuteNonQuery();
            }
            finally
            {
                IncidentsConnection.Close();
            }
            
        }
        ////Delete
        internal static bool DeleteEvent(int eventId)
        {
            bool EventIdDel;
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlCommand AddEvent = new SqlCommand("DeleteEvent", IncidentsConnection);
            AddEvent.CommandType = CommandType.StoredProcedure;
            AddEvent.Parameters.Add("@Id", SqlDbType.Int).Value = eventId;
            try
            {
                IncidentsConnection.Open();
                EventIdDel = int.Parse(AddEvent.ExecuteScalar().ToString())==0;
            }
            finally { 
                IncidentsConnection.Close(); 
            }
            return EventIdDel;
        }

    }

    //private class SupplierInfo
    //{
    //    private int _supplierID;
    //    private string _companyName;

    //    public SupplierInfo(int supplierID, string companyName)
    //    {
    //        _supplierID = supplierID;
    //        _companyName = companyName;
    //    }

    //    public int SupplierID
    //    {
    //        get { return _supplierID; }
    //        set { _supplierID = value; }
    //    }

    //    public string CompanyName
    //    {
    //        get { return _companyName; }
    //        set { _companyName = value; }
    //    }
    //}


  

   

}
