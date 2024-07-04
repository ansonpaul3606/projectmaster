using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class PriorityConversionModel
    {

        public class PriorityConversionModelView
        {
            public long BranchID { get; set; }
            public string AsonDate { get; set; }
            public List<PriorityConversionDetailsView> PriorityConversionDetailsView { get; set; }
            public List<BranchList> BranchList { get; set; }
        }

        public class PriorityConversionDetailsView
        {
            public long LeadID { get; set; }
            public string LeadNo { get; set; }
            public string CustomerName { get; set; }
            public string Category { get; set; }     
            public string Product { get; set; }    
            public string AssignTo { get; set; }
            public string TargetDate { get; set; }
            public string CurrentPriority { get; set; }
            public long FK_PriorityOld { get; set; }


        }

        public class BranchList
        {
            public Int32 FK_Branch { get; set; }
            public string Branch { get; set; }


        }
        public class PriorityConversionDetailsListout
        {
            public long LeadID { get; set; }
            public string LeadNo { get; set; }
            public string CustomerName { get; set; }
            public string Category { get; set; }
            public string Product { get; set; }
            public string AssignTo { get; set; }
            public string TargetDate { get; set; }
            public string CurrentPriority { get; set; }
            public bool Status { get; set; }
            public long FK_PriorityOld { get; set; }

        }


        public class Prioritydetailsinput
        {
            public Int32 FK_Branch { get; set; }
            public string AsonDate { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
          
        }
        public class GetModeData
        {
            public Int32 Mode { get; set; }
        }
        public class PriorityList
        {
            public string ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class PriorityConversionDetails
        {
            public long FK_Lead { get; set; }
            public long FK_PriorityNew { get; set; }
            public long FK_PriorityOld { get; set; }
        }

        public class InputPriorityConversionlist
        {
          
            public long FK_Branch { get; set; }
            public List<PriorityConversionDetails> PriorityConversionDetails { get; set; }
            public DateTime AsonDate { get; set; }
            public long FK_Company { get; set; }

            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
        }
        public class UpdatePriorityConversion
        {
            public byte UserAction { get; set; }
            
            public long ID_LeadPriorityConversion { get; set; }
            public long FK_Branch { get; set; }
            public string PriorityConversionDetails { get; set; }
            public DateTime LpcDate { get; set; }
            public long FK_Company { get; set; }

            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long LpcMode { get; set; }
            public long FK_LeadPriorityConversion { get; set; }

        }
        public APIGetRecordsDynamic<PriorityConversionDetailsListout> GetPriorityLeadDetails(Prioritydetailsinput input, string companyKey)
        {
            return Common.GetDataViaProcedure<PriorityConversionDetailsListout, Prioritydetailsinput>(companyKey: companyKey, procedureName: "ProPriorityLeadDetailSelect", parameter: input);

        }
        public APIGetRecordsDynamic<PriorityList> GetPriorityList(GetModeData input, string companyKey)
        {
            return Common.GetDataViaProcedure<PriorityList, GetModeData>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);
        }

        public Output UpdatePriorityListData(UpdatePriorityConversion input, string companyKey)
        {
            return Common.UpdateTableData<UpdatePriorityConversion>(parameter: input, companyKey: companyKey, procedureName: "ProLeadPriorityConversionUpdate");
        }
    }
}