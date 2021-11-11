using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    
    public class Employee : IDomainObject
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Salary { get; set; }

        public City City { get; set; }
        
        public override string ToString()
        {
            return ID + " " + Name + " " + Age + " " + Salary + " " + City.Name;
        }
    }

}
