using life_quality_back.Data.Models;

namespace life_quality_back.Data.Repositories
{
    public class UserRepository
    {
        private AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public string GetPasswordById(int id)
        {
            return _context.Users.Where(x => x.DoctorId== id).Select(x => x.Password).FirstOrDefault();
        }
        public string GetLoginById(int id)
        {
            return _context.Users.Where(x => x.DoctorId == id).Select(x => x.Login).FirstOrDefault();
        }
    }
}
