using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace PerfectWebERP.Models
{
    public class IncentiveProcessModel
    {

        public class IncentiveProcessViewlist
        {
            public List<Branchs> BranchList { get; set; }
            public List<Department> deprtmnt { get; set; }
            public List<Incentivetype> IncentivetypeList { get; set; }
            public List<IncentiveProcessDetailsView> IncentiveProcessDetailsView { get; set; }

            public List<GetEditincentiveoutprocessview> GetEditincentiveoutprocessview { get; set; }
          //  public List<EditPopupUpdatelists> EditPopupUpdatelist { get; set; }
            public List <IncentiveProcessDetailsList> IncentiveProcessDetailsList { get; set; }
            public string EmpName { get; set; }
            public string EmpDepartmentName { get; set; }
            public string EmpBranchName { get; set; }
            public long ID_IncentiveProcess { get; set; }

        }
        public class Branchs
        {
            public int BranchID { get; set; }
            public string Branch { get; set; }

        }
       
        public class Department
        {
            public int DepartmentID { get; set; }
            public string DepartmentName { get; set; }

        }
        public class Incentivetype
        {
            public Int32 IncentiveTypeID { get; set; }
            public string IncentiveType { get; set; }

        }
        public class Inputvalueforprocessview
        {
            public int UserAction { get; set; }
            public Int64? FK_IncentiveType { get; set; } = 0;
            public DateTime? FromDate { get; set; }
            public DateTime? ToDate { get; set; }
            public DateTime? ProcessDate { get; set; }
            public long? FK_Department { get; set; } = 0;
            public long? FK_Branch { get; set; } = 0;
            public long? FK_Employee { get; set; } = 0;
            public long FK_BranchCodeUser { get; set; }
            public long? GroupID { get; set; } = 0;
            public List<EditPopupUpdatelists> EditPopupUpdatelist { get; set; }
            public List<IncentiveProcessDetailsList> IncentiveProcessDetailsList { get; set; }
            public string TransMode { get; set; }
            public int Clear { get; set; }
        }
        public class Inputvalueforprocess
        {
            public int UserAction { get; set; }
            public Int64? FK_IncentiveType { get; set; } = 0;
            public DateTime ?FromDate { get; set; }
            public DateTime ?ToDate { get; set; }
            public DateTime ?ProcessDate { get; set; }
            public long? FK_Department { get; set; } = 0;
            public long? FK_Branch { get; set; } = 0;
            public long? FK_Employee { get; set; } = 0;
            public long FK_BranchCodeUser { get; set; }
            public long? GroupID { get; set; } = 0;
            public List<EditPopupUpdatelists> EditPopupUpdatelist { get; set; }
            public List<IncentiveProcessDetailsList> IncentiveProcessDetailsList { get; set; }
            public string TransMode { get; set; }

        }
        public class GetInputvalueforprocess
        {
            public long ?IPWCGroupID { get; set; }
            public long ?Mode { get; set; }
            public Int64 ?FK_IncentiveType { get; set; }
            public DateTime ?FromDate { get; set; }
            public DateTime ?ToDate { get; set; }
            public DateTime ?ProcessDate { get; set; }
            public Int64 ?FK_Department { get; set; }
            public Int64 ?FK_Branch { get; set; }
            public Int64 ?FK_Employee { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EditPopupUpdatelist { get; set; }
            //public long TotalCount { get; set; }
            //public long SlNo { get; set; }
            public string EntrBy { get; set; }
            public int Clear { get; set; }
        }
        public class GetInputvalueforprocessview
        {
            public long SlNo { get; set; }
            public long? IPWCGroupID { get; set; }
            public long? Mode { get; set; }
            public Int64? FK_IncentiveType { get; set; }
            public DateTime? FromDate { get; set; }
            public DateTime? ToDate { get; set; }
            public DateTime? ProcessDate { get; set; }
            public Int64? FK_Department { get; set; }
            public Int64? FK_Branch { get; set; }
            public Int64? FK_Employee { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EditPopupUpdatelist { get; set; }
            public string IncentiveType { get; set; }
            public long ID_IncentiveProcess { get; set; }
            public string Employee { get; set; }
            public string Department { get; set; }
            public long TotalCount { get; set; }
           
        }
        public class IncentiveProcessDetailsView
        {
            public long? ID_IncentiveProcessDetails { get; set; }
            public Int64 ?SlNo { get; set; }
            public long ?EmployeeID { get; set; }
            public string Employee { get; set; }
            public int ?BranchID { get; set; }
            public string Branch { get; set; }
            public int ?DepartmentID { get; set; }
            public string DepartmentName { get; set; }
            public string LastIncentiveDate { get; set; }
            public decimal ActualIncentive { get; set; }

            public decimal Payable { get; set; }
            public Int64 ?GroupID { get; set; }
            public Int64 Clear { get; set; } 
            public Int64 ErrCode { get; set; }
            public string ErrMsg { get; set; }
        }


        public class GetEditIncentiveprocessInput
        {
            public Int64 FK_Employee { get; set; }
            public Int64 FK_Branch { get; set; }
            public Int64 FK_Department { get; set; }
            public long FK_Company { get; set; }
        }
        public class GetEditincentiveoutprocessview
        {
            public Int64 SlNo { get; set; }
            public string Module { get; set; }
            public string AccountNo { get; set; }
            public string Activity { get; set; }
            public string  Value { get; set; }  
            public string  CalculationMethod { get; set; }
            public decimal ActualIncentive { get; set; }
            public decimal WorkSheetPayable { get; set; }
            public Int64 ID_IncentiveProcessWorkSheetCalc { get; set; }
         

        }

            public APIGetRecordsDynamic<IncentiveProcessDetailsView> GetInputvalueforIncentiveprocessdetails(GetInputvalueforprocess input, string companyKey)
        {
            return Common.GetDataViaProcedure<IncentiveProcessDetailsView, GetInputvalueforprocess>(companyKey: companyKey, procedureName: "ProIncentiveProcess", parameter: input);

        }

        public APIGetRecordsDynamic<GetEditincentiveoutprocessview> GetInputvalueforIncentiveprocessedit(GetInputvalueforprocess input, string companyKey)
        {
            return Common.GetDataViaProcedure<GetEditincentiveoutprocessview, GetInputvalueforprocess>(companyKey: companyKey, procedureName: "ProIncentiveProcess", parameter: input);

        }
        public class EditPopupUpdatelists
        {
            public Int64 ID_IncentiveProcessWorkSheetCalc { get; set; }
            public decimal Payable { get; set; }

        }
      public class UpdateIncentiveProcessData
        {
            public int UserAction { get; set; }
            public long ID_IncentiveProcess { get; set; }
            public Int64 FK_IncentiveType { get; set; }
            public DateTime? FromDate { get; set; }
            public DateTime? ToDate { get; set; }
            public DateTime? ProcessDate { get; set; }
            public long FK_Department { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Employee { get; set; } 
          //  public long GroupID { get; set; } 
            public string IncentiveProcessDetailsList { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get;set; }
            public string TransMode { get; set; }

        }
        public class IncentiveProcessDetailsList
        {
            
            public long FK_Employee { get; set; }
          
            public int FK_Branch { get; set; }
          
            public int FK_Department { get; set; }
          
            public string LastIncentiveDate { get; set; }
            public decimal ActualIncentive { get; set; }

            public decimal Payable { get; set; }
           public Int64 GroupID { get; set; }
            public bool Direct { get; set; }

        }
      
        public Output UpdateEditpopupData(GetInputvalueforprocess input, string companyKey)
        {
            return Common.UpdateTableData<GetInputvalueforprocess>(parameter: input, companyKey: companyKey, procedureName: "ProIncentiveProcess");
        }
        public Output UpdateIncentiveProcessInfo(UpdateIncentiveProcessData input, string companyKey)
        {
            return Common.UpdateTableData<UpdateIncentiveProcessData>(parameter: input, companyKey: companyKey, procedureName: "ProUpdateIncentiveProcess");
        }
        public class GetIncentiveProcess
        {
            public Int64 FK_IncentiveProcess { get; set; }
            public Int64 FK_Company { get; set; }
            public string TransMode { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }

            public int Mode { get; set; }
            public string Name { get; set; }
        }
        public APIGetRecordsDynamic<GetInputvalueforprocessview> GetIncentiveProcessListData(GetIncentiveProcess input, string companyKey)
        {
            return Common.GetDataViaProcedure<GetInputvalueforprocessview, GetIncentiveProcess>(companyKey: companyKey, procedureName: "ProIncentiveProcessSelect", parameter: input);

        }
        public class IncentiveProcessInfoView
        {
            [Required(ErrorMessage = "No Incentive Process Selected")]
            public Int64 IncentiveProcessID { get; set; }
            [Required(ErrorMessage = "Please Select The Reason")]
            public Int64 ReasonID { get; set; }
            public string TransMode { get; set; }
        }
        public class DeleteIncentiveProcess
        {
            public long FK_IncentiveProcess { get; set; }
            public string TransMode { get; set; }
            public long FK_Reason { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }

        }
        public Output DeleteIncentiveProcessData(DeleteIncentiveProcess input, string companyKey)
        {
            return Common.UpdateTableData<DeleteIncentiveProcess>(parameter: input, companyKey: companyKey, procedureName: "ProIncentiveProcessDelete");
        }
        public class IncentiveProcessDetailsViews
        {
            public long? ID_IncentiveProcessDetails { get; set; }
            public Int64 SlNo { get; set; }
            public long EmployeeID { get; set; }
            public string Employee { get; set; }
            public int BranchID { get; set; }
            public string Branch { get; set; }
            public int DepartmentID { get; set; }
            public string Department { get; set; }
            public string LastIncentiveDate { get; set; }
            public decimal ActualIncentive { get; set; }

            public decimal Payable { get; set; }
            public Int64 GroupID { get; set; }
            public bool Direct { get; set; }

        }
        public APIGetRecordsDynamic<IncentiveProcessDetailsViews> GetIncentiveProcessListDatas(GetIncentiveProcess input, string companyKey)
        {
            return Common.GetDataViaProcedure<IncentiveProcessDetailsViews, GetIncentiveProcess>(companyKey: companyKey, procedureName: "ProIncentiveProcessSelect", parameter: input);

        }

        public class GetEditincentiveoutprocessviews
        {
            public Int64 SlNo { get; set; }
            public string Module { get; set; }
            public string AccountNo { get; set; }
            public string Activity { get; set; }
            public string Value { get; set; }
            public string CalculationMethod { get; set; }
            public string ActualIncentive { get; set; }
            public string Payable { get; set; }
          


        }
        public APIGetRecordsDynamic<GetEditincentiveoutprocessviews> GetIncentiveProcesspopListDatas(GetIncentiveProcess input, string companyKey)
        {
            return Common.GetDataViaProcedure<GetEditincentiveoutprocessviews, GetIncentiveProcess>(companyKey: companyKey, procedureName: "ProIncentiveProcessSelect", parameter: input);

        }
    }
}