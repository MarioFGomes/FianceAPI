using Finance.Domain.Entities;
using Finance.Domain.Repositories;

namespace Finance.Infrastructure.DataAcess.Repositories; 
public class UserRepository: BaseRepository<User>, IUserRepository {
    public UserRepository(FinanceContext _context): base(_context) { }
  

}
