using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class IncentiveTypeModel
    {

        public class IncentiveTypeListModel
        {
            public int SortOrder { get; set; }

        }
        public class Incentivetypeview
        {
            public Int64 SlNo { get; set; }
            public long FK_IncentiveType { get; set; }
            public long ID_IncentiveType { get; set; }
            public short IncTModule { get; set; }

            public string IncTName { get; set; }
            public string IncTShortName { get; set; }
            public bool IncTAcposting { get; set; }

            public Int64 AccountHead { get; set; }
            public long AccountHead1 { get; set; }
            public Int32 SortOrder { get; set; }
            public string TransMode { get; set; }
            public Int64 TotalCount { get; set; }

            public string AHeadName { get; set; }
            public string AHeadName1 { get; set; }
            public string IncTModuleName { get; set; }

        }
        public class UpdateIncentiveTypes
        {
            public long FK_IncentiveType { get; set; }
            public long ID_IncentiveType { get; set; }
            public short IncTModule { get; set; }

            public string IncTName { get; set; }
            public string IncTShortName { get; set; }
            public bool IncTAcposting { get; set; }
           
            public Int64 IncAcHead { get; set; }
            public long IncAcDistribHead { get; set; }
            public Int32 SortOrder { get; set; }
            public string TransMode { get; set; }

            public Int64 FK_Company { get; set; }
            public Int16 UserAction { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
        }

        public class IncentiveTypeInfoView
        {
            [Required(ErrorMessage = "No TaxType Selected")]
            public Int64 IncentiveTypeID { get; set; }
            [Required(ErrorMessage = "Please Select The Reason")]
            public Int64 ReasonID { get; set; }
        }

        public Output UpdateIncentiveType(UpdateIncentiveTypes input, string companyKey)
        {
            return Common.UpdateTableData<UpdateIncentiveTypes>(parameter: input, companyKey: companyKey, procedureName: "ProIncentiveTypeUpdate");
        }
        public class DeleteIncentiveType
        {
            public long FK_IncentiveType { get; set; }
            public string TransMode { get; set; }
            public long FK_Reason { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }

        }
        public Output DeleteIncentiveTypeData(DeleteIncentiveType input, string companyKey)
        {
            return Common.UpdateTableData<DeleteIncentiveType>(parameter: input, companyKey: companyKey, procedureName: "ProIncentiveTypeDelete");
        }
        public class GetIncentiveType
        {
            public Int64 FK_IncentiveType { get; set; }
            public Int64 FK_Company { get; set; }
            public string TransMode { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }


            public string Name { get; set; }
        }
        public APIGetRecordsDynamic<Incentivetypeview> GetIncentiveTypeData(GetIncentiveType input, string companyKey)
        {
            return Common.GetDataViaProcedure<Incentivetypeview, GetIncentiveType>(companyKey: companyKey, procedureName: "ProIncentiveTypeSelect", parameter: input);

        }
    }
}