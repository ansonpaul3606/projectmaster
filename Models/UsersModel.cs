/*----------------------------------------------------------------------
Created By	: Amritha Ak
Created On	: 21/02/2022
Purpose		: users
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
    public class usersModel
    {

        public class usersView
        {

            public long SlNo { get; set; }

            [Range(1, long.MaxValue, ErrorMessage = "Select Employee")]
            public long? EmployeeID { get; set; }
            public string Employee { get; set; }
            [Required(ErrorMessage = "Please Enter User Code")]
            public long UsersID{ get; set; }
            public string UserCode { get; set; }
            [Range(1, long.MaxValue, ErrorMessage = "Select User Role")]
            public long UserRoleID { get; set; }
            [Range(1, long.MaxValue, ErrorMessage = "Select Branch")]
            public long BranchID { get; set; }
            public string Branch { get; set; }
            [Range(1, long.MaxValue, ErrorMessage = "Select Branch Type")]
            public long BranchTypeID { get; set; }
            
            public string BranchType { get; set; }

            public long BranchModeID { get; set; }
            //public string Password { get; set; }

           


         
          
            public long CashLimit { get; set; }
            public string UserPassword { get; set; }

          

           
            public string UserRole { get; set; }
            public long ModeID { get; set; }
            public long Debug { get; set; }
            public long FK_Reason { get; set; }
            public Int64 TotalCount { get; set; }
            public bool UserMrpEdit { get; set; }
            public bool UserPriceEdit { get; set; }
            //public int FK_Branch { get; set; }
            //public int FK_BranchMode { get; set; }
            //public int FK_Company { get; set; }
            public string Mobile { get; set; }
            public string Email { get; set; }
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public long CountryID { get; set; }
            public long StatesID { get; set; }
            public long DistrictID { get; set; }
            public long AreaID { get; set; }
            public long PostID { get; set; }
        }

        public class Updateusers
        {
            public long ID_Users { get; set; }

            public string UserCode { get; set; }

            public long UserCashlimit { get; set; }
            public string UserPassword { get; set; }

            public long FK_BranchType { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Employee { get; set; }
            public long FK_UserRole { get; set; }

            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public byte UserAction { get; set; }
            public long FK_Users { get; set; }
            public bool UserMrpEdit { get; set; }
            public bool UserPriceEdit { get; set; }

            public long BranchModeID { get; set; }
            public string Employee { get; set; }
            public string Mobile { get; set; }
            public string Email { get; set; }
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public long CountryID { get; set; }
            public long StatesID { get; set; }
            public long DistrictID { get; set; }
            public long AreaID { get; set; }
            public long PostID { get; set; }
            // public DateTime UserExpDate { get; set; }




        }
        public class status
        {
            public Boolean IsAdded { get; set; }
        }
        public class userrelateddata
        {
            public long BranchID { get; set; }
            public long BranchModeID { get; set;}
            public long Fk_Company { get; set; }
        }

        public class Userlistrelated
        {
            public   long FK_Branch { get; set; }
            public long FK_BranchMode { get; set; }
            public long FK_Company { get; set; }
        }


        public static string _deleteProcedureName = "ProUsersDelete";
        public static string _updateProcedureName = "ProUsersUpdate";
        public static string _selectProcedureName = "ProUsersSelect";
        public static string _selectProcedureNameEmpCust = "ProUsersEmployeeCustomeroOthrlistusingBranchmode";
        public class Deleteusers
        {
            public long FK_users { get; set; }
            public string TransMode { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long Debug { get; set; }
            public long FK_Reason { get; set; }
        }

        public class UsersInfoView
        {
            public long ID_Users { get; set; }

            public long ReasonID { get; set; }

        }


        public class UsersViewList
        {
            public List<Userrole> UserRolelists { get; set; }
            public List<Branchs> BranchList { get; set; }
            public List<BranchTypes> BranchTypelists { get; set; }
           
            public Int32 SortOrder { get; set; }

        }

        public class BranchTypes
        {
            public int BranchTypeID { get; set; }
            public string BranchType { get; set; }

            public long BranchModeID { get; set; }
            //public long FK_BranchType { get; set; }

        }
        public class Branchs
        {
            public int BranchID { get; set; }
            public string Branch { get; set; }
            //public long FK_Branch { get; set; }


        }

        public class Userrole
        {
            public int UserRoleID { get; set; }
            public string UserRole { get; set; }
            //public long FK_UserRole { get; set; }

        }


        public class Employees
        {
            public int EmployeeID { get; set; }
            public string Name { get; set; }
            public string Code { get; set; }
            public string Phone { get; set; }
            public string Address { get; set; }
           // public string Email { get; set; }
            //public long FK_Employee { get; set; }


        }
        ////public class Customer
        ////{
        ////    public int CustomerID { get; set; }
        ////    public string CustomerName { get; set; }
        ////    public long FK_Customer { get; set; }


        //}
        //public class test
        //{
        //    public dynamic data  { get; set; }
          
        //}

        public class BranchModeID
        {
            public long FK_BranchModeID { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Company { get; set; }
        }
            public class usersID
        {
            public Int64 FK_users { get; set; }

            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }

            public string Name { get; set; }

        }
        public Output UpdateusersData(Updateusers input, string companyKey)
        {
            return Common.UpdateTableData<Updateusers>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteusersData(Deleteusers input, string companyKey)
        {
            return Common.UpdateTableData<Deleteusers>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<usersView> GetusersData(usersID input, string companyKey)
        {
            return Common.GetDataViaProcedure<usersView, usersID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
        public APIGetRecordsDynamic<usersView> GetEmployeeCustomerotherData(BranchModeID input, string companyKey)
        {
            return Common.GetDataViaProcedure<usersView, BranchModeID>(companyKey: companyKey, procedureName: _selectProcedureNameEmpCust, parameter: input);
        }
    }
}


