using Finance.Communication.Request;
using Finance.Communication.Response;

namespace Finance.Application.UseCase.Wallet.Create; 
public interface ICreateWalletUseCase {
    public Task<ApiResponse<WalletResponse>> Execute(WalletRequest request,Guid UserId);
}
