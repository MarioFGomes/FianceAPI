using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Communication.Request; 
public class TransactionRequest {
    public Guid ReceiverWalletId { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public string currency { get; set; }
}
