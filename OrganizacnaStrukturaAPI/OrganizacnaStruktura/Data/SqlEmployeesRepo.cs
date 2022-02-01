using System;
using System.Collections.Generic;
using System.Linq;
using OrganizacnaStruktura.Models;

namespace OrganizacnaStruktura.Data
{
    public class SqlEmployeesRepo : IEmployeesRepo
    {
        //databázový kontext
        private readonly CompaniesContext _context;

        public SqlEmployeesRepo(CompaniesContext context)
        {
            _context = context;
        }

        //metóda, ktorá vytvorí zamestnanca a pridá ho do databázy
        public void CreateItem(Employee item)
        {
            if(item == null)
                throw new ArgumentNullException(nameof(item));
            _context.Employees.Add(item);
        }

        //metóda, ktorá vymaže zamestnanca z databázy
        public void DeleteItem(Employee item)
        {
            if(item == null)
                throw new ArgumentNullException(nameof(item));
            _context.Employees.Remove(item);
        }

        //metóda, ktorá vráti všetkých zamestnancov
        public IEnumerable<Employee> GetAllItems()
        {
            return _context.Employees.ToList();
        }

        //metóda, ktorá vráti konkrétneho zamestnanca na základe ID
        public Employee GetItemById(int id)
        {
            return _context.Employees.FirstOrDefault(p => p.Id == id);
        }

        //metóda, ktorá uloží zmeny vykonané v databáze
        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}