using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

namespace htdCoreData
{
    public class Source
    {
        public int VideoInput { get; set; }
        public int AudioInput{ get; set; }
        public string SourceName{ get; set; }
        public string Icon{ get; set; }
        public string SubpageType { get; set; }
        public string VideoDetected{ get; set; }
        public string NoVideoDetected { get; set; }

        

        
        public Source()
        { 
        }
        
        public Source(int audioInput, int videoInput, string sourceName, string icon, string subpageType, string videoDetected, string noVideoDetected)
            : this()
        {
            this.AudioInput = audioInput;
            this.VideoInput = videoInput;
            this.SourceName = sourceName;
            this.Icon = icon;
            this.SubpageType = subpageType;
            this.VideoDetected = videoDetected;
            this.NoVideoDetected = noVideoDetected;

        }
       
      



       
      
        
                
    }

   
}