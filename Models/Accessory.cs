using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS2_991666875_S.Models
{
    //This code is the Model Object for the Accessories
    public partial class Accessory
    {
        //Props
        public int AccessoryId { get; set; }
        public string AccessoryName { get; set; }
        public float AccessoryPrice { get; set; }
        public short UnitInStock { get; set; }

        //Constructor
        public Accessory()
        {

        }
    }
}
