using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PerfectWebERP.Controllers
{
    public class ProductionStockListController : Controller
    {
        // GET: ProductionStockList
        public ActionResult Index(string mtd)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.FK_Branch = _userLoginInfo.FK_Branch;
            ViewBag.mtd = mtd;
              CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            return View();
        }
        public ActionResult LoadProductionStocklistForm(string mtd)
        {
            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ViewBag.FK_Branch = _userLoginInfo.FK_Branch;
            ViewBag.FK_Department = _userLoginInfo.FK_Department;


         
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            return PartialView("_AddProductionStockList");
        }
        public ActionResult GetAnalysisreportdetail(ProductionStockListModel.ProductionStockListModelView Data)

        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ProductionStockListModel objfld = new ProductionStockListModel();

            var data = objfld.GetProductionStockListData(input: new ProductionStockListModel.ProductionStockListModelView
            {
                FK_Product=Data.FK_Product,
                Quantity=Data.Quantity,
                DeliveryDate=Data.DeliveryDate,
                Mode = 1,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine
            },

            companyKey: _userLoginInfo.CompanyKey);



            return Json(data, JsonRequestBehavior.AllowGet);
            //return Json(new { data }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GetAnalysisreportsubsectiondetail(ProductionStockListModel.ProductionStockListModelView Data)

        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ProductionStockListModel objfld = new ProductionStockListModel();

            var datasub = objfld.GetProductionStockSubListData(input: new ProductionStockListModel.ProductionStockListModelView
            {
                FK_Product = Data.FK_Product,
                Quantity = Data.Quantity,
                DeliveryDate = Data.DeliveryDate,
                Mode = 2,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine
            },

            companyKey: _userLoginInfo.CompanyKey);



            return Json( datasub, JsonRequestBehavior.AllowGet);


        }
        public ActionResult GetProductionStatusdetial(ProductionStockListModel.ProductionStockListModelView Data)

        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ProductionStockListModel objfld = new ProductionStockListModel();

            var datastatus = objfld.GetProductionStatusdetial(input: new ProductionStockListModel.ProductionStockListModelView
            {
                FK_Product = Data.FK_Product,
                Quantity = Data.Quantity,
                DeliveryDate = Data.DeliveryDate,
                Mode = 3,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine
            },

            companyKey: _userLoginInfo.CompanyKey);


            return Json(datastatus , JsonRequestBehavior.AllowGet);

    


        }
        public ActionResult GetStatusreportsubsectiondetail(ProductionStockListModel.ProductionStockListModelView Data)

        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ProductionStockListModel objfld = new ProductionStockListModel();

            var datastatus = objfld.GetStatusreportsubsectiondetail(input: new ProductionStockListModel.ProductionStockListModelView
            {
                FK_Product = Data.FK_Product,
                Quantity = Data.Quantity,
                DeliveryDate = Data.DeliveryDate,
                Mode = 4,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine
            },

            companyKey: _userLoginInfo.CompanyKey);


            return Json(datastatus, JsonRequestBehavior.AllowGet);




        }
    }
}