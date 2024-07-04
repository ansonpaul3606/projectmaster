using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PerfectWebERP.General;

namespace PerfectWebERP.Models
{
    public class SecurityTypeModel
    {

        public class SecurityTypeViewModel
        {
            public long SlNo { get; set; }
            [Required(ErrorMessage = "Please Enter Customer Category Name")]

            public string SecurityTypeName { get; set; }

            [Required(ErrorMessage = "Please Enter Short Name ")]
            public string SecurityTypeShortName { get; set; }

            public Int16 SecurityTypeID { get; set; }
            public int SortOrder { get; set; }
            public long ReasonID { get; set; }

        }

        public class Sortorderlist
        {
            public Int32 SortOrder { get; set; }
        }

        public static string _updateProcedureName = "proSecurityTypeUpdate";
        public class UpdateSecurityType
        {
            public long ID_SecurityType { get; set; }
            public string SectyName { get; set; }
            public string SectyShortName { get; set; }

            public Int32 SortOrder { get; set; }
           


            public long FK_SecurityType { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string TransMode { get; set; }
            public long Debug { get; set; }
            public string EntrBy { get; set; }
           
            public long FK_Machine { get; set; }
            public byte UserAction { get; set; }

        }

        public class SecurityTypeInfoView
        {
            [Required(ErrorMessage = "Please select a SecurityType")]
            [Range(1, long.MaxValue, ErrorMessage = "Please select a SecurityType")]
            public Int64 SecurityTypeID { get; set; }
            [Required(ErrorMessage = "Please select the reason")]
            public Int64 ReasonID { get; set; }

        }

        public static string _deleteProcedureName = "proSecurityTypeDelete";
        public class DeleteSecurityType
        {
            public Int64 FK_SecurityType { get; set; }
            public string TransMode { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long Debug { get; set; }
            public long FK_Reason { get; set; }
            public long FK_BranchCodeUser { get; set; }
        }
        


        public static string _selectProcedureName = "proSecurityTypeSelect";
        public class GetSecurityType
        {
            public Int64 FK_SecurityType { get; set; }
            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public long FK_BranchCodeUser
            {
                get; set;
            }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }

        }

        public Output UpdateSecurityTypeData(UpdateSecurityType input, string companyKey)
        {
            return Common.UpdateTableData<UpdateSecurityType>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }

        public Output DeleteSecurityTypeData(DeleteSecurityType input, string companyKey)
        {
            return Common.UpdateTableData<DeleteSecurityType>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }

        public APIGetRecordsDynamic<SecurityTypeViewModel> GetSecurityTypeData(GetSecurityType input, string companyKey)
        {
            return Common.GetDataViaProcedure<SecurityTypeViewModel, GetSecurityType>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }

    }
}