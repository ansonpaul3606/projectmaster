/*- Created By : Amritha A K  Created On : 08/02/2022  Purpose  : Occupation  ---  Modification  On   By     OMID/Remarks  -------------------------------------------------------------------------  -------------------------------------------------------------------------*/  using PerfectWebERP.General;
/*----------------------------------------------------------------------
Created By	: Amritha A K
Created On	: 08/02/2022
Purpose		: Occupation
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
    public class OccupationModel
    {

        public class OccupationView
        {
            public long SlNo { get; set; }
            public long ID { get; set; }
            [Required(ErrorMessage = "Please Enter Name")]
            public string Name { get; set; }
            [Required(ErrorMessage = "Please Enter Short Name")]
            public string ShortName { get; set; }
            [Required(ErrorMessage = "Please Enter Sort Order")]
            public Int32 SortOrder { get; set; }
            public string TransMode { get; set; }
        }
        public class OccupationViewList
        {
          
        public Int32 SortOrder { get; set; }
        }
        public class UpdateOccupation
        {
            public long ID_Occupation { get; set; }
            public string OccpName { get; set; }
            public string OccpShortName { get; set; }
            public Int32 SortOrder { get; set; }
            public long FK_Occupation { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string TransMode { get; set; }
            public long Debug { get; set; }
            public string EntrBy { get; set; }
      
            public long FK_Machine { get; set; }
            public byte UserAction { get; set; }
        }
       
        public class DeleteOccupation
        {
            public long ID_Occupation { get; set; }
            public string TransMode { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long Debug { get; set; }
            public long FK_Reason { get; set; }
            public long FK_BranchCodeUser { get; set; }
        }

        public class OccupationInfoView
        {
            public long ID_Occupation { get; set; }

            public long ReasonID { get; set; }
        }

        public class OccupationID
        {
            public Int64 ID_Occupation { get; set; }
            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }

        }

        public static string _deleteProcedureName = "ProOccupationDelete";
        public static string _updateProcedureName = "ProOccupationUpdate";
        public static string _selectProcedureName = "ProOccupationSelect";

        public Output UpdateOccupationData(UpdateOccupation input, string companyKey)
        {
            return Common.UpdateTableData<UpdateOccupation>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteOccupationData(DeleteOccupation input, string companyKey)
        {
            return Common.UpdateTableData<DeleteOccupation>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<OccupationView> GetOccupationData(OccupationID input, string companyKey)
        {
            return Common.GetDataViaProcedure<OccupationView, OccupationID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
    }
}

