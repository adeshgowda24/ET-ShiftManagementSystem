using ShiftMgtDbContext.Data;
using ShiftMgtDbContext.Entities;
using Microsoft.EntityFrameworkCore;
using ShiftManagementServises.Servises;


namespace ShiftManagementServises.Servises
{
    public interface IUserRepository
    {
        Task<User> AuthenticateAsync(string username, string password);

        Task<User> RegisterAsync(User user);
    }
    public class UserRepository : IUserRepository

    {
        private readonly ShiftManagementDbContext _ShiftManagementDbContext;

        public UserRepository(ShiftManagementDbContext ShiftManagementDbContext)
        {
            this._ShiftManagementDbContext = ShiftManagementDbContext;
        }
        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user = await _ShiftManagementDbContext.users.FirstOrDefaultAsync(x => x.username.ToLower() == username.ToLower() && x.password == password);

            if (user == null)
            {
                return null;
            }

            var userRoles = await _ShiftManagementDbContext.usersRoles.Where(x => x.Userid == user.id).ToListAsync();


            if (userRoles.Any())
            {
                user.Roles = new List<string>();
                foreach (var item in userRoles)
                {
                    var role = await _ShiftManagementDbContext.roles.FirstOrDefaultAsync(x => x.Id == item.RoleId);
                    if (item != null)
                    {
                        user.Roles.Add(role.Name);
                    }
                }
            }

            user.password = null;
            return user;
        }

        public async Task<User> RegisterAsync(User user)
        {
            user.id = Guid.NewGuid();
            await _ShiftManagementDbContext.users.AddAsync(user);
            await _ShiftManagementDbContext.SaveChangesAsync();
            return user;
            
        }
    }
}
