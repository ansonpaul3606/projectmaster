using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class ComplaintListModel
    {
        public class ComplaintView
        {
            public int ID_ComplaintList { get; set; }


            [Required(ErrorMessage = "Please Enter Complaint Type Name")]
            [StringLength(150, ErrorMessage = "Maximum 150 character")]
            public string ComplaintName { get; set; }
            [Required(ErrorMessage = "Please Enter Complaint Type Short Name")]
            [StringLength(10, ErrorMessage = "Maximum 10 characture")]
            public string ShortName { get; set; }

            public Int32 SortOrder { get; set; }      
            public int ComplaintType { get; set; }
            public string TransMode { get; set; }
            public Int64 TotalCount { get; set; }
            public string ComplaintTypeName { get; set; }
            [Required(ErrorMessage = "Please Enter Complaint Priority")]
            public Int32 ComplaintPriority { get; set; }


        }

        public class UpdateComplaintList
        {
            public byte UserAction { get; set; }
            public int Debug { get; set; }
            public string TransMode { get; set; }
            public long ID_ComplaintList{ get; set; }
            public string CompntName { get; set; }
            public string CompntShortName { get; set; }
            public Int32 CompntPriority { get; set; }
            public Int32 SortOrder { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }

            public string EntrBy { get; set; }
        
            public long FK_Machine { get; set; }
            public long BackupId { get; set; }
            public Int64 FK_ComplaintList { get; set; }
            public int ComplaintType { get; set; }



        }

        public static string _deleteProcedureName = "ProComplaintListDelete";
        public static string _updateProcedureName = "proComplaintListUpdate";
        public static string _selectProcedureName = "ProComplaintListSelect";

        public class DeleteView
        {

            public long ID_ComplaintList { get; set; }
            [Required(ErrorMessage = "Please Select a Reason")]
            public Int64 ReasonID { get; set; }
        }


        public class DeleteComplaintList
        {
            public string TransMode { get; set; }
            public Int64 FK_ComplaintList { get; set; }
            public int Debug { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Reason { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int32 FK_BranchCodeUser { get; set; }
        }

        public class Complaint_List
        {
            public int SortOrder { get; set; }
            public List<ComplaintList> ComplaintListData { get; set; }
        }

        public class GetComplaintList
        {
            public Int64 ID_ComplaintList { get; set; }
            public string UserCode { get; set; }
            public Int64 FK_Company { get; set; }


        }
        public class ComplaintListlID
        {
            public Int64 FK_ComplaintList { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_Company { get; set; }
            public string Name { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
        }
        public class ComplaintList
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class ModeValue
        {
            public Int32 Mode { get; set; }
        }
        public Output UpdateComplaintData(UpdateComplaintList input, string companyKey)
        {
            return Common.UpdateTableData<UpdateComplaintList>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteComplaintData(DeleteComplaintList input, string companyKey)
        {
            return Common.UpdateTableData<DeleteComplaintList>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<ComplaintView> GeComplaintData(ComplaintListlID input, string companyKey)
        {
            return Common.GetDataViaProcedure<ComplaintView, ComplaintListlID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }
        public APIGetRecordsDynamic<ComplaintList> GetComplaintListData(ModeValue input, string companyKey)
        {
            return Common.GetDataViaProcedure<ComplaintList, ModeValue>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);
        }

    }
}