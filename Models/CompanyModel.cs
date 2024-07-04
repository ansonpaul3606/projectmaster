
//@*----------------------------------------------------------------------
//Created By  : Amritha AK
//Created On  : 26/01/2022
//Purpose		: Company
//-------------------------------------------------------------------------
//Modification
//On          By OMID/Remarks
//-------------------------------------------------------------------------
//-------------------------------------------------------------------------*@

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PerfectWebERP.General;


namespace PerfectWebERP.Models
{
    public class CompanyModel
    {
        public class CompanyView
        {
            public long SlNo { get; set; }
            public long CompanyID { get; set; }
          //  [Required(ErrorMessage = "Please Enter Code")]
            public string Code { get; set; }
          //  [Required(ErrorMessage = "Please Enter Name")]
            public string Name { get; set; }
           // [Required(ErrorMessage = "Please Enter Short Name")]
            public string ShortName { get; set; }
           // [Required(ErrorMessage = "Please Enter Contact Person")]
            public string ContactPerson { get; set; }
           // [Required(ErrorMessage = "Please Enter Contact P Mobile")]
            public string ContactPMobile { get; set; }
          //  [Required(ErrorMessage = "Please Enter Contact P Email")]
            public string ContactPEmail { get; set; }
           // [Required(ErrorMessage = "Please Enter Register No")]
            public string RegisterNo { get; set; }
         //   [Required(ErrorMessage = "Please Enter Address1")]
            public string Address1 { get; set; }
            // [Required(ErrorMessage = "Please Enter Address2")]
            public string Address2 { get; set; }
            // [Required(ErrorMessage = "Please Enter Address3")]
            public string Address3 { get; set; }
           // [Required(ErrorMessage = "Please Enter Email")]
            public string Email { get; set; }
            // [Required(ErrorMessage = "Please Enter Web Site")]
            public string WebSite { get; set; }
          //  [Required(ErrorMessage = "Please Enter Mobile")]
            public string Mobile { get; set; }
            //[Required(ErrorMessage = "Please Enter Phone")]
            public string Phone { get; set; }
            //  [Required(ErrorMessage = "Please Enter Fax")]
            public string Fax { get; set; }
           // [Required(ErrorMessage = "Please Enter Start Date")]
            public DateTime StartDate { get; set; }
           // [Required(ErrorMessage = "Please Enter Software installed date")]
            public DateTime Softinstalled { get; set; }
            //[Required(ErrorMessage = "Please Enter Social Media Website1")]
            public string SocialMediaWebsite1 { get; set; }
            // [Required(ErrorMessage = "Please Enter Social Media Website2")]
            public string SocialMediaWebsite2 { get; set; }
            //[Required(ErrorMessage = "Please Enter Sort Order")]
            

            
      
            public string Organization { get; set; }
            [Required(ErrorMessage = "Select Organization")]
            public long OrganizationID { get; set; }

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
          //  [Required(ErrorMessage = "Please Enter Area")]
            public string Area { get; set; }

            public long? AreaID { get; set; }

            [Required(ErrorMessage = "Please Enter Post")]
            public string Post { get; set; }
            public long ?PostID { get; set; }

            [Required(ErrorMessage = "Please Enter Place")]
            public string Place { get; set; }
            public long? PlaceID { get; set; }

            public string PinCode { get; set; }

            public Int64 TotalCount { get; set; }
               public Int32 SortOrder { get; set; }

            public List<CompanyImage> CompanyImageList { get; set; }
            public string TransMode { get; set; }
            //public List<CompanyImage2> CompanyImageList2 { get; set; }
            public int CompCategory { get; set; }
            public string SMSShortname { get; set; }
        }

        public class CompanyImage
        {
          
            public Int64 ID_CompanyImage { get; set; }
            public long FK_Company { get; set; }
            public int ComImgMode { get; set; }
           
            public string ComImg { get; set; }
            public string ComImgName { get; set; }

            public byte[] ComImgValue { get; set; }
        }
        public class CompanyImages
        {

            public Int64 ID_CompanyImage { get; set; }
            public long FK_Company { get; set; }
            public int ComImgMode { get; set; }         
            public string ComImgValue { get; set; }
        }
        //public class CompanyImage2
        //{

        //    public Int64 ID_CompanyImage { get; set; }
        //    public long FK_Company { get; set; }
        //    public int ComImgMode { get; set; }
        //    public byte[] ComImg { get; set; }


        //}



        public class UpdateCompany
        {
            public string CompanyImageList { get; set; }
            public long ID_Company { get; set; }
            public string CompCode { get; set; }
            public string CompName { get; set; }
            public string CompShortName { get; set; }
            public string CompContactPerson { get; set; }
            public string CompContactPMobile { get; set; }
            public string CompContactPEmail { get; set; }
            public string CompRegisterNo { get; set; }
            public string CompAddress1 { get; set; }
            public string CompAddress2 { get; set; }
            public string CompAddress3 { get; set; }
            public string CompEmail { get; set; }
            public string CompWebSite { get; set; }
            public string CompMobile { get; set; }
            public string CompPhone { get; set; }
            public string CompFax { get; set; }
            public DateTime CompStartDate { get; set; }
            public DateTime CompSoftinstalled { get; set; }
            public string CompSocialMediaWebsite1 { get; set; }
            public string CompSocialMediaWebsite2 { get; set; }
            public Int32 SortOrder { get; set; }
            public long FK_Organization { get; set; }
            public long FK_Country { get; set; }
            public long FK_State { get; set; }
            public long FK_District { get; set; }
            public long FK_Post { get; set; }
            public long FK_Place { get; set; }
            public long FK_Area { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string TransMode { get; set; }
             public long Debug { get; set; }            
            public string EntrBy { get; set; }
            public long FK_Reason { get; set; }
            public long FK_Machine { get; set; }
            public string SMSShortname { get; set; }
            public int CompCategory { get; set; }
            public int UserAction { get; set; }
           
        }

        public class CompanyViewList
        {
            public List<ImgMode> ImgModeList { get; set; }
            public List<ImgMode> Category { get; set; }
            public Int32 SortOrder { get; set; }

        }
        public class ModeData
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class ImgMode
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }

        public class CompanyInfoView
        {
            public long ID_Company { get; set; }

            public long ReasonID { get; set; }
        }



        public class searchPinModel
        {
            public string Country { get; set; }

            public long CountryID { get; set; }
            [Required(ErrorMessage = "Please Enter State")]
            public string States { get; set; }

            public long StatesID { get; set; }
            [Required(ErrorMessage = "Please Enter District")]
            public string District { get; set; }
            [Required(ErrorMessage = "Please Enter Post")]
            public long DistrictID { get; set; }
            [Required(ErrorMessage = "Please Enter Area")]
            public string Area { get; set; }

            public long AreaID { get; set; }

            [Required(ErrorMessage = "Please Enter Post")]
            public string Post { get; set; }
            public long PostID { get; set; }

            [Required(ErrorMessage = "Please Enter Place")]
            public string Place { get; set; }
            public long PlaceID { get; set; }
        }

        public static string _deleteProcedureName = "proCompanyDelete";
        public static string _updateProcedureName = "proCompanyUpdate";
        public static string _selectProcedureName = "proCompanySelect";

        public class DeleteCompany
        {
            public long ID_Company { get; set; }
            public string TransMode { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long Debug { get; set; }
            public long FK_Reason { get; set; }
            public long FK_BranchCodeUser { get; set; }
        }

        public class Sortorderlist
        {

            public Int32 SortOrder { get; set; }
        }
       

        public class Organizationlist
        {
            public int OrganizationID { get; set; }
            public string Organization { get; set; }
        }
       

        public class CompanyID
        {
          
          
            public Int64 ID_Company { get; set; }
            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string Name { get; set; }


        }
        public class CompanyImgId
        {


            public Int64 ID_Company { get; set; }        
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
           

        }


        public class ModeLead
        {
            public Int32 Mode { get; set; }
        }

        public Output UpdateCompanyData(UpdateCompany input, string companyKey)
        {
            return Common.UpdateTableData<UpdateCompany>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteCompanyData(DeleteCompany input, string companyKey)
        {
            return Common.UpdateTableData<DeleteCompany>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<CompanyView> GetCompanyData(CompanyID input, string companyKey)
        {
        return Common.GetDataViaProcedure<CompanyView, CompanyID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }
        public APIGetRecordsDynamic<CompanyImages> GetSubImage(CompanyImgId input, string companyKey)
        {
            return Common.GetDataViaProcedure<CompanyImages, CompanyImgId>(companyKey: companyKey, procedureName: "ProCompanyImageSelect", parameter: input);

        }
        public APIGetRecordsDynamic<ImgMode> GetImgModelist(ModeLead input, string companyKey)
        {
            return Common.GetDataViaProcedure<ImgMode, ModeLead>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }


    }

}
        
