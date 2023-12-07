using life_quality_back.Data.Models;

namespace life_quality_back.Controllers.Conclusion
{
    public class GenerateConclusion
    {
        private readonly string _questionaryName;
        private readonly int _score;
        private readonly string conclusion = "The conclusion for this result is: ";

        public GenerateConclusion(string questionaryName, List<ResultsPatientAnswer> results)
        {
            _questionaryName = questionaryName;
            _score = CalculationPoints(results);
        }

        public string GetConclusion()
        {
            if (_questionaryName.Equals("National Institutes of Health stroke scale (NIHSS)"))
            {
                return ConclusionNIHSS();
            }
            if (_questionaryName.Equals("Barthel Index (BI)"))
            {
                return ConclusionBI();
            }
            if (_questionaryName.Equals("Berg Balance Test (BBS)"))
            {
                return ConclusionBBS();
            }
            if (_questionaryName.Equals("Problem Areas In Diabetes (PAID)"))
            {
                return ConclusionPAID();
            }
            if (_questionaryName.Equals("Diabetes Distress Scale (DDS-17)"))
            {
                return ConclusionDDS_17();
            }
            if (_questionaryName.Equals("Insulin Treatment Appraisal Scale (ITAS)"))
            {
                return ConclusionITAS();
            }

            return string.Empty;
        }
        
        private string ConclusionNIHSS()
        {
            if (_score == 0)
            {
                return conclusion + "No stroke symptoms!";
            }
            if (_score >= 1 && _score <= 4)
            {
                return conclusion + "Mild stroke!";
            }
            if (_score >= 5 && _score <= 15)
            {
                return conclusion + "Moderate stroke!";
            }
            if (_score >= 16 && _score <= 20)
            {
                return conclusion + "Moderate to severe stroke!";
            }
            if (_score >= 21 && _score <= 42)
            {
                return conclusion + "Severe stroke!";
            }

            return string.Empty;
        }
        private string ConclusionBI()
        {
            if (_score == 100)
            {
                return conclusion + "The patient is considered completely independent and able to perform all daily activities without assistance!";
            }
            if (_score >= 90 && _score <= 99)
            {
                return conclusion + "Mild to moderate limitations in performing some daily tasks, but the patient remains fairly independent!";
            }
            if (_score >= 60 && _score <= 89)
            {
                return conclusion + "Moderate limitations in performing many daily tasks. The patient may need help!";
            }
            if (_score >= 40 && _score <= 59)
            {
                return conclusion + "Significant limitations in independence. The patient needs a lot of help!";
            }
            if (_score >= 0 && _score <= 39)
            {
                return conclusion + "High level of dependence. The patient finds it difficult or impossible to perform many daily activities without assistance!";
            }

            return string.Empty;
        }
        private string ConclusionBBS()
        {
            if (_score >= 0 && _score <= 20)
            {
                return conclusion + "Uses a wheelchair!";
            }
            if (_score > 20 && _score <= 40)
            {
                return conclusion + "Walks with help!";
            }
            if (_score > 40 && _score <= 56)
            {
                return conclusion + "Independent!";
            }

            return string.Empty;
        }
        private string ConclusionPAID()
        {
            var score = _score * 1.25;

            if (score >= 40)
            {
                return conclusion + "Severe diabetes distress!";
            }
            if (score < 40)
            {
                return conclusion + "Moderate to severe distress!";
            }

            return string.Empty;
        }
        private string ConclusionDDS_17()
        {
            if (_score >= 17 && _score <= 45)
            {
                return conclusion + "Low level of distress. The patient is probably coping well with the emotional aspects of diabetes!";
            }
            if (_score >= 46 && _score <= 74)
            {
                return conclusion + "Average level of distress. May indicate a certain level of emotional discomfort and stress, but the patient can still practice managing emotions!";
            }
            if (_score >= 75 && _score <= 102)
            {
                return conclusion + "High level of distress. Signals a high level of emotional pressure associated with diabetes. The patient may need additional psychological support and stress management!";
            }

            return string.Empty;
        }
        private string ConclusionITAS()
        {
            if (_score >= 20 && _score <= 46)
            {
                return conclusion + "Low levels of distress or negative attitudes!";
            }
            if (_score >= 47 && _score <= 73)
            {
                return conclusion + "Average level of distress or negative attitude!";
            }
            if (_score >= 74 && _score <= 100)
            {
                return conclusion + "High levels of distress or negative attitudes!";
            }

            return string.Empty;
        }

        private static int CalculationPoints(List<ResultsPatientAnswer> results)
        {
            int totalPoints = 0;

            foreach (ResultsPatientAnswer answer in results)
            {
                totalPoints += answer.PatientAnswer.AnswerValue;
            }

            return totalPoints;
        }
    }
}
