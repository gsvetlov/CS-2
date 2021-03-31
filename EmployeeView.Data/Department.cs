namespace EmployeeViewer.Data
{
    public class Department : Entity
    {
        public string Name { get; set; }
        public Employee Director { get; set; }

    }
}
