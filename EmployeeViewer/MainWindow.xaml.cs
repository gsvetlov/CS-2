using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using EmployeeViewer.Data;

namespace EmployeeViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        private readonly InMemoryDatabase database = new InMemoryDatabase();
        public MainWindow()
        {
            InitializeComponent();
            UpdateBindings();
        }

        private void UpdateBindings()
        {
            lvEmployees.ItemsSource = null;
            lvEmployees.ItemsSource = database.Employees;
            ctrEmployeeInfo.SetDepartments(database.Departments);

        }

        private void LvEmployees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
            {
                ctrEmployeeInfo.SetEmployee(e.AddedItems[0] as Employee);
            }
        }

        

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (lvEmployees.SelectedItems.Count < 1)
                return;
            _ = ctrEmployeeInfo.UpdateEmployee();
            UpdateBindings();
        }

        private void btnNewDepartment_Click(object sender, RoutedEventArgs e)
        {
            CreateDepartment newDepartmentWindow = new CreateDepartment(database);
            newDepartmentWindow.Owner = this;
            newDepartmentWindow.ShowDialog();
            UpdateBindings();
        }

        private void btnNewEmployee_Click(object sender, RoutedEventArgs e)
        {
            CreateEmployee newEmployeeWindow = new CreateEmployee(database);
            newEmployeeWindow.Owner = this;
            newEmployeeWindow.ShowDialog();
            UpdateBindings();
        }
    }
}
