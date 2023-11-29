using life_quality_back.Data.Models;
using life_quality_back.Data.Repositories;
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
        public async Task<ActionResult<List<Data.Models.Results>>> GetAll()
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
        public async Task<ActionResult<List<Data.Models.Results>>> GetAllByDoctorId(int doctorId)
        {
            int t = doctorId;
            var res = _repository.GetAllByDoctorId(doctorId);
            return ((res == null) || (!res.Any())) ? BadRequest($"Results for this doctor {doctorId} not found") : Ok(res);
        }
    }
}
