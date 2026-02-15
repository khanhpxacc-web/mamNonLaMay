using System.Text.Json;
using MamNonApp.Interfaces;
using MamNonApp.Models.ViewModels;

namespace MamNonApp.Services
{
    /// <summary>
    /// SEO Service Implementation
    /// </summary>
    public class SeoService : ISeoService
    {
        private readonly string _siteName = "Trường Mầm Non Lá Mây";
        private readonly string _baseUrl = "https://mamnonlamay.edu.vn";

        public SeoViewModel GetHomeSeo()
        {
            return new SeoViewModel
            {
                Title = $"{_siteName} | Trường Mầm Non Hàng Đầu TP.HCM",
                MetaDescription = "Trường Mầm Non Lá Mây - Nơi ươm mầm tương lai cho trẻ em Việt Nam. Chương trình giáo dục chất lượng cao, đội ngũ giáo viên chuyên nghiệp, cơ sở vật chất hiện đại.",
                MetaKeywords = "trường mầm non, giáo dục mầm non, trường mẫu giáo, nhà trẻ, TP.HCM",
                CanonicalUrl = _baseUrl,
                OgTitle = $"{_siteName} | Trường Mầm Non Hàng Đầu",
                OgDescription = "Trường Mầm Non Lá Mây - Nơi ươm mầm tương lai",
                OgImage = $"{_baseUrl}/images/og-image.svg",
                OgType = "website"
            };
        }

        public SeoViewModel GetAboutSeo()
        {
            return new SeoViewModel
            {
                Title = $"Giới Thiệu | {_siteName}",
                MetaDescription = "Tìm hiểu về Trường Mầm Non Lá Mây - Lịch sử phát triển, sứ mệnh, tầm nhìn và đội ngũ giáo viên của chúng tôi.",
                MetaKeywords = "giới thiệu trường mầm non, sứ mệnh, tầm nhìn, đội ngũ giáo viên",
                CanonicalUrl = $"{_baseUrl}/gioi-thieu",
                OgTitle = $"Giới Thiệu | {_siteName}",
                OgDescription = "Tìm hiểu về Trường Mầm Non Lá Mây",
                OgImage = $"{_baseUrl}/images/about-og.jpg",
                OgType = "article"
            };
        }

        public SeoViewModel GetProgramsSeo()
        {
            return new SeoViewModel
            {
                Title = $"Chương Trình Học | {_siteName}",
                MetaDescription = "Các chương trình giáo dục mầm non chất lượng cao tại Lá Mây: Nhà trẻ (1-2 tuổi), Mẫu giáo nhỡ (3-4 tuổi), Mẫu giáo lớn (5-6 tuổi) và các lớp năng khiếu.",
                MetaKeywords = "chương trình mầm non, chương trình mẫu giáo, lớp nhà trẻ, lớp năng khiếu",
                CanonicalUrl = $"{_baseUrl}/chuong-trinh-hoc",
                OgTitle = $"Chương Trình Học | {_siteName}",
                OgDescription = "Các chương trình giáo dục chất lượng cao tại Lá Mây",
                OgImage = $"{_baseUrl}/images/programs-og.jpg",
                OgType = "article"
            };
        }

        public SeoViewModel GetGallerySeo()
        {
            return new SeoViewModel
            {
                Title = $"Thư Viện Ảnh | {_siteName}",
                MetaDescription = "Khám phá hình ảnh về hoạt động của trường, các buổi học sôi động và những khoảnh khắc đáng yêu của trẻ tại Lá Mây.",
                MetaKeywords = "thư viện ảnh mầm non, hình ảnh trường học, hoạt động trẻ em",
                CanonicalUrl = $"{_baseUrl}/thu-vien-anh",
                OgTitle = $"Thư Viện Ảnh | {_siteName}",
                OgDescription = "Những khoảnh khắc đáng yêu của trẻ tại Lá Mây",
                OgImage = $"{_baseUrl}/images/gallery-og.jpg",
                OgType = "website"
            };
        }

        public SeoViewModel GetContactSeo()
        {
            return new SeoViewModel
            {
                Title = $"Liên Hệ | {_siteName}",
                MetaDescription = "Liên hệ với Trường Mầm Non Lá Mây để được tư vấn về chương trình học và tham quan trường. Hotline: 0964106259",
                MetaKeywords = "liên hệ trường mầm non, đăng ký học, tư vấn giáo dục",
                CanonicalUrl = $"{_baseUrl}/lien-he",
                OgTitle = $"Liên Hệ | {_siteName}",
                OgDescription = "Liên hệ với Trường Mầm Non Lá Mây",
                OgImage = $"{_baseUrl}/images/contact-og.jpg",
                OgType = "website"
            };
        }

        public string GenerateStructuredData(string type, object data)
        {
            object structuredData = type.ToLower() switch
            {
                "organization" => new
                {
                    @context = "https://schema.org",
                    @type = "Preschool",
                    name = _siteName,
                    url = _baseUrl,
                    logo = $"{_baseUrl}/images/logo.png",
                    address = new
                    {
                        @type = "PostalAddress",
                        streetAddress = "170 Mạc Quyết, Anh Dũng, Dương Kinh",
                        addressLocality = "Hải Phòng",
                        addressRegion = "Hải Phòng",
                        addressCountry = "VN"
                    },
                    telephone = "0964106259",
                    email = "info@mamnonlamay.edu.vn",
                    sameAs = new[]
                    {
                        "https://facebook.com/mamnonlamay"
                    }
                },
                "website" => new
                {
                    @context = "https://schema.org",
                    @type = "WebSite",
                    name = _siteName,
                    url = _baseUrl,
                    potentialAction = new
                    {
                        @type = "SearchAction",
                        target = $"{_baseUrl}/search?q={{search_term_string}}",
                        queryinput = "required name=search_term_string"
                    }
                },
                "breadcrumb" => data,
                _ => new { }
            };

            return JsonSerializer.Serialize(structuredData, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            });
        }
    }
}

