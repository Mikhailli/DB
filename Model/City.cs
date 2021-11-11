﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class City : IDomainObject
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public ICollection<Employee> Employees { get; set; }
        
    }
}
