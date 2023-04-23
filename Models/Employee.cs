using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS2_991666875_S.Models
{
    public partial class Employee
    {
        //Props
        public int EmployeeId { get; set; }
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
        public string EmployeeTitle { get; set; }
        public string EmployeeCity { get; set; } 
        //Constructor
        public Employee()
        {

        }
    }
}
