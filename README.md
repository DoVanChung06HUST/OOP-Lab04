# OOP - Bài 04: Hệ thống tính lương và thưởng nhân sự

**Đỗ Văn Chung - 202418855 - Lớp học phần 174373**

## Chạy chương trình
Cần .NET SDK 8 hoặc SDK mới hơn có hỗ trợ target .NET 8. Mở terminal tại thư mục gốc:

```bash
dotnet run --project source/OopLab04.csproj
dotnet run --project source/OopLab04.csproj -- --test
```

Bộ kiểm thử có 20 nhóm (TC01–TC20), in PASS/FAIL và trả exit code 1 khi có lỗi.
Bản này chưa được biên dịch/chạy C# trong môi trường soạn thảo do chưa có .NET SDK.
Các con số trong báo cáo được ghi là **kết quả mong đợi**, không phải log thực thi.
Sau khi chạy trên máy, có thể ghi kết quả thực tế:

```bash
dotnet run --project source/OopLab04.csproj > demo-output.txt
dotnet run --project source/OopLab04.csproj -- --test > test-output.txt
```

## Cấu trúc
- `source/`: 5 lớp nghiệp vụ, Program, PayrollTests và project C#.
- `report/202418855_DoVanChung_Lab04.tex`: báo cáo có sơ đồ TikZ, không cần ảnh ngoài.
- `report/202418855_DoVanChung_Lab04.pdf`: bản PDF tương ứng.

## Biên dịch báo cáo
Dùng XeLaTeX, có font DejaVu Serif, DejaVu Sans Mono và gói TikZ.

```bash
cd report
xelatex 202418855_DoVanChung_Lab04.tex
xelatex 202418855_DoVanChung_Lab04.tex
```

Sửa lệnh `\newcommand{\GithubURL}{}` đầu file LaTeX thành URL repository của bạn,
ví dụ `\newcommand{\GithubURL}{https://github.com/TEN-TAI-KHOAN/TEN-REPO}`.
Báo cáo tự chuyển từ dòng chờ bổ sung sang liên kết bấm được.
Sau khi chạy bộ kiểm thử, cập nhật mục trạng thái kiểm chứng và bảng kết quả thực tế.

## Các lựa chọn thiết kế
Dùng `decimal`, Employee trừu tượng, constructor ủy quyền, ba overload `addBonus`,
`List<Employee>` và các lời gọi đa hình. Chỉ lưu tổng thưởng; reason được kiểm tra
nhưng không lưu. Không lưu tiền làm thêm vì có thể tính từ số giờ và đơn giá.
Mã nhân sự/phòng ban so sánh không phân biệt hoa thường, chuỗi được cắt khoảng trắng.
Payroll là bảng lương đang xử lý, không phải bản chụp lịch sử: sửa đối tượng nhân sự
sẽ ảnh hưởng các Payroll đang giữ cùng tham chiếu. Không dùng chung các đối tượng
còn thay đổi để lưu nhiều kỳ đã chốt; cần tạo snapshot nếu mở rộng nghiệp vụ.

## Đưa lên GitHub
Tạo repository trống; tải toàn bộ thư mục này lên (không tải bin/obj).
Không có thư viện NuGet ngoài và không có thông tin bí mật trong mã nguồn.
