using Finance.Domain.Entities;
using Finance.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Finance.Infrastructure.DataAcess.Repositories; 
public class UserRepository: BaseRepository<User>, IUserRepository {
    public UserRepository(FinanceContext _context, ILogger<BaseRepository<User>> logger) : base(_context,logger) { }
  

}
