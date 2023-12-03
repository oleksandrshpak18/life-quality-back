using life_quality_back.Data.Interfaces;
using life_quality_back.Data.Models;
using life_quality_back.Data.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace life_quality_back.Data.Repositories
{
    public class ResultsRepository 
    {
        private AppDbContext _context;
        public ResultsRepository(AppDbContext context) 
        { 
            _context = context;
        }
        public IEnumerable<ResultsVM> GetAll()
        {
            return _context.Results
                .Select(x => new ResultsVM
                {
                    Date = x.Date,
                    isSaved = x.isSaved,
                    QuestionnaireId = x.QuestionnaireId,
                    QuestionnaireName = x.Questionnaire.QuestionnaireName,
                    PatientId = x.PatientId,
                    PatientFirstName = x.Patient.FirstName,
                    PatientLastName = x.Patient.LastName,
                    DiseaseName = x.Patient.Disease.DiseaseName,
                    DoctorId = x.Patient.DoctorId,
                    Gender = x.Patient.Gender,
                    ResultsId = x.ResultsId,
                    Age = DateTime.Today.Year - x.Patient.BirthDate.Year - (DateTime.Today.DayOfYear < x.Patient.BirthDate.DayOfYear ? 1 : 0)
                })
                .ToList();
    }
    public Models.Results GetById(int id)
        {
            return _context.Results
                .Include(x => x.Patient)
                //.Include(x => x.Questionnaire)
                    //.ThenInclude(x => x.Questions)
                    //.ThenInclude(x => x.Answers)
                .Include(x => x.ResultsPatientAnswers)
                    .ThenInclude(x => x.PatientAnswer)
                .FirstOrDefault(x => x.ResultsId == id);
        }

        public IEnumerable<ResultsVM> GetAllByDoctorId(int doctorId)
        {
            return GetAll().Where(x => x.DoctorId == doctorId).ToList();
        }

        public Models.Results ToggleSavedResult (int id)
        {
            var result = _context.Results.Find(id);

            if (result != null)
            {
                result.isSaved = !result.isSaved;
                _context.SaveChanges();
            }
            return result;
        }
    }
}
