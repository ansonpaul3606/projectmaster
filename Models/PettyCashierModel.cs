
using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace PerfectWebERP.Models
{
    public class PettyCashierModel
    {
        public class PettyCashierModelViewModel
        {
            public long SlNo { get; set; }
            public long PettyCashierID { get; set; }
           public string Names { get; set; }
            public string ShortName { get; set; }
            public bool Active { get; set; }
            public long SortOrder { get; set; }
            public long AccountHead { get; set; }
            public long AccountHeadSub { get; set; }
            public string AHeadName { get; set; }
            public int AccountCode { get; set; }
            public int AccountSHCode { get; set; }
            public string ASHeadName { get; set; }
            public long TotalCount { get; set; }
            public long ReasonID { get; set; }



        }
        public class AccountHeadView
        {

            public long AccountHead { get; set; }

            public string AHeadName { get; set; }
            public int AccountCode { get; set; }


        }

        public class AccountSubHeadView
        {
            public long AccountHeadSub { get; set; }
            public int AccountSHCode { get; set; }
            public string ASHeadName { get; set; }

        }
        public class PettyCashierListModel
        {

            

            public int SortOrder { get; set; }

        }

        public class UpdatePettyCashier
        {
            public long ID_PettyCashier { get; set; }
            public long FK_PettyCashier { get; set; }
            public string PtyCshrName { get; set; }
            public string PtyCshrShortName { get; set; }
            public bool PtyCshrActive { get; set; }
            public long FK_AccountHead { get; set; }
            public long FK_AccountHeadSub { get; set; }
            public long SortOrder { get; set; }
            public long FK_Company { get; set; }
            public Int16 UserAction { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }



        }
        public class GetPettyCashier
        {
            public Int64 FK_PettyCashier { get; set; }
            public Int64 FK_Company { get; set; }
            public string TransMode { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }


            public string Name { get; set; }
        }
        public class PettyCashierInfoView
        {
          
            public Int64 PettyCashierID { get; set; }
          
            public Int64 ReasonID { get; set; }
        }

        public class DeletePettyCashier
        {
            public long FK_PettyCashier { get; set; }
            public string TransMode { get; set; }
            public long FK_Reason { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }

        }
        public class GetPettyCashierinfo
        {
            public Int64 FK_PettyCashier { get; set; }
            public Int64 FK_Company { get; set; }
            public string TransMode { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }


            public string Name { get; set; }
        }
        public class OutputTest
        {
            public long Column1 { get; set; }
            public int ErrCode { get; set; }
            public string ErrMsg { get; set; }
        }

        public static string _deleteProcedureName = "ProPettyCashierDelete";
        public static string _selectProcedureName = "ProPettyCashierSelect";
        public static string _updateProcedureName = "ProPettyCashierUpdate";
        //public Output UpdatePettyCashierData(UpdatePettyCashier input, string companyKey)
        //{
        //    return Common.UpdateTableData<UpdatePettyCashier>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        //}
        public APIGetRecordsDynamic<OutputTest> UpdatePettyCashierData(UpdatePettyCashier input, string companyKey)
        {
            return Common.GetDataViaProcedure<OutputTest, UpdatePettyCashier>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeletePettyCashierData(DeletePettyCashier input, string companyKey)
        {
            return Common.UpdateTableData<DeletePettyCashier>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<PettyCashierModelViewModel> GetPettyCashierData(GetPettyCashierinfo input, string companyKey)
        {
            return Common.GetDataViaProcedure<PettyCashierModelViewModel, GetPettyCashierinfo>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }
    }
}