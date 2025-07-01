using Finance.Domain.Entities;
using Finance.Domain.Repositories;

namespace Finance.Infrastructure.DataAcess.Repositories; 
public class TransactionRepository:BaseRepository<Transaction>, ITransactionRepository {
    public TransactionRepository(FinanceContext _context) : base(_context) { }
  
}
