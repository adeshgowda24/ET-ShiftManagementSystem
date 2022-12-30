using Microsoft.EntityFrameworkCore;
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
        Task<IEnumerable<ProjectDetail>> GetProjecDetails();
    }
    public class ProjectDatailServises : IProjectDatailServises
    {
        private readonly ShiftManagementDbContext _shiftDbContext;
        public ProjectDatailServises(ShiftManagementDbContext shiftDbContext)
        {
            _shiftDbContext = shiftDbContext;
        }
        public async Task<IEnumerable<ProjectDetail>> GetProjecDetails()
        {

            return await _shiftDbContext.projectDetails.Include(x => x.project).Include(y => y.shift).ToListAsync();
        }


    }
}
