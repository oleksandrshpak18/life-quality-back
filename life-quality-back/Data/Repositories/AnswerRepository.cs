using life_quality_back.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace life_quality_back.Data.Repositories
{
    public class AnswerRepository
    {
        private AppDbContext _context;
        public AnswerRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Answer> GetAnswersToQuestion(int questionId)
        {
            return _context.Answers.Where(x =>  x.QuestionId == questionId).ToList();
        }
    }
}
