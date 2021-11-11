using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Model;


namespace DapperDataAccessLayer
{
    public class DapperRepository<T> : IRepository<T> where T : class, IDomainObject, new()
    {

        static string connectionString = "Data Source = localhost\\SQLEXPRESS; Initial Catalog = tempdb; Integrated Security = True";
        IDbConnection db = new SqlConnection(connectionString);

        public void Create(T t)
        {
            var sqlQuery = "INSERT INTO Employees (Name, Age, Salary) VALUES(@Name, @Age, @Salary); SELECT CAST(SCOPE_IDENTITY() as int)";
            int employeeId = db.Query<int>(sqlQuery, t).FirstOrDefault();
            t.ID = employeeId;

        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM Employees WHERE ID = @id";
                db.Execute(sqlQuery, new { id });
            }
        }

        public IEnumerable<T> GetAll()
        {
            return db.Query<T>("SELECT * FROM Employees").ToList(); 
        }

        public void GetById(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                db.Query<Employee>("SELECT * FROM Emplouees WHERE ID = @id", new { id }).FirstOrDefault();
            }
        }

        public void Save(T obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE Employees SET Name = @Name, Age = @Age WHERE ID = @ID";
                db.Execute(sqlQuery, obj);
            }
        }
    }

}
