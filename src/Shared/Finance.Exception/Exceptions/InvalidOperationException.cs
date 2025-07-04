namespace Finance.Exception.Exceptions; 
public class InvalidOperationException:FinanceException {

    public InvalidOperationException(IList<string> messageError) : base(messageError) { }

    public InvalidOperationException(string messageError) : base(messageError) { }
}
