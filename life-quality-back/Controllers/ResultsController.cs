using life_quality_back.Data.Filtering;
using life_quality_back.Data.Models;
using life_quality_back.Data.Repositories;
using life_quality_back.Data.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace life_quality_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultsController : ControllerBase
    {
        private ResultsRepository _repository;
        public ResultsController(ResultsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<ResultsVM>>> GetAll()
        {
            return Ok(_repository.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Data.Models.Results>> GetById(int id)
        {
            var res = _repository.GetById(id);
            return res == null ? BadRequest($"Results with id {id} not found") : Ok(res);
        }

        [HttpGet("doctor/{doctorId}")]
        public async Task<ActionResult<List<ResultsVM>>> GetAllByDoctorId(int doctorId)
        {
            var res = _repository.GetAllByDoctorId(doctorId);
            return ((res == null) || (!res.Any())) ? BadRequest($"Results for this doctor {doctorId} not found") : Ok(res);
        }

        [HttpPut("toggle-saved/{id}")]
        public async Task<ActionResult<Data.Models.Results>> ToggleSavedResult(int id)
        {
            var res = _repository.ToggleSavedResult(id);
            return res == null ? BadRequest($"Error updating result with id = {id}") : Ok(res);
        }

        [HttpGet("filter")]
        public async Task<ActionResult<List<ResultsVM>>> GetFiltered(
            [FromQuery] int doctorId = -1,
            [FromQuery] DateTime? beginTime = null,
            [FromQuery] DateTime? endTime = null,
            [FromQuery] string ?gender = null,
            [FromQuery] string ?diseaseName = null,
            [FromQuery] int? minAge = null,
            [FromQuery] int? maxAge = null,
            [FromQuery] string ?questionnaireName = null)
        {
            if(doctorId <= -1)
            {
                return BadRequest($"Parameter doctorId is required. Actual value is: {doctorId}");
            }
            FilterParameters filterParameters = new FilterParametersBuilder()
                .SetBeginDate(beginTime)
                .SetEndDate(endTime)
                .SetGender(gender)
                .SetDiseaseName(diseaseName)
                .SetMinAge(minAge)
                .SetMaxAge(maxAge)
                .SetQuestionnaireName(questionnaireName)
                .SetDoctorId(doctorId) 
                .Build();
            
            return Ok(_repository.GetFiltered(filterParameters));
        }

        [HttpGet("saved-results/{doctorId}")]
        public async Task<ActionResult<List<string>>> GetTypesOfSavedResults(int doctorId)
        {
            var savedQuestionnaireNames = _repository.GetDoctorSavedQuestionnaireNames(doctorId);

            return savedQuestionnaireNames == null ? BadRequest($"Error updating result with doctor id = {doctorId}") : Ok(savedQuestionnaireNames);

        }

        [HttpGet("saved-results/{doctorId}/{questionnaireName}")]
        public async Task<ActionResult<List<string>>> GetPatientsSavedResultsByQuestionnaireName(int doctorId, string questionnaireName)
        {
            var savedQuestionnaireNames = _repository.GetDoctorSavedResultsByQuestionnaireName(doctorId, questionnaireName);

            return savedQuestionnaireNames == null ? BadRequest($"Error updating result with doctor id = {doctorId}") : Ok(savedQuestionnaireNames);

        }
    }
}
