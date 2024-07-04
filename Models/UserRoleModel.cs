//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace PerfectWebERP.Models
//{
//    public class UserRoleModel
//    {
//    }
//

/*----------------------------------------------------------------------
Created By	: Amith surya
Created On	: 07/02/2022
Purpose		: UserRole
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
    public class UserRoleModel
    {

        public class UserRoleView
        {
            public long UserRoleID { get; set; }
            public Int64 ReasonID { get; set; }
            [Required(ErrorMessage = "Please Enter User Role Name")]
            public string UserRoleName { get; set; }
            [Required(ErrorMessage = "Please Enter User Role Short Name")]
            public string UserRoleShortName { get; set; }
            [Required(ErrorMessage = "Please Enter  Admin")]
            public bool UsrrlAdmin { get; set; }
            public bool UsrrlManager { get; set; }
            [Required(ErrorMessage = "Please Enter User Role Opr")]
            public bool UsrrlOpr { get; set; }
            [Required(ErrorMessage = "Please Enter User Role Type Add")]
            public bool UsrrlTypeAdd { get; set; }
            [Required(ErrorMessage = "Please Enter User Role Type Edt")]
            public bool UsrrlTypeEdit { get; set; }
            [Required(ErrorMessage = "Please Enter User Role Type Del")]
            public bool UsrrlTypeDelete { get; set; }
            [Required(ErrorMessage = "Please Enter User Role Settigs Add")]
            public bool UsrrlSettingsAdd { get; set; }
            [Required(ErrorMessage = "Please Enter User Role Settigs Edt")]
            public bool UsrrlSettingsEdit { get; set; }
            [Required(ErrorMessage = "Please Enter User Role Settigs Del")]
            public bool UsrrlSettingsDelete { get; set; }
            [Required(ErrorMessage = "Please Enter User Role Ms Add")]
            public bool UsrrlMsAdd { get; set; }
            [Required(ErrorMessage = "Please Enter User Role Ms Edit")]
            public bool UsrrlMsEdit { get; set; }
            [Required(ErrorMessage = "Please Enter User Role Ms Delete")]
            public bool UsrrlMsDelete { get; set; }
            //[Required(ErrorMessage = "Please Enter User Role Msth")]
            //public bool UsrrlMsth { get; set; }
            [Required(ErrorMessage = "Please Enter User Role Tr Add")]
            public bool UsrrlTrAdd { get; set; }
            [Required(ErrorMessage = "Please Enter User Role Tr Edit")]
            public bool UsrrlTrEdit { get; set; }
            [Required(ErrorMessage = "Please Enter rUser Rolel Tr Delete")]
            public bool UsrrlTrDelete { get; set; }
            //[Required(ErrorMessage = "Please Enter User Role Auth")]
            //public bool UsrrlAuth { get; set; }
            [Required(ErrorMessage = "Please Enter User Role Br Cls")]
            public bool UsrrlBrCls { get; set; }
            //[Required(ErrorMessage = "Please Enter User Role Type Settigs Rpt")]
            //public bool UsrrlTypeSettingsRpt { get; set; }
            //[Required(ErrorMessage = "Please Enter User Role Ms Rpt")]
            //public bool UsrrlMsRpt { get; set; }
            //[Required(ErrorMessage = "Please Enter User Role Tr Rpt")]
            //public bool UsrrlTrRpt { get; set; }
            [Required(ErrorMessage = "Please Enter User Role Dash Board")]
            public bool UsrrlDashBoard { get; set; }
            //[Required(ErrorMessage = "Please Enter User Role Acc Rpt")]
            //public bool UsrrlAccRpt { get; set; }
            [Required(ErrorMessage = "Please Enter User Role M I S Rpt")]
            public bool UsrrlMISRpt { get; set; }
            // [Required(ErrorMessage = "Please Enter Sort Order")]


            public bool UsrrlTypeView { get; set; }
            public bool UsrrlApplicationUser { get; set; }
            public bool UsrrlSettingView { get; set; }
            public bool UsrrlMasterView { get; set; }
            public bool UsrrlTransactionView { get; set; }
            public bool UsrrlViewReport { get; set; }

            public bool UsrrlSecurtyUser { get; set; }
            public bool UsrrlSecurtyBackUp { get; set; }
            public bool UsrrlSecurtyAuth { get; set; }
            public bool UsrrlDayBegin { get; set; }
            public bool UsrrlPrintReport { get; set; }
            public bool UsrrlPrintVoucher { get; set; }

            public Int32 SortOrder { get; set; }
             public Int64 TotalCount { get; set; }
            public bool UsrrlEditSalesPrice { get; set; }
        }
        public class UserRoleViewList
        {

            public Int32 SortOrder { get; set; }
        }
        public class Settings
        {
            public bool GsValue { get; set; }
            public string GsField { get; set; }

        }
        public class UpdateUserRole
        {
            public Int32 UserAction { get; set; }
            public long ID_UserRole { get; set; }
            public string UsrrlName { get; set; }
            public string UsrrlShortName { get; set; }
            public bool UsrrlAdmin { get; set; }
            public bool UsrrlManager { get; set; }
            public bool UsrrlOpr { get; set; }
            public bool UsrrlTyAdd { get; set; }
            public bool UsrrlTyEdt { get; set; }
            public bool UsrrlTyDel { get; set; }
            public bool UsrrlStAdd { get; set; }
            public bool UsrrlStEdt { get; set; }
            public bool UsrrlStDel { get; set; }
            public bool UsrrlMsAdd { get; set; }
            public bool UsrrlMsEdt { get; set; }
            public bool UsrrlMsDel { get; set; }
           // public bool UsrrlMsth { get; set; }
            public bool UsrrlTrAdd { get; set; }
            public bool UsrrlTrEdt { get; set; }
            public bool UsrrlTrDel { get; set; }
          //  public bool UsrrlAuth { get; set; }
            public bool UsrrlBrCls { get; set; }
            //public bool UsrrlTyStRpt { get; set; }
            //public bool UsrrlMsRpt { get; set; }
           // public bool UsrrlTrRpt { get; set; }
            public bool UsrrlDashBoard { get; set; }
           // public bool UsrrlAccRpt { get; set; }
            public bool UsrrlMISRpt { get; set; }
            public string EntrBy { get; set; }

            public bool UsrrlAppUser { get; set; }
            public bool UsrrlTyView { get; set; }
            public bool UsrrlStView { get; set; }
            public bool UsrrlMsView { get; set; }
            public bool UsrrlTrView { get; set; }
            public bool UsrrlViewRpt { get; set; }
            public bool UsrrlSecurUser { get; set; }
            public bool UsrrlSecurBackUp { get; set; }
            public bool UsrrlSecurAuth { get; set; }
           
            public bool UsrrlDayBeg { get; set; }
            public bool UsrrlPtRpt { get; set; }
            public bool UsrrlPtVoucher { get; set; }


            public Int32 SortOrder { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
           
          
            public long FK_Machine { get; set; }
            public long BackupId { get; set; }
            public long FK_UserRole { get; set; }
            public bool UsrrlEditSalesPrice { get; set; }
        }
        public static string _deleteProcedureName = "ProUserRoleDelete";
        public static string _updateProcedureName = "ProUserRoleUpdate";
        public static string _selectProcedureName = "ProUserRoleSelect";

        public class DeleteUserRole
        {
            public long FK_UserRole { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public Int64 FK_Reason { get; set; }
            public Int64 FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
        }

        //public class DeleteSubCategory
        //{
        //    public long FK_SubCategory { get; set; }

        //    public string EntrBy { get; set; }
           
            
        //    public long FK_Machine { get; set; }
        //    public long FK_Branch { get; set; }
         

        //}


        public class UserRoleID
        {
            public Int64 ID_UserRole { get; set; }
            public string UsrrlName { get; set; }
            public string UsrrlShortName { get; set; }
            public bool UsrrlAdmin { get; set; }
            public bool UsrrlOpr { get; set; }
            public bool UsrrlTyAdd { get; set; }
            public bool UsrrlTyEdt { get; set; }
            public bool UsrrlTyDel { get; set; }
            public bool UsrrlStAdd { get; set; }
            public bool UsrrlStEdt { get; set; }
            public bool UsrrlStDel { get; set; }
            public bool UsrrlMsAdd { get; set; }
            public bool UsrrlMsEdt { get; set; }
            public bool UsrrlMsDel { get; set; }
            public bool UsrrlMsth { get; set; }
            public bool UsrrlTrAdd { get; set; }
            public bool UsrrlTrEdt { get; set; }
            public bool UsrrlTrDel { get; set; }
            public bool UsrrlAuth { get; set; }
            public bool UsrrlBrCls { get; set; }
            public bool UsrrlTyStRpt { get; set; }
            public bool UsrrlMsRpt { get; set; }
            public bool UsrrlTrRpt { get; set; }
            public bool UsrrlDashBoard { get; set; }
            public bool UsrrlAccRpt { get; set; }
            public bool UsrrlMISRpt { get; set; }
            public Int32 SortOrder { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public bool Passed { get; set; }
            public bool Cancelled { get; set; }
            public string EntrBy { get; set; }
            public DateTime EntrOn { get; set; }
            public string CancelBy { get; set; }
            public DateTime CancelOn { get; set; }
            public long FK_Reason { get; set; }
            public long FK_Machine { get; set; }
            public long BackupId { get; set; }
            public long FK_UserRole { get; set; }

        }

        public class GetUserRole //Common Procedure
        {
            public Int64 FK_UserRole { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public Int64 FK_Machine { get; set; }

            public Int64 FK_Company { get; set; }

            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string Name { get; set; }
        }

        public class SortList
        {
            public int SortOrder { get; set; }
        }

        public Output UpdateUserRoleData(UpdateUserRole input, string companyKey)
        {
            return Common.UpdateTableData<UpdateUserRole>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteUserRoleData(DeleteUserRole input, string companyKey)
        {
            return Common.UpdateTableData<DeleteUserRole>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<UserRoleView> GetUserRoleData(GetUserRole input, string companyKey)
        {
            return Common.GetDataViaProcedure<UserRoleView, GetUserRole>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
    }
}
