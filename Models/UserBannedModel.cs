/*----------------------------------------------------------------------
Created By	: AmrithaAk
Created On	: 26/03/2022
Purpose		: UserBanned
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
    public class UserBannedModel
    {

        public class UserBannedView
        {
            public long SlNo { get; set; }
            public long UserBannedID { get; set; }
            public int BranchID { get; set; }
            public string Branch { get; set; }
            public int UsersID { get; set; }
            public string UserCode { get; set; } 
            public byte BanMethodID { get; set; }
            public string BanMethodName { get; set; }
            public DateTime UbFromDate { get; set; }
            public DateTime? UbToDate { get; set; }

            public string UserReasonBan { get; set;}
            public string UserReasonUnBan { get; set; }

            // public long ReasonBanID { get; set; }
            public Int64 TotalCount { get; set; }

        }

        public class UserbanViewList
        {
            public List<BanMethod> BanMethodList { get; set; }
            public List<Branchs> BranchList { get; set; }
           // public List<ReasonBan> ReasonBanList { get; set; }


        }

        public class BanMethod
        {
            public int BanMethodID { get; set; }
            public string BanMethodName { get; set; }
            // public long FK_States { get; set; }

        }
        //public class ReasonBan
        //{
        //    public int ReasonBanID { get; set; }
        //    public string ReasonBanName { get; set; }
        //    // public long FK_States { get; set; }

        //}

        public class Branchs
        {
            public int BranchID { get; set; }
            public string Branch { get; set; }


        }

        public class Users
        {
            public int UsersID { get; set; }

            public string UserName { get; set; }
            public string UserCode { get; set; }
            public string BranchType { get; set; }
          
            //public string UserCodenumber { get; set; }
        }
        public class BranchID
        {
            public Int64 FK_Branch { get; set; }
            // public Int64 FK_Branch { get; set; }
            public Int64 FK_Company { get; set; }
        }

        public class UpdateUserBanned
            {
                public long ID_UserBanned { get; set; }
                public long FK_User { get; set; }
                public byte UbMethod { get; set; }
                public DateTime UbFromDate { get; set; }
                public DateTime? UbToDate { get; set; }
                public bool UbBanned { get; set; }
                public String  UbReasonBan { get; set; }
                public string UbBanBy { get; set; }
               // public DateTime UbBanOn { get; set; }
                public String UbReasonUnBan { get; set; }

                public long FK_Company { get; set; }
                public long FK_BranchCodeUser { get; set; }
                public string TransMode { get; set; }
                public long Debug { get; set; }
                public string EntrBy { get; set; }
                public long FK_Branch { get; set; }

                public long FK_Machine { get; set; }
                public byte UserAction { get; set; }
               public long FK_UserBanned { get; set; }

        }
            public static string _deleteProcedureName = "ProUserBannedDelete";
            public static string _updateProcedureName = "ProUserBannedUpdate";
            public static string _selectProcedureName = "ProUserBannedSelect";

            public class DeleteUserBanned
            {
                public long FK_UserBanned { get; set; }
                public string TransMode { get; set; }
                public long FK_Company { get; set; }
                public string EntrBy { get; set; }
                public long FK_Machine { get; set; }
                public long Debug { get; set; }
                public long FK_Reason { get; set; }
                public long FK_BranchCodeUser { get; set; }
            }


            public class UserBannedID
            {
                public Int64 ID_UserBanned { get; set; }
                public Int64 FK_Company { get; set; }
                public string EntrBy { get; set; }
                public Int64 FK_Machine { get; set; }
             public long FK_BranchCodeUser { get; set; }
             public string TransMode { get; set; }
                public Int32 PageIndex { get; set; }
                public Int32 PageSize { get; set; }
            public string Name { get; set; }
        }
            public class UserBannInfoView
            {
                public long ID_UserBanned { get; set; }

                public long ReasonID { get; set; }
            }

            public Output UpdateUserBannedData(UpdateUserBanned input, string companyKey)
            {
                return Common.UpdateTableData<UpdateUserBanned>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
            }
            public Output DeleteUserBannedData(DeleteUserBanned input, string companyKey)
            {
                return Common.UpdateTableData<DeleteUserBanned>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
            }
            public APIGetRecordsDynamic<UserBannedView> GetUserBannedData(UserBannedID input, string companyKey)
            {
                return Common.GetDataViaProcedure<UserBannedView, UserBannedID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
            }
        }
    }



