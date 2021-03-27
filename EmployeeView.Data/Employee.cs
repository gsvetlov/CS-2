using System.ComponentModel;
using System.Runtime.CompilerServices;

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

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Employee Copy()
        {
            return this.MemberwiseClone() as Employee;
        }
    }
}
