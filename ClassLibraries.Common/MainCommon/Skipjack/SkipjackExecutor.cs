using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Web.UI;
using System.Globalization;
using System.Threading;
using System.Web;
using MainCommon.Skipjack;

namespace MainCommon
{
    public static class SkipjackExecutor
    {
        private static Dictionary<string, string> _errorCodes = new Dictionary<string, string>();


        static SkipjackExecutor()
        {
            _errorCodes.Add("1", null);
            _errorCodes.Add("-35", "Invalid credit card number");
            _errorCodes.Add("-37", "Error failed communication");
            _errorCodes.Add("-39", "Error length serial number");
            _errorCodes.Add("-51", "Invalid Billing Zip Code");
            _errorCodes.Add("-52", "Invalid Shipto zip code");
            _errorCodes.Add("-53", "Invalid expiration date");
            _errorCodes.Add("-54", "Error length account number date");
            _errorCodes.Add("-55", "Invalid Billing Street Address");
            _errorCodes.Add("-56", "Invalid Shipto Street Address");
            _errorCodes.Add("-57", "Error length transaction amount");
            _errorCodes.Add("-58", "Invalid Name");
            _errorCodes.Add("-59", "Error length location");
            _errorCodes.Add("-60", "Invalid Billing State");
            _errorCodes.Add("-61", "Invalid Shipto State");
            _errorCodes.Add("-62", "Error length order string");
            _errorCodes.Add("-64", "Invalid Phone Number");
            _errorCodes.Add("-65", "Empty name");
            _errorCodes.Add("-66", "Empty email");
            _errorCodes.Add("-67", "Empty street address");
            _errorCodes.Add("-68", "Empty city");
            _errorCodes.Add("-69", "Empty state");
            _errorCodes.Add("-79", "Error length customer name");
            _errorCodes.Add("-80", "Error length shipto customer name");
            _errorCodes.Add("-81", "Error length customer location");
            _errorCodes.Add("-82", "Error length customer state");
            _errorCodes.Add("-83", "Invalid Phone Number");
            _errorCodes.Add("-84", "Pos error duplicate ordernumber");
            _errorCodes.Add("-91", "Pos_error_CVV2");
            _errorCodes.Add("-92", "Pos_error_Error_Approval_Code");
            _errorCodes.Add("-93", "Pos_error_Blind_Credits_Not_Allowed");
            _errorCodes.Add("-94", "Pos_error_Blind_Credits_Failed");
            _errorCodes.Add("-95", "Pos_error_Voice_Authorizations_Not_Allowed");

        }


        public static Pair SJAuthorize(long order_number, string first_name, string last_name, string email, string postal_address, string city, string state, string postal_code
            , string country, string phone, long cart_number, DateTime cart_expiration_date, decimal amount
            , string serial_number, string developer_serial_number, string authorize_service_url, StringBuilder log)
        {
            //string serial_number =  "000407353233";
            //string developer_serial_number = "100017762725";
            //string authorize_service_url = @"https://developer.skipjackic.com/scripts/evolvcc.dll?AuthorizeAPI";

            if (log != null)
                log.Append("Authorize\r\n");

            StringBuilder sb = new StringBuilder();
            StringBuilder sbPublic = new StringBuilder();
            sb.Append("serialnumber=" + serial_number);
            sb.Append("&developerserialnumber=" + developer_serial_number);

            sbPublic.Append("&sjname=" + HttpUtility.UrlEncode(first_name + " " + last_name));
            sbPublic.Append("&email=" + HttpUtility.UrlEncode(email));
            sbPublic.Append("&streetaddress=" + HttpUtility.UrlEncode(postal_address));
            sbPublic.Append("&city=" + HttpUtility.UrlEncode(city));
            sbPublic.Append("&state=" + HttpUtility.UrlEncode(string.IsNullOrEmpty(state) ? "NA" : state));
            sbPublic.Append("&zipcode=" + HttpUtility.UrlEncode(postal_code));
            sbPublic.Append("&country=" + HttpUtility.UrlEncode(country));
            sbPublic.Append("&shiptophone=" + HttpUtility.UrlEncode(string.IsNullOrEmpty(state) ? "212-555-1212" : phone));
            sbPublic.Append("&ordernumber=" + order_number + "AUTH");
            
            //HttpUtility.UrlEncode(cart_number.ToString().Substring(cart_number.ToString().Length - 3) + last_name)
            //sb.Append("&sjname=" + first_name + " " + last_name);
            //sb.Append("&email=" + email);
            //sb.Append("&streetaddress=" + postal_address);
            //sb.Append("&city=" + city);
            //sb.Append("&state=" + state);
            //sb.Append("&zipcode=" + postal_code);
            //sb.Append("&country=" + country);
            //sb.Append("&shiptophone=" + phone);
            //sb.Append("&ordernumber=" + cart_number.ToString().Substring(cart_number.ToString().Length - 3) + last_name);
            
            sb.Append("&accountnumber=" + cart_number);
            sbPublic.Append("&month=" + cart_expiration_date.Month);
            sbPublic.Append("&year=" + cart_expiration_date.Year);
            CultureInfo info = new CultureInfo("en-US");
            sbPublic.Append("&transactionamount=" + amount.ToString("F2", info));
            sbPublic.Append("&orderstring=" + "1~Authorization~" + amount.ToString("F2", info) + "~1~N~||");
            sb.Append(sbPublic);

            Pair rval = new Pair();
            try
            {
                if (log != null) log.Append("Authorize request, " + DateTime.Now.ToString("T") + "\r\n" + sbPublic.ToString() + "\r\n\r\n");
                string responseString = SendRequest(authorize_service_url, sb);
                if (log != null) log.Append("Authorize response\r\n" + responseString + "\r\n\r\n");

                Dictionary<SkipJackAuthorize, string> respDict = ParseSJAuthorizeResponse(responseString);

                string errorMessage = GetErrorMessage(respDict);
                string declineMessage = respDict[SkipJackAuthorize.DeclineMessage];
                if (errorMessage == null && declineMessage != null)
                {
                    errorMessage = declineMessage;
                }
                //SJ Errors processing
                //• Transaction was not further processed and the transaction data was not submitted to the Processor/Issuer as indicated by the szIsApproved = <empty>.
                //• szAuthorizationresponseCode = <empty> indicates that the card Issuer has not processed the transaction.
                //• Authcode = <empty> means that no Authorization was obtained. ??
                //• szIsApproved = 0, Issuing bank declined the transaction.
                //• szAuthorizationResponseCode = <empty> indicates that the card Issuer has not processed the transaction.
                //• Authcode = <empty> means that the Authorization was Declined. ??
                if (respDict[SkipJackAuthorize.IsApproved] == "EMPTY")
                {
                    throw new Exception(errorMessage);
                }
                else if (respDict[SkipJackAuthorize.AuthorizationResponseCode] == "EMPTY")
                {
                    throw new Exception(errorMessage);
                }
                else if (respDict[SkipJackAuthorize.ApprovalCode] == "EMPTY")
                {
                    throw new Exception(errorMessage);
                }
                if (respDict[SkipJackAuthorize.IsApproved] != "1" || string.IsNullOrEmpty(respDict[SkipJackAuthorize.TransactionID])) throw new Exception(errorMessage);

                rval.First = respDict[SkipJackAuthorize.TransactionID];
                rval.Second = declineMessage;

            }
            catch (Exception ex)
            {
                throw new SJUnhandledException(ex);
            }

            return rval;
        }

        private static string GetErrorMessage(Dictionary<SkipJackAuthorize, string> respDict)
        {
            string rval = respDict[SkipJackAuthorize.ReturnCode];

            if (_errorCodes.ContainsKey(rval))
                rval = _errorCodes[rval];

            return rval;
        }

        public static string Pay(string sj_tran_id, long shopping_order_id, long? authorize_order_id,
            decimal amount, string serial_number, string developer_serial_number, string change_status_url, string get_status_url, StringBuilder log = null)
        {
            string rval = null;
            try
            {
                if (log != null)
                    log.Append("Charge\r\n");

                var response = SJChangeStatus(shopping_order_id, sj_tran_id, SkipJackChangeStatusType.AuthorizeAdditional, amount, false, authorize_order_id,
                    serial_number, developer_serial_number, change_status_url, log);

                if (string.Compare(response[SkipJackChangeStatus.StatusResponseCode], "SUCCESSFUL", true) != 0)
                    throw new SJDeclineException(string.Format("{0}:{1}", response[SkipJackChangeStatus.StatusResponseCode], response[SkipJackChangeStatus.StatusResponseMessage]));
                rval = response[SkipJackChangeStatus.TransactionID];

                try
                {
                    Settle(shopping_order_id, serial_number, developer_serial_number, change_status_url, get_status_url, rval, log);
                }
                catch (SJTimeoutException ex)
                {
                    SJChangeStatus(shopping_order_id, rval, SkipJackChangeStatusType.Delete, 0, false, null,
                        serial_number, developer_serial_number, change_status_url);

                    throw ex;
                }
                if (log != null)
                    log.Append("Charge complete\r\n");
            }
            catch (Exception ex)
            {
                if (log != null)
                    log.Append("Charge error\r\n" + ex.Message);
                if (ex is SJTimeoutException || ex is SJDeclineException) throw;
                else throw new SJUnhandledException(ex);
            }

            return rval;
        }

        private static void Settle(long order_number, string serial_number, string developer_serial_number, string change_status_url, string get_status_url, string rval, StringBuilder log = null)
        {
            if (log != null)
                log.Append("Settle transaction\r\n------------------\r\n");
            try
            {
                Dictionary<SkipJackGetStatus, string> response2 = null;
                var i = 0;
                var maxTryCount = 60;
                for (; i < maxTryCount; i++)
                {
                    System.Threading.Thread.Sleep(1000);
                    response2 = SJGetStatus(order_number, serial_number, developer_serial_number, get_status_url, log);
                    var currentStatus = response2[SkipJackGetStatus.StatusCode].Substring(0, 1);
                    var pendingStatus = response2[SkipJackGetStatus.StatusCode].Substring(1, 1);
                    if (currentStatus != "1" && currentStatus != "0")
                        throw new Exception(string.Format("Illegal current transaction status {0}:{1}", response2[SkipJackGetStatus.StatusCode], response2[SkipJackGetStatus.StatusMessage]));
                    if (currentStatus == "3" || currentStatus == "8") return;
                    if (pendingStatus == "2" || pendingStatus == "5" || pendingStatus == "7") break;
                }
                if (i == maxTryCount) throw new SJTimeoutException(string.Format("{0}:{1}", response2[SkipJackGetStatus.StatusCode], response2[SkipJackGetStatus.StatusMessage]));

                try
                {
                    var response3 = SJChangeStatus(order_number, rval, SkipJackChangeStatusType.Settle, null, true, null,
                        serial_number, developer_serial_number, change_status_url, log);
                }
                catch (Exception ex)
                {
                    if (log != null)
                        log.Append("Settle transaction error, try to manual settle\r\n" + ex.Message + "\r\n\r\n");
                }
            }
            finally
            {
                if (log != null)
                    log.Append("------------------\r\n\r\n");
            }
        }

        public static Dictionary<SkipJackGetStatus, string> SJGetStatus(long order_number,
            string serial_number, string developer_serial_number, string get_status_url, StringBuilder log = null)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("szserialnumber=" + serial_number);
            sb.Append("&szdeveloperserialnumber=" + developer_serial_number);

            sb.Append("&szordernumber=" + order_number);
            //sb.Append("&szdate=" + date.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture));

            if (log != null) log.Append("GetStatus request, " + DateTime.Now.ToString("T") + "\r\n" + sb.ToString() + "\r\n\r\n");
            string responseString = SendRequest(get_status_url, sb);
            if (log != null) log.Append("GetStatus response\r\n" + responseString + "\r\n\r\n");

            return ParseSkipJackGetStatus(responseString, order_number.ToString());
        }


        private static Dictionary<SkipJackChangeStatus, string> SJChangeStatus(long order_id, string sj_tran_id, SkipJackChangeStatusType type, decimal? amount, bool force_settlement, long? authorize_order_id
            , string serial_number, string developer_serial_number, string change_status_url, StringBuilder log = null)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("szserialnumber=" + serial_number);
            sb.Append("&szdeveloperserialnumber=" + developer_serial_number);
            if (type == SkipJackChangeStatusType.AuthorizeAdditional)
            {
                sb.Append("&sznewordernumber=" + order_id);
                if (authorize_order_id != null)
                    sb.Append("&szordernumber=" + authorize_order_id + "AUTH");
                else
                    sb.Append("&sztransactionid=" + sj_tran_id);

            }

            else if (type == SkipJackChangeStatusType.Settle)
            {
                sb.Append("&szordernumber=" + order_id);
            }
            sb.Append("&szdesiredstatus=" + type.ToString());
            if (amount != null)
            {
                CultureInfo info = new CultureInfo("en-US");
                sb.Append("&szamount=" + amount.Value.ToString("F2", info));
            }


            //if(force_settlement)
            //    sb.Append("&szforcesettlement=1");
            sb.Append("&szforcesettlement=0");

            if (log != null) log.Append("Change status request" + DateTime.Now.ToString("T") + "\r\n" + sb.ToString() + "\r\n\r\n");
            var response = SendRequest(change_status_url, sb);
            if (log != null) log.Append("Change status response\r\n" + response + "\r\n\r\n");

            return ParseSkipJackChangeStatus(response);
        }

        private static string SendRequest(string url, StringBuilder post_data)
        {
            byte[] data = System.Text.Encoding.UTF8.GetBytes(post_data.ToString());

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            Stream newStream = request.GetRequestStream();
            newStream.Write(data, 0, data.Length);
            newStream.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StreamReader sr = new StreamReader(responseStream, System.Text.Encoding.UTF8);
            string responseString = sr.ReadToEnd();
            sr.Close();
            responseStream.Close();

            return responseString;
        }

        private static Dictionary<SkipJackAuthorize, string> ParseSJAuthorizeResponse(string response)
        {
            Dictionary<SkipJackAuthorize, string> rval = null;
            if (!string.IsNullOrEmpty(response))
            {
                rval = new Dictionary<SkipJackAuthorize, string>();

                string[] arrLines = response.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                string[] arrResponse = arrLines[1].Split(new string[] { "\",\"" }, StringSplitOptions.None);

                rval.Add(SkipJackAuthorize.ApprovalCode, arrResponse[(byte)SkipJackAuthorize.ApprovalCode].Replace("\"", ""));
                rval.Add(SkipJackAuthorize.SerialNumber, arrResponse[(byte)SkipJackAuthorize.SerialNumber]);
                rval.Add(SkipJackAuthorize.TransactionAmount, arrResponse[(byte)SkipJackAuthorize.TransactionAmount]);
                rval.Add(SkipJackAuthorize.DeclineMessage, arrResponse[(byte)SkipJackAuthorize.DeclineMessage]);
                rval.Add(SkipJackAuthorize.AVSResponseCode, arrResponse[(byte)SkipJackAuthorize.AVSResponseCode]);
                rval.Add(SkipJackAuthorize.AVSResponseMessage, arrResponse[(byte)SkipJackAuthorize.AVSResponseMessage]);
                rval.Add(SkipJackAuthorize.OrderNumber, arrResponse[(byte)SkipJackAuthorize.OrderNumber]);
                rval.Add(SkipJackAuthorize.AuthorizationResponseCode, arrResponse[(byte)SkipJackAuthorize.AuthorizationResponseCode]);
                rval.Add(SkipJackAuthorize.IsApproved, arrResponse[(byte)SkipJackAuthorize.IsApproved]);
                rval.Add(SkipJackAuthorize.CVV2ResponseCode, arrResponse[(byte)SkipJackAuthorize.CVV2ResponseCode]);
                rval.Add(SkipJackAuthorize.CVV2ResponseMessage, arrResponse[(byte)SkipJackAuthorize.CVV2ResponseMessage]);
                rval.Add(SkipJackAuthorize.ReturnCode, arrResponse[(byte)SkipJackAuthorize.ReturnCode]);
                rval.Add(SkipJackAuthorize.TransactionID, arrResponse[(byte)SkipJackAuthorize.TransactionID]);
                rval.Add(SkipJackAuthorize.CAVVResponseCode, arrResponse[(byte)SkipJackAuthorize.CAVVResponseCode]);
            }

            return rval;
        }

        private static Dictionary<SkipJackChangeStatus, string> ParseSkipJackChangeStatus(string response)
        {
            Dictionary<SkipJackChangeStatus, string> rval = null;
            if (!string.IsNullOrEmpty(response))
            {
                rval = new Dictionary<SkipJackChangeStatus, string>();

                var arrLines = response.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                string[] responseRecords = ParseStatus(response, arrLines);

                rval.Add(SkipJackChangeStatus.SerialNumber, responseRecords[(byte)SkipJackChangeStatus.SerialNumber].Replace("\"", ""));
                rval.Add(SkipJackChangeStatus.TransactionAmount, responseRecords[(byte)SkipJackChangeStatus.TransactionAmount]);
                rval.Add(SkipJackChangeStatus.DesiredStatus, responseRecords[(byte)SkipJackChangeStatus.DesiredStatus]);
                rval.Add(SkipJackChangeStatus.StatusResponseCode, responseRecords[(byte)SkipJackChangeStatus.StatusResponseCode]);
                rval.Add(SkipJackChangeStatus.StatusResponseMessage, responseRecords[(byte)SkipJackChangeStatus.StatusResponseMessage]);
                rval.Add(SkipJackChangeStatus.OrderNumber, responseRecords[(byte)SkipJackChangeStatus.OrderNumber]);
                rval.Add(SkipJackChangeStatus.TransactionID, responseRecords[(byte)SkipJackChangeStatus.TransactionID].Replace("\"", ""));
            }

            return rval;
        }

        private static Dictionary<SkipJackGetStatus, string> ParseSkipJackGetStatus(string response, string order_number)
        {
            Dictionary<SkipJackGetStatus, string> rval = null;
            if (!string.IsNullOrEmpty(response))
            {
                rval = new Dictionary<SkipJackGetStatus, string>();

                var arrLines = response.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                string[] responseRecords = ParseStatus(response, arrLines);
                if (responseRecords[(byte)SkipJackGetStatus.OrderNumber] != order_number) throw new Exception("Unknown skipjack error on: " + arrLines[1]);

                rval.Add(SkipJackGetStatus.SerialNumber, responseRecords[(byte)SkipJackGetStatus.SerialNumber].Replace("\"", ""));
                rval.Add(SkipJackGetStatus.TransactionAmount, responseRecords[(byte)SkipJackGetStatus.TransactionAmount]);
                rval.Add(SkipJackGetStatus.StatusCode, responseRecords[(byte)SkipJackGetStatus.StatusCode]);
                rval.Add(SkipJackGetStatus.StatusMessage, responseRecords[(byte)SkipJackGetStatus.StatusMessage]);
                rval.Add(SkipJackGetStatus.OrderNumber, responseRecords[(byte)SkipJackGetStatus.OrderNumber]);
                rval.Add(SkipJackGetStatus.TransactionDate, responseRecords[(byte)SkipJackGetStatus.TransactionDate]);
                rval.Add(SkipJackGetStatus.TransactionID, responseRecords[(byte)SkipJackGetStatus.TransactionID]);
            }

            return rval;
        }

        private static string[] ParseStatus(string response, string[] arrLines)
        {
            string[] responseRecords;

            arrLines = response.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            if (arrLines.Length < 2) throw new Exception("Need more result from skipjack");
            var statusRecords = arrLines[0].Split(new string[] { "\",\"" }, StringSplitOptions.None);
            responseRecords = arrLines.Last().Split(new string[] { "\",\"" }, StringSplitOptions.None);
            var szError = statusRecords[1];
            if (szError != "0") throw new Exception("Skipjack report error is: " + arrLines[0] + "\r\n" + arrLines[1]);

            return responseRecords;
        }
    }

    public enum SkipJackChangeStatusType
    {
        AuthorizeAdditional,
        Settle,
        Delete,
    }

    public enum SkipJackAuthorize : byte
    {
        ApprovalCode = 0,
        SerialNumber = 1,
        TransactionAmount = 2,
        DeclineMessage = 3,
        AVSResponseCode = 4,
        AVSResponseMessage = 5,
        OrderNumber = 6,
        AuthorizationResponseCode = 7,
        IsApproved = 8,
        CVV2ResponseCode = 9,
        CVV2ResponseMessage = 10,
        ReturnCode = 11,
        TransactionID = 12,
        CAVVResponseCode = 13
    }

    public enum SkipJackChangeStatus : byte
    {
        SerialNumber = 0,
        TransactionAmount = 1,
        DesiredStatus = 2,
        StatusResponseCode = 3,
        StatusResponseMessage = 4,
        OrderNumber = 5,
        TransactionID = 6
    }

    public enum SkipJackGetStatus : byte
    {
        SerialNumber = 0,
        TransactionAmount = 1,
        StatusCode = 2,
        StatusMessage = 3,
        OrderNumber = 4,
        TransactionDate = 5,
        TransactionID = 6,
        ApprovalCode = 7,
        BatchNumber = 8
    }
}
