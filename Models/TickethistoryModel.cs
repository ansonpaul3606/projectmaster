using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class TickethistoryModel
    {
        public class GetTicket
        {
            public string TicketNo { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
           
        }


        public APIGetRecordsDynamicdn<dynamic> GetTicketlist(GetTicket input, string companyKey)
        {
            return Common.GetDataViaProcedureDynamic<GetTicket>(companyKey: companyKey, procedureName: "ProSelectTicketDetails", parameter: input);
        }
    }
}