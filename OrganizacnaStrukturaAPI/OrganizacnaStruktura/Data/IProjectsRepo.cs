using System.Collections.Generic;
using OrganizacnaStruktura.Dtos;
using OrganizacnaStruktura.Models;

namespace OrganizacnaStruktura.Data
{
    public interface IProjectsRepo
    {
        bool SaveChanges();
        IEnumerable<Project> GetAllItems();
        Project GetItemById(int id);
        Project CreateItem(ProjectCreateDto createdProject);
        bool UpdateItem(ProjectUpdateDto updatedProject, Project oldProject);
        void DeleteItem(Project item);
    }
}