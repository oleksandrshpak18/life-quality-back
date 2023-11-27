using life_quality_back.Data.Interfaces;
using life_quality_back.Data.Models;

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
            return _context.TreatmentStrategies.ToList();
        }

        public TreatmentStrategy GetById(int id)
        {
            return _context.TreatmentStrategies.FirstOrDefault(x => x.TreatmentStrategyId == id);
        }
    }
}
