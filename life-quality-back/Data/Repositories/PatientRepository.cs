using life_quality_back.Data.Interfaces;
using life_quality_back.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace life_quality_back.Data.Repositories
{
    public class PatientRepository :IGet<Patient>
    {
        private AppDbContext _context;
        public PatientRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Patient> GetAll()
        {
            return _context.Patients
                .Include(p => p.TreatmentStrategy)
                //.ThenInclude(p => p.QuestionnaireTreatmentStrategy)
                //.ThenInclude(p => p.Questionnaire)
                .Include(p => p.Disease)
                .Include(p => p.Doctor)
                .ToList();
        }

        public Patient GetById(int id)
        {
            return _context.Patients
                .Include(p => p.TreatmentStrategy)
                .ThenInclude(p => p.QuestionnaireTreatmentStrategy)
                .ThenInclude(p => p.Questionnaire)
                .Include(p => p.Disease)
                .Include(p => p.Doctor)
                .FirstOrDefault(x => x.PatientId == id);
        }
    }
}
