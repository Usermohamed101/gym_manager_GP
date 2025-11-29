using GymManagmentSystem.DataBase;
using GymManagmentSystem.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GymManagmentSystem.Repos
{


    public interface IUserRepo : IRepo<User, string>
    {

        bool update(UpdateUserDto usr, string id);
        Task<bool> CreateRoleAsync(string roleName);
        Task<bool> DeleteRoleAsync(string roleName);
        Task<bool> AssignRoleAsync(string userId, string roleName);
        Task<bool> RemoveRoleAsync(string userId, string roleName);
        Task<IList<string>> GetUserRolesAsync(string userId);
        Task<IEnumerable<IdentityRole>> GetAllRolesAsync();
        Task<IEnumerable<User>> GetUsersInRoleAsync(string roleName);

    }












    public class UserRepo :IUserRepo
    {

        private readonly GymDb contxt;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public UserRepo(GymDb contxt, UserManager<User> userManager,RoleManager<IdentityRole> roleManager)
        {
            this.contxt = contxt;
            this._roleManager = roleManager;
            this._userManager = userManager;
        }
        public async Task<bool> add(User E,string Password)
        {
           
              await  _userManager.CreateAsync(E, Password);
                return true;
         
        }

        public bool Delete(string id)
        {

            User usr = getById(id);

                contxt.Users.Remove(usr);
                if (contxt.Users.Entry(usr).State == Microsoft.EntityFrameworkCore.EntityState.Detached)
                {
                    contxt.SaveChanges();
                    return true;
                }
               return false;
                
           
        }

        public List<User> getAll()
        {
            return contxt.Users.ToList();
        }

        public User getById(string id)
        {
            return  contxt.Users.FirstOrDefault(u => u.Id == id);
        }

      public  bool update(UpdateUserDto usr, string id)
        {
            User newUsr = getById(id);

            return true;



        }

        public async Task<bool> CreateRoleAsync(string roleName)
        {
            if (await _roleManager.RoleExistsAsync(roleName))
                return false;

            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
            return result.Succeeded;
        }

        public async Task<bool> DeleteRoleAsync(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null) return false;

            var result = await _roleManager.DeleteAsync(role);
            return result.Succeeded;
        }

        public async Task<bool> AssignRoleAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            if (!await _roleManager.RoleExistsAsync(roleName))
                return false;

            var result = await _userManager.AddToRoleAsync(user, roleName);
            return result.Succeeded;
        }

        public async Task<bool> RemoveRoleAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            if (!await _roleManager.RoleExistsAsync(roleName))
                return false;

            var result = await _userManager.RemoveFromRoleAsync(user, roleName);
            return result.Succeeded;
        }

        public async Task<IList<string>> GetUserRolesAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return new List<string>();

            return await _userManager.GetRolesAsync(user);
        }

        public async Task<IEnumerable<IdentityRole>> GetAllRolesAsync()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        public async Task<IEnumerable<User>> GetUsersInRoleAsync(string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
                return Enumerable.Empty<User>();

            return await _userManager.GetUsersInRoleAsync(roleName);
        }






    }

}
