using life_quality_back.Data.Filtering;
using life_quality_back.Data.Interfaces;
using life_quality_back.Data.Models;
using life_quality_back.Data.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

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
                .Include(x => x.Questionnaire)
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

        public IEnumerable<ResultsVM> GetFiltered(FilterParameters parameters)
        {
            IQueryable<Models.Results> query = _context.Results;
            
            query = query.Where(r => r.Patient.DoctorId == parameters.DoctorId);

            if (parameters.BeginDate.HasValue)
            {
                DateTime beginDate = parameters.BeginDate.Value;
                query = query.Where(r => r.Date >= beginDate);
            }

            if (parameters.EndDate.HasValue)
            {
                DateTime endDate = parameters.EndDate.Value;
                query = query.Where(r => r.Date <= endDate);
            }

            if (!string.IsNullOrEmpty(parameters.Gender))
            {
                query = query.Where(r => r.Patient.Gender == parameters.Gender);
            }

            if (!string.IsNullOrEmpty(parameters.DiseaseName))
            {
                query = query.Where(r => r.Patient.Disease.DiseaseName.Contains(parameters.DiseaseName));
            }

            if (parameters.MinAge.HasValue && parameters.MinAge > 0)
            {
                // Assuming BirthDate is a DateTime property in Patient
                DateTime minBirthDate = DateTime.Now.AddYears(-parameters.MinAge.Value);
                query = query.Where(r => r.Patient.BirthDate <= minBirthDate);
            }

            if (parameters.MaxAge.HasValue && parameters.MaxAge > 0)
            {
                DateTime maxBirthDate = DateTime.Now.AddYears(-parameters.MaxAge.Value - 1);
                query = query.Where(r => r.Patient.BirthDate >= maxBirthDate);
            }

            if (!string.IsNullOrEmpty(parameters.QuestionnaireName))
            {
                query = query.Where(r => r.Questionnaire.QuestionnaireName.Contains(parameters.QuestionnaireName));
            }

            // Project the results to ResultsVM
            var resultsVMs = query.Select(x => new ResultsVM
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
            });

            return resultsVMs.ToList();
        }
    
        public IEnumerable<Dictionary<string, int>> GetDoctorSavedQuestionnaireNames(int doctorId)
        {
            var doctorResults = GetAllByDoctorId(doctorId).ToList();

            var savedQuestionnaireNames = doctorResults
                                                .GroupBy(result => result.QuestionnaireName)
                                                .Select(group => new { QuestionnaireName = group.Key, Count = CountSavedResultsByQuestionnaireName(group.Key, doctorResults) })
                                                .ToDictionary(item => item.QuestionnaireName, item => item.Count);

            return new List<Dictionary<string, int>> { savedQuestionnaireNames };
        }

        private int CountSavedResultsByQuestionnaireName(string questionnaireName, List<ResultsVM> doctorResults)
        {
            return doctorResults.Count(result => result.isSaved && result.QuestionnaireName.Equals(questionnaireName));
        }

        public IEnumerable<Dictionary<string, int>> GetDoctorSavedResultsByQuestionnaireName(int doctorId, string questionnaireName)
        {
            var doctorResults = GetAllByDoctorId(doctorId)
                                                .Where(result => result.QuestionnaireName.Equals(questionnaireName))
                                                .ToList();

            var savedPatientsResultsByQuestionnaireName = doctorResults
                                                .GroupBy(result => result.PatientFirstName + " " + result.PatientLastName)
                                                .Select(group => new { PatientName = group.Key, Count = CountSavedPatientResultsByQuestionnaireName(questionnaireName, group.Key, doctorResults) })
                                                .ToDictionary(item => item.PatientName, item => item.Count);

            return new List<Dictionary<string, int>> { savedPatientsResultsByQuestionnaireName };
        }
        private int CountSavedPatientResultsByQuestionnaireName(string questionnaireName, string patientName, List<ResultsVM> doctorResults)
        { 
            return doctorResults.Count(result => result.isSaved &&
                    result.QuestionnaireName.Equals(questionnaireName) &&
                    result.PatientFirstName.Equals(patientName[..patientName.IndexOf(" ")]) &&
                    result.PatientLastName.Equals(patientName[(patientName.IndexOf(" ") + 1)..]));
        }

        public IEnumerable<Models.Results> GetPatientSavedResultsByQuestionnaireName(int doctorId, string questionnaireName, string patientName) 
        {
            var patientSavedResultsByQuestionnaireName = GetAllByDoctorId(doctorId)
                                                                            .Where(results => results.isSaved &&
                                                                                results.QuestionnaireName.Equals(questionnaireName) &&
                                                                                results.PatientFirstName.Equals(patientName[..patientName.IndexOf(" ")]) &&
                                                                                results.PatientLastName.Equals(patientName[(patientName.IndexOf(" ") + 1)..]))
                                                                            .Select(results => results.ResultsId)
                                                                            .ToList();

            var savedResults = patientSavedResultsByQuestionnaireName
                    .Select(id => GetById(id))
                    .Where(result => result != null)
                    .ToList();


            return savedResults;
        }

        public int GetTotalCountSavedResultForDoctor(int doctorId)
        {
            return GetAllByDoctorId(doctorId).Where(result => result.isSaved).Count();
        }
    }
}
