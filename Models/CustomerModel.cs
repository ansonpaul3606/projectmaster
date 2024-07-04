using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class CustomerModel
    {
       
        #region View Based Model
        //----  Customer View model
        public class CustomerView
        {
            public byte UserAction { get; set; }
            public Int64 CustomerID { get; set; }
            public string CustomerNumber { get; set; }
            public string CustomerName { get; set; }
            public string CustomerGSTINNo { get; set; }
            public string CustomerAddress1 { get; set; }
            public string CustomerAddress2 { get; set; }
            public string CustomerLocation { get; set; }
            public string CustomerMobile { get; set; }
            public string CustomerPhone { get; set; }
            public string CustomerEmail { get; set; }
            public string CustomerWebsite { get; set; }
            public byte CustomerCanvasMode { get; set; }
            public Int64 CustomerCanvasBy { get; set; }
            public Int64 LeadGenerateID { get; set; }
            public Int64 CountryID { get; set; }
            public string Country { get; set; }
            public Int64 StateID { get; set; }
            public string State { get; set; }
            public Int64 DistrictID { get; set; }
            public string District { get; set; }
            public Int64 PostID { get; set; }
            public string Post { get; set; }
            public Int64 CategoryID { get; set; }
            public string Category { get; set; }
            public Int64 CustomerTypeID { get; set; }
            public string CustomerType { get; set; }
            public Int64 BranchID { get; set; }
            public string Branch { get; set; }
            public Int64 EmployeeID { get; set; }
            public string Employee { get; set; }
            public string CustomerContactPerson { get; set; }
            public string CustomerContactMobile { get; set; }
            public string CustomerContactEmail { get; set; }
            //public byte CustomerMode { get; set; }
            //public bool CustomerStatus { get; set; }
            //public DateTime? CustomerStatusOn { get; set; }
            public Int64 CompanyID { get; set; }
            public Int64 BranchCodeUserID { get; set; }
            //public bool Passed { get; set; }
            //public Int64 BackupID { get; set; }
        }
        public class CustomerInfoView
        {
            [Required(ErrorMessage = "Please select a Customer")]
            [Range(1, long.MaxValue, ErrorMessage = "Please select a customer")]
            public Int64 CustomerID { get; set; }
            [Required(ErrorMessage = "Please select the reason")]
            public Int64 ReasonID { get; set; }

        }
        public class CustomerFormInputViewModel
        {
            [Required(ErrorMessage ="Please select a branch")]
            [Range(1, long.MaxValue, ErrorMessage = "Please select a branch")]
            public Int64 BranchID { get; set; }
            [Required(ErrorMessage = "Please select a Customer Type")]
            [Range(1, long.MaxValue, ErrorMessage = "Please select a Customer Type")]
            public Int64 CustomerTypeID { get; set; }
            [Required(ErrorMessage = "Please select a Customer")]
            [Range(1, long.MaxValue, ErrorMessage = "Please select a Customer")]
            public Int64 CustomerID { get; set; }
            [Required(ErrorMessage = "Please select a Lead")]
            [Range(1, long.MaxValue, ErrorMessage = "Please select a Lead")]
            public Int64 LeadGenerateID { get; set; }
            [Required(ErrorMessage = "Please enter Customer Name")]
            [StringLength(150, ErrorMessage ="Maximum 150 characture")]
            public string CustomerName { get; set; }
            [Required(ErrorMessage = "Please enter Address 1")]
            [StringLength(150, ErrorMessage = "Maximum 150 characture")]
            public string CustomerAddress1 { get; set; }
            [Required(ErrorMessage = "Please enter  Address 2")]
            [StringLength(150, ErrorMessage = "Maximum 150 characture")]
            public string CustomerAddress2 { get; set; }
            [Required(ErrorMessage = "Please enter Customer Location")]
            [StringLength(150, ErrorMessage = "Maximum 150 characture")]
            public string CustomerLocation { get; set; }
            [Required(ErrorMessage = "Please select a Country")]
            [Range(1, long.MaxValue, ErrorMessage = "Please select a Country")]
            public Int64 CountryID { get; set; }
            [Required(ErrorMessage = "Please select a State")]
            [Range(1, long.MaxValue, ErrorMessage = "Please select a State")]
            public Int64 StateID { get; set; }
            [Required(ErrorMessage = "Please select a District")]
            [Range(1, long.MaxValue, ErrorMessage = "Please select a District")]
            public Int64 DistrictID { get; set; }
            [Required(ErrorMessage = "Please select a Post")]
            [Range(1, long.MaxValue, ErrorMessage = "Please select a Post")]
            public Int64 PostID { get; set; }
            [Required(ErrorMessage = "Please Enter Customer Mobile")]
            [StringLength(15, MinimumLength = 7, ErrorMessage = "Phone number must be between 7 and 15 digits")]
            [RegularExpression(@"^[\+]?[0-9]*$", ErrorMessage = "Not a valid mobile number")]
            public string CustomerMobile { get; set; }
            [Required(ErrorMessage = "Please Enter Customer Phone")]
            [StringLength(15, MinimumLength = 7, ErrorMessage = "Phone number must be between 7 and 15 digits")]
            [RegularExpression(@"^[\+]?[0-9]*$", ErrorMessage = "Not a valid phone number")]
            public string CustomerPhone { get; set; }
         
            public string CustomerEmail { get; set; }
          
            public string CustomerWebsite { get; set; }
            [Required(ErrorMessage = "Please Enter Customer GSTIN number")]
            [StringLength(15, MinimumLength = 15, ErrorMessage = "15 digits")]
            public string CustomerGSTINNo { get; set; }
            public byte CustomerCanvasMode { get; set; }
            public Int64 CustomerCanvasBy { get; set; }
            [Required(ErrorMessage = "Please select a Category")]
            [Range(1, long.MaxValue, ErrorMessage = "Please select a Category")]
            public Int64 CategoryID { get; set; }
            [Required(ErrorMessage = "Please Enter Contact Person")]
            [StringLength(150, ErrorMessage = "Maximum 150 characture ")]
            public string CustomerContactPerson { get; set; }
            [Required(ErrorMessage = "Please Enter Contact Person Mobile")]
            [StringLength(15, MinimumLength = 7, ErrorMessage = "Phone number must be between 7 and 15 digits")]
            [RegularExpression(@"^[\+]?[0-9]*$", ErrorMessage = "Not a valid phone number")]
            public string CustomerContactMobile { get; set; }
            [Required(ErrorMessage = "Please Enter Contact Person Email")]
            [EmailAddress(ErrorMessage = "Invalid Contact Email")]
            public string CustomerContactEmail { get; set; }
            [Required(ErrorMessage = "Please Enter Customer number")]
            [StringLength(15, MinimumLength = 15, ErrorMessage = "15 digits")]
            public string CustomerNumber { get; set; }
        }
        public class CustomerFormViewModel
        {
            public List<StateModel.StatesView> StateList { get; set; }
            public List<DistrictModel.DistrictView> DistrictList { get; set; }
            public List<PostModel.PostView> PostList { get; set; }
            public List<string> CountryList { get; set; }//<--temp datatype
            public List<string> LocationList { get; set; }//<--temp datatype
            public List<string> CustomerTypeList { get; set; }//<--temp datatype
            public List<string> CanvasByList { get; set; }//<--temp datatype
            public List<string> CanvasModeList { get; set; }//<--temp datatype
            public List<string> CanvasCategoryList { get; set; }//<--temp datatype
        }

        #endregion View Based Model

        #region Procedure Based Model
        //---- Customer stored procedure model paramet


        public static string _updateProcedureName = "proCustomerUpdate";
        public class UpdateCustomer
        {
            //------- Insert /update
            public byte UserAction { get; set; }
            //--------
            public Int64 ID_Customer { get; set; }
            public string CusNumber { get; set; }
            public string CusName { get; set; }
            public string CusGSTINNo { get; set; }
            public string CusAddress1 { get; set; }
            public string CusAddress2 { get; set; }
            public string CusLocation { get; set; }
            public string CusMobile { get; set; }
            public string CusPhone { get; set; }
            public string CusEmail { get; set; }
            public string CusWebsite { get; set; }
            public byte CusCanvasMode { get; set; }
            public Int64 CusCanvasBy { get; set; }
            public Int64 FK_LeadGenerate { get; set; }
            public Int64 FK_Country { get; set; }
            public Int64 FK_State { get; set; }
            public Int64 FK_District { get; set; }
            public Int64 FK_Post { get; set; }
            public Int64 FK_Category { get; set; }
            public Int64 FK_CustomerType { get; set; }
            public Int64 FK_Branch { get; set; }
            public Int64 FK_Employee { get; set; }
            public string CusContactPerson { get; set; }
            public string CusContactMobile { get; set; }
            public string CusContactEmail { get; set; }
            public byte CusMode { get; set; }
            public bool CusStatus { get; set; }
            public DateTime? CusStatusOn { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
          //  public DateTime EntrOn { get; set; }
            public Int64 FK_Machine { get; set; }
        }
        public static string _selectProcedureName = "proCustomerSelect";
        public class GetCustomer
        {
            public Int64 ID_Customer { get; set; }
            public string UserCode { get; set; }
            public Int64 FK_Company { get; set; }
        }

        public static string _deleteProcedureName = "proCustomerDelete";
        public class DeleteCustomer
        {
            public Int64 ID_Customer { get; set; }
            public string CancelBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Reason { get; set; }
            public DateTime? CancelOn { get; set; }
        }

        #endregion Procedure Based Model

        #region Database operations
        public Output UpdateCustomerData(UpdateCustomer input, string companyKey)
        {
            return Common.UpdateTableData<UpdateCustomer>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteCustomerData(DeleteCustomer input, string companyKey)
        {
            return Common.UpdateTableData<DeleteCustomer>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<CustomerView> GetCustomertData(GetCustomer input, string companyKey)
        {
            return Common.GetDataViaProcedure<CustomerView, GetCustomer>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
        #endregion Database operations

    }
}