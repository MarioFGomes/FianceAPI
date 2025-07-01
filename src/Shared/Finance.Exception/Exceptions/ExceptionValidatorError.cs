
namespace Finance.Exception.Exceptions;
public class ExceptionValidatorError : FinanceException {
   public ExceptionValidatorError(IList<string> messageError) : base(messageError) { }

   public ExceptionValidatorError(string messageError) : base(messageError) { }
}
