using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;
using Crestron.SimplSharp.CrestronIO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
//using System.IO;

namespace htdCoreData
{
    public class SourceCreation
    {
        public uint debug = 0;
        
        
        public delegate void SendSourceToPlus(SimplSharpString sourceName, SimplSharpString sourceIcon, SimplSharpString subpageType);
        public SendSourceToPlus SourceData { get; set; }

          
        /// <summary>
        /// Method that takes an integer and uses that as the key to extract name / icon out of the source dictionary
        /// and then uses the callback to send it back into S+
        /// </summary>
        /// <param name="sourceNumber"></param>
       
        public void RetrieveDesiredSource(uint sourceNumber)
        {
            String JSONString = File.ReadToEnd("\\NVRAM\\sources.json", Encoding.UTF8);
            string nestedName = "SourceName";
            string nestedIcon = "Icon";
            string nestedSubpage = "SubpageType";
            string key = "Source" + sourceNumber.ToString();
            if (JSONString.Length > 0)
            {
                try
                {
                    JObject jObject = JObject.Parse(JSONString);
                    if (jObject[key] != null)
                    {
                        string RequestName = (string)jObject[key][nestedName];
                        string RequestIcon = (string)jObject[key][nestedIcon];
                        string RequestSubpage = (string)jObject[key][nestedSubpage];
                        SourceData(RequestName, RequestIcon, RequestSubpage);
                        if (debug > 0)
                        {
                            CrestronConsole.Print("\n Requested Source Info Name:{0}  Icon:{1} Subpage Type: {2}", 
                                RequestName, RequestIcon, RequestSubpage);
                        }

                    }
                
                }
                catch (Exception e)
                {
                    ErrorLog.Error("\n SourceCreation RetrieveDesiredSource Exception: " + e.Message);

                }
            }

        }
       
        public string VideoDetected(uint sourceNumber)
        {
            String JSONString = File.ReadToEnd("\\NVRAM\\sources.json", Encoding.UTF8);
            string nestedPhrase = "VideoDetected";
            string key = "Source" + sourceNumber.ToString();
            var phraseToReturn = "";
            if (JSONString.Length > 0)
            {
                try
                {
                    JObject jObject = JObject.Parse(JSONString);
                    if (jObject[key] != null)
                    {
                        phraseToReturn = (string)jObject[key][nestedPhrase];
                       
                        if (debug > 0)
                        {
                            CrestronConsole.Print("\n Requested Source Video Detected:{0}", phraseToReturn);
                        }
                    }

                }
                catch (Exception e)
                {
                    ErrorLog.Error("\n SourceCreation VideoDetected Exception: " + e.Message);

                }
            }
            return phraseToReturn;

        }
        public string NoVideoDetected(uint sourceNumber)
        {
            String JSONString = File.ReadToEnd("\\NVRAM\\sources.json", Encoding.UTF8);
            string nestedPhrase = "NoVideoDetected";
            string key = "Source" + sourceNumber.ToString();
            var phraseToReturn = "";
            if (JSONString.Length > 0)
            {
                try
                {
                    JObject jObject = JObject.Parse(JSONString);
                    if (jObject[key] != null)
                    {
                        phraseToReturn = (string)jObject[key][nestedPhrase];

                        if (debug > 0)
                        {
                            CrestronConsole.Print("\n Requested Source No Video Detected:{0}", phraseToReturn);
                        }
                    }

                }
                catch (Exception e)
                {
                    ErrorLog.Error("\n SourceCreation NoVideoDetected Exception: " + e.Message);

                }
            }
            return phraseToReturn;

        }
        // *** Old way I was doing the JSon where I would send it into a dictionary
        
        /*
       public Dictionary<int, Source> Sources { get; set; }

       private class Temp
       {
           public int Key { get; set; }
           public Source NewSources { get; set; }

       }
             
       /// <summary>
       /// 
       /// Initializing the source retrieved info from json file and creates a dictionary for all sources
       /// </summary>
      public void InitializeSources()
       {

           try
           {
              String JSONString = File.ReadToEnd("\\NVRAM\\sources.json", Encoding.UTF8);
              List<Temp> output = JsonConvert.DeserializeObject<List<Temp>>(JSONString);
              Sources = output.ToDictionary(x => x.Key, y => y.NewSources);
               if (debug > 0)
               {
                   CrestronConsole.Print("\n Source JSON Data is Complete.  Total Source Count: {0}",
               Sources.Count);
               }
           }
           catch (Exception e)
           {

               ErrorLog.Error("\n SourceCreation InitializeSources Exception: " + e.Message);
           }
            
        }
        * */

        

        
    }
    
    
}