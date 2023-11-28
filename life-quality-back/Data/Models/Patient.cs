using System.ComponentModel.DataAnnotations.Schema;

namespace life_quality_back.Data.Models
{
    public class Patient
    {
        public int PatientId { get; set; }
        public string FirstName { get; set;}
        public string? MiddleName { get; set;}   
        public string LastName { get; set;}
        public string Anamnesis { get; set;}
        public string Email { get; set;}
        public string Gender { get; set;}
        public DateTime BirthDate {  get; set;}
        public DateTime RehabilitationStartDate {  get; set;}

        public int TreatmentStrategyId { get; set; } // Foreign key property
        public virtual TreatmentStrategy TreatmentStrategy { get; set; }

        public int DiseaseId { get; set; } // Foreign key property
        public virtual Disease Disease { get; set; }

        public int DoctorId { get; set; } // Foreign key property
        public virtual Doctor Doctor { get; set; }
    }
}
