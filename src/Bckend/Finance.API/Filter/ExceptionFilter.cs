using Finance.Communication.Response;
using Finance.Exception;
using Finance.Exception.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using InvalidOperationException = Finance.Exception.Exceptions.InvalidOperationException;

namespace Finance.API.Filter;
public class ExceptionFilter : IExceptionFilter {

    private readonly Dictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

    public ExceptionFilter() {
        _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
        {
                { typeof(ExceptionValidatorError), context => HandleException<ExceptionValidatorError>(context) },
                { typeof(ResourceNotFoundException), context => HandleException<ResourceNotFoundException>(context) },
                { typeof(ResourceAlreadyExistsException), context => HandleException<ResourceAlreadyExistsException>(context) },
                { typeof(InvalidOperationException), context => HandleException<InvalidOperationException>(context) },
                { typeof(BusinessException), context => HandleException<BusinessException>(context) }
        };
    }
    public void OnException(ExceptionContext context) {
        if (context.Exception is FinanceException) {

            HandleFinanceExcpetion(context);

        } else {
            ThrowUnknowError(context);
        }
    }

    private void HandleFinanceExcpetion(ExceptionContext context) {

        var exceptionType = context.Exception.GetType();

        if (_exceptionHandlers.ContainsKey(exceptionType)) {
            _exceptionHandlers[exceptionType](context);
        }

    }

    private void HandleException<T>(ExceptionContext context) where T : FinanceException {
        T? exception = context.Exception as T;

        if (exception != null) {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Result = new ObjectResult(new ResponseErrorJson(exception.MessageError.ToList(), context.HttpContext.Response.StatusCode));
        }
    }

    private void ThrowUnknowError(ExceptionContext context) {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Result = new ObjectResult(new ResponseErrorJson(ResourceMessage.InternalServerError, context.HttpContext.Response.StatusCode));
    }


}
