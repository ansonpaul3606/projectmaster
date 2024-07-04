using System;
using System.Collections.Generic;
using System.Text;

namespace PerfectWebERP.Business
{
    public class BLCommon
    {
        public class Output
        {
            public bool IsProcess { get; set; }
            public string Message { get; set; }
            public string Code { get; set; }
            public dynamic Data { get; set; }
        }

    }
}
