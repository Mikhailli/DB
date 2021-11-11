using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccessLayer
{
    class EmployeeContext : DbContext
    {
        public EmployeeContext() : base("Data Source = localhost\\SQLEXPRESS; Initial Catalog = tempdb; Integrated Security = True") { }

        public DbSet<Employee> Employees { set; get; }
    }
    class DepartmentContext : DbContext
    {
        public DepartmentContext() : base("Data Source = localhost\\SQLEXPRESS; Initial Catalog = tempdb; Integrated Security = True") { }

        public DbSet<City> Cities { set; get; }
    }

    public class EntityRepository <T> : IRepository <T>  where T : class , new()
    {
        EmployeeContext db = new EmployeeContext();

        DepartmentContext db1 = new DepartmentContext();

        public IEnumerable<T> GetAll()
        {
            return db.Set<T>();
        }

        public void Create(T obj)
        {
            db.Set<T>().Add(obj);
        }
        public void Save(T obj)
        {
            db1.SaveChanges();
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            foreach(Employee emp in db.Employees)
            {
                if (emp.ID == id)
                {
                    db.Set<T>().Remove(emp as T);
                }
            }
            
        }

        public void GetById(int id)
        {
            db.Set<T>().Find();
        }
    }

    

}
