using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace NCRClass
{
    public class NCRController
    {

        #region Detail Records
        public static int AddDetailRecord(string DRreportNumber,string DRReportType,int customerId,string rcompany,string rcustomercontact ,string rcustomerphone,string rContactFax,string rCustomerEmail,
                    int operatorId, string rOperatorName,string rOperatorPhone ,string rOperatorFax,string rOperatorEmail,string rOperatorExt,
                    int engineerId,string rInitiatorName ,string rInitiatorPhone ,string rInitiatorExt ,string rInitiatorFax,string rInitiatorEmail,
                    int locationId,string rDistrict,string rManagerName,string rmanagerAddress,string rLocCityAddress,string rProvState,
                    DateTime DRDateTB,string DRtitle ,string DRfieldticket,string DRwellname,int WellTypeId,string DRLSDTB,string DRSectionTB,   
                    string DRTownshipTB,string DRRangeTB ,string DRMeridianTB ,int CasingSizeId,int LinerSizeId,
                    string DRTMDTB,string DRTVDTB,string unitmeasure,int SystemTypeId,int ToolListId,int problemId,int parentId,int subcatid,string Notes,
                    string DRExecSummary,string DRDescription,string DRObservations,string DRnotes2,string DRPrefaceTA ,string DRCauseAnalysisTA)
        {
        return Incidents.AddDetailRecord(DRreportNumber,DRReportType,
                    customerId,rcompany, rcustomercontact ,rcustomerphone,rContactFax, rCustomerEmail,
                    operatorId, rOperatorName, rOperatorPhone, rOperatorFax, rOperatorEmail, rOperatorExt,
                    engineerId, rInitiatorName ,rInitiatorPhone ,rInitiatorExt ,rInitiatorFax ,rInitiatorEmail,
                    locationId,rDistrict, rManagerName,rmanagerAddress,rLocCityAddress,rProvState,
                    DRDateTB, DRtitle ,DRfieldticket,DRwellname,WellTypeId,DRLSDTB,DRSectionTB,   
                    DRTownshipTB, DRRangeTB,DRMeridianTB,CasingSizeId,LinerSizeId,
                    DRTMDTB, DRTVDTB,unitmeasure,SystemTypeId,ToolListId,problemId,parentId,subcatid,Notes,
                    DRExecSummary,DRDescription,DRObservations,DRnotes2,DRPrefaceTA ,DRCauseAnalysisTA);

        }
        public static void UpdateDetailRecord(int reportId, string DRreportNumber, string DRReportType, int customerId, string rcompany, string rcustomercontact, string rcustomerphone, string rContactFax, string rCustomerEmail, int operatorId, string rOperatorName, string rOperatorPhone, string rOperatorFax, string rOperatorEmail, string rOperatorExt,
             int engineerId, string rInitiatorName, string rInitiatorPhone, string rInitiatorExt, string rInitiatorFax, string rInitiatorEmail,
             int locationId, string rDistrict, string rManagerName, string rmanagerAddress, string rLocCityAddress, string rProvState,
             DateTime ReportDate, string DRtitle, string DRfieldticket, string DRwellname, int WellTypeId, string DRLSDTB, string DRSectionTB,
             string DRTownshipTB, string DRRangeTB, string DRMeridianTB, int CasingSizeId, int LinerSizeId,
             string DRTMDTB, string DRTVDTB, string unitmeasure, int SystemTypeId, int ToolListId, int problemId, int parentId, int subcatid, string Notes,
             string DRExecSummary, string DRDescription, string DRObservations, string DRnotes2, string DRPrefaceTA, string DRCauseAnalysisTA)
        {
            Incidents.UpdateDetailRecord(reportId,DRreportNumber, DRReportType,
                    customerId, rcompany, rcustomercontact, rcustomerphone, rContactFax, rCustomerEmail,
                    operatorId, rOperatorName, rOperatorPhone, rOperatorFax, rOperatorEmail, rOperatorExt,
                    engineerId, rInitiatorName, rInitiatorPhone, rInitiatorExt, rInitiatorFax, rInitiatorEmail,
                    locationId, rDistrict, rManagerName, rmanagerAddress, rLocCityAddress, rProvState,
                    ReportDate, DRtitle, DRfieldticket, DRwellname, WellTypeId, DRLSDTB, DRSectionTB,
                    DRTownshipTB, DRRangeTB, DRMeridianTB, CasingSizeId, LinerSizeId,
                    DRTMDTB, DRTVDTB, unitmeasure, SystemTypeId, ToolListId, problemId, parentId, subcatid, Notes,
                    DRExecSummary, DRDescription, DRObservations, DRnotes2, DRPrefaceTA, DRCauseAnalysisTA);
        }
        public static void DeleteDetailReport(int reportId,string reportNumber)
        {
            BLClass.DeleteDetailReport(reportId,reportNumber);
        }
    public static DataSet GetDetailsById(int reportId)
    {
        return Incidents.GetDetailsById(reportId);
    }
    public static DataSet GetAllDetailReports(int pageIndex, int PageSize, int numTotalDetails)
    {
        return Incidents.GetAllDetailReports(pageIndex, PageSize, numTotalDetails);
    }
    public static DataSet GetAllDetailReports()
    {
        return Incidents.GetAllDetailReports();
    }
        #endregion

    #region Support Tables
        #region Customers
    //----------------------------------  Customers -----------------------------  
        public static DataSet LookupAllCustomers()
        {
            return Customers.LookupAllCustomers();
        }
        public static DataSet LookupCustomer(int customerId)
        {
            return Customers.LookupCustomer(customerId);
        }
        public static DataSet LookupCustomerByCustomerName(string CustomerName)
        {
            return Customers.LookupCustomerByCustomerName(CustomerName);

        }
        public static int AddCustomer(string company, string contactname, string contactphone, string contactext, string contactfax, string Email)
        {
            int customerId = Customers.AddCustomer(company, contactname, contactphone, contactext, contactfax, Email);
            return customerId;
        }
        public static void UpdateCustomer(int customerId, string company, string contactname, string contactphone, string contactext, string contactfax, string Email)
        {
            Customers.UpdateCustomer(customerId, company, contactname, contactphone, contactext, contactfax, Email);
           
        }
        public static bool DeleteCustomer(int customerId)
        {
            bool customerDelete;
            customerDelete = Customers.DeleteCustomer(customerId);
            return customerDelete;
        }
        #endregion


        #region Engineers
        //----------------------------------  Engineers -----------------------------  

        public static DataSet LookupAllEngineers()
        {
            return Engineers.LookupAllEngineers();
        }
        public static DataSet LookupEngineer(int engineerId)
        {
            return Engineers.LookupEngineer(engineerId);
        }
        public static int AddEngineer(string EngineerName, string EngineerPhone, string EngineerExt, string EngineerFax, string EngineerEmail)
        {
             int engineerId =Engineers.AddEngineer( EngineerName,  EngineerPhone,  EngineerExt,  EngineerFax, EngineerEmail);
             return engineerId;
        }
        public static void UpdateEngineer(int EngineerId, string EngineerName, string EngineerPhone, string EngineerExt, string EngineerFax, string EngineerEmail)
        {
            Engineers.UpdateEngineer( EngineerId,  EngineerName,  EngineerPhone,  EngineerExt,  EngineerFax,  EngineerEmail);
        }
        public static bool DeleteEngineer(int EngineerId)
        {
            bool engineerDel;
            engineerDel = Engineers.DeleteEngineer(EngineerId);
            return engineerDel;
        }
        #endregion

        #region Locations
        //----------------------------------  Locations -----------------------------  
        public static DataSet LookupAllLocations()
        {
            return Locations.LookupAllLocations();
        }
        public static DataSet LookupLocation(int locationId)
        {
            return Locations.LookupLocation(locationId);
        }

        public static int AddLocation(string district, string mgrName, string address, string city, string provstate)
        {
            int locationId = Locations.AddLocation(district, mgrName, address, city, provstate);
            return locationId;
        }
        public static void UpdateLocation(int locationId, string district, string mgrName, string address, string city, string provstate)
        {
            Locations.UpdateLocation(locationId,  district,  mgrName,  address,  city,  provstate);
        }
        public static bool DeleteLocation(int locationId)
        {
            bool deleteId = Locations.DeleteLocation(locationId);
            return deleteId;
        }
        #endregion


        #region Operators
        //----------------------------------  Operators  -----------------------------  
        public static DataSet LookupAllOperators()
        {
            return Operators.LookupAllOperators();
        }
        public static DataSet LookupOperator(int operatorId)
        {
            return Operators.LookupOperator(operatorId);
        }
        public static int AddOperator(string operatorName, string @operatorPhone, string operatorExt, string @operatorFax, string operatorEmail)
        {
            int operatorId;
            operatorId = Operators.AddOperator( operatorName,  @operatorPhone,  operatorExt,  @operatorFax,  operatorEmail);
            return operatorId;
        }
        public static void UpdateOperator(int operatorId, string operatorName, string @operatorPhone, string operatorExt, string @operatorFax, string operatorEmail)
        {
            Operators.UpdateOperator( operatorId,  operatorName,  @operatorPhone,  operatorExt,  @operatorFax,  operatorEmail);
        }
        public static bool DeleteOperator(int operatorId)
        {
            bool deleteid = Operators.DeleteOperator(operatorId);
            return deleteid;
        }
        #endregion

        #region Categories
        //----------------------------------  Categories   -----------------------------  

        public static DataSet LookupAllCategoriesByParentId(int parentId)
        {
            return Categories.LookupAllCategoriesByParentId(parentId);
        }
        public static DataSet LookupCategoryName(int parentId)
        {
            return Categories.LookupCategoryName(parentId);
        }
        public static int AddCategory(int parentid, string Name)
        {
            return Categories.AddCategory(parentid, Name);
        }
        public static void UpdateCategory(int Id, int parentId, string Name)
        {
            Categories.UpdateCategory(Id,parentId,Name);
        }
        public static bool DeleteCategory(int Id)
        {
            bool deleteid = Categories.DeleteCategory(Id);
            return deleteid ;
        }
        #endregion

        #region ProblemOccurances
        ////----------------------------------  Problem Occurances  -----------------------------  
        public static DataSet LookupAllProblems()
        {
            return ProblemOccurances.LookupAllProblems();
        }
        public static DataSet LookupProblem(int problemId)
        {
            return ProblemOccurances.LookupProblem(problemId);
        }
        public static void UpdateProblemOccurance(int Id, string Name, string Def)
        {
            ProblemOccurances.UpdateProblemOccurance(Id, Name, Def);
        }
        public static int AddProblemOccurance(string Name, string Def)
        {
            return ProblemOccurances.AddProblemOccurance(Name, Def);
        }
        public static bool DeleteProblemOccurance(int problemId)
        {
            bool deleteid = ProblemOccurances.DeleteProblemOccurance(problemId);
            return deleteid;
        }

        #endregion

        #region Miscellaneous Classes for dropdowns
        //----------------------------------  Misc Class  -----------------------------  

        #region WellTypes
        public static DataSet LookupAllWellTypes()
        {
            return Misc.LookupAllWellTypes();
        }
        public static DataSet LookupWellType(int welltypeId)
        {
            return Misc.LookupWellType(welltypeId);
        }
        public static int AddWellType(string Name)
        {
            return Misc.AddWellType(Name);
        }
        public static void UpdateWellType(int Id, string Name)
        {
            Misc.UpdateWellType(Id,Name);
        }

        public static bool DeleteWellType(int WellTypeId)
        {
            bool deleteid = Misc.DeleteWellType(WellTypeId);
            return deleteid;
        }
        #endregion

        #region CasingSize
        //----------------------  CasingSizes  ------------------
        public static DataSet LookupAllCasingSizes()
        {
            return Misc.LookupAllCasingSizes();
        }
        public static DataSet LookupCasingSize(int casingSizeId)
        {
            return Misc.LookupCasingSize(casingSizeId);
        }
        public static int AddCasingSize(string Name)
        {
            return Misc.AddCasingSize(Name);
        }
        public static void UpdateCasingSize(int Id, string Name)
        {
            Misc.UpdateCasingSize(Id,Name);
        }
        public static bool DeleteCasingSize(int CasingId)
        {
            bool deleteid = Misc.DeleteCasingSize(CasingId);
            return deleteid;
        }
        #endregion

        #region LinerSizes
        //----------------------  LinerSizes  ------------------
        public static DataSet LookupAllLinerSizes()
        {
            return Misc.LookupAllLinerSizes();
        }
        public static DataSet LookupLinerSize(int linerSizeId)
        {
            return Misc.LookupLinerSize(linerSizeId);
        }
        public static int AddLinerSize(string Name)
        {
            return Misc.AddLinerSize(Name);
        }
        public static void UpdateLinerSize(int Id, string Name)
        {
            Misc.UpdateLinerSize(Id,Name);
        }
        public static bool DeleteLinerSize(int LinerId)
        {
            bool deleteid = Misc.DeleteLinerSize(LinerId);
            return deleteid;
        }
        #endregion

        #region SystemTypes
        //----------------------  SystemTypes  ------------------
        public static DataSet LookupAllSystemTypes()
        {
            return Misc.LookupAllSystemTypes();
        }
        public static DataSet LookupSystemType(int SystemTypeId)
        {
            return Misc.LookupSystemType(SystemTypeId);
        }
        public static int AddSystemType(string Name)
        {
            return Misc.AddSystemType(Name);
        }
        public static void UpdateSystemType(int Id, string Name)
        {
            Misc.UpdateSystemType(Id,Name);
        }
        public static bool DeleteSystemType(int SystemTypeId)
        {
            bool deleteid = Misc.DeleteSystemType(SystemTypeId);
            return deleteid;
        }
        #endregion 

        #region ToolLists
        //----------------------  ToolLists  ------------------
        public static DataSet LookupAllToolLists()
        {
            return Misc.LookupAllToolLists();
        }
        public static DataSet LookupToolList(int ToolListId)
        {
            return Misc.LookupToolList(ToolListId);
        }
        public static int AddToolList(string Name)
        {
            return Misc.AddToolList(Name);
        }
        public static void UpdateToolList(int Id, string Name)
        {
            Misc.UpdateToolList(Id,Name);
        }
        public static bool DeleteToolList(int ToolListId)
        {
            bool deleteid = Misc.DeleteToolList(ToolListId);
            return deleteid;
        }
        #endregion 

        #endregion

    #endregion


        #region GridviewHandlings
        //----------------------  GridViews Detail Records  ------------------
        #region Events
        //----------------------  Events  ------------------

        public static DataSet getEventsByReportID(int reportId)
        {
            return Events.LookupEvents(reportId);   
        }
        public static DataSet getEventByEventId(int Id)
        {
            return Events.LookupEventByEventId(Id);   
        }
        public static DataSet GetEventDDLById(int Id)
        {
            return Events.GetEventDDLById(Id);
        }
        public static int AddEvent(int rid, string title, string content, string edate)
        {
            return Events.AddEvent(rid, title, content, edate);

        }
        public static void UpdateEvent(int id, int rid, string title, string content, string edate)
        {
            Events.UpdateEvent( id,  rid,  title,  content, edate);
        }
        public static bool DeleteEvent(int eventId)
        {
            return Events.DeleteEvent(eventId);

        }
        #endregion 
        #region Causes
        //----------------------  Causes  ------------------

        public static DataSet getCausesByReportId(int reportId)
        {
            return Causes.LookupCauses(reportId);
        }
        public static DataSet LookupCauseDDLById(int Id)
        {
            return Causes.LookupCauseDDLById(Id);
        }
        public static int AddCause(int rid, string title, string content)
        {
            return Causes.AddCause(rid, title, content);
        }
        public static void UpdateCause(int id, int rid, string title, string content)
        {
            Causes.UpdateCause(id, rid, title, content);
        }
        public static void DeleteCause(int causeId)
        {
            Causes.DeleteCause(causeId);
        }
        #endregion

        #region Corrections
        //----------------------  Corrections  ------------------
        public static DataSet getCorrectionsByReportId(int reportId)
        {
            return Corrections.LookupCorrections(reportId);
        }
        public static DataSet LookupCorrectionByActionId(int Id)
        {
            return Corrections.LookupCorrectionByActionId(Id);
        }
        public static DataSet GetCorrectiveActionsDDLById(int Id)
        {
            return Corrections.GetCorrectiveActionsDDLById(Id);
        }
        public static int AddCorrection(int rid, string title, string content)
        {
            return Corrections.AddCorrection(rid,title,content);
        }
        public static void UpdateCorrection(int id, int rid, string title, string content)
        {
            Corrections.UpdateCorrection(id, rid, title, content);
        }
        public static void DeleteCorrection(int correctionId)
        {
            Corrections.DeleteCorrection(correctionId);
        }
        #endregion

        #region Attachments
        //---------------------- Attachments  ------------------
        public static DataSet getAttachmentsByReportId(int reportId)
        {
            return Attachments.LookupAttachments(reportId);
        }
        public static DataSet GetAttachementDDLById(int Id)
        {
            return Attachments.GetAttachementDDLById(Id);
        }
        public static int AddAttachment(int rid, string title, string filename, string source, string content)
        {
            return Attachments.AddAttachment(rid, title, filename, source, content);
        }
        public static void UpdateAttachment(int id, int rid, string title, string filename, string source, string content)
        {
            Attachments.UpdateAttachment(id, rid, title, filename, source, content);
        }
        public static void DeleteAttachment(int attachmentId)
        {
            Attachments.DeleteAttachment(attachmentId);
        }

        #endregion

        #region ReportNumbering
        //---------------------- Initialize ReportNumbering  ------------------
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sub"></param>
        /// <returns></returns>
        public static int AddReportNumbers(string sub)
        {
            return ReportNumbers.AddReportNumbers(sub);
        }
        //do I need this?
        public static DataSet GetFirstReport()
        {
            return Incidents.GetFirstReport();
        }
        public static string GetNextReportId()
        {
            return BLClass.GetNextReportId();
        }
        public static bool ReportNumberUsed(string ReportNumber)
        {
            return BLClass.ReportNumberUsed(ReportNumber);
        }
        #endregion
        #endregion


        #region Search DataSet
        public static DataSet SearchFields(int customerId, int operatorid, int engineerid, int locationid, string ReportType, string SearchDateTB, string DRtitle, string DRfieldticket, string DRwellname, int WellTypeDDL, string DRLSDTB, string DRSectionTB, string DRTownshipTB, string DRRangeTB, string DRMeridianTB, int CasingSizeDDL, int LinerSizeDDL, string DRTVDTB, string DRTMDTB, int ToolListDDL, int SystemTypeDDL, string DRExecSummary, string DRDescription, string DRObservations, string Notes, string Notes2, int ProbOccurDDL, int CategoryDDL, int SubCategoryDDL, string DRPrefaceTA, string EventsTB, string AttachmentTB, string CausesTB, string DRCauseAnalysisTA, string RemediationTB)
        {
            return BLClass.SearchFields( customerId, operatorid, engineerid, locationid,  ReportType, SearchDateTB,DRtitle,DRfieldticket, DRwellname,  WellTypeDDL, DRLSDTB, DRSectionTB, DRTownshipTB, DRRangeTB, DRMeridianTB, CasingSizeDDL, LinerSizeDDL, DRTVDTB, DRTMDTB, ToolListDDL, SystemTypeDDL,  DRExecSummary, DRDescription, DRObservations, Notes, Notes2, ProbOccurDDL, CategoryDDL, SubCategoryDDL, DRPrefaceTA, EventsTB, AttachmentTB, CausesTB, DRCauseAnalysisTA, RemediationTB);
        
        }
        #endregion
    }
}


//note to self:
//Presentation layer
//Try
//{
//crap
//}
//catch (Exception ex)
//{
//lblMessages.text = ex.message()
//}


// DAL
//Try{
//crap
//myconn.open()
//do stuff
//}
//Finally{
//myconn.close()
//}

//return something
