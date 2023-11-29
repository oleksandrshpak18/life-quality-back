namespace life_quality_back.Data.Models
{
    public class Results
    {
        public int ResultsId { get; set; }
        public DateTime Date { get; set; }
        public bool isSaved { get; set; }
        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; }
        public int QuestionnaireId { get; set; }
        public virtual Questionnaire Questionnaire { get; set; }
        public virtual IEnumerable<ResultsPatientAnswer> ResultsPatientAnswers { get; set; }
    }
}
