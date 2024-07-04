using System.Web;
using System.Web.Optimization;

namespace PerfectWebERP
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            //moved ~/Content/site.css to another bundle
            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Content/css").Include(
                 "~/Content/bootstrap.css"));

            //---  22 December 2021 | Sonin : Add custom bundle  [  S T A R T  ]

            //--- Website template CSS
            /*
                Note :
                ~/Assets/css/style.css has some other css referance imported in it 
             */
            bundles.Add(new StyleBundle("~/Content/perfectERPTemplateCSS").Include(
                 "~/Assets/css/style.css"
                 ));

            //--- Website template JS
            /*
                 Note :
                 JQuery and bootsrtap js code is inside global.min.js(~/Assets/vendor/global/global.min.js) ,so both jquery and bootsstrap referance in the project(default ~/bundles/jquery| ~/bundles/bootstrap) is commented in _Layout
                 if we want to use this we need to fist comment the jqery and boorstarp section in global.min.js and then uncomment ~/bundles/jquery & ~/bundles/bootstrap in _layout
             */
            bundles.Add(new ScriptBundle("~/bundles/perfectERPTemplateJS").Include(
                "~/Assets/vendor/global/global.min.js"
                ));

            #region :: Datatable Referance ::
            //--- Datatable plugin script referance 
            bundles.Add(new ScriptBundle("~/bundles/datatablePlugin").Include(
                      "~/Assets/vendor/datatables/js/jquery.dataTables.min.js"
                      , "~/Assets/vendor/datatables/extra/dataTables.buttons.min.js"
                      , "~/Assets/vendor/datatables/extra/buttons.bootstrap4.min.js"
                      , "~/Assets/vendor/datatables/extra/jszip.min.js"
                      , "~/Assets/vendor/datatables/extra/pdfmake.min.js"
                      , "~/Assets/vendor/datatables/extra/vfs_fonts.js"
                      , "~/Assets/vendor/datatables/extra/buttons.html5.min.js"
                      , "~/Assets/vendor/datatables/extra/buttons.print.min.js"
                      , "~/Assets/vendor/datatables/extra/buttons.colVis.min.js"
                      ));
            //--- Datatable plugin css referance
            bundles.Add(new StyleBundle("~/Content/datatablePluginCSS").Include(
                    "~/Assets/vendor/datatables/css/jquery.dataTables.min.css"
                    ));
            #endregion :: Datatable Referance ::

            //--- Other external plugin script referance
            bundles.Add(new ScriptBundle("~/bundles/externalPlugin").Include(
                        "~/Assets/vendor/bootstrap-select/dist/js/bootstrap-select.min.js"
                        , "~/Assets/vendor/owl-carousel/owl.carousel.js"
                        , "~/Assets/vendor/moment/moment.min.js"
                        , "~/Assets/vendor/bootstrap-daterangepicker/daterangepicker.js"
                        , "~/Assets/vendor/clockpicker/js/bootstrap-clockpicker.min.js"
                        , "~/Assets/vendor/toastr/js/toastr.min.js"
                        , "~/Assets/vendor/dropzone/dist/dropzone.js"
                        , "~/Assets/vendor/chartist/js/chartist.min.js"
                        , "~/Assets/vendor/chartist-plugin-tooltips/js/chartist-plugin-tooltip.min.js"
                        , "~/Assets/vendor/chart.js/Chart.bundle.min.js"
                        ,"~/Assets/vendor/morris/morris.min.js"
                        ,"~/Assets/vendor/morris/raphael-min.js"                      
                        ));

            //--- Other external plugin css referance
            bundles.Add(new StyleBundle("~/Content/externalPluginCSS").Include(
                     "~/Assets/vendor/bootstrap-select/dist/css/bootstrap-select.min.css"
                     , "~/Assets/vendor/owl-carousel/owl.carousel.css"
                     , "~/Assets/vendor/bootstrap-daterangepicker/daterangepicker.css"
                     , "~/Assets/vendor/clockpicker/css/bootstrap-clockpicker.min.css"
                     , "~/Assets/vendor/pickadate/themes/default.css"
                     , "~/Assets/vendor/pickadate/themes/default.date.css"
                     , "~/Assets/vendor/toastr/css/toastr.min.css"
                     , "~/Assets/vendor/dropzone/dist/dropzone.css"
                     , "~/Assets/vendor/chartist/css/chartist.min.css"
                     ));

            //--- Add custom js file which are required globally in this bundle
            bundles.Add(new ScriptBundle("~/bundles/perfectERP/js").Include(
                "~/Scripts/custom.js"
                  , "~/Scripts/jqDOM/customClassValidations.js"
                , "~/Scripts/jqDOM/tableDOMCreation.js"
                , "~/Scripts/jqDOM/searchDOMCreation.js"
                , "~/Scripts/commonFuncitons.js"
                , "~/Scripts/partialViewFunctions.js"//js file to save add and update function
                  , "~/Scripts/jquery.CommonPopSearch.js"//js file for commonpop search
                ));

            //--- Add custom css file which are require globally in this bundle
            bundles.Add(new StyleBundle("~/Content/perfectERP/css").Include(
                  "~/Content/site.css"
                  ));
            //---  23 December 2021 | Sonin : Add custom bundle  [  E N D  ] 

        }
    }
}
