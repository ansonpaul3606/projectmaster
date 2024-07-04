//@*----------------------------------------------------------------------
//Created By  : Athul M.
//Created On  : 22/01/2022
//Purpose     : DocumentType
//-------------------------------------------------------------------------
//Modification
//On          By OMID/Remarks
//-------------------------------------------------------------------------
//-------------------------------------------------------------------------*@
using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace PerfectWebERP.Models
{
    public class DocumentTypeModel
    {
        public class DocumentTypeView
        {
            public Int32 DocumentTypeID { get; set; }
            [Required(ErrorMessage = "Please Enter Document Name")]
            [StringLength(150, ErrorMessage = "Maximum 150 character")]
            public string Document { get; set; }
            [Required(ErrorMessage = "Please Enter Document Short Name")]
            [StringLength(10, ErrorMessage = "Maximum 10 characture")]
            public string ShortName { get; set; }
            [Required(ErrorMessage = "Please Enter Document Description")]
            public string Description { get; set; }
            public string TransMode { get; set; }
            public Int32 SortOrder { get; set; }
            public Int64 TotalCount { get; set; }
        }
        public class UpdateDocumentType
        {
            public int UserAction { get; set; }
            public int Debug { get; set; }
            public string TransMode { get; set; }
            public long ID_DocumentType { get; set; }
            public string DocTName { get; set; }
            public string DocTShortName { get; set; }
            public string DocTDescription { get; set; }
            public Int32 SortOrder { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
           // public DateTime EntrOn { get; set; }

            public long FK_Machine { get; set; }
            public long BackupId { get; set; }
            public Int64 FK_DocumentType { get; set; }
           
            


        }
        public static string _deleteProcedureName = "proDocumentTypeDelete";
        public static string _updateProcedureName = "proDocumentTypeUpdate";
        public static string _selectProcedureName = "ProDocumentTypeSelect";
        public class DocList
        {
            public int SortOrder { get; set; }
        }

        public class DeleteView
        {

            public long DocumentTypeID { get; set; }
            [Required(ErrorMessage = "Please Select a Reason")]
            public Int64 ReasonID { get; set; }
        }




        public class DeleteDocumentType
        {
            public string TransMode { get; set; }
            public Int64 FK_DocumentType { get; set; }
            public int Debug { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Reason { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int32 FK_BranchCodeUser { get; set; }
           
        }


        public class DocumentTypeID
        {
            public Int64 FK_DocumentType { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_Company { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string Name { get; set; }
        }
        public Output UpdateDocumentTypeData(UpdateDocumentType input, string companyKey)
        {
            return Common.UpdateTableData<UpdateDocumentType>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteDocumentTypeData(DeleteDocumentType input, string companyKey)
        {
            return Common.UpdateTableData<DeleteDocumentType>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<DocumentTypeView> GeDocumentTypeData(DocumentTypeID input, string companyKey)
        {
            return Common.GetDataViaProcedure<DocumentTypeView, DocumentTypeID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }
    }
}


