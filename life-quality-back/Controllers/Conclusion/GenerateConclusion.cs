using life_quality_back.Data.Models;
using Microsoft.AspNetCore.Http;

namespace life_quality_back.Controllers.Conclusion
{
    public class GenerateConclusion
    {
        private readonly string _questionaryName;
        private readonly List<ResultsPatientAnswer> _results;
        private readonly string conclusion = "The conclusion for this result is: ";

        public GenerateConclusion(string questionaryName, List<ResultsPatientAnswer> results)
        {
            _questionaryName = questionaryName;
            _results = results;
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
            if (_questionaryName.Equals("WOMAC (Western Ontario and McMaster Universities Osteoarthritis Index)"))
            {
                return ConclusionWOMAC();
            }
            if (_questionaryName.Equals("Knee Injury and Osteoarthritis Outcome Score (KOOS)"))
            {
                return ConclusionKOOS();
            }
            if (_questionaryName.Equals("HOOS (Hip disability and Osteoarthritis Outcome Score)"))
            {
                return ConclusionHOOS();
            }

            return string.Empty;
        }
        
        private string ConclusionNIHSS()
        {
            int score = CalculationPoints(_results);

            if (score == 0)
            {
                return conclusion + "No stroke symptoms!";
            }
            if (score >= 1 && score <= 4)
            {
                return conclusion + "Mild stroke!";
            }
            if (score >= 5 && score <= 15)
            {
                return conclusion + "Moderate stroke!";
            }
            if (score >= 16 && score <= 20)
            {
                return conclusion + "Moderate to severe stroke!";
            }
            if (score >= 21 && score <= 42)
            {   
                return conclusion + "Severe stroke!";
            }

            return string.Empty;
        }
        private string ConclusionBI()
        {
            int score = CalculationPoints(_results);

            if (score == 100)
            {
                return conclusion + "The patient is considered completely independent and able to perform all daily activities without assistance!";
            }
            if (score >= 90 && score <= 99)
            {
                return conclusion + "Mild to moderate limitations in performing some daily tasks, but the patient remains fairly independent!";
            }
            if (score >= 60 && score <= 89)
            {
                return conclusion + "Moderate limitations in performing many daily tasks. The patient may need help!";
            }
            if (score >= 40 && score <= 59)
            {
                return conclusion + "Significant limitations in independence. The patient needs a lot of help!";
            }
            if (score >= 0 && score <= 39)
            {
                return conclusion + "High level of dependence. The patient finds it difficult or impossible to perform many daily activities without assistance!";
            }

            return string.Empty;
        }
        private string ConclusionBBS()
        {
            int score = CalculationPoints(_results);

            if (score >= 0 && score <= 20)
            {
                return conclusion + "Uses a wheelchair!";
            }
            if (score > 20 && score <= 40)
            {
                return conclusion + "Walks with help!";
            }
            if (score > 40 && score <= 56)
            {
                return conclusion + "Independent!";
            }

            return string.Empty;
        }
        private string ConclusionPAID()
        {
            double score = CalculationPoints(_results) * 1.25;

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
            int score = CalculationPoints(_results);

            if (score >= 17 && score <= 45)
            {
                return conclusion + "Low level of distress. The patient is probably coping well with the emotional aspects of diabetes!";
            }
            if (score >= 46 && score <= 74)
            {
                return conclusion + "Average level of distress. May indicate a certain level of emotional discomfort and stress, but the patient can still practice managing emotions!";
            }
            if (score >= 75 && score <= 102)
            {
                return conclusion + "High level of distress. Signals a high level of emotional pressure associated with diabetes. The patient may need additional psychological support and stress management!";
            }

            return string.Empty;
        }
        private string ConclusionITAS()
        {
            int score = CalculationPoints(_results);

            if (score >= 20 && score <= 46)
            {
                return conclusion + "Low levels of distress or negative attitudes!";
            }
            if (score >= 47 && score <= 73)
            {
                return conclusion + "Average level of distress or negative attitude!";
            }
            if (score >= 74 && score <= 100)
            {
                return conclusion + "High levels of distress or negative attitudes!";
            }

            return string.Empty;
        }

        private string ConclusionWOMAC()
        {
            int painScore = CalculationPoints(_results.Take(5).ToList());
            int[] painLimits = { 0, 5, 6, 10, 11, 15, 16, 20 };
            string[] painPossibleConclusions = { "Little to no pain", "Mild pain", "Moderate pain", "Severe pain" };

            string painConclusion = culculateNultiplesConcusion(painScore, painLimits, painPossibleConclusions);


            int stiffnessScore = CalculationPoints(_results.Skip(5).Take(2).ToList());
            int[] stiffnessLimits = { 0, 2, 3, 4, 5, 6, 7, 8 };
            string[] stiffnessPossibleConclusions = { "Little to no stiffness", "Mild stiffness", "Moderate stiffness", "Severe stiffness" };

            string stiffnessConclusion = culculateNultiplesConcusion(stiffnessScore, stiffnessLimits, stiffnessPossibleConclusions);


            int physicalFunctionScore = CalculationPoints(_results.Skip(7).ToList());
            int[] physicalFunctionLimits = { 0, 10, 11, 20, 21, 40, 41, 68 };
            string[] physicalFunctionPossibleConclusions =  { 
                                                                "Little to no difficulty in physical function", 
                                                                "Mild difficulty in physical function",
                                                                "Moderate difficulty in physical function",
                                                                "Severe difficulty in physical function" 
                                                            };

            string physicalFunctionConclusion = culculateNultiplesConcusion(physicalFunctionScore, physicalFunctionLimits, physicalFunctionPossibleConclusions);

            return conclusion + painConclusion + " " + stiffnessConclusion + " " + physicalFunctionConclusion;
        }

        private string ConclusionKOOS()
        {
            int painScore = 100 - (CalculationPoints(_results.Take(9).ToList()) * 100 / 36);
            int[] painLimits = { 0, 25, 26, 50, 51, 75, 76, 100 };
            string[] painPossibleConclusions = { "Severe pain", "Moderate pain", "Mild pain", "Little to no pain" };

            string painConclusion = culculateNultiplesConcusion(painScore, painLimits, painPossibleConclusions);

            int symptomsScore = 100 - (CalculationPoints(_results.Skip(9).Take(7).ToList()) * 100 / 28);
            int[] symptomsLimits = { 0, 25, 26, 50, 51, 75, 76, 100 };
            string[] symptomsPossibleConclusions = { "Severe symptoms", "Moderate symptoms", "Mild symptoms", "Few to no symptoms" };

            string symptomsConclusion = culculateNultiplesConcusion(symptomsScore, symptomsLimits, symptomsPossibleConclusions);

            int activityScore = 100 - (CalculationPoints(_results.Skip(16).Take(17).ToList()) * 100 / 68);
            int[] activityLimits = { 0, 25, 26, 50, 51, 75, 76, 100 };
            string[] activityPossibleConclusions = { "Severe difficulty", "Moderate difficulty", "Mild difficulty", "Little to no difficulty" };

            string activityConclusion = culculateNultiplesConcusion(activityScore, activityLimits, activityPossibleConclusions);

            int sportScore = 100 - (CalculationPoints(_results.Skip(33).Take(5).ToList()) * 100 / 20);
            int[] sportLimits = { 0, 25, 26, 50, 51, 75, 76, 100 };
            string[] sportPossibleConclusions = { "Severe limitations", "Moderate limitations", "Mild limitations", "Few to no limitations" };

            string sportConclusion = culculateNultiplesConcusion(sportScore, sportLimits, sportPossibleConclusions);

            int qqlScore = 100 - (CalculationPoints(_results.Skip(38).ToList()) * 100 / 16);
            int[] qqlLimits = { 0, 25, 26, 50, 51, 75, 76, 100 };
            string[] qqlPossibleConclusions = { "Poor quality of life", "Fair quality of life", "Good quality of life", "Excellent quality of life" };

            string qqlConclusion = culculateNultiplesConcusion(qqlScore, qqlLimits, qqlPossibleConclusions);

            return conclusion + painConclusion + " " + symptomsConclusion + " " + activityConclusion + " " + sportConclusion + " " + qqlConclusion;
        }

        private string ConclusionHOOS()
        {
            int symptomsScore = 100 - (CalculationPoints(_results.Take(5).ToList()) * 100 / 20);
            int[] symptomsLimits = { 0, 25, 26, 50, 51, 75, 76, 100 };
            string[] symptomsPossibleConclusions = { "Severe symptoms", "Moderate symptoms", "Mild symptoms", "Few to no symptoms" };

            string symptomsConclusion = culculateNultiplesConcusion(symptomsScore, symptomsLimits, symptomsPossibleConclusions);

            int painScore = 100 - (CalculationPoints(_results.Skip(5).Take(10).ToList()) * 100 / 40);
            int[] painLimits = { 0, 25, 26, 50, 51, 75, 76, 100 };
            string[] painPossibleConclusions = { "Severe pain", "Moderate pain", "Mild pain", "Little to no pain" };

            string painConclusion = culculateNultiplesConcusion(painScore, painLimits, painPossibleConclusions);

            int activityScore = 100 - (CalculationPoints(_results.Skip(15).Take(17).ToList()) * 100 / 68);
            int[] activityLimits = { 0, 25, 26, 50, 51, 75, 76, 100 };
            string[] activityPossibleConclusions = { "Severe difficulty", "Moderate difficulty", "Mild difficulty", "Little to no difficulty" };

            string activityConclusion = culculateNultiplesConcusion(activityScore, activityLimits, activityPossibleConclusions);

            int sportScore = 100 - (CalculationPoints(_results.Skip(32).Take(4).ToList()) * 100 / 16);
            int[] sportLimits = { 0, 25, 26, 50, 51, 75, 76, 100 };
            string[] sportPossibleConclusions = { "Severe limitations", "Moderate limitations", "Mild limitations", "Few to no limitations" };

            string sportConclusion = culculateNultiplesConcusion(sportScore, sportLimits, sportPossibleConclusions);

            int qqlScore = 100 - (CalculationPoints(_results.Skip(36).ToList()) * 100 / 16);
            int[] qqlLimits = { 0, 25, 26, 50, 51, 75, 76, 100 };
            string[] qqlPossibleConclusions = { "Poor quality of life", "Fair quality of life", "Good quality of life", "Excellent quality of life" };

            string qqlConclusion = culculateNultiplesConcusion(qqlScore, qqlLimits, qqlPossibleConclusions);

            return conclusion + painConclusion + " " + symptomsConclusion + " " + activityConclusion + " " + sportConclusion + " " + qqlConclusion;
        }

        private string culculateNultiplesConcusion(int score, int[] limits, string[] possibleConclusions)
        {
            if (score >= limits[0] && score <= limits[1])
            {
                return possibleConclusions[0];
            }
            if (score >= limits[2] && score <= limits[3])
            {
                return possibleConclusions[1];
            }
            if (score >= limits[4] && score <= limits[5])
            {
                return possibleConclusions[2];
            }
            if (score >= limits[6] && score <= limits[7])
            {
                return possibleConclusions[3];
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
