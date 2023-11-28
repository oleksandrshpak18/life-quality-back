namespace life_quality_back.Data.Models
{
    public class Questionnaire
    {
        public int QuestionnaireId { get; set; }
        public string QuestionnaireName { get; set; }
        public virtual IEnumerable<Question> Questions { get; set; }
    }
}
