using Finance.Communication.Request;
using Finance.Communication.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Application.UseCase.Wallet.Fetch; 
public interface IFetchWalletMov {
    Task<WalletMovPagedResponse> Execute(WalletMovQueryRequest request, Guid UserId);
}
