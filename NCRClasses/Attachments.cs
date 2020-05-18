using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace NCRClass
{
    class Attachments
    {
        internal static DataSet LookupAttachments(int Id)
        {//Id= report id
            DataSet Attachments = null;
            using (SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString))
            {
                SqlDataAdapter LookupAttachmentsAdapter =
                    new SqlDataAdapter("Exec GetAllAttachmentsById @Id", IncidentsConnection);
                LookupAttachmentsAdapter.SelectCommand.Parameters.Add
                    ("@Id", SqlDbType.Int).Value = Id;
                Attachments = new DataSet();
                LookupAttachmentsAdapter.Fill(Attachments, "Attachments");
            }
            return Attachments;
        }
        //internal static DataSet LookupAttachmentByAttachId(int Id)
        //{//Id= report id
        //    DataSet Attachment = null;
        //    using (SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString))
        //    {
        //        SqlDataAdapter LookupAttachmentsAdapter =
        //            new SqlDataAdapter("Exec GetAttachmentById @Id", IncidentsConnection);
        //        LookupAttachmentsAdapter.SelectCommand.Parameters.Add
        //            ("@Id", SqlDbType.Int).Value = Id;
        //        Attachment = new DataSet();
        //        LookupAttachmentsAdapter.Fill(Attachment, "Attachment");
        //    }
        //    return Attachment;
        //}
        internal static DataSet GetAttachementDDLById(int Id)
        {
            DataSet Attachement = null;
            using (SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString))
            {
                SqlDataAdapter LookupEventsAdapter =
                    new SqlDataAdapter("Exec GetAttachmentsDDLById @Id", IncidentsConnection);
                LookupEventsAdapter.SelectCommand.Parameters.Add
                    ("@Id", SqlDbType.Int).Value = Id;
                Attachement = new DataSet();
                LookupEventsAdapter.Fill(Attachement, "Attachement");
            }
            return Attachement;

        }

        internal static int AddAttachment(int rid, string title,string filename,string source, string content)
        {
            int attachmentId;
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlCommand AddAttachment = new SqlCommand("AddAttachment", IncidentsConnection);
            AddAttachment.CommandType = CommandType.StoredProcedure;
            AddAttachment.Parameters.Add("@reportId", SqlDbType.Int).Value = rid;
            AddAttachment.Parameters.Add("@title", SqlDbType.NVarChar).Value = title;
            AddAttachment.Parameters.Add("@filename", SqlDbType.NVarChar).Value = filename;
            AddAttachment.Parameters.Add("@sourcefolder", SqlDbType.NVarChar).Value = source;
            AddAttachment.Parameters.Add("@explanation", SqlDbType.VarChar).Value = content;
            try
            {
                IncidentsConnection.Open();
                attachmentId = int.Parse(AddAttachment.ExecuteScalar().ToString());
            }
            finally
            {
                IncidentsConnection.Close();
            }
            return attachmentId;
        }

        internal static void UpdateAttachment(int id, int rid, string title,string filename,string source, string content)
        {
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlCommand AddAttachment = new SqlCommand("UpdateAttachment", IncidentsConnection);
            AddAttachment.CommandType = CommandType.StoredProcedure;
            AddAttachment.Parameters.Add("@Id", SqlDbType.VarChar).Value = id;
            AddAttachment.Parameters.Add("@reportId", SqlDbType.VarChar).Value = rid;
            AddAttachment.Parameters.Add("@title", SqlDbType.VarChar).Value = title;
            AddAttachment.Parameters.Add("@filename", SqlDbType.NVarChar).Value = filename;
            AddAttachment.Parameters.Add("@source", SqlDbType.NVarChar).Value = source;
            AddAttachment.Parameters.Add("@text", SqlDbType.VarChar).Value = content;
            try
            {
                IncidentsConnection.Open();
                AddAttachment.ExecuteNonQuery();
            }
            finally
            {
                IncidentsConnection.Close();
            }
        }
        ////Delete
        internal static void DeleteAttachment(int attachmentId)
        {
            SqlConnection IncidentsConnection = new SqlConnection(GlobalConstants.IncidentsConnectionString);
            SqlCommand AddAttachment = new SqlCommand("DeleteAttachment", IncidentsConnection);
            AddAttachment.CommandType = CommandType.StoredProcedure;
            AddAttachment.Parameters.Add("@Id", SqlDbType.Int).Value = attachmentId;
            try
            {
                IncidentsConnection.Open();
                AddAttachment.ExecuteNonQuery();
            }
            finally
            {
                IncidentsConnection.Close();
            }
        }


    }
}
