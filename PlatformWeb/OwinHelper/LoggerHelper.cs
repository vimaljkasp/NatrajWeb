using System;
using System.Configuration;
using System.IO;
using System.Net.Http;
using context = System.Web.HttpContext;
namespace PlatformWeb
{
    /// <summary>  
    /// Summary description for ExceptionLogging  
    /// </summary>  
    public static class LoggerHelper
    {

        private static String ErrorlineNo, Errormsg, extype, exurl, requestMethod, ErrorLocation, requestUri,requestBody;

        private static DateTime requestTimestamp;

        public static void WriteErrorToFile(Exception ex, HttpRequestMessage requestMessage )
        {
            var line = Environment.NewLine + Environment.NewLine;

            ErrorlineNo = ex.StackTrace.Substring(ex.StackTrace.Length - 7, 7);
            Errormsg = ex.GetType().Name.ToString();
            extype = ex.GetType().ToString();
            
            ErrorLocation = ex.Message.ToString();
            requestMethod = requestMessage.Method.Method;
            requestTimestamp = DateTime.Now;
            requestUri = requestMessage.RequestUri.ToString();
            requestBody= requestMessage.Content.ReadAsStringAsync().Result;
            try
            {
                string filepath = Path.GetFullPath(ConfigurationManager.AppSettings["LoggerPath"]) ;  //Text File Path

                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);

                }
                filepath = filepath + DateTime.Today.ToString("dd-MM-yy") + ".txt";   //Text File Name
                if (!File.Exists(filepath))
                { 
                    File.Create(filepath).Dispose();

                }
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    string error = "Log Written Date:" + " " + DateTime.Now.ToString() + line
                        + "Error Line No :" + " " + ErrorlineNo + line 
                        + "Error Message:" + " " + Errormsg + line
                        + "Exception Type:" + " " + extype + line
                        + "Error Location :" + " " + ErrorLocation + line 
                        + " Error Page Url:" + " " + requestUri + line 
                        + " Method Type:"+ requestMethod + line 
                        + "User Host IP:" + " "  + line;
                    sw.WriteLine("-----------Exception Details on " + " " + DateTime.Now.ToString() + "-----------------");
                    sw.WriteLine("-------------------------------------------------------------------------------------");
                    sw.WriteLine(line);
                    sw.WriteLine(error);
                    sw.WriteLine("--------------------------------*End*------------------------------------------");
                    sw.WriteLine(line);
                    sw.Flush();
                    sw.Close();

                }

            }
            catch (Exception e)
            {
                e.ToString();

            }
        }

    }
}