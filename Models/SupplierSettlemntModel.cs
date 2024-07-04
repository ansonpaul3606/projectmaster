using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class SupplierSettlemntModel
    {
        public class DropDownListModel
        {
            public List<PaymentMethodModel.PaymentMethodView> PaymentView { get; set; }
            public List<BillType> BillTypeList { get; set; }
            public List<BillTypeModel.BillTypeView> BillTypeListView { get; set; }
            public List<LeadFrom> BranchNameList { get; set; }
            public List<Branchs> BranchList { get; set; }
            public List<BranchTypes> BranchTypelists { get; set; }
        }
        public class BillType
        {
            public long ID_BillType { get; set; }
            public string BTName { get; set; }
            public string Mode { get; set; }
        }
        public class Branchs
        {
            public int BranchID { get; set; }
            public string Branch { get; set; }
            public long FK_Branch { get; set; }
        }

        public class BranchTypes
        {
            public int BranchTypeID { get; set; }
            public string BranchType { get; set; }
            public long BranchModeID { get; set; }
            public long FK_BranchMode { get; set; }
            public long FK_BranchType { get; set; }
        }

        public class LeadFrom
        {
            public Int32 ID_LeadFrom { get; set; }
            public string LeadFromName { get; set; }
        }
    }
}