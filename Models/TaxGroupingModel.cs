using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class TaxGroupingModel
    {

        public class TaxGroupView
        {
            public long SlNo { get; set; }
            public long TaxGroupID { get; set; }
            public String Name { get; set; }
            public String ShortName { get; set; }
            public Int32 SortOrder { get; set; }
            public List<TaxType> TaxTypeDetails { get; set; }
            public long TotalCount { get; set; }
            public long ReasonID { get; set; }
        }

    public class TaxType
    {
        public long ID_TaxType { get; set; }

        public string TaxtyName { get; set; }

        public decimal TaxPercentage { get; set; }
        public decimal AmountWise { get; set; }
        public Boolean Percentage { get; set; }
        public Boolean TaxOnNetamount { get; set; }
    }

        public class TaxTypesub
        {
            public long ID_TaxType { get; set; }

            public string TaxtyName { get; set; }

            public string TaxPercentage { get; set; }
            public string AmountWise { get; set; }
            public Boolean Percentage { get; set; }
            public Boolean TaxOnNetamount { get; set; }
        }

        public class UpdateTaxGroup
        {
            public long ID_TaxGroup { get; set; }
            public string TGName { get; set; }
            public string TGShortName { get; set; }
            public string TaxTypeDetails { get; set; }
            public Int32 SortOrder { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string TransMode { get; set; }
            public long Debug { get; set; }
            public string EntrBy { get; set; }
         
            public long FK_Machine { get; set; }
            public byte UserAction { get; set; }
        }
        public static string _deleteProcedureName = "ProTaxGroupDelete";
        public static string _updateProcedureName = "ProTaxGroupUpdate";
        public static string _selectProcedureName = "ProTaxGroupSelect";

        public class DeleteTaxGroup
        {
            public long FK_TaxGroup { get; set; }
           
            public string TransMode { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long Debug { get; set; }
            public long FK_Reason { get; set; }
            public long FK_BranchCodeUser { get; set; }
        }

      
        public class TaxGroupID
        {
            public Int64 FK_TaxGroup { get; set; }
            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string Name { get; set; }
            public long Detailed { get; set; }
        }
        public Output UpdateTaxGroupData(UpdateTaxGroup input, string companyKey)
        {
            return Common.UpdateTableData<UpdateTaxGroup>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteTaxGroupData(DeleteTaxGroup input, string companyKey)
        {
            return Common.UpdateTableData<DeleteTaxGroup>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<TaxGroupView> GetTaxGroupData(TaxGroupID input, string companyKey)
        {
            return Common.GetDataViaProcedure<TaxGroupView, TaxGroupID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
        public APIGetRecordsDynamic<TaxTypesub> GetTaxGroupSubtableData(TaxGroupID input, string companyKey)
        {
            return Common.GetDataViaProcedure<TaxTypesub, TaxGroupID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
    }
}

