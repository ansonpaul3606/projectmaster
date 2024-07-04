using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace PerfectWebERP.Models
{
    public class MediaSubMasterModel
    {       
        public class MediaSubMasterListModel
        {
            public List<MediaMaster> MediaMasterList { get; set; }
            public int SortOrder { get; set; }

        }
        public class MediaMaster
        {
            public short MediaMasterID { get; set; }
            public string MediaMasterName { get; set; }
        }

        public class MediaSubMasterInputFromViewModel
        {
            public long FK_MediaSubMaster { get; set; }
            public long ID_MediaSubMaster { get; set; }
            public string SubMediaName { get; set; }
            public string SubMediaShortName { get; set; }
            public short FK_Media { get; set; }
            public Int32 SortOrder { get; set; }
            public string TransMode { get; set; }
            public long Debug { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public byte UserAction { get; set; }
               public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public long ReasonID { get; set; }
        }
        public class UpdateMediaSubMaster
        {

            public long ID_MediaSubMaster { get; set; }    
             public string SubMdaName { get; set; }
            public string SubMdaShortName { get; set; }
            public short FK_Media { get; set; }
            public Int32 SortOrder { get; set; }
            public long FK_MediaSubMaster { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string TransMode { get; set; }
            public long Debug { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public byte UserAction { get; set; }

        }
        public class InputMediaSubMasterID
        {
            public Int64 ID_MediaSubMaster { get; set; }
            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }

            public long FK_BranchCodeUser { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string Name { get; set; }
        }

        public class MediaSubMasterView
        {

            public long SlNo { get; set; }
            public long MediaSubMasterID { get; set; }
            public string SubMediaName { get; set; }
            public string SubMediaShortName { get; set; }
            public short FK_Media { get; set; }
            public Int32 SortOrder { get; set; }
            public string MediaTypeName { get; set; }
            public Int64 TotalCount { get; set; }
            public string TransMode { get; set; }

        }
        public class MediaSubMasterInfoView
        {
            [Required(ErrorMessage = "No media Selected")]
            public Int64 MediaSubMasterID { get; set; }
            [Required(ErrorMessage = "Please select the media type")]
            public Int64 FK_Media { get; set; }

        }
        public class DeleteMediaSubMaster
        {
            public Int64 ID_MediaSubMaster { get; set; }
            public string TransMode { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long Debug { get; set; }
            public long FK_Reason { get; set; }
            public long FK_BranchCodeUser { get; set; }
        }

        public static string _updateProcedureName = "proMediaSubMasterUpdate";
        public static string _selectProcedureName = "proMediaSubMasterSelect";
        public static string _deleteProcedureName = "proMediaSubMasterDelete";

        public Output UpdateMediaSubMasterData(UpdateMediaSubMaster input, string companyKey)
        {
            return Common.UpdateTableData<UpdateMediaSubMaster>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public APIGetRecordsDynamic<MediaSubMasterView> GetMediaSubMasterData(InputMediaSubMasterID input, string companyKey)
        {
            return Common.GetDataViaProcedure<MediaSubMasterView, InputMediaSubMasterID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }
    }
}