using life_quality_back.Data.Models;
using life_quality_back.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace life_quality_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private DoctorRepository _repository;

        public DoctorController(DoctorRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        private async Task<ActionResult<List<Doctor>>> GetAll()
        {
            return Ok(_repository.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Doctor>>> GetById(int id)
        {
            var res = _repository.GetById(id);
            return res == null ? BadRequest($"Doctor with id {id} not found") : Ok(res);
        }
    }
}
