using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using OrganizacnaStruktura.Data;
using OrganizacnaStruktura.Dtos;
using OrganizacnaStruktura.Models;

namespace OrganizacnaStruktura.Controllers
{
    //api/divisions
    [Route("api/[controller]")]
    [ApiController]
    public class DivisionsController : ControllerBase
    {
        private readonly IDivisionRepo _repositary;

        public DivisionsController(IDivisionRepo repositary)
        {
            _repositary = repositary;
        }

        //GET api/divisions
        [HttpGet]
        public ActionResult<IEnumerable<Division>> GetAllDivisions()
        {
            var divisionsItems = _repositary.GetAllItems();
            return Ok(divisionsItems);
        }

        //GET api/divisions/{id}
        [HttpGet("{id}", Name = "GetDivisionById")]
        public ActionResult<Division> GetDivisionById(int id)
        {
            var divisionItem = _repositary.GetItemById(id);
            if(divisionItem == null)
                return NotFound();
            return Ok(divisionItem);
        }

        //POST api/divisions
        [HttpPost]
        public ActionResult <Division> CreateDivision(DivisionCreateDto divisionCreateDto)
        {
             var divisionModel = _repositary.CreateItem(divisionCreateDto);
             if(divisionModel == null)
                return BadRequest();
             _repositary.SaveChanges();
             return CreatedAtRoute(nameof(GetDivisionById), new {Id = divisionModel.Id},divisionModel);
        }

        //PUT api/divisions/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateDivision(int id, DivisionUpdateDto divisionUpdateDto)
        {
            var divisionModelFromRepo = _repositary.GetItemById(id);
            if(divisionModelFromRepo == null)
                return NotFound();
            if(!_repositary.UpdateItem(divisionUpdateDto,divisionModelFromRepo))
                return BadRequest();
            _repositary.SaveChanges();
            return NoContent();
        }

        //DELETE api/divisions/{id}
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var divisionModelFromRepo = _repositary.GetItemById(id);
            if(divisionModelFromRepo == null)
                return NotFound();
            _repositary.DeleteItem(divisionModelFromRepo);
            _repositary.SaveChanges();
            return NoContent();
        }
    }
}