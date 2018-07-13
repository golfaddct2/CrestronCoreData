using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;
using Crestron.SimplSharp.CrestronIO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace htdCoreData
{
    public class Routing
    {
        public uint debug = 0;
        public Dictionary<int, Source> Sources { get; set; }



        public delegate void AnalogDataToPlus(ushort sourceVideo, ushort sourceAudio);
        public AnalogDataToPlus RoutingData { get; set; }

        public delegate void INeedTheName(SimplSharpString activeName);
        public INeedTheName RetrieveName { get; set; }

        public void RetrieveRouting(uint sourceNumber)
        {
            String JSONString = File.ReadToEnd("\\NVRAM\\Sources.json", Encoding.UTF8);
            string nestedVideo = "VideoInput";
            string nestedAudio = "AudioInput";
            string key = "Source" + sourceNumber.ToString();
            if (JSONString.Length > 0)
            {
                try
                {
                    JObject jObject = JObject.Parse(JSONString);
                    if (jObject[key] != null)
                    {
                        int RequestVideoInput = (int)jObject[key][nestedVideo];
                        int RequestAudioInput = (int)jObject[key][nestedAudio];
                        if (debug > 0)
                        {
                            CrestronConsole.Print("\n Source Video:{0} Audio:{1}", RequestVideoInput, RequestAudioInput);

                        }

                        RoutingData((ushort)RequestVideoInput, (ushort)RequestAudioInput);

                       
                    }

                }
                catch (Exception e)
                {
                    ErrorLog.Error("\n Routing RetrieveRouting Exception: " + e.Message);

                }
            }

        }
        
       
       public ushort SourceFb(ushort videoSourceStatus)
        {
           String SourceString = File.ReadToEnd("\\NVRAM\\Sources.json", Encoding.UTF8);
           ushort activeSource = 0;
           string nestedKey = "VideoInput";
           string nestedName = "SourceName";
           string activeName;
           int compareTo = videoSourceStatus;
           if (SourceString.Length > 0)
            {
                try
                {
                    JObject jObject = JObject.Parse(SourceString);
                    
                    for (int i = 1; i <= jObject.Count; i++)
                    {
                        string key = "Source" + i;
                        //CrestronConsole.Print("\n Trying {0} compared to Fb {1}", key, compareTo);
                        int fromJson = (int)jObject[key][nestedKey];
                        if (fromJson == compareTo)
                            {
                                activeSource = (ushort)i;
                                activeName = (string)jObject[key][nestedName];
                                RetrieveName(activeName);
                                //CrestronConsole.Print("\n SourceFb for Loop if is True now breaking.  Active Source is {0}",activeSource);
                                break;
                            }


                        //CrestronConsole.Print("\n SourceFb for Loop {0} Value: {1}", key, fromJson);
                        

                    } 
               

                }
                catch (Exception e)
                {
                    ErrorLog.Error("\n Routing SourceFb Exception: " + e.Message);

                }
      
            }
           return activeSource;
           
        }

       public void AudioSourceFb(ushort audioSourceStatus)
       {
           String SourceString = File.ReadToEnd("\\NVRAM\\Sources.json", Encoding.UTF8);
           
           string nestedKey = "AudioInput";
           string nestedName = "SourceName";
           string activeName;
           int compareTo = audioSourceStatus;
           if (SourceString.Length > 0)
           {
               try
               {
                   JObject jObject = JObject.Parse(SourceString);

                   for (int i = 1; i <= jObject.Count; i++)
                   {
                       string key = "Source" + i;
                       int fromJson = (int)jObject[key][nestedKey];
                       if (fromJson == compareTo)
                       {
                           activeName = (string)jObject[key][nestedName];
                           RetrieveName(activeName);
                           break;
                       }

                   }


               }
               catch (Exception e)
               {
                   ErrorLog.Error("\n Routing AudioSourceFb Exception: " + e.Message);

               }

           }

       }

    }
}