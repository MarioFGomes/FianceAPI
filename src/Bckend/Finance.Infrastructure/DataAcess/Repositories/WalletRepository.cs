using Finance.Domain.Entities;
using Finance.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Finance.Infrastructure.DataAcess.Repositories; 
public class WalletRepository : BaseRepository<Wallet>, IWalletRepository {
    public WalletRepository(FinanceContext _context, ILogger<BaseRepository<Wallet>> logger) : base(_context, logger) { }
   
}
