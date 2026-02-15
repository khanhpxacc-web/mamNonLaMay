using MamNonApp.Models.ViewModels;

namespace MamNonApp.Interfaces
{
    /// <summary>
    /// Interface cho SEO Service
    /// </summary>
    public interface ISeoService
    {
        SeoViewModel GetHomeSeo();
        SeoViewModel GetAboutSeo();
        SeoViewModel GetProgramsSeo();
        SeoViewModel GetGallerySeo();
        SeoViewModel GetContactSeo();
        string GenerateStructuredData(string type, object data);
    }
}
