using life_quality_back.Data.Interfaces;
using life_quality_back.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace life_quality_back.Data.Repositories
{
    public class DiseaseRepository : IGet<Disease>
    {
        private AppDbContext _context;
        public DiseaseRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Disease> GetAll()
        {
            return _context.Diseases.ToList();
        }

        public Disease GetById(int id)
        {
            return _context.Diseases.FirstOrDefault(x => x.DiseaseId == id);
        }
    }
}
