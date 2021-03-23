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
    /// Interaction logic for CreateDepartment.xaml
    /// </summary>
    public partial class CreateDepartment : Window
    {
        private readonly InMemoryDatabase database;
        public CreateDepartment(InMemoryDatabase database)
        {
            this.database = database;
            InitializeComponent();
        }

        private void AddDepartment(string departmentName)
        {
            database.Departments.Add(new Department() { Name = departmentName });            
        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(tbName.Text)) return;
            AddDepartment(tbName.Text);
            tbName.Text = String.Empty;
            MessageBox.Show("Department added successfully");
        }
    }
}
