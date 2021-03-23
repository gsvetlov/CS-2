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

namespace EmployeeViewer.Controls
{
    /// <summary>
    /// Interaction logic for EmployeeInfo.xaml
    /// </summary>
    public partial class EmployeeInfo : UserControl
    {
        private Employee employee;
        private List<Department> departments;
        public EmployeeInfo()
        {
            InitializeComponent();
        }

        public void SetDepartments(List<Department> list)
        {
            departments = list;
            cbDepartment.ItemsSource = departments;
        }

        public void SetEmployee(Employee emp)
        {
            employee = emp;
            tbFirstName.Text = employee.FirstName;
            tbLastName.Text = employee.LastName;
            tbMiddleName.Text = employee.MiddleName;
            tbAge.Text = employee.Age.ToString();
            rbGenderMale.IsChecked = employee.Gender == Gender.Male;
            rbGenderFemale.IsChecked = !rbGenderMale.IsChecked;
            cbDepartment.SelectedItem = employee.Department;
        }

        public Employee UpdateEmployee()
        {
            employee.FirstName = tbFirstName.Text;
            employee.LastName = tbLastName.Text;
            employee.MiddleName = tbMiddleName.Text;
            employee.Age = int.TryParse(tbAge.Text, out int age) ? age : employee.Age;
            employee.Gender = rbGenderMale.IsChecked ?? false ? Gender.Male : Gender.Female;
            employee.Department = (Department)cbDepartment.SelectedItem;
            return employee;
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
