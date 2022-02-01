using System.Collections.Generic;
using OrganizacnaStruktura.Dtos;
using OrganizacnaStruktura.Models;

namespace OrganizacnaStruktura.Data
{
    public interface IDepartmentsRepo
    {
        bool SaveChanges();
        IEnumerable<Department> GetAllItems();
        Department GetItemById(int id);
        Department CreateItem(DepartmentCreateDto createdDepartment);
        bool UpdateItem(DepartmentUpdateDto updatedDepartment, Department oldDepartment);
        void DeleteItem(Department item);
    }
}