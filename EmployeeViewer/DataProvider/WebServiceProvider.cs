using System.Collections.ObjectModel;
using System.Linq;

using EmployeeViewer.Communication.EmployeeViewerService;

namespace EmployeeViewer.DataProvider
{
    class WebServiceProvider : IDataProvider
    {
        private readonly EmployeeViewerServiceSoapClient client;
        public ObservableCollection<Department> Departments { get; set; }
        public ObservableCollection<Employee> Employees { get; set; }

        public WebServiceProvider()
        {
            client = new EmployeeViewerServiceSoapClient();
            Departments = new ObservableCollection<Department>(client.GetDepartments());
            Employees = new ObservableCollection<Employee>(client.GetEmployees());

            //не разобрался, как реализовать связывание десериализованых объектов без такого костыля.
            Link();
        }

        private void Link() // сопоставляем ссылки между Employee.Department и коллекцией Department
        {
            foreach (var employee in Employees)
            {
                employee.Department = Departments.Single((d) => d.Id == employee.Department.Id);
            }
        }

        public void Add(Employee employee)
        {
            employee.Id = client.AddEmployee(employee);
            Employees.Add(employee);

        }
        public void Update(int index, Employee employee)
        {
            int result = client.UpdateEmployee(employee);
            if (result > 0) Employees[index] = employee;

        }
        public void Add(Department department)
        {
            department.Id = client.AddDepartment(department);
            Departments.Add(department);
        }
    }
}
