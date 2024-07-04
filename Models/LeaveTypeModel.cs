/*----------------------------------------------------------------------
Created By	: AmrithaAk
Created On	: 09/03/2023
Purpose		: LeaveType
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
    public class LeaveTypeModel
    {

        public class LeaveTypeView
        {
            public long ID_LeaveType { get; set; }
            public long SlNo { get; set; }
            public string LTName { get; set; }
           
            public string LTShortName { get; set; }
          
            public Int64 LTMode { get; set; }

            public Int32 SortOrder { get; set; }
            public Int64 TotalCount { get; set; }


        }

        public class Leavetypeviewlist
        {
            public List<LTModelist> LTModelist { get; set; }
            public Int32 SortOrder { get; set; }
        }
        public class LTModelist
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class UpdateLeaveType
        {
            public int UserAction { get; set; }
            public long ID_LeaveType { get; set; }
            public string LTName { get; set; }
            public string LTShortName { get; set; }
            public Int64 LTMode { get; set; }
            public Int32 SortOrder { get; set; }
           
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
          
            public long FK_LeaveType { get; set; }

           

        }
        public static string _deleteProcedureName = "ProLeavetypeDelete";
        public static string _updateProcedureName = "ProLeavetypeUpdate";
        public static string _selectProcedureName = "ProLeavetypeSelect";

        public class DeleteLeaveType
        {
            public long ID_LeaveType { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
        }
        public class ModeLeave
        {
            public Int32 Mode { get; set; }
        }

        public class LeaveTypeID
        {
            public Int64 ID_LeaveType { get; set; }
        }

        public class LeaveTypelistID
        {
            public long FK_LeaveType { get; set; }
            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public Int32 PageSize { get; set; }
            public string Name { get; set; }
           
        }
       

        public class LeaveTypeRsnView
        {
            public long ID_LeaveType { get; set; }

            public long ReasonID { get; set; }
        }

        public class LeaveTypeDelete
        {
            public long FK_LeaveType { get; set; }
            public string TransMode { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long Debug { get; set; }
            public long FK_Reason { get; set; }
            public long FK_BranchCodeUser { get; set; }



        }
        public Output UpdateLeaveTypeData(UpdateLeaveType input, string companyKey)
        {
            return Common.UpdateTableData<UpdateLeaveType>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteLeaveTypeData(LeaveTypeDelete input, string companyKey)
        {
            return Common.UpdateTableData<LeaveTypeDelete>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<LeaveTypeView> GetLeaveTypeData(LeaveTypeID input, string companyKey)
        {
            return Common.GetDataViaProcedure<LeaveTypeView, LeaveTypeID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
        public APIGetRecordsDynamic<LTModelist> Getleavemodelst(ModeLeave input, string companyKey)
        {
            return Common.GetDataViaProcedure<LTModelist, ModeLeave>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }
        public APIGetRecordsDynamic<LeaveTypeView> GetLeaveTypeData(LeaveTypelistID input, string companyKey)
        {
            return Common.GetDataViaProcedure<LeaveTypeView, LeaveTypelistID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }
      
    }
}

