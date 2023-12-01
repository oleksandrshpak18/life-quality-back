namespace life_quality_back.Data.ViewModels
{
    public class ResultsVM
    {
        public int ResultsId {  get; set; }
        public DateTime Date { get; set; }
        public bool isSaved {  get; set; }
        public int PatientId { get; set; }
        public string PatientFirstName { get; set;}
        public string PatientLastName { get; set;}
        public string Gender { get; set;}
        public string DiseaseName { get; set;}
        public int Age { get; set;}
        public int QuestionnaireId { get; set; }
        public string QuestionnaireName { get; set  ; }
        public int DoctorId { get; set; }
    }
}
