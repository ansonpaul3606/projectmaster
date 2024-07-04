using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class RecoveryAllowanceModel
    {

        public class RecoveryAllowanceView
        {
            public long SlNo { get; set; }

            public long ID_Allowance { get; set; }
            public long ID_AllowanceType { get; set; }

            public string ALWName { get; set; }
            public int ALWType { get; set; }

            public string ALWShortName { get; set; }
            public int ALWMode { get; set; }
            public string Mode { get; set; }
            public string ModeName { get; set; }
            public long FK_AllowanceType { get; set; }

            public long? FK_AccountHead { get; set; }
            public long? FK_AccountHeadSub { get; set; }
            public long ?AccountHead { get; set; }
            public long ?AccountHeadSub { get; set; }
            public string AHeadName { get; set; }
            public Int64 ReasonID { get; set; }
            public long FK_Company { get; set; }
            public string Name { get; set; }
            public Int64 TotalCount { get; set; }

            public string ASHeadName { get; set; }
            public Int32 SortOrder { get; set; }
            public List<ModeList> ModeList { get; set; }
        }

        //public class RecoveryAllowanceListModel
        //{

        //    //public List<TypeMode> TypeModeList { get; set; }

        //    public int SortOrder { get; set; }

        //}
        public class ModeSelect
        {
            public int Mode { get; set; }
        }
        public class ModeList
        {
            public long ID_Mode { get; set; }
            public string ModeName { get; set; }


        }
     
        public class AccountHeadView
        {

            public long AccountHead { get; set; }

            public string AHeadName { get; set; }
            public int AccountCode { get; set; }


        }

        public class AccountSubHeadView
        {
            public long AccountHeadSub { get; set; }
            public int AccountSHCode { get; set; }
            public string ASHeadName { get; set; }

        }

        public class RecoveryAllowanceInfoView
        {
            //[Required(ErrorMessage = "No Maintenance Selected")]
            public Int64 ID_AllowanceType { get; set; }
            //[Required(ErrorMessage = "Please Select The Reason")]
            public Int64 ReasonID { get; set; }
            public long ID_Allowance { get; set; }

        }

        public class RecoveryAllowanceUpdate
        {

         
            public long ID_AllowanceType { get; set; }

            public string ALWName { get; set; }

            public string ALWShortName { get; set; }

            public int ALWMode { get; set; }
            public long Debug { get; set; }
            public string TransMode { get; set; }

            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }

            public long FK_Machine { get; set; }
            public long FK_AccountHead { get; set; }
            public long FK_AccountHeadSub { get; set; }
         
            public long ALWType { get; set; }
            public int SortOrder { get; set; }
            public long FK_BackupId { get; set; }
            public long FK_AllowanceType { get; set; }


            public byte UserAction { get; internal set; }
        }

        public class RecoveryAllowance
        {
            public long FK_AllowanceType { get; set; }
            //public Int64 ID_Country { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string Name { get; set; }
          



        }
        public class RecoveryAllowanceInputFromViewModel
        {

            //[Required(ErrorMessage = "NO Maintenance Selected")]
            public long ID_Allowance { get; set; }
            //[Required(ErrorMessage = "Please Enter Maintenance")]
            public string ALWName { get; set; }

            //[Required(ErrorMessage = "Please Enter Short Name")]

            public long ALWType { get; set; }
            public int ALWMode { get; set; }

            public string ALWShortName { get; set; }
            public long AccountHead { get; set; }
            public long AccountHeadSub { get; set; }
            public Int32 SortOrder { get; set; }
            public string TransMode { get; set; }

        }

        public class TypeList
        {

            //public List<TypeMode> TypeModeList { get; set; }

            public string ALWType { get; set; }

        }

        public class GetRecoveryAllowance
        {
            public Int64 FK_AllowanceType { get; set; }
            public Int64 FK_Company { get; set; }
            public string TransMode { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
        }



        public class DeleteRecoveryAllowance
        {

            public string TransMode { get; set; }
            public long FK_AllowanceType { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long Debug { get; set; }
            public long FK_Reason { get; set; }
            public long FK_Company { get; set; }

            public long FK_BranchCodeUser { get; set; }

        }

        public Output UpdateRecoveryAllowanceData(RecoveryAllowanceUpdate input, string companyKey)
        {
            return Common.UpdateTableData<RecoveryAllowanceUpdate>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }

        //public Output DeleteRecoveryData(DeletePriceType input, string companyKey)
        //{
        //    return Common.UpdateTableData<DeletePriceType>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        //}

     
        public APIGetRecordsDynamic<RecoveryAllowanceView> GetRecoveryAllowanceData(RecoveryAllowance input, string companyKey)
        {
            return Common.GetDataViaProcedure<RecoveryAllowanceView, RecoveryAllowance>(companyKey: companyKey, procedureName: "ProAllowanceTypeSelect", parameter: input);

        }
       
        public Output DeleteRecoveryAllowanceData(DeleteRecoveryAllowance input, string companyKey)
        {
            return Common.UpdateTableData<DeleteRecoveryAllowance>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public static string _updateProcedureName = "ProAllowanceTypeUpdate";
        public static string _deleteProcedureName = "ProAllowanceTypeDelete ";
        public static string _selectProcedureName = "ProAllowanceTypeSelect";




































    }
}