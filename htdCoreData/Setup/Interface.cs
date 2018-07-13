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
    
    public class Interface
    {

        public uint debug = 0;
        public uint count;
        public string MenuItemName;
        public string MenuItemIcon;
        public ushort MenuItemBlank;
        internal string pathName;

        // Delegate Needed for sending interlock data back to s+
        public delegate void MenuFunctions(int[]press);
        public MenuFunctions MenuButtons { get; set; }
               
        Menu menuControl = new Menu();


        /// <summary>
        /// Create Data and Icons for Menu
        /// </summary>
        public void GetCount(string path)
        {
            //String JSONString = File.ReadToEnd("\\NVRAM\\interfacedata.json", Encoding.UTF8);
            pathName = path;
            String JSONString = File.ReadToEnd(path, Encoding.UTF8);
            if (JSONString.Length > 0)
            {
                try
                {
                    JObject jObject = JObject.Parse(JSONString);
                    count = (uint)jObject.Count;
                    menuControl.CreateInterlock((int)count); // populate the array
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
        public void GetMenuItem(int number)
        {

            String JSONString = File.ReadToEnd(pathName, Encoding.UTF8);
            string nestedName = "Name";
            string nestedIcon = "Icon";
            string nestedEnableStatus = "Enable";
            string key = "Item" + number.ToString();
            string frontTagWhite = "<Font color=\"#ffffff\">";
            string frontTagRed = "<Font color=\"#ff0000\">";
            string endTag = "</Font>";
            if (JSONString.Length > 0)
            {
                try
                {
                    JObject jObject = JObject.Parse(JSONString);
                    if (jObject[key] != null)
                    {
                        int TempStatus = (int)jObject[key][nestedEnableStatus];
                        if (TempStatus > 0)
                        {
                            if ((string)jObject[key][nestedName] != "Power Off")
                            {
                                MenuItemName = frontTagWhite + (string)jObject[key][nestedName] + endTag;
                            }
                            else
                            {
                                MenuItemName = frontTagRed + (string)jObject[key][nestedName] + endTag;
                            }
                            MenuItemIcon = (string)jObject[key][nestedIcon];
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

        public void GetMenuItemSized(int number)
        {

            String JSONString = File.ReadToEnd(pathName, Encoding.UTF8);
            string nestedName = "Name";
            string nestedIcon = "Icon";
            string nestedEnableStatus = "Enable";
            string key = "Item" + number.ToString();
            string frontTagWhite = "<Font color=\"#ffffff\" size=\"22\">";
            string frontTagRed = "<Font color=\"#ff0000\" size=\"22\">";
            string endTag = "</Font>";
            if (JSONString.Length > 0)
            {
                try
                {
                    JObject jObject = JObject.Parse(JSONString);
                    if (jObject[key] != null)
                    {
                        int TempStatus = (int)jObject[key][nestedEnableStatus];
                        if (TempStatus > 0)
                        {
                            if ((string)jObject[key][nestedName] != "Power Off")
                            {
                                MenuItemName = frontTagWhite + (string)jObject[key][nestedName] + endTag;
                            }
                            else
                            {
                                MenuItemName = frontTagRed + (string)jObject[key][nestedName] + endTag;
                            }
                            MenuItemIcon = (string)jObject[key][nestedIcon];
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


        /// <summary>
        /// Interacting with the menu
        /// 
        /// </summary>
        /// <param name="itemPressed"></param>
        public void MenuItemPress(int itemPressed)
        {
            // ** add an if to check what type of control that item pressed is and perform method accordingly

            // this will be for interlocked items
            Array.Clear(menuControl.Interlock, 0, menuControl.Interlock.Length); // set all values in array to 0
            menuControl.Interlock[itemPressed] = 1;
            MenuButtons(menuControl.Interlock); // send the array values back to s+
        
        }
       
        // ** Create dictionary based off menu items
        // ** Add property that says what that menu item will control and update its properties accordingly
      
    }

      
   
    
    
}