using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

namespace htdCoreData.Setup
{
    public class Menu
    {
        // create an array of menu items
        public int[]Interlock { get; set; }
        

        // use inputs and outputs for s+ to provide feedback on the panels
        public Menu()
        {

        }
        
        public Menu(int ArraySize)
        {
            Interlock = new int[ArraySize];
        }

        public void CreateInterlock(int ArraySize)
        {
            Interlock = new int[ArraySize];
        
        }
        // use json to notate if the menu item is a source or a toggle menu that is then cleared by other sources
        

        // 
    }
}