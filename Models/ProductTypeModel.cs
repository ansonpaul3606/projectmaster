using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class ProductTypeModel
    {
        public class ProductTypeView
        {
            public long SlNo { get; set; }

            public long ID_PriceFixingType { get; set; }

            public string PftTypeName { get; set; }

            public string PftTypeShortName { get; set; }
            public long FK_SubMaster { get; set; }
            public long ?FK_Master { get; set; }

            public int PftMasterType { get; set; }
            public Int32 SortOrder { get; set; }

            public long FK_PriceFixingType { get; set; }

            public string TransMode { get; set; }
            public string Names { get; set; }
            public string Name { get; set; }
            public List<CustomertypeList> CustomertypeList { get; set; }

            public List<CustomerList> CustomerList { get; set; }

            public Int64 TotalCount { get; set; }

        }

        public class PriceTypeInputView
        {

            public string PftTypeName { get; set; }

            public string PftTypeShortName { get; set; }
            public Int16 FK_PriceFixingType { get; set; }
            public Int16 ID_PriceFixingType { get; set; }
            
            public int PftMasterType { get; set; }


            public int Sort { get; set; }
            public long ReasonID { get; set; }

            public Int64 TotalCount { get; set; }

            public string TransMode { get; set; }

        }


        public class CustomertypeList
        {
            public Int32 FK_Master { get; set; }
           public string Customertype { get; set; }

        }

     

        public class CustomerList
        {
            public Int32 FK_SubMaster { get; set; }
            public string Names { get; set; }

        }


        public class PriceType
        {
            public long FK_PriceFixingType { get; set; }
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

        public class DeletePriceType
        {
            public string TransMode { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long Debug { get; set; }
            public long FK_Reason { get; set; }
            public long FK_PriceFixingType { get; set; }

            public long FK_BranchCodeUser { get; set; }

        }
        public Output UpdateProducttypeData(ProducttypeUpdate input, string companyKey)
        {
            return Common.UpdateTableData<ProducttypeUpdate>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeletePriceTypeData(DeletePriceType input, string companyKey)
        {
            return Common.UpdateTableData<DeletePriceType>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<ProductTypeView> GetPriceTypeData(PriceType input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProductTypeView, PriceType>(companyKey: companyKey, procedureName: "ProPriceFixingTypeSelect", parameter: input);

        }
        public class ProducttypeUpdate
        {

            public long ID_PriceFixingType { get; set; }

            public string PftTypeName { get; set; }

            public string PftTypeShortName { get; set; }

            public int PftMasterType { get; set; }
            public long Debug { get; set; }
            public string TransMode { get; set; }

            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }

            public long FK_Machine { get; set; }
            public long FK_SubMaster { get; set; }
            public long FK_Master { get; set; }
            public int SortOrder { get; set; }
            public long FK_PriceFixingType { get; set; }

            public byte UserAction { get; internal set; }
        }


        public static string _updateProcedureName = "ProPriceFixingTypeUpdate";
        public static string _deleteProcedureName = "ProPriceFixingTypeDelete ";
        public static string _selectProcedureName = "ProPriceFixingTypeSelect";


    }

 
}