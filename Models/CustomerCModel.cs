using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class CustomerCModel
    {

            public class CustomerView
            {
            public long SlNo { get; set; }
            public long CustomerID { get; set; }
                //[Required(ErrorMessage = "Please Enter Number")]
                public string Number { get; set; }
                //[Required(ErrorMessage = "Please Enter Name")]
                public string Name { get; set; }
               // [Required(ErrorMessage = "Please Enter G S T I N No")]
                public string GSTINNo { get; set; }
                //[Required(ErrorMessage = "Please Enter Address")]
                public string Address1 { get; set; }
               // [Required(ErrorMessage = "Please Enter Mobile")]
                public string Mobile { get; set; }
                //[Required(ErrorMessage = "Please Enter Phone")]
                public string Phone { get; set; }
               // [Required(ErrorMessage = "Please Enter Email")]
                public string Email { get; set; }
                public long BranchTypeID { get; set; }
                public long LeadGenerateID { get; set; }
                public string LeadGenerateNo { get; set; }
                public string LGCustomer { get; set; }

                public bool Individual { get; set; }
            public bool Individuals { get; set; }
            // [Range(1, long.MaxValue, ErrorMessage = "Select Country")]
            public long CountryID { get; set; }
                public string Country { get; set; }
              //  [Range(1, long.MaxValue, ErrorMessage = "Select States")]
                public long StatesID { get; set; }
                public string States { get; set; }
               // [Range(1, long.MaxValue, ErrorMessage = "Select District")]
                public long DistrictID { get; set; }
                public string District { get; set; }
               // [Range(1, long.MaxValue, ErrorMessage = "Select Area")]
                public long ?AreaID { get; set; }
                public string Area { get; set; }
                //[Range(1, long.MaxValue, ErrorMessage = "Select Post")]
                public long ?PostID { get; set; }
                public string Post { get; set; }
                public string PinCode { get; set; }
               // [Range(1, long.MaxValue, ErrorMessage = "Select Place")]
                public long ?PlaceID { get; set; }
                public string Place { get; set; }
              
               // [Range(1, long.MaxValue, ErrorMessage = "Select Customer Type")]
                public long CustomerTypeID { get; set; }
                public string CustomerType { get; set; }
               // [Range(1, long.MaxValue, ErrorMessage = "Select Branch")]
                public long BranchID { get; set; }
                public string Branch { get; set; }
               // [Range(1, long.MaxValue, ErrorMessage = "Select Occupation")]
                public long ?OccupationID { get; set; }
                public string Occupation { get; set; }
               

              //  [Required(ErrorMessage = "Please Enter Contact Person")]
                public string ContactPerson { get; set; }
               // [Required(ErrorMessage = "Please Enter Contact Mobile")]
                public string ContactMobile { get; set; }
              //  [Required(ErrorMessage = "Please Enter Contact Email")]
                public string ContactEmail { get; set; }
            public Int64 TotalCount { get; set; }
            public string TransMode { get; set; }
            public string Landmark { get; set; }

            public string CusReferenceNo { get; set; }
            public bool IGSTCust { get; set; }
        }
            //-------------------------------------------------


            public class CustomerIDView
            {
                public long CustomerID { get; set; }
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

                public long ?AreaID { get; set; }


                public string Post { get; set; }
                public long ?PostID { get; set; }


                public string Place { get; set; }
                public long ?PlaceID { get; set; }
            }

            public class CustomerListModel
            {


                public List<BranchTypelist> BranchTypeList { get; set; }
               
                public List<Occupationlist> OccupationList { get; set; }
              
                public string CustomerNumber { get; set; }
             

            }



            //--------------------------------------

            public class UpdateCustomer
            {
                public long ID_Customer { get; set; }
                public long FK_Customer { get; set; }
                public string CusNumber { get; set; }
                public string CusName { get; set; }
                public string CusGSTINNo { get; set; }
                public string CusAddress1 { get; set; }
                public string CusLandmark { get; set; }
            public string CusMobile { get; set; }
                public string CusPhone { get; set; }
                public string CusEmail { get; set; }
                public long FK_LeadGenerate { get; set; }
                public long FK_Country { get; set; }
                public long FK_State { get; set; }
                public long FK_Area { get; set; }
                public long FK_Post { get; set; }
                public long FK_Place { get; set; }
                public long FK_District { get; set; }
                public long FK_Occupation { get; set; }
                public long FK_CustomerType { get; set; }
                public long FK_Branch { get; set; }
              
                public string CusContactPerson { get; set; }
                public string CusContactMobile { get; set; }
                public string CusContactEmail { get; set; }
                public string TransMode { get; set; }

                public Int64 FK_Company { get; set; }
                public Int16 UserAction { get; set; }
                public Int64 FK_Machine { get; set; }
                public Int64 FK_BranchCodeUser { get; set; }
                public string EntrBy { get; set; }
                public string CusReferenceNo { get; set; }
                //public bool IGSTCust { get; set; }
        }
            public static string _deleteProcedureName = "ProCustomerDeletes";
            public static string _updateProcedureName = "ProCustomerUpdates";
            public static string _selectProcedureName = "ProCustomerSelects";

            public class DeleteCustomer
            {
                public string TransMode { get; set; }
                public long FK_Customer { get; set; }
                public long FK_Reason { get; set; }
                public long FK_Company { get; set; }
                public long FK_Machine { get; set; }
                public long FK_BranchCodeUser { get; set; }
                public string EntrBy { get; set; }


            }


            public class CustomerID
            {
                //public Int64 ID_Customer { get; set; }
                public string TransMode { get; set; }
                public long FK_Customer { get; set; }
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
            public class Occupationlist
            {
                public long OccupationID { get; set; }
                public string Occupation { get; set; }


            }
            public class Districtlist
            {
                public long DistrictID { get; set; }
                public string District { get; set; }


            }
           

            public class BranchTypelist
            {
                public long BranchTypeID { get; set; }

                public string BranchType { get; set; }
            }
            public class Branchlist
            {
                public long BranchID { get; set; }

                public string Branch { get; set; }
            }
            public class CustomerTypelist
            {
                public long CustomerTypeID { get; set; }

                public string CustomerType { get; set; }
                public string CustomerCategory { get; set; }
                public string CustomerSector { get; set; }
                public bool Individual { get; set; }
             
            }

        public class LeadGenerateListModel
        {
            public long LeadGenerateID { get; set; }
            public string LeadGenerateNo { get; set; }
            public string LGCustomer { get; set; }
           
        }



        public Output UpdateCustomerData(UpdateCustomer input, string companyKey)
            {
                return Common.UpdateTableData<UpdateCustomer>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
            }
        public Output UpdateCustomerOthersData(UpdateCustomer input, string companyKey)
        {
            return Common.UpdateTableData<UpdateCustomer>(parameter: input, companyKey: companyKey, procedureName: "ProCustomerOthersUpdate");
        }
        public Output DeleteCustomerData(DeleteCustomer input, string companyKey)
            {
                return Common.UpdateTableData<DeleteCustomer>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
            }
            public APIGetRecordsDynamic<CustomerView> GetCustomerData(CustomerID input, string companyKey)
            {
                return Common.GetDataViaProcedure<CustomerView, CustomerID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
            }

            public class CustomerGSTINView
            {
                public string GSTINNo { get; set; }
                public long FK_Company { get; set; }

            }
            public APIGetRecordsDynamic<CustomerView> GetCustomerByGST(CustomerGSTINView input, string companyKey)
            {
                return Common.GetDataViaProcedure<CustomerView, CustomerGSTINView>(companyKey: companyKey, procedureName: "proCustomerGSTSelect", parameter: input);
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

        public class inputGetCustomerNo {
            public long FK_Company { get; set; }
            public string Submodule { get; set; }
        }
        public class outputGetCustomerNo
        {
            public string CustomerNumber { get; set; }
        }
        public APIGetRecordsDynamic<outputGetCustomerNo> GetCustomerNo(inputGetCustomerNo input, string companyKey)
        {
            return Common.GetDataViaProcedure<outputGetCustomerNo, inputGetCustomerNo>(companyKey: companyKey, procedureName: "ProGetAccountNo", parameter: input);
        }


    }
    }