
////----------------------------------------------------------------------
//Created By  : AmrithaAk
//Created On	: 19/01/2022
//Purpose		: Organization
//-------------------------------------------------------------------------
//Modification
//On          By OMID/Remarks
//-------------------------------------------------------------------------
//-------------------------------------------------------------------------//
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PerfectWebERP.General;

namespace PerfectWebERP.Models
{
    public class OrganizationModel
    {
        public class OrganizationView
        {
            public long SlNo { get; set; }
            public long OrganizationID { get; set; }
         
            public string Code { get; set; }
        
            public string Name { get; set; }
          
            public string ShortName { get; set; }
          
            public string ContactPerson { get; set; }
           
            public string ContactPMobile { get; set; }
          
            public string ContactPEmail { get; set; }
          
            public string RegisterNo { get; set; }
           
            public string Address1 { get; set; }
          //  [Required(ErrorMessage = "Please Enter Address2")]
            public string Address2 { get; set; }
           // [Required(ErrorMessage = "Please Enter Address3")]
            public string Address3 { get; set; }
          
            public string Email { get; set; }
           // [Required(ErrorMessage = "Please Enter Web Site")]
            public string WebSite { get; set; }
          
            public string Mobile { get; set; }
           // [Required(ErrorMessage = "Please Enter Phone")]
            public string Phone { get; set; }
           // [Required(ErrorMessage = "Please Enter Fax")]
            public string Fax { get; set; }
         
            public DateTime StartDate { get; set; }
          //  [Required(ErrorMessage = "Please Enter S Media Website1")]
            public string SocialMediaWebsite1 { get; set; }
           // [Required(ErrorMessage = "Please Enter S Media Website2")]
            public string SocialMediaWebsite2 { get; set; }
            //[Required(ErrorMessage = "Please Enter Sort Order")]
            public long SortOrder { get; set; }
          
            public string Country { get; set; }
           
            public long CountryID { get; set; }
         
            public string States { get; set; }
            
            public long StatesID { get; set; }
  
            public string District { get; set; }
          
            public long DistrictID { get; set; }
           // [Required(ErrorMessage = "Please Enter Area")]
            public string Area { get; set; }
           
            public long? AreaID { get; set; }

          
            public string Post { get; set; }
            public long ?PostID { get; set; }

         
            public string Place { get; set; }
            public long ?PlaceID { get; set; }

            public string PinCode { get; set; }
            public string TransMode { get; set; }
            public Int64 TotalCount { get; set; }
        }
        public class UpdateOrganization
        {
            public long ID_Organization { get; set; }
            public string OrgCode { get; set; }
            public string OrgName { get; set; }
            public string OrgShortName { get; set; }
            public string OrgContactPerson { get; set; }
            public string OrgContactPMobile { get; set; }
            public string OrgContactPEmail { get; set; }
            public string OrgRegisterNo { get; set; }
            public string OrgAddress1 { get; set; }
            public string OrgAddress2 { get; set; }
            public string OrgAddress3 { get; set; }
            public string OrgEmail { get; set; }
            public string OrgWebSite { get; set; }
            public string OrgMobile { get; set; }
            public string OrgPhone { get; set; }
            public string OrgFax { get; set; }
            public DateTime OrgStartDate { get; set; }
            public string OrgSocialMediaWebsite1 { get; set; }
            public string OrgSocialMediaWebsite2 { get; set; }
            public long SortOrder { get; set; }
            public long FK_Country { get; set; }
            public long FK_States { get; set; }
            public long FK_District { get; set; }
            public long FK_Area { get; set; }
            public long FK_Post { get; set; }
            public long FK_Place { get; set; }

            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public byte UserAction { get; set; }
            public long FK_Organization { get; set; }
            public string TransMode { get; set; }
           

        }
        public class OrganizationInfoView
        {
            public long ID_Organization { get; set; }

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
            public long ?PostID { get; set; }

            //[Required(ErrorMessage = "Please Enter Place")]
            //public string Place { get; set; }
            //public long PlaceID { get; set; }
        }

        public static string _deleteProcedureName = "proOrganizationDelete";
        public static string _updateProcedureName = "ProOrganizationUpdate";
        public static string _selectProcedureName = "proOrganizationSelect";

        public class DeleteOrganization
        {
            public long ID_Organization { get; set; }
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
        public class PostList
        {
            public int PostID { get; set; }
            public string Post{ get; set; }

            public string PinCode { get; set; }
            //public long FK_Post { get; set; }

        }


        public class Countrylist
        {
            public int CountryID { get; set; }
            public string Country { get; set; }
        }

        public class Statelist
        {
            public int StatesID { get; set; }
            public string States { get; set; }
        }
        public class Postlistrelated
        {
            public int DistrictID { get; set; }
            public int PlaceID { get; set; }
        }


        public class Districtlist
        {
            public int DistrictID { get; set; }
            public string District { get; set; }
        }
        public class Arealist
        {
            public int AreaID { get; set; }
            public string Area { get; set; }
        }

        public class Placelist
        {
            public int PlaceID { get; set; }
            public string Place { get; set; }
        }
        public class OrganizationID
        {
            public Int64 ID_Organization { get; set; }
            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
          
            public string Name { get; set; }

        }






        public Output UpdateOrganizationData(UpdateOrganization input, string companyKey)
        {
            return Common.UpdateTableData<UpdateOrganization>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteOrganizationData(DeleteOrganization input, string companyKey)
        {
            return Common.UpdateTableData<DeleteOrganization>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<OrganizationView> GetOrganizationData(OrganizationID input, string companyKey)
        {
            return Common.GetDataViaProcedure<OrganizationView, OrganizationID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }
    }
}