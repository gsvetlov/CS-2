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
using System.Windows.Shapes;

using EmployeeViewer.Data;

namespace EmployeeViewer
{
    /// <summary>
    /// Interaction logic for CreateEmployee.xaml
    /// </summary>
    public partial class CreateEmployee : Window
    {
        private readonly InMemoryDatabase database;
        public CreateEmployee(InMemoryDatabase database)
        {
            this.database = database;
            InitializeComponent();
            ctrEmployeeInfo.CaptionText = "New Employee";
            ctrEmployeeInfo.SetGenderEditable();
            //ctrEmployeeInfo.SetDepartments(database.Departments);
            ctrEmployeeInfo.SetEmployee(new Employee());
        }

        private bool Create()
        {
            Employee employee = ctrEmployeeInfo.UpdateEmployee(); //TODO Validate employee
            if (database.Employees.Contains(employee)) return false;
            database.Employees.Add(employee);
            ctrEmployeeInfo.SetEmployee(new Employee());
            return true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Create())
                MessageBox.Show("Employee added successfully");
            else
                MessageBox.Show("Failed to add new employee");
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
