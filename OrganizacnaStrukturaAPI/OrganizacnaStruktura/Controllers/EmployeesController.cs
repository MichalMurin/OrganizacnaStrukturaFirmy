using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrganizacnaStruktura.Data;
using OrganizacnaStruktura.Dtos;
using OrganizacnaStruktura.Models;

namespace OrganizacnaStruktura.Controllers
{
    //api/employees
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesRepo _repositary;
        private readonly IMapper _mapper;

        public EmployeesController(IEmployeesRepo repositary, IMapper mapper)
        {
            _repositary = repositary;
            _mapper = mapper;
        }

        //GET api/employees
        [HttpGet]
        public ActionResult<IEnumerable<Employee>> GetAllEmployees()
        {
            var employeesItems = _repositary.GetAllItems();
            return Ok(employeesItems);
        }

        //GET api/employees/{id}
        [HttpGet("{id}", Name = "GetEmployeeById")]
        public ActionResult<Employee> GetEmployeeById(int id)
        {
            var employeeItem = _repositary.GetItemById(id);
            if(employeeItem == null)
                return NotFound();
            return Ok(employeeItem);
        }

        //POST api/employees
        [HttpPost]
        public ActionResult <Employee> CreateEmployee(EmployeeCreateDto employeeCreateDto)
        {
             var employeeModel = _mapper.Map<Employee>(employeeCreateDto);
             _repositary.CreateItem(employeeModel);
             _repositary.SaveChanges();
             return CreatedAtRoute(nameof(GetEmployeeById), new {Id = employeeModel.Id},employeeModel);
        }

        //PUT api/employees/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateEmployee(int id, EmployeeUpdateDto employeeUpdateDto)
        {
            var employeeModelFromRepo = _repositary.GetItemById(id);
            if(employeeModelFromRepo == null)
                return NotFound();
            _mapper.Map(employeeUpdateDto, employeeModelFromRepo);
            _repositary.SaveChanges();
            return NoContent();
        }

        //DELETE api/employees/{id}
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var employeeModelFromRepo = _repositary.GetItemById(id);
            if(employeeModelFromRepo == null)
                return NotFound();
            _repositary.DeleteItem(employeeModelFromRepo);
            _repositary.SaveChanges();
            return NoContent();
        }
    }
}