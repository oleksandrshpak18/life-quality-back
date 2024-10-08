﻿using life_quality_back.Data.Models;
using life_quality_back.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace life_quality_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiseaseController : ControllerBase
    {
        private DiseaseRepository _repository;

        public DiseaseController(DiseaseRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Disease>>> GetAll()
        {
            return Ok(_repository.GetAll());
        }

        [HttpGet("{id}")]
        private async Task<ActionResult<List<Disease>>> GetById(int id)
        {
            var res = _repository.GetById(id);
            return res == null ? BadRequest($"Disease with id {id} not found") : Ok(res);
        }
    }
}
