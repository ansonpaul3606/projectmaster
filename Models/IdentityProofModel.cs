/*----------------------------------------------------------------------
Created By	: Diljith
Created On	: 30/01/2022
Purpose		: Category
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
    public class IdentityProofModel
    {

        public class IdentityProofView
        {
            public long SlNo { get; set; }
            public long IdentityProofID { get; set; }
            public string IdentityProof { get; set; }
            public string ShortName { get; set; }
            public Int32 IDType { get; set; }
            public string IDtypeName { get; set; }
            public Int32 SortOrder { get; set; }
            public bool IDProof { get; set; }
            public bool AddressProof { get; set; }
            public bool Mask { get; set; }
            public bool Mandatory { get; set; }

            public Int64 TotalCount { get; set; }


        }
        public class IdentityProofInputFromViewModel
        {
            ////[Required(ErrorMessage = "No IdentityProof Selected")]
            public long IdentityProofID { get; set; }

            ////[Required(ErrorMessage = "Please Enter IdentityProof")]
            public string IdentityProof { get; set; }

            ////[Required(ErrorMessage = "Please Enter Short Name")]
            public string ShortName { get; set; }

            //[Range(1, long.MaxValue, ErrorMessage = "Select Category Mode")]
            public Int32 IDType { get; set; }
            public string IDtypeName { get; set; }

            public Int32 SortOrder { get; set; }

            //[Required(ErrorMessage = "Please Enter IDProof")]
            public bool IDProof { get; set; }

            //[Required(ErrorMessage = "Please Enter AddressProof")]
            public bool AddressProof { get; set; }

            //[Required(ErrorMessage = "Please Enter Mask")]
            public bool Mask { get; set; }

            //[Required(ErrorMessage = "Please Enter Mandatory")]
            public bool Mandatory { get; set; }

            public string TransMode { get; set; }

        }
        public class IdentityProofInfoView
        {
            [Required(ErrorMessage = "No IdentityProof Selected")]
            public Int64 IdentityProofID { get; set; }
            [Required(ErrorMessage = "Please Select The Reason")]
            public Int64 ReasonID { get; set; }
        }



        public static string _updateProcedureName = "proIdentityProofUpdate";
        public class UpdateIdentityProof
        {

            public byte UserAction { get; set; }
            public Int64 FK_Branch { get; set; }
            public Int64 FK_IdentityProof { get; set; }
            public long ID_IdentityProof { get; set; }
            public Int64 IdType { get; set; }
            public string IdName { get; set; }
            public string ShortName { get; set; }
            public Int32 SortOrder { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            //public bool Passed { get; set; }
            public string EntrBy { get; set; }
            //public long FK_Reason { get; set; }
            public long FK_Machine { get; set; }
            public long BackupId { get; set; }
            public bool IdAddrProof { get; set; }
            public bool IdProof { get; set; }
            public bool IdExpiryDateValidation { get; set; }
            public bool IdReqMask { get; set; }
            public string TransMode { get; set; }

        }

        public static string _selectProcedureName = "proIdentityProofSelect";
        public class GetIdentityProof
        {
            public Int64 FK_IdentityProof { get; set; }
            public string TransMode { get; set; }
            public Int64 FK_Company { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string Name { get; set; }
        }

        public static string _deleteProcedureName = "proIdentityProofDelete";
        public class DeleteIdentityProof
        {
            public long FK_IdentityProof { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Reason { get; set; }
            public Int64 FK_Branch { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
        }
        public class ModeList
        {
            public Int32 IDType { get; set; }
            public string IDtypeName { get; set; }
          
        }
        public class IdentityProofListModel
        {

            public List<ModeList> ModeList { get; set; }

            public int SortOrder { get; set; }

        }


        public Output UpdateIdentityProofData(UpdateIdentityProof input, string companyKey)
        {
            return Common.UpdateTableData<UpdateIdentityProof>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteIdentityProofData(DeleteIdentityProof input, string companyKey)
        {
            return Common.UpdateTableData<DeleteIdentityProof>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<IdentityProofView> GetIdentityProofData(GetIdentityProof input, string companyKey)
        {
            return Common.GetDataViaProcedure<IdentityProofView, GetIdentityProof>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }
    }
}

