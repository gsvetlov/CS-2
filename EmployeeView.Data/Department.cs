﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeView.Data
{
    public class Department
    {
        public string Name { get; set; }
        public Employee Director { get; set; }
    }
}
