using Finance.Application.Validators;
using Finance.Communication.Request;
using Finance.Communication.Response;
using Finance.Domain.Repositories;
using Finance.Exception.Exceptions;


namespace Finance.Application.UseCase.Wallet.Fetch;
public class FetchWalletMov : IFetchWalletMov {
    private readonly IWalletMovementRepository _WalletMovementRepository;
    private readonly IWalletRepository _walletRepository;
    public FetchWalletMov(IWalletMovementRepository walletMovementRepository, IWalletRepository walletRepository)
    {
        _WalletMovementRepository = walletMovementRepository;
        _walletRepository = walletRepository;
    }
    public async Task<WalletMovPagedResponse> Execute(WalletMovQueryRequest request,Guid UserId) {

        Validar(request);

        var query= _WalletMovementRepository.GetQueryable();
        
        var wallet=await _walletRepository.GetOneAsync(e=>e.UserId==UserId);

        var startDateUtc = DateTime.SpecifyKind(request.StartDate.Date, DateTimeKind.Utc);
        var endDateUtcExclusive = DateTime.SpecifyKind(request.EndDate.Date.AddDays(1), DateTimeKind.Utc);

        query = query.Where(e =>
            e.CreatedAt >= startDateUtc &&
            e.CreatedAt < endDateUtcExclusive &&
            e.WalletId == wallet.Id);

        int totalCount = query.Count();
        
        int skip = (request.Page - 1) * request.PageSize;
        
        var items = query.Skip(skip).Take(request.PageSize).ToList();

        return new WalletMovPagedResponse {
            Items = items.Select(e => new WalletMovimentResponse {
                WalletId = wallet.Id,
                Amount = e.Amount,
                Description = e.Description,
                MovimentType = e.MovimentType,
                currency = e.currency,
                TransactionId = e.TransactionId,
                CreatedAt = e.CreatedAt,
               
            }),
            TotalCount = totalCount,
            Page = request.Page,
            PageSize = request.PageSize
        };


    }

    private static void Validar(WalletMovQueryRequest request) {
        var validar = new FetchWalletMovValidator();
        var result = validar.Validate(request);

        if (!result.IsValid) {
            var messageError = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ExceptionValidatorError(messageError);
        }
    }
}
