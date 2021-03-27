using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

using EmployeeViewer.Data;

namespace EmployeeViewer.Controls
{
    /// <summary>
    /// Interaction logic for EmployeeInfo.xaml
    /// </summary>
    public partial class EmployeeInfo : UserControl, INotifyPropertyChanged
    {
        private Employee employee;
        public bool IsGenderEditable { get; set; } = false;
        public List<Gender> Genders { get; set; }
        public Employee Employee
        {
            get => employee;
            set { employee = value; NotifyPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Department> Departments { get; set; }
        public string CaptionText { get => tCaption.Text; set => tCaption.Text = value; }
        public EmployeeInfo()
        {
            InitializeComponent();
            this.DataContext = this;
            Genders = new List<Gender>(Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList());
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
