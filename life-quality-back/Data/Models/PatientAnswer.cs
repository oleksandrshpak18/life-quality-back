namespace life_quality_back.Data.Models
{
    public class PatientAnswer
    {
        public int PatientAnswerId { get; set; }
        //public int ResultId { get; set; }
        //public virtual Results Results { get; set; }
        public string QuestionText { get; set; }
        public string AnswerText { get; set; }
        public int AnswerValue { get; set; }
        //public int QuestionId { get; set; }
        //public virtual Question Question { get; set; }
        //public int AnswerId { get; set; }
        //public virtual Answer Answer { get; set; }
        //public string AnswerText { get; set; }
        //public int AnswerValue { get; set; }
    }
}
