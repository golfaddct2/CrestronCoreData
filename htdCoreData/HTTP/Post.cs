using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;
using Crestron.SimplSharp.Net.Http;

namespace htdCoreData.HTTP
{
    public class Post
    {
            
            private HttpClient httpClient;
            public int debug;
            public delegate void ResponseHandler(SimplSharpString data);
            public ResponseHandler DataReceived { set; get; }
            
            public void Initialize()
            {
                httpClient = new HttpClient();
                
            }

            //Dispatch requests
            public void postRms(string serverUrl, string requestUrl, string body)
            {
                HttpClientRequest request = new HttpClientRequest();

                request.KeepAlive = false;
                request.Header.ContentType = "text/plain";
                request.RequestType = RequestType.Post;
                request.ContentSource = ContentSource.ContentString;
                request.ContentString = body;
                String requestString = serverUrl + requestUrl;
                request.Url.Parse(requestString);
                try
                {
                    //
                    //CrestronConsole.PrintLine("\nhttp trying get request: " + requestString);
                    
                    httpClient.DispatchAsync(request, OnHTTPClientResponseCallback);

                }
                catch (Exception exception)
                {
                    CrestronConsole.PrintLine("\nhttps exception with requestString: {0} and exception: {1}", requestString, exception.Message);
                }
            }
            
            // *** Add a method for source logic that takes info from s+ then uses the rmssource method to get the info to pass along
            
            
            // Callback
            public void OnHTTPClientResponseCallback(HttpClientResponse userobj, HTTP_CALLBACK_ERROR error)
            {
                try
                {
                    if (error != HTTP_CALLBACK_ERROR.COMPLETED)
                    {
                        ErrorLog.Error("HttpGetRequest OnHTTPClientResponseCallback error: " + error.ToString());
                    }
                    else if (userobj.ContentString.Length > 0)
                    {
                        DataReceived(userobj.ContentString);
                    }
                }
                catch (Exception e)
                {
                    ErrorLog.Error("Exception HttpGetRequest OnHTTPClientResponseCallback: " + e.Message);
                }
            }
        }
    }
