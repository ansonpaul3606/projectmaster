using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class User
    {
        public class Login
        {
            [Required(ErrorMessage = "Please Enter User Code")]
            public string Username { get; set; }
            [Required(ErrorMessage = "Please Enter Password")]
            public string Passsword { get; set; }
        }
        public class UserTable
        {
            public string UserCode { get; set; }
            public string Password { get; set; }
            public long FK_Machine { get; set; }
            public long FK_Company { get; set; }
            public long Mode { get; set; }
            //public long CheckMode { get; set; }
            public Boolean Debug { get; set; }

            
        }

        public class AttemptCount
        {
            public long ID_Users { get; set; }

            public long LoginAttempt { get; set; }
            public string UserCode { get; set; }
        }
        public class LoginAttempt
        {
            
            public long ID_Users { get; set; }

            public Boolean Sucess { get; set; }
        }

    }
    public class UserForgotPasswordInfo
    {
        public string ErrCode { get; set; }
        public string ErrMsg { get; set; }
        public string UserEmail { get; set; }
        public string UserMobile { get; set; }
        public long ID_Users { get; set; }
        public long FK_Company { get; set; }
        public string EmployeeName { get; set; }


    }
    public class UserLoginInfo
    {
        public short FK_Machine { get; set; }
        public string EntrBy { get; set; }
        public string CompanyKey { get; set; }
        public int FK_Company { get; set; }
        public int FK_Employee { get; set; }
        public Int16 FK_Branch { get; set; }
        public Int16 FK_BranchCodeUser { get; set; }
        public Int16 FK_Department { get; set; }
        public string UserName { get; set; }
        public int FK_UserRole { get; set; }
        public string UserRole { get; set; }

        public long FK_Country { get; set; }
        public string CntryName { get; set; }
        public long FK_States { get; set; }
        public string StName { get; set; }
        public long FK_District { get; set; }
        public string DtName { get; set; }

        public string UserAvatar { get; set; }
        public string Password { get; set; }
        public long ID_Users { get; set; }
        public Boolean UsrrlTyAdd { get; set; }
        public Boolean UsrrlTyEdt { get; set; }
        public Boolean UsrrlTyDel { get; set; }
        public Boolean UsrrlTyView { get; set; }
        public Boolean UsrrlStAdd { get; set; }
        public Boolean UsrrlStEdt { get; set; }
        public Boolean UsrrlStDel { get; set; }
        public Boolean UsrrlStView { get; set; }
        public Boolean UsrrlMsAdd { get; set; }
        public Boolean UsrrlMsEdt { get; set; }
        public Boolean UsrrlMsDel { get; set; }
        public Boolean UsrrlMsView { get; set; }
        public Boolean UsrrlMsth { get; set; }
        public Boolean UsrrlTrAdd { get; set; }
        public Boolean UsrrlTrEdt { get; set; }

        public Boolean UsrrlTrDel { get; set; }
        public Boolean UsrrlTrView { get; set; }

        public Boolean UsrrlAppUser { get; set; }
        public Boolean UsrrlViewRpt { get; set; }
        public Boolean UsrrlSecurUser { get; set; }
        public Boolean UsrrlSecurBackUp { get; set; }
        public Boolean UsrrlSecurAuth { get; set; }
        public Boolean UsrrlDayBeg { get; set; }
        public Boolean UsrrlPtRpt { get; set; }
        public Boolean UsrrlPtVoucher { get; set; }
        public Boolean UsrrlAdmin { get; set; }
        public Boolean UsrrlManager { get; set; }
        public Boolean UserEditSalprice { get; set; }
        public DateTime? UserpasswordExpDate { get; set; }
        public DateTime? UserExpDate { get; set; }
        public UserAcssRightInfo PageAccessRights { get; set; }

        public string ErrCode { get; set; }

        public string ErrMsg { get; set; }

        public string UserImage { get; set; }

        public string Branch { get; set; }
        public string Company { get; set; }
        public string CompanyLogo { get; set; }
        public string CompCategory { get; set; }
        public int ProductType { get; set; }
        public bool UserMrpEdit { get; set; }
        public bool UserPriceEdit { get; set; }
        public Int32 RemainingDays { get; set; }
    }

    public class UserChangeInfo
    {
        public short FK_Machine { get; set; }
        public string EntrBy { get; set; }
        public string CompanyKey { get; set; }
        public int FK_Company { get; set; }
        public int FK_Employee { get; set; }
        public Int16 FK_Branch { get; set; }
        public Int16 FK_BranchCodeUser { get; set; }
        public string UserName { get; set; }
        public int FK_UserRole { get; set; }
        public string UserRole { get; set; }

        public string UserAvatar { get; set; }
        public string Password { get; set; }
        public long ID_Users { get; set; }
        public Boolean UsrrlTyAdd { get; set; }
        public Boolean UsrrlTyEdt { get; set; }
        public Boolean UsrrlTyDel { get; set; }
        public Boolean UsrrlTyView { get; set; }
        public Boolean UsrrlStAdd { get; set; }
        public Boolean UsrrlStEdt { get; set; }
        public Boolean UsrrlStDel { get; set; }
        public Boolean UsrrlStView { get; set; }
        public Boolean UsrrlMsAdd { get; set; }
        public Boolean UsrrlMsEdt { get; set; }
        public Boolean UsrrlMsDel { get; set; }
        public Boolean UsrrlMsView { get; set; }
        public Boolean UsrrlMsth { get; set; }
        public Boolean UsrrlTrAdd { get; set; }
        public Boolean UsrrlTrEdt { get; set; }

        public Boolean UsrrlTrDel { get; set; }
        public Boolean UsrrlTrView { get; set; }

        public Boolean UsrrlAppUser { get; set; }
        public Boolean UsrrlViewRpt { get; set; }
        public Boolean UsrrlSecurUser { get; set; }
        public Boolean UsrrlSecurBackUp { get; set; }
        public Boolean UsrrlSecurAuth { get; set; }
        public Boolean UsrrlDayBeg { get; set; }
        public Boolean UsrrlPtRpt { get; set; }
        public Boolean UsrrlPtVoucher { get; set; }
        public Boolean UsrrlAdmin { get; set; }

        public DateTime UserpasswordExpDate { get; set; }
        public UserAcssRightInfo PageAccessRights { get; set; }

    }
    
    public class  UserLoginExpired
    {
        public Boolean Expired { get; set; }

        public Boolean ExpiryDate { get; set; }
    }
    public class UserAcssRightInfo
    {
        public Boolean UsrrlTyAdd { get; set; }
        public Boolean UsrrlTyEdt { get; set; }
        public Boolean UsrrlTyDel { get; set; }
        public Boolean UsrrlTyView { get; set; }
        public Boolean UsrrlStAdd { get; set; }
        public Boolean UsrrlStEdt { get; set; }
        public Boolean UsrrlStDel { get; set; }
        public Boolean UsrrlStView { get; set; }
        public Boolean UsrrlMsAdd { get; set; }
        public Boolean UsrrlMsEdt { get; set; }
        public Boolean UsrrlMsDel { get; set; }
        public Boolean UsrrlMsView { get; set; }
        
        public Boolean UsrrlMsth { get; set; }
        public Boolean UsrrlTrAdd { get; set; }
        public Boolean UsrrlTrEdt { get; set; }

        public Boolean UsrrlTrDel { get; set; }
        public Boolean UsrrlTrView { get; set; }

        public Boolean UsrrlAppUser { get; set; }
        public Boolean UsrrlViewRpt { get; set; }
        public Boolean UsrrlSecurUser { get; set; }
        public Boolean UsrrlSecurBackUp { get; set; }
        public Boolean UsrrlSecurAuth { get; set; }
        public Boolean UsrrlDayBeg { get; set; }
        public Boolean UsrrlPtRpt { get; set; }
        public Boolean UsrrlPtVoucher { get; set; }
        public Boolean UsrrlAdmin { get; set; }
        public Boolean UserEditSalprice { get; set; }
    }

 

}
