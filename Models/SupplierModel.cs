
/*----------------------------------------------------------------------
Created By	: Jisi Rajan
Created On	: 02/02/2022
Purpose		: Supplier
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
    public class SupplierModel
    {

        public class SupplierView
        {
            public long SlNo { get; set; }
            public long SupplierID { get; set; }
            public string Number { get; set; }
          
            // [Required(ErrorMessage = "Please Enter Name")]
            public string Name { get; set; }
            public string ShortName { get; set; }
            //[Required(ErrorMessage = "Please Enter Contact Person")]
            public string ContactPerson { get; set; }
           // [Required(ErrorMessage = "Please Enter Phone")]
            public string Phone { get; set; }
            //[Required(ErrorMessage = "Please Enter Mobile")]
            public string Mobile { get; set; }
           // [Required(ErrorMessage = "Please Enter Email")]
            public string Email { get; set; }
          //  [Required(ErrorMessage = "Please Enter Fax")]
            public string Fax { get; set; }
           // [Required(ErrorMessage = "Please Enter Address")]
            public string Address { get; set; }
            //[Range(1, long.MaxValue, ErrorMessage = "Select Country")]
            public long CountryID { get; set; }
            public string Country { get; set; }
          //  [Range(1, long.MaxValue, ErrorMessage = "Select States")]
            public long StatesID { get; set; }
            public string States { get; set; }
          //  [Range(1, long.MaxValue, ErrorMessage = "Select District")]
            public long DistrictID { get; set; }
            public string District { get; set; }
          //  [Range(1, long.MaxValue, ErrorMessage = "Select Area")]
            public long? AreaID { get; set; }
            public string Area { get; set; }
            //[Range(1, long.MaxValue, ErrorMessage = "Select Post")]
            public long? PostID { get; set; }
            public string Post { get; set; }
          //  [Range(1, long.MaxValue, ErrorMessage = "Select Place")]
            public long ?PlaceID { get; set; }
            public string Place { get; set; }
           // [Required(ErrorMessage = "Please Enter G S T I N No")]
            public string GSTINNo { get; set; }
           // [Required(ErrorMessage = "Please Enter Active")]
            public bool Active { get; set; }
          //  [Required(ErrorMessage = "Please Enter Descriptions")]
            public string Descriptions { get; set; }
           // [Required(ErrorMessage = "Please Enter Mode")]
            public string Mode { get; set; }
            public string ModeName { get; set; }

          //  [Required(ErrorMessage = "Please Enter Sort Order")]
            public Int32 SortOrder { get; set; }
          //  [Required(ErrorMessage = "Please Enter Bank Name")]
            public string BankName { get; set; }
           // [Required(ErrorMessage = "Please Enter Branch Name")]
            public string BranchName { get; set; }
         //   [Required(ErrorMessage = "Please Enter Account Holder Name")]
            public string AccountHolderName { get; set; }
         //   [Required(ErrorMessage = "Please Enter Bank Account")]
            public string BankAccount { get; set; }
            //[Required(ErrorMessage = "Please Enter Confirm Bank Account")]
            //public string ConfirmBankAccount { get; set; }
           // [Required(ErrorMessage = "Please Enter I F S C Code")]
            public string IFSCCode { get; set; }
          //  [Required(ErrorMessage = "Please Enter Aadhar Card")]
            public string AadharCard { get; set; }
          //  [Required(ErrorMessage = "Please Enter Pan No")]
            public string PanNo { get; set; }
            public string PinCode { get; set; }
            public Int64 TotalCount { get; set; }
            public long? GoodsTransID { get; set; }
           // public long FK_SupplierType { get; set; }
            public long ID_SupplierType { get; set; }
           // public string STName { get; set; }
           public long FK_Branch { get; set; }

            public decimal OpeningBalance { get; set; }
            public Int16 FK_TransType { get; set; }
            public string OpeningBalDate { get; set; }
            public Boolean OpeningDisplay { get; set; }
            public long? AccountHeadID { get; set; }
            public string  AccountHeadName { get; set; }
            //[Required(ErrorMessage = "Please Enter Account Head Sub")]

        }
        //-------------------------------------------------


        public class SupplierIDView
        {
            public long SupplierID { get; set; }
            public long ReasonID { get; set; }
        }
        public class SearchPinModel
        {
            public string Country { get; set; }

            public long CountryID { get; set; }

            public string States { get; set; }

            public long StatesID { get; set; }

            public string District { get; set; }

            public long DistrictID { get; set; }

            public string Area { get; set; }

            public long? AreaID { get; set; }


            public string Post { get; set; }
            public long ?PostID { get; set; }


            public string Place { get; set; }
            public long ?PlaceID { get; set; }
        }
        public class ModeList
        {
            public short ModeID { get; set; }
            public string ModeName { get; set; }
            public string Mode { get; set; }
        }
        public class GoodstransList
        {
            public short GoodsTransID { get; set; }
            public string GoodsTransName { get; set; }
        }
        public class SupplierListModel
        {

            public List<ModeList> ModeList { get; set; }
            public List<GoodstransList> GoodstransList { get; set; }
            public List<SupplierTypeNameList> SupplierType { get; set; }
            public int SortOrder { get; set; }
            public string SupNumber { get; set; }
            public List<BranchHead> branchHeads { get; set; }
            public List<TransTypes> TransTypeList { get; set; }

        }
        public class TransTypes
        {
            public long TransTypeID { get; set; }
            public string TransType { get; set; }

            public long FK_TransType { get; set; }

        }


        //--------------------------------------

        public class UpdateSupplier
        {
            public long ID_Supplier { get; set; }
            public long FK_Supplier { get; set; }
            public string SupNumber { get; set; }
            public string SuppName { get; set; }
            public string SuppContactPerson { get; set; }
            public string SuppPhone { get; set; }
            public string SuppMobile { get; set; }
            public string SuppEmail { get; set; }
            public string SuppFax { get; set; }
            public string SuppAddress { get; set; }
            public long FK_Country { get; set; }
            public long FK_States { get; set; }
            public long FK_District { get; set; }
            public long FK_Area { get; set; }
            public long FK_Post { get; set; }
            public long FK_Place { get; set; }
            public string SuppGSTINNo { get; set; }
            public bool Active { get; set; }
            public string Descriptions { get; set; }
            public string Mode { get; set; }
            public Int32 SortOrder { get; set; }
            public string BankName { get; set; }
            public string BranchName { get; set; }
            public string AccountHolderName { get; set; }
            public string BankAccount { get; set; }
            //public string ConfirmBankAccount { get; set; }
            public string IFSCCode { get; set; }
            public string AadharCard { get; set; }
            public string PanNo { get; set; }

            public string TransMode { get; set; }

            public Int64 FK_Company { get; set; }
            public Int16 UserAction { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long? GoodsTransID { get; set; }
            public long FK_SupplierType { get; set; }
            public long FK_Branch { get; set; }
            public string SuppShortname { get; set; }
            public decimal OpeningBalance { get; set; }
            public Int16 FK_TransType { get; set; }
            public string  OpeningBalDate { get; set; }

            public long FK_AccountHead { get; set; }
           
        }
        public static string _deleteProcedureName = "ProSupplierDelete";
        public static string _updateProcedureName = "ProSupplierUpdate";
        public static string _selectProcedureName = "ProSupplierSelect";

        public class DeleteSupplier
        {
            public string TransMode { get; set; }
            public long FK_Supplier { get; set; }
            public long FK_Reason { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }


        }

        public class SupplierTypeNameList
        {
            public Int64 ID_SupplierType { get; set; }
            public string STName { get; set; }
        }


        public class SupplierID
        {
            //public Int64 ID_Supplier { get; set; }
            public string TransMode { get; set; }
            public long FK_Supplier { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public string Name { get; set; }

        }
        public class Placelist
        {
            public long PlaceID { get; set; }
            public string Place { get; set; }


        }
        public class Countrylist
        {
            public int CountryID { get; set; }
            public string Country { get; set; }
           

        }
        public class Postlist
        {
            public long PostID { get; set; }
            public string Post { get; set; }

            public string PinCode { get; set; }

        }

        public class InputPlace
        {
             public long DistrictID { get; set; }
            public long PlaceID { get; set; }



        }
        public class Arealist
        {
            public long AreaID { get; set; }
            public string Area { get; set; }


        }

        public class Districtlist
        {
            public long DistrictID { get; set; }
            public string District { get; set; }


        }

        public Output UpdateSupplierData(UpdateSupplier input, string companyKey)
        {
            return Common.UpdateTableData<UpdateSupplier>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteSupplierData(DeleteSupplier input, string companyKey)
        {
            return Common.UpdateTableData<DeleteSupplier>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<SupplierView> GetSupplierData(SupplierID input, string companyKey)
        {
            return Common.GetDataViaProcedure<SupplierView, SupplierID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }

        public class SupplierGSTINView
        {
            public string GSTINNo { get; set; }
            public long FK_Company { get; set; }

        }
        public APIGetRecordsDynamic<SupplierView> GetSupplierByGST(SupplierGSTINView input, string companyKey)
        {
            return Common.GetDataViaProcedure<SupplierView, SupplierGSTINView>(companyKey: companyKey, procedureName: "proSupplierGSTSelect", parameter: input);
        }

        //function to getpincode 


        public class InputPincode
        {
            public long FK_Company { get; set; }
            public string Pincode { get; set; }
        }

        public APIGetRecordsDynamic<SearchPinModel> GetDetailsByPincodeData(InputPincode input, string companyKey)
        {
            return Common.GetDataViaProcedure<SearchPinModel, InputPincode>(companyKey: companyKey, procedureName: "proGetAddressByPin", parameter: input);
        }

        public class inputGetSupplierNo
        {
            public long FK_Company { get; set; }
        }
        public class outputGetSupplierNo
        {
            public string SupplierNumber { get; set; }
        }

        public class BranchHead
        {
            public long FK_Branch { get; set; }
            public string BranchName { get; set; }
        }
        public class InputBranch
        {
            public long FK_Company { get; set; }
        }

        public APIGetRecordsDynamic<outputGetSupplierNo> GetSupplierNo(inputGetSupplierNo input, string companyKey)
        {
            return Common.GetDataViaProcedure<outputGetSupplierNo, inputGetSupplierNo>(companyKey: companyKey, procedureName: "ProGetSupplierNo", parameter: input);
        }

        public APIGetRecordsDynamic<BranchHead> GetBranchHead(InputBranch input, string companyKey)
        {
            return Common.GetDataViaProcedure<BranchHead, InputBranch>(companyKey: companyKey, procedureName: "ProSelectAccountHeadBranch", parameter: input);
        }


    }
}
