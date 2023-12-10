using life_quality_back.Controllers.Conclusion;
using life_quality_back.Data;
using life_quality_back.Data.Models;
using life_quality_back.Data.Repositories;
using life_quality_back.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace life_quality_back.Test
{
    public class GenerateConclusionTests
    {
        private static List<Data.Models.ResultsPatientAnswer> GenerateRandomResultsPatientAnswers(int score)
        {
            List<Data.Models.ResultsPatientAnswer> artificialResults = new List<Data.Models.ResultsPatientAnswer>()
            {
                new Data.Models.ResultsPatientAnswer
                {
                    PatientAnswer = new PatientAnswer
                    {
                        QuestionText = "",
                        AnswerText = "",
                        AnswerValue = score
                    }
                }
            };

            return artificialResults;
        }

        [Theory]
        [InlineData("National Institutes of Health stroke scale (NIHSS)", 0, "No stroke symptoms!")]
        [InlineData("National Institutes of Health stroke scale (NIHSS)", 1, "Mild stroke!")]
        [InlineData("National Institutes of Health stroke scale (NIHSS)", 2, "Mild stroke!")]
        [InlineData("National Institutes of Health stroke scale (NIHSS)", 4, "Mild stroke!")]
        [InlineData("National Institutes of Health stroke scale (NIHSS)", 5, "Moderate stroke!")]
        [InlineData("National Institutes of Health stroke scale (NIHSS)", 10, "Moderate stroke!")]
        [InlineData("National Institutes of Health stroke scale (NIHSS)", 15, "Moderate stroke!")]
        [InlineData("National Institutes of Health stroke scale (NIHSS)", 16, "Moderate to severe stroke!")]
        [InlineData("National Institutes of Health stroke scale (NIHSS)", 18, "Moderate to severe stroke!")]
        [InlineData("National Institutes of Health stroke scale (NIHSS)", 20, "Moderate to severe stroke!")]
        [InlineData("National Institutes of Health stroke scale (NIHSS)", 21, "Severe stroke!")]
        [InlineData("National Institutes of Health stroke scale (NIHSS)", 30, "Severe stroke!")]
        [InlineData("National Institutes of Health stroke scale (NIHSS)", 42, "Severe stroke!")]
        public void GetConclusion_ReturnConclusionNIHSS(string questionaryName, int score, string expectedResult)
        {
            //ARRANGE
            List<Data.Models.ResultsPatientAnswer> results = GenerateRandomResultsPatientAnswers(score);
            GenerateConclusion conclusion = new GenerateConclusion(questionaryName, results);

            //ACT
            string result = conclusion.GetConclusion();

            //ASSERT
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("Barthel Index (BI)", 100, "The patient is considered completely independent and able to perform all daily activities without assistance!")]
        [InlineData("Barthel Index (BI)", 99, "Mild to moderate limitations in performing some daily tasks, but the patient remains fairly independent!")]
        [InlineData("Barthel Index (BI)", 95, "Mild to moderate limitations in performing some daily tasks, but the patient remains fairly independent!")]
        [InlineData("Barthel Index (BI)", 90, "Mild to moderate limitations in performing some daily tasks, but the patient remains fairly independent!")]
        [InlineData("Barthel Index (BI)", 88, "Moderate limitations in performing many daily tasks. The patient may need help!")]
        [InlineData("Barthel Index (BI)", 81, "Moderate limitations in performing many daily tasks. The patient may need help!")]
        [InlineData("Barthel Index (BI)", 60, "Moderate limitations in performing many daily tasks. The patient may need help!")]
        [InlineData("Barthel Index (BI)", 59, "Significant limitations in independence. The patient needs a lot of help!")]
        [InlineData("Barthel Index (BI)", 49, "Significant limitations in independence. The patient needs a lot of help!")]
        [InlineData("Barthel Index (BI)", 40, "Significant limitations in independence. The patient needs a lot of help!")]
        [InlineData("Barthel Index (BI)", 39, "High level of dependence. The patient finds it difficult or impossible to perform many daily activities without assistance!")]
        [InlineData("Barthel Index (BI)", 30, "High level of dependence. The patient finds it difficult or impossible to perform many daily activities without assistance!")]
        [InlineData("Barthel Index (BI)", 0, "High level of dependence. The patient finds it difficult or impossible to perform many daily activities without assistance!")]
        public void GetConclusion_ReturnConclusionBI(string questionaryName, int score, string expectedResult)
        {
            //ARRANGE
            List<Data.Models.ResultsPatientAnswer> results = GenerateRandomResultsPatientAnswers(score);
            GenerateConclusion conclusion = new GenerateConclusion(questionaryName, results);

            //ACT
            string result = conclusion.GetConclusion();

            //ASSERT
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("Berg Balance Test (BBS)", 0, "Uses a wheelchair!")]
        [InlineData("Berg Balance Test (BBS)", 10, "Uses a wheelchair!")]
        [InlineData("Berg Balance Test (BBS)", 20, "Uses a wheelchair!")]
        [InlineData("Berg Balance Test (BBS)", 21, "Walks with help!")]
        [InlineData("Berg Balance Test (BBS)", 30, "Walks with help!")]
        [InlineData("Berg Balance Test (BBS)", 40, "Walks with help!")]
        [InlineData("Berg Balance Test (BBS)", 41, "Independent!")]
        [InlineData("Berg Balance Test (BBS)", 48, "Independent!")]
        [InlineData("Berg Balance Test (BBS)", 56, "Independent!")]
        public void GetConclusion_ReturnConclusionBBS(string questionaryName, int score, string expectedResult)
        {
            //ARRANGE
            List<Data.Models.ResultsPatientAnswer> results = GenerateRandomResultsPatientAnswers(score);
            GenerateConclusion conclusion = new GenerateConclusion(questionaryName, results);

            //ACT
            string result = conclusion.GetConclusion();

            //ASSERT
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("Problem Areas In Diabetes (PAID)", 0, "Moderate to severe distress!")]
        [InlineData("Problem Areas In Diabetes (PAID)", 20, "Moderate to severe distress!")]
        [InlineData("Problem Areas In Diabetes (PAID)", 31, "Moderate to severe distress!")]
        [InlineData("Problem Areas In Diabetes (PAID)", 32, "Severe diabetes distress!")]
        [InlineData("Problem Areas In Diabetes (PAID)", 40, "Severe diabetes distress!")]
        [InlineData("Problem Areas In Diabetes (PAID)", 45, "Severe diabetes distress!")]
        public void GetConclusion_ReturnConclusionPAID(string questionaryName, int score, string expectedResult)
        {
            //ARRANGE
            List<Data.Models.ResultsPatientAnswer> results = GenerateRandomResultsPatientAnswers(score);
            GenerateConclusion conclusion = new GenerateConclusion(questionaryName, results);

            //ACT
            string result = conclusion.GetConclusion();

            //ASSERT
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("Diabetes Distress Scale (DDS-17)", 17, "Low level of distress. The patient is probably coping well with the emotional aspects of diabetes!")]
        [InlineData("Diabetes Distress Scale (DDS-17)", 25, "Low level of distress. The patient is probably coping well with the emotional aspects of diabetes!")]
        [InlineData("Diabetes Distress Scale (DDS-17)", 45, "Low level of distress. The patient is probably coping well with the emotional aspects of diabetes!")]
        [InlineData("Diabetes Distress Scale (DDS-17)", 46, "Average level of distress. May indicate a certain level of emotional discomfort and stress, but the patient can still practice managing emotions!")]
        [InlineData("Diabetes Distress Scale (DDS-17)", 56, "Average level of distress. May indicate a certain level of emotional discomfort and stress, but the patient can still practice managing emotions!")]
        [InlineData("Diabetes Distress Scale (DDS-17)", 74, "Average level of distress. May indicate a certain level of emotional discomfort and stress, but the patient can still practice managing emotions!")]
        [InlineData("Diabetes Distress Scale (DDS-17)", 75, "High level of distress. Signals a high level of emotional pressure associated with diabetes. The patient may need additional psychological support and stress management!")]
        [InlineData("Diabetes Distress Scale (DDS-17)", 89, "High level of distress. Signals a high level of emotional pressure associated with diabetes. The patient may need additional psychological support and stress management!")]
        [InlineData("Diabetes Distress Scale (DDS-17)", 102, "High level of distress. Signals a high level of emotional pressure associated with diabetes. The patient may need additional psychological support and stress management!")]

        public void GetConclusion_ReturnConclusionDDS_17(string questionaryName, int score, string expectedResult)
        {
            //ARRANGE
            List<Data.Models.ResultsPatientAnswer> results = GenerateRandomResultsPatientAnswers(score);
            GenerateConclusion conclusion = new GenerateConclusion(questionaryName, results);

            //ACT
            string result = conclusion.GetConclusion();

            //ASSERT
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("Insulin Treatment Appraisal Scale (ITAS)", 20, "Low levels of distress or negative attitudes!")]
        [InlineData("Insulin Treatment Appraisal Scale (ITAS)", 35, "Low levels of distress or negative attitudes!")]
        [InlineData("Insulin Treatment Appraisal Scale (ITAS)", 46, "Low levels of distress or negative attitudes!")]
        [InlineData("Insulin Treatment Appraisal Scale (ITAS)", 47, "Average level of distress or negative attitude!")]
        [InlineData("Insulin Treatment Appraisal Scale (ITAS)", 59, "Average level of distress or negative attitude!")]
        [InlineData("Insulin Treatment Appraisal Scale (ITAS)", 73, "Average level of distress or negative attitude!")]
        [InlineData("Insulin Treatment Appraisal Scale (ITAS)", 74, "High levels of distress or negative attitudes!")]
        [InlineData("Insulin Treatment Appraisal Scale (ITAS)", 89, "High levels of distress or negative attitudes!")]
        [InlineData("Insulin Treatment Appraisal Scale (ITAS)", 100, "High levels of distress or negative attitudes!")]

        public void GetConclusion_ReturnConclusionITAS(string questionaryName, int score, string expectedResult)
        {
            //ARRANGE
            List<Data.Models.ResultsPatientAnswer> results = GenerateRandomResultsPatientAnswers(score);
            GenerateConclusion conclusion = new GenerateConclusion(questionaryName, results);

            //ACT
            string result = conclusion.GetConclusion();

            //ASSERT
            Assert.Equal(expectedResult, result);
        }
    }
}
