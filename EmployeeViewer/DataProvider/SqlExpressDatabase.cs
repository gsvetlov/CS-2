using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;

using EmployeeViewer.Data;

namespace EmployeeViewer.DataProvider
{
    public class SqlExpressDatabase : IDataProvider
    {
        private readonly string connectionString;

        public ObservableCollection<Department> Departments { get; set; }

        public ObservableCollection<Employee> Employees { get; set; }

        public SqlExpressDatabase(string connectionString) : this(connectionString, false) { }

        public SqlExpressDatabase(string connectionString, bool create)
        {
            this.connectionString = connectionString;
            Departments = new ObservableCollection<Department>();
            Employees = new ObservableCollection<Employee>();

            if (create) SyncToDatabase();
            else LoadFromDatabase();

        }
        public void Add(Employee employee) // TODO refactor to generic
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var insertIntoEmployees = $@"insert into dbo.employees (first_name, last_name, middle_name, age, gender, department_id) values ('{employee.FirstName}', '{employee.LastName}', '{employee.MiddleName}', {employee.Age}, {(int)employee.Gender}, {Departments.Single(d => d.Name == employee.Department.Name).Id})";
                var command = new SqlCommand(insertIntoEmployees, connection);
                var result = command.ExecuteNonQuery();
                if (result > 0)
                {
                    employee.Id = GetEmployeeId(employee, connection);
                    Employees.Add(employee);
                }
            }
        }
        public void Add(Department department) // TODO refactor to generic
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var insertIntoDepartments = $@"insert into [dbo].[departments] (name) values ('{department.Name}')";
                var command = new SqlCommand(insertIntoDepartments, connection);
                var result = command.ExecuteNonQuery();
                if (result > 0)
                {
                    department.Id = GetDepartmentId(department, connection);
                    Departments.Add(department);
                }
            }
        }

        public void Update(int index, Employee employee)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                int id = employee.Id == 0 ? GetEmployeeId(employee, connection) : employee.Id;
                var updateEmployee = $@"update dbo.employees set first_name = '{employee.FirstName}', last_name = '{employee.LastName}', middle_name = '{employee.MiddleName}', age = {employee.Age}, gender = {(int)employee.Gender}, department_id = {Departments.Single(d => d.Name == employee.Department.Name).Id} where id = {id}";
                var command = new SqlCommand(updateEmployee, connection);
                var result = command.ExecuteNonQuery();
                if (result > 0)
                {
                    Employees[index] = employee;
                }
            }
        }

        private int GetEmployeeId(Employee employee, SqlConnection connection) // TODO refactor to generic
        {
            var getEmployeeId = $@"select id from dbo.employees where (first_name = '{employee.FirstName}' and last_name = '{employee.LastName}' and middle_name = '{employee.MiddleName}' and age = {employee.Age} and gender = {(int)employee.Gender} and department_id = {Departments.Single(d => d.Name == employee.Department.Name).Id})";
            var command = new SqlCommand(getEmployeeId, connection);
            using (var result = command.ExecuteReader())
                return result.Read() ? result.GetInt32(0) : default;
        }

        private int GetDepartmentId(Department department, SqlConnection connection) // TODO refactor to generic
        {
            var getDepartmentId = $@"select id from dbo.departments where name = '{department.Name}'";
            var command = new SqlCommand(getDepartmentId, connection);
            using (var result = command.ExecuteReader())
                return result.Read() ? result.GetInt32(0) : default;
        }


        private void LoadFromDatabase()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                GetDepartments(connection);
                GetEmployees(connection);
            }
        }

        private void GetEmployees(SqlConnection connection)
        {
            var getAllEmployees = $@"select * from [dbo].[employees]";
            var command = new SqlCommand(getAllEmployees, connection);
            using (var result = command.ExecuteReader())
            {
                while (result.Read())
                {
                    var employee = new Employee()
                    {
                        Id = result.GetInt32(0),
                        FirstName = result.GetString(1),
                        LastName = result.GetString(2),
                        MiddleName = result.GetString(3),
                        Age = result.GetInt32(4),
                        Gender = (Gender)(result.GetBoolean(5) ? 1 : 0),
                        Department = Departments.Single(d => d.Id == result.GetInt32(6))
                    };
                    Employees.Add(employee);
                }
            }
        }

        private void GetDepartments(SqlConnection connection)
        {
            var getAllDepartments = $"select * from [dbo].[departments]";
            var command = new SqlCommand(getAllDepartments, connection);
            using (var result = command.ExecuteReader())
            {
                while (result.Read())
                {
                    var department = new Department()
                    {
                        Id = result.GetInt32(0),
                        Name = result.GetString(1),
                        Director = null
                    };
                    Departments.Add(department);
                }
            }
        }

        private void SyncToDatabase()
        {
            IDataProvider data = new InMemoryDatabase();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                foreach (var department in data.Departments)
                {
                    var insertIntoDepartments = $@"insert into [dbo].[departments] (name) values ('{department.Name}')";
                    var command = new SqlCommand(insertIntoDepartments, connection);
                    command.ExecuteNonQuery();
                }
                GetDepartments(connection);
                foreach (var employee in data.Employees)
                {
                    var insertIntoEmployees = $@"insert into [dbo].[employees] (first_name, last_name, middle_name, age, gender, department_id) values ('{employee.FirstName}', '{employee.LastName}', '{employee.MiddleName}', {employee.Age}, {(int)employee.Gender}, {Departments.Single(d => d.Name == employee.Department.Name).Id})";
                    var command = new SqlCommand(insertIntoEmployees, connection);
                    command.ExecuteNonQuery();
                }
                GetEmployees(connection);
            }
        }


    }
}
