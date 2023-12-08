using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using life_quality_back.Data.Models;
using life_quality_back.Data.Repositories;
using life_quality_back.Controllers.Conclusion;

namespace life_quality_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultConclusionController : ControllerBase
    {
        private ResultsRepository _repository;

        public ResultConclusionController(ResultsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{questionnaireId}")]
        public async Task<ActionResult<string>> GetResultConclusionById(int questionnaireId)
        {
            var questionnaire = _repository.GetById(questionnaireId);

            var questionnaireName = questionnaire.Questionnaire.QuestionnaireName;

            var results = questionnaire.ResultsPatientAnswers.ToList();

            GenerateConclusion conclusion = new GenerateConclusion(questionnaireName, results);

            return Ok(conclusion.GetConclusion());
        }
    }
}
