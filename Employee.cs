/**********************
 * Mã sinh viên: 202418855
 * Họ tên: Đỗ Văn Chung
 **********************/
using System;
using System.Globalization;

namespace OopLab04
{
    // Lớp trừu tượng: không có một công thức lương chung cho mọi nhân sự.
    public abstract class Employee
    {
        private readonly string employeeId;
        private readonly string fullName;
        private readonly string department;
        private decimal monthlyBonus;

        public string EmployeeId { get { return employeeId; } }
        public string FullName { get { return fullName; } }
        public string Department { get { return department; } }
        public decimal MonthlyBonus { get { return monthlyBonus; } }

        protected Employee(string employeeId, string fullName)
            : this(employeeId, fullName, "Unassigned") { }

        protected Employee(string employeeId, string fullName, string department)
        {
            this.employeeId = requireText(employeeId, nameof(employeeId));
            this.fullName = requireText(fullName, nameof(fullName));
            this.department = requireText(department, nameof(department));
            monthlyBonus = 0m;
        }

        protected static string requireText(string value, string parameter)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Nội dung không được rỗng.", parameter);
            return value.Trim();
        }

        protected static decimal nonNegative(decimal value, string parameter)
        {
            if (value < 0m) throw new ArgumentOutOfRangeException(parameter);
            return value;
        }

        public void addBonus(decimal amount)
        {
            if (amount <= 0m) throw new ArgumentOutOfRangeException(nameof(amount));
            // Phép cộng hoàn tất rồi mới gán: nếu tràn số, tổng cũ được giữ nguyên.
            monthlyBonus = checked(monthlyBonus + amount);
        }

        public void addBonus(decimal amount, string reason)
        {
            requireText(reason, nameof(reason));
            addBonus(amount);
        }

        public void addBonus(decimal rate, decimal referenceAmount, string reason)
        {
            requireText(reason, nameof(reason));
            if (rate <= 0m || rate > 0.5m)
                throw new ArgumentOutOfRangeException(nameof(rate));
            if (referenceAmount <= 0m)
                throw new ArgumentOutOfRangeException(nameof(referenceAmount));
            addBonus(checked(rate * referenceAmount));
        }

        // Chỉ lưu tổng thưởng; gọi khi tái sử dụng nhân sự cho kỳ mới.
        public void resetMonthlyBonus() { monthlyBonus = 0m; }
        public abstract decimal calculateGrossPay();
        public abstract string getEmployeeType();
        public virtual void displayPayrollInfo()
        {
            Console.WriteLine("{0} | {1} | {2} | {3}",
                EmployeeId, FullName, Department, getEmployeeType());
            Console.WriteLine("  Thưởng: {0}; Tổng: {1} VND",
                money(MonthlyBonus), money(calculateGrossPay()));
        }
        protected static string money(decimal value)
        {
            return value.ToString("N0", CultureInfo.GetCultureInfo("vi-VN"));
        }
    }
}
