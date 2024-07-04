
//    @*----------------------------------------------------------------------
//Created By	: Amritha A K
//Created On	: 25/01/2022
//Purpose		: branchtype
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
	public class branchtypeModel
        {

        public class branchtypeView
        {
            public long SlNo { get; set; }
            public long BranchtypeID { get; set; }

            [Range(1, long.MaxValue, ErrorMessage = "Select Branch Mode")]
            public byte BranchModeID { get; set; }
            public string BranchMode { get; set; }
            [Required(ErrorMessage = "Please Enter Name")]
            public string Name { get; set; }
           [Required(ErrorMessage = "Please Enter Short Name")]
            public string ShortName { get; set; }
             [Required(ErrorMessage = "Please Enter Sort Order")]
             public Int32 SortOrder { get; set; }
            public Int64 TotalCount { get; set; }
            public string TransMode { get; set; }
        }

            public class Updatebranchtype
            {
                public long ID_BranchType { get; set; }
                public byte FK_BranchMode { get; set; }
                public string BTName { get; set; }
                public string BTShortName { get; set; }
                public Int32 SortOrder { get; set; }
                public long FK_Company { get; set; }
               public long FK_BranchCodeUser { get; set; }
                public string TransMode { get; set; }
               public long Debug { get; set; }
                public string EntrBy { get; set; }
              public long FK_BranchType { get; set; }

                 public long FK_Machine { get; set; }
                  public byte UserAction { get; set; }



        }
            public static string _deleteProcedureName = "proBranchTypeDelete";
            public static string _updateProcedureName = "proBranchTypeUpdate";
            public static string _selectProcedureName = "proBranchTypeSelect";

            public class Deletebranchtype
            {
                public long ID_BranchType { get; set; }
            public string TransMode { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long Debug { get; set; }
            public long FK_Reason { get; set; }
            public long FK_BranchCodeUser { get; set; }

        }


    
        public class BranchMode
        {
            public int BranchModeID { get; set; }
            public string BranchModeName { get; set; }
           // public long FK_States { get; set; }

        }
        public class BranchViewList
        {
            public List<BranchMode> BranchModeList { get; set; }
            public Int32 SortOrder { get; set; }
        }

        public class GetBranchtype
        {
            public Int64 ID_BranchType { get; set; }
            public string UserCode { get; set; }
            public Int64 FK_Company { get; set; }
        }


        public class InputbranchtypeID
        {
            public Int64 ID_BranchType { get; set; }
            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }

            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string Name { get; set; }

        }

        public class BranchtypeInfoView
        {
            public long ID_BranchType { get; set; }

            public long ReasonID { get; set; }
        }


        public Output UpdatebranchtypeData(Updatebranchtype input, string companyKey)
            {
                return Common.UpdateTableData<Updatebranchtype>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
            }
            public Output DeletebranchtypeData(Deletebranchtype input, string companyKey)
            {
                return Common.UpdateTableData<Deletebranchtype>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
            }
            public APIGetRecordsDynamic<branchtypeView> GetbranchtypeData(InputbranchtypeID input, string companyKey)
            {
                return Common.GetDataViaProcedure<branchtypeView, InputbranchtypeID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

            }
        }
    }



