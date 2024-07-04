using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class MediaPromotionReportModel
    {
        public class MediaPromotionList
        {
            public List<MediaList> MediaList { get; set; }
            public List<SubMediaList> SubMediaList { get; set; }
            public List<Branchs> BranchList { get; set; }
        }
        public class Branchs
        {
            public int BranchID { get; set; }
            public string Branch { get; set; }
        }
        public class MediaList
        {
            public long FK_MediaMaster { get; set; }
            public string MediaName { get; set; }
        }
        public class SubMediaList
        {
            public long FK_MediaSubMaster { get; set; }
            public string SubMediaName { get; set; }
        }
    }
}