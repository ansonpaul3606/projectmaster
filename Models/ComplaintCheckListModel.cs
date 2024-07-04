/*----------------------------------------------------------------------
Created By	: Athul M
Created On	: 31/01/2022
Purpose		: ComplaintCheckList
-------------------------------------------------------------------------
Modification
On			By					OMID/Remarks
-------------------------------------------------------------------------
-------------------------------------------------------------------------*/

using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PerfectWebERP.Models
{
    public class ComplaintCheckListModel
    {

        public class ComplaintCheckListView
        {
            public long SlNo { get; set; }
            public Int64 ID_ComplaintCheckList { get; set; }
            public string ComplaintCheckingDetails { get; set; }
            public Int32 CategoryID { get; set; }
            public string Category { get; set; }
            public long FK_Product { get; set; }
            
            public long FK_Complaint { get; set; }

            public string TransMode { get; set; }
            public List<ComplaintStatus> comlist { get; set; }
            public string Product { get; set; }
            public string Complaint { get; set; }
            public Int64 TotalCount { get; set; }

           
          
        }
        public class CategoryList
        {
            public Int32 CategoryID { get; set; }
            public string Category { get; set; }

        }





        public class UpdateComplaintCheckList
        {
            public int UserAction { get; set; }
            public int Debug { get; set; }
            public string TransMode { get; set; }
          
            public long ID_ComplaintCheckList { get; set; }
          
            public string ChklstCheckingDetails { get; set; }

            public long FK_Product { get; set; }
            public long FK_Category { get; set; }
            
            public long FK_Complaint { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long BackupId { get; set; }
            public string ComplaintCheckListDetails { get; set; }
            public Int64 FK_ComplaintCheckList { get; set; }

            
            
           
            
          
        }
        public static string _deleteProcedureName = "ProComplaintCheckListDelete";
        public static string _updateProcedureName = "ProComplaintCheckListUpdate";
        public static string _selectProcedureName = "ProComplaintCheckListSelect";
        public static string _selectComplaintDetails = "ProComplaintCheckListDetails";


        public class DeleteComplaintCheckList
        {
            public string TransMode { get; set; }
            public Int64 FK_ComplaintCheckList { get; set; }
            public int Debug { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Reason { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int32 FK_BranchCodeUser { get; set; }
        }


        public class DeleteView
        {

            public long ID_ComplaintCheckList { get; set; }
           
            public Int64 ReasonID { get; set; }
        }

        public class Productlist
        {
            public Int32 ProductID { get; set; }
            public string ProductName { get; set; }

        }
        public class ComplaintList
        {
            public int ComplaintListID { get; set; }
            public string Complaint { get; set; }
        }

        public class ComplaintCheckList
        {
            public long SlNo { get; set; }
            public long ID_ComplaintCheckList { get; set; }
            public List<Productlist> Productlists { get; set; }
            public List<ComplaintList> ComplaintLists { get; set; }
            public List<CategoryList> CategoryList { get; set; }
        }

        public class ComplaintStatus
        {
           
            public string Output { get; set; }
            public string ComStatus { get; set; }

        }




        public class ComplaintCheckListID
        {
            public Int64 FK_ComplaintCheckList { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_Company { get; set; }
            public string Name { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }

        }
        public Output UpdateComplaintCheckListData(UpdateComplaintCheckList input, string companyKey)
        {
            return Common.UpdateTableData<UpdateComplaintCheckList>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteComplaintCheckListData(DeleteComplaintCheckList input, string companyKey)
        {
            return Common.UpdateTableData<DeleteComplaintCheckList>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<ComplaintCheckListView> GetComplaintCheckListData(ComplaintCheckListID input, string companyKey)
        {
            return Common.GetDataViaProcedure<ComplaintCheckListView, ComplaintCheckListID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }

        public class ComplaintDetails
        {
            public Int64 FK_ComplaintCheckList { get; set; }
            public Int64 FK_Company { get;set;}


        }


        public APIGetRecordsDynamic<ComplaintStatus> GetComplaintDetails(ComplaintDetails input, string companyKey)
        {
            return Common.GetDataViaProcedure<ComplaintStatus, ComplaintDetails>(companyKey: companyKey, procedureName: _selectComplaintDetails, parameter: input);
        }

    }
}

