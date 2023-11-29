namespace life_quality_back.Data.Models
{
    public class ResultsPatientAnswer
    {
        public int ResultsPatientAnswerId { get; set; }
        public int ResultsId { get; set; }
        public int PatientAnswerId { get; set; }
        public virtual PatientAnswer PatientAnswer { get; set; }
    }
}
