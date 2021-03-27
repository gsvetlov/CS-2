using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EmployeeViewer.Data;

namespace EmployeeViewer
{
    public class InMemoryDatabase
    {
        private static readonly Random random = new Random();
        public ObservableCollection<Employee> Employees { get; set; }
        public ObservableCollection<Department> Departments { get; set; }

        public InMemoryDatabase()
        {
            Departments = new ObservableCollection<Department>(GenerateDepartments(10));
            Employees = new ObservableCollection<Employee>(GenerateEmployees(50));
        }

        private string GenerateString(int length, bool withCapital = false)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                sb.Append((char)random.Next('a', 'z'+1));
            }
            if (withCapital)
                sb[0] = (char)(sb[0] - 32); // turn first char to uppercase
            return sb.ToString();
        }

        private List<Department> GenerateDepartments(int count)
        {
            var departments = new List<Department>(count);
            for (int i = 0; i < count; i++)
                departments.Add(new Department() { Name = GenerateString(random.Next(3, 10), true) });
            return departments;
        }

        private List<Employee> GenerateEmployees(int count)
        {
            var employees = new List<Employee>(count);
            for (int i = 0; i < count; i++)
            {
                string firstName = GenerateString(random.Next(6) + 3, true);
                string lastName = GenerateString(random.Next(6) + 3, true);
                string middleName = GenerateString(random.Next(6) + 3, true);
                var gender = (Gender)random.Next(2);
                int age = random.Next(18, 90);
                var department = Departments?[random.Next(Departments.Count)];
                employees.Add(new Employee()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    MiddleName = middleName,
                    Gender = gender,
                    Age = age,
                    Department = department
                });
            }
            return employees;
        }

       
    }
}
