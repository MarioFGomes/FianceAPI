namespace Finance.Exception.Exceptions;
public class ResourceNotFoundException : FinanceException {

    public ResourceNotFoundException(IList<string> messageError) : base(messageError) { }

    public ResourceNotFoundException(string messageError) : base(messageError) { }
}
