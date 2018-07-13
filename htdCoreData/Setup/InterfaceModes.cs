using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Crestron.SimplSharp;
using Crestron.SimplSharp.CrestronIO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using htdCoreData.Setup;

namespace htdCoreData
{
    
    public class InterfaceModes
    {

        public uint debug = 0;
        public string MenuItemName { get; set; }
        public uint count { get; set; }
        public string MenuItemIcon { get; set; }
        public ushort MenuItemBlank { get; set; }


        // Delegate Needed for sending interlock data back to s+
        public delegate void MenuFunctions(int[]press);
        public MenuFunctions MenuButtons { get; set; }
               
        Menu menuInterlockModes = new Menu();


        /// <summary>
        /// Create Data and Icons for Menu
        /// </summary>
        public void GetCount(int mode)
        {
            String JSONString = File.ReadToEnd("\\NVRAM\\interfacedata.json", Encoding.UTF8);
            string nestedMode = GetMode(mode);
            if (JSONString.Length > 0)
            {
                try
                {
                    JObject jObject = JObject.Parse(JSONString);
                    count = (uint)jObject[nestedMode].Count();
                    menuInterlockModes.CreateInterlock((int)count); // populate the array
                        if (debug > 0)
                        {
                            CrestronConsole.Print("\n Amount of JSON Tokens is {0}",
                                count);
                        }

                }
                catch (Exception e)
                {
                    ErrorLog.Error("\n Interface GetCount Exception: " + e.Message);

                }
            }
        
        
        }
        public void GetMenuItem(int number, int mode)
        {

            String JSONString = File.ReadToEnd("\\NVRAM\\interfacedata.json", Encoding.UTF8);
            
            string nestedName = "Name";
            string nestedIcon = "Icon";
            string nestedEnableStatus = "Enable";
            string key = "Item" + number.ToString();
            string frontTagWhite = "<Font color=\"#ffffff\">";
            string frontTagRed = "<Font color=\"#ff0000\">";
            string endTag = "</Font>";
            string nestedMode = GetMode(mode);
            
            if (JSONString.Length > 0)
            {
                try
                {
                    JObject jObject = JObject.Parse(JSONString);
                    if (jObject[nestedMode][key] != null)
                    {
                        int TempStatus = (int)jObject[nestedMode][key][nestedEnableStatus];
                        if (TempStatus > 0)
                        {
                            if ((string)jObject[nestedMode][key][nestedName] != "Power Off")
                            {
                                MenuItemName = frontTagWhite + (string)jObject[nestedMode][key][nestedName] + endTag;
                            }
                            else
                            {
                                MenuItemName = frontTagRed + (string)jObject[nestedMode][key][nestedName] + endTag;
                            }
                            MenuItemIcon = (string)jObject[nestedMode][key][nestedIcon];
                        }
                        else
                        {
                            MenuItemName = "";
                            MenuItemIcon = "Blank";
                        }
                        MenuItemBlank = (ushort)TempStatus;
                                             
                        if (debug > 0)
                        {
                            
                            CrestronConsole.Print("\n Requested Menu Info Name:{0} Icon:{1} Visible: {2}",
                                MenuItemName, MenuItemIcon, MenuItemBlank);
                        }

                    }

                }
                catch (Exception e)
                {
                    ErrorLog.Error("\n Interface GetMenuItem Exception: " + e.Message);

                }
            }
            

        }

        public string RetrieveName()
        {
            return MenuItemName;
        }

        public string RetrieveIcon()
        {
            return MenuItemIcon;
        
        }

        public ushort RetrieveVisibility()
        {
            return MenuItemBlank;
        }

        public string GetMode(int mode)
        {
            string _Mode;
            if (mode > 1)
                _Mode = "Advanced";
            else
                _Mode = "Easy";
           
            return _Mode;
        }
      
       
      
    }

      
   
    
    
}