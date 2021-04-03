using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

using EmployeeViewer.Communication.EmployeeViewerService;
using EmployeeViewer.DataProvider;

namespace EmployeeViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        private readonly IDataProvider database;
        public ObservableCollection<Employee> Employees { get; set; }
        public Employee SelectedEmployee { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            database = new WebServiceProvider();
            this.DataContext = this;
            Employees = database.Employees;
            ctrEmployeeInfo.Departments = database.Departments;
        }

        private void LvEmployees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
            {
                SelectedEmployee = e.AddedItems[0] as Employee;
                ctrEmployeeInfo.Employee = SelectedEmployee.Clone();
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (lvEmployees.SelectedItems.Count < 1) return;
            database.Update(Employees.IndexOf(SelectedEmployee), ctrEmployeeInfo.Employee);
        }

        private void BtnNewDepartment_Click(object sender, RoutedEventArgs e)
        {
            var newDepartmentDialog = new CreateDepartment();

            if (newDepartmentDialog.ShowDialog() == true)
            {
                database.Add(newDepartmentDialog.Department);
                MessageBox.Show("Department added successfully");
            }
        }

        private void BtnNewEmployee_Click(object sender, RoutedEventArgs e)
        {
            var newEmployeeDialog = new CreateEmployee(database.Departments);
            if (newEmployeeDialog.ShowDialog() == true)
                if (Employees.Contains(newEmployeeDialog.Employee))
                    MessageBox.Show("Employee exists", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                    database.Add(newEmployeeDialog.Employee);
        }
    }
}
