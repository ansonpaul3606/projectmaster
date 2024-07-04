using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace PerfectWebERP.Models
{
    public class ManufacturerModel
    {

        public class ManufacturerView
        {
            public long SlNo { get; set; }
            public long ManufacturerID { get; set; }
            public string Manufacturer { get; set; }
            public string ShortName { get; set; }
            public string ModeID { get; set; }
            public string ModeName { get; set; }
            public Int32 SortOrder { get; set; }
            public bool Mask { get; set; }
            public string TransMode { get; set; }

        }
        public class ManufacturerInputFromViewModel
        {

            public long SlNo { get; set; }
            ////[Required(ErrorMessage = "No Manufacturer Selected")]
            public long ManufacturerID { get; set; }

            ////[Required(ErrorMessage = "Please Enter Manufacturer")]
            public string Manufacturer { get; set; }

            ////[Required(ErrorMessage = "Please Enter Short Name")]
            public string ShortName { get; set; }

            //[Range(1, long.MaxValue, ErrorMessage = "Select Category Mode")]
            public string Mode { get; set; }
            public string ModeName { get; set; }
            public string Mobile { get; set; }
            public string PMobile { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public string GSTIN { get; set; }
            public string Description { get; set; }
            public string Address { get; set; }
            public Int32 SortOrder { get; set; }
            public Int32 ?PlaceID { get; set; }

            public string Pin { get; set; }
            public bool Active { get; set; }
            public string Country { get; set; }

            public long CountryID { get; set; }
            [Required(ErrorMessage = "Please Enter State")]
            public string States { get; set; }
            public string Place { get; set; }

            public long StatesID { get; set; }
            [Required(ErrorMessage = "Please Enter District")]
            public string District { get; set; }
            [Required(ErrorMessage = "Please Enter Post")]
            public long DistrictID { get; set; }
  
            [Required(ErrorMessage = "Please Enter Post")]
            public string Post { get; set; }
            public long? PostID { get; set; }

            public Int64 TotalCount { get; set; }
            public string TransMode{ get; set; }

        }

        public class ManufacturerInfoView
        {
            [Required(ErrorMessage = "No Manufacturer Selected")]
            public Int64 ManufacturerID { get; set; }
            [Required(ErrorMessage = "Please Select The Reason")]
            public Int64 ReasonID { get; set; }
        }
        public static string _updateProcedureName = "proManufacturerUpdate";      
        public class UpdateManufacturer
        {

            public byte UserAction { get; set; }
            public Int64 FK_Branch { get; set; }
            public Int64 FK_Manufacturer { get; set; }

            public string Mode { get; set; }
            public long ID_Manufacturer { get; set; }
            public string ManufName { get; set; }
            public string ManufShortName { get; set; }
           /// public string FK_ManufPin { get; set; }
            public string ManufAddress { get; set; }
            public string ManufEmail { get; set; }
            public string ManufMobile { get; set; }
            public string ManufPContact { get; set; }
            public string ManufPhone { get; set; }
            public string ManufGSTIN { get; set; }
            public string ManufDescription { get; set; }
            public bool ManufActive { get; set; }
            public Int32 SortOrder { get; set; }
            public Int64 FK_Country { get; set; }
            public Int64 FK_Place { get; set; }
            public Int64 FK_State { get; set; }
            public Int64 FK_District { get; set; }
            public Int64 FK_Post { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long BackupId { get; set; }
            public string TransMode { get; set; }



        }

        public static string _selectProcedureName = "proManufacturerSelect";
        public class GetManufacturer
        {
            public Int64 FK_Manufacturer { get; set; }
            public string TransMode { get; set; }
            public Int64 FK_Company { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string Name { get; set; }
        }

        public static string _deleteProcedureName = "proManufacturerDelete";
        public class DeleteManufacturer
        {
            public long FK_Manufacturer { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Reason { get; set; }
            public Int64 FK_Branch { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
        }
        public class ModeList
        {
            public short ModeID { get; set; }
            public string ModeName { get; set; }
            public string Mode { get; set; }
            public short IDType { get; set; }
            public string IDtypeName { get; set; }

            public List<Country> CountryList { get; set; }
            public long Country { get; set; }
            public string CountryName { get; set; }
            public long FK_Country { get; set; }
            public int SortOrder { get; set; }

        }

        public class Country
        {
            public int CountryID { get; set; }
            public string CountryName { get; set; }
            public long FK_Country { get; set; }

        }

        public class State
        {
            public int StateID { get; set; }
            public string StateName { get; set; }
            public long FK_States{ get; set; }

        }

        public class District
        {
            public int DistrictID { get; set; }
            public string DistrictName { get; set; }
            public long FK_District { get; set; }

        }

        public class Post
        {
            public int PostID { get; set; }
            public string PostName { get; set; }
            public long FK_Post { get; set; }

        }



        public class ManufacturerListModel
        {

            public List<ModeList> ModeList { get; set; }

            public List<Country> CountryList { get; set; }
            public List<State> StateList { get; set; }
            public List<District> DistrictList { get; set; }
            public List<Post> PostList { get; set; }

            public int SortOrder { get; set; }

        }
        public class ManufacturerGSTINView
        {
            public string GSTIN { get; set; }
            public long FK_Company { get; set; }

        }
        public APIGetRecordsDynamic<ManufacturerInputFromViewModel> GetManufacturerByGST(ManufacturerGSTINView input, string companyKey)
        {
            return Common.GetDataViaProcedure<ManufacturerInputFromViewModel, ManufacturerGSTINView>(companyKey: companyKey, procedureName: "proManufacturerGSTSelect", parameter: input);
        }

        public Output UpdateManufacturerData(UpdateManufacturer input, string companyKey)
        {
            return Common.UpdateTableData<UpdateManufacturer>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteManufacturerData(DeleteManufacturer input, string companyKey)
        {
            return Common.UpdateTableData<DeleteManufacturer >(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<ManufacturerInputFromViewModel> GetManufacturerData(GetManufacturer input, string companyKey)
        {
            return Common.GetDataViaProcedure<ManufacturerInputFromViewModel, GetManufacturer>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }
    }
}