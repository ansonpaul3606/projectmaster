//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace PerfectWebERP.Models
//{
//    public class AMCSettingsModel
//    {
//        public class AMCSettingsViewList
//        {
//            public long SlNo { get; set; }

//            public List<AMCTypeNameList> AMCTypeName { get; set; }

//            public Int64 TotalCount { get; set; }
//        }

//        public class AMCTypeNameList
//        {
//            public Int32 AMCTypeID { get; set; }
//            public string AMCName { get; set; }

//        }

//        public class AMCSettingsView
//        {
//            public long ID_AMCSettings { get; set}}
//            public long FK_AMCType { get; set}
//            public DateTime EffectDate { get; set; }
//            public long? FK_AccountHead { get; set; }
//            public long? FK_AccountHeadSub { get; set; }
//            public List<AMCDetailsView> AMCDetails { get; set; }
//        }
//        public class AMCDetailsView
//        {
//            public long ID_AMCSettingsDetails { get; set; }
//            public long FK_AMCSettings { get; set; }
//            public Int64 AMCSMonthFrom { get; set; }
//            public Int64 AMCSMonthTo { get; set; }
//            public Int64 AMCSNoOfServices { get; set; }
//            public decimal AMCSAmount { get; set; }
//            public decimal AMCSPercentage { get; set; }       

//        }

//}

using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PerfectWebERP.Models
{
    public class AMCSettingsModel
    {

        public class AMCSettingsView
        {
            public DateTime EffectDate { get; set; }
            public long? FK_AccountHead { get; set; }
            public long? FK_AccountHeadSub { get; set; }
            public long ID_AMCSettings { get; set; }
            public long FK_AMCType { get; set; }
            public List<AMCSettingsDetailsView> AMCSettingsDetails { get; set; }
            public string TransMode { get; set; }
            public List<TaxGroup> TaxgroupList { get; set; }
            public bool IncludeTax { get; set; }
            public long? TaxGroupID { get; set; }
        }

        public class AMCSettingsViewList
        {
            [Range(1, long.MaxValue, ErrorMessage = "Select A M C Type")]
            public List<AMCTypeNameList> AMCType { get; set; }
            [Required(ErrorMessage = "Please Enter Effect Date")]
            public DateTime EffectDate { get; set; }
            public long? FK_AccountHead { get; set; }
            [Range(1, long.MaxValue, ErrorMessage = "Select Account Head")]
          
            public long? FK_AccountHeadSub { get; set; }
            [Range(1, long.MaxValue, ErrorMessage = "Select Account Head Sub")]
         
          //  public List<AMCSettingsDetailsView> AMCSettingsDetails { get; set; }
            public long ID_AMCSettings { get; set; }
            public long FK_AMCSettings { get; set; }
            public string TransMode { get; set; }
            public Int64 TotalCount { get; set; }
            public Int64 Detailed { get; set; }
            public Int64 AMCSMonthFrom { get; set; }
            public Int64 AMCSMonthTo { get; set; }
            public Int64 AMCSNoOfServices { get; set; }
            public Int64 AMCSCalMethod { get; set; }
            public decimal AMCSAmount { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public long FK_AMCType { get; set; }
            public List<AMCSettingsDetailsView> AMCSettingsDetails { get; set; }
            public List<TaxGroup> TaxgroupList { get; set; }
            public long? TaxGroupID { get; set; }
            public bool IncludeTax { get; set; }
        }

        public class AMCTypeNameList
        {
            public Int32 AMCTypeID { get; set; }
            public string AMCName { get; set; }

        }
        public class AMCSettingsDetailsID
        {
            public long ID_AMCSettings { get; set; }
            public long FK_AMCSettings { get; set; }

        }
        public class TaxGroup
        {
            public long? TaxGroupID { get; set; }
            public string TaxGroupName { get; set; }
        }
        public class AMCSettingsDetailsView
        {
            public long ID_AMCSettingsDetails { get; set; }
            public long FK_AMCSettings { get; set; }
            public Int64 AMCSMonthFrom { get; set; }
            public Int64 AMCSMonthTo { get; set; }
            public Int64 AMCRenewPeriod { get; set; }
            
            public Int64 AMCSNoOfServices { get; set; }
            public Int64 AMCSCalMethod { get; set; }
            public decimal AMCSAmount { get; set; }
        }
        public class AMCSettingsDetailsViewSelect
        {
           
            public long FK_AMCSettings { get; set; }
            public Int64 AMCSMonthFrom { get; set; }
            public Int64 AMCSMonthTo { get; set; }
            public Int64 AMCRenewPeriod { get; set; }
            public Int64 AMCSNoOfServices { get; set; }
            public Int64 AMCSCalMethod { get; set; }
            public decimal AMCSAmount { get; set; }
            public long? FK_AccountHead { get; set; }
            public long? FK_AccountHeadSub { get; set; }
        }

        public class UpdateAMCSettings
        {
            public long ID_AMCSettings { get; set; }
            public long FK_AMCSettings { get; set; }
            //public long FK_AMCType { get; set; }
            //public List<AMCTypeNameList> FK_AMCType { get; set; }
            public long FK_AMCType { get; set; }
            public DateTime EffectDate { get; set; }
            public long? FK_AccountHead { get; set; }
            public long? FK_AccountHeadSub { get; set; }
            public long? FK_TaxGroup { get; set; }
            public string FK_AMCSettingsDetails { get; set; }
            public Int16 UserAction { get; set; }
            public long Debug { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public string TransMode { get; set; }
            
          public bool AMCAmountIncludeTax { get; set; }
        }
        public static string _deleteProcedureName = "ProAMCSettingsDelete";
        public static string _updateProcedureName = "ProAMCSettingsUpdate";
        public static string _selectProcedureName = "ProAMCSettingsSelect";

        public class DeleteAMCSettings
        {
            public long FK_AMCSettings { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public string TransMode { get; set; }
            public long FK_Company { get; set; }
            public long FK_Reason { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public long Debug { get; set; }
        }


        public class AMCSettingsID
        {
            //public Int64 ID_AMCSettings { get; set; }
            //public string TransMode { get; set; }
            //public long FK_AMCSettings { get; set; }
            //public Int32 PageIndex { get; set; }
            //public Int32 PageSize { get; set; }
            //public long FK_Company { get; set; }
            //public long FK_Machine { get; set; }
            //public long FK_BranchCodeUser { get; set; }
            //public string EntrBy { get; set; }
            public string TransMode { get; set; }
            public long FK_AMCSettings { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public Int64 Detailed { get; set; }
        }

        public class AMCSettingsViewData
        {
            public long SlNo { get; set; }
            public DateTime EffectDate { get; set; }
            //public long FK_AMCSettings { get; set; }
            public long ID_AMCSettings { get; set; }
            public string AMCName { get; set; }
            //public Int32 AMCTypeID { get; set; }
            public long? FK_AccountHead { get; set; }
            public long? FK_AccountHeadSub { get; set; }
            public Int64 TotalCount { get; set; }

        }

        public class AMCSettingsViewDetails
        {
            public long SlNo { get; set; }
            public DateTime EffectDate { get; set; }
            public long FK_AMCSettings { get; set; }
            public long ID_AMCSettings { get; set; }
            public string AMCName { get; set; }
            public Int32 AMCTypeID { get; set; }
            public string AccountHeadName { get; set; }
            public string AccountSubHeadName { get; set; }
            public long? FK_AccountHead { get; set; }
            public long? FK_AccountHeadSub { get; set; }
            public List<TaxGroup> TaxgroupList { get; set; }
            public long? TaxGroupID { get; set; }
            public string TaxGroupName { get; set; }
            public Int64 TotalCount { get; set; }
            public bool IncludeTax { get; set; }

        }


        public class AMCSettingsViewDet
        {
            public long FK_AMCSettings { get; set; }
            //public Int64 AMCSMonthFrom { get; set; }
            //public Int64 AMCSMonthTo { get; set; }
            //public Int64 AMCSNoOfServices { get; set; }
            //public Int64 AMCSCalMethod { get; set; }
            //public decimal AMCSAmount { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public Int64 Detailed { get; set; }

        }
        public Output UpdateAMCSettingsData(UpdateAMCSettings input, string companyKey)
        {
            return Common.UpdateTableData<UpdateAMCSettings>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteAMCSettingsData(DeleteAMCSettings input, string companyKey)
        {
            return Common.UpdateTableData<DeleteAMCSettings>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<AMCSettingsViewData> GetAMCSettings(AMCSettingsID input, string companyKey)
        {
            return Common.GetDataViaProcedure<AMCSettingsViewData, AMCSettingsID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
        public APIGetRecordsDynamic<AMCSettingsViewDetails> GetAMCSettingsData(AMCSettingsID input, string companyKey)
        {
            return Common.GetDataViaProcedure<AMCSettingsViewDetails, AMCSettingsID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
      
        public APIGetRecordsDynamic<AMCSettingsDetailsViewSelect> GetAMCSettingsDetailsSelect(AMCSettingsViewDet input, string companyKey)
        {
            return Common.GetDataViaProcedure<AMCSettingsDetailsViewSelect, AMCSettingsViewDet>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
    }
}