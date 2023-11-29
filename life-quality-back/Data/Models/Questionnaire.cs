namespace life_quality_back.Data.Models
{
    public class Questionnaire
    {
        public int QuestionnaireId { get; set; }
        public string QuestionnaireName { get; set; }
        public string QuestionnaireDescription { get; set; }
        public virtual IEnumerable<Question> Questions { get; set; }
    }
}
