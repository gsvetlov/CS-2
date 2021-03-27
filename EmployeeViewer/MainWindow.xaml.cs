using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

using EmployeeViewer.Data;

namespace EmployeeViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        private readonly InMemoryDatabase database = new InMemoryDatabase();
        public ObservableCollection<Employee> Employees { get; set; }
        public Employee SelectedEmployee { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            Employees = database.Employees;
            ctrEmployeeInfo.Departments = database.Departments;
        }

        private void LvEmployees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
            {
                SelectedEmployee = e.AddedItems[0] as Employee;
                ctrEmployeeInfo.Employee = SelectedEmployee.Copy();
            }
        }



        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (lvEmployees.SelectedItems.Count < 1) return;
            Employees[Employees.IndexOf(SelectedEmployee)] = ctrEmployeeInfo.Employee;
        }

        private void BtnNewDepartment_Click(object sender, RoutedEventArgs e)
        {
            var newDepartmentDialog = new CreateDepartment(database);
            newDepartmentDialog.ShowDialog();
        }

        private void BtnNewEmployee_Click(object sender, RoutedEventArgs e)
        {
            var newEmployeeDialog = new CreateEmployee(database);
            if (newEmployeeDialog.ShowDialog() == true)
            {
                AddEmployee(newEmployeeDialog.Employee);
            }

        }

        private void AddEmployee(Employee employee)
        {
            if (Employees.Contains(employee))
                MessageBox.Show("Employee exists", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                database.Employees.Add(employee);
            }
        }
    }
}
