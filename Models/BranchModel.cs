
/*----------------------------------------------------------------------
Created By	: Amritha A K
Created On	: 29/01/2022
Purpose		: Branch
-------------------------------------------------------------------------
Modification
On			By					OMID/Remarks
-------------------------------------------------------------------------
-------------------------------------------------------------------------*/
using PerfectWebERP.General;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace PerfectWebERP.Models
    {

        public class BranchModel
        {
        public class BranchView

        {
            public long SlNo { get; set; }
            public long BranchID { get; set; }
            [Required(ErrorMessage = "Please Enter  Code")]
            public Int32 Code { get; set; }
            [Required(ErrorMessage = "Please Enter  Name")]
            public string Name { get; set; }
            [Required(ErrorMessage = "Please Enter Short Name")]
            public string ShortName { get; set; }
            [Required(ErrorMessage = "Please Enter  Address1")]
            public string Address1 { get; set; }
            [Required(ErrorMessage = "Please Enter Address2")]
            public string Address2 { get; set; }
           // [Required(ErrorMessage = "Please Enter  Phone")]
            public string Phone { get; set; }
            [Required(ErrorMessage = "Please Enter  Mobile")]
            public string Mobile { get; set; }
            public string Email { get; set; }

            public string GSTNo { get; set; }
           
            // [Required(ErrorMessage = "Please Enter  Cash Position Limit")]
            public long CashPositionLimit { get; set; }
           // [Required(ErrorMessage = "Please Enter  Cash Re Order Level")]
            public long CashReOrderLevel { get; set; }
          //  [Required(ErrorMessage = "Please Enter  Latitude")]
            public string Latitude { get; set; }
           // [Required(ErrorMessage = "Please Enter Br Longitude")]
            public string Longitude { get; set; }
         //   [Required(ErrorMessage = "Please Enter  Start Time")]
            public string StartTime { get; set; }
          //  [Required(ErrorMessage = "Please Enter  End Time")]
            public string EndTime { get; set; }
           // [Required(ErrorMessage = "Please Enter Sort Order")]
            public Int32 SortOrder { get; set; }
            [Range(1, long.MaxValue, ErrorMessage = "Select Post")]
            [Required(ErrorMessage = "Please Enter Country")]
            public string Country { get; set; }

            public long CountryID { get; set; }
            [Required(ErrorMessage = "Please Enter State")]
            public string States { get; set; }

            public long StatesID { get; set; }
            [Required(ErrorMessage = "Please Enter District")]
            public string District { get; set; }
            [Required(ErrorMessage = "Please Enter Post")]
            public long DistrictID { get; set; }
         //   [Required(ErrorMessage = "Please Enter Area")]
            public string Area { get; set; }

            public long? AreaID { get; set; }

            [Required(ErrorMessage = "Please Enter Post")]
            public string Post { get; set; }
            public long ?PostID { get; set; }

            [Required(ErrorMessage = "Please Enter Place")]
            public string Place { get; set; }
            public long? PlaceID { get; set; }

            public string PinCode { get; set; }
            public long? EmployeeID { get; set; }
            public string Employee { get; set; }
           // [Range(1, long.MaxValue, ErrorMessage = "Select Branch Parent")]
           public long? BranchParentID { get; set; }
            public string BranchParent { get; set; }
            [Range(1, long.MaxValue, ErrorMessage = "Select Branch Type")]
            public long BranchTypeIDs { get; set; }
            public long BranchTypeID { get; set; }
            public string BranchTypess { get; set; }
            // [Range(1, long.MaxValue, ErrorMessage = "Select Account Head")]

            public int? AccountHead { get; set; }
            public int? AccountHeadSub { get; set; }
            public string AHeadName { get; set; }

            public int AccountCode { get; set; }
            public int AccountSHCode { get; set; }
            public string ASHeadName { get; set; }
            // [Range(1, lovvv
               public long FK_Machine { get; set; }
                public long Debug { get; set; }
                public long FK_Reason { get; set; }
            public Int64 TotalCount { get; set; }

            public string ContactPerson { get; set; }

            public string TransMode { get; set; }

            public List<ImageData> ImageList { get; set; }
            public List<BranchBankDetails> BranchBankDetails { get; set; }
            public long FK_IntroduceBranch { get; set; }
            public long BranchMode { get; set; }

        }


        public class BranchViewList
        {
            public List<Branch> BranchList { get; set; }
            public List<BranchTypes> BranchTypelists { get; set; }
            public List<BranchBankDetails> BranchBankDetails { get; set; }
            public Int32 SortOrder { get; set; }

        }
        public class BranchBankDetails
        {
            public Int16 ID_BranchBankDetails { get; set; }

            public string BankName { get; set; }

            public string BranchName { get; set; }
            public string BankAccount { get; set; }
            public string IFSCCode { get; set; }






        }

        public class BranchTypes
        {
            public long BranchTypeIDs { get; set; }
            public string BranchTypess { get; set; }
            public long BranchMode { get; set; }

          //  public long FK_BranchType { get; set; }

        }
        public class Branch
        {
            public int BranchParentID { get; set; }
            public string BranchParent { get; set; }
           // public long FK_BranchParent { get; set; }


        }
        public class Employees
        {
            public int EmployeeID { get; set; }
            public string Employee { get; set; }
           // public long FK_Employee{ get; set; }


        }

        public class UpdateBranch
        {
            public long ID_Branch { get; set; }
            public Int32 BrCode { get; set; }
            public string BrName { get; set; }
            public string BrShortName { get; set; }
            public string BrAddress1 { get; set; }
            public string BrAddress2 { get; set; }
            public string BrPhone { get; set; }
            public string GSTNo { get; set; }
            public string BrMobile { get; set; }
            public string BrEmail { get; set; }
            public long BrCashPositionLimit { get; set; }
            public long BrCashReOrderLevel { get; set; }
            public string BrLatitude { get; set; }
            public string BrLongitude { get; set; }
            public string BrStartTime { get; set; }
            public string BrEndTime { get; set; }
            public Int32 SortOrder { get; set; }
            public long FK_Post { get; set; }
            public long FK_Place { get; set; }
            public long FK_Area { get; set; }
            public long FK_District { get; set; }
            public long FK_States { get; set; }
            public long FK_Country { get; set; }
            public long FK_Employee { get; set; }
            public long FK_BranchParent { get; set; }
            public long FK_BranchType { get; set; }
            public Int32 FK_AccountHead { get; set; }
            public Int32 FK_AccountSubHead { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public byte UserAction { get; set; }
            public long FK_Branch { get; set; }
            public string TransMode { get; set; }
            public string BrContactPerson { get; set; }
            public string ImageList { get; set; }
            public string BranchBankDetails { get; set; }
            public long FK_IntroduceBranch { get; set; }
            public long BranchMode { get; set; }
        }
        public class DeleteBranch
        {
            public long ID_Branch { get; set; }
            public string TransMode { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long Debug { get; set; }
            public long FK_Reason { get; set; }
           // public long FK_BranchCodeUser { get; set; }
        }
        public class BranchInfoView
        {
            public long ID_Branch { get; set; }

            public long ReasonID { get; set; }
        }


        public class BranchID
            {
                public Int64 ID_Branch { get; set; }
                public Int64 FK_Company { get; set; }
                public string EntrBy { get; set; }
                public Int64 FK_Machine { get; set; }
                public string TransMode { get; set; }
               public Int32 PageIndex { get; set; }
              public Int32 PageSize { get; set; }
              public string Name { get; set; }
            public Int64 Mode { get; set; }
        }
        public class ImageData
        {

            public Int64 ID_CompanyImage { get; set; }
            public long FK_Company { get; set; }
            public int ComImgMode { get; set; }

            public string ComImg { get; set; }
            public string ComImgName { get; set; }

            public byte[] ComImgValue { get; set; }
        }
        public class BranchImages
        {
            public Int64 ID_BranchImage { get; set; }
            public int SVImgMode { get; set; }
            public string SVImgValue { get; set; }
            public string SVImgName { get; set; }
            public string SVModeName { get; set; }
        }
        public static string _deleteProcedureName = "ProBranchDelete";
        public static string _updateProcedureName = "ProBranchUpdate";
        public static string _selectProcedureName = "ProBranchSelect";
        public Output UpdateBranchData(UpdateBranch input, string companyKey)
        {
            return Common.UpdateTableData<UpdateBranch>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteBranchData(DeleteBranch input, string companyKey)
        {
            return Common.UpdateTableData<DeleteBranch>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<BranchView> GetBranchData(BranchID input, string companyKey)
        {
            return Common.GetDataViaProcedure<BranchView, BranchID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
        public APIGetRecordsDynamic<BranchImages> GetBranchImageData(BranchID input, string companyKey)
        {
            return Common.GetDataViaProcedure<BranchImages, BranchID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }
        public class BranchBankDetailsSubSelect
        {
            public Int64 FK_Branch { get; set; }
            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
          
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public Int32 PageSize { get; set; }
            public string Name { get; set; }

        }
        public APIGetRecordsDynamic<BranchBankDetails> GetBranchbankdetailsData(BranchBankDetailsSubSelect input, string companyKey)
        {
            return Common.GetDataViaProcedure<BranchBankDetails, BranchBankDetailsSubSelect>(companyKey: companyKey, procedureName: "ProBranchBankDetailsSelect", parameter: input);

        }
    }
    }
