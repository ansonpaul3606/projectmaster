using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PerfectWebERPAPI.Interface;

namespace PerfectWebERPAPI.Business
{
    class BlMobileNotificationFormat
    {
        public static SendNotification ConvertToSendNotification(DataTable dt)
        {
            SendNotification log = new SendNotification();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static FireBaseInfoList ConvertToFireBaseInfo(DataSet ds)
        {
            FireBaseInfoList log = new FireBaseInfoList();

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count>0)
                {
                    FireBaseInfoList generalSettings = ConvertToFireBaseInfo_AuthorizationKey(ds.Tables[0]);
                    log.AuthorizationKey = generalSettings.AuthorizationKey;
                    log.APIURL = generalSettings.APIURL;

                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        log.ResponseCode = "0";
                        log.ResponseMessage = "Success";
                        log.FireBaseInfo = ConvertToFireBaseInfoList(ds.Tables[1]);
                    }
                    else
                    {
                        log.ResponseCode = "-2";
                        log.ResponseMessage = "No Receiver Found";
                    }
                }
                else
                {
                    log.ResponseCode = "-2";
                    log.ResponseMessage = "Invalid Authentication Key";
                }
                
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<FireBaseInfo> ConvertToFireBaseInfoList(DataTable dt)
        {
            List<FireBaseInfo> lst = new List<FireBaseInfo>();
            foreach (DataRow dr in dt.Rows)
            {
                FireBaseInfo obj = new FireBaseInfo();
                obj.ID_PushNotificationData = Convert.ToInt64(dr["ID_PushNotificationData"]);
                obj.PnUserToken = Convert.ToString(dr["PnUserToken"]);
                obj.PnTitle = Convert.ToString(dr["PnTitle"]);
                obj.PnBody = Convert.ToString(dr["PnBody"]);
                obj.PnImage = Convert.ToString(dr["PnImage"]);
                obj.PnURL = Convert.ToString(dr["PnURL"]);
                obj.IsSend = Convert.ToBoolean(dr["IsSend"]);
                obj.PnReferenceNo = Convert.ToString(dr["PnReferenceNo"]);

                lst.Add(obj);
            }
            return lst;

        }
        public static FireBaseInfoList ConvertToFireBaseInfo_AuthorizationKey(DataTable dt)
        {
            FireBaseInfoList lst = new FireBaseInfoList();
            foreach (DataRow dr in dt.Rows)
            {

                lst.AuthorizationKey = Convert.ToString(dr["AuthorizationKey"]);
                lst.APIURL= Convert.ToString(dr["APIURL"]);

            }
            return lst;

        }
        public static SaveFireBaseSetings ConvertToSaveFireBaseSetings(DataTable dt)
        {
            SaveFireBaseSetings output=new SaveFireBaseSetings();
            FirebaseSettingsID lst = new FirebaseSettingsID();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {

                    lst.FK_FireBaseSettings = Convert.ToString(dr["FK_PushNotification"]);


                }
                output.FirebaseSettingsID = lst;
                output.ResponseMessage = "Registered Successfully";
                output.ResponseCode = "0";
            }
            else
            {
                output.ResponseMessage = "Registration Failed";
                output.ResponseCode = "-2";
            }

         

            return output;

        }
        public static CommonAPIR ConvertToResponse(DataSet ds)
        {
            CommonAPIR log = new CommonAPIR();

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {

                        log.ResponseCode = Convert.ToString(dr["Status"]);
                        log.ResponseMessage = Convert.ToString(dr["Message"]);

                    }
                }
                else
                {
                    log.ResponseCode = "-2";
                    log.ResponseMessage = "update failed";
                }

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
    }
}
