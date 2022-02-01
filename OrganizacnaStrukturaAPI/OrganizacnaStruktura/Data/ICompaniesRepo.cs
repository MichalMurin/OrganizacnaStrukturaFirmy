using System.Collections.Generic;
using OrganizacnaStruktura.Dtos;
using OrganizacnaStruktura.Models;

namespace OrganizacnaStruktura.Data
{
    public interface ICompaniesRepo
    {
        bool SaveChanges();
        IEnumerable<Company> GetAllItems();
        Company GetItemById(int id);
        Company CreateItem(CompanyCreateDto createdCompany);
        bool UpdateItem(CompanyUpdateDto updatedComapany, Company oldCompany);
        void DeleteItem(Company item);

    }
}