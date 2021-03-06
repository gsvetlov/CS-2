using System;
using System.Collections.Generic;

namespace HomeTask02
{
    public abstract class BaseEmployee : IEquatable<BaseEmployee>, IComparable<BaseEmployee>
    {
        protected BaseEmployee(string name) => Name = name ?? throw new ArgumentNullException(nameof(name));

        public string Name { get; set; }
        public abstract decimal GetAvgMonthlyWages();
        public override bool Equals(object obj) => Equals(obj as BaseEmployee);
        public bool Equals(BaseEmployee other) => other != null && Name == other.Name;

        public int CompareTo(BaseEmployee other) => Name.CompareTo(other.Name);
        public override int GetHashCode() => 539060726 + EqualityComparer<string>.Default.GetHashCode(Name);

        public override string ToString() => $"Employee {Name}";
    }    
    public class EmployeeCompareByByWages : IComparer<BaseEmployee>
    {
        public int Compare(BaseEmployee x, BaseEmployee y) => x.GetAvgMonthlyWages().CompareTo(y.GetAvgMonthlyWages());
    }
}
