-- Xóa user admin cũ (nếu có) để app tự tạo mới
DELETE FROM AspNetUserRoles WHERE UserId IN (SELECT Id FROM AspNetUsers WHERE Email = 'admin@mamnonlamay.edu.vn');
DELETE FROM AspNetUsers WHERE Email = 'admin@mamnonlamay.edu.vn';

-- Kiểm tra kết quả
SELECT 'Đã xóa user admin cũ. Giờ hãy chạy app để tự động tạo admin mới.' AS Message;
SELECT * FROM AspNetUsers WHERE Email = 'admin@mamnonlamay.edu.vn';
