using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace NCRClass
{
    class BLClass
    {
        /// <summary>
        /// This handles logic of Report Number functioning
        /// Each new add gets the first available record number not in use.
        /// used='N' passes it back to the presentation layer and 
        /// sets that report number used to 'Y'
        /// </summary>
        /// <returns></returns>
        public static string GetNextReportId()
        {
            //get the next available reportno. 
            DataSet Reports = null;
            string reportNumber = "";
            int Id=0;
            Reports = ReportNumbers.GetNextReportId();
            //want to only grab the first row ReportNumber and id.
            int count = Reports.Tables[0].Rows.Count;
            if (count > 0 && Id==0 )
            {
                reportNumber = Reports.Tables["ReportNo"].Rows[0]["ReportId"].ToString();
                int.TryParse(Reports.Tables["ReportNo"].Rows[0]["Id"].ToString(), out Id);
                ReportNumbers.UpdateReportNumbers(Id, reportNumber, 'Y');
            }
            return reportNumber;
        }

        internal static bool ReportNumberUsed(string ReportNumber)
        {   //check if reportnumber used in detail reports if not then 
            //update reportnumbers table with 'n'

            bool flag = true;
            int validReport = 0;
            validReport = ReportNumbers.ReportNumberUsed(ReportNumber);
            if (validReport == 0)
            {//if it exists, the user canceled out of anything but add mode. so we dont change it
                DataSet Reports = null;
                int Id=0;
                Reports = ReportNumbers.getReportInfo(ReportNumber);
                int.TryParse(Reports.Tables["Report"].Rows[0]["Id"].ToString(), out Id);
                if (Id != 0)
                {
                    ReportNumbers.UpdateReportNumbers(Id, ReportNumber, 'N');
                }
                flag = false;
            }
            return flag;
        }
        internal static void DeleteDetailReport(int reportId,string reportNumber)
        { 
            //delete the record if success
            bool success = false;
            try
            {
                success = Incidents.DeleteDetailReport(reportId);
                if (success == true)
                {
                    DataSet Reports = null;
                    int Id = 0;
                    Reports = ReportNumbers.getReportInfo(reportNumber);
                    int.TryParse(Reports.Tables["Report"].Rows[0]["Id"].ToString(), out Id);
                    if (Id != 0)
                    {
                        ReportNumbers.UpdateReportNumbers(Id, reportNumber, 'N');
                    }
                }
            }catch
            {
                throw new Exception("The delete failed.");
            }
        }
        internal static string addAndToArray(string[] likequery,int i)
        {//we have an array to step through to put and in
            string query="";
            //count the array 
            //i=34
            //i = i;// -1;//33
            for(int a=0;a<i;a++)
            {
                if (a != (i - 1))
                {
                    query += " " + likequery[a] + " and ";
                }
                else {
                    query += " " + likequery[a];
                
                }
            }
            //34 slots filled
            //35 with date
            return query;
        }
        internal static string checkStatus(string likequery, int counter)
        {
            if (counter != 0)
            {
                likequery += " and ";
            }
            return likequery;
        }
        internal static string getTableJoins(string[] tables)
        {
            string query = " ";
            for (int i = 0; i < tables.Length; i++)
            { 
                query += " Inner Join " + tables[i] + " ON Incidents.ReportId = " + tables[i]+".ReportID";
            
            }

            return query;
        }
        //DataSet
        internal static DataSet SearchFields(int customerId, int operatorid, int engineerid, int locationid, string ReportType, string SearchDateTB, string DRtitle, string DRfieldticket, string DRwellname, int WellTypeDDL, string DRLSDTB, string DRSectionTB, string DRTownshipTB, string DRRangeTB, string DRMeridianTB, int CasingSizeDDL, int LinerSizeDDL, string DRTVDTB, string DRTMDTB, int ToolListDDL, int SystemTypeDDL, string DRExecSummary, string DRDescription, string DRObservations, string Notes, string Notes2, int ProbOccurDDL, int CategoryDDL, int SubCategoryDDL, string DRPrefaceTA, string EventsTB, string AttachmentTB, string CausesTB, string DRCauseAnalysisTA, string RemediationTB)
        { 
            string[] likequery = new string[35];
            string query = "";
            string[] tables = new string[4];
            List<string> fields = new List<string>();
            //string datatables = "";
            //int counter = 0;
            int i=0;
            if (customerId != 0)
            {
                likequery[i] = " CustomerID=" + customerId ;
                i++;
                //counter = addCounter(counter);
                //checkStatus(likequery,counter);
            }
            if (operatorid != 0)
            {
                likequery[i] = "OperatorID=" + operatorid ;
                i++;
            }
            if (engineerid != 0)
            {
                likequery[i] = "InitiatorID=" + engineerid ;
                i++;
            }
            if (locationid != 0)
            {
                likequery[i] = "LocationID=" + locationid ;
                i++;
            }

            if(ReportType.Trim() !="")
            {
                likequery[i] = "ReportType Like '%" + ReportType + "%' ";
                i++;
            }
            if (SearchDateTB.Trim() != "")
            {
                likequery[i] = " ReportDate = '" + SearchDateTB + "'";
                i++;
            }
            if (DRfieldticket.Trim() !="")
            {
                likequery[i] = " FieldTicket Like '%" + DRfieldticket + "%'";
                i++;

            }
            if (DRwellname.Trim() !="")
            {
                likequery[i] = "WellName Like '%" + DRwellname + "%' ";
                i++;
            }

            if (DRLSDTB.Trim() !="")
            {
                likequery[i] = "LSD Like '%" + DRLSDTB + "%' ";
                i++;
            }

            if (DRSectionTB.Trim() !="")
            {
                likequery[i] = "Section Like '%" + DRSectionTB + "%' ";
                i++;
            }

            if (DRTownshipTB.Trim() != "")
            {
                likequery[i] = "Township Like '%" + DRTownshipTB + "%' ";
                i++;
            }

            if (DRRangeTB.Trim() !="")
            {
                likequery[i] = "DRRange Like '%" + DRRangeTB + "%' ";
                i++;
            }

            if (DRMeridianTB.Trim() !="")
            {
                likequery[i] = "Meridian Like '%" + DRMeridianTB + "%' ";
                i++;
            }

            if (WellTypeDDL != 0)
            {
                likequery[i] = "WellTypeID=" + WellTypeDDL ;
                i++;
            }

            if (DRTMDTB.Trim() !="")
            {
                likequery[i] += "WellTMD Like '%" + DRTMDTB + "%' ";
                i++;
            }

            if (DRTVDTB.Trim() !="")
            {
                likequery[i] = "WellTVD Like '%" + DRTVDTB + "%' ";
                i++;
            }
            if (CasingSizeDDL != 0)
            {
                likequery[i] = "CasingSizeID =" + CasingSizeDDL;
                i++;
            }
            if (LinerSizeDDL != 0)
            {
                likequery[i] = "LinerSize=" + LinerSizeDDL;
                i++;
            }
            if (SystemTypeDDL != 0)
            {
                likequery[i] = "SystemType=" + SystemTypeDDL;
                i++;
            }

            if (ToolListDDL != 0)
            {
                likequery[i] = "ToolList=" + ToolListDDL;
                i++;
            }
            if (ProbOccurDDL != 0)
            {
                likequery[i] = "ProblemId =" + ProbOccurDDL;
                i++;
            }
            if (CategoryDDL != 0)
            {
                likequery[i] = "CategoryId=" + CategoryDDL ;
                i++;
            }
            if (SubCategoryDDL != 0)
            {
                likequery[i] = "CatDescriptId=" + SubCategoryDDL;
                i++;
            }
            if (Notes.Trim() !="")
            {
                likequery[i] = "ProblemNotes Like '%" + Notes + "%' ";
                i++;
            }
            if (DRExecSummary.Trim() !="")
            {
                likequery[i] = "ExecSummary Like '%" + DRExecSummary + "%' ";
                i++;
            }
            if (DRDescription.Trim() !="")
            {
                likequery[i] = "Description Like '%" + DRDescription + "%' ";
                i++;
            }
            if (DRObservations.Trim() !="")
            {
                likequery[i] = "Observation Like '%" + DRObservations + "%' ";
                i++;
            }
            if (Notes2.Trim() !="")
            {
                likequery[i] = "EventNotes Like '%" + Notes2 + "%' ";
                i++;
            }
            if (DRPrefaceTA.Trim() !="")
            {
                likequery[i] = "Preface Like '%" + DRPrefaceTA + "%' ";
                i++;
            }
            if (DRCauseAnalysisTA.Trim() !="")
            {
                likequery[i] = "CauseAnalysis Like '%" + DRCauseAnalysisTA + "%' ";
                i++;
            }
            if (EventsTB.Trim() != "")
            {
                likequery[i] = "Events.Title Like '%" + EventsTB + "%' or Events.EventText Like '%" + EventsTB + "%'";
                i++;
                fields.Add("Events");
                //tables[0] = "Events";
            }
            if (CausesTB.Trim() != "")
            {
                likequery[i] = "Causes.CauseTitle Like '%" + CausesTB + "%' or Causes.CauseText Like '%" + CausesTB + "%'";
                i++;
                fields.Add("Causes");
                //tables[1] = "Causes";
            }
            if (RemediationTB.Trim() != "")
            {
                likequery[i] = "CorrectiveActions.CorrectiveActTitle Like '%" + RemediationTB + "%' or CorrectiveActions.CorrectiveActTitle Like '%" + RemediationTB + "%'";
                i++;
                fields.Add("CorrectiveActions");
                //tables[2] = "CorrectiveActions";
            }
            if (AttachmentTB.Trim() != "")
            {
                likequery[i] = "Attachments.AttachmentTitle Like '%" + AttachmentTB + "' ";
                i++;
                fields.Add("Attachments");
                //tables[3] = "Attachments";
            }
            //call a function to make the array into a string with and in between each value based on i
            //i = likequery.Length;
            //minus one at present bc the length is for all items on search screen. 
            //date is not included in the code.
            //i = i - 1;
            query=addAndToArray(likequery,i);
            //based on tables use different search procedure?
            //if (fields.Count != 0)
            //{
            //    tables=fields.ToArray();
            //    string tablejoin = getTableJoins(tables);
            //    query = "Count is: " + fields.Count + " Select * from Incidents " + tablejoin + " where " + query;
            //}
            //else
            //{
                query = "Count is: " + i + " Select * from Incidents where " + query;
            //}

            //return query; 
            //lets pass the tables we are possibly comparing -- the support gridview tables

             return Incidents.SearchFields(query,EventsTB,CausesTB,RemediationTB,AttachmentTB);
        }
    }
    //Note to self
    //BLL
    //Try
    //{
    //crap;
    //if (something is not right == true){
    //throw new exception("OMG I GOT PWNED!"
    //}
    //}
}
