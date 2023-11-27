using life_quality_back.Data.Models;
using life_quality_back.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace life_quality_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TreatmentStrategyController : ControllerBase
    {
        private TreatmentStrategyRepository _repository;

        public TreatmentStrategyController(TreatmentStrategyRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<TreatmentStrategy>>> GetAll()
        {
            return Ok(_repository.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<TreatmentStrategy>>> GetById(int id)
        {
            var res = _repository.GetById(id);
            return res == null ? BadRequest($"Treatment strategy with id {id} not found") : Ok(res);
        }
    }
}
