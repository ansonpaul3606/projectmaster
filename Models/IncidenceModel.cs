using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace PerfectWebERP.Models
{
    public class IncidenceModel
    {

            public class IncidencesView
            {
                public long SlNo { get; set; }

                public long IncidenceID { get; set; }

                public string Incidence { get; set; }

                public string ShortName { get; set; }

                public string Description { get; set; }


                public Int32 SortOrder { get; set; }
            public string Mode { get; set; }
            public Int64 TotalCount { get; set; }
            public string AccountHead { get; set; }
            public long FK_AccountHead { get; set; }
            public string AccountSubHead { get; set; }
            public long FK_AccountSubHead { get; set; }
        }

            public class IncidencesInputFromViewModel
            {

                [Required(ErrorMessage = "No Incidence Selected")]
                public long IncidenceID { get; set; }

                [Required(ErrorMessage = "Please Enter Incidence Name")]
                public string Incidence { get; set; }
                [Required(ErrorMessage = "Please Enter Short Name ")]
                public string ShortName { get; set; }
                public string Description { get; set; }
                public Int32 SortOrder { get; set; }
                public string TransMode { get; set; }
                public string Mode { get; set; }
                public string AccountHead { get; set; }
                public long FK_AccountHead { get; set; }
                public string AccountSubHead { get; set; }
                public long FK_AccountSubHead { get; set; }

        }

            public class IncidenceInfoView
            {
                [Required(ErrorMessage = "No Incidence Selected")]
                public Int64 IncidenceID { get; set; }
                [Required(ErrorMessage = "Please select the reason")]
                public Int64 ReasonID { get; set; }

            }


            public static string _updateProcedureName = "proIncidenceUpdate";
            public class UpdateIncidence
            {
                public long FK_Incidence { get; set; }
                public long ID_Incidence { get; set; }
                public string IncidenceName { get; set; }
                public string IncidenceShortName { get; set; }
                public string IncidenceDescription { get; set; }
                public Int32 SortOrder { get; set; }
                public string TransMode { get; set; }

                public Int64 FK_Company { get; set; }
                public Int16 UserAction { get; set; }
                public Int64 FK_Machine { get; set; }
                public Int64 FK_BranchCodeUser { get; set; }
                public string EntrBy { get; set; }
                public string Mode { get; set; }
                public long FK_AccountHead { get; set; }
                public long FK_AccountSubHead { get; set; }

            }


        public static string _deleteProcedureName = "proIncidenceDelete";
            public class DeleteIncidence
            {
                public Int64 FK_Incidence { get; set; }
            public string TransMode { get; set; }
            public long FK_Reason { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }

        }


        public class IncidenceListModel
            {
                public int SortOrder { get; set; }
            public List<ModeList> modeList { get; set; }
        }
        public class ModeList
        {
            public int ModeID { get; set; }
            public string ModeName { get; set; }
            public string Mode { get; set; }
            public int FK_ModuleType { get; set; }
        }
        public class InputIncidenceID
            {
            public Int64 FK_Incidence { get; set; }
            public Int64 FK_Company { get; set; }
            public string TransMode { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            public string Name { get; set; }

        }


            public Output UpdateIncidenceData(UpdateIncidence input, string companyKey)
            {
                return Common.UpdateTableData<UpdateIncidence>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
            }
            public Output DeleteIncidenceData(DeleteIncidence input, string companyKey)
            {
                return Common.UpdateTableData<DeleteIncidence>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
            }


            public static string _selectProcedureName = "proIncidenceSelect";
            public APIGetRecordsDynamic<IncidencesView> GetIncidenceData(InputIncidenceID input, string companyKey)
            {
                return Common.GetDataViaProcedure<IncidencesView, InputIncidenceID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

            }

        }
    }