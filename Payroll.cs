/**********************
 * Mã sinh viên: 202418855
 * Họ tên: Đỗ Văn Chung
 **********************/
using System;
using System.Collections.Generic;
using System.Globalization;
namespace OopLab04
{
    // Kết tập: danh sách giữ tham chiếu Employee, không sao chép đối tượng.
    public class Payroll
    {
        private readonly string period;
        private readonly List<Employee> employees = new List<Employee>();
        public string Period { get { return period; } }
        public int Count { get { return employees.Count; } }
        public Payroll(string period)
        {
            DateTime parsed;
            if (period == null || !DateTime.TryParseExact(period, "yyyy-MM",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out parsed))
                throw new ArgumentException("Kỳ lương phải có dạng yyyy-MM.", nameof(period));
            this.period = period;
        }
        public bool addEmployee(Employee employee)
        {
            if (employee == null) throw new ArgumentNullException(nameof(employee));
            if (findEmployee(employee.EmployeeId) != null) return false;
            employees.Add(employee);
            return true;
        }
        // Trả về null khi không tìm thấy. Mã so sánh không phân biệt hoa/thường.
        public Employee findEmployee(string employeeId)
        {
            if (string.IsNullOrWhiteSpace(employeeId))
                throw new ArgumentException("Mã không được rỗng.", nameof(employeeId));
            foreach (Employee employee in employees)
                if (string.Equals(employee.EmployeeId, employeeId.Trim(),
                    StringComparison.OrdinalIgnoreCase)) return employee;
            return null;
        }
        public decimal calculateTotalPayroll()
        {
            decimal total = 0m;
            foreach (Employee employee in employees) total += employee.calculateGrossPay();
            return total;
        }
        public decimal calculatePayrollByDepartment(string department)
        {
            if (string.IsNullOrWhiteSpace(department))
                throw new ArgumentException("Phòng ban không được rỗng.", nameof(department));
            decimal total = 0m;
            foreach (Employee employee in employees)
                if (string.Equals(employee.Department, department.Trim(),
                    StringComparison.OrdinalIgnoreCase)) total += employee.calculateGrossPay();
            return total;
        }
        // Nếu hòa, lấy người được thêm trước. Danh sách rỗng trả về null.
        public Employee findHighestPaidEmployee()
        {
            Employee highest = null;
            foreach (Employee employee in employees)
                if (highest == null || employee.calculateGrossPay() > highest.calculateGrossPay())
                    highest = employee;
            return highest;
        }
        public void displayPayroll()
        {
            Console.WriteLine("BẢNG LƯƠNG KỲ " + Period);
            if (employees.Count == 0) Console.WriteLine("Chưa có nhân sự.");
            foreach (Employee employee in employees) employee.displayPayrollInfo();
            Console.WriteLine("TỔNG: {0:N0} VND", calculateTotalPayroll());
        }
    }
}
