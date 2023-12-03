namespace life_quality_back.Data.Filtering
{
    public class FilterParameters
    {
        public DateTime ?BeginDate { get; set; }
        public DateTime ?EndDate { get; set; }
        public string ?Gender { get; set; }
        public string ?DiseaseName { get; set; }
        public int ?MinAge { get; set; }
        public int ?MaxAge { get; set; }
        public string? QuestionnaireName { get; set; }
        public int DoctorId { get; set; }
    }
}
