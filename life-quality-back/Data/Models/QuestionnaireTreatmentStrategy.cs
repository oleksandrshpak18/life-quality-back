namespace life_quality_back.Data.Models
{
    public class QuestionnaireTreatmentStrategy
    {
        public int QuestionnaireTreatmentStrategyId { get; set; }
        public int QuestionnaireId { get; set; }
        public virtual Questionnaire Questionnaire { get; set; }
        public int TreatmentStrategyId { get; set; }
        //public virtual TreatmentStrategy TreatmentStrategy { get; set; }
    }
}
