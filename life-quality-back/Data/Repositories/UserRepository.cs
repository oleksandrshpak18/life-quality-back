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

        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }
        public (string, string) GetAuthorizationDataById(int id)
        {
            return (
                    _context.Users.Where(x => x.UserId == id).Select(x => x.Login).FirstOrDefault(),
                    _context.Users.Where(x => x.UserId == id).Select(x => x.Password).FirstOrDefault()
                );
        }
        public int GetDoctorIdById(int id)
        {
            return _context.Users.Where(x => x.UserId == id).Select(x => x.DoctorId).FirstOrDefault();
        }
    }
}
