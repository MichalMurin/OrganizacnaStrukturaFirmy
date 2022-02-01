using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using OrganizacnaStruktura.Data;
using OrganizacnaStruktura.Dtos;
using OrganizacnaStruktura.Models;

namespace OrganizacnaStruktura.Controllers
{
    //api/departments
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentsRepo _repositary;

        public DepartmentsController(IDepartmentsRepo repositary)
        {
            _repositary = repositary;
        }

        //GET api/departments
        [HttpGet]
        public ActionResult<IEnumerable<Department>> GetAllDepartments()
        {
            var departmentItems = _repositary.GetAllItems();
            return Ok(departmentItems);
        }

        //GET api/departments/{id}
        [HttpGet("{id}", Name = "GetDepartmentById")]
        public ActionResult<Department> GetDepartmentById(int id)
        {
            var departmentItem = _repositary.GetItemById(id);
            if(departmentItem == null)
                return NotFound();
            
            return Ok(departmentItem);
        }

        //POST api/departments
        [HttpPost]
        public ActionResult <Department> CreateDepartment(DepartmentCreateDto departmentCreateDto)
        {
             var departmentModel = _repositary.CreateItem(departmentCreateDto);
             if(departmentModel == null)
                return BadRequest();
             _repositary.SaveChanges();
             return CreatedAtRoute(nameof(GetDepartmentById), new {Id = departmentModel.Id},departmentModel);
        }

        //PUT api/departments/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateDepartment(int id, DepartmentUpdateDto departmentUpdateDto)
        {
            var departmentModelFromRepo = _repositary.GetItemById(id);
            if(departmentModelFromRepo == null)
                return NotFound();
            if(!_repositary.UpdateItem(departmentUpdateDto, departmentModelFromRepo))
                return BadRequest();
            _repositary.SaveChanges();
            return NoContent();
        }

        //DELETE api/departments/{id}
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var departmentModelFromRepo = _repositary.GetItemById(id);
            if(departmentModelFromRepo == null)
                return NotFound();
            _repositary.DeleteItem(departmentModelFromRepo);
            _repositary.SaveChanges();
            return NoContent();
        }
    }
}