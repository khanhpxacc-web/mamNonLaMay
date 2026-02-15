using MamNonApp.Models.Entities;

namespace MamNonApp.Models.ViewModels
{
    /// <summary>
    /// ViewModel cho trang chi tiết chương trình
    /// </summary>
    public class ProgramDetailViewModel
    {
        public SeoViewModel Seo { get; set; } = new();
        public EducationProgram? Program { get; set; }
    }
}
