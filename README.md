# OOP – Bài 04: Hệ thống tính lương và thưởng nhân sự

**Họ tên:** Đỗ Văn Chung  
**Mã sinh viên:** 202418855  
**Lớp học phần:** 174373  
**Ngôn ngữ:** C# – .NET 10

## Nội dung

Chương trình tính lương và thưởng cho ba loại nhân sự:
- Nhân viên hưởng lương cố định.
- Nhân viên hưởng lương theo giờ.
- Nhân viên kinh doanh hưởng lương cơ bản và hoa hồng.

Hỗ trợ quản lý bảng lương, tìm nhân sự, tính tổng thu nhập toàn bộ/theo phòng ban và tìm người có thu nhập cao nhất.

## Cấu trúc mã nguồn

| Tệp | Nội dung |
|---|---|
| Employee.cs | Lớp cơ sở trừu tượng, thông tin chung và ba phương thức cộng thưởng nạp chồng. |
| SalariedEmployee.cs | Nhân viên lương cố định. |
| HourlyEmployee.cs | Nhân viên theo giờ, tính tiền làm thêm. |
| SalesEmployee.cs | Nhân viên kinh doanh và cập nhật doanh số. |
| Payroll.cs | Quản lý danh sách và tổng hợp bảng lương. |
| Program.cs | Chương trình chạy dữ liệu mẫu. |
| PayrollTests.cs | Bộ kiểm thử gồm 20 nhóm. |
| OopLab04.csproj | Cấu hình Console project, target .NET 10. |

## Cách chạy

Cài .NET SDK 10, tải hoặc clone repository rồi mở terminal tại thư mục chứa `OopLab04.csproj`.

Chạy chương trình:

```bash
dotnet run --project OopLab04.csproj
```

Chạy kiểm thử:

```bash
dotnet run --project OopLab04.csproj -- --test
```

## Kết quả thực thi

Chương trình đã chạy thành công trên Windows bằng terminal trong VS Code.

| Nhân sự | Thu nhập (VND) |
|---|---:|
| E001 – Nguyễn Minh An | 18.000.000 |
| E002 – Trần Thu Bình | 15.500.000 |
| E003 – Lê Hoàng Chi | 17.500.000 |
| E004 – Phạm Quốc Dũng | 19.000.000 |

- Tổng bảng lương: **70.000.000 VND**.
- Tổng phòng Hỗ trợ: **33.000.000 VND**.
- Thu nhập cao nhất: **E004 – Phạm Quốc Dũng**.
- Bộ kiểm thử: **20 PASS, 0 FAIL**.

## Thiết kế

Chương trình vận dụng đóng gói, constructor ủy quyền, nạp chồng phương thức, kế thừa, ghi đè, đa hình và kết tập.

Dùng `decimal` cho tiền và `List<Employee>` để tổng hợp bằng lời gọi đa hình. Chỉ lưu tổng thưởng; tiền làm thêm được tính từ số giờ và đơn giá. Bảng lương giữ tham chiếu đến nhân sự; việc lưu lịch sử kỳ đã chốt nằm ngoài phạm vi bài tập.
