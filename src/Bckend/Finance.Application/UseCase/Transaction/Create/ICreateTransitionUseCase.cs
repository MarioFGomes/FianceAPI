using Finance.Communication.Request;
using Finance.Communication.Response;

namespace Finance.Application.UseCase.Transaction.Create; 
public interface ICreateTransitionUseCase {
    public Task<ApiResponse<TransactionResponse>> Execute(TransactionRequest request, Guid UserId);
}
