/**********************
 * Mã sinh viên: 202418855
 * Họ tên: Đỗ Văn Chung
 **********************/
using System;
namespace OopLab04
{
    public class HourlyEmployee : Employee
    {
        private readonly decimal hourlyRate;
        private readonly decimal workedHours;
        public HourlyEmployee(string employeeId, string fullName)
            : this(employeeId, fullName, "Unassigned", 0m, 0m) { }
        public HourlyEmployee(string employeeId, string fullName, string department,
            decimal hourlyRate, decimal workedHours)
            : base(employeeId, fullName, department)
        {
            this.hourlyRate = nonNegative(hourlyRate, nameof(hourlyRate));
            if (workedHours < 0m || workedHours > 250m)
                throw new ArgumentOutOfRangeException(nameof(workedHours));
            this.workedHours = workedHours;
        }
        public override decimal calculateGrossPay()
        {
            return Math.Min(workedHours, 160m) * hourlyRate
                + Math.Max(workedHours - 160m, 0m) * hourlyRate * 1.5m + MonthlyBonus;
        }
        public override string getEmployeeType() { return "HourlyEmployee"; }
        public override void displayPayrollInfo()
        {
            base.displayPayrollInfo();
            Console.WriteLine("  Đơn giá: {0}; Giờ thường: {1}; Giờ vượt: {2}",
                money(hourlyRate), Math.Min(workedHours, 160m),
                Math.Max(workedHours - 160m, 0m));
        }
    }
}
