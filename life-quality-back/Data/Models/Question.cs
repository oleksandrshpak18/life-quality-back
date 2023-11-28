namespace life_quality_back.Data.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public int QuestionnaireId { get; set; }
        public virtual IEnumerable<Answer> Answers { get; set; }
    }
}
