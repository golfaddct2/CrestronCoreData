using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

namespace htdCoreData
{
    public class MediaRoom
    {
        public string RoomName { get; set; }
        public string Icon { get; set; }
        public uint HasVideo { get; set; }
        public uint Id { get; set; }
        public string CurrentSource { get; set; }
        public uint VideoRoute { get; set; }
        public uint AudioRoute { get; set; }

        public MediaRoom()
        { 
        
        }
        public MediaRoom(string roomName, string roomIcon, uint roomId)
        {
            this.RoomName = roomName;
            this.Icon = roomIcon;
            this.Id = roomId;
         
        }

        public void SourceChoice(int audio, int video)
        {
            AudioRoute = (uint)audio;
            VideoRoute = (uint)video;
        
        }

        /* Needs logic here that takes what the interface
          has selected as the source, takes that info from the source class instance,
          then passes it to the below instance
         * */
     
    }
}