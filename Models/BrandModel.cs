using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class BrandModel
    {
       
            public class BrandView
            {
                public int ID_Brand { get; set; }
                public string BrName { get; set; }
                public string BrShortName { get; set; }
                public Int32 SortOrder { get; set; }
                public string Mode { get; set; }
                public string ModuleName { get; set; }
            public long FK_Manufacturer { get; set; }
        }
        public class ModeList
        {
            public int ModeID { get; set; }
            public string ModeName { get; set; }
            public string Mode { get; set; }
            public int FK_ModuleType { get; set; }
        }
        public class BrandList
        {
            public List<ModeList> modeList { get; set; }
            public int SortOrder { get; set; }
            public List<ManufacturerList> ManufacturerList { get; set; }
        }
        public class ManufacturerList
        {
            public long FK_Manufacturer { get; set; }
            public string ManufactureName { get; set; }

        }

        public class UpdateBrand
        {
            public int UserAction { get; set; }
            public int Debug { get; set; }
            public string TransMode { get; set; }
            public long ID_Brand { get; set; }
            public string BrName { get; set; }
            public string BrShortName { get; set; }
            public string Mode { get; set; }
            public Int32 SortOrder { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long FK_Manufacturer { get; set; }
        }
        public class GetBrandDetails
        {
            public Int64 FK_Brand { get; set; }
            public Int64 PageIndex { get; set; }
            public Int64 PageSize { get; set; }
            public string EntrBy { get; set; }
            public string Name { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }

        }
        public class GetBrandbyIdDetails
        {
            public Int64 FK_Brand { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
        }
        public class BrandSelectDetails
        {
            public long SlNo { get; set; }
            public long ID_Brand { get; set; }
            public string BrName { get; set; }
            public string BrShortName { get; set; }
            public string Mode { get; set; }
            public Int32 SortOrder { get; set; }
            public int FK_ModuleType { get; set; }
            public Int64 TotalCount { get; set; }
            public long FK_Manufacturer { get; set; }
        }
        public class DeleteBrand
        {
            public string TransMode { get; set; }
            public Int64 FK_Brand { get; set; }
            public int Debug { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Reason { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int32 FK_BranchCodeUser { get; set; }
        }
        public class DeleteView
        {
            public long ID_Brand { get; set; }
            public Int64 ReasonID { get; set; }
        }
        public Output UpdateBrandData(UpdateBrand input, string companyKey)
        {
            return Common.UpdateTableData<UpdateBrand>(parameter: input, companyKey: companyKey, procedureName: "ProBrandUpdate");
        }
        public APIGetRecordsDynamic<BrandSelectDetails> GetBrandSelect(GetBrandDetails input, string companyKey)
        {
            return Common.GetDataViaProcedure<BrandSelectDetails, GetBrandDetails>(companyKey: companyKey, procedureName: "ProBrandSelect", parameter: input);

        }
        public APIGetRecordsDynamic<BrandSelectDetails> GetBrandSelectData(GetBrandbyIdDetails input, string companyKey)
        {
            return Common.GetDataViaProcedure<BrandSelectDetails, GetBrandbyIdDetails>(companyKey: companyKey, procedureName: "ProBrandSelect", parameter: input);

        }
        public Output DeleteBrandData(DeleteBrand input, string companyKey)
        {
            return Common.UpdateTableData<DeleteBrand>(parameter: input, companyKey: companyKey, procedureName: "ProBrandDelete");
        }
    }
}


    
