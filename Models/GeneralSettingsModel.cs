using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class GeneralSettingsModel
    {
        public class GeneralSettingsView
        {
            public string GsModule { get; set; }
            public string GsField { get; set; }
            public bool GsValue { get; set; }
            public string GsData { get; set; }
            public long ID_GeneralSettings { get; set; }
        }
        public class GeneralSettingsUpdate
        {
            public string GsModule { get; set; }
            public string GsField { get; set; }
            public bool GsValue { get; set; }
            public string GsData { get; set; }
            public long ID_GeneralSettings { get; set; }
            public long FK_GeneralSettings { get; set; }
            public long FK_Company { get; set; }
            public long FK_Branch { get; set; }
            public Int16 UserAction { get; set; }
            public Int16 Debug { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
        }
        public class GeneralSettingsInput
        {
            public Int64 PageIndex { get; set; }
            public Int64 PageSize { get; set; }
            public string EntrBy { get; set; }
            public string Name { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public long FK_GeneralSettings { get; set; }
        }
        public class GeneralSettingsOutput
        {
            public long TotalCount { get; set; }
            public long SlNo { get; set; }
            public string GsModule { get; set; }
            public string GsField { get; set; }
            public bool GsValue { get; set; }
            public string GsData { get; set; }
            public long ID_GeneralSettings { get; set; }

        }
        public Output Updategeneralsettingsdetails(GeneralSettingsUpdate input, string companyKey)
        {
            return Common.UpdateTableData<GeneralSettingsUpdate>(parameter: input, companyKey: companyKey, procedureName: "ProGeneralSettingsUpdate");
        }

        public APIGetRecordsDynamic<GeneralSettingsOutput> Getgenerealsettingsdetails(GeneralSettingsInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<GeneralSettingsOutput, GeneralSettingsInput>(companyKey: companyKey, procedureName: "ProGeneralSettingsSelect", parameter: input);
        }
    }
}