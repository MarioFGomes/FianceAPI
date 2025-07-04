using Finance.Communication.Request;
using Finance.Domain.Entities;

namespace Finance.Application.Mappers; 
public static class TransactionMapper {
    public static Transaction ToTransactionDomain(this TransactionRequest request,Guid SenderWalletId) 
    {
        return new Transaction 
        {
            Amount = request.Amount,
            ReceiverWalletId = request.ReceiverWalletId,
            SenderWalletId = SenderWalletId,
            Description = request.Description
        };
    }
}
