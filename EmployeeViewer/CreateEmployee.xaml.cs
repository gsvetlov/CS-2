using System.Windows;

using EmployeeViewer.Data;

namespace EmployeeViewer
{
    /// <summary>
    /// Interaction logic for CreateEmployee.xaml
    /// </summary>
    public partial class CreateEmployee : Window
    {
        public Employee Employee { get => ctrEmployeeInfo.Employee; }
        public CreateEmployee(InMemoryDatabase database)
        {
            InitializeComponent();
            ctrEmployeeInfo.Employee = new Employee();
            ctrEmployeeInfo.CaptionText = "New Employee";
            ctrEmployeeInfo.IsGenderEditable = true;
            ctrEmployeeInfo.Departments = database.Departments;
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Button_Cancel(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
