using Microsoft.EntityFrameworkCore.Migrations.Operations;
using ShiftMgtDbContext.Data;
using ShiftMgtDbContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftManagementServises.Servises
{
    public interface IProjectDatailServises
    {
        List<ProjectDetail> GetProjecDetails(int ProjectId);
    }
    public class ProjectDatailServises : IProjectDatailServises
    {
        private readonly ShiftManagementDbContext _shiftDbContext;
        public ProjectDatailServises(ShiftManagementDbContext shiftDbContext)
        {
            _shiftDbContext = shiftDbContext;
        }
        public List<ProjectDetail> GetProjecDetails(int ProjectID)
        {

            return _shiftDbContext.projectDetails.Where(pro => pro.ProjectDetailsID == ProjectID).ToList();
        }


    }
}
