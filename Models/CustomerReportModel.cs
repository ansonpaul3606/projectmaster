using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PerfectWebERP.General;
using System.Data;
using Newtonsoft.Json;

namespace PerfectWebERP.Models
{
    public class CustomerReportModel
    {
        public class CustomerReportList
        {
            public int ReportMode { get; set; }
            public string Companyname { get; set; }
            public List<Branchs> BranchList { get; set; }
            public List<Department> DepartmentList { get; set; } 
            public List<Categorydata> CategoryList { get; set; }
            public List<ModeDetails> ModeList { get; set; }
        }
        public class Branchs
        {
            public int BranchID { get; set; }
            public string Branch { get; set; }
        }
        public class Department
        {
            public int DepartmentID { get; set; }
            public string DepartmentName { get; set; }
        }
        public class Categorydata
        {
            public Int32 ID_Category { get; set; }
            public string CategoryName { get; set; }
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