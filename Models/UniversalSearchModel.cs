using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PerfectWebERP.Models
{
    public class UniversalSearchModel
    {

        public class UniversalsearchView
        {
            public Int64 Mode { get; set; }
            public string Name { get; set; }
            public string MobileNo { get; set; }
            public string Address { get; set; }
            public string ReferenceNo { get; set; }
            public long FK_Company { get; set; }

            public long FK_Branch { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public Int64 TotalCount { get; set; }
        }

        public class UniversalsearchOut
        {
            public long SlNo { get; set; }
            public string Module { get; set; }
            public string TransMode{ get; set; }
            public long ReferenceID { get; set; }
            public string ReferenceNo { get; set; }
            public string CusName { get; set; }
            public string MobileNo { get; set; }
            public string EMail { get; set; }
            public string Address { get; set; }

            public string Place { get; set; }

            public string Area { get; set; }
            public Int64 TotalCount { get; set; }
        }
        public APIGetRecordsDynamic<UniversalsearchOut> GetUniversalSearchData(UniversalsearchView input, string companyKey)
        {
            return Common.GetDataViaProcedure<UniversalsearchOut, UniversalsearchView>(companyKey: companyKey, procedureName: "ProUniversalSearchSelect", parameter: input);

        }
    }
}