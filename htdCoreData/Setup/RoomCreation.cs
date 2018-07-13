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
    class RoomCreation
    {
       
        String JSONString = File.ReadToEnd("\\NVRAM\\rooms.json",Encoding.UTF8);

        private class Temp
        {
            internal int Key { get; set; }
            public MediaRoom NewRooms { get; set; }

        }

        public Dictionary<int, MediaRoom> MediaRooms { get; set; }

        public RoomCreation()
        {
            List<Temp> output = JsonConvert.DeserializeObject<List<Temp>>(JSONString);
            MediaRooms = output.ToDictionary(x => x.Key, y => y.NewRooms);

            CrestronConsole.Print("Room JSON Data is Complete.  Total Room Count: {0}",
                MediaRooms.Count);


        }
        
        
        
        
        
        
        /*public Dictionary<int, MediaRoom> Rooms { get; set; }

        public RoomCreation()
        {
            Rooms = new Dictionary<int, MediaRoom>();
            Rooms.Add(101, new MediaRoom
            {
                Icon = "Kitchen",
                RoomName = "Kitchen",
                Id = 1

            });

            Rooms.Add(102, new MediaRoom
            {
                Icon = "Living",
                RoomName = "Living Room",
                Id = 2
            });

            Rooms.Add(103, new MediaRoom
            {
                Icon = "Bedroom",
                RoomName = "MBed",
                Id = 3
            });

        }
         */
         
         
    }
}