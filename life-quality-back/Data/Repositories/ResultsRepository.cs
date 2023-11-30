using life_quality_back.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace life_quality_back.Data.Repositories
{
    public class ResultsRepository : IGet<Data.Models.Results>
    {
        private AppDbContext _context;
        public ResultsRepository(AppDbContext context) 
        { 
            _context = context;
        }
        public IEnumerable<Data.Models.Results> GetAll()
        {
            return _context.Results
                    .Include(x => x.Patient)
                    .Include(x => x.Questionnaire)
                        .ThenInclude(x => x.Questions)
                        .ThenInclude(x => x.Answers)
                    .Include(x => x.ResultsPatientAnswers)
                        .ThenInclude(x => x.PatientAnswer)
                    .ToList();
        }

        public Models.Results GetById(int id)
        {
            return _context.Results
                .Include(x => x.Patient)
                .Include(x => x.Questionnaire)
                    .ThenInclude(x => x.Questions)
                    .ThenInclude(x => x.Answers)
                .Include(x => x.ResultsPatientAnswers)
                    .ThenInclude(x => x.PatientAnswer)
                .FirstOrDefault(x => x.ResultsId == id);
        }

        public IEnumerable<Data.Models.Results> GetAllByDoctorId(int doctorId)
        {
            return GetAll().Where(x => x.Patient.DoctorId == doctorId).ToList();
        }
    }
}
