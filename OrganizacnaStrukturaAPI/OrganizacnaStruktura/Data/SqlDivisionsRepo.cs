using System;
using System.Collections.Generic;
using System.Linq;
using OrganizacnaStruktura.Dtos;
using OrganizacnaStruktura.Models;

namespace OrganizacnaStruktura.Data
{
    public class SqlDivisionsRepo : IDivisionRepo
    {
        //databázový kontext
        private readonly CompaniesContext _context;

        public SqlDivisionsRepo(CompaniesContext context)
        {
            _context = context;
        }

        //metóda, ktorá vytvorí divíziu, pridelí jej vedúceho a priradí divíziu k firme
        public Division CreateItem(DivisionCreateDto createdDivision)
        {
            if(createdDivision == null)
                throw new ArgumentNullException();
            var headOfDivision = _context.Employees.FirstOrDefault(p => p.Id == createdDivision.HeadOfDivisionId);
            if(headOfDivision == null)
                return null;
            var newDivision = new Division{Name = createdDivision.Name, HeadOfDivision = headOfDivision};
            var company = _context.Companies.FirstOrDefault(p => p.Id == createdDivision.CompanyId);
            if(company != null)
            {
                if(company.Divisions == null)
                    company.Divisions = new List<Division>();
                company.Divisions.Add(newDivision);
                _context.Companies.Update(company);
            }
            _context.Divisions.Add(newDivision);
            return newDivision;
        }

        //metóda, ktorá vymaže divíziu z databázy
        public void DeleteItem(Division item)
        {
            if(item == null)
                throw new ArgumentNullException(nameof(item));
            _context.Divisions.Remove(item);
        }

        //metóda, ktorá vráti zoznam všetkých divízií v databáze
        public IEnumerable<Division> GetAllItems()
        {
            var listOfDivisions = _context.Divisions.ToList();
             foreach(var division in listOfDivisions)
             {
                 var headOfDivision = _context.Employees.FirstOrDefault(p => p.Id == division.HeadOfDivisionId);
                division.HeadOfDivision = headOfDivision;
                var projects = _context.Projects.Where(p => p.DivisionId == division.Id).ToList();
                if(projects.Count() > 0)
                    division.Projects = new List<Project>();
                foreach(var project in projects)
                {
                    project.HeadOfProject = _context.Employees.FirstOrDefault(p => p.Id == project.HeadOfProjectId);
                    var departments = _context.Departments.Where(p => p.ProjectId == project.Id).ToList();
                    if(departments.Count() > 0)
                    {
                        project.Departments = new List<Department>();
                        foreach(var dep in departments)
                        {
                            dep.HeadOfDepartment = _context.Employees.FirstOrDefault(p => p.Id == dep.HeadOfDepartmentId);
                            project.Departments.Add(dep);
                        }      
                    }
                    division.Projects.Add(project);
                }       
            }
            return listOfDivisions;
        }

        //metóda, ktorá vráti konkrétnu divíziu na základe ID
        public Division GetItemById(int id)
        {
            var division = _context.Divisions.FirstOrDefault(p => p.Id == id);
            if(division == null)
                return null;
            var headOfDivision = _context.Employees.FirstOrDefault(p => p.Id == division.HeadOfDivisionId);
            division.HeadOfDivision = headOfDivision;
            var projects = _context.Projects.Where(p => p.DivisionId == division.Id).ToList();
                if(projects.Count() > 0)
                    division.Projects = new List<Project>();
                foreach(var project in projects)
                {
                    project.HeadOfProject = _context.Employees.FirstOrDefault(p => p.Id == project.HeadOfProjectId);
                    var departments = _context.Departments.Where(p => p.ProjectId == project.Id).ToList();
                    if(departments.Count() > 0)
                    {
                        project.Departments = new List<Department>();
                        foreach(var dep in departments)
                        {
                            dep.HeadOfDepartment = _context.Employees.FirstOrDefault(p => p.Id == dep.HeadOfDepartmentId);
                            project.Departments.Add(dep);
                        }
                          
                    }
                    division.Projects.Add(project);
                }
            return division;
        }

        //metóda, ktorá uloží zmeny vykonané v databáze
        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        //metóda, ktorá upraví divíziu na základe parametra updatedDivision
        public bool UpdateItem(DivisionUpdateDto updatedDivision, Division oldDivision)
        {
            var old =_context.Divisions.FirstOrDefault(p => p.Id == oldDivision.Id);
            var headOfDivision =_context.Employees.FirstOrDefault(p => p.Id == updatedDivision.HeadOfDivisionId);
            if(headOfDivision == null)
                return false;
            var company = _context.Companies.FirstOrDefault(p => p.Id == old.CompanyId);
            if(company != null)
            {
                company.Divisions.Remove(old);
                _context.Companies.Update(company);
            }
            if(old.CompanyId != updatedDivision.CompanyId)
                company = _context.Companies.FirstOrDefault(p => p.Id == updatedDivision.CompanyId);
            old.Name = updatedDivision.Name;
            old.HeadOfDivision = headOfDivision;
            old.CompanyId = updatedDivision.CompanyId;
            if(company != null)
            {
                if(company.Divisions == null)
                    company.Divisions = new List<Division>();
                company.Divisions.Add(old);
                _context.Companies.Update(company);
                _context.Divisions.Update(old);
                return true;
            }
            return false;
        }
    }
}