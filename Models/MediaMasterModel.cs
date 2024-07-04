using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class MediaMasterModel
    {

        public class MediaMasterView
        {

            public long SlNo { get; set; }
            public long MediaMasterID { get; set; }


            public string MediaName { get; set; }

            public string MediaShortName { get; set; }



            public short MediaTypeID { get; set; }

            public Int32 SortOrder { get; set; }

            public string MediaTypeName { get; set; }

            public Int64 TotalCount { get; set; }
            public string TransMode { get; set; }

        }

        public class MediaMasterInputFromViewModel
        {
            public long SlNo { get; set; }
            //  [Required(ErrorMessage = "No Media Type Selected")]
            public long MediaMasterID { get; set; }

            [Required(ErrorMessage = "Please Enter Name")]
            public string MediaName { get; set; }
            [Required(ErrorMessage = "Please Enter Short Name ")]
            public string MediaShortName { get; set; }


            [Range(1, long.MaxValue, ErrorMessage = "Select Media Type")]
            public short MediaTypeID { get; set; }

            public Int32 SortOrder { get; set; }

            public long ReasonID { get; set; }
            public string TransMode { get; set; }

        }

        public class MediaMasterInfoView
        {
            [Required(ErrorMessage = "No media Selected")]
            public Int64 MediaMasterID { get; set; }
            [Required(ErrorMessage = "Please select the media type")]
            public Int64 FK_MediaTypeID { get; set; }

        }


        public static string _updateProcedureName = "proMediaMasterUpdate";
        public class UpdateMediaMaster
        {

            public long ID_MediaMaster { get; set; }
            public short FK_MediaType { get; set; }

            public string MdaShortName { get; set; }
            public string MdaName { get; set; }
            public Int32 SortOrder { get; set; }
             public long FK_MediaMaster { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string TransMode { get; set; }
            public long Debug { get; set; }
            public string EntrBy { get; set; }
            //public long FK_Reason { get; set; }
            public long FK_Machine { get; set; }
            public byte UserAction { get; set; }

        }

        public class SelectMediaMaster
        {

            public long ID_MediaMaster { get; set; }
            public short FK_MediaType { get; set; }

            public string MdaShortName { get; set; }
            public string MdaName { get; set; }
            public Int32 SortOrder { get; set; }
            public Int64 FK_Company { get; set; }
            public byte UserAction { get; set; }
            public Int64 FK_Machine { get; set; }
            public string UserCode { get; set; }
            public Int32 BranchCode { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string AuditData { get; set; }
            public string SqlUpdateQuery { get; set; }
            public Int64 FK_Reason { get; set; }
            public string EntrBy { get; set; }
            public Int64 BackupId { get; set; }
            public bool Cancelled { get; set; }

        }

        public static string _deleteProcedureName = "proMediaMasterDelete";
        public class DeleteMediaMaster
        {
            public Int64 ID_MediaMaster { get; set; }
            public string TransMode { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long Debug { get; set; }
            public long FK_Reason { get; set; }
            public long FK_BranchCodeUser { get; set; }
        }

        public class MediaMaster
        {
            public short MediaTypeID { get; set; }
            public string MediaTypeName { get; set; }
        }


        public class MediaMasterListModel
        {
            public List<MediaMaster> MediaTypeList { get; set; }
            public int SortOrder { get; set; }

        }

        public class InputMediaMasterID
        {
            public Int64 ID_MediaMaster { get; set; }
            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }

            public long FK_BranchCodeUser { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string Name { get; set; }



        }


        public Output UpdateMediaMasterData(UpdateMediaMaster input, string companyKey)
        {
            return Common.UpdateTableData<UpdateMediaMaster>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteMediaMasterData(DeleteMediaMaster input, string companyKey)
        {
            return Common.UpdateTableData<DeleteMediaMaster>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }


        public static string _selectProcedureName = "proMediaMasterSelect";
        public APIGetRecordsDynamic<MediaMasterView> GetMediaMasterData(InputMediaMasterID input, string companyKey)
        {
            return Common.GetDataViaProcedure<MediaMasterView, InputMediaMasterID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }



    }
}