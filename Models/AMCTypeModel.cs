using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class AMCTypeModel
    {
        public class AMCList
        {
            public int SortOrder { get; set; }
        }

        public class AMCTypeView
        {
            public Int64 AMCTypeID { get; set; }
            public string AMCName { get; set; }
            public string AMCShortName { get; set; }
            public Int16 SortOrder { get; set; }
            public string EntrBy { get; set; }
            public DateTime EntrOn { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string TransMode { get; set; }
            public Int64 TotalCount { get; set; }
        }
        public class AMCTypeInputView
        {
            public Int64 AMCTypeID { get; set; }
            public string AMCTypeName { get; set; }
            public string AMCTypeShortName { get; set; }
            public Int16 SortOrder { get; set; }
            public string TransMode { get; set; }

        }

        public class UpdateAMCType
        {

            public byte UserAction { get; set; }
            public long ID_AMCType { get; set; }
            public long FK_AMCType { get; set; }
            public string AMCName { get; set; }
            public string AMCShortName { get; set; }
            public Int32 SortOrder { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public int Debug { get; set; }
            public string TransMode { get; set; }
        }

        public class GetAMCType
        {
            public Int64 FK_AMCType { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public Int64 FK_Machine { get; set; }
            public string Name { get; set; }
            public Int64 FK_Company { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string TransMode { get; set; }
        }

        public class AMCTypeInfoView
        {
            [Required(ErrorMessage = "Please select a AMC Type")]
            [Range(1, long.MaxValue, ErrorMessage = "Please select a AMC Type")]
            public Int64 AMCTypeID { get; set; }
            [Required(ErrorMessage = "Please select the reason")]
            public Int64 ReasonID { get; set; }
        }

        public class DeleteAMCType
        {
            public string TransMode { get; set; }
            public Int64 FK_AMCType { get; set; }
            public Int64 FK_Reason { get; set; }
            public int Debug { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_Company { get; set; }
            public Int32 FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }

        }

        public static string _updateProcedureName = "ProAMCTypeUpdate";
        public static string _selectProcedureName = "ProAMCTypeSelect";
        public static string _deleteProcedureName = "ProAMCTypeDelete";

        public Output UpdateAMCTypeData(UpdateAMCType input, string companyKey)
        {
            return Common.UpdateTableData<UpdateAMCType>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public APIGetRecordsDynamic<AMCTypeView> GetAMCTypeData(GetAMCType input, string companyKey)
        {
            return Common.GetDataViaProcedure<AMCTypeView, GetAMCType>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }

        public Output DeleteAMCData(DeleteAMCType input, string companyKey)
        {
            return Common.UpdateTableData<DeleteAMCType>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
    }
}