using System;
using System.Windows;

using EmployeeViewer.Communication.EmployeeViewerService;

namespace EmployeeViewer
{
    /// <summary>
    /// Interaction logic for CreateDepartment.xaml
    /// </summary>
    public partial class CreateDepartment : Window
    {
        public Department Department { get; set; }
        public CreateDepartment()
        {
            InitializeComponent();
        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(tbName.Text)) return;
            Department = new Department() { Name = tbName.Text };
            tbName.Text = String.Empty;
            DialogResult = true;
        }
    }
}
