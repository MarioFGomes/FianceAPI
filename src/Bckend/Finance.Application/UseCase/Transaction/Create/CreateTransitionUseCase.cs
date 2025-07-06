using Finance.Application.Mappers;
using Finance.Application.Validators;
using Finance.Communication.Request;
using Finance.Communication.Response;
using Finance.Domain.Repositories;
using Finance.Exception;
using Finance.Exception.Exceptions;
using InvalidOperationException = Finance.Exception.Exceptions.InvalidOperationException;

namespace Finance.Application.UseCase.Transaction.Create;
public class CreateTransitionUseCase : ICreateTransitionUseCase {
    private readonly ITransactionRepository _transactionRepository;
    private readonly IWalletRepository _walletRepository;
    public CreateTransitionUseCase(ITransactionRepository transactionRepository, IWalletRepository walletRepository)
    {
        _transactionRepository = transactionRepository;
        _walletRepository = walletRepository;
       
    }
    public async Task<ApiResponse<TransactionResponse>> Execute(TransactionRequest request, Guid UserId) 
    {
        Validate(request);
        try {
            var transition = request.ToTransactionDomain(UserId);
            var WalletSender = await _walletRepository.GetOneAsync(e => e.UserId == UserId && e.currency==request.currency);
            var WalletReceiver = await _walletRepository.GetOneAsync(e => e.Id == request.ReceiverWalletId && e.currency==request.currency);
            if (WalletSender is null || WalletReceiver is null) throw new InvalidOperationException("Operação Invalida");
            transition.TransferTo(WalletSender, WalletReceiver, request.Amount);
            WalletSender.Debit(transition.Amount, transition.Description, request.currency,transition.Id);
            WalletReceiver.Credit(transition.Amount, transition.Description,request.currency, transition.Id);
            
            _transactionRepository.BeginTransaction();
           
            await _transactionRepository.AddOneAsync(transition);

            _transactionRepository.CommitTransaction();
            return new ApiResponse<TransactionResponse> {
                Success = true,
                Message = ResourceMessage.TransferCompleted,
                Data = new TransactionResponse { ReceiverWalletId = transition.ReceiverWalletId, SenderWalletId = transition.SenderWalletId, Amount = transition.Amount, Description = transition.Description, CreatedAt = transition.CreatedAt,StatusTransaction=transition.StatusTransaction }
            };
        } catch {
            _transactionRepository.RollbackTransaction();
            throw;
        }
    }
    private static void Validate(TransactionRequest request) {
        var validar = new TransactionValidator();
        var result = validar.Validate(request);

        if (!result.IsValid) {
            var messageError = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ExceptionValidatorError(messageError);
        }
    }
}
