using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
 
    public class Providermodel
    {

        public class ProviderView
        {
            public long ID_Provider { get; set; }           
            public string ProvName { get; set; }
            public string ProvShortName { get; set; }
            public string Description { get; set; }
            public Int32 SortOrder { get; set; }
            public string Mode { get; set; }
            public string ModuleName { get; set; }
            public long FK_Paper { get; set; }

        }

        public class ModeList
        {
            public int ModeID { get; set; }
            public string ModeName { get; set; }
            public string Mode { get; set; }
            public int FK_ModuleType { get; set; }
        }
        public class ProviderList
        {
            public List<ModeList> modeList { get; set; }
            public int SortOrder { get; set; }
            public List<PaperList> PaperList { get; set; }
        }
        public class PaperList
        {
            public long FK_Paper { get; set; }
            public string PaperName { get; set; }
        }


        public class UpdateProvider
        {
            public int UserAction { get; set; }
            public int Debug { get; set; }
            public string TransMode { get; set; }
            public long ID_Provider { get; set; }

            public long FK_Provider { get; set; }
            public string ProvName { get; set; }
            public string ProvShortName { get; set; }
            public string Description { get; set; }
            public string Mode { get; set; }
            public Int32 SortOrder { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long FK_Paper { get; set; }
           
        }
        public class GetProviderDetails
        {
            public long FK_Provider { get; set; }
            public Int64 PageIndex { get; set; }
            public Int64 PageSize { get; set; }
            public string EntrBy { get; set; }
            public string Name { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string TransMode { get; set; }

        }
        public class GetProviderbyIdDetails
        {
            public long FK_Provider { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
        }
        public class ProviderSelectDetails
        {
            public long SlNo { get; set; }
            public long ID_Provider { get; set; }
            public string ProvName { get; set; }
            public string ProvShortName { get; set; }

            public string Description { get; set; }
            public string Mode { get; set; }
            public long FK_Paper { get; set; }    
            public Int32 SortOrder { get; set; }
            public int FK_ModuleType { get; set; }
            public Int64 TotalCount { get; set; }
        }
        public class DeleteProvider
        {
            public string TransMode { get; set; }
            public long FK_Provider { get; set; }
            public int Debug { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Reason { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int32 FK_BranchCodeUser { get; set; }
        }
        public class DeleteView
        {
            public long ID_Provider { get; set; }
            public Int64 ReasonID { get; set; }
        }
        public Output UpdateProviderData(UpdateProvider input, string companyKey)
        {
            return Common.UpdateTableData<UpdateProvider>(parameter: input, companyKey: companyKey, procedureName: "ProProviderUpdate");
        }
        public APIGetRecordsDynamic<ProviderSelectDetails> GetProviderSelect(GetProviderDetails input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProviderSelectDetails, GetProviderDetails>(companyKey: companyKey, procedureName: "ProProviderSelect", parameter: input);

        }
        public APIGetRecordsDynamic<ProviderSelectDetails> GetProviderSelectData(GetProviderbyIdDetails input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProviderSelectDetails, GetProviderbyIdDetails>(companyKey: companyKey, procedureName: "ProProviderSelect", parameter: input);

        }
        public Output DeleteProviderData(DeleteProvider input, string companyKey)
        {
            return Common.UpdateTableData<DeleteProvider>(parameter: input, companyKey: companyKey, procedureName: "ProProviderDelete");
        }
    }
}



