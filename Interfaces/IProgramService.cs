using System.Collections.Generic;
using System.Threading.Tasks;
using MamNonApp.Models.Entities;

namespace MamNonApp.Interfaces
{
    /// <summary>
    /// Interface cho Program Service
    /// </summary>
    public interface IProgramService
    {
        Task<IEnumerable<EducationProgram>> GetAllProgramsAsync();
        Task<IEnumerable<EducationProgram>> GetActiveProgramsAsync();
        Task<EducationProgram?> GetProgramByIdAsync(int id);
        Task<IEnumerable<EducationProgram>> GetProgramsByAgeGroupAsync(string ageGroup);
    }
}
