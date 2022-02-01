using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using OrganizacnaStruktura.Data;
using OrganizacnaStruktura.Dtos;
using OrganizacnaStruktura.Models;

namespace OrganizacnaStruktura.Controllers
{
    //api/projects
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectsRepo _repositary;

        public ProjectsController(IProjectsRepo repositary)
        {
            _repositary = repositary;
        }

        //GET api/projects
        [HttpGet]
        public ActionResult<IEnumerable<Project>> GetAllProjects()
        {
            var projectsItems = _repositary.GetAllItems();
            return Ok(projectsItems);
        }

        //GET api/projects/{id}
        [HttpGet("{id}", Name = "GetProjectById")]
        public ActionResult<Project> GetProjectById(int id)
        {
            var projectItem = _repositary.GetItemById(id);
            if(projectItem == null)
                return NotFound();
            return Ok(projectItem);
        }

        //POST api/projects
        [HttpPost]
        public ActionResult <Project> CreateProject(ProjectCreateDto projectCreateDto)
        {
             var projectModel = _repositary.CreateItem(projectCreateDto);
             if(projectModel == null)
                return BadRequest();
             _repositary.SaveChanges();
             return CreatedAtRoute(nameof(GetProjectById), new {Id = projectModel.Id},projectModel);
        }

        //PUT api/projects/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateProject(int id, ProjectUpdateDto projectUpdateDto)
        {
            var projectModelFromRepo = _repositary.GetItemById(id);
            if(projectModelFromRepo == null)
                return NotFound();
            if(!_repositary.UpdateItem(projectUpdateDto, projectModelFromRepo))
                return BadRequest();
            _repositary.SaveChanges();
            return NoContent();
        }

        //DELETE api/projects/{id}
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var projectModelFromRepo = _repositary.GetItemById(id);
            if(projectModelFromRepo == null)
                return NotFound();
            _repositary.DeleteItem(projectModelFromRepo);
            _repositary.SaveChanges();
            return NoContent();
        }
    }
}