using life_quality_back.Data.Models;

namespace life_quality_back.Data.Repositories
{
    public class QuestionRepository
    {
        private AppDbContext _context;
        public QuestionRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Question> GetQuestionsToQuestionnaire(int questionnaireId)
        {
            return _context.Questions.Where(x => x.QuestionnaireId == questionnaireId).ToList();
        }
    }
}
