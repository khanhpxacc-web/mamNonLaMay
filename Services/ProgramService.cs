using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MamNonApp.Interfaces;
using MamNonApp.Models.Entities;

namespace MamNonApp.Services
{
    /// <summary>
    /// Program Service Implementation
    /// </summary>
    public class ProgramService : IProgramService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProgramService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<EducationProgram>> GetAllProgramsAsync()
        {
            return await _unitOfWork.Programs.GetAllAsync();
        }

        public async Task<IEnumerable<EducationProgram>> GetActiveProgramsAsync()
        {
            var programs = await _unitOfWork.Programs.GetAllAsync();
            return programs.Where(p => p.IsActive).OrderBy(p => p.DisplayOrder);
        }

        public async Task<EducationProgram?> GetProgramByIdAsync(int id)
        {
            return await _unitOfWork.Programs.GetByIdAsync(id);
        }

        public async Task<IEnumerable<EducationProgram>> GetProgramsByAgeGroupAsync(string ageGroup)
        {
            var programs = await _unitOfWork.Programs.GetAllAsync();
            return programs.Where(p => p.IsActive && p.AgeGroup == ageGroup);
        }
    }
}

