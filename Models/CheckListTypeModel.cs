using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PerfectWebERP.Models
{
    public class CheckListTypeModel
    {

        public class CheckListTypeView
        {
            public int SlNo { get; set; }
            public long ID_CheckListType { get; set; }
            public string CLTyName { get; set; }           
            public string CLTyShortName { get; set; }           
            public Int32 SortOrder { get; set; }          
            public string TransMode { get; set; }
            public int TotalCount { get; set; }
        }

        public class UpdateCheckListType
        {
            public int UserAction { get; set; }
            public short Debug { get; set; }
            public long ID_CheckListType { get; set; }
            public string CLTyName { get; set; }
            public string CLTyShortName { get; set; }
            public Int32 SortOrder { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
          
            public string EntrBy { get; set; }
          
            public long FK_Machine { get; set; }           
            public string TransMode { get; set; }
            public long FK_CheckListType { get; set; }
        }
        public static string _deleteProcedureName = "ProCheckListTypeDelete";
        public static string _updateProcedureName = "ProCheckListTypeUpdate";
        public static string _selectProcedureName = "ProCheckListTypeSelect";

        public class DeleteCheckListType
        {
            public long FK_CheckListType { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long FK_Reason { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
        }


        public class CheckListTypeID
        {
            public long ID_CheckListType { get; set; }
            public string TransMode { get; set; }
        }
        public class GetCheckListType
        {
            public long FK_CheckListType { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public int FK_BranchCodeUser { get; set; }
            public string Name { get; set; }
            public string TransMode { get; set; }
        }
        public class OutputTest
        {
            public long Column1 { get; set; }
            public int ErrCode { get; set; }
            public string ErrMsg { get; set; }
        }
        //public Output UpdateCheckListTypeData(UpdateCheckListType input, string companyKey)
        //{
        //    return Common.UpdateTableData<UpdateCheckListType>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        //}
        public APIGetRecordsDynamic<OutputTest> UpdateCheckListTypeData(UpdateCheckListType input, string companyKey)
        {
            return Common.GetDataViaProcedure<OutputTest, UpdateCheckListType>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteCheckListTypeData(DeleteCheckListType input, string companyKey)
        {
            return Common.UpdateTableData<DeleteCheckListType>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<CheckListTypeView> GetCheckListTypeData(GetCheckListType input, string companyKey)
        {
            return Common.GetDataViaProcedure<CheckListTypeView, GetCheckListType>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
    }
}
