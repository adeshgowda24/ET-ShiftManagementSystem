using ShiftMgtDbContext.Data;
using ShiftMgtDbContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftManagementServises.Servises
{
    public interface IShiftServices
    {
        Shift GetShiftDetails(int ShiftId);
    }
    public class ShiftServices : IShiftServices
    {
        private readonly ShiftManagementDbContext _Shiftservices;
        public ShiftServices(ShiftManagementDbContext Shiftservices)
        {
            _Shiftservices = Shiftservices;
        }
        public Shift GetShiftDetails(int shiftId)
        {
            return _Shiftservices.Shifts.FirstOrDefault(x => x.ShiftId == shiftId);
        }
    }
}
