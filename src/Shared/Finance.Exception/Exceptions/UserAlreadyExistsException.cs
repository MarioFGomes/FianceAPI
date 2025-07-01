

namespace Finance.Exception.Exceptions; 
public class UserAlreadyExistsException: FinanceException {
    public UserAlreadyExistsException(IList<string> messageError) : base(messageError) { }

    public UserAlreadyExistsException(string messageError) : base(messageError) { }
}
