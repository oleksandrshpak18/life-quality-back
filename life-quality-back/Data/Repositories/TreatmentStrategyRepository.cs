using life_quality_back.Data.Interfaces;
using life_quality_back.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace life_quality_back.Data.Repositories
{
    public class TreatmentStrategyRepository : IGet<TreatmentStrategy>
    {
        private AppDbContext _context;
        public TreatmentStrategyRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<TreatmentStrategy> GetAll()
        {
            return _context.TreatmentStrategies
                .Include(x => x.QuestionnaireTreatmentStrategy)
                .ThenInclude(x => x.Questionnaire)
                .ToList();
        }

        public TreatmentStrategy GetById(int id)
        {
            return _context.TreatmentStrategies
                .Include(x => x.QuestionnaireTreatmentStrategy)
                .ThenInclude(x => x.Questionnaire)
                .FirstOrDefault(x => x.TreatmentStrategyId == id);
        }
    }
}
