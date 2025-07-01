namespace Finance.Exception.Exceptions;
public class FinanceException : SystemException {
    public IList<string> MessageError { get; set; }

    public FinanceException(IList<string> messageError) {
        MessageError = messageError;
    }

    public FinanceException(string message) {
        MessageError = new List<string> {
            message
        };
    }
}