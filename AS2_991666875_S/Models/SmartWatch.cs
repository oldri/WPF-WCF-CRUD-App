using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS2_991666875_S.Models
{
    //This code is the Model Object for the Smart Watches
    public partial class SmartWatch
    {
        //Props
        public int SmartWatchId { get; set; }
        public string SmartWatchName { get; set; }
        public float SmartWatchPrice { get; set; }
        public short UnitInStock { get; set; }

        //Constructor
        public SmartWatch()
        {

        }
    }
}
