# Trường Mầm Non Lá Mây - MamNonApp

Một trang web giới thiệu trường mầm non hoàn chỉnh sử dụng .NET 9 MVC với các design patterns chuẩn và tối ưu SEO.

## 🎯 Tính Năng

### Các Trang
- **Trang chủ** - Tổng quan về trường, chương trình học, đội ngũ giáo viên, thư viện ảnh
- **Giới thiệu** - Lịch sử, sứ mệnh, tầm nhìn, đội ngũ giáo viên
- **Chương trình học** - Các khóa học theo độ tuổi (1-2 tuổi, 3-4 tuổi, 5-6 tuổi)
- **Thư viện ảnh** - Hình ảnh hoạt động của trường
- **Liên hệ** - Form liên hệ, bản đồ, thông tin liên hệ

### Chuẩn SEO
- ✅ Meta tags (title, description, keywords) đầy đủ
- ✅ Open Graph tags (Facebook, Zalo sharing)
- ✅ Twitter Card tags
- ✅ Structured Data (JSON-LD)
- ✅ Sitemap.xml động
- ✅ Robots.txt
- ✅ URL thân thiện (Việt Nam hóa)
- ✅ Canonical URLs
- ✅ Responsive design

## 🏗️ Kiến Trúc & Design Patterns

### 1. Repository Pattern
```
Interfaces/IRepository.cs         - Generic Repository Interface
Repositories/Repository.cs        - Repository Implementation
Repositories/UnitOfWork.cs        - Unit of Work Pattern
```

### 2. Service Layer Pattern
```
Interfaces/ITeacherService.cs     - Teacher Service Interface
Interfaces/IProgramService.cs     - Program Service Interface
Interfaces/IGalleryService.cs     - Gallery Service Interface
Services/TeacherService.cs        - Implementation
Services/ProgramService.cs        - Implementation
```

### 3. Dependency Injection
Tất cả services được đăng ký trong `Program.cs` với Scoped lifetime:
```csharp
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ITeacherService, TeacherService>();
// ... etc
```

### 4. MVC Pattern
```
Controllers/      - Xử lý request
Models/           - Entities, DTOs, ViewModels
Views/            - Razor views
wwwroot/          - Static files
```

### 5. Data Transfer Objects (DTOs)
```
Models/DTOs/      - Data Transfer Objects
Models/ViewModels/ - View-specific models
```

## 📁 Cấu Trúc Thư Mục

```
MamNonApp/
├── Controllers/
│   ├── HomeController.cs
│   ├── ProgramsController.cs
│   ├── GalleryController.cs
│   ├── ContactController.cs
│   ├── SitemapController.cs
│   └── RobotsController.cs
├── Interfaces/
│   ├── IRepository.cs
│   ├── IUnitOfWork.cs
│   ├── ITeacherService.cs
│   ├── IProgramService.cs
│   ├── IGalleryService.cs
│   ├── IContactService.cs
│   ├── ISchoolInfoService.cs
│   └── ISeoService.cs
├── Models/
│   ├── Entities/
│   │   ├── BaseEntity.cs
│   │   ├── Teacher.cs
│   │   ├── EducationProgram.cs
│   │   ├── GalleryItem.cs
│   │   ├── ContactMessage.cs
│   │   ├── SchoolInfo.cs
│   │   └── Testimonial.cs
│   ├── DTOs/
│   └── ViewModels/
│       ├── SeoViewModel.cs
│       ├── HomeViewModel.cs
│       ├── AboutViewModel.cs
│       ├── ProgramsViewModel.cs
│       └── ContactViewModel.cs
├── Repositories/
│   ├── Repository.cs
│   ├── UnitOfWork.cs
│   └── InMemoryDataStore.cs
├── Services/
│   ├── TeacherService.cs
│   ├── ProgramService.cs
│   ├── GalleryService.cs
│   ├── ContactService.cs
│   ├── SchoolInfoService.cs
│   └── SeoService.cs
├── Views/
│   ├── Shared/
│   │   ├── _Layout.cshtml
│   │   └── _SeoMeta.cshtml
│   ├── Home/
│   │   ├── Index.cshtml
│   │   └── About.cshtml
│   ├── Programs/
│   │   ├── Index.cshtml
│   │   └── Detail.cshtml
│   ├── Gallery/
│   │   └── Index.cshtml
│   └── Contact/
│       └── Index.cshtml
├── wwwroot/
│   ├── css/site.css
│   ├── js/site.js
│   └── images/
└── Program.cs
```

## 🚀 Hướng Dẫn Chạy

### Yêu Cầu
- .NET 9 SDK
- Visual Studio 2022 hoặc VS Code

### Chạy Project
```bash
# 1. Di chuyển vào thư mục project
cd MamNonApp

# 2. Restore packages
dotnet restore

# 3. Build project
dotnet build

# 4. Run project
dotnet run

# Hoặc chạy với URL cụ thể
dotnet run --urls "http://localhost:5000"
```

### Truy Cập
- Trang chủ: `http://localhost:5000`
- Giới thiệu: `http://localhost:5000/gioi-thieu`
- Chương trình học: `http://localhost:5000/chuong-trinh-hoc`
- Thư viện ảnh: `http://localhost:5000/thu-vien-anh`
- Liên hệ: `http://localhost:5000/lien-he`
- Sitemap: `http://localhost:5000/sitemap.xml`
- Robots: `http://localhost:5000/robots.txt`

## 🔧 Tùy Chỉnh

### Thêm Database (Entity Framework Core)
1. Cài đặt packages:
```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
```

2. Tạo DbContext và cấu hình trong `Program.cs`

3. Thay thế `InMemoryDataStore` bằng Entity Framework

### Thay Đổi Thông Tin Trường
Sửa file `Repositories/InMemoryDataStore.cs` - method `SeedData()`

### Thay Đổi Màu Sắc
Sửa CSS variables trong `wwwroot/css/site.css`:
```css
:root {
    --primary-color: #ffc107;  /* Màu chính */
    --secondary-color: #0d6efd; /* Màu phụ */
}
```

## 📱 Responsive Breakpoints

- Desktop: > 992px
- Tablet: 768px - 991px
- Mobile: < 767px

## 🔒 Bảo Mật

- Anti-forgery token cho forms
- Input validation
- HTTPS redirection (production)
- Response compression & caching

## 📄 License

MIT License - Copyright (c) 2024 MamNonApp
