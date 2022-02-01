using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using OrganizacnaStruktura.Data;
using OrganizacnaStruktura.Dtos;
using OrganizacnaStruktura.Models;

namespace OrganizacnaStruktura.Controllers
{
    //api/companies
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompaniesRepo _repositary;

        public CompaniesController(ICompaniesRepo repositary)
        {
            _repositary = repositary;
        }

        //GET api/companies
        [HttpGet]
        public ActionResult<IEnumerable<Company>> GetAllCompanies()
        {
            var companiesItems = _repositary.GetAllItems();
            return Ok(companiesItems);
        }

        //GET api/companies/{id}
        [HttpGet("{id}", Name = "GetCompanyById")]
        public ActionResult<Company> GetCompanyById(int id)
        {
            var companyItem = _repositary.GetItemById(id);
            if(companyItem == null)
                return NotFound();
            return Ok(companyItem);
        }

        //POST api/companies
        [HttpPost]
        public ActionResult <Company> CreateCompany(CompanyCreateDto companyCreateDto)
        {
            var companyModel = _repositary.CreateItem(companyCreateDto);
            if(companyModel == null)
                return BadRequest();
             _repositary.SaveChanges();
             return CreatedAtRoute(nameof(GetCompanyById), new {Id = companyModel.Id},companyModel);
        }

        //PUT api/companies/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateCompany(int id, CompanyUpdateDto companyUpdateDto)
        {
            var companyModelFromRepo = _repositary.GetItemById(id);
            if(companyModelFromRepo == null)
                return NotFound();
            if(!_repositary.UpdateItem(companyUpdateDto,companyModelFromRepo))
                return BadRequest();
            _repositary.SaveChanges();
            return NoContent();
        }

        //DELETE api/companies/{id}
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var companyModelFromRepo = _repositary.GetItemById(id);
            if(companyModelFromRepo == null)
                return NotFound();
            _repositary.DeleteItem(companyModelFromRepo);
            _repositary.SaveChanges();
            return NoContent();
        }
    }
}