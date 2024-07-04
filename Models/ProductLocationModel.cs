using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class ProductLocationModel
    {
        public class ProductLocationList
        {
            public List<Branch> BranchList { get; set; }
            public int ?SortOrder { get; set; }
        }

        public class Branch
        {
            public Int32 BranchID { get; set; }
            public string BranchName { get; set; }

        }
        public class GetProductLocation
        {
            public Int64 FK_ProductLocation { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public string Name { get; set; }
            public Int64 FK_Company { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string TransMode { get; set; }
        }
        public class ProductLocationView
        {
            public long FK_ProductLocation { get; set; }

            public long ID_ProductLocation { get; set; }
            public long ReasonID { get; set; }

            public Int64 ProductLocationID { get; set; }
            public string LocationName { get; set; }
            public string LocationShortName { get; set; }
            public long BranchID { get; set; }
            public long ?SortOrder { get; set; }
            public string EntrBy { get; set; }
            public DateTime EntrOn { get; set; }
            public Int64 TotalCount { get; set; }
        }
        public class ProductionLocationUpdate
        {
            public long ID_ProductLocation { get; set; }
            public long ?SortOrder { get; set; }
            public string LocationName { get; set; }
            public string LocationShortName { get; set; }
            public long FK_Branch { get; set; }
            public long Debug { get; set; }
            public string TransMode { get; set; }
            public long FK_Machine { get; set; }
            public long FK_Company { get; set; }

            public long FK_ProductLocation { get; set; }
            public string EntrBy { get; set; }
            public byte UserAction { get; internal set; }
        }
        public class DeleteProductionLocation
        {
            public long FK_ProductLocation { get; set; }
            public long FK_Reason { get; set; }

            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long Debug { get; set; }
        }
        public APIGetRecordsDynamic<ProductLocationView> GetProductLocationData(GetProductLocation input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProductLocationView, GetProductLocation>(companyKey: companyKey, procedureName: "ProProductLocationSelect", parameter: input);
        }
        public Output UpdateProductionLoactionData(ProductionLocationUpdate input, string companyKey)
        {
            return Common.UpdateTableData<ProductionLocationUpdate>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }


        public Output DeleteAdvancePaymentData(DeleteProductionLocation input, string companyKey)
        {
            return Common.UpdateTableData<DeleteProductionLocation>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }


        public static string _updateProcedureName = "ProProductLocationUpdate";
        public static string _deleteProcedureName = "ProProductLocationDelete";
        public static string _selectProcedureName = "ProProductLocationSelect";

    }
}