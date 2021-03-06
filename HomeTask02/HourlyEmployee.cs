using System;

namespace HomeTask02
{
    public class HourlyEmployee : BaseEmployee
    {
        public HourlyEmployee(string name, decimal hourlyRate) : base(name)
        {
            HourlyRate = hourlyRate > 0 ? hourlyRate : throw new ArgumentOutOfRangeException(nameof(hourlyRate));
        }

        public decimal HourlyRate { get; }
        public override decimal GetAvgMonthlyWages() => HourlyRate * 8 * 20.8m;
        public override string ToString() => $"{base.ToString()}\tHourly {HourlyRate:F2}\tMonthly {GetAvgMonthlyWages():F2}";
    }
}
