using Finance.Domain.Entities;
using Finance.Domain.Repositories;

namespace Finance.Infrastructure.DataAcess.Repositories; 
public class WalletRepository : BaseRepository<Wallet>, IWalletRepository {
    public WalletRepository(FinanceContext _context) : base(_context) { }
   
}
