using Microsoft.EntityFrameworkCore;
using ShiftMgtDbContext.Data;
using ShiftMgtDbContext.Entities;


namespace Servises.ProjectServises
{
    public interface IProjectServises
    {
        Project GetProject(int ProjectId);
        long AddProject(Project project);
    }

    public class ProjectServises : IProjectServises
    {
        private readonly ShiftManagementDbContext _shiftDbContext;
        public ProjectServises(ShiftManagementDbContext shiftDbContext)
        {
            _shiftDbContext = shiftDbContext;
        }

        public long AddProject(Project project)
        {
            _shiftDbContext.projects.Add(project);
            _shiftDbContext.SaveChanges();
            return project.ProjectId;
        }



        public Project GetProject(int ProjectId)
        {
            return _shiftDbContext.projects.AsNoTracking().FirstOrDefault(a => a.ProjectId == ProjectId);
        }

    }
}