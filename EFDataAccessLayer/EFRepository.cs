using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EFDataAccessLayer
{
    class EmployeeContext : DbContext
    {
        public EmployeeContext() : base(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=tempdb;Integrated Security=True; MultipleActiveResultSets=True") { }

        public DbSet<Employee> Employees { set; get; }
    }
    class CityContext : DbContext
    {
        public CityContext() : base(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=tempdb;Integrated Security=True; MultipleActiveResultSets=True") { }

        public DbSet<City> City { set; get; }
    }

    public class EntityRepository <T> : IRepository <T>  where T : class , new()
    {
        EmployeeContext employeeDB = new EmployeeContext();
        CityContext cityDB = new CityContext();

        public IEnumerable<T> GetAll()
        {
            if (typeof(T).FullName == "Model.Employee")
            return employeeDB.Set<T>();
            else return cityDB.Set<T>();
        }

        public void Create(T obj)
        {
            if (obj is Employee)
            {               
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@Name", (obj as Employee).Name);
                parameters[1] = new SqlParameter("@Age", (obj as Employee).Age);
                parameters[2] = new SqlParameter("@Salary", (obj as Employee).Salary);
                parameters[3] = new SqlParameter("@City_ID", (obj as Employee).City.ID);
                cityDB.Database.ExecuteSqlCommand(@"INSERT INTO Employees (Name, Age, Salary, City_ID) VALUES(@Name, @Age, @Salary, @City_ID);
                SELECT * FROM Employees WHERE ID = CAST(SCOPE_IDENTITY() as int);", parameters);                   
            }
            else if (typeof(T).FullName != "Model.Employee")
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@Name", (obj as City).Name);
                cityDB.Database.ExecuteSqlCommand(@"INSERT INTO Cities (Name) VALUES(@Name); SELECT * FROM Cities WHERE ID = CAST(SCOPE_IDENTITY() as int);", parameters);
            }
        }
        public void Save(T obj)
        {
            if (typeof(T).FullName == "Model.Employee")
            employeeDB.Database.SqlQuery<T>("UPDATE Employees SET Name = @Name, Age = @Age WHERE ID = @ID");           
            else cityDB.Database.SqlQuery<T>("UPDATE Cities SET Name = @Name WHERE ID = @ID"); 
        }

        public void Delete(int id)
        {
            if (typeof(T).FullName == "Model.Employee")
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@id", id);

                var sqlQuery = "DELETE FROM Employees WHERE ID = @id";
                employeeDB.Database.ExecuteSqlCommandAsync(sqlQuery, parameters);
            }
            else
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@id", id);

                var sqlQuery = "DELETE FROM Employees WHERE City_ID = @id";
                employeeDB.Database.ExecuteSqlCommandAsync(sqlQuery, parameters);

                var sqlQuery1 = "DELETE FROM Cities WHERE ID = @id";                
                cityDB.Database.ExecuteSqlCommandAsync(sqlQuery1, parameters);
            }           
        }

        public void GetById(int id)
        {
            employeeDB.Set<T>().Find();
        }
        public List<int> GetCityId()
        {            
            var q = employeeDB.Database.SqlQuery<int>("SELECT City_ID FROM Employees ORDER BY ID").ToList();
            return q;
        }
    }    
}
