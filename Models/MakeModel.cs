using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class MakeModel
    {
        public class MakeView
        {            
            public long ID_Maker { get; set; }
            public string MakName { get; set; }            
            public string MakShortName { get; set; }
            public Int32 SortOrder { get; set; }      
            public string Mode { get; set; }
            public string ModuleName { get; set; }
        }
        public class ModeList
        {
            public int ModeID { get; set; }
            public string ModeName { get; set; }
            public string Mode { get; set; }
            public int FK_ModuleType { get; set; }
        }
        public class MakeList
        {
            public List<ModeList> modeList { get; set; }
            public int SortOrder { get; set; }
        }
        public class UpdateMake
        {
            public int UserAction { get; set; }
            public int Debug { get; set; }
            public string TransMode { get; set; }
            public long ID_Maker { get; set; }
            public string MakName { get; set; }
            public string MakShortName { get; set; }
            public string Mode { get; set; }
            public Int32 SortOrder { get; set; }            
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
        }
        public class GetMakeDetails
        {
            public Int64 FK_Maker { get; set; }
            public Int64 PageIndex { get; set; }
            public Int64 PageSize { get; set; }
            public string EntrBy { get; set; }
            public string Name { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }

        }
        public class GetmakerbyIdDetails
        {
            public Int64 FK_Maker { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
        }
        public class MakeSelectDetails
        {
            public long SlNo { get; set; }
            public long ID_Maker { get; set; }
            public string MakName { get; set; }
            public string MakShortName { get; set; }
            public string Mode { get; set; }
            public Int32 SortOrder { get; set; }
            public int FK_ModuleType { get; set; }
            public Int64 TotalCount { get; set; }
        }
        public class DeleteMaker
        {
            public string TransMode { get; set; }
            public Int64 FK_Maker { get; set; }
            public int Debug { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Reason { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int32 FK_BranchCodeUser { get; set; }
        }
        public class DeleteView
        {
            public long ID_Maker { get; set; }            
            public Int64 ReasonID { get; set; }
        }
        public Output UpdateMakeData(UpdateMake input, string companyKey)
        {
            return Common.UpdateTableData<UpdateMake>(parameter: input, companyKey: companyKey, procedureName: "ProMakerUpdate");
        }
        public APIGetRecordsDynamic<MakeSelectDetails> GetMakeSelect(GetMakeDetails input, string companyKey)
        {
            return Common.GetDataViaProcedure<MakeSelectDetails, GetMakeDetails>(companyKey: companyKey, procedureName: "ProMakerSelect", parameter: input);

        }
        public APIGetRecordsDynamic<MakeSelectDetails> GetMakeSelectData(GetmakerbyIdDetails input, string companyKey)
        {
            return Common.GetDataViaProcedure<MakeSelectDetails, GetmakerbyIdDetails>(companyKey: companyKey, procedureName: "ProMakerSelect", parameter: input);

        }
        public Output DeleteMakerData(DeleteMaker input, string companyKey)
        {
            return Common.UpdateTableData<DeleteMaker>(parameter: input, companyKey: companyKey, procedureName: "ProMakerDelete");
        }
    }
}