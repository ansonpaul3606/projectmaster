using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PerfectWebERPAPI.Interface
{
    public interface IFunctions
    {
        string DbName { get; set; }
        string TableName { get; set; }
        string Module { get; set; }
        Int32 MachineID { get; set; }
        Int16 BranchCodeUser { get; set; }
        string UserCode { get; set; }
        string ObjectName { get; set; }
        string ObjectParameters { get; set; }
        string ObjectArguments { get; set; }
        string ObjectDataTypes { get; set; }
        string QuerytoExecute { get; set; }
        string ObjectSplitChar { get; set; }
        byte[] ObjectPhoto { get; set; }
        byte[] ObjectSign { get; set; }
    }
}
