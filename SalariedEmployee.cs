/**********************
 * Mã sinh viên: 202418855
 * Họ tên: Đỗ Văn Chung
 **********************/
using System;
namespace OopLab04
{
    public class SalariedEmployee : Employee
    {
        private readonly decimal monthlySalary;
        private readonly decimal responsibilityAllowance;
        public SalariedEmployee(string employeeId, string fullName)
            : this(employeeId, fullName, "Unassigned", 0m, 0m) { }
        public SalariedEmployee(string employeeId, string fullName, string department,
            decimal monthlySalary, decimal responsibilityAllowance)
            : base(employeeId, fullName, department)
        {
            this.monthlySalary = nonNegative(monthlySalary, nameof(monthlySalary));
            this.responsibilityAllowance = nonNegative(responsibilityAllowance,
                nameof(responsibilityAllowance));
        }
        public override decimal calculateGrossPay()
        { return monthlySalary + responsibilityAllowance + MonthlyBonus; }
        public override string getEmployeeType() { return "SalariedEmployee"; }
        public override void displayPayrollInfo()
        {
            base.displayPayrollInfo();
            Console.WriteLine("  Lương tháng: {0}; Phụ cấp: {1}",
                money(monthlySalary), money(responsibilityAllowance));
        }
    }
}
