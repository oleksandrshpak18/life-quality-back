namespace life_quality_back.Data.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Password { get; set; }
        public string Login { get; set; }
        public int DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; }
    }
}
