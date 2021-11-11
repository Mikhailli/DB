using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using EFDataAccessLayer;
using DapperDataAccessLayer;

namespace BLogic
{
    public class BL
    {
        public IRepository<Employee> EmployeeRepository { get; set; }
        public IRepository<City> CityRepository { get; set; }

        public BL(IRepository<Employee> employeeRepository, IRepository<City> cityRepository)
        {
            EmployeeRepository = employeeRepository;
            CityRepository = cityRepository;
        }

        public List<Employee> Employees = new List<Employee>();

        public List<City> Cities = new List<City> { new City { ID = 1, Name = "Красноярск" },
        new City {ID = 2, Name = "Москва" }, new City {ID = 3, Name = "Санкт-Петербург" } };

        public List<int> CityIDs = new List<int>();

        public List<int> EmpsIDs = new List<int>();

        public void GetAll()
        {           
            int i = 0;
            Employees.Clear();
            CityIDs = EmployeeRepository.GetCityId();
            Cities.Clear();
            foreach (City c in CityRepository.GetAll())
                Cities.Add(c);
            foreach (var emp in EmployeeRepository.GetAll())
            {                                                  
                 if (CityIDs.Count != 0)
                 {
                     foreach (int id in CityIDs)
                     {
                         if (id == EmployeeRepository.GetCityId().ElementAt(i))
                         {
                             foreach (City city in Cities)
                             {
                                 if (city.ID == id)
                                     emp.City = city;
                             }
                             Employees.Add(emp);
                         }                           
                     }
                     i++;
                 }
                 if (!EmpsIDs.Contains(emp.ID))
                 {
                     EmpsIDs.Add(emp.ID);
                 }                
            }                     
            Cities.Clear();
            foreach (var city in CityRepository.GetAll())
            {
                Cities.Add(city);
            }
        }
        public string GetById(int id)
        {
            string employer = "Работника с таким Id нет.";
            foreach (var emp in EmployeeRepository.GetAll())
            {
                if (emp.ID == id)
                    employer = emp.ToString();
            }
            return employer;
        }

        public void AddCity(string name)
        {
            Cities.Clear();
            City city = new City()
            {
                Name = name
            };
            CityRepository.Create(city);
            CityRepository.Save(city);
            foreach (var c in CityRepository.GetAll())
            {               
                Cities.Add(c);                
            }
        }
        public void AddEmployee(string name, int age, int salary, int cityID)
        {
            Employees.Clear();
            Employee employee = new Employee
            {
                Name = name,
                Age = age,
                Salary = salary,
                City = CityRepository.GetAll().ElementAt(cityID - 1)
            };
            employee.City.ID = cityID;
            EmployeeRepository.Create(employee);
            EmployeeRepository.Save(employee);            
            int j = 0;
            Employees.Clear();
            CityIDs.Clear();
            CityIDs = EmployeeRepository.GetCityId();            
            foreach (var emp in EmployeeRepository.GetAll())
            {                
                emp.City = Cities[EmployeeRepository.GetCityId().ElementAt(j) - 1]; ;
                j++;
                Employees.Add(emp);
                if (!EmpsIDs.Contains(emp.ID))
                {
                    EmpsIDs.Add(emp.ID);
                }                
            }            
        }
        
        public void DeleteEmployees(int id)
        {
            List<int> ClonEmpsIDs = new List<int>();
            foreach (int ID in EmpsIDs)
            {
                ClonEmpsIDs.Add(ID);
            }
            EmpsIDs.Clear();
            List<Employee> ClonEmployees = new List<Employee>();
            foreach (Employee emp in Employees)
            {
                ClonEmployees.Add(emp);
            }
            foreach (int ID in ClonEmpsIDs)
            {
                if (ID != id)
                    EmpsIDs.Add(ID);
            }
            Employees.Clear();
            foreach (Employee emp in ClonEmployees)
            {
                if (emp.ID != id)
                    Employees.Add(emp);
                else
                {
                    EmployeeRepository.Delete(id);
                    EmployeeRepository.Save(emp);
                }
            }
        }
        public string[] TransformEmployeesToString()
        {
            int i = -1;
            string[] str = new string[Employees.Count()];
            foreach (Employee emp in Employees)
            {
                i++;
                str[i] = emp.ToString();
            }
            return str;
        }
        public string[] TransformCitiesToString()
        {
            int i = -1;
            string[] str = new string[Cities.Count()];
            foreach (City city in Cities)
            {
                i++;
                str[i] = city.ID + " " + city.Name;
            }
            return str;
        }
        public int AverageAge()
        {
            int totalAge = 0;
            int counter = 0;
            foreach (Employee emp in Employees)
            {
                totalAge += emp.Age;
                counter++;
            }
            if (counter == 0)
                return 0;
            else return totalAge / counter;
        }
        public int AverageSalary()
        {
            int totalSalary = 0;
            int counter = 0;
            foreach (Employee emp in Employees)
            {
                totalSalary += emp.Salary;
                counter++;
            }
            if (counter == 0)
                return 0;
            else return totalSalary / counter;
        }
    }
}
