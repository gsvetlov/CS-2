using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeViewer.Data
{
    public class Employee : INotifyPropertyChanged
    {
        private string firstName;
        private string middleName;
        private string lastName;
        private int age;
        private Gender gender;
        private Department department;
        public string FirstName
        {
            get => firstName;
            set
            {
                firstName = value;
                NotifyPropertyChanged();
            }
        }
        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                NotifyPropertyChanged();
            }
        }
        public string MiddleName
        {
            get => middleName;
            set
            {
                middleName = value;
                NotifyPropertyChanged();
            }
        }
        public int Age
        {
            get => age;
            set
            {
                age = value;
                NotifyPropertyChanged();
            }
        }
        public Gender Gender
        {
            get => gender;
            set
            {
                gender = value;
                NotifyPropertyChanged();
            }
        }
        public Department Department
        {
            get => department;
            set
            {
                department = value;
                NotifyPropertyChanged();
            }
        }
        public string FullName => $"{FirstName} {MiddleName[0]}. {LastName}";

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
