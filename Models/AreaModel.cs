using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class AreaModel
    {
        public class Area
        {

            // [Required(ErrorMessage = "No State Selected")]
            public long ID_Area { get; set; }

            [Required(ErrorMessage = "Please Enter Area Name")]
            public string AreaName { get; set; }
            [Required(ErrorMessage = "Please Enter Short Name For Area ")]
            public string AreaShortName { get; set; }

            //[Required(ErrorMessage = "Please Select From Country")]
            [DisplayName("District")]
            [Range(1, long.MaxValue, ErrorMessage = "Select District")]
            public long DistrictID { get; set; }
            public Int32 SortOrder { get; set; }
            public Int64 FK_Company { get; set; }
            public byte UserAction { get; set; }
            public Int64 FK_Machine { get; set; }

            public Int64 FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }

            public long FK_Branch { get; set; }

            public Int64 FK_Area { get; set; }
            public string TransMode { get; set; }

            public Int64 ReasonID { get; set; }
            public string District { get; set; }
            public long TypeID { get; set; }
            public Int64 TotalCount { get; set; }
        }

        public static string _updateProcedureName = "proAreaUpdate";
        public class UpdateArea
        {

            public long ID_Area { get; set; }

            public string AreaName { get; set; }
            public string AreaShortName { get; set; }

            //[Required(ErrorMessage = "Please Select From Country")]

            public long DistrictID { get; set; }

            public Int32 SortOrder { get; set; }
            public Int64 FK_Company { get; set; }
            public byte UserAction { get; set; }
            public Int64 FK_Machine { get; set; }
            public string TransMode { get; set; }


            public Int64 FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }

            public long FK_Branch { get; set; }

            public Int64 FK_Area { get; set; }
            public long TypeID { get; set; }
        }

        public class AreaTypeList
        {
            public short ID_Mode { get; set; }
            public string ModeName { get; set; }
        }


        public class AreaListModel
        {
            public List<DistrictModel.DistrictView> DistrictList { get; set; }
            // public List<District> DistrictList;
            public List<AreaTypeList> AreaList { get; set; }
            public Int64 SortOrder { get; set; }
        }
        public class DeleteAreaModel
        {
            public long ID_Area { get; set; }
            
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }

            public Int64 FK_Reason { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
        }

        public class InputArea
        {
            public Int64 ID_Area { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
        }

        public class ModeLead
        {
            public Int32 Mode { get; set; }
        }

        public class GetArea
        {
            public Int64 ID_Area { get; set; }
            public Int64 FK_Company { get; set; }
            public string TransMode { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            public string Name { get; set; }
        }
        public Output UpdateArearData(UpdateArea input, string companyKey)
        {
            //// UpdateCustomer table = this.ConvertToUpdateCustomer(input);
            return Common.UpdateTableData<UpdateArea>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public static string _selectProcedureName = "proAreaSelect";
        public APIGetRecordsDynamic<AreaModel> GetDistrictData(InputArea input, string companyKey)
        {
            return Common.GetDataViaProcedure<AreaModel, InputArea>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }

        public static string _deleteProcedureName = "ProAreaDelete";
        public Output DeleteAreaData(DeleteAreaModel input, string companyKey)
        {
            return Common.UpdateTableData<DeleteAreaModel>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<AreaTypeList> GeLeadStatusList(ModeLead input, string companyKey)
        {
            return Common.GetDataViaProcedure<AreaTypeList, ModeLead>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }
        public APIGetRecordsDynamic<Area> GetAreaData(GetArea input, string companyKey)
        {
            return Common.GetDataViaProcedure<Area, GetArea>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }

    } 
}