namespace life_quality_back.Data.Filtering
{
    public class FilterParametersBuilder
    {
        private FilterParameters _filterParameters;
        public FilterParametersBuilder()
        {
            _filterParameters = new FilterParameters();
        }

        public  FilterParametersBuilder SetBeginDate(DateTime ?beginDate)
        {
            _filterParameters.BeginDate = beginDate;
            return this;
        }

        public  FilterParametersBuilder SetEndDate(DateTime ?endDate)
        {
            _filterParameters.EndDate = endDate;
            return this;
        }

        public  FilterParametersBuilder SetGender(string gender)
        {
            _filterParameters.Gender = gender;
            return this;
        }

        public  FilterParametersBuilder SetDiseaseName(string diseaseName)
        {
            _filterParameters.DiseaseName = diseaseName;
            return this;
        }

        public  FilterParametersBuilder SetMinAge(int ?minAge)
        {
            _filterParameters.MinAge = minAge;
            return this;
        }

        public  FilterParametersBuilder SetMaxAge(int ?maxAge)
        {
            _filterParameters.MaxAge = maxAge;
            return this;
        }

        public  FilterParametersBuilder SetQuestionnaireName(string questionnaireName)
        {
            _filterParameters.QuestionnaireName = questionnaireName;
            return this;
        }

        public  FilterParametersBuilder SetDoctorId(int doctorId)
        {
            _filterParameters.DoctorId = doctorId;
            return this;
        }

        public FilterParameters Build()
        {
            // You can perform additional validation or customization here before returning the built object
            return _filterParameters;
        }
    }
}
