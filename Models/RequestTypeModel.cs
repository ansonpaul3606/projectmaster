
using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PerfectWebERP.Models
{
    public class RequestTypeModel
    {

        public class RequestTypeView
        {
            public long SlNo { get; set; }
            public long RequestTypeID { get; set; }

            [Required(ErrorMessage = "Please Enter Request Type Name")]
            public string RequestTypeName { get; set; }

            [Required(ErrorMessage = "Please Enter Request Type Short Name")]
            public string RequestTypeShortName { get; set; }

            public string Mode { get; set; }
            public string ModeName { get; set; }
            [Required(ErrorMessage = "Please Enter Sort Order")]
            public Int32 SortOrder { get; set; }

            [Required(ErrorMessage = "Please Select Reason")]
            public long ReasonID { get; set; }
            public Int64 TotalCount { get; set; }
        }

        public class UpdateRequestType
        {
            
            public long FK_RequestType { get; set; }
            public string TransMode { get; set; }
            public long ID_RequestType { get; set; }
            public string ReqTypeName { get; set; }
            public string ReqTypeShortName { get; set; }
            public string Mode { get; set; }
            public Int32 SortOrder { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            //public bool Passed { get; set; }
            //public bool Cancelled { get; set; }
            public string EntrBy { get; set; }
            //public DateTime EntrOn { get; set; }
            //public string CancelBy { get; set; }
            //public DateTime CancelOn { get; set; }
            //public long FK_Reason { get; set; }
            public long FK_Machine { get; set; }
            public long BackupId { get; set; }
            public byte UserAction { get; set; }
        }
        public static string _deleteProcedureName = "ProRequestTypeDelete";
        public static string _updateProcedureName = "ProRequestTypeUpdate";
        public static string _selectProcedureName = "ProRequestTypeSelect";

        public class DeleteRequestType
        {
            public long FK_RequestType { get; set; }
            
            public string EntrBy { get; set; }
            public string TransMode { get; set; }

            public long FK_Machine { get; set; }
            public long FK_Company { get; set; }
            public long FK_Reason { get; set; }
            public long FK_BranchCodeUser { get; set; }

        }

        public class Module
        {
            public short ModeID { get; set; }
            public string ModeName { get; set; }
            public string Mode { get; set; }
        }

        public class RequestTypeList
        {

            public List<Module> Modulelist { get; set; }

            public int SortOrder { get; set; }

        }


        //procedure input for select
        public class RequestTypeID
        {
            public Int64 FK_RequestType { get; set; }

            public string TransMode { get; set; }

            public Int32 PageIndex { get; set; }


            public Int32 PageSize { get; set; }

            public string EntrBy { get; set; }

            public long FK_Company { get; set; }


            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string Name { get; set; }

        }

         
        public Output UpdateRequestTypeData(UpdateRequestType input, string companyKey)
        {
            return Common.UpdateTableData<UpdateRequestType>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteRequestTypeData(DeleteRequestType input, string companyKey)
        {
            return Common.UpdateTableData<DeleteRequestType>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<RequestTypeView> GetRequestTypeData(RequestTypeID input, string companyKey)
        {
            return Common.GetDataViaProcedure<RequestTypeView, RequestTypeID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
    }
}
