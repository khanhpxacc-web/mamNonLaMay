using System;
using System.Collections.Generic;
using MamNonApp.Models.Entities;

namespace MamNonApp.Repositories
{
    /// <summary>
    /// In-memory data store cho demo
    /// </summary>
    public static class InMemoryDataStore
    {
        public static List<Teacher> Teachers { get; } = new();
        public static List<EducationProgram> Programs { get; } = new();
        public static List<GalleryItem> GalleryItems { get; } = new();
        public static List<ContactMessage> ContactMessages { get; } = new();
        public static List<SchoolInfo> SchoolInfo { get; } = new();
        public static List<Testimonial> Testimonials { get; } = new();
        public static List<Activity> Activities { get; } = new();
        public static List<News> News { get; } = new();

        static InMemoryDataStore()
        {
            SeedData();
        }

        private static void SeedData()
        {
            // Seed School Info
            SchoolInfo.Add(new SchoolInfo
            {
                Id = 1,
                Name = "Trường Mầm Non Lá Mây",
                Address = "170 Mạc Quyết, Anh Dũng, Dương Kinh, Hải Phòng",
                Phone = "0964106259",
                Email = "info@mamnonlamay.edu.vn",
                Website = "https://mamnonlamay.edu.vn",
                FacebookUrl = "https://facebook.com/mamnonlamay",
                Description = "Trường Mầm Non Lá Mây - Nơi ươm mầm tương lai cho trẻ em Việt Nam. Chúng tôi cam kết mang đến môi trường học tập an toàn, thân thiện và sáng tạo.",
                Mission = "Xây dựng môi trường giáo dục mầm non chất lượng cao, giúp trẻ phát triển toàn diện về thể chất, trí tuệ, tình cảm và thẩm mỹ.",
                Vision = "Trở thành trường mầm non hàng đầu với chương trình giáo dục tiên tiến, đội ngũ giáo viên chuyên nghiệp và cơ sở vật chất hiện đại.",
                EstablishedYear = 2010,
                StudentCount = 350,
                TeacherCount = 45,
                ClassCount = 15
            });

            // Seed Teachers
            Teachers.AddRange(new[]
            {
                new Teacher
                {
                    Id = 1,
                    FullName = "Cô Nguyễn Thị Lan",
                    Position = "Hiệu trưởng",
                    Bio = "Hơn 20 năm kinh nghiệm trong lĩnh vực giáo dục mầm non, tốt nghiệp Đại học Sư phạm Hà Nội.",
                    AvatarUrl = "https://images.unsplash.com/photo-1494790108377-be9c29b29330?w=400&h=400&fit=crop",
                    Email = "lan.nt@mamnon.edu.vn",
                    ExperienceYears = 20,
                    Qualifications = "Thạc sĩ Giáo dục Mầm non"
                },
                new Teacher
                {
                    Id = 2,
                    FullName = "Cô Trần Thị Hoa",
                    Position = "Phó Hiệu trưởng",
                    Bio = "15 năm kinh nghiệm giảng dạy, chuyên gia về phương pháp Montessori.",
                    AvatarUrl = "https://images.unsplash.com/photo-1438761681033-6461ffad8d80?w=400&h=400&fit=crop",
                    Email = "hoa.tt@mamnon.edu.vn",
                    ExperienceYears = 15,
                    Qualifications = "Cử nhân Sư phạm Mầm non"
                },
                new Teacher
                {
                    Id = 3,
                    FullName = "Cô Lê Thị Minh",
                    Position = "Giáo viên chủ nhiệm",
                    Bio = "10 năm kinh nghiệm, yêu thương và tận tâm với từng học sinh.",
                    AvatarUrl = "https://images.unsplash.com/photo-1534528741775-53994a69daeb?w=400&h=400&fit=crop",
                    Email = "minh.lt@mamnon.edu.vn",
                    ExperienceYears = 10,
                    Qualifications = "Cử nhân Sư phạm Mầm non"
                },
                new Teacher
                {
                    Id = 4,
                    FullName = "Cô Phạm Thị Hồng",
                    Position = "Giáo viên âm nhạc",
                    Bio = "Chuyên gia âm nhạc thiếu nhi, giúp trẻ phát triển khiếu năng khiếu từ nhỏ.",
                    AvatarUrl = "https://images.unsplash.com/photo-1544005313-94ddf0286df2?w=400&h=400&fit=crop",
                    Email = "hong.pt@mamnon.edu.vn",
                    ExperienceYears = 8,
                    Qualifications = "Cử nhân Âm nhạc"
                }
            });

            // Seed Programs
            Programs.AddRange(new[]
            {
                new EducationProgram
                {
                    Id = 1,
                    Title = "Chương trình Nhà trẻ (1-2 tuổi)",
                    Description = "Chương trình chăm sóc và giáo dục trẻ từ 1-2 tuổi với môi trường an toàn, ấm áp.",
                    Content = "Chương trình tập trung vào:\n- Phát triển vận động cơ bản\n- Ngôn ngữ và giao tiếp\n- Cảm xúc và xã hội\n- Thói quen tốt trong sinh hoạt hàng ngày",
                    ImageUrl = "https://images.unsplash.com/photo-1503454537195-1dcabb73ffb9?w=600&h=400&fit=crop",
                    AgeGroup = "1-2 tuổi",
                    Price = 3500000,
                    Duration = 5,
                    Schedule = "Thứ 2 - Thứ 6 (7:00 - 16:30)",
                    DisplayOrder = 1
                },
                new EducationProgram
                {
                    Id = 2,
                    Title = "Chương trình Mẫu giáo nhỡ (3-4 tuổi)",
                    Description = "Giúp trẻ phát triển toàn diện qua các hoạt động vui chơi và học tập.",
                    Content = "Chương trình bao gồm:\n- Nhận biết và tư duy\n- Ngôn ngữ và tiếng Anh\n- Nghệ thuật và sáng tạo\n- Vận động và thể thao",
                    ImageUrl = "https://images.unsplash.com/photo-1587654780291-39c9404d746b?w=600&h=400&fit=crop",
                    AgeGroup = "3-4 tuổi",
                    Price = 3200000,
                    Duration = 5,
                    Schedule = "Thứ 2 - Thứ 6 (7:00 - 16:30)",
                    DisplayOrder = 2
                },
                new EducationProgram
                {
                    Id = 3,
                    Title = "Chương trình Mẫu giáo lớn (5-6 tuổi)",
                    Description = "Chuẩn bị kỹ năng cho trẻ vào lớp 1 với chương trình học bài bản.",
                    Content = "Nội dung học tập:\n- Chuẩn bị chữ cái và số đếm\n- Kỹ năng tự phục vụ\n- Giao tiếp và kỹ năng xã hội\n- Tiếng Anh cơ bản",
                    ImageUrl = "https://images.unsplash.com/photo-1567057419565-4349c49d8a04?w=600&h=400&fit=crop",
                    AgeGroup = "5-6 tuổi",
                    Price = 3000000,
                    Duration = 5,
                    Schedule = "Thứ 2 - Thứ 6 (7:00 - 16:30)",
                    DisplayOrder = 3
                },
                new EducationProgram
                {
                    Id = 4,
                    Title = "Chương trình Năng khiếu",
                    Description = "Các lớp học năng khiếu: âm nhạc, múa, vẽ, võ thuật.",
                    Content = "Các môn học:\n- Piano và nhạc cụ\n- Múa ballet và hiện đại\n- Vẽ sáng tạo\n- Võ Taekwondo",
                    ImageUrl = "https://images.unsplash.com/photo-1596464716127-f2a82984de30?w=600&h=400&fit=crop",
                    AgeGroup = "3-6 tuổi",
                    Price = 1500000,
                    Duration = 2,
                    Schedule = "Thứ 7 (8:00 - 11:00)",
                    DisplayOrder = 4
                }
            });

            // Seed Gallery Items
            GalleryItems.AddRange(new[]
            {
                new GalleryItem
                {
                    Id = 1,
                    ImageUrl = "https://images.unsplash.com/photo-1587654780291-39c9404d746b?w=600",
                    Title = "Hoạt động ngoài trời",
                    Description = "Trẻ vui chơi trong khuôn viên trường",
                    Category = "Hoạt động",
                    IsFeatured = true,
                    DisplayOrder = 1
                },
                new GalleryItem
                {
                    Id = 2,
                    ImageUrl = "https://images.unsplash.com/photo-1567057419565-4349c49d8a04?w=600",
                    Title = "Giờ học vẽ",
                    Description = "Trẻ sáng tạo với màu sắc",
                    Category = "Lớp học",
                    IsFeatured = true,
                    DisplayOrder = 2
                },
                new GalleryItem
                {
                    Id = 3,
                    ImageUrl = "https://images.unsplash.com/photo-1596464716127-f2a82984de30?w=600",
                    Title = "Lễ Tết Trung Thu",
                    Description = "Trẻ tham gia phá cỗ Trung Thu",
                    Category = "Sự kiện",
                    IsFeatured = true,
                    DisplayOrder = 3
                },
                new GalleryItem
                {
                    Id = 4,
                    ImageUrl = "https://images.unsplash.com/photo-1580582932707-520aed937b7b?w=600",
                    Title = "Hoạt động thể chất",
                    Description = "Trẻ tập thể dục buổi sáng",
                    Category = "Hoạt động",
                    IsFeatured = true,
                    DisplayOrder = 4
                },
                new GalleryItem
                {
                    Id = 5,
                    ImageUrl = "https://images.unsplash.com/photo-1574966739987-65e38db0f7ce?w=600",
                    Title = "Giờ ăn bán trú",
                    Description = "Trẻ ăn uống đầy đủ dinh dưỡng",
                    Category = "Lớp học",
                    IsFeatured = true,
                    DisplayOrder = 5
                },
                new GalleryItem
                {
                    Id = 6,
                    ImageUrl = "https://images.unsplash.com/photo-1541692641319-981cc79ee10a?w=600",
                    Title = "Ngày Nhà Giáo Việt Nam",
                    Description = "Tri ân thầy cô 20/11",
                    Category = "Sự kiện",
                    IsFeatured = true,
                    DisplayOrder = 6
                }
            });

            // Seed Testimonials
            Testimonials.AddRange(new[]
            {
                new Testimonial
                {
                    Id = 1,
                    ParentName = "Chị Nguyễn Thị An",
                    Content = "Con tôi đã học ở đây được 2 năm và phát triển rất tốt. Các cô giáo rất tận tâm và yêu thương trẻ.",
                    Rating = 5,
                    ChildName = "Bé Minh Anh",
                    IsFeatured = true,
                    DisplayOrder = 1
                },
                new Testimonial
                {
                    Id = 2,
                    ParentName = "Anh Trần Văn Bình",
                    Content = "Môi trường học tập sạch sẽ, an toàn. Chương trình học phong phú giúp con tôi hào hứng đi học mỗi ngày.",
                    Rating = 5,
                    ChildName = "Bé Gia Bảo",
                    IsFeatured = true,
                    DisplayOrder = 2
                },
                new Testimonial
                {
                    Id = 3,
                    ParentName = "Chị Lê Thị Cẩm",
                    Content = "Tôi rất hài lòng với chất lượng giáo dục tại đây. Con tôi đã học được nhiều kỹ năng tự lập.",
                    Rating = 5,
                    ChildName = "Bé Khánh Ly",
                    IsFeatured = true,
                    DisplayOrder = 3
                }
            });

            // Seed Activities
            Activities.AddRange(new[]
            {
                new Activity
                {
                    Id = 1,
                    Title = "Ngày hội Gia đình 2024",
                    Summary = "Sự kiện kết nối gia đình và nhà trường với nhiều hoạt động vui chơi hấp dẫn",
                    Content = "Ngày hội Gia đình là sự kiện thường niên của trường nhằm tạo cơ hội cho phụ huynh và trẻ cùng tham gia các hoạt động vui chơi, gắn kết tình cảm gia đình. Các hoạt động bao gồm: thi gia đình khéo tay, trò chơi dân gian, tiết mục văn nghệ, và bữa tiệc buffet ngoài trời.",
                    ImageUrl = "https://images.unsplash.com/photo-1541692641319-981cc79ee10a?w=800",
                    ThumbnailUrl = "https://images.unsplash.com/photo-1541692641319-981cc79ee10a?w=400",
                    Category = "Sự kiện",
                    EventDate = new DateTime(2024, 6, 1),
                    Location = "Sân trường Lá Mây",
                    IsFeatured = true,
                    DisplayOrder = 1,
                    ViewCount = 156
                },
                new Activity
                {
                    Id = 2,
                    Title = "Chuyến dã ngoại Thảo Cầm Viên",
                    Summary = "Trẻ được khám phá thế giới động vật và thiên nhiên",
                    Content = "Chuyến dã ngoại đến Thảo Cầm Viên là một phần của chương trình học ngoài trời, giúp trẻ tiếp xúc với thiên nhiên, tìm hiểu về các loài động vật. Các em được quan sát thực tế, nghe hướng dẫn viên giới thiệu và tham gia workshop vẽ tranh động vật.",
                    ImageUrl = "https://images.unsplash.com/photo-1503454537195-1dcabb73ffb9?w=800",
                    ThumbnailUrl = "https://images.unsplash.com/photo-1503454537195-1dcabb73ffb9?w=400",
                    Category = "Ngoại khóa",
                    EventDate = new DateTime(2024, 5, 15),
                    Location = "Thảo Cầm Viên Sài Gòn",
                    IsFeatured = true,
                    DisplayOrder = 2,
                    ViewCount = 203
                },
                new Activity
                {
                    Id = 3,
                    Title = "Lễ Tổng kết năm học 2023-2024",
                    Summary = "Gala tổng kết với nhiều tiết mục văn nghệ và trao giải thưởng",
                    Content = "Lễ Tổng kết năm học là dịp để nhìn lại những thành tựu của trẻ trong năm học vừa qua. Chương trình bao gồm: phát biểu của Hiệu trưởng, biểu diễn văn nghệ của các lớp, trao giấy khen và quà cho trẻ, và tiệc buffet cuối năm.",
                    ImageUrl = "https://images.unsplash.com/photo-1596464716127-f2a82984de30?w=800",
                    ThumbnailUrl = "https://images.unsplash.com/photo-1596464716127-f2a82984de30?w=400",
                    Category = "Lễ hội",
                    EventDate = new DateTime(2024, 5, 25),
                    Location = "Hội trường trường",
                    IsFeatured = true,
                    DisplayOrder = 3,
                    ViewCount = 312
                },
                new Activity
                {
                    Id = 4,
                    Title = "Cuộc thi Vẽ tranh 'Mái nhà của em'",
                    Summary = "Phát huy sáng tạo và tình yêu gia đình qua nét vẽ",
                    Content = "Cuộc thi vẽ tranh với chủ đề 'Mái nhà của em' đã thu hút hơn 200 bé tham gia. Các tác phẩm được trưng bày tại hành lang trường và bình chọn online. Đây là hoạt động nhằm phát triển khả năng sáng tạo và tình cảm gia đình cho trẻ.",
                    ImageUrl = "https://images.unsplash.com/photo-1567057419565-4349c49d8a04?w=800",
                    ThumbnailUrl = "https://images.unsplash.com/photo-1567057419565-4349c49d8a04?w=400",
                    Category = "Học tập",
                    EventDate = new DateTime(2024, 4, 20),
                    Location = "Phòng học và hành lang trường",
                    IsFeatured = false,
                    DisplayOrder = 4,
                    ViewCount = 178
                },
                new Activity
                {
                    Id = 5,
                    Title = "Ngày hội Thể thao 2024",
                    Summary = "Các trò chơi vận động vui nhộn phát triển thể chất cho trẻ",
                    Content = "Ngày hội Thể thao với các môn: chạy tiếp sức, nhảy bao bố, kéo co, bắn bóng rổ. Trẻ được tham gia theo đội và học được tinh thần đồng đội, fair-play.",
                    ImageUrl = "https://images.unsplash.com/photo-1580582932707-520aed937b7b?w=800",
                    ThumbnailUrl = "https://images.unsplash.com/photo-1580582932707-520aed937b7b?w=400",
                    Category = "Thể thao",
                    EventDate = new DateTime(2024, 3, 15),
                    Location = "Sân thể thao trường",
                    IsFeatured = true,
                    DisplayOrder = 5,
                    ViewCount = 245
                },
                new Activity
                {
                    Id = 6,
                    Title = "Workshop Làm bánh Trung Thu",
                    Summary = "Trẻ tự tay làm bánh Trung Thu cùng bố mẹ",
                    Content = "Workshop làm bánh Trung Thu là hoạt động truyền thống mỗi dịp Tết Trung Thu. Trẻ được hướng dẫn làm bánh nướng và bánh dẻo, tìm hiểu ý nghĩa của Tết Trung Thu.",
                    ImageUrl = "https://images.unsplash.com/photo-1574966739987-65e38db0f7ce?w=800",
                    ThumbnailUrl = "https://images.unsplash.com/photo-1574966739987-65e38db0f7ce?w=400",
                    Category = "Vui chơi",
                    EventDate = new DateTime(2024, 9, 10),
                    Location = "Phòng chức năng",
                    IsFeatured = true,
                    DisplayOrder = 6,
                    ViewCount = 189
                }
            });

            // Seed News
            News.AddRange(new[]
            {
                new News
                {
                    Id = 1,
                    Title = "Thông báo tuyển sinh năm học 2024-2025",
                    Slug = "thong-bao-tuyen-sinh-nam-hoc-2024-2025",
                    Summary = "Trường Mầm Non Lá Mây thông báo tuyển sinh các lớp mầm non cho năm học mới với nhiều ưu đãi hấp dẫn.",
                    Content = @"<p>Trường Mầm Non Lá Mây trân trọng thông báo tuyển sinh năm học 2024-2025 với các thông tin sau:</p>
                    <h4>1. Đối tượng tuyển sinh</h4>
                    <ul>
                        <li>Nhà trẻ: Trẻ từ 12 tháng đến 24 tháng tuổi</li>
                        <li>Mẫu giáo nhỡ: Trẻ từ 3 đến 4 tuổi</li>
                        <li>Mẫu giáo lớn: Trẻ từ 5 đến 6 tuổi</li>
                    </ul>
                    <h4>2. Thời gian nhận hồ sơ</h4>
                    <p>Từ ngày 01/03/2024 đến hết ngày 30/06/2024</p>
                    <h4>3. Ưu đãi đặc biệt</h4>
                    <ul>
                        <li>Giảm 20% học phí tháng đầu cho học sinh mới</li>
                        <li>Tặng bộ đồng phục miễn phí</li>
                        <li>Giảm 10% cho anh chị em cùng nhà</li>
                    </ul>
                    <h4>4. Hồ sơ cần chuẩn bị</h4>
                    <ul>
                        <li>Đơn xin nhập học (theo mẫu của trường)</li>
                        <li>Giấy khai sinh bản sao</li>
                        <li>Sổ tiêm chủng photo</li>
                        <li>2 ảnh 3x4</li>
                    </ul>",
                    ImageUrl = "https://images.unsplash.com/photo-1503676260728-1c00da094a0b?w=800",
                    Author = "Ban Giám hiệu",
                    Category = "Thông báo",
                    Tags = "tuyển sinh, năm học mới, ưu đãi",
                    IsFeatured = true,
                    PublishedAt = new DateTime(2024, 3, 1),
                    ViewCount = 1256
                },
                new News
                {
                    Id = 2,
                    Title = "5 cách giúp trẻ thích đi học mầm non",
                    Slug = "5-cach-giup-tre-thich-di-hoc-mam-non",
                    Summary = "Nhiều phụ huynh lo lắng khi con không thích đi học. Dưới đây là 5 cách giúp trẻ yêu thích việc đến trường.",
                    Content = @"<p>Giai đoạn đầu đi học mầm non là thử thách lớn với nhiều trẻ. Dưới đây là 5 cách giúp trẻ thích đi học:</p>
                    <h4>1. Chuẩn bị tâm lý trước khi đi học</h4>
                    <p>Hãy nói với trẻ về những điều thú vị ở trường: bạn bè, thầy cô, đồ chơi. Đọc sách về chủ đề đi học để trẻ có hình dung tích cực.</p>
                    <h4>2. Tạo thói quen sinh hoạt đều đặn</h4>
                    <p>Cho trẻ đi ngủ sớm, dậy sớm để tránh vội vàng buổi sáng. Thói quen đều đặn giúp trẻ cảm thấy an toàn.</p>
                    <h4>3. Gửi trẻ đúng giờ, đón đúng giờ</h4>
                    <p>Việc đón trẻ đúng giờ cam kết giúp trẻ cảm thấy tin tưởng. Trẻ sẽ biết bố mẹ sẽ đến đón mình.</p>
                    <h4>4. Trao đổi với giáo viên thường xuyên</h4>
                    <p>Hãy hỏi giáo viên về tình hình của trẻ ở trường để có cách hỗ trợ phù hợp.</p>
                    <h4>5. Tạo không khí vui vẻ khi đi học</h4>
                    <p>Hãy tạo ra những nghi thức vui vẻ như ôm hôn tạm biệt, hẹn gặp lại sau giờ học.</p>",
                    ImageUrl = "https://images.unsplash.com/photo-1587654780291-39c9404d746b?w=800",
                    Author = "Cô Nguyễn Thị Lan",
                    Category = "Kiến thức",
                    Tags = "tâm lý trẻ, đi học, gợi ý",
                    IsFeatured = true,
                    PublishedAt = new DateTime(2024, 2, 15),
                    ViewCount = 892
                },
                new News
                {
                    Id = 3,
                    Title = "Trường đạt chứng nhận Chất lượng Quốc gia",
                    Slug = "truong-dat-chung-nhan-chat-luong-quoc-gia",
                    Summary = "Trường Mầm Non Lá Mây vinh dự đạt chứng nhận Chất lượng Quốc gia năm 2024.",
                    Content = @"<p>Chúng tôi vui mừng thông báo Trường Mầm Non Lá Mây đã chính thức đạt chứng nhận Chất lượng Quốc gia do Bộ Giáo dục và Đào tạo cấp.</p>
                    <p>Đây là kết quả của quá trình nỗ lực không ngừng trong việc nâng cao chất lượng giáo dục, đầu tư cơ sở vật chất và đào tạo đội ngũ giáo viên.</p>
                    <h4>Tiêu chí đánh giá bao gồm:</h4>
                    <ul>
                        <li>Chất lượng giáo dục và chăm sóc</li>
                        <li>Cơ sở vật chất, trang thiết bị</li>
                        <li>Đội ngũ giáo viên và nhân viên</li>
                        <li>An toàn vệ sinh thực phẩm</li>
                        <li>Sự hài lòng của phụ huynh</li>
                    </ul>",
                    ImageUrl = "https://images.unsplash.com/photo-1580582932707-520aed937b7b?w=800",
                    Author = "Ban Giám hiệu",
                    Category = "Tin tức",
                    Tags = "chứng nhận, chất lượng, thành tích",
                    IsFeatured = true,
                    PublishedAt = new DateTime(2024, 1, 20),
                    ViewCount = 1567
                },
                new News
                {
                    Id = 4,
                    Title = "Workshop: Dinh dưỡng cho trẻ mầm non",
                    Slug = "workshop-dinh-duong-cho-tre-mam-non",
                    Summary = "Chuyên gia dinh dưỡng chia sẻ về chế độ ăn uống khoa học cho trẻ 1-6 tuổi.",
                    Content = @"<p>Trường phối hợp với Viện Dinh dưỡng Quốc gia tổ chức workshop về dinh dưỡng cho trẻ mầm non.</p>
                    <h4>Nội dung workshop:</h4>
                    <ul>
                        <li>Chế độ dinh dưỡng cân bằng cho từng độ tuổi</li>
                        <li>Cách xử lý khi trẻ biếng ăn</li>
                        <li>Thực đơn mẫu trong một tuần</li>
                        <li>Các món ăn healthy cho bé</li>
                    </ul>",
                    ImageUrl = "https://images.unsplash.com/photo-1490645935967-10de6ba17061?w=800",
                    Author = "TS. Nguyễn Văn A",
                    Category = "Sự kiện",
                    Tags = "dinh dưỡng, workshop, sức khỏe",
                    IsFeatured = false,
                    PublishedAt = new DateTime(2024, 2, 28),
                    ViewCount = 723
                },
                new News
                {
                    Id = 5,
                    Title = "Khai giảng lớp Tiếng Anh mầm non",
                    Slug = "khai-giang-lop-tieng-anh-mam-non",
                    Summary = "Chương trình Tiếng Anh chuẩn Cambridge dành cho trẻ 4-6 tuổi chính thức khai giảng.",
                    Content = @"<p>Trường chính thức khai giảng chương trình Tiếng Anh mầm non với giáo trình Cambridge Starters.</p>
                    <h4>Đặc điểm chương trình:</h4>
                    <ul>
                        <li>Giáo viên bản ngữ có chứng chỉ CELTA</li>
                        <li>Sĩ số tối đa 15 học sinh/lớp</li>
                        <li>Học qua trò chơi và hoạt động</li>
                        <li>Chuẩn bị cho chứng chỉ Cambridge Starters</li>
                    </ul>",
                    ImageUrl = "https://images.unsplash.com/photo-1544717305-2782549b5136?w=800",
                    Author = "Cô Trần Thị Hoa",
                    Category = "Thông báo",
                    Tags = "tiếng anh, khai giảng, ngoại ngữ",
                    IsFeatured = false,
                    PublishedAt = new DateTime(2024, 3, 5),
                    ViewCount = 634
                },
                new News
                {
                    Id = 6,
                    Title = "Phương pháp Montessori tại trường",
                    Slug = "phuong-phap-montessori-tai-truong",
                    Summary = "Tìm hiểu về phương pháp giáo dục Montessori được áp dụng tại Lá Mây.",
                    Content = @"<p>Phương pháp Montessori là phương pháp giáo dục nổi tiếng thế giới, tập trung vào việc phát triển tự nhiên của trẻ.</p>
                    <h4>Nguyên tắc chính:</h4>
                    <ul>
                        <li>Tôn trọng tốc độ phát triển của từng trẻ</li>
                        <li>Môi trường học tập chuẩn bị kỹ lưỡng</li>
                        <li>Khuyến khích trẻ tự học và khám phá</li>
                        <li>Giáo viên là người hướng dẫn, không phải giảng dạy</li>
                    </ul>",
                    ImageUrl = "https://images.unsplash.com/photo-1587654780291-39c9404d746b?w=800",
                    Author = "Cô Trần Thị Hoa",
                    Category = "Kiến thức",
                    Tags = "montessori, phương pháp, giáo dục",
                    IsFeatured = true,
                    PublishedAt = new DateTime(2024, 1, 10),
                    ViewCount = 1089
                }
            });
        }
    }
}
