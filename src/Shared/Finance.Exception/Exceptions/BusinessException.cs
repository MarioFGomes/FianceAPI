namespace Finance.Exception.Exceptions; 
public class BusinessException: FinanceException {

    public BusinessException(IList<string> messageError) : base(messageError) { }

    public BusinessException(string messageError) : base(messageError) { }
}
