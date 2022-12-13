using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Web.UI;
using static System.Net.Mime.MediaTypeNames;

namespace Backend.Services
{
    public class ApplicationService : IApplicationService
    {
        BussinessLogic.IPersons _persons;
        BussinessLogic.ISalaries _salaries;

        //Here you should create Menu which your Console application will show to user
        //User should be able to choose between: 1. Movie star 2. Calculate Net salary 3. Exit
        public ApplicationService(BussinessLogic.IPersons persons, BussinessLogic.ISalaries salaries)
        {
            _persons =  persons;
            _salaries = salaries;
        }

        public void Run()
        {
            ConsoleKey userChoice = DisplayMenu();
            Console.WriteLine();

            try
            {
                switch (userChoice)
                {
                    case ConsoleKey.D1:
                        DisplayMovieStars();
                        PressAnyKeyMsg();
                        Run();
                        break;

                    case ConsoleKey.D2:
                        CalculateNetSalary();
                        PressAnyKeyMsg();
                        Run();
                        break;

                    case ConsoleKey.D3:
                        Environment.Exit(0);
                        break;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                PressAnyKeyMsg();
                Run();
            }
        }

        private ConsoleKey DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("Main Menu");
            Console.WriteLine("---------");
            Console.WriteLine();
            Console.WriteLine("1. View Movie Star List");
            Console.WriteLine("2. Calculate Net Salary");
            Console.WriteLine("3. Exit");
            Console.WriteLine();
            ConsoleKey key = Console.ReadKey().Key;
            while (key != ConsoleKey.D1 && key != ConsoleKey.D2 && key != ConsoleKey.D3)
            {
                Console.Write("\b");
                Console.Write(" ");
                Console.Write("\b");
                key = Console.ReadKey().Key;
            }

            return key;
        }

        private void DisplayMovieStars()
        {
            DateTime bornDate = DateTime.Parse("01/01/1996");
            string nationality = "China";
            List<Models.Person>persons = _persons.GetBornAfterDateByNationality(bornDate, nationality);
            Console.Clear();
            if (persons != null && persons.Count > 0)
            {
                for (int i = 0; i < persons.Count; i++)
                {
                    Console.WriteLine(persons[i].Name);
                    Console.WriteLine(persons[i].Sex);
                    Console.WriteLine(persons[i].Nationality);
                    var timeSpan = DateTime.Now - persons[i].DateOfBirth;
                    int age = new DateTime(timeSpan.Ticks).Year - 1;
                    Console.WriteLine(age.ToString() + " years old");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("No data available");
            }
        }

        private void CalculateNetSalary()
        {
            bool isValid = false;
            double dSalary = 0;

            Console.Clear();
            Console.Write("Enter a valid gross salary: ");
            string sSalary = Console.ReadLine();

            isValid = double.TryParse(sSalary, out dSalary);
            if (isValid)
            {
                isValid = (dSalary > 0);
            }
            if (!isValid)
            {
                Console.WriteLine();
                Console.WriteLine("The input salary is in a wrong format");
                PressAnyKeyMsg();
                Run();
            }
            else
            {
                double netSalary = _salaries.CalculateNet(dSalary);
                Console.WriteLine("Net Salary: " + netSalary.ToString("C", CultureInfo.CurrentCulture));
            }
        }

        private void PressAnyKeyMsg()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to return to menu");
            Console.ReadKey();
        }

    }
}
