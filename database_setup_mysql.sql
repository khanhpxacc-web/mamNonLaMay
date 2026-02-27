-- =====================================================
-- SCRIPT TẠO DATABASE CHO TRƯỜNG MẦM NON LÁ MÂY
-- MySQL Database Setup with Sample Data
-- =====================================================

-- 1. TẠO DATABASE
-- -----------------------------------------------------
DROP DATABASE IF EXISTS mamnon_lamay;
CREATE DATABASE mamnon_lamay 
    CHARACTER SET utf8mb4 
    COLLATE utf8mb4_unicode_ci;

USE mamnon_lamay;

-- 2. TẠO BẢNG
-- -----------------------------------------------------

-- Bảng Thông tin trường học
CREATE TABLE SchoolInfo (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(200) NOT NULL DEFAULT 'Trường Mầm Non Lá Mây',
    Address VARCHAR(500),
    Phone VARCHAR(20) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    Website VARCHAR(255),
    FacebookUrl VARCHAR(200),
    YoutubeUrl VARCHAR(200),
    Description TEXT,
    Mission TEXT,
    Vision TEXT,
    EstablishedYear INT DEFAULT 0,
    StudentCount INT DEFAULT 0,
    TeacherCount INT DEFAULT 0,
    ClassCount INT DEFAULT 0,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME NULL,
    IsActive TINYINT(1) DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Bảng Giáo viên
CREATE TABLE Teachers (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    FullName VARCHAR(100) NOT NULL,
    Position VARCHAR(200),
    Bio VARCHAR(500),
    AvatarUrl VARCHAR(255),
    Email VARCHAR(100),
    Phone VARCHAR(20),
    ExperienceYears INT DEFAULT 0,
    Qualifications TEXT,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME NULL,
    IsActive TINYINT(1) DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Bảng Chương trình học
CREATE TABLE EducationPrograms (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Title VARCHAR(100) NOT NULL,
    Description VARCHAR(500),
    Content TEXT,
    ImageUrl VARCHAR(255),
    AgeGroup VARCHAR(50),
    Price DECIMAL(18,2),
    Duration INT DEFAULT 0,
    Schedule VARCHAR(255),
    DisplayOrder INT DEFAULT 0,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME NULL,
    IsActive TINYINT(1) DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Bảng Hoạt động
CREATE TABLE Activities (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Title VARCHAR(200) NOT NULL,
    Summary VARCHAR(500),
    Content TEXT,
    ImageUrl VARCHAR(255),
    ThumbnailUrl VARCHAR(255),
    Category VARCHAR(100),
    EventDate DATETIME NULL,
    Location VARCHAR(255),
    IsFeatured TINYINT(1) DEFAULT 0,
    ViewCount INT DEFAULT 0,
    DisplayOrder INT DEFAULT 0,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME NULL,
    IsActive TINYINT(1) DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Bảng Tin tức
CREATE TABLE News (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Title VARCHAR(200) NOT NULL,
    Slug VARCHAR(200),
    Summary VARCHAR(500),
    Content TEXT,
    ImageUrl VARCHAR(255),
    Author VARCHAR(100),
    Category VARCHAR(100),
    Tags TEXT,
    IsFeatured TINYINT(1) DEFAULT 0,
    ViewCount INT DEFAULT 0,
    PublishedAt DATETIME NULL,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME NULL,
    IsActive TINYINT(1) DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Bảng Thư viện ảnh
CREATE TABLE GalleryItems (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    ImageUrl VARCHAR(255) NOT NULL,
    Title VARCHAR(200),
    Description VARCHAR(500),
    Category VARCHAR(50),
    IsFeatured TINYINT(1) DEFAULT 0,
    DisplayOrder INT DEFAULT 0,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME NULL,
    IsActive TINYINT(1) DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Bảng Phản hồi/Đánh giá
CREATE TABLE Testimonials (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    ParentName VARCHAR(100) NOT NULL,
    AvatarUrl VARCHAR(255),
    Content VARCHAR(1000) NOT NULL,
    Rating INT DEFAULT 5,
    ChildName VARCHAR(100),
    IsFeatured TINYINT(1) DEFAULT 0,
    DisplayOrder INT DEFAULT 0,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME NULL,
    IsActive TINYINT(1) DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Bảng Tin nhắn liên hệ
CREATE TABLE ContactMessages (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    ParentName VARCHAR(100) NOT NULL,
    Phone VARCHAR(20) NOT NULL,
    Email VARCHAR(100),
    ChildName VARCHAR(100),
    ChildAge INT NULL,
    Message VARCHAR(1000) NOT NULL,
    IsRead TINYINT(1) DEFAULT 0,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME NULL,
    IsActive TINYINT(1) DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Bảng Users (cho ASP.NET Identity)
CREATE TABLE AspNetUsers (
    Id VARCHAR(450) NOT NULL PRIMARY KEY,
    UserName VARCHAR(256),
    NormalizedUserName VARCHAR(256),
    Email VARCHAR(256),
    NormalizedEmail VARCHAR(256),
    EmailConfirmed TINYINT(1) NOT NULL DEFAULT 0,
    PasswordHash LONGTEXT,
    SecurityStamp LONGTEXT,
    ConcurrencyStamp LONGTEXT,
    PhoneNumber LONGTEXT,
    PhoneNumberConfirmed TINYINT(1) NOT NULL DEFAULT 0,
    TwoFactorEnabled TINYINT(1) NOT NULL DEFAULT 0,
    LockoutEnd DATETIME(6) NULL,
    LockoutEnabled TINYINT(1) NOT NULL DEFAULT 1,
    AccessFailedCount INT NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Bảng Roles (cho ASP.NET Identity)
CREATE TABLE AspNetRoles (
    Id VARCHAR(450) NOT NULL PRIMARY KEY,
    Name VARCHAR(256),
    NormalizedName VARCHAR(256),
    ConcurrencyStamp LONGTEXT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Bảng UserRoles (cho ASP.NET Identity)
CREATE TABLE AspNetUserRoles (
    UserId VARCHAR(450) NOT NULL,
    RoleId VARCHAR(450) NOT NULL,
    PRIMARY KEY (UserId, RoleId),
    FOREIGN KEY (UserId) REFERENCES AspNetUsers(Id) ON DELETE CASCADE,
    FOREIGN KEY (RoleId) REFERENCES AspNetRoles(Id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Bảng UserClaims (cho ASP.NET Identity)
CREATE TABLE AspNetUserClaims (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    UserId VARCHAR(450) NOT NULL,
    ClaimType LONGTEXT,
    ClaimValue LONGTEXT,
    FOREIGN KEY (UserId) REFERENCES AspNetUsers(Id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Bảng RoleClaims (cho ASP.NET Identity)
CREATE TABLE AspNetRoleClaims (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    RoleId VARCHAR(450) NOT NULL,
    ClaimType LONGTEXT,
    ClaimValue LONGTEXT,
    FOREIGN KEY (RoleId) REFERENCES AspNetRoles(Id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Bảng UserLogins (cho ASP.NET Identity)
CREATE TABLE AspNetUserLogins (
    LoginProvider VARCHAR(450) NOT NULL,
    ProviderKey VARCHAR(450) NOT NULL,
    ProviderDisplayName LONGTEXT,
    UserId VARCHAR(450) NOT NULL,
    PRIMARY KEY (LoginProvider, ProviderKey),
    FOREIGN KEY (UserId) REFERENCES AspNetUsers(Id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Bảng UserTokens (cho ASP.NET Identity)
CREATE TABLE AspNetUserTokens (
    UserId VARCHAR(450) NOT NULL,
    LoginProvider VARCHAR(450) NOT NULL,
    Name VARCHAR(450) NOT NULL,
    Value LONGTEXT,
    PRIMARY KEY (UserId, LoginProvider, Name),
    FOREIGN KEY (UserId) REFERENCES AspNetUsers(Id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 3. THÊM DỮ LIỆU MẪU
-- -----------------------------------------------------

-- Dữ liệu mẫu SchoolInfo
INSERT INTO SchoolInfo (Name, Address, Phone, Email, Website, FacebookUrl, YoutubeUrl, Description, Mission, Vision, EstablishedYear, StudentCount, TeacherCount, ClassCount, CreatedAt, IsActive)
VALUES (
    'Trường Mầm Non Lá Mây',
    '170 Mạc Quyết, Anh Dũng, Dương Kinh, Hải Phòng',
    '0964106259',
    'info@mamnonlamay.edu.vn',
    'https://mamnonlamay.edu.vn',
    'https://facebook.com/mamnonlamay',
    'https://youtube.com/mamnonlamay',
    'Trường Mầm Non Lá Mây - Nơi ươm mầm tương lai cho trẻ em Việt Nam. Chúng tôi cam kết mang đến môi trường học tập an toàn, thân thiện và sáng tạo.',
    'Xây dựng môi trường giáo dục mầm non chất lượng cao, giúp trẻ phát triển toàn diện về thể chất, trí tuệ, tình cảm và thẩm mỹ.',
    'Trở thành trường mầm non hàng đầu với chương trình giáo dục tiên tiến, đội ngũ giáo viên chuyên nghiệp và cơ sở vật chất hiện đại.',
    2010,
    350,
    45,
    15,
    NOW(),
    1
);

-- Dữ liệu mẫu Teachers
INSERT INTO Teachers (FullName, Position, Bio, AvatarUrl, Email, Phone, ExperienceYears, Qualifications, CreatedAt, IsActive) VALUES
('Cô Nguyễn Thị Lan', 'Hiệu trưởng', 'Hơn 20 năm kinh nghiệm trong lĩnh vực giáo dục mầm non, tốt nghiệp Đại học Sư phạm Hà Nội.', 'https://images.unsplash.com/photo-1494790108377-be9c29b29330?w=400&h=400&fit=crop', 'lan.nt@mamnon.edu.vn', '0912345678', 20, 'Thạc sĩ Giáo dục Mầm non', NOW(), 1),
('Cô Trần Thị Hoa', 'Phó Hiệu trưởng', '15 năm kinh nghiệm giảng dạy, chuyên gia về phương pháp Montessori.', 'https://images.unsplash.com/photo-1438761681033-6461ffad8d80?w=400&h=400&fit=crop', 'hoa.tt@mamnon.edu.vn', '0912345679', 15, 'Cử nhân Sư phạm Mầm non', NOW(), 1),
('Cô Lê Thị Minh', 'Giáo viên chủ nhiệm', '10 năm kinh nghiệm, yêu thương và tận tâm với từng học sinh.', 'https://images.unsplash.com/photo-1534528741775-53994a69daeb?w=400&h=400&fit=crop', 'minh.lt@mamnon.edu.vn', '0912345680', 10, 'Cử nhân Sư phạm Mầm non', NOW(), 1),
('Cô Phạm Thị Hồng', 'Giáo viên âm nhạc', 'Chuyên gia âm nhạc thiếu nhi, giúp trẻ phát triển khiếu năng khiếu từ nhỏ.', 'https://images.unsplash.com/photo-1544005313-94ddf0286df2?w=400&h=400&fit=crop', 'hong.pt@mamnon.edu.vn', '0912345681', 8, 'Cử nhân Âm nhạc', NOW(), 1);

-- Dữ liệu mẫu EducationPrograms
INSERT INTO EducationPrograms (Title, Description, Content, ImageUrl, AgeGroup, Price, Duration, Schedule, DisplayOrder, CreatedAt, IsActive) VALUES
('Chương trình Nhà trẻ (1-2 tuổi)', 'Chương trình chăm sóc và giáo dục trẻ từ 1-2 tuổi với môi trường an toàn, ấm áp.', 'Chương trình tập trung vào:\n- Phát triển vận động cơ bản\n- Ngôn ngữ và giao tiếp\n- Cảm xúc và xã hội\n- Thói quen tốt trong sinh hoạt hàng ngày', 'https://images.unsplash.com/photo-1503454537195-1dcabb73ffb9?w=600&h=400&fit=crop', '1-2 tuổi', 3500000, 5, 'Thứ 2 - Thứ 6 (7:00 - 16:30)', 1, NOW(), 1),
('Chương trình Mẫu giáo nhỡ (3-4 tuổi)', 'Giúp trẻ phát triển toàn diện qua các hoạt động vui chơi và học tập.', 'Chương trình bao gồm:\n- Nhận biết và tư duy\n- Ngôn ngữ và tiếng Anh\n- Nghệ thuật và sáng tạo\n- Vận động và thể thao', 'https://images.unsplash.com/photo-1587654780291-39c9404d746b?w=600&h=400&fit=crop', '3-4 tuổi', 3200000, 5, 'Thứ 2 - Thứ 6 (7:00 - 16:30)', 2, NOW(), 1),
('Chương trình Mẫu giáo lớn (5-6 tuổi)', 'Chuẩn bị kỹ năng cho trẻ vào lớp 1 với chương trình học bài bản.', 'Nội dung học tập:\n- Chuẩn bị chữ cái và số đếm\n- Kỹ năng tự phục vụ\n- Giao tiếp và kỹ năng xã hội\n- Tiếng Anh cơ bản', 'https://images.unsplash.com/photo-1567057419565-4349c49d8a04?w=600&h=400&fit=crop', '5-6 tuổi', 3000000, 5, 'Thứ 2 - Thứ 6 (7:00 - 16:30)', 3, NOW(), 1),
('Chương trình Năng khiếu', 'Các lớp học năng khiếu: âm nhạc, múa, vẽ, võ thuật.', 'Các môn học:\n- Piano và nhạc cụ\n- Múa ballet và hiện đại\n- Vẽ sáng tạo\n- Võ Taekwondo', 'https://images.unsplash.com/photo-1596464716127-f2a82984de30?w=600&h=400&fit=crop', '3-6 tuổi', 1500000, 2, 'Thứ 7 (8:00 - 11:00)', 4, NOW(), 1);

-- Dữ liệu mẫu Activities
INSERT INTO Activities (Title, Summary, Content, ImageUrl, ThumbnailUrl, Category, EventDate, Location, IsFeatured, ViewCount, DisplayOrder, CreatedAt, IsActive) VALUES
('Ngày hội Gia đình 2024', 'Sự kiện kết nối gia đình và nhà trường với nhiều hoạt động vui chơi hấp dẫn', 'Ngày hội Gia đình là sự kiện thường niên của trường nhằm tạo cơ hội cho phụ huynh và trẻ cùng tham gia các hoạt động vui chơi, gắn kết tình cảm gia đình.', 'https://images.unsplash.com/photo-1541692641319-981cc79ee10a?w=800', 'https://images.unsplash.com/photo-1541692641319-981cc79ee10a?w=400', 'Sự kiện', '2024-06-01 08:00:00', 'Sân trường Lá Mây', 1, 156, 1, NOW(), 1),
('Chuyến dã ngoại Thảo Cầm Viên', 'Trẻ được khám phá thế giới động vật và thiên nhiên', 'Chuyến dã ngoại đến Thảo Cầm Viên là một phần của chương trình học ngoài trời, giúp trẻ tiếp xúc với thiên nhiên.', 'https://images.unsplash.com/photo-1503454537195-1dcabb73ffb9?w=800', 'https://images.unsplash.com/photo-1503454537195-1dcabb73ffb9?w=400', 'Ngoại khóa', '2024-05-15 08:00:00', 'Thảo Cầm Viên', 1, 203, 2, NOW(), 1),
('Lễ Tổng kết năm học 2023-2024', 'Gala tổng kết với nhiều tiết mục văn nghệ và trao giải thưởng', 'Lễ Tổng kết năm học là dịp để nhìn lại những thành tựu của trẻ trong năm học vừa qua.', 'https://images.unsplash.com/photo-1596464716127-f2a82984de30?w=800', 'https://images.unsplash.com/photo-1596464716127-f2a82984de30?w=400', 'Lễ hội', '2024-05-25 18:00:00', 'Hội trường trường', 1, 312, 3, NOW(), 1),
('Cuộc thi Vẽ tranh "Mái nhà của em"', 'Phát huy sáng tạo và tình yêu gia đình qua nét vẽ', 'Cuộc thi vẽ tranh với chủ đề "Mái nhà của em" đã thu hút hơn 200 bé tham gia.', 'https://images.unsplash.com/photo-1567057419565-4349c49d8a04?w=800', 'https://images.unsplash.com/photo-1567057419565-4349c49d8a04?w=400', 'Học tập', '2024-04-20 09:00:00', 'Phòng học và hành lang trường', 0, 178, 4, NOW(), 1),
('Ngày hội Thể thao 2024', 'Các trò chơi vận động vui nhộn phát triển thể chất cho trẻ', 'Ngày hội Thể thao với các môn: chạy tiếp sức, nhảy bao bố, kéo co, bắn bóng rổ.', 'https://images.unsplash.com/photo-1580582932707-520aed937b7b?w=800', 'https://images.unsplash.com/photo-1580582932707-520aed937b7b?w=400', 'Thể thao', '2024-03-15 08:00:00', 'Sân thể thao trường', 1, 245, 5, NOW(), 1),
('Workshop Làm bánh Trung Thu', 'Trẻ tự tay làm bánh Trung Thu cùng bố mẹ', 'Workshop làm bánh Trung Thu là hoạt động truyền thống mỗi dịp Tết Trung Thu.', 'https://images.unsplash.com/photo-1574966739987-65e38db0f7ce?w=800', 'https://images.unsplash.com/photo-1574966739987-65e38db0f7ce?w=400', 'Vui chơi', '2024-09-10 15:00:00', 'Phòng chức năng', 1, 189, 6, NOW(), 1);

-- Dữ liệu mẫu News
INSERT INTO News (Title, Slug, Summary, Content, ImageUrl, Author, Category, Tags, IsFeatured, ViewCount, PublishedAt, CreatedAt, IsActive) VALUES
('Thông báo tuyển sinh năm học 2024-2025', 'thong-bao-tuyen-sinh-nam-hoc-2024-2025', 'Trường Mầm Non Lá Mây thông báo tuyển sinh các lớp mầm non cho năm học mới với nhiều ưu đãi hấp dẫn.', '<p>Trường Mầm Non Lá Mây trân trọng thông báo tuyển sinh năm học 2024-2025...</p>', 'https://images.unsplash.com/photo-1503676260728-1c00da094a0b?w=800', 'Ban Giám hiệu', 'Thông báo', 'tuyển sinh, năm học mới, ưu đãi', 1, 1256, '2024-03-01 08:00:00', NOW(), 1),
('5 cách giúp trẻ thích đi học mầm non', '5-cach-giup-tre-thich-di-hoc-mam-non', 'Nhiều phụ huynh lo lắng khi con không thích đi học. Dưới đây là 5 cách giúp trẻ yêu thích việc đến trường.', '<p>Giai đoạn đầu đi học mầm non là thử thách lớn với nhiều trẻ...</p>', 'https://images.unsplash.com/photo-1587654780291-39c9404d746b?w=800', 'Cô Nguyễn Thị Lan', 'Kiến thức', 'tâm lý trẻ, đi học, gợi ý', 1, 892, '2024-02-15 10:00:00', NOW(), 1),
('Trường đạt chứng nhận Chất lượng Quốc gia', 'truong-dat-chung-nhan-chat-luong-quoc-gia', 'Trường Mầm Non Lá Mây vinh dự đạt chứng nhận Chất lượng Quốc gia năm 2024.', '<p>Chúng tôi vui mừng thông báo Trường Mầm Non Lá Mây đã chính thức đạt chứng nhận...</p>', 'https://images.unsplash.com/photo-1580582932707-520aed937b7b?w=800', 'Ban Giám hiệu', 'Tin tức', 'chứng nhận, chất lượng, thành tích', 1, 1567, '2024-01-20 09:00:00', NOW(), 1),
('Workshop: Dinh dưỡng cho trẻ mầm non', 'workshop-dinh-duong-cho-tre-mam-non', 'Chuyên gia dinh dưỡng chia sẻ về chế độ ăn uống khoa học cho trẻ 1-6 tuổi.', '<p>Trường phối hợp với Viện Dinh dưỡng Quốc gia tổ chức workshop...</p>', 'https://images.unsplash.com/photo-1490645935967-10de6ba17061?w=800', 'TS. Nguyễn Văn A', 'Sự kiện', 'dinh dưỡng, workshop, sức khỏe', 0, 723, '2024-02-28 14:00:00', NOW(), 1),
('Khai giảng lớp Tiếng Anh mầm non', 'khai-giang-lop-tieng-anh-mam-non', 'Chương trình Tiếng Anh chuẩn Cambridge dành cho trẻ 4-6 tuổi chính thức khai giảng.', '<p>Trường chính thức khai giảng chương trình Tiếng Anh mầm non...</p>', 'https://images.unsplash.com/photo-1544717305-2782549b5136?w=800', 'Cô Trần Thị Hoa', 'Thông báo', 'tiếng anh, khai giảng, ngoại ngữ', 0, 634, '2024-03-05 08:00:00', NOW(), 1),
('Phương pháp Montessori tại trường', 'phuong-phap-montessori-tai-truong', 'Tìm hiểu về phương pháp giáo dục Montessori được áp dụng tại Lá Mây.', '<p>Phương pháp Montessori là phương pháp giáo dục nổi tiếng thế giới...</p>', 'https://images.unsplash.com/photo-1587654780291-39c9404d746b?w=800', 'Cô Trần Thị Hoa', 'Kiến thức', 'montessori, phương pháp, giáo dục', 1, 1089, '2024-01-10 10:00:00', NOW(), 1);

-- Dữ liệu mẫu GalleryItems
INSERT INTO GalleryItems (ImageUrl, Title, Description, Category, IsFeatured, DisplayOrder, CreatedAt, IsActive) VALUES
('https://images.unsplash.com/photo-1587654780291-39c9404d746b?w=600', 'Hoạt động ngoài trời', 'Trẻ vui chơi trong khuôn viên trường', 'Hoạt động', 1, 1, NOW(), 1),
('https://images.unsplash.com/photo-1567057419565-4349c49d8a04?w=600', 'Giờ học vẽ', 'Trẻ sáng tạo với màu sắc', 'Lớp học', 1, 2, NOW(), 1),
('https://images.unsplash.com/photo-1596464716127-f2a82984de30?w=600', 'Lễ Tết Trung Thu', 'Trẻ tham gia phá cỗ Trung Thu', 'Sự kiện', 1, 3, NOW(), 1),
('https://images.unsplash.com/photo-1580582932707-520aed937b7b?w=600', 'Hoạt động thể chất', 'Trẻ tập thể dục buổi sáng', 'Hoạt động', 1, 4, NOW(), 1),
('https://images.unsplash.com/photo-1574966739987-65e38db0f7ce?w=600', 'Giờ ăn bán trú', 'Trẻ ăn uống đầy đủ dinh dưỡng', 'Lớp học', 1, 5, NOW(), 1),
('https://images.unsplash.com/photo-1541692641319-981cc79ee10a?w=600', 'Ngày Nhà Giáo Việt Nam', 'Tri ân thầy cô 20/11', 'Sự kiện', 1, 6, NOW(), 1);

-- Dữ liệu mẫu Testimonials
INSERT INTO Testimonials (ParentName, AvatarUrl, Content, Rating, ChildName, IsFeatured, DisplayOrder, CreatedAt, IsActive) VALUES
('Chị Nguyễn Thị An', NULL, 'Con tôi đã học ở đây được 2 năm và phát triển rất tốt. Các cô giáo rất tận tâm và yêu thương trẻ.', 5, 'Bé Minh Anh', 1, 1, NOW(), 1),
('Anh Trần Văn Bình', NULL, 'Môi trường học tập sạch sẽ, an toàn. Chương trình học phong phú giúp con tôi hào hứng đi học mỗi ngày.', 5, 'Bé Gia Bảo', 1, 2, NOW(), 1),
('Chị Lê Thị Cẩm', NULL, 'Tôi rất hài lòng với chất lượng giáo dục tại đây. Con tôi đã học được nhiều kỹ năng tự lập.', 5, 'Bé Khánh Ly', 1, 3, NOW(), 1);

-- Dữ liệu mẫu AspNetRoles (Admin role)
INSERT INTO AspNetRoles (Id, Name, NormalizedName) VALUES
('1', 'Admin', 'ADMIN');

-- LƯU Ý: Admin user sẽ được tạo tự động khi chạy ứng dụng lần đầu
-- Thông tin đăng nhập: admin@mamnonlamay.edu.vn / Admin@123

-- 4. HOÀN TẤT
-- -----------------------------------------------------
SELECT 'Database mamnon_lamay đã được tạo thành công!' AS Message;
SELECT CONCAT('Số lượng bảng: ', (SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = 'mamnon_lamay')) AS Info;
