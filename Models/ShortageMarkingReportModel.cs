using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PerfectWebERP.General;
using System.Data;
using Newtonsoft.Json;

namespace PerfectWebERP.Models
{
    public class ShortageMarkingReportModel
    {
        public class ShortageMarkingReportList
        {
            public int ReportMode { get; set; }
            public string Companyname { get; set; }
            public List<Branchs> BranchList { get; set; }
        }
        public class Branchs
        {
            public int BranchID { get; set; }
            public string Branch { get; set; }
        }
        public class ModeDetails
        {
            public long ID_Mode { get; set; }
            public string ModeName { get; set; }

        }
        public class ModeInput
        {
            public int Mode { get; set; }
            public long Group { get; set; } = 0;

        }
        public APIGetRecordsDynamic<ModeDetails> GetModeList(ModeInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<ModeDetails, ModeInput>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }
    }
}