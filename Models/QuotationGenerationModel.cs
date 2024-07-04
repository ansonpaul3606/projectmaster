using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class QuotationGenerationModel
    {
        public static string _deleteProcedureName = "ProQuotationGenerationDelete";
        public static string _updateProcedureName = "ProQuotationGenerationUpdate";
        public static string _selectProcedureName = "ProQuotationGenerationSelect";
        public class QuotationGenerationNew
        {
            public long ID_QuotationGen { get; set; }
            public DateTime QuoDate { get; set; }
            public DateTime QuoExpireDate { get; set; }
            public string QuoTerms { get; set; }
            public string TransMode { get; set; }
            public Int64? LastID { get; set; }
            public List<QuotationGenerationProduct> QuotationDetails { get; set; }
        }
        public class QuotationGenerationProduct
        {
            public long ID_QuotationGenProductDetails { get; set; }
            public long SlNo { get; set; }
            public long FK_Quotation { get; set; }        
            public long FK_Product { get; set; }
            public decimal QpdQuantity { get; set; }
        }
        public class QuotationGenerationView
        {
            public int SlNo { get; set; }
            public long ID_QuotationGen { get; set; }
            public string QuoNO { get; set; }          
            public DateTime QuoDate { get; set; }         
            public DateTime QuoExpireDate { get; set; }           
            public string QuoTerms { get; set; }
            public Int64 TotalCount { get; set; }
            public Int64? LastID { get; set; }
        }

        public class UpdateQuotationGeneration
        {
            public int UserAction { get; set; }
            public long ID_QuotationGen { get; set; }
            public string TransMode { get; set; }
            public string QuoNO { get; set; }
            public DateTime QuoDate { get; set; }
            public DateTime QuoExpireDate { get; set; }
            public string QuoTerms { get; set; }
            public long FK_Company { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Department { get; set; }
            public long FK_BranchCodeUser { get; set; }           
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public string QuotationDetails { get; set; }
            public Int64 FK_QuotationGen { get; set; }
            public Int64? LastID { get; set; }
        }
        public class GetQuotationGen
        {
            public string TransMode { get; set; }
            public Int64 ID_QuotationGen { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string Name { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
        }

        public class DeleteQuotationGeneration
        {
            public long ID_QuotationGen { get; set; }
            public long FK_Reason { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
        }


        public class QuotationGenerationID
        {
            public Int64 ID_QuotationGeneration { get; set; }
        }
        public class QuotationItemDetails
        {
            public Int64 ID_QuotationGen { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
        }
        public class QuotationProductDetailsView
        {
            public long ID_QuotationGenProductDetails { get; set; }
            public long SlNo { get; set; }
            public long FK_QuotationGen { get; set; }           
            public long FK_Product { get; set; }
            public string QpdQuantity { get; set; }          
            public string ProdName { get; set; }
        }
        public Output UpdateQuotationGenerationData(UpdateQuotationGeneration input, string companyKey)
        {
            return Common.UpdateTableData<UpdateQuotationGeneration>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteQuotationGenerationData(DeleteQuotationGeneration input, string companyKey)
        {
            return Common.UpdateTableData<DeleteQuotationGeneration>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<QuotationGenerationView> GetQuotationGenerationData(GetQuotationGen input, string companyKey)
        {
            return Common.GetDataViaProcedure<QuotationGenerationView, GetQuotationGen>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
        public APIGetRecordsDynamic<QuotationProductDetailsView> GetQuotationItemDetailsSelect(QuotationItemDetails input, string companyKey)
        {
            return Common.GetDataViaProcedure<QuotationProductDetailsView, QuotationItemDetails>(companyKey: companyKey, procedureName: "ProQuotationGenItemDetailsSelect", parameter: input);
        }
    }
}