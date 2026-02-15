using System.Collections.Generic;
using MamNonApp.Models.Entities;

namespace MamNonApp.Models.ViewModels
{
    /// <summary>
    /// ViewModel cho trang chương trình học
    /// </summary>
    public class ProgramsViewModel
    {
        public SeoViewModel Seo { get; set; } = new();
        public IEnumerable<EducationProgram> Programs { get; set; } = new List<EducationProgram>();
    }
}
