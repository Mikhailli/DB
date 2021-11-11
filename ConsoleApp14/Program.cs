using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLogic;
using Ninject;


namespace ConsoleApp14
{
    class Program
    {
        static void Main(string[] args)
        {
            IKernel ninjectKernel = new StandardKernel(new SimpleConfigModule());
            BL logic = ninjectKernel.Get<BL>();
            logic.GetAll();

            while (true)
            {
                Console.WriteLine("1 - Добавить сотрудника");
                Console.WriteLine("2 - Показать всех сотрудников");
                Console.WriteLine("3 - Удалить сотрудника");
                Console.WriteLine("4 - Добавить город");
                Console.WriteLine("5 - Показать все города");
                Console.WriteLine("6 - Выйти");
                string tempSmth = Console.ReadLine();
                if (tempSmth == "1")
                {
                    Console.Write("Имя: ");
                    string name = Console.ReadLine();
                    Console.Write("Возраст: ");
                    int age = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Зарплата: ");
                    int salary = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Город: ");
                    int cityId = Convert.ToInt32(Console.ReadLine());
                    logic.AddEmployee(name, age, salary, cityId);

                }
                else if (tempSmth == "2")
                {
                    logic.GetAll();
                    string[] empsInfo = logic.TransformEmployeesToString();
                    for (int i = 0; i < empsInfo.Length; i++)
                    {
                        Console.WriteLine("Номер сотрудника: " + logic.EmpsIDs[i]);

                        string[] info = empsInfo[i].Split();
                        Console.WriteLine("Имя сотрудника: " + info[1]);
                        Console.WriteLine("Возраст сотрудника: " + info[2]);
                        Console.WriteLine("Зарплата сотрудника: " + info[3]);
                        Console.WriteLine("Город сотрудника: " + info[4]);
                        Console.WriteLine();
                    }
                    Console.WriteLine();
                }
                else if (tempSmth == "3")
                {
                    Console.WriteLine("Введите номер сотрудника");
                    int numOf = Convert.ToInt32(Console.ReadLine());
                    //Console.WriteLine("Сотрудник с именем " + logic.TransformEmployeesToString()[numOf - 1].Split()[1] + " был удалён из базы");
                    logic.DeleteEmployees(numOf);
                    logic.GetAll();
                }
                else if (tempSmth == "4")
                {
                    Console.Write("Название: ");
                    string name = Console.ReadLine();                    
                    logic.AddCity(name);

                }
                else if (tempSmth == "5")
                {
                    logic.GetAll();
                    string[] citiesInfo = logic.TransformCitiesToString();
                    for (int i = 0; i < citiesInfo.Length; i++)
                    {
                        Console.WriteLine(citiesInfo[i]);                                               
                    }
                    Console.WriteLine();
                }
                else if (tempSmth == "6")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Try again");
                }
            }
            Console.ReadLine();
        }
    }
    
}
