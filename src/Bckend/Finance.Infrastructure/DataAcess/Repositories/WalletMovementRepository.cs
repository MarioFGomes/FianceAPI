using Finance.Domain.Entities;
using Finance.Domain.Repositories;

namespace Finance.Infrastructure.DataAcess.Repositories; 
public class WalletMovementRepository : BaseRepository<WalletMovement>, IWalletMovement {
    public WalletMovementRepository(FinanceContext _context) : base(_context) { }
}
