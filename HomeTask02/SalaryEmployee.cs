using System;

namespace HomeTask02
{
    class SalaryEmployee : BaseEmployee
    {
        public decimal Salary { get; }
        public SalaryEmployee(string name, decimal salary) : base(name)
        {
            Salary = salary > 0 ? salary : throw new ArgumentOutOfRangeException(nameof(salary));
        }
        public override decimal GetAvgMonthlyWages() => Salary;
        public override string ToString() => $"{base.ToString()}\tSalary {Salary:F2}\tMonthly {GetAvgMonthlyWages():F2}";
    }
}
