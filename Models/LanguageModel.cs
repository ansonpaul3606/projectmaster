

using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace PerfectWebERP.Models
{
    public class LanguageModel
    {

        public class LanguageView
        {
            public long SlNo { get; set; }
            public Int64 LanguageID { get; set; }
            public Int64 FK_Product { get; set; }
            public string Language { get; set; }
            public string ShortName { get; set; }
            public Int16 SortOrder { get; set; }
            public string RegionalName { get; set; }
            public long ReasonID { get; set; }

            public Int64 TotalCount { get; set; }
            public string TransMode { get; set; }

        }
        public class LanguageFormData
        {

            public int SortOrder { get; set; }
        }
        public class Regional
        {
            public string RegionalName { get; set; }

        }
        public class LanguageInfoView
        {
            [Required(ErrorMessage = "Please select a Language")]
            [Range(1, long.MaxValue, ErrorMessage = "Please select a Language")]
            public Int64 ID_Language { get; set; }

        }



        public static string _selectProcedureName = "ProLanguageSelect";
        public class GetLanguage
        {
            //public Int64 ID_Language { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_Language { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string Name { get; set; }

        }

        public static string _updateProcedureName = "proLanguageUpdate";
        public class UpdateLanguage
        {
            public Int64 ID_Language { get; set; }
            public int UserAction { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public Int64 FK_Company { get; set; }
            public Int16 SortOrder { get; set; }
            public string EntrBy { get; set; }
            public string TransMode { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_Product { get; set; }
            public Int64 FK_Branch { get; set; }
            public Int64 BackupId { get; set; }
            public Int64 FK_Language { get; set; }
            public Int16 Debug { get; set; }
            public string ShortName { get; set; }
            public string LangName { get; set; }
            public string RegionalName { get; set; }

        }

        public static string _deleteProcedureName = "proLanguageDelete";
        public class DeleteLanguage
        {
            //public Int64 ID_Language { get; set; }
            //public string CancelBy { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_Language { get; set; }
            public Int64 Debug { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Reason { get; set; }
            public Int64 FK_Branch { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string TransMode { get; set; }
            

        }


        public Output UpdateLanguageData(UpdateLanguage input, string companyKey)
        {
            return Common.UpdateTableData<UpdateLanguage>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteLanguageData(DeleteLanguage input, string companyKey)
        {
            return Common.UpdateTableData<DeleteLanguage>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<LanguageView> GetLanguageData(GetLanguage input, string companyKey)
        {
            return Common.GetDataViaProcedure<LanguageView, GetLanguage>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
    }
}

