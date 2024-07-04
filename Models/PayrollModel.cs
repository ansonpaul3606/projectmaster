using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class PayrollModel
    {
        public static string _deleteProcedureName = "ProMenuGroupDelete";
        public static string _updateProcedureName = "ProMenuGroupUpdate";
        public static string _selectProcedureName = "ProMenuGroupSelect";
        public class SalaryProcessNew
        {           
            public IEnumerable<Branchs> Branch { get; set; }
            public IEnumerable<DepartmentList> Department { get; set; }
        }
        public class Branchs
        {
            public int BranchID { get; set; }
            public string Branch { get; set; }
        }
        public class DepartmentList
        {
            public long DepartmentID { get; set; }
            public string Department { get; set; }

        }
        public class ModuleCriteria
        {
            public Int64 Mode { get; set; }
        }
        public class Module
        {
            public string ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class MenuGroupView
        {
            public int SlNo { get; set; }
            public long ID_MenuGroup { get; set; }
            public string MnuGrpName { get; set; }
            public string SubModule { get; set; }
            public bool MnuGrpVisible { get; set; }
            public Int64 SortOrder { get; set; }
            public string MnuGrpIcon { get; set; }
            public int TotalCount { get; set; }
        }

        public class UpdateMenuGroup
        {
            public int UserAction { get; set; }
            public int Debug { get; set; }
            public long ID_MenuGroup { get; set; }
            public string MnuGrpName { get; set; }
            public string SubModule { get; set; }
            public bool MnuGrpVisible { get; set; }
            public Int64 SortOrder { get; set; }
            public string MnuGrpIcon { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long FK_MenuGroup { get; set; }

        }
        public class DeleteMenuGroup
        {
            public long FK_MenuGroup { get; set; }
            public long FK_Reason { get; set; }
            public string EntrBy { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }



        }
        public class MenuGroupID
        {
            public Int64 ID_MenuGroup { get; set; }
        }
        public class GetSalaryData
        {           
            public DateTime ProcessDate { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Department { get; set; }
            public long FK_Employee { get; set; }
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public long FK_Company { get; set; }
            public int Mode { get; set; }
        }
        //new
        
        public class SetSalaryData
        {
            public long? ID_SalaryProcess { get; set; }
            public long ID_PaySlipTrans { get; set; }
            public string EmpCode { get; set; }
            public string Empname { get; set; }
            public long FK_Employee { get; set; }
            public DateTime? FromDate { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Department { get; set; }
            public long FK_EmployeeType { get; set; }
            public DateTime ProcessDate { get; set; }
            public DateTime? ToDate { get; set; }
            public DateTime? TransDate { get; set; }
            public long GroupID { get; set; }
            public decimal NetAmount { get; set; }

            public long? ErrCode { get; set; }
            public string ErrrMsg { get; set; }
        }
        public class SetSalaryDataDetails
        {
            public long? ID_SalaryProcessDetails { get; set; }
            public long? FK_SalaryProcess { get; set; }
            public long ID_PaySlipTransSub { get; set; }
            public long FK_ID_PaySlipTrans { get; set; }
            public long FK_AllowanceType { get; set; }
            public string TypeName { get; set; }
            public string ShortName { get; set; }
            public long ALWMode { get; set; }
            public long ALWType { get; set; }
            public long FK_Employee { get; set; }
            public decimal AlsDetAmount { get; set; }
           
        }
        //new end
        public class SetSalaryData1
        {
            public long ID_SalaryProcess { get; set; }
            public string EmpCode { get; set; }
            public string EmpFName { get; set; }
            public string Basic { get; set; }
            public string DA { get; set; }
            public string HRA { get; set; }
            public string OT { get; set; }
            public string OthersAllowance { get; set; }
            public string PF { get; set; }
            public string PeF { get; set; }
            public string ESI { get; set; }
            public string WF { get; set; }
            public string VPF { get; set; }
            public string LIC { get; set; }
            public string IncomeTax { get; set; }
            public string ProfessionalTax { get; set; }
            public string Insurance { get; set; }
            public string Advance { get; set; }
            public string OthersRecovery { get; set; }
            public string Leave { get; set; }
            public string NetAmount { get; set; }
            public long ErrCode { get; set; }
            public string ErrMsg { get; set; }

            public long FK_Branch { get; set; }
            public long FK_Department { get; set; }
        }
        public class InputSalaryData
        {           
            public int UserAction { get; set; }
            public long GroupID { get; set; }
            public long FK_Company { get; set; }
            public long FK_Department { get; set; }
            public long FK_Employee { get; set; }
            public long FK_Branch { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public long FK_Machine { get; set; }
            public List<SetSalaryData> SalaryProcessDetails { get; set; }
            public List<SetSalaryDataDetails> SalarySubdetails { get; set; }
            public string EntrBy { get; set; }
            public long FK_Reason { get; set; }
            public DateTime ProcessDate { get; set; }
        }
        public class UpdateSalaryData
        {
            public int UserAction { get; set; }
            public long GroupID { get; set; }
            public long FK_Company { get; set; }
            public long FK_Department { get; set; }
            public long FK_Employee { get; set; }
            
            public long FK_Branch { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public long FK_Machine { get; set; }
            public string SalaryProcessDetails { get; set; }
            public string SalarySubdetails { get; set; }
            public string EntrBy { get; set; }
            public long FK_Reason { get; set; }
            public DateTime ProcessDate { get; set; }
        }
        public class GetSalaryProcessData
        {
            public DateTime? FromDate { get; set; }
            public DateTime? ToDate { get; set; }
            public Int64 PageIndex { get; set; }
            public Int64 PageSize { get; set; }
            public long? FK_Branch { get; set; }
            public long? FK_Department { get; set; }
            public long? FK_Employee { get; set; }
            public string Name { get; set; }
            public int Mode { get; set; }
            public long FK_SalaryProcess { get; set; }
        }
        public class SalaryProcessDataInfo
        {
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public Int64 pageIndex { get; set; }
            public Int64 pageSize { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Department { get; set; }
            public long FK_Employee { get; set; }
            public string Name { get; set; }
            public int Mode { get; set; }
            public long FK_SalaryProcess { get; set; }
        }
        public class SetSalaryDataList
        {           
            public long SlNo { get; set; }
            public DateTime ProcessedDate { get; set; }
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public string Branch { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Employee { get; set; }
            public string EmployeeName { get; set; }
            //public string EmployeeName { get; set; }
            public string Department { get; set; }
            public long FK_Department { get; set; }
            public long GroupID { get; set; }
            public Int64 TotalCount { get; set; }
        }
        public class GetSalaryProcessedDetails
        {
            public long ID_SalaryProcess { get; set; }
            public string EmpCode { get; set; }
            public string EmpFName { get; set; }
            public string Basic { get; set; }
            public string DA { get; set; }
            public string HRA { get; set; }
            public string OT { get; set; }
            public string OthersAllowance { get; set; }
            public string PF { get; set; }
            public string PeF { get; set; }
            public string ESI { get; set; }
            public string WF { get; set; }
            public string VPF { get; set; }
            public string LIC { get; set; }
            public string IncomeTax { get; set; }
            public string ProfessionalTax { get; set; }
            public string Insurance { get; set; }
            public string Advance { get; set; }
            public string OthersRecovery { get; set; }
            public string Leave { get; set; }
            public string NetAmount { get; set; }
            public long ErrCode { get; set; }
            public string ErrMsg { get; set; }

            public DateTime ProcessDate { get; set; }
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }

            //public string ToDateString { get {
            //        return this.ToDate.ToString("yyyy-MM-dd");
            //    } }
            public long FK_Branch { get; set; }
            public long FK_Department { get; set; }

        }
        public APIGetRecordsDynamic<Module> GeMenuGroupModuleData(ModuleCriteria input, string companyKey)
        {
            return Common.GetDataViaProcedure<Module, ModuleCriteria>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);
        }
        public Output UpdateMenuGroupData(UpdateMenuGroup input, string companyKey)
        {
            return Common.UpdateTableData<UpdateMenuGroup>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteMenuGroupData(DeleteMenuGroup input, string companyKey)
        {
            return Common.UpdateTableData<DeleteMenuGroup>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }  
        //////
        public APIGetRecordsDynamic<SetSalaryData> GetSalaryDetailsData(GetSalaryData input, string companyKey)
        {
            return Common.GetDataViaProcedure<SetSalaryData, GetSalaryData>(companyKey: companyKey, procedureName: "ProGetSalaryProcess", parameter: input);
        }
        public APIGetRecordsDynamic<SetSalaryDataDetails> GetSalaryDetails(GetSalaryData input, string companyKey)
        {
            return Common.GetDataViaProcedure<SetSalaryDataDetails, GetSalaryData>(companyKey: companyKey, procedureName: "ProGetSalaryProcess", parameter: input);
        }
        //////
        public Output UpdateSalaryInfo(UpdateSalaryData input, string companyKey)
        {
            return Common.UpdateTableData<UpdateSalaryData>(parameter: input, companyKey: companyKey, procedureName: "ProUpdateSalaryProcess");
        }
        public APIGetRecordsDynamic<SetSalaryDataList> GetSalaryProcessSelect(GetSalaryProcessData input, string companyKey)
        {
            return Common.GetDataViaProcedure<SetSalaryDataList, GetSalaryProcessData>(companyKey: companyKey, procedureName: "ProSelectSalaryProcess", parameter: input);
        }
        public APIGetRecordsDynamic<SetSalaryData> GetSalaryProcessSelectById(GetSalaryProcessData input, string companyKey)
        {
            return Common.GetDataViaProcedure<SetSalaryData, GetSalaryProcessData>(companyKey: companyKey, procedureName: "ProSelectSalaryProcess", parameter: input);
        }
        public APIGetRecordsDynamic<SetSalaryDataDetails> GetSalaryProcessSelectByMode2(GetSalaryProcessData input, string companyKey)
        {
            return Common.GetDataViaProcedure<SetSalaryDataDetails, GetSalaryProcessData>(companyKey: companyKey, procedureName: "ProSelectSalaryProcess", parameter: input);
        }
    }
}