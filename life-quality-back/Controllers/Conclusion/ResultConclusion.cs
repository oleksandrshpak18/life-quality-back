using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using life_quality_back.Data.Models;
using life_quality_back.Data.Repositories;
using life_quality_back.Controllers.Conclusion;

namespace life_quality_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultConclusion : ControllerBase
    {
        private ResultsRepository _repository;

        public ResultConclusion(ResultsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<string>> GetResultConclusionById(int id)
        {
            var questionnaire = _repository.GetById(id);

            var questionnaireName = questionnaire.Questionnaire.QuestionnaireName;

            var results = questionnaire.ResultsPatientAnswers.ToList();

            GenerateConclusion conclusion = new GenerateConclusion(questionnaireName, results);

            return Ok(conclusion.GetConclusion());
        }
    }
}
