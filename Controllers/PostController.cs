using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using PerfectWebERP.Business;
using PerfectWebERP.DataAccess;
using PerfectWebERP.Interface;
using PerfectWebERP.Models;
using PerfectWebERP.Services;
using System.Data;
using Newtonsoft.Json;

using PerfectWebERP.General;
using System.ComponentModel.DataAnnotations;
using PerfectWebERP.Filters;

namespace PerfectWebERP.Controllers
{
    public class PostController : Controller
    {
        // GET: Post
        public ActionResult Post(string mtd)
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

            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.mtd = mtd;

            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);

            return View();
        }
        public ActionResult PostView(string mtd)
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
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;

            ViewBag.PagedAccessRights = pageAccess;
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.mtd = mtd;

            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);

            PostModel.LocationList output = new PostModel.LocationList();

            var AreaList = Common.GetDataViaQuery<PostModel.Area>(parameters: new APIParameters
            {
                TableName = "Area",
                SelectFields = "ID_Area AS AreaID,AreaName AS AreaName",
                Criteria = "Cancelled=0 AND Passed=1 AND FK_Company="+ _userLoginInfo.FK_Company ,
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey
            );

            output.AreaList = AreaList.Data;

            var a = Common.GetDataViaProcedure<NextSortOrderOutput, NextSortOrder>(
             companyKey: _userLoginInfo.CompanyKey,
             procedureName: "ProGetNextNo",
             parameter: new NextSortOrder
             {
                 TableName = "Post",
                 FieldName = "SortOrder",
                 Debug = 0
             });

            output.SortOrder = a.Data[0].NextNo;
            var AreaTransMode = Common.GetDataViaQuery<PostModel.Areatransmodes>(parameters: new APIParameters
            {
                TableName = "MenuList",
                SelectFields = "TransMode as AreaTransMode",
                Criteria = "ControllerName='Area'AND cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
       companyKey: _userLoginInfo.CompanyKey);

          
            string Area = AreaTransMode.ToString();
            ViewBag.AreaTransMode = objCmnMethod.EncryptString(Area);
           
           

            return PartialView("_AddPostForm", output);
        }



        [HttpGet]
        public ActionResult GetAreaList()
        {
            #region ::  Check User Session to verifyLogin  ::

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

            #endregion ::  Check User Session to verifyLogin  ::

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            var data = Common.GetDataViaQuery<PostModel.Area>(parameters: new APIParameters
            {
                TableName = "Area",
                SelectFields = "ID_Area AS AreaID,AreaName AS AreaName",
                Criteria = "Cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey

               );

            return Json(data, JsonRequestBehavior.AllowGet);

        }

        //[HttpGet]
        //public ActionResult GetDistrictList()
        //{
        //    #region ::  Check User Session to verifyLogin  ::

        //    if (Session["UserLoginInfo"] is null)
        //    {
        //        return Json(new
        //        {
        //            Process = new Output
        //            {
        //                IsProcess = false,
        //                Message = new List<string> { "Please login to continue" },
        //                Status = "Session Timeout",
        //            }
        //        }, JsonRequestBehavior.AllowGet);
        //    }

        //    #endregion ::  Check User Session to verifyLogin  ::

        //    UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
        //    var data = Common.GetDataViaQuery<District>(parameters: new APIParameters
        //    {
        //        TableName = "District",
        //        SelectFields = "ID_District AS DistrictID,DtName AS DistrictName",
        //        Criteria = "Cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
        //        SortFields = "",
        //        GroupByFileds = ""
        //    },
        //    companyKey: _userLoginInfo.CompanyKey

        //       );

        //    return Json(data, JsonRequestBehavior.AllowGet);

        //}
        [HttpPost]
       //[ValidateAntiForgeryToken()]
        public ActionResult AddPost(PostModel.PostInputView newpost)
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

            Output _output = new Output();
            ModelState.Remove("AreaName");
            ModelState.Remove("AreaID");
          
            // ModelState.Remove("PostID");

            ModelState.Remove("SortOrder");
            if (!ModelState.IsValid)
            {

                // since no need to continue just return
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = ModelState.Values.SelectMany(m => m.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList(),
                        Status = "Validation failed",
                    }
                }, JsonRequestBehavior.AllowGet);
            }
   
            PostModel post = new PostModel();
            var datresponse = post.UpdatePostData(input: new PostModel.UpdatePost
            {

                UserAction = 1,
                Debug = 0,
                TransMode = newpost.TransMode,
                ID_Post=newpost.PostID,
                PostName = newpost.PostName,
                PostShortName = newpost.PostShortName,
                PinCode = newpost.PostCode,
                SortOrder = newpost.SortOrder,
                FK_Area= newpost.AreaID,
              
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                BackupId = newpost.PostID,
                FK_Post = newpost.PostID,
                


            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
      //  [ValidateAntiForgeryToken()]
        public ActionResult UpdatePost(PostModel.PostInputView newpost)
        {

            #region ::  Check User Session to verifyLogin  ::

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

            #endregion ::  Check User Session to verifyLogin  ::
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            ModelState.Remove("AreaName");
            ModelState.Remove("AreaID");
          
            // ModelState.Remove("PostID");

            ModelState.Remove("SortOrder");
            #region :: Model validation  ::
            if (!ModelState.IsValid)
            {

                // since no need to continue just return
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = ModelState.Values.SelectMany(m => m.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList(),
                        Status = "Validation failed",
                    }
                }, JsonRequestBehavior.AllowGet);
            }


            #endregion :: Model validation  ::

            PostModel post = new PostModel();

            var datresponse = post.UpdatePostData(input: new PostModel.UpdatePost
            {



                UserAction = 2,
                Debug = 0,
                TransMode = newpost.TransMode,
                ID_Post = newpost.PostID,
                PostName = newpost.PostName,
                PostShortName = newpost.PostShortName,
                PinCode = newpost.PostCode,
                SortOrder = newpost.SortOrder,
                FK_Area = newpost.AreaID,
              
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                BackupId = newpost.PostID,
                FK_Post = newpost.PostID,






            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        //public ActionResult GetPostList()
        //{
        //    if (Session["UserLoginInfo"] is null)
        //    {
        //        return Json(new
        //        {
        //            Process = new Output
        //            {
        //                IsProcess = false,
        //                Message = new List<string> { "Please login to continue" },
        //                Status = "Session Timeout",
        //            }
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //    UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

        //    PostModel POSTlist = new PostModel();

        //    var outputList = POSTlist.GetPostData(companyKey: _userLoginInfo.CompanyKey, input: new PostModel.Post_ID
        //    {
        //        FK_Post = 0,
        //        FK_Company = _userLoginInfo.FK_Company,
        //        FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
        //        FK_Machine = _userLoginInfo.FK_Machine,
        //    });
        //    return Json(outputList, JsonRequestBehavior.AllowGet);


        //}


        [HttpPost]

        public ActionResult GetPostList(int pageSize, int pageIndex, string Name)
        {
            #region ::  Check User Session to verifyLogin  ::

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

            #endregion ::  Check User Session to verifyLogin  ::

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            string transMode = "";

            PostModel POSTlist = new PostModel();

            var data =POSTlist.GetPostData(companyKey: _userLoginInfo.CompanyKey, input: new PostModel.Post_ID
            {
                FK_Post=0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                TransMode = transMode
            });

            // return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]


       // [ValidateAntiForgeryToken()]
        public ActionResult GetPostInfo(PostModel.DeleteView data)
        {

            #region ::  Check User Session to verifyLogin  ::

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

            #endregion ::  Check User Session to verifyLogin  ::


            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            ModelState.Remove("PostID");
            ModelState.Remove("PostName");
            ModelState.Remove("SortOrder");
            ModelState.Remove("PostShortName");
            ModelState.Remove("PostCode");
            ModelState.Remove("FK_Area");
          

            #region :: Model validation  ::
            //removing a node in model while validating
            //--- Model validation 
            if (!ModelState.IsValid)
            {

                // since no need to continue just return
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = ModelState.Values.SelectMany(m => m.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList(),
                        Status = "Validation failed",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion :: Model validation  ::


            PostModel post = new PostModel();

            var PostInfo = post.GetPostData(companyKey: _userLoginInfo.CompanyKey, input: new PostModel.Post_ID
            {
                FK_Post = data.PostID,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine
            });


            return Json(PostInfo, JsonRequestBehavior.AllowGet);

        }

        public ActionResult DeletePost(PostModel.DeleteView data)
        {
            #region ::  Check User Session to verifyLogin  ::

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

            #endregion ::  Check User Session to verifyLogin  ::

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
         
            #region :: Model validation  ::
            //removing a node in model while validating
            //--- Model validation 
            if (!ModelState.IsValid)
            {

                // since no need to continue just return
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = ModelState.Values.SelectMany(m => m.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList(),
                        Status = "Validation failed",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion :: Model validation  ::

            PostModel post = new PostModel();

            Output datresponse = post.DeletePostData(input: new PostModel.DeletePost {
                FK_Post = data.PostID,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Reason = data.ReasonID,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                Debug = 0,
                TransMode = "",

            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPlaceList(PostModel.District_ID plc)
        {

            #region ::  Check User Session to verifyLogin  ::

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

            #endregion ::  Check User Session to verifyLogin  ::


            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            #region :: Model validation  ::
            //removing a node in model while validating
            //--- Model validation 
            if (!ModelState.IsValid)
            {

                // since no need to continue just return
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = ModelState.Values.SelectMany(m => m.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList(),
                        Status = "Validation failed",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion :: Model validation  ::


            var placeList = Common.GetDataViaQuery<PostModel.PlaceByDistrict>(parameters: new APIParameters
            {
                TableName = "Place P JOIN District D ON D.ID_District=P.FK_District  AND D.Cancelled=0 AND D.Passed=1 AND D.FK_Company=" +_userLoginInfo.FK_Company,
                SelectFields = "P.ID_Place,P.PlcName AS Place",
                Criteria = "P.FK_District="+ plc.ID_District + " AND P.Cancelled=0 AND P.Passed=1 AND P.FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey
            );

            //var PlaceInfo = Common.GetDataViaProcedure<PostModel.Place_ID, PostModel.District_ID>(companyKey: _userLoginInfo.CompanyKey, procedureName: "ProDistrictPlaceSelect", parameter: new PostModel.District_ID { FK_District = plc.FK_District });

            return Json(placeList, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetFilter()
        {

            List<FilterKeyValuePair> keyValuePairs = new List<FilterKeyValuePair>();
            for (int i = 0; i < 10; i++)
            {
                keyValuePairs.Add(new FilterKeyValuePair { FilterJSONKey = "key " + i, FilterJSONvalue = "value " + i });
            }




            SearchFilter PostNameSearch = new SearchFilter()
            {
                FilterNameKey = "PostNameSearch",
                //FilterDataURL = "/filter/cu",
                FilterType = "input",
                FilterData = new FilterData_input
                {
                    FilterLabel = "Post Name",
                    FilterName = "PostName",
                    Placeholder = "Enter Post name"
                }
            };

            SearchFilter PostCodeSearch = new SearchFilter()
            {
                FilterNameKey = "PostCodeSearch",
                //FilterDataURL = "/filter/cu",
                FilterType = "input",
                FilterData = new FilterData_input
                {
                    FilterLabel = "Post Code",
                    FilterName = "PostCode",
                    Placeholder = "Enter Post Code"
                }
            };


            SearchFilter DistrictSearch = new SearchFilter()
            {
                FilterNameKey = "DistrictSearch",
                //FilterDataURL = "/filter/cu",
                FilterType = "dropdown",
                FilterData = new FilterData_dropdown
                {
                    FilterLabel = "District",
                    FilterName = "DistrictID",
                    //FilterValue = "",
                    MultipleSelect = true,
                    SelectOptions = keyValuePairs
                }
            };

            SearchFilter PlaceSearch = new SearchFilter()
            {
                FilterNameKey = "PlaceSearch",
                //FilterDataURL = "/filter/cu",
                FilterType = "dropdown",
                FilterData = new FilterData_dropdown
                {
                    FilterLabel = "Place",
                    FilterName = "PlaceID",
                    //FilterValue = "",
                    MultipleSelect = true,
                    SelectOptions = keyValuePairs
                }
            };
            List<SearchFilter> GetALLFilter = new List<SearchFilter>();
            List<SearchFilter> GetALLFilter2 = new List<SearchFilter>
                            {
                                PostNameSearch,
                                PostCodeSearch,
                                DistrictSearch,
                                PlaceSearch,
                            };

            return Json(new { GetALLFilter2 }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetPostDeleteReasonList()
        {
            #region ::  Check User Session to verifyLogin  ::

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

            #endregion ::  Check User Session to verifyLogin  ::


            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            ReasonModel reason = new ReasonModel();

            var outputList = reason.GetReasonData(companyKey: _userLoginInfo.CompanyKey, input: new ReasonModel.InputReasonID { FK_Reason = 0, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Machine = _userLoginInfo.FK_Machine, PageIndex = 0, PageSize = 0, TransMode = "" });


            APIGetRecordsDynamic<ReasonModel.ReasonsView> deleteReason = new APIGetRecordsDynamic<ReasonModel.ReasonsView>
            {
                Process = outputList.Process,
                Data = outputList.Data.Where(a => a.ModeID == 1).OrderBy(a => a.SortOrder).ToList()
            };


            return Json(deleteReason, JsonRequestBehavior.AllowGet);
        }

    }
}
