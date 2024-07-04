using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PerfectWebERP.Models;
using CrystalDecisions.CrystalReports.Engine;

namespace PerfectWebERP.Reports
{
    public partial class Errorpage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadReport();
        }
        public void LoadReport()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
        }
    }
}