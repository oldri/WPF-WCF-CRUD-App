using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS2_991666875_S.Models
{
    //This code is the Model Object for the Trackers
    public partial class Tracker
    {
        //Props
        public int TrackerId { get; set; }
        public string TrackerName { get; set; }
        public float TrackerPrice { get; set; }
        public short UnitInStock { get; set; }

        //Constructor
        public Tracker()
        {

        }
    }
}
