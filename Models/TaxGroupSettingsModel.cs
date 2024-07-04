/*----------------------------------------------------------------------
Created By	: NIJI TJ
Created On	: 06/03/2023
Purpose		: TaxGroupSettings
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
    public class TaxGroupSettingsModel
    {

        public class TaxGroupSettingsView
        {
            public long SlNo { get; set; }
            public DateTime TransDate { get; set; }
            public DateTime EffectDate { get; set; }
            public List<TaxGroup> TaxgroupList { get; set; }
            public string TransMode { get; set; }
            public long ReasonID { get; set; }
            public long LastID { get; set; }
         

            public long ID_TaxGroupSettings { get; set; }
            public Int64 TotalCount { get; set; }
        }
        public class TaxGroup
        {
            public long? TaxGroupID { get; set; }
            public string TaxGroupName { get; set; }
        }

        public class UpdateTaxGroupSettings
        {
            public int UserAction { get; set; }
            public long FK_TaxGroupSettings { get; set; }

            public long ID_TaxGroupSettings { get; set; }
            public DateTime TransDate { get; set; }
            public DateTime EffectDate { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            //public bool Passed { get; set; }
            public string EntrBy { get; set; }
            public long LastID { get; set; }
            public string TaxGroupSettingsDetails { get; set; }
        }
        public static string _deleteProcedureName = "ProTaxGroupSettingsDelete";
        public static string _updateProcedureName = "ProTaxGroupSettingsUpdate";
        public static string _selectProcedureName = "ProTaxGroupSettingsSelect";

        public class DeleteTaxGroupSettings
        {
            public long FK_TaxGroupSettings { get; set; }
            //public string TransMode { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long FK_Company { get; set; }
            public long FK_Reason { get; set; }
            public long FK_BranchCodeUser { get; set; }
        }


        public class TaxGroupSettings
        {
            public long ID_TaxGroupSettings { get; set; }
            public DateTime TransDate { get; set; }
            public DateTime EffectDate { get; set; }
            public int LastID { get; set; }
            public List<TaxGroupSettingsDetail> TaxGroupSettingsDetails { get; set; }


        }
        public class TaxGroupSettingsID
        {
            public Int64 FK_TaxGroupSettings { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
 
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string Name { get; set; }
            public Int32 Detailed { get; set; }
        }
        public class TaxGroupSettingsDetail
        {
            public long FK_Product { get; set; }         
            public long FK_TaxGroup { get; set; }
            public bool TgsEffectExistStock { get; set; }
            public string ProductName { get; set; }
            public string TaxGroupName { get; set; }
        }
        public Output UpdateTaxGroupSettingsData(UpdateTaxGroupSettings input, string companyKey)
        {
            return Common.UpdateTableData<UpdateTaxGroupSettings>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteTaxGroupSettingsData(DeleteTaxGroupSettings input, string companyKey)
        {
            return Common.UpdateTableData<DeleteTaxGroupSettings>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<TaxGroupSettingsDetail> GetTaxGroupSettingsDetailsData(TaxGroupSettingsID input, string companyKey)
        {
            return Common.GetDataViaProcedure<TaxGroupSettingsDetail, TaxGroupSettingsID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
        public APIGetRecordsDynamic<TaxGroupSettingsView> GetTaxGroupSettingsData(TaxGroupSettingsID input, string companyKey)
        {
            return Common.GetDataViaProcedure<TaxGroupSettingsView, TaxGroupSettingsID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
    }
}
 
