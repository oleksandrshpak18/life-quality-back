namespace life_quality_back.Data.Models
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        public string FirstName { get; set; }
        public string ? MiddleName { get; set; }
        public string LastName { get; set; } 
        public string Email { get; set; }
        public string ?Phone { get; set; }
        public string Password { get; set; } 
        public string ?Gender { get; set; } 
        public string Speciality { get; set; }
        public string ?TypeOfDoctor { get; set; }
        public string ?Education { get; set; }
    }
}
