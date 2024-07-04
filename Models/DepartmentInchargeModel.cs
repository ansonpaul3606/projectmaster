using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class DepartmentInchargeModel
    {
        public class DepartmentInchargeView
        {
            public long FK_Branch { get; set; }
            public long FK_Department { get; set; }
            public long FK_Incharge { get; set; }
            public string Remarks { get; set; }
            public string TransMode { get; set; }
            public long ID_DepartmentIncharge { get; set; }
            public DateTime EffectDate { get; set; }
            public List<Branch> Branchlists { get; set; }
            public List<Department> deptlists { get; set; }
        }
        public class Branch
        {
            public long BranchID { get; set; }
            public string BranchName { get; set; }
        }
        public class Department
        {
            public long DeptID { get; set; }
            public string DeptName { get; set; }
        }
        public class UpdateDepIncharge
        {
            public Int32 UserAction { get; set; }
            public Int32 Debug { get; set; }
            public DateTime EffectDate { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Department { get; set; }
            public long FK_Incharge { get; set; }
            public string Remarks { get; set; }
            public long ID_DepartmentIncharge { get; set; }
            public long FK_DepartmentIncharge { get; set; }
            public string TransMode { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
        }
        public class DIViewInput
        {
            public Int64 PageIndex { get; set; }
            public Int64 PageSize { get; set; }
            public string EntrBy { get; set; }
            public string TransMode { get; set; }
            public string Name { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public long FK_DepartmentIncharge { get; set; }
        }

        public class DIViewOutPut
        {
            public DateTime EffectDate { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Department { get; set; }
            public long FK_Incharge { get; set; }
            public string Remarks { get; set; }
            public long ID_DepartmentIncharge { get; set; }
            public long TotalCount { get; set; }
            public string Employee { get; set; }
            public string Branch { get; set; }
            public string Department { get; set; }

        }

        public class DeleteInput
        {
            public long ReasonID { get; set; }
            public string TransMode { get; set; }
            public long FK_DepartmentIncharge { get; set; }
        }

        public class DeleteDepartmentIncharge
        {

            public string EntrBy { get; set; }
            public string TransMode { get; set; }
            public long FK_DepartmentIncharge { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public long Debug { get; set; }
            public long FK_Reason { get; set; }


        }
        public Output UpdateDepartmentIncharge(UpdateDepIncharge input, string companyKey)
        {
            return Common.UpdateTableData<UpdateDepIncharge>(parameter: input, companyKey: companyKey, procedureName: "ProDepartmentInchargeUpdate");
        }
        public APIGetRecordsDynamic<DIViewOutPut> GetDeptInchargelist(DIViewInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<DIViewOutPut, DIViewInput>(companyKey: companyKey, procedureName: "ProDepartmentInchargeSelect", parameter: input);
        }
        public Output DeleteDepartmentInchargeData(DeleteDepartmentIncharge input, string companyKey)
        {
            return Common.UpdateTableData<DeleteDepartmentIncharge>(parameter: input, companyKey: companyKey, procedureName: "ProDepartmentInchargeDelete");
        }


    }
}