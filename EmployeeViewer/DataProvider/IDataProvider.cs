using System.Collections.ObjectModel;

using EmployeeViewer.Data;

namespace EmployeeViewer.DataProvider
{
    public interface IDataProvider
    {
        ObservableCollection<Department> Departments { get; set; }
        ObservableCollection<Employee> Employees { get; set; }

        void Add(Employee employee);
        void Update(int index, Employee employee);
        void Add(Department department);
    }
}