using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI.Core.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(
            ApplicationDbContext context,
            ILogger logger
            ) : base(context, logger)
        {  
        }

        public override async Task<IEnumerable<User>> All()
        {
            try
            {
                return await _dbSet.ToListAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "{Repo} All method error.", typeof(UserRepository));
                return new List<User>();
            }
        }

        public override async Task<bool> Update(User entity)
        {
            try
            {
                var existingUser = await _dbSet.Where(o => o.Id == entity.Id)
                                     .SingleOrDefaultAsync();
                if (existingUser == null)
                    return false;

                existingUser.FirstName = entity.FirstName;
                existingUser.LastName = entity.LastName;
                existingUser.Email = entity.Email;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Update method error.", typeof(UserRepository));
                return false;
            }
        }

        public override async Task<bool> Delete(Guid Id)
        {
            try
            {
                var exist = await _dbSet.Where(o => o.Id == Id)
                                        .SingleOrDefaultAsync();

                if (exist != null)
                {
                    _dbSet.Remove(exist);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "{Repo} Delete method error.", typeof(UserRepository));
                return false;
            }
        }
    }
}
