using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;

namespace FacebookApiFanPageToken
{

    class Program
    {
        static string pageID = "pageID";
        static string appID = "appID";
        static string appKey = "appKey";
        static string longLiveToken = "longLiveToken";

        static string GetFanPageToken()
        {
            string requestString = "https://graph.facebook.com/{0:s}?fields=access_token&access_token={1:s}";
            requestString = String.Format(requestString, pageID, longLiveToken);

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(requestString);
            httpWebRequest.Method = "GET";
            httpWebRequest.Timeout = 3000;

            string responseString = string.Empty;

            using (HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse())
            {
                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    responseString = sr.ReadToEnd();
                }
            }

            JToken responseJson = JValue.Parse(responseString);
            string fanPageToken = string.Empty;

            try
            {
                fanPageToken = responseJson.Value<string>("access_token");
            }
            catch
            {
                fanPageToken = "";
            }

            return fanPageToken;
        }


        static void Main(string[] args)
        {
            Console.WriteLine(GetFanPageToken());

            Console.ReadKey();
        }
    }
}
