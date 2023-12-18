using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetManager.Data;
using NetManager.Helpers;
using NetManager.Models;

namespace NetManager.Services
{
    public class UsersServices
    {
        private readonly NetManagerContext _dbContext;

        public UsersServices(NetManagerContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<UsersServices>> GetAllAdmins()
        {
            return (IEnumerable<UsersServices>)await _dbContext.Users.ToListAsync();
        }

        public async Task<Users?> GetAdminById(int id)
        {
            var admin = await _dbContext.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (admin == null)
            {
                return null;
            }
            return admin;
        }



        public async Task CreateAdmin([Bind("Firstname,Lastname,Email,Password")] Users users)
        {

            string hashedPassword = PasswordHasher.HashPassword(users.Password);

            users.Password = hashedPassword;

            _dbContext.Users.Add(users);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAdmin(int id, Users admin)
        {
            var existingAdmin = await _dbContext.Users.FindAsync(id);

            if (existingAdmin != null)
            {
                existingAdmin.Email = admin.Email;
                // Mettez à jour d'autres propriétés si nécessaire
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteAdmin(int id)
        {
            var admin = await _dbContext.Users.FindAsync(id);

            if (admin != null)
            {
                _dbContext.Users.Remove(admin);
                await _dbContext.SaveChangesAsync();
            }
        }

    }
}
