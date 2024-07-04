/*----------------------------------------------------------------------
Created By	: Amritha A K
Created On	: 03/02/2022
Purpose		: OtherChargeType
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
    public class OtherChargeTypeModel
    {
        public class OtherChargeTypeView
        {
            public long SlNo { get; set; }
            public long OtherChargeTypeID { get; set; }
            [Required(ErrorMessage = "Please Enter  Name")]
            public string Names { get; set; }
  
            public string ShortName { get; set; }
            [Required(ErrorMessage = "Please Select Trans Type")]
            public byte TransTypeID { get; set; }
            public string TransType { get; set; }

            // [Required(ErrorMessage = "Please Enter Octy Sales")]
            public bool Sales { get; set; }
            //[Required(ErrorMessage = "Please Enter Octy Sales Return")]
            public bool SalesReturn { get; set; }
            // [Required(ErrorMessage = "Please Enter Octy Purchase")]
            public bool Purchase { get; set; }
            // [Required(ErrorMessage = "Please Enter Octy Purchase Return")]
            public bool PurchaseReturn { get; set; }
            // [Required(ErrorMessage = "Please Enter Octy Other")]
            public bool Other { get; set; }
            public long? AccountHead { get; set; }
            public long? AccountHeadSub { get; set; }
            public string AHeadName { get; set; }
            public int? AccountCode { get; set; }
            public int? AccountSHCode { get; set; }
            public string ASHeadName { get; set; }
            public Int32 SortOrder { get; set; }
            public Int64 TotalCount { get; set; }
            public string Name { get; set; }
            public string TransMode { get; set; }
            public long FK_TaxGroup { get; set; }
        }

        public class OtherChargeTypeViewList
        {

            public List<TransTypes> TransTypeList { get; set; }
            public Int32 SortOrder { get; set; }
            public List<TaxGroup> TaxgroupList { get; set; }

        }

        public class TransTypes
        {
            public int TransTypeID { get; set; }
            public string TransType { get; set; }

            public long FK_TransType { get; set; }

        }
        public class TaxGroup
        {
            public long TaxGroupID { get; set; }
            public string TaxGroupName { get; set; }
        }
        public class UpdateOtherChargeType
        {
            public long ID_OtherChargeType { get; set; }
            public string OctyName { get; set; }
            public string OctyShortName { get; set; }
            public byte OctyTransType { get; set; }
            public bool OctySales { get; set; }
            public bool OctySalesReturn { get; set; }
            public bool OctyPurchase { get; set; }
            public bool OctyPurchaseReturn { get; set; }
            public bool OctyOther { get; set; }
            public long FK_AccountHead { get; set; }
            public long FK_AccountSubHead { get; set; }

            public Int32 SortOrder { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public byte UserAction { get; set; }
            public long FK_OtherChargeType { get; set; }
            public string TransMode { get; set; }
            public long FK_TaxGroup { get; set; }
        }


        public class DeleteOtherChargeType
        {
            public long ID_OtherChargeType { get; set; }
            public string TransMode { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long Debug { get; set; }
            public long FK_Reason { get; set; }

            public long FK_BranchCodeUser { get; set; }

        }
        public class OtherChargeTypeID
        {
            public Int64 ID_OtherChargeType { get; set; }
            public Int64 FK_Company { get; set; }
            public String EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string Name { get; set; }

        }

        public class OtherChargeTypeInfoView
        {
            public long ID_OtherChargeType { get; set; }

            public long ReasonID { get; set; }
        }

        public static string _deleteProcedureName = "ProOtherChargeTypeDelete";
        public static string _updateProcedureName = "ProOtherChargeTypeUpdate";
        public static string _selectProcedureName = "ProOtherChargeTypeSelect";

        public Output UpdateOtherChargeTypeData(UpdateOtherChargeType input, string companyKey)
        {
            return Common.UpdateTableData<UpdateOtherChargeType>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteOtherChargeTypeData(DeleteOtherChargeType input, string companyKey)
        {
            return Common.UpdateTableData<DeleteOtherChargeType>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<OtherChargeTypeView> GetOtherChargeTypeData(OtherChargeTypeID input, string companyKey)
        {
            return Common.GetDataViaProcedure<OtherChargeTypeView, OtherChargeTypeID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
    }
}
