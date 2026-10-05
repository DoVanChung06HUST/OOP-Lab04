/**********************
 * Mã sinh viên: 202418855
 * Họ tên: Đỗ Văn Chung
 **********************/
using System;
namespace OopLab04
{
    // Mỗi nhóm có dữ liệu độc lập; lỗi làm tiến trình trả mã 1.
    internal static class PayrollTests
    {
        private static int passed, failed;
        private static void equal(decimal expected, decimal actual)
        { if (expected != actual) throw new Exception("Expected " + expected + ", actual " + actual); }
        private static void truth(bool condition)
        { if (!condition) throw new Exception("Điều kiện không thỏa."); }
        private static void reject<T>(Action action) where T : Exception
        {
            try { action(); }
            catch (T) { return; }
            throw new Exception("Thiếu ngoại lệ " + typeof(T).Name);
        }
        private static void test(string name, Action action)
        {
            try { action(); passed++; Console.WriteLine("PASS " + name); }
            catch (Exception ex) { failed++; Console.WriteLine("FAIL " + name + ": " + ex.Message); }
        }
        public static int Run()
        {
            passed = failed = 0;
            test("TC01 - Dữ liệu mẫu và tổng hợp", () =>
            {
                Payroll p = new Payroll("2026-09");
                Employee a = new SalariedEmployee("E001", "Nguyễn Minh An", "Đào tạo", 15000000m, 2000000m);
                Employee b = new HourlyEmployee("E002", "Trần Thu Bình", "Hỗ trợ", 100000m, 150m);
                Employee c = new HourlyEmployee("E003", "Lê Hoàng Chi", "Hỗ trợ", 100000m, 170m);
                Employee d = new SalesEmployee("E004", "Phạm Quốc Dũng", "Kinh doanh", 8000000m, 200000000m, .05m);
                a.addBonus(1000000m); b.addBonus(500000m, "Tốt"); d.addBonus(.02m, 50000000m, "Tốt");
                Employee[] all = { a, b, c, d }; decimal[] expected = { 18000000m, 15500000m, 17500000m, 19000000m };
                for (int i = 0; i < all.Length; i++) { equal(expected[i], all[i].calculateGrossPay()); truth(p.addEmployee(all[i])); }
                equal(70000000m, p.calculateTotalPayroll()); equal(33000000m, p.calculatePayrollByDepartment("Hỗ trợ"));
                truth(p.findHighestPaidEmployee() == d); truth(p.findEmployee(" e001 ") == a);
            });
            test("TC02 - Constructor rút gọn", () =>
            {
                Employee[] all = { new SalariedEmployee("A", "An"), new HourlyEmployee("B", "Bình"), new SalesEmployee("C", "Chi") };
                foreach (Employee e in all) { truth(e.Department == "Unassigned"); equal(0m, e.MonthlyBonus); equal(0m, e.calculateGrossPay()); }
            });
            test("TC03 - Mã, tên, phòng rỗng", () =>
            {
                reject<ArgumentException>(() => new SalariedEmployee(" ", "An"));
                reject<ArgumentException>(() => new SalariedEmployee("A", " "));
                reject<ArgumentException>(() => new SalariedEmployee("A", "An", " ", 0m, 0m));
            });
            test("TC04 - Lương và phụ cấp âm", () =>
            {
                reject<ArgumentOutOfRangeException>(() => new SalariedEmployee("A", "An", "P", -1m, 0m));
                reject<ArgumentOutOfRangeException>(() => new SalariedEmployee("A", "An", "P", 0m, -1m));
                reject<ArgumentOutOfRangeException>(() => new SalesEmployee("A", "An", "P", -1m, 0m, 0m));
            });
            test("TC05 - Giờ biên 0, 160, 250", () =>
            {
                equal(0m, new HourlyEmployee("A", "An", "P", 100000m, 0m).calculateGrossPay());
                equal(16000000m, new HourlyEmployee("A", "An", "P", 100000m, 160m).calculateGrossPay());
                equal(29500000m, new HourlyEmployee("A", "An", "P", 100000m, 250m).calculateGrossPay());
            });
            test("TC06 - Giờ sai và đơn giá âm", () =>
            {
                reject<ArgumentOutOfRangeException>(() => new HourlyEmployee("A", "An", "P", 1m, -1m));
                reject<ArgumentOutOfRangeException>(() => new HourlyEmployee("A", "An", "P", 1m, 251m));
                reject<ArgumentOutOfRangeException>(() => new HourlyEmployee("A", "An", "P", -1m, 0m));
            });
            test("TC07 - Hoa hồng biên 0 và 0.3", () =>
            {
                equal(0m, new SalesEmployee("A", "An", "P", 0m, 100m, 0m).calculateGrossPay());
                equal(30m, new SalesEmployee("A", "An", "P", 0m, 100m, .3m).calculateGrossPay());
            });
            test("TC08 - Hoa hồng ngoài miền", () =>
            {
                reject<ArgumentOutOfRangeException>(() => new SalesEmployee("A", "An", "P", 0m, 0m, -.01m));
                reject<ArgumentOutOfRangeException>(() => new SalesEmployee("A", "An", "P", 0m, 0m, .31m));
            });
            test("TC09 - Cập nhật doanh số có kiểm soát", () =>
            {
                SalesEmployee e = new SalesEmployee("A", "An", "P", 0m, 100m, .1m);
                e.updateSalesRevenue(200m); equal(20m, e.calculateGrossPay());
                reject<ArgumentOutOfRangeException>(() => e.updateSalesRevenue(-1m)); equal(20m, e.calculateGrossPay());
                reject<ArgumentOutOfRangeException>(() => new SalesEmployee("B", "Bình", "P", 0m, -1m, 0m));
            });
            test("TC10 - Thưởng cố định sai", () =>
            {
                Employee e = new SalariedEmployee("A", "An");
                reject<ArgumentOutOfRangeException>(() => e.addBonus(0m));
                reject<ArgumentOutOfRangeException>(() => e.addBonus(-1m)); equal(0m, e.MonthlyBonus);
            });
            test("TC11 - Lý do rỗng không thay đổi thưởng", () =>
            {
                Employee e = new SalariedEmployee("A", "An"); e.addBonus(10m);
                reject<ArgumentException>(() => e.addBonus(5m, " "));
                reject<ArgumentException>(() => e.addBonus(.1m, 100m, null)); equal(10m, e.MonthlyBonus);
            });
            test("TC12 - Tỷ lệ thưởng biên và sai", () =>
            {
                Employee e = new SalariedEmployee("A", "An"); e.addBonus(.5m, 100m, "Tốt"); equal(50m, e.MonthlyBonus);
                reject<ArgumentOutOfRangeException>(() => e.addBonus(0m, 100m, "Tốt"));
                reject<ArgumentOutOfRangeException>(() => e.addBonus(-.1m, 100m, "Tốt"));
                reject<ArgumentOutOfRangeException>(() => e.addBonus(.51m, 100m, "Tốt")); equal(50m, e.MonthlyBonus);
            });
            test("TC13 - Giá trị tham chiếu sai", () =>
            {
                Employee e = new SalariedEmployee("A", "An");
                reject<ArgumentOutOfRangeException>(() => e.addBonus(.1m, 0m, "Tốt"));
                reject<ArgumentOutOfRangeException>(() => e.addBonus(.1m, -1m, "Tốt")); equal(0m, e.MonthlyBonus);
            });
            test("TC14 - Cộng dồn và đặt lại thưởng", () =>
            {
                Employee e = new SalariedEmployee("A", "An");
                e.addBonus(10m); e.addBonus(20m, "Tốt"); e.addBonus(.1m, 100m, "Tốt"); equal(40m, e.MonthlyBonus);
                e.resetMonthlyBonus(); equal(0m, e.MonthlyBonus);
            });
            test("TC15 - Mã trùng và null", () =>
            {
                Payroll p = new Payroll("2026-09"); truth(p.addEmployee(new SalariedEmployee("A", "An")));
                truth(!p.addEmployee(new HourlyEmployee(" a ", "Bình"))); equal(1m, p.Count);
                reject<ArgumentNullException>(() => p.addEmployee(null)); equal(1m, p.Count);
            });
            test("TC16 - Danh sách rỗng và không tìm thấy", () =>
            {
                Payroll p = new Payroll("2026-09"); equal(0m, p.calculateTotalPayroll());
                equal(0m, p.calculatePayrollByDepartment("P")); truth(p.findEmployee("X") == null);
                truth(p.findHighestPaidEmployee() == null); p.displayPayroll();
            });
            test("TC17 - Hòa thu nhập chọn người thêm trước", () =>
            {
                Payroll p = new Payroll("2026-09"); Employee a = new SalariedEmployee("A", "An");
                p.addEmployee(a); p.addEmployee(new HourlyEmployee("B", "Bình")); truth(p.findHighestPaidEmployee() == a);
            });
            test("TC18 - Kỳ lương và truy vấn sai", () =>
            {
                reject<ArgumentException>(() => new Payroll("2026-13")); reject<ArgumentException>(() => new Payroll("2026-9"));
                Payroll p = new Payroll("2026-09"); reject<ArgumentException>(() => p.findEmployee(" "));
                reject<ArgumentException>(() => p.calculatePayrollByDepartment(" "));
            });
            test("TC19 - Hiển thị đa hình đủ thành phần", () =>
            {
                Payroll p = new Payroll("2026-09"); p.addEmployee(new SalariedEmployee("A", "An"));
                p.addEmployee(new HourlyEmployee("B", "Bình")); p.addEmployee(new SalesEmployee("C", "Chi"));
                System.IO.TextWriter original = Console.Out; var capture = new System.IO.StringWriter();
                try { Console.SetOut(capture); p.displayPayroll(); } finally { Console.SetOut(original); }
                string text = capture.ToString(); truth(text.Contains("Lương tháng:"));
                truth(text.Contains("Giờ vượt:")); truth(text.Contains("Hoa hồng:"));
            });
            test("TC20 - Tham chiếu giữ hành vi lớp dẫn xuất", () =>
            {
                Payroll p = new Payroll("2026-09"); SalesEmployee e = new SalesEmployee("A", "An", "P", 0m, 100m, .1m);
                p.addEmployee(e); e.updateSalesRevenue(200m); equal(20m, p.calculateTotalPayroll());
                truth(p.findEmployee("A").getEmployeeType() == "SalesEmployee");
            });
            Console.WriteLine("Kết quả: {0} PASS, {1} FAIL", passed, failed);
            return failed == 0 ? 0 : 1;
        }
    }
}
