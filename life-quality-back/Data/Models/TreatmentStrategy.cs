namespace life_quality_back.Data.Models
{
    public class TreatmentStrategy
    {
        public int TreatmentStrategyId { get; set; }
        public string TreatmentStrategyName { get; set; }
        public string TreatmentStrategyDescription { get; set; }

        public virtual IEnumerable<QuestionnaireTreatmentStrategy> QuestionnaireTreatmentStrategy { get; set; }
    }
}
