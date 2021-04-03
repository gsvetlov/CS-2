namespace EmployeeViewer.Data
{
    public class Department : Entity
    {
        public string Name { get; set; }

        public override string ToString() => $"{Id}: Department: {Name}";

    }
}
