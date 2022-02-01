using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using OrganizacnaStruktura.Dtos;
using OrganizacnaStruktura.Models;

namespace OrganizacnaStruktura.Data
{
    public class SqlDepartmentRepo : IDepartmentsRepo
    {
        //Databázový kontext
        private readonly CompaniesContext _context;

        public SqlDepartmentRepo(CompaniesContext context)
        {
            _context = context;
        }

        //metóda, ktorá vytvorí nové oddelenie na základe hodnôt v parametri, pridelí mu vedúceho a oddelenie zaradí do projektu
        public Department CreateItem(DepartmentCreateDto createdDepartment)
        {
            if(createdDepartment == null)
                throw new ArgumentNullException();
            var headOfDepartment = _context.Employees.FirstOrDefault(p => p.Id == createdDepartment.HeadOfDepartmentId);
            if(headOfDepartment == null)
                return null;
            var newDepartment = new Department{Name = createdDepartment.Name, HeadOfDepartment = headOfDepartment};
            var project = _context.Projects.FirstOrDefault(p => p.Id == createdDepartment.ProjectId);
            if(project != null)
            {
                if(project.Departments == null)
                    project.Departments = new List<Department>();
                project.Departments.Add(newDepartment);
                _context.Projects.Update(project);
            }
            _context.Departments.Add(newDepartment);
            return newDepartment;
        }

        //metóda, ktorá vymaže oddelenie z databázy
        public void DeleteItem(Department item)
        {
            if(item == null)
                throw new ArgumentNullException(nameof(item));
            _context.Departments.Remove(item);
        }

        //metóda, ktorá vráti zoznam všetkých oddelení
        public IEnumerable<Department> GetAllItems()
        {
            var listOfDepartments = _context.Departments.ToList();
            foreach(var department in listOfDepartments)
            {
                var headOfDepartment = _context.Employees.FirstOrDefault(p => p.Id == department.HeadOfDepartmentId);
                department.HeadOfDepartment = headOfDepartment;
            }
            return listOfDepartments;
        }

        //metóda, ktorá vráti konkrétne oddelenie, na základe ID
        public Department GetItemById(int id)
        {
            var department = _context.Departments.FirstOrDefault(p => p.Id == id);
            if(department == null)
                return null;
            var headOfDepartment = _context.Employees.FirstOrDefault(p => p.Id == department.HeadOfDepartmentId);
            department.HeadOfDepartment = headOfDepartment;
            return department;
        }

        //metóda, ktorá uloží zmeny vykonané v databáze
        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        //metóda, ktorá upraví oddelenie, na základe parametru updatedDepartment
        public bool UpdateItem(DepartmentUpdateDto updatedDepartment, Department oldDepartment)
        {
            var old =_context.Departments.FirstOrDefault(p => p.Id == oldDepartment.Id);
            var headOfDepartment =_context.Employees.FirstOrDefault(p => p.Id == updatedDepartment.HeadOfDepartmentId);
            if(headOfDepartment == null)
                return false;
            var project = _context.Projects.FirstOrDefault(p => p.Id == old.ProjectId);
            if(project != null)
            {
                project.Departments.Remove(old);
                _context.Projects.Update(project);
            }
            if(old.ProjectId != updatedDepartment.ProjectId)
                project = _context.Projects.FirstOrDefault(p => p.Id == updatedDepartment.ProjectId);
            old.Name = updatedDepartment.Name;
            old.HeadOfDepartment = headOfDepartment;
            old.ProjectId = updatedDepartment.ProjectId;
            if(project != null)
            {
                if(project.Departments == null)
                    project.Departments = new List<Department>();
                project.Departments.Add(old);
                _context.Projects.Update(project);
                _context.Departments.Update(old);
                return true;
            }
            return false;
        }
    }
}