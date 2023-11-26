using life_quality_back.Data.Interfaces;
using life_quality_back.Data.Models;

namespace life_quality_back.Data.Repositories
{
    public class DoctorRepository : IGet<Doctor>
    {
        private AppDbContext _context;
        public DoctorRepository(AppDbContext context)
        {
            _context = context;   
        }
        public IEnumerable<Doctor> GetAll()
        {
            return _context.Doctors.ToList();
        }

        public Doctor GetById(int id)
        {
            return _context.Doctors.FirstOrDefault(x => x.DoctorId == id);
        }
    }
}
