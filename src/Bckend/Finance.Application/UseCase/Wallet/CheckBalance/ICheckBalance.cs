

using Finance.Communication.Response;

namespace Finance.Application.UseCase.Wallet.CheckBalance; 
public interface ICheckBalance {
    Task<ApiResponse<WalletResponse>> Execute(Guid UserId,string currency);
}
