/**********************
 * Mã sinh viên: 202418855
 * Họ tên: Đỗ Văn Chung
 **********************/
using System;
namespace OopLab04
{
    public class SalesEmployee : Employee
    {
        private readonly decimal baseSalary;
        private decimal salesRevenue;
        private readonly decimal commissionRate;
        public SalesEmployee(string employeeId, string fullName)
            : this(employeeId, fullName, "Unassigned", 0m, 0m, 0m) { }
        public SalesEmployee(string employeeId, string fullName, string department,
            decimal baseSalary, decimal salesRevenue, decimal commissionRate)
            : base(employeeId, fullName, department)
        {
            this.baseSalary = nonNegative(baseSalary, nameof(baseSalary));
            if (commissionRate < 0m || commissionRate > 0.3m)
                throw new ArgumentOutOfRangeException(nameof(commissionRate));
            this.commissionRate = commissionRate;
            updateSalesRevenue(salesRevenue);
        }
        public void updateSalesRevenue(decimal value)
        { salesRevenue = nonNegative(value, nameof(value)); }
        public override decimal calculateGrossPay()
        { return baseSalary + salesRevenue * commissionRate + MonthlyBonus; }
        public override string getEmployeeType() { return "SalesEmployee"; }
        public override void displayPayrollInfo()
        {
            base.displayPayrollInfo();
            Console.WriteLine("  Lương cơ bản: {0}; Doanh số: {1}; Tỷ lệ: {2:P0}; Hoa hồng: {3}",
                money(baseSalary), money(salesRevenue), commissionRate,
                money(salesRevenue * commissionRate));
        }
    }
}
