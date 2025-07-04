

namespace Finance.Exception.Exceptions; 
public class ResourceAlreadyExistsException: FinanceException {
    public ResourceAlreadyExistsException(IList<string> messageError) : base(messageError) { }

    public ResourceAlreadyExistsException(string messageError) : base(messageError) { }
}
