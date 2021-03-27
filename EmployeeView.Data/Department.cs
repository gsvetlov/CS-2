using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeViewer.Data
{
    public class Department
    {
        public string Name { get; set; }
        public Employee Director { get; set; }
       
    }
}
