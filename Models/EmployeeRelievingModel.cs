using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class EmployeeRelievingModel
    {

        public class EmployeeRelievingView
        {
            public long EmployeeRelievingID { get; set; }
            public long EmployeeID { get; set; }
            public long SlNo { get; set; }

            public string Employee { get; set; }
            public DateTime ERDate { get; set; }
          
            public long ERReason { get; set; }
            public string ERRemarks { get; set; }
            public List<EmployeeRelievinglist> EmployeeRelievinglist { get; set; }
            public Int64 TotalCount { get; set; }
            public long? LastID { get; set; }
            public string TransMode { get; set; }
            public Int64 ReasonID { get; set; }

        }

        public class EmployeeRelievinglist
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class ModeEmployeeRelieving
        {
            public Int32 Mode { get; set; }
        }
        public APIGetRecordsDynamic<EmployeeRelievinglist> GetEmployeeRelievingReasonList(ModeEmployeeRelieving input, string companyKey)
        {
            return Common.GetDataViaProcedure<EmployeeRelievinglist, ModeEmployeeRelieving>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }

        public class UpdateEmployeeRelieved
        {
            public byte UserAction { get; set; }
            public long ID_EmployeeRelieved { get; set; }
            public long FK_EmployeeRelieved { get; set; }
            
            public long FK_Employee { get; set; }
            public DateTime ERDate { get; set; }
          
            public long ERMode { get; set; }
            public string ERRemarks { get; set; }
          
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }

            public string EntrBy { get; set; }

            public long FK_Machine { get; set; }
            public bool Debug { get; set; }
            public string TransMode { get; set; }
        }
        public Output UpdateEmployeeRelievedData(UpdateEmployeeRelieved input, string companyKey)
        {
            return Common.UpdateTableData<UpdateEmployeeRelieved>(parameter: input, companyKey: companyKey, procedureName: "proEmployeeRelievedUpdate");
        }

        public class InputEmployeeRelievedID
        {
            public Int64 FK_EmployeeRelieved { get; set; }
            public Int64 FK_Company { get; set; }
            public string TransMode { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public Int64 PageIndex { get; set; }
            public Int64 PageSize { get; set; }
          
            public string Name { get; set; }

        }
        public APIGetRecordsDynamic<EmployeeRelievingView> GetEmployeeRelievingData(InputEmployeeRelievedID input, string companyKey)
        {
            return Common.GetDataViaProcedure<EmployeeRelievingView, InputEmployeeRelievedID>(companyKey: companyKey, procedureName: "ProEmployeeRelievedSelect", parameter: input);

        }
        public class DeleteSiteInspection
        {
            public Int64 FK_Employeerelieved { get; set; }
            public string TransMode { get; set; }
            public long FK_Reason { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }

        }
        public Output DeleteEmployeerelievedData(DeleteSiteInspection input, string companyKey)
        {
            return Common.UpdateTableData<DeleteSiteInspection>(parameter: input, companyKey: companyKey, procedureName: "ProEmployeerelievedDelete");
        }

    }
}