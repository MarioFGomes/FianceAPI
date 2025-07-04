using Finance.Domain.Entities;
using Finance.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Finance.Infrastructure.DataAcess.Repositories; 
public class TransactionRepository:BaseRepository<Transaction>, ITransactionRepository {
    public TransactionRepository(FinanceContext _context, ILogger<BaseRepository<Transaction>> logger) : base(_context, logger) { }
  
}
