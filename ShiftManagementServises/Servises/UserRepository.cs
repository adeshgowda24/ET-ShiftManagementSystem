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

            //user.Roles=  _ShiftManagementDbContext.roles.Where(x => x.Id == user.id).Select(a=>a.Id.ToString()).ToList();
            if (userRoles.Any())
            {
                user.Roles = new List<string>();
                foreach (var item in userRoles)
                {
                    var role = await _ShiftManagementDbContext.roles.FirstOrDefaultAsync(x => x.Id == item.RoleId);
                    if (role != null)
                    {
                        user.Roles.Add(role.Name);
                    }
                }
            }

            user.password = null;
            return user;
        }

        public async Task<User> RegisterAsync(User user /*,Role AssignRole*/ )
        {
            user.id = Guid.NewGuid();
            await _ShiftManagementDbContext.users.AddAsync(user);
            await _ShiftManagementDbContext.SaveChangesAsync();

            //need to check roleand cerate a record acordingly
            //if (AssignRole == )
            //{

            //}
            //var role = _ShiftManagementDbContext.roles.Where(x => x.Name == "User");


            ////eg Admin>> take this roleid from role table and add userid

            //_ShiftManagementDbContext.usersRoles = role.Where(x => x.Id = );

            return user;
            
        }
    }
}
