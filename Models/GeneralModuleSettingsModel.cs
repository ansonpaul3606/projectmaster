using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class GeneralModuleSettingsModel
    {


        //List<SettingsDetails> ModuleSettingsList { get; set; }

        public class GenModSettingsView
        {         // You can also add new instances later with default values


            public List<SettingsDetails> SettingsModuleDetails = new List<SettingsDetails>
            {
                //Here GSLabel is the id of corresponing control in UI.Name and id of a control is same in UI
                new SettingsDetails { GsModule = "CMMN", GsField = "CMMN001" ,GSLabel="CustomerCreatedOn"},

                new SettingsDetails { GsModule = "LEAD", GsField = "LEAD001",GSLabel="MRPInLead" },
                new SettingsDetails { GsModule = "LEAD", GsField = "LEAD002",GSLabel="LeadRequest" },

                new SettingsDetails { GsModule = "SERV", GsField = "SERV001",GSLabel="AccountpostingforBillingSer" },

                new SettingsDetails { GsModule = "INVT", GsField = "INVT001",GSLabel="PricefromStock" },
                new SettingsDetails { GsModule = "INVT", GsField = "INVT002",GSLabel="FinancialPlanSalesOrder" },
                new SettingsDetails { GsModule = "INVT", GsField = "INVT003",GSLabel="Multipleunitinsales" },
                new SettingsDetails { GsModule = "INVT", GsField = "INVT004",GSLabel="SalesWithoutStock" },
                new SettingsDetails { GsModule = "INVT", GsField = "INVT005",GSLabel="StandbyStockInSales" },
                new SettingsDetails { GsModule = "INVT", GsField = "INVT006",GSLabel="LoadDefaultEmployeeDepartmentinSales" },
                new SettingsDetails { GsModule = "INVT", GsField = "INVT007",GSLabel="BuyBackInSales" },
                new SettingsDetails { GsModule = "INVT", GsField = "INVT008",GSLabel="ChangeMOPInSales" },
                new SettingsDetails { GsModule = "INVT", GsField = "INVT009",GSLabel="EditDiscountInSales" },

                new SettingsDetails { GsModule = "PROJ", GsField = "PROJ001",GSLabel="ProductwiseLead" },
                new SettingsDetails { GsModule = "PROJ", GsField = "PROJ002",GSLabel="ProformaInvoice" },
                new SettingsDetails { GsModule = "PROJ", GsField = "PROJ003",GSLabel="AccountpostingBillingProj" },
                new SettingsDetails { GsModule = "PROJ", GsField = "PROJ004",GSLabel="AccountpostingExtrawork" },
                new SettingsDetails { GsModule = "PROJ", GsField = "PROJ005",GSLabel="FinancialPlanProj" },
                new SettingsDetails { GsModule = "PROJ", GsField = "PROJ006",GSLabel="ProductStandByMaterialAllocation" },
                new SettingsDetails { GsModule = "PROJ", GsField = "PROJ007",GSLabel="BuyBackInProjectCreation" },

                new SettingsDetails { GsModule = "ACNT", GsField = "ACNT001",GSLabel="BranchHeadPost" },


                new SettingsDetails { GsModule = "VHLE", GsField = "VHLE001",GSLabel="AccountpostingPaperRenewaVeh" },
                new SettingsDetails { GsModule = "VHLE", GsField = "VHLE002",GSLabel="AccountpostingPurchaseVeh" },
                new SettingsDetails { GsModule = "VHLE", GsField = "VHLE003",GSLabel="AccountpostingTransactionLogVeh" },

                new SettingsDetails { GsModule = "TOOL", GsField = "TOOL001",GSLabel="AccountpostingPaperRenewalTool" },
                new SettingsDetails { GsModule = "TOOL", GsField = "TOOL002",GSLabel="AccountpostingPurchaseTool" },
                new SettingsDetails { GsModule = "TOOL", GsField = "TOOL003",GSLabel="AccountpostingTransactionLogTool" },


                new SettingsDetails { GsModule = "MAPP", GsField = "MAPP001",GSLabel="LocationTracking" },
                new SettingsDetails { GsModule = "MAPP", GsField = "MAPP002",GSLabel="VoiceRecording" },
                new SettingsDetails { GsModule = "MAPP", GsField = "MAPP003",GSLabel="Attendance" },

                new SettingsDetails { GsModule = "PORT", GsField = "PORT001",GSLabel="ProductEnquiryCustPortal" },
                new SettingsDetails { GsModule = "PORT", GsField = "PORT002",GSLabel="ProductEnquiryDealarPortal" },

            };




        }
        public class SettingsDetails
        {
            public string GsModule { get; set; }
            public string GsField { get; set; }
            public string GSLabel { get; set; }

        }
        public class GeneralModuleSettingsDetails
        {
            public string SecValue { get; set; }
            public string SecField { get; set; }
        }
            public class GeneralSettingsModel
        {

            public string GsModule { get; set; }
            public string GsField { get; set; }
            public Boolean GsValue { get; set; }
        }
        public class GetGeneralModuleSettings
        {
            public string SecDate { get; set; }

            public string SecModule { get; set; }

            public string SecValue { get; set; }
            public string SecField { get; set; }

        }
        public class GetGeneralModuleSettingsDetails
        {
            public DateTime AsOnDate { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string SecModule { get; set; }

        }
        public class UpdateGeneralModuleSettings
        {
            public DateTime EffectDate { get; set; }

            public string SecModule { get; set; } 
            public List<GeneralModuleSettingsDetails> SettingsDetails { get; set; }
        }
        public class UpdateGeneralModuleSettingInput
        {
            public DateTime EffectDate { get; set; }

            public string SecModule { get; set; }
            public string EntrBy { get; set; }
            public string SettingsDetails { get; set; }
            public long FK_Company { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Machine { get; set; }
          
        }
        private string _updateProcedureName = "ProSoftwareSecurityUpdate";
        private string _selectProcedureName = "ProSoftwareSecuritySelect";
        public Output UpdateGeneralModuleSettingsDetails(UpdateGeneralModuleSettingInput input, string companyKey)
        {
            return Common.UpdateTableData<UpdateGeneralModuleSettingInput>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public APIGetRecordsDynamic<GetGeneralModuleSettings> GetGeneralModuleSettingsDetailsList(GetGeneralModuleSettingsDetails input, string companyKey)
        {
            return Common.GetDataViaProcedure<GetGeneralModuleSettings, GetGeneralModuleSettingsDetails>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }

    }
}