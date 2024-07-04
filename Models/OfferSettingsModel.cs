using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class OfferSettingsModel
    {
        public class OfferView
        {

            public Int32 FK_Type { get; set; }
            public string Typename { get; set; }
            public string OfrName { get; set; }
            public DateTime OfrEffectDate { get; set; }
            public DateTime OfrExpireDate { get; set; }
            public bool OfrActive { get; set; }
            public string OfrDescription { get; set; }
            public long FK_Category { get; set; }
            public long FK_Product { get; set; }
            public long FK_Offers { get; set; }
          
            public List<CategoryList> CategoryList { get; set; }
            public List<OfferSettingsDetails> OfferDetails { get; set; }
            public Int64? LastID { get; set; }
        }
        public class InputProductID
        {
            public long ProductID { get; set; }

        }
        public class OfferSettingsDetails
        {
            public Int16 FK_Category { get; set; }
            public Int16? FK_Product { get; set; }
            public string CatName { get; set; }
            public string Product { get; set; }
            public decimal ? SalPrice { get; set; }
            public decimal ?MRP { get; set; }
        }

        public class CategoryList
        {
            public Int32 FK_Category { get; set; }
            public string Category { get; set; }
        }

        public class OfferSettingUpdate
        {
            public Int32 UserAction { get; set; }
            public Int32 Debug { get; set; }
            public string TransMode { get; set; }
            public long ID_Offers { get; set; }
            public string OfrName { get; set; }
            public DateTime OfrEffectDate { get; set; }
            public DateTime OfrExpireDate { get; set; }
            public bool OfrActive { get; set; }
            public string OfrDescription { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long FK_Offers { get; set; }
            public Int32 FK_Type { get; set; }
            public string OfferDetails { get; set; }
            public Int64? LastID { get; set; } = 0;
        }

        public class OfferInput
        {
            public Int64 PageIndex { get; set; }
            public Int64 PageSize { get; set; }
            public string EntrBy { get; set; }
            public string TransMode { get; set; }
            public string Name { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public long FK_Offers { get; set; }
            public int Details { get; set; }
        }

        public class OfferOutput
        {
            public long SlNo { get; set; }
            public string TransMode { get; set; }
            public long TotalCount { get; set; }
            public Int32 FK_Type { get; set; }
            public string Typename { get; set; }
            public string OfrName { get; set; }
            public DateTime OfrEffectDate { get; set; }
            public DateTime OfrExpireDate { get; set; }
            public bool OfrActive { get; set; }
            public string OfrDescription { get; set; }
            public string ID_Offers { get; set; }
            public Int64? LastID { get; set; }
        }

        public class DeleteInput
        {
            public long ReasonID { get; set; }
            public string TransMode { get; set; }
            public long ID_Offers { get; set; }
        }

        public class DeleteOfferSetting
        {
            public string TransMode { get; set; }
            public long FK_Offers { get; set; }
            public Int32 Debug { get; set; }
            public string EntrBy { get; set; }
            public long FK_Reason { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
        }

        public class GetOfferDetailsGrid
        {
            public long ID_OffersDetails { get; set; }
            public long FK_Offers { get; set; }
            public long FK_Category { get; set; }
            public string Category { get; set; }
            public long FK_Product { get; set; }
            public string Product { get; set; }
            public decimal? SalPrice { get; set; }
            public decimal? MRP { get; set; }
            public decimal MinRate { get; set; }
            public decimal MaxRate { get; set; }
        }

        public Output UpdateOfferdetails(OfferSettingUpdate input, string companyKey)
        {
            return Common.UpdateTableData<OfferSettingUpdate>(parameter: input, companyKey: companyKey, procedureName: "ProOffersUpdate");
        }

        public APIGetRecordsDynamic<OfferOutput> GetOfferdetails(OfferInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<OfferOutput, OfferInput>(companyKey: companyKey, procedureName: "ProOffersSelect", parameter: input);
        }
        public Output DeleteOffer(DeleteOfferSetting input, string companyKey)
        {
            return Common.UpdateTableData<DeleteOfferSetting>(parameter: input, companyKey: companyKey, procedureName: "ProOffersDelete");
        }

        public APIGetRecordsDynamic<GetOfferDetailsGrid> GetOfferGrid(OfferInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<GetOfferDetailsGrid, OfferInput>(companyKey: companyKey, procedureName: "ProOffersSelect", parameter: input);
        }

    }
}


      
      
		
       