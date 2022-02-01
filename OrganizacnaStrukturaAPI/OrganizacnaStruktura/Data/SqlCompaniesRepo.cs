using System;
using System.Collections.Generic;
using System.Linq;
using OrganizacnaStruktura.Dtos;
using OrganizacnaStruktura.Models;

namespace OrganizacnaStruktura.Data
{
    public class SqlCompaniesRepo : ICompaniesRepo
    {
        //Databázový kontext
        private readonly CompaniesContext _context;

        public SqlCompaniesRepo(CompaniesContext context)
        {
            _context = context;
        }

        //Metóda vytvorí novú firmu, priradí jej riaditeľa a uloží ju do databázy
        public Company CreateItem(CompanyCreateDto createdCompany)
        {
            if(createdCompany == null)
                throw new ArgumentNullException();
            var ceo = _context.Employees.FirstOrDefault( p => p.Id == createdCompany.CeoId);
            if(ceo == null)
                return null;            
            var newCompany = new Company{Name = createdCompany.Name, Ceo = ceo};           
            _context.Companies.Add(newCompany);
            return newCompany;
        }

        //metóda odstráni firmu z databázy
        public void DeleteItem(Company item)
        {
            if(item == null)
                throw new ArgumentNullException(nameof(item));            
            _context.Companies.Remove(item);
        }

        //metóda ktorá vráti všetky firmy v databáze a ich celé štruktúry, každému uzlu priradí vedúceho
        public IEnumerable<Company> GetAllItems()
        {            
            var listOfCompanies = _context.Companies.ToList();
            foreach(var company in listOfCompanies)
        {
                var ceo = _context.Employees.FirstOrDefault(p => p.Id == company.CeoId);
                company.Ceo = ceo;
                var divisions = _context.Divisions.Where(p => p.CompanyId == company.Id).ToList();
                if(divisions.Count() > 0)
                {
                    company.Divisions = new List<Division>();
                    foreach(var division in divisions)
                    {
                        division.HeadOfDivision = _context.Employees.FirstOrDefault(p => p.Id == division.HeadOfDivisionId);
                        var projects = _context.Projects.Where(p => p.DivisionId == division.Id).ToList();
                        division.Projects = new List<Project>();
                        foreach(var proj in projects)
                        {
                            proj.HeadOfProject = _context.Employees.FirstOrDefault(p => p.Id == proj.HeadOfProjectId);
                            var departments = _context.Departments.Where(p => p.ProjectId == proj.Id).ToList();
                            proj.Departments = new List<Department>();
                            foreach(var dep in departments)
                            {
                                dep.HeadOfDepartment = _context.Employees.FirstOrDefault(p => p.Id == dep.HeadOfDepartmentId);
                                proj.Departments.Add(dep);
                            }
                            division.Projects.Add(proj);
                        }
                        company.Divisions.Add(division);
                    }
                }                    
            }
            return listOfCompanies;
        }

        //metóda, ktorá vráti celú štruktúru firmy na základe ID, každému uzlu priradí vedúceho
        public Company GetItemById(int id)
        {
            var company = _context.Companies.FirstOrDefault(p => p.Id == id);
            if(company == null)
                return null;
            var ceo = _context.Employees.FirstOrDefault(p => p.Id == company.CeoId);
            company.Ceo = ceo;
            var divisions = _context.Divisions.Where(p => p.CompanyId == company.Id).ToList();
                if(divisions.Count() > 0)
                {
                    company.Divisions = new List<Division>();
                    foreach(var division in divisions)
                        {
                            division.HeadOfDivision = _context.Employees.FirstOrDefault(p => p.Id == division.HeadOfDivisionId);
                            var projects = _context.Projects.Where(p => p.DivisionId == division.Id).ToList();
                            division.Projects = new List<Project>();
                            foreach(var proj in projects)
                            {
                            proj.HeadOfProject = _context.Employees.FirstOrDefault(p => p.Id == proj.HeadOfProjectId);
                            var departments = _context.Departments.Where(p => p.ProjectId == proj.Id).ToList();
                            proj.Departments = new List<Department>();
                            foreach(var dep in departments)
                            {
                                dep.HeadOfDepartment = _context.Employees.FirstOrDefault(p => p.Id == dep.HeadOfDepartmentId);
                                proj.Departments.Add(dep);
                            }
                            division.Projects.Add(proj);
                        }
                        company.Divisions.Add(division);
                    }
                }
            return company;
        }
    
        //metóda uloží zmeny vykonané v databáze
        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        //metóda upraví firmu na základe nových hodnôt v parametri updatedCompany
        public bool UpdateItem(CompanyUpdateDto updatedComapany, Company oldCompany)
        {
            var old =_context.Companies.FirstOrDefault(p => p.Id == oldCompany.Id);
            var ceo =_context.Employees.FirstOrDefault(p => p.Id == updatedComapany.CeoId);
            if(ceo == null)
                return false;
            old.Name = updatedComapany.Name;
            old.Ceo = ceo;
            _context.Companies.Update(old);
            return true;
        }
    }
}