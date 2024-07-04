using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public static class TransModeSettings
    {
        public class TransModePara
        {
            public string ControllerName { get; set; }
            public string Url { get; set; }
            public long FK_MenuGroup { get; set; }
            public long FK_Company { get; set; }
        }
        public class TransModeData
        {
            public string TransMode { get; set; }
        }
       
        public static string GetTransMode(string menuGrp,string ctr,string action, string companyKey, int fk_company)
        {
            CommonMethod objCmnMethod = new CommonMethod();
            string mGrp = objCmnMethod.DecryptString(menuGrp);
            string result = "";
            TransModePara input = new TransModePara();
            input.ControllerName = ctr;
            input.Url = action;
            input.FK_MenuGroup = Convert.ToInt32(mGrp);
            input.FK_Company = fk_company;          
            var transData =Common.GetDataViaProcedure<TransModeData, TransModePara>(companyKey: companyKey, procedureName: "ProGetTransMode", parameter: input);
            if(transData.Data.Count>0)
            {
                result = transData.Data[0].TransMode;
            }
            return result;
        }
    }
}