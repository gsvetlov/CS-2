using System;

namespace EmployeeViewer.Communication.EmployeeViewerService
{
    public partial class Employee : ICloneable
    {
        public Employee Clone() => MemberwiseClone() as Employee;
        object ICloneable.Clone() => Clone();
    }
}
