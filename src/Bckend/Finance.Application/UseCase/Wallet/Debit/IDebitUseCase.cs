using Finance.Communication.Request;
using Finance.Communication.Response;

namespace Finance.Application.UseCase.Wallet.Debit; 
public interface IDebitUseCase {
    public Task<ApiResponse<WalletMovimentResponse>> Execute(WalletMovimentRequest request,Guid userId);
}
