using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class PostModel
    {
        //public class Table
        //{

        public class Areatransmodes
        {
            public string AreaTransMode { get; set; }
        }
        public class PostView
        {

            public long SlNo { get; set; }
            public Int16 SortOrder { get; set; }
            public int PostID { get; set; }
            public string PostName { get; set; }
            public string PostShortName { get; set; }
            public string PostCode { get; set; }

          
            public Int64 FK_Area { get; set; }

            public string EntrBy { get; set; }

            public DateTime EntrOn { get; set; }
           
            public string UserCode { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
          //  public Int16 FK_Branch { get; set; }
            public string District { get; set; }
           
            public Int64 TotalCount { get; set; }


        }

        public class PlaceByDistrict
        {
            public long ID_Place { get; set; }
            public string Place { get; set; }
        }

        public class PostInputView
        {

        public int PostID { get; set; }
         
            public string PostName { get; set; }
          
            public string PostShortName { get; set; }
          
            public string PostCode { get; set; }
          
            public long AreaID { get; set; }
            public string AreaName { get; set; }


            public Int16 SortOrder { get; set; }
            public string TransMode { get; set; }

        }

        public static string _updateProcedureName = "proPostUpdate";
        public static string _deleteProcedureName = "proPostDelete";
        public static string _selectProcedureName = "ProPostSelect";
        public class UpdatePost
        {

            public int UserAction { get; set; }
            public int Debug { get; set; }
            public string TransMode { get; set; }
            public long FK_Post { get; set; }
            public long ID_Post { get; set; }
        
            public string PostName { get; set; }
            public string PostShortName { get; set; }
            public string PinCode { get; set; }
            public Int32 SortOrder { get; set; }
            public Int64 FK_Area { get; set; }
           
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long BackupId { get; set; }
         


        }
        public class Post_ID
        {
            public Int64 FK_Post { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_Company { get; set; }
            public string Name { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
        }

        public class Place_ID
        {
           
            public Int16 ID_Place { get; set; }
            public string Place { get; set; }


        }
        public class District_ID
        {
           
            public Int64 ID_District { get; set; }
        }

        public class DeleteView
        {
  
            public long PostID { get; set; }
            [Required(ErrorMessage = "Please Select a Reason")]
            public Int64 ReasonID { get; set; }
        }
        public class DeletePost
        {
            public string TransMode { get; set; }
            public Int64 FK_Post { get; set; }
            public int Debug { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Reason { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int32 FK_BranchCodeUser { get; set; }

        }


        



        public class LocationList
        {
            public List<Area> AreaList { get; set; }
           // public List<District> DistrictList { get; set; }
            public int SortOrder { get; set; }
            public string AreaTransMode { get; set; }
        }
        
        public class Area
        {
            public Int64 AreaID { get; set; }
             public String AreaName { get; set; }
        }

        public class InputPost
        {
            public Int16 ID_Post { get; set; }
        }

        
       
        public Output UpdatePostData(UpdatePost input, string companyKey)
        {
            //// UpdateCustomer table = this.ConvertToUpdateCustomer(input);
            return Common.UpdateTableData<UpdatePost>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }

        public Output DeletePostData(DeletePost input, string companyKey)
        {
            return Common.UpdateTableData<DeletePost>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }

        public APIGetRecordsDynamic<PostView> GetPostData(Post_ID input, string companyKey)
        {
            return Common.GetDataViaProcedure<PostView, Post_ID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }



    }
}