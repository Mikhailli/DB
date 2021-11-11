using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace EFDataAccessLayer
{
    public class DataContext: DbContext
    {
        public DbSet<Employee> Employee { get; set; }
        public DbSet<City> City { get; set; }
        public DataContext() : base(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=tempdb;Integrated Security=True; MultipleActiveResultSets=True") { }
        
    }
}
