using life_quality_back.Data.Models;
using life_quality_back.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace life_quality_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionnaireController : ControllerBase
    {
        private QuestionnaireRepository _repository;

        public QuestionnaireController(QuestionnaireRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Questionnaire>>> GetAll()
        {
            return Ok(_repository.GetAll());
        }

        [HttpGet("{id}")]
        private async Task<ActionResult<Questionnaire>> GetById(int id)
        {
            var res = _repository.GetById(id);
            return res == null ? BadRequest($"Doctor with id {id} not found") : Ok(res);
        }
    }
}
