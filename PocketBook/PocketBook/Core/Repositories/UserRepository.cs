using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PocketBook.Core.IRepositories;
using PocketBook.Data;
using PocketBook.Model;

namespace PocketBook.Core.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DbContext context, ILogger logger) : 
            base(context, logger) { }

        public override async Task<IEnumerable<User>> All()
        {
            try
            {
                return await DbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "{Repo} All function error", typeof(UserRepository));
                return new List<User>();
            }
        }

        public override async Task<bool> Update(User entity)
        {
            try
            {
                var existingUser = await DbSet.Where(x => x.Id == entity.Id)
                    .FirstOrDefaultAsync();

                if (existingUser == null)
                    return await Add(entity);

                existingUser.FirstName = entity.FirstName;
                existingUser.LastName = entity.LastName;
                existingUser.Email = entity.Email;

                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "{Repo} Update function error", typeof(UserRepository));
                return false;
            }
        }

        public override async Task<bool> Delete(Guid id)
        {
            try
            {
                var exist = await DbSet.Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                if (exist == null) return false;

                DbSet.Remove(exist);

                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "{Repo} Delete function error", typeof(UserRepository));
                return false;
            }
        }
    }
}
