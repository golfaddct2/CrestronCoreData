using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

namespace htdCoreData
{
    public class Schedule
    {

        public delegate void ReturnValue(ushort trigger);
        public ReturnValue ValueIsGood { get; set; }

        // pass in a timed event
        public void EventReady(string eventName)
        { 
            // based on event name due a check if its the correct day of week
            if (eventName == GetDay())
                     ValueIsGood(1);
        }
        
        
        // private method to determine day of week
        private string GetDay()
        {
            DayOfWeek dow = DateTime.Now.DayOfWeek;
            //CrestronConsole.PrintLine("Day of Week is {0}", dow);
            string dayName = "";
            switch (dow)
            {
                case DayOfWeek.Friday:
                    dayName = "Friday";
                    break;
                case DayOfWeek.Monday:
                    dayName = "Monday";
                    break;
                case DayOfWeek.Saturday:
                    dayName = "Saturday";
                    break;
                case DayOfWeek.Sunday:
                    dayName = "Sunday";
                    break;
                case DayOfWeek.Thursday:
                    dayName = "Thursday";
                    break;
                case DayOfWeek.Tuesday:
                    dayName = "Tuesday";
                    break;
                case DayOfWeek.Wednesday:
                    dayName = "Wednesday";
                    break;
             }
            return dayName;
        
        }


    }
}