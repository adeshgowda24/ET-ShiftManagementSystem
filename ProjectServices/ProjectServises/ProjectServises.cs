
using testet.Models;

namespace Servises.ProjectServises
{
    public interface IProjectServises
    {
        Project GetProject(int ProjectId);
    }

    public class ProjectServises : IProjectServises
    {
        private readonly ShiftManagementContext _shiftDbContext;
        public ProjectServises(ShiftManagementContext shiftDbContext)
        {
            _shiftDbContext = shiftDbContext;
        }

        public Project GetProject(int ProjectId)
        {
            return _shiftDbContext.Projects.FirstOrDefault(a => a.ProjectId == ProjectId);
        }
    }
}