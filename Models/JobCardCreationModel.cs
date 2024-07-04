/*----------------------------------------------------------------------
Created By	: Kavya K
Created On	: 19/12/2022
Purpose		: Job Card Creation
-------------------------------------------------------------------------
Modification
On			By					OMID/Remarks
-------------------------------------------------------------------------
20-12-2022      Kavya K         Added Class 
-------------------------------------------------------------------------*/
using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PerfectWebERP.Models
{
    public class JobCardCreationModel
    {
        public class PeriodTypeList
        {
            public List<PeriodType> PeriodType { get; set; }
            public String JobCardNo { get; set; }
            public long? LastID { get; set; }
        }
      
        public class PeriodTypeMode
        {
            public Int32 Mode { get; set; }
        }

        public class inputJobCardNo
        {
            public long FK_Company { get; set; }
            public long FK_Branch { get; set; }
            public string Submodule { get; set; }
            public long FK_Type { get; set; }
            public DateTime TransDate { get; set; }
        }
        public class OutputJobCardNo
        {
            public string AccountNo { get; set; }
        }

        public class PeriodType
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public APIGetRecordsDynamic<PeriodType> GetPeriodTypeList(PeriodTypeMode input, string companyKey)
        {
            return Common.GetDataViaProcedure<PeriodType, PeriodTypeMode>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);
        }
    
        public class JobCardView
        {
            public Int64 SlNo { get; set; }
            public Int64 JobCardType { get; set; }
            public Int64 Priority { get; set; }          
            public long ID_JobCard { get; set; }
            public long FK_JobCard { get; set; }
            public DateTime StartDate { get; set; }         
            public DateTime? TargetDate { get; set; }
            public String JobCardNo { get; set; }
            public long FK_Master { get; set; }
            public string TransMode { get; set; }          
            public Int64 TotalCount { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string Name { get; set; }
            public List<JobCardDetailsView> JobCardDetails { get; set; }
            public string CardType { get; set; }
            public string Priorty { get; set; }
            public string ProductName { get; set; }
            public decimal Quantity { get; set; }
            public string ProjectName { get; set; }
            public string SoNo { get; set; }
            public Int64 PeriodType { get; set; }
            public long Period { get; set; }
            public long NoofCount { get; set; }
            public DateTime? TypeEndDate { get; set; }
            public long GroupID { get; set; }
            public long FK_Product { get; set; }
            public DateTime SODate { get; set; }
            public DateTime SODates { get; set; }
            public DateTime StartDates { get; set; }
            public long? LastID { get; set; }
            
        }

        public class JobCardDetailsView
        {
            public long ID_JobCardDetails { get; set; }
            public long FK_JobCard { get; set; }
            public long FK_Product { get; set; }
            public decimal Quantity { get; set; }
        }
       
        public class UpdateJobCard
        {
            public long ID_JobCard { get; set; }
            public long FK_JobCard { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime? TargetDate { get; set; }
            public Int64 JobCardType { get; set; }
            public Int64 Priority { get; set; }
            public String JobCardNo { get; set; }
            public long FK_Master { get; set; }
            public string TransMode { get; set; }
            public Int64 UserAction { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public Int64 Debug { get; set; }
            public string JobCardDetails { get; set; }
            public Int64 PeriodType { get; set; }
            public long Period { get; set; }
            public long NoofCount { get; set; }
            public DateTime? Date { get; set; }
            public long? LastID { get; set; }
        }

        public static string _deleteProcedureName = "ProJobCardDelete";
        public static string _updateProcedureName = "ProJobCardUpdate";
        public static string _selectProcedureName = "ProJobCardSelect";

        public class DeleteJobCard
        {
            public long GroupID { get; set; }
            public string TransMode { get; set; }
            public long Debug { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public Int64 FK_Reason { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public long FK_Company { get; set; }
        }
        public class GetJobCard
        {
            public long GroupID { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string Name { get; set; }
            public Int16 Detailed { get; set; }
        }
        public class NextOrder
        {
            public string TableName { get; set; }
            public string FieldName { get; set; }
            public byte Debug { get; set; }
        }
        public class NextOrderOutput
        {
            public int NextNo { get; set; }
        }
        public Output UpdateJobCardData(UpdateJobCard input, string companyKey)
        {
            return Common.UpdateTableData<UpdateJobCard>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteJobCardData(DeleteJobCard input, string companyKey)
        {
            return Common.UpdateTableData<DeleteJobCard>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<JobCardView> GetJobCardData(GetJobCard input, string companyKey)
        {
            return Common.GetDataViaProcedure<JobCardView, GetJobCard>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
        public APIGetRecordsDynamic<JobCardView> GetJobCardSelectDetails(GetJobCard input, string companyKey)
        {
            return Common.GetDataViaProcedure<JobCardView, GetJobCard>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
        public APIGetRecordsDynamic<JobCardView> GetJobCardItemDetailsSelect(GetJobCard input, string companyKey)
        {
            return Common.GetDataViaProcedure<JobCardView, GetJobCard>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
        public APIGetRecordsDynamic<OutputJobCardNo> GetJobCardNo(inputJobCardNo input, string companyKey)
        {
            //return Common.GetDataViaProcedure<OutputJobCardNo, inputJobCardNo>(companyKey: companyKey, procedureName: "ProGetJobCardNo", parameter: input);
            return Common.GetDataViaProcedure<OutputJobCardNo, inputJobCardNo>(companyKey: companyKey, procedureName: "ProLoadAccountNo", parameter: input);
        }
        //testing purpose
    }
}