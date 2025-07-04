using Finance.Communication.Request;
using Finance.Communication.Response;
using Finance.Domain.Entities;

namespace Finance.Application.UseCase.Wallet.Credite; 
public interface ICrediteUseCase {
    public Task<ApiResponse<WalletMovimentResponse>> Execute(WalletMovimentRequest request, Guid userId);
}
