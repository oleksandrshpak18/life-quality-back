using life_quality_back.Data.Interfaces;
using life_quality_back.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace life_quality_back.Data.Repositories
{
    public class QuestionnaireRepository : IGet<Questionnaire>
    {
        private AppDbContext _context;
        public QuestionnaireRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Questionnaire> GetAll()
        {
            return _context.Questionnaires
                            .Include(x => x.Questions)
                            .ThenInclude(q => q.Answers)
                            .ToList(); 
        }

        public Questionnaire GetById(int id)
        {
            return _context.Questionnaires
                            .Include(x => x.Questions)
                            .ThenInclude(q => q.Answers)
                            .FirstOrDefault(x => x.QuestionnaireId == id);
        }
    }
}
