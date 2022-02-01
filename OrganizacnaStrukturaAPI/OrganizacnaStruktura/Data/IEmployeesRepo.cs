using System.Collections.Generic;
using OrganizacnaStruktura.Models;

namespace OrganizacnaStruktura.Data
{
    public interface IEmployeesRepo
    {
        bool SaveChanges();
        IEnumerable<Employee> GetAllItems();
        Employee GetItemById(int id);
        void CreateItem(Employee item);
        void DeleteItem(Employee item);
    }
}