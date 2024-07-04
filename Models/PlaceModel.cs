using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PerfectWebERP.General;



namespace PerfectWebERP.Models
{


    public class PlaceModel
    {
   


        public class PlaceView
        {
            public long SlNo { get; set; }
            public string District { get; set; }
            public string Place { get; set; }
            public string ShortName { get; set; }
            public string DtName { get; set; }
            public Int16 SortOrder { get; set; }
            public int PlaceID { get; set; }
            public int PostID { get; set; }
            public Int64 TotalCount { get; set; }

        }
        public class PlaceInputView
        {
            public long SlNo { get; set; }
            //[Required(ErrorMessage = "Please Select a place")]
            public string Place { get; set; }
            [Required(ErrorMessage = "Please Enter Short name")]
            public string ShortName { get; set; }
            [Required(ErrorMessage = "Please Enter Sort Order")]
            public Int16 SortOrder { get; set; }

            //[Required(ErrorMessage = "Please Select a place")]
            public int PlaceID { get; set; }
            //public int ID_Place { get; set; }
            [Required(ErrorMessage = "Please Select a District")]

            public int PostID { get; set; }
            public string PostName { get; set; }
            public Int64 TotalCount { get; set; }
            public string TransMode { get; set; }
            //public string DtName { get; set; }
        }
        public class Place_ID
        {
            public int PlaceID { get; set; }
            public int PostID { get; set; }

        }
        public static string _updateProcedureName = "proPlaceUpdate";
        public class UpdatePlace
        {
            
            public Int64 ID_Place { get; set; }
            public Int64 FK_Place { get; set; }
            public Int64 FK_Post { get; set; }
            public Int16 SortOrder { get; set; }
            public string PlcName { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string TransMode { get; set; }
            public long Debug { get; set; }
            public string EntrBy { get; set; }
            //public long FK_Reason { get; set; }
            public long FK_Machine { get; set; }
            public byte UserAction { get; set; }
           
            public string PlcShortName { get; set; }
            
       
        }
        public static string _deleteProcedureName = "proPlaceDelete";
        public class DeletePlace
        {
            public Int64 FK_Place { get; set; }
            public string TransMode { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long Debug { get; set; }
            public long FK_Reason { get; set; }
            public long FK_BranchCodeUser { get; set; }

        }
        public static string _selectProcedureName = "proPlaceSelect";
        public class GetPlace
        {
            public Int64 FK_Place { get; set; }
            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
           public string Name { get; set; }


        }
        public class ID
        {
            public Int64 ID_Place { get; set; }
            public long ReasonID { get; set; }
        }
        public class Locationlist
        {
            public List<Post> PostList { get; set; }
            public int SortOrder { get; set; }
        }

        public class Post
        {
            public int PostID { get; set; }
            public string PostName { get; set; }
        }


        public Output UpdatePlaceData(UpdatePlace input, string companyKey)
        {
            //// UpdateCustomer table = this.ConvertToUpdateCustomer(input);
            return Common.UpdateTableData<UpdatePlace>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeletePlaceData(DeletePlace input, string companyKey)
        {
            return Common.UpdateTableData<DeletePlace>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<PlaceView> GetPlacetData(GetPlace input, string companyKey)
        {
            return Common.GetDataViaProcedure<PlaceView, GetPlace>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }


    }
}