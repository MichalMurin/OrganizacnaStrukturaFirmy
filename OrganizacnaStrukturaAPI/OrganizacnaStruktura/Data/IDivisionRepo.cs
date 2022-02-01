using System.Collections.Generic;
using OrganizacnaStruktura.Dtos;
using OrganizacnaStruktura.Models;

namespace OrganizacnaStruktura.Data
{
    public interface IDivisionRepo
    {
        bool SaveChanges();
        IEnumerable<Division> GetAllItems();
        Division GetItemById(int id);
        Division CreateItem(DivisionCreateDto createdDivision);
        bool UpdateItem(DivisionUpdateDto updatedDivision, Division oldDivision);
        void DeleteItem(Division item);
    }
}