using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class BackupSettingModel
    {
        public class BackupSettingModelView
        {

           public long SlNo { get; set; }
            public long ID_BackupSettings { get; set; }
            public long BSBackupType { get; set; }
            public string BSBackupName { get; set; }
            public long BSKeepOldCopy { get; set; }
            public long BSSortOrder { get; set; }
            public long ?SortOrder { get; set; }
            public bool BSOverwrite { get; set; }
            public bool BSActive { get; set; }
            public string BSBackupPath { get; set; }
            public Int64 TotalCount { get; set; }
            public string Name { get; set; }
            public Int64? LastID { get; set; }
            public long ReasonID { get; set; }
            public string TransMode { get; set; }
        }

 
    public class BackupSettingModelList
    {
        public int ?SortOrder { get; set; }

    }
        public class Viewdocpath
        {
            public HttpPostedFileBase Docs { get; set; }
            public string EditDocPath { get; set; }
            public  string Path { get; set; }
        }
        public class UpdateBackupSetting
        {
            public long UserAction { get; set; }
            public long Debug { get; set; }
            public string TransMode { get; set; }
            public long ID_BackupSettings { get; set; }
            public long BSBackupType { get; set; }
            public string BSBackupName { get; set; }
            public string BSBackupPath { get; set; }
            public long BSKeepOldCopy { get; set; }
            public long BSSortOrder { get; set; }
            public bool BSOverwrite { get; set; }
            public bool BSActive { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }

            public string EntrBy { get; set; }

            public long FK_Machine { get; set; }
            public long FK_BackupSettings { get; set; }
        }

        public Output AddBOMProject(UpdateBackupSetting input, string companyKey)
        {
            return Common.UpdateTableData<UpdateBackupSetting>(companyKey: companyKey, procedureName: "ProBackupSettingsUpdate", parameter: input);
        }

        public class BackupSettingViewID
        {
            public Int64 FK_BackupSettings { get; set; }
            public Int64 FK_Company { get; set; }
            public String EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public String Name { get; set; }
           

        }
        public APIGetRecordsDynamic<BackupSettingModelView> GetBOMProjectlistviewData(BackupSettingViewID input, string companyKey)
        {
            return Common.GetDataViaProcedure<BackupSettingModelView, BackupSettingViewID>(companyKey: companyKey, procedureName: "ProBackupSettingsSelect", parameter: input);
        }
        public class DeleteBackupSetting
        {
            public long FK_BackupSettings { get; set; }
            public string TransMode { get; set; }

            public long FK_Reason { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
        }
        public Output DeleteBackupSettingData(DeleteBackupSetting input, string companyKey)
        {
            return Common.UpdateTableData<DeleteBackupSetting>(parameter: input, companyKey: companyKey, procedureName: "ProBackupSettingsDelete");
        }
    }
}