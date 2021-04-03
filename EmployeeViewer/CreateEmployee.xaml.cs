using System.Collections.ObjectModel;
using System.Windows;

using EmployeeViewer.Communication.EmployeeViewerService;

namespace EmployeeViewer
{
    /// <summary>
    /// Interaction logic for CreateEmployee.xaml
    /// </summary>
    public partial class CreateEmployee : Window
    {
        public Employee Employee { get => ctrEmployeeInfo.Employee; }
        public CreateEmployee(ObservableCollection<Department> departments)
        {
            InitializeComponent();
            ctrEmployeeInfo.Employee = new Employee();
            ctrEmployeeInfo.CaptionText = "New Employee";
            ctrEmployeeInfo.IsGenderEditable = true;
            ctrEmployeeInfo.Departments = departments;
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
