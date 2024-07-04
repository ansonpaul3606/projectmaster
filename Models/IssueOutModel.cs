


/*----------------------------------------------------------------------
Created By	: amritha ak
Created On	: 23/08/2022
Purpose		: IssueOut
-------------------------------------------------------------------------
Modification
On			By					OMID/Remarks
-------------------------------------------------------------------------
-------------------------------------------------------------------------*/

using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PerfectWebERP.Models
{
    public class IssueOutModel
    {

        public class IssueOutView
        {
            public long SlNo { get; set; }
         
            public long IssueOutID { get; set; }
            public DateTime ISOutDate { get; set; }
           
            public byte ISOutType { get; set; }
           public string ModeName { get; set; }

            public String ISOutRemarks { get; set; }
          
            public long DepartmentID { get; set; }
           public long ProductID1 { get; set; }
            public long BranchID { get; set; }
            public Int64 TotalCount { get; set; }
            public string Name { get; set; }
            public long CurrentQuantity { get; set; }
            public String TransMode { get; set; }
            public long DecreaseQuantity { get; set; }
            public List<IssueOutDetails> IssueOutDetails { get; set; }
            public Int64? LastID { get; set; }



        }
        public class IssueOutDetails
        {
            public long ID_IssueOutDetails { get; set; }
            public long SLNo { get; set; }
            public long ProductID { get; set; }
            public String Product { get; set; }
            public long Quantity { get; set; }
            public long FK_Stock { get; set; }

            public long CurrentQuantity { get; set; }

        }
        public class IssueOutViewList
        {

            public List<Department> DepartmentList { get; set; }
            public List<IssueMode> IssueModeList { get; set; }
            public Int64? LastID { get; set; }
        }
        public class Department
        {
            public Int32 DepartmentID { get; set; }
            public string DepartmentName { get; set; }

        }



        public class IssueMode
        {
            public long ID_Mode { get; set; }
            public string ModeName { get; set; }


        }

        public class ChangeModeInput
        {
            public int Mode { get; set; }

        }
        public class UpdateIssueOut
        {
            public long ID_IssueOut { get; set; }
            public DateTime IsoutDate { get; set; }
            public byte IsoutType { get; set; }
            public string IsOutRemarks { get; set; }
            public long FK_Department { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
                   
            public string EntrBy { get; set; }
            public Int64? LastID { get; set; }
            public byte UserAction { get; set; }
            public int Debug { get; set; }
            public String TransMode { get; set; }
            public long FK_Machine { get; set; }
            public String IssueOutDetails { get; set; }

        }
     
        
        public class DeleteIssueOut
        {
            public long FK_IssueOut { get; set; }
            public string TransMode { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long Debug { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public long FK_Reason { get; set; }
        }

        public class IssueOutID
        {
            public Int64 FK_IssueOut { get; set; }
            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string Name { get; set; }
            public Byte Detailed { get; set; }
        }

        public static string _deleteProcedureName = "ProIssueOutDelete";
        public static string _updateProcedureName = "ProIssueOutUpdate";
        public static string _selectProcedureName = "ProIssueOutSelect";

        public Output UpdateIssueOutData(UpdateIssueOut input, string companyKey)
        {
            return Common.UpdateTableData<UpdateIssueOut>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteIssueOutData(DeleteIssueOut input, string companyKey)
        {
            return Common.UpdateTableData<DeleteIssueOut>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<IssueOutView> GetIssueOutData(IssueOutID input, string companyKey)
        {
            return Common.GetDataViaProcedure<IssueOutView, IssueOutID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
        public class IssueOutSubSelect
        {
            public Int64 FK_IssueOut { get; set; }
            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string Name { get; set; }
            public Byte Detailed { get; set; }


        }
        public APIGetRecordsDynamic<IssueOutDetails> GetSubTableIssueOutData(IssueOutSubSelect input, string companyKey)
        {
            return Common.GetDataViaProcedure<IssueOutDetails, IssueOutSubSelect>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }
        public class IssueOutInfoView
        {
            public long IssueOutID { get; set; }
           
            public long ReasonID { get; set; }
        }
    }
}






