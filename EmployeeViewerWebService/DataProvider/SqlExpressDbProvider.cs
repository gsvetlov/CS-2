using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;

using EmployeeViewer.Data;

namespace EmployeeViewerWebService.DataProvider
{
    public class SqlExpressDbProvider : BaseDbProvider, IEntityProvider//<Employee>, IEntityProvider<Department>
    {
        public SqlExpressDbProvider(string connectionString) : base(connectionString) { }

        protected override void Connect()
        {
            try
            {
                base.Connect();
                Debug.WriteLine("connection open");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }
        protected override void Disconnect()
        {
            try
            {
                base.Disconnect();
                Debug.WriteLine("connection closed");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public int Add(Employee entity)
        {
            try
            {
                var insert = $@"insert into dbo.employees (first_name, last_name, middle_name, age, gender, department_id) values ('{entity.FirstName}', '{entity.LastName}', '{entity.MiddleName}', {entity.Age}, {(int)entity.Gender}, {entity.Department.Id})";
                return Command(insert) > 0 ? GetId(entity) : -1;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }
        public int Remove(Employee entity) => throw new NotImplementedException();
        public int Update(Employee employee)
        {
            try
            {
                int id = employee.Id > 0 ? employee.Id : FindEmployee(employee).First().Id;
                var update = $@"update dbo.employees set first_name = '{employee.FirstName}', last_name = '{employee.LastName}', middle_name = '{employee.MiddleName}', age = {employee.Age}, gender = {(int)employee.Gender}, department_id = {employee.Department.Id} where id = {id}";
                int result = Command(update);
                Debug.WriteLine($"updated {result} entries");
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
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
            try
            {
                return Command(insert) > 0 ? GetId(department) : -1;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
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

    //    public ObservableCollection<Department> Departments { get; set; }

    //    public ObservableCollection<Employee> Employees { get; set; }        

    //    public SqlExpressDbProvider()
    //    {
    //        this.connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["employeeDb"].ConnectionString;
    //        Departments = new ObservableCollection<Department>();
    //        Employees = new ObservableCollection<Employee>();

    //        LoadFromDatabase();

    //    }
    //    public void Create(Employee employee)
    //    {
    //        using (SqlConnection connection = new SqlConnection(connectionString))
    //        {
    //            connection.Open();
    //            var insertIntoEmployees = $@"insert into dbo.employees (first_name, last_name, middle_name, age, gender, department_id) values ('{employee.FirstName}', '{employee.LastName}', '{employee.MiddleName}', {employee.Age}, {(int)employee.Gender}, {Departments.Single(d => d.Name == employee.Department.Name).Id})";
    //            var command = new SqlCommand(insertIntoEmployees, connection);
    //            var result = command.ExecuteNonQuery();
    //            if (result > 0)
    //            {
    //                employee.Id = GetEmployeeId(employee, connection);
    //                Employees.Add(employee);
    //            }
    //        }
    //    }
    //    public void Add(Department department) // TODO refactor to generic
    //    {
    //        using (SqlConnection connection = new SqlConnection(connectionString))
    //        {
    //            connection.Open();
    //            var insertIntoDepartments = $@"insert into [dbo].[departments] (name) values ('{department.Name}')";
    //            var command = new SqlCommand(insertIntoDepartments, connection);
    //            var result = command.ExecuteNonQuery();
    //            if (result > 0)
    //            {
    //                department.Id = GetDepartmentId(department, connection);
    //                Departments.Add(department);
    //            }
    //        }
    //    }

    //    public void Update(int index, Employee employee)
    //    {
    //        using (SqlConnection connection = new SqlConnection(connectionString))
    //        {
    //            connection.Open();
    //            int id = employee.Id == 0 ? GetEmployeeId(employee, connection) : employee.Id;
    //            var updateEmployee = $@"update dbo.employees set first_name = '{employee.FirstName}', last_name = '{employee.LastName}', middle_name = '{employee.MiddleName}', age = {employee.Age}, gender = {(int)employee.Gender}, department_id = {Departments.Single(d => d.Name == employee.Department.Name).Id} where id = {id}";
    //            var command = new SqlCommand(updateEmployee, connection);
    //            var result = command.ExecuteNonQuery();
    //            if (result > 0)
    //            {
    //                Employees[index] = employee;
    //            }
    //        }
    //    }

    //    private int GetEmployeeId(Employee employee, SqlConnection connection) // TODO refactor to generic
    //    {
    //        var getEmployeeId = $@"select id from dbo.employees where (first_name = '{employee.FirstName}' and last_name = '{employee.LastName}' and middle_name = '{employee.MiddleName}' and age = {employee.Age} and gender = {(int)employee.Gender} and department_id = {Departments.Single(d => d.Name == employee.Department.Name).Id})";
    //        var command = new SqlCommand(getEmployeeId, connection);
    //        using (var result = command.ExecuteReader())
    //            return result.Read() ? result.GetInt32(0) : default;
    //    }

    //    private int GetDepartmentId(Department department, SqlConnection connection) // TODO refactor to generic
    //    {
    //        var getDepartmentId = $@"select id from dbo.departments where name = '{department.Name}'";
    //        var command = new SqlCommand(getDepartmentId, connection);
    //        using (var result = command.ExecuteReader())
    //            return result.Read() ? result.GetInt32(0) : default;
    //    }


    //    private void LoadFromDatabase()
    //    {
    //        using (SqlConnection connection = new SqlConnection(connectionString))
    //        {
    //            connection.Open();
    //            GetDepartments(connection);
    //            GetEmployees(connection);
    //        }
    //    }

    //    private void GetEmployees(SqlConnection connection)
    //    {
    //        var getAllEmployees = $@"select * from [dbo].[employees]";
    //        var command = new SqlCommand(getAllEmployees, connection);
    //        using (var result = command.ExecuteReader())
    //        {
    //            while (result.Read())
    //            {
    //                var employee = new Employee()
    //                {
    //                    Id = result.GetInt32(0),
    //                    FirstName = result.GetString(1),
    //                    LastName = result.GetString(2),
    //                    MiddleName = result.GetString(3),
    //                    Age = result.GetInt32(4),
    //                    Gender = (Gender)(result.GetBoolean(5) ? 1 : 0),
    //                    Department = Departments.Single(d => d.Id == result.GetInt32(6))
    //                };
    //                Employees.Add(employee);
    //            }
    //        }
    //    }

    //    private void GetDepartments(SqlConnection connection)
    //    {
    //        var getAllDepartments = $"select * from [dbo].[departments]";
    //        var command = new SqlCommand(getAllDepartments, connection);
    //        using (var result = command.ExecuteReader())
    //        {
    //            while (result.Read())
    //            {
    //                var department = new Department()
    //                {
    //                    Id = result.GetInt32(0),
    //                    Name = result.GetString(1),
    //                    Manager = null
    //                };
    //                Departments.Add(department);
    //            }
    //        }
    //    }

}
