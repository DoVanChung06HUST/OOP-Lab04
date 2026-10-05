/**********************
 * Mã sinh viên: 202418855
 * Họ tên: Đỗ Văn Chung
 **********************/
using System;
using System.Globalization;
using System.Text;
namespace OopLab04
{
    internal static class Program
    {
        private static int Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("vi-VN");
            if (args.Length > 0 && args[0] == "--test") return PayrollTests.Run();
            Payroll payroll = new Payroll("2026-09");
            Employee a = new SalariedEmployee("E001", "Nguyễn Minh An", "Đào tạo", 15000000m, 2000000m);
            Employee b = new HourlyEmployee("E002", "Trần Thu Bình", "Hỗ trợ", 100000m, 150m);
            Employee c = new HourlyEmployee("E003", "Lê Hoàng Chi", "Hỗ trợ", 100000m, 170m);
            Employee d = new SalesEmployee("E004", "Phạm Quốc Dũng", "Kinh doanh", 8000000m, 200000000m, 0.05m);
            a.addBonus(1000000m);
            b.addBonus(500000m, "Hoàn thành công việc");
            d.addBonus(0.02m, 50000000m, "Thưởng doanh số");
            foreach (Employee e in new Employee[] { a, b, c, d }) payroll.addEmployee(e);
            payroll.displayPayroll();
            Console.WriteLine("PHÒNG HỖ TRỢ: {0:N0} VND", payroll.calculatePayrollByDepartment("Hỗ trợ"));
            Employee highest = payroll.findHighestPaidEmployee();
            if (highest != null) Console.WriteLine("CAO NHẤT: {0} - {1}", highest.EmployeeId, highest.FullName);
            return 0;
        }
    }
}
