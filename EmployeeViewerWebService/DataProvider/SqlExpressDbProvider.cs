using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;

using EmployeeViewer.Data;

namespace EmployeeViewerWebService.DataProvider
{
    public class SqlExpressDbProvider : BaseDbProvider, IEntityProvider
    {
        public SqlExpressDbProvider(string connectionString) : base(connectionString) { }

        public int Add(Employee employee)
        {
            var insert = $@"insert into dbo.employees (first_name, last_name, middle_name, age, gender, department_id) values ('{employee.FirstName}', '{employee.LastName}', '{employee.MiddleName}', {employee.Age}, {(int)employee.Gender}, {employee.Department.Id})";
            return Command(insert) > 0 ? GetId(employee) : -1;
        }
        public int Remove(Employee entity) => throw new NotImplementedException();
        public int Update(Employee employee)
        {
            int id = employee.Id > 0 ? employee.Id : FindEmployee(employee).First().Id;
            var update = $@"update dbo.employees set first_name = '{employee.FirstName}', last_name = '{employee.LastName}', middle_name = '{employee.MiddleName}', age = {employee.Age}, gender = {(int)employee.Gender}, department_id = {employee.Department.Id} where id = {id}";
            int result = Command(update);
            Debug.WriteLine($"updated {result} entries");
            return result;
        }
        public List<Employee> GetEmployees()
        {
            var queryAll = $@"SELECT * FROM dbo.employees";
            return Query(queryAll, (reader) =>
             {
                 var result = new List<Employee>();
                 while (reader.Read())
                     result.Add(ParseEmployee(reader));
                 return result;
             });
        }
        public Employee GetEmployee(int id)
        {
            var getEmployee = $"select * from dbo.employees where id = {id}";
            return Query(getEmployee, (reader) => reader.Read() ? ParseEmployee(reader) : null);
        }
        public List<Employee> FindEmployee(Employee pattern)
        {
            StringBuilder searchQuery = new StringBuilder("select * from dbo.employees where ");
            if (!string.IsNullOrWhiteSpace(pattern.FirstName))
                searchQuery.Append($"first_name = '{pattern.FirstName}' and ");
            if (!string.IsNullOrWhiteSpace(pattern.LastName))
                searchQuery.Append($"last_name = '{pattern.LastName}' and ");
            if (!string.IsNullOrWhiteSpace(pattern.MiddleName))
                searchQuery.Append($"middle_name = '{pattern.MiddleName}' and ");
            if (pattern.Age != 0)
                searchQuery.Append($"age = {pattern.Age} and ");
            if (pattern.Department?.Id > 0)
                searchQuery.Append($"department_id = {pattern.Department.Id} and ");
            searchQuery.Append($"gender = {(int)pattern.Gender}");
            return Query(searchQuery.ToString(), (reader) =>
            {
                var result = new List<Employee>();
                while (reader.Read())
                    result.Add(ParseEmployee(reader));
                return result;
            });
        }
        public int Add(Department department)
        {
            var insert = $@"insert into [dbo].[departments] (name) values ('{department.Name}')";
            return Command(insert) > 0 ? GetId(department) : -1;
        }
        public int Remove(Department entity) => throw new NotImplementedException();
        public int Update(Department department) => throw new NotImplementedException();
        public List<Department> GetDepartments()
        {
            var getAllDepartments = $"select * from dbo.departments";
            return Query(getAllDepartments, (reader) =>
            {
                var result = new List<Department>();
                while (reader.Read())
                    result.Add(ParseDepartment(reader));
                return result;
            });
        }
        public Department GetDepartment(int id)
        {
            var getDepartment = $"select * from dbo.departments where id = {id}";
            return Query(getDepartment, (reader) => reader.Read() ? ParseDepartment(reader) : null);
        }
        public List<Department> FindDepartment(Department pattern) => throw new NotImplementedException();

        private int GetId(Employee employee)
        {
            var queryId = $@"select id from dbo.employees where (first_name = '{employee.FirstName}' and last_name = '{employee.LastName}' and middle_name = '{employee.MiddleName}' and age = {employee.Age} and gender = {(int)employee.Gender} and department_id = {employee.Department.Id})";
            return Query<int>(queryId, (reader) => reader.Read() ? reader.GetInt32(0) : default);
        }
        private int GetId(Department department)
        {
            var queryId = $@"select id from dbo.departments where name = '{department.Name}'";
            return Query(queryId, (reader) => reader.Read() ? reader.GetInt32(0) : default);
        }

        private Employee ParseEmployee(SqlDataReader reader) =>
            new Employee()
            {
                Id = reader.GetInt32(0),
                FirstName = reader.GetString(1),
                LastName = reader.GetString(2),
                MiddleName = reader.GetString(3),
                Age = reader.GetInt32(4),
                Gender = (Gender)(reader.GetBoolean(5) ? 1 : 0),
                Department = GetDepartment(reader.GetInt32(6))
            };

        private Department ParseDepartment(SqlDataReader reader) =>
            new Department()
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1)
            };
    }
}
