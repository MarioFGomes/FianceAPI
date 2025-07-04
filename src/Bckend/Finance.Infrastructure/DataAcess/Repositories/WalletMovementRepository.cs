using Finance.Domain.Entities;
using Finance.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Finance.Infrastructure.DataAcess.Repositories; 
public class WalletMovementRepository : BaseRepository<WalletMovement>, IWalletMovementRepository {
    public WalletMovementRepository(FinanceContext _context, ILogger<BaseRepository<WalletMovement>> logger) : base(_context,logger) { }
}
