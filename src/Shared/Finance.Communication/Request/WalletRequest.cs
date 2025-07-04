using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Communication.Request; 
public class WalletRequest {
    public decimal Balance { get; set; }
    public string currency { get; set; }
}
