using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using EFDataAccessLayer;
using Model;


namespace DapperDataAccessLayer
{
    public class DapperRepository<T> : IRepository<T> where T : class, IDomainObject, new()
    {
        static string connectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=tempdb;Integrated Security=True; MultipleActiveResultSets=True";
        IDbConnection db = new SqlConnection(connectionString);

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM Employees WHERE ID = @id";
                db.Execute(sqlQuery, new { id });
            }
        }

        public List<int> GetCityId()
        {
            var q2 = db.Query<int>("SELECT City_ID FROM Employees ORDER BY ID").ToList();                                 
            return q2;
    }
        public IEnumerable<T> GetAll()
        {
            if (typeof(T).FullName == "Model.Employee")
                return db.Query<T>("SELECT * FROM Employees").ToList(); //INNER JOIN Cities ON Employees.City_ID = Cities.ID ").ToList(); 
            else return db.Query<T>("SELECT * FROM Cities").ToList();
        }

        public void GetById(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                db.Query<Employee>("SELECT * FROM Employees WHERE ID = @id", new { id }).FirstOrDefault();
            }
        }

        public void Save(T t)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (t is Employee)
                {
                    if ((t as Employee) != null)
                    {
                        var p = new DynamicParameters(new { City_ID = (t as Employee).City.ID, Name = (t as Employee).Name, Age = (t as Employee).Age, Salary = (t as Employee).Salary, ID = (t as Employee).ID });
                        db.Execute("UPDATE Employees SET Name = @Name, Age = @Age, Salary = @Salary, City_ID = @City_ID WHERE ID = @ID", p);
                    }
                }
                else if ((t as City)!= null)
                {
                    var p = new DynamicParameters(new { Name = (t as City).Name, ID = (t as City).ID });
                    db.Execute("UPDATE Cities SET Name = @Name WHERE ID = @ID", p);
                }
            }
        }
        public void Create(T t)
        {
            if (t is Employee)
            {
                if ((t as Employee) != null)
                {
                    var p = new DynamicParameters(new { City_ID = (t as Employee).City.ID, Name = (t as Employee).Name, Age = (t as Employee).Age, Salary = (t as Employee).Salary, ID = 1 });
                    using (IDbConnection db = new SqlConnection(connectionString))
                    {
                        db.Execute(@"INSERT INTO Employees (Name, Age, Salary, City_ID) VALUES(@Name, @Age, @Salary, @City_ID);
                    SELECT * FROM Employees WHERE ID = CAST(SCOPE_IDENTITY() as int);", p);
                    }
                }
            }
            else if ((t as City) != null)
            {
                var p = new DynamicParameters(new {Name = (t as City).Name, ID = 1 });
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    db.Execute(@"INSERT INTO Cities (Name) VALUES(@Name);
                    SELECT * FROM Cities WHERE ID = CAST(SCOPE_IDENTITY() as int);", p);
                }
            }
        }
    }

}
