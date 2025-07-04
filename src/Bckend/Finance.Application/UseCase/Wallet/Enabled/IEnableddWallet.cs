using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Application.UseCase.Wallet.Enabled; 
public interface IEnableddWallet {
    Task<bool> Execute(Guid UserId, string currency);
}
