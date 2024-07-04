/*----------------------------------------------------------------------
Created By	: Jisi Rajan
Created On	: 16/02/2022
Purpose		: EmployeeCreation
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
    public class EmployeeCreationModel
    {
         
        public class EmployeeCreationView
        {

            public long SlNo { get; set; }

            public long EmployeeID { get; set; }
           // [Required(ErrorMessage = "Please Enter Employeeyee No")]
            public string EmployeeNo{ get; set; }

           // [Required(ErrorMessage = "Please Enter Employeeyee Name")]
            public string EmployeeName { get; set; }
           // [Required(ErrorMessage = "Please Enter Employeeyee Mobile")]
            public string EmployeeMobile { get; set; }
           // [Required(ErrorMessage = "Please Enter Employeeyee Phone")]
            public string EmployeePhone { get; set; }
           // [Required(ErrorMessage = "Please Enter Employeeyee Email")]
            public string EmployeeEmail { get; set; }
           // [Required(ErrorMessage = "Please Enter Employeeyee Address")]
            public string EmployeeAddress { get; set; }
            //[Range(1, long.MaxValue, ErrorMessage = "Select Country")]
            public long CountryID { get; set; }
            public string Country { get; set; }
            //[Range(1, long.MaxValue, ErrorMessage = "Select States")]
            public long StatesID { get; set; }
           
            public string States { get; set; }
            //[Range(1, long.MaxValue, ErrorMessage = "Select District")]
            public long DistrictID { get; set; }
           
            public string District { get; set; }
           // [Range(1, long.MaxValue, ErrorMessage = "Select Area")]
            public long ?AreaID { get; set; }
            public string Area { get; set; }
           // [Range(1, long.MaxValue, ErrorMessage = "Select Post")]
            public long ?PostID { get; set; }
            public string Post { get; set; }
           // [Range(1, long.MaxValue, ErrorMessage = "Select Place")]
            public long? PlaceID { get; set; }
            public string Place { get; set; }
            public string PinCode { get; set; }
            //[Range(1, long.MaxValue, ErrorMessage = "Select Branch")]
            public long BranchID { get; set; }
            public string Branch { get; set; }

            public long BranchTypeID { get; set; }
            public string BranchType { get; set; }

            
            public long DepartmentID { get; set; }

            public string Department { get; set; }
            [Range(1, long.MaxValue, ErrorMessage = "Select Designation")]
            public long DesignationID { get; set; }

            public string Designation { get; set; }

            [Range(1, long.MaxValue, ErrorMessage = "Select Employee Type")]
            public long EmployeeTypeID { get; set; }
            public string EmployeeType { get; set; }
            public string TransMode { get; set; }
            public Int64 TotalCount { get; set; }
            public string LEmployeeName { get; set; }
        }

        public class UpdateEmployeeCreation
        {
            public long FK_Employee { get; set; }
            public long ID_Employee { get; set; }
            public string EmployeeNo { get; set; }
            public string EmployeeName { get; set; }
            public string EmployeeMobile { get; set; }
            public string EmployeePhone { get; set; }
            public string EmployeeEmail { get; set; }
            public string EmployeeAddress { get; set; }
            public long FK_Country { get; set; }
            public long FK_State { get; set; }
            public long FK_District { get; set; }
            public long FK_Area { get; set; }
            public long FK_Place { get; set; }
            public long FK_Post { get; set; }
            public long FK_Branch { get; set; }
            public long FK_EmployeeType { get; set; }
            public long FK_Designation { get; set; }
            public long FK_Department { get; set; }
            public string TransMode { get; set; }

            public Int64 FK_Company { get; set; }
            public Int16 UserAction { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public string EmployeeImageList { get; set; }
            public string LEmployeeName { get; set; }
        }
        public static string _deleteProcedureName = "ProEmployeeDelete";
        public static string _updateProcedureName = "ProEmployeeUpdate"; 
        public static string _selectProcedureName = "ProEmployeeSelect";

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

        public class DesignationList
        {
            public long DesignationID { get; set; }
            public string Designation { get; set; }
          
        }
        public class DepartmentList
        {
            public long DepartmentID { get; set; }
            public string Department { get; set; }

        }
        public class EmployeeTypeList
        {
            public short EmployeeTypeID { get; set; }
            public string EmployeeType { get; set; }
           
        }
        public class EmployeeListModel
        {

            public List<BranchTypelist> BranchTypeList { get; set; }
            public List<Branchlist> BranchList { get; set; }
            public List<DesignationList> DesignationList { get; set; }
            public List<DepartmentList> DepartmentList { get; set; }
            public List<EmployeeTypeList> EmployeeTypeList { get; set; }

            public string EmployeeNumber { get; set; }


        }

      
        public class DeleteEmployeeCreation
        {
            public string TransMode { get; set; }
            public long FK_Employee { get; set; }
            public long FK_Reason { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
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

            public long AreaID { get; set; }


            public string Post { get; set; }
            public long PostID { get; set; }


            public string Place { get; set; }
            public long PlaceID { get; set; }
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
        public class EmployeeCreationID
        {
            public string TransMode { get; set; }
            public long FK_Employee { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public string Name { get; set; }
        }

        public class EmployeeIDView
        {
            public long EmployeeID { get; set; }
            public long ReasonID { get; set; }
        }



        public class EmployeeImages
        {

            public Int64 ID_EmployeeImage { get; set; }
            public long FK_Employee { get; set; }
            public int FK_AccountCode { get; set; }
            public int EmpImgmode { get; set; }
            public string EmpImgValue { get; set; }
            public string EmpImgName { get; set; }
            public string Code { get; set; }
        }

        //function to getpincode 


        public class InputPincode
        {
            public long FK_Company { get; set; }
            public string Pincode { get; set; }
        }
        public class EmployeeImgId
        {


            public Int64 ID_Employee { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }


        }
        public APIGetRecordsDynamic<SearchPinModel> GetDetailsByPincodeData(InputPincode input, string companyKey)
        {
            return Common.GetDataViaProcedure<SearchPinModel, InputPincode>(companyKey: companyKey, procedureName: "proGetAddressByPin", parameter: input);
        }

        public class inputGetEmployeeNo
        {
            public long FK_Company { get; set; }
        }
        public class outputGetEmployeeNo
        {
            public string EmployeeNumber { get; set; }
        }
        public APIGetRecordsDynamic<outputGetEmployeeNo> GetEmployeeNo(inputGetEmployeeNo input, string companyKey)
        {
            return Common.GetDataViaProcedure<outputGetEmployeeNo, inputGetEmployeeNo>(companyKey: companyKey, procedureName: "ProGetEmployeeNo", parameter: input);
        }


        public Output UpdateEmployeeCreationData(UpdateEmployeeCreation input, string companyKey)
        {
            return Common.UpdateTableData<UpdateEmployeeCreation>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteEmployeeCreationData(DeleteEmployeeCreation input, string companyKey)
        {
            return Common.UpdateTableData<DeleteEmployeeCreation>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
       

        public APIGetRecordsDynamic<EmployeeCreationView> GetEmployeeCreationData(EmployeeCreationID input, string companyKey)
        {
            return Common.GetDataViaProcedure<EmployeeCreationView, EmployeeCreationID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
       
        public APIGetRecordsDynamic<EmployeeImages> GetEmployeeImage(EmployeeImgId input, string companyKey)
        {
            return Common.GetDataViaProcedure<EmployeeImages, EmployeeImgId>(companyKey: companyKey, procedureName: "ProEmployeeImageSelect", parameter: input);

        }
    }
}


