using System;
using System.Collections.Generic;
using System.Linq;
using OrganizacnaStruktura.Dtos;
using OrganizacnaStruktura.Models;

namespace OrganizacnaStruktura.Data
{
    public class SqlProjectsRepo : IProjectsRepo
    {
        //databázový kontext
        private readonly CompaniesContext _context;

        public SqlProjectsRepo(CompaniesContext context)
        {
            _context = context;
        }

        //metóda, ktorá výtvorí projekt a priradí ho k divízii
        public Project CreateItem(ProjectCreateDto createdProject)
        {
            if(createdProject == null)
               throw new ArgumentNullException();
            var headOfProject = _context.Employees.FirstOrDefault(p => p.Id == createdProject.HeadOfProjectId);
            if(headOfProject == null)
                return null;
            var newProject = new Project{Name = createdProject.Name, HeadOfProject = headOfProject};
            var division = _context.Divisions.FirstOrDefault(p => p.Id == createdProject.DivisionId);
            if(division != null)
            {
                if(division.Projects == null)
                    division.Projects = new List<Project>();
                division.Projects.Add(newProject);
                _context.Divisions.Update(division);
            }
            _context.Projects.Add(newProject);
            return newProject;
        }
        
        //metóda, ktorá vymaže projekt a jeho oddelenia
        public void DeleteItem(Project item)
        {
            if(item == null)
                throw new ArgumentNullException(nameof(item));
            var departments = _context.Departments.Where( p => p.ProjectId == item.Id);
            foreach(var department in departments)
                _context.Departments.Remove(department);
            _context.Projects.Remove(item);
        }

        //metóda, ktorá vráti zoznam všetkých projektov
        public IEnumerable<Project> GetAllItems()
        {
            var listOfProjects = _context.Projects.ToList();
            foreach(var project in listOfProjects)
            {
                var headOfProject = _context.Employees.FirstOrDefault(p => p.Id == project.HeadOfProjectId);
                project.HeadOfProject = headOfProject;
                var departments = _context.Departments.Where(p => p.ProjectId == project.Id).ToList();
                if(departments.Count() > 0)
                    project.Departments = new List<Department>();
                foreach(var department in departments)
                {
                    department.HeadOfDepartment = _context.Employees.FirstOrDefault(p => p.Id == department.HeadOfDepartmentId);
                    project.Departments.Add(department);
                }        
            }
            return listOfProjects;
        }

        //metóda, ktorá vráti konkrétny projekt, na základe ID
        public Project GetItemById(int id)
        {
            var project = _context.Projects.FirstOrDefault(p => p.Id == id);
            if(project == null)
                return null;
            var headOfProject = _context.Employees.FirstOrDefault(p => p.Id == project.HeadOfProjectId);
            project.HeadOfProject = headOfProject;
            var departments = _context.Departments.Where(p => p.ProjectId == id).ToList();
            if(departments.Count() > 0)
            {
                project.Departments = new List<Department>();                
                foreach(var department in departments)
                {
                    department.HeadOfDepartment = _context.Employees.FirstOrDefault(p => p.Id == department.HeadOfDepartmentId);
                    project.Departments.Add(department);
                }        
            }   
            return project;
        }

        //metóda, ktorá uloží zmeny vykonané v databáze
        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        //metóda, ktorá upraví projekt na základe parametra updatedProject
        public bool UpdateItem(ProjectUpdateDto updatedProject, Project oldProject)
        {
            var old =_context.Projects.FirstOrDefault(p => p.Id == oldProject.Id);
            var headOfProject =_context.Employees.FirstOrDefault(p => p.Id == updatedProject.HeadOfProjectId);
            if(headOfProject == null)
                return false;

            var division = _context.Divisions.FirstOrDefault(p => p.Id == old.DivisionId);
            if(division != null)
            {
                division.Projects.Remove(old);
                _context.Divisions.Update(division);
            }
            if(old.DivisionId != updatedProject.DivisionId)
                division = _context.Divisions.FirstOrDefault(p => p.Id == updatedProject.DivisionId);
            old.Name = updatedProject.Name;
            old.HeadOfProject = headOfProject;
            old.DivisionId = updatedProject.DivisionId;
            if(division != null)
            {
                if(division.Projects == null)
                    division.Projects = new List<Project>();
                division.Projects.Add(old);
                _context.Divisions.Update(division);
                _context.Projects.Update(old);
                return true;
            }
            return false;
        }
    }
}