namespace Finance.Communication.Response;
public class ResponseErrorJson {

    public IList<string> Errors { get; set; }
    public int StatusCode { get; set; }
    public ResponseErrorJson(string error, int statusCode) {
        Errors = new List<string> {
            error
        };
        StatusCode = statusCode;
    }

    public ResponseErrorJson(List<string> errors, int statusCode) {
        Errors = errors;
        StatusCode = statusCode;
    }

    public ResponseErrorJson(string messagem) {
        Errors = new List<string>
        {
            messagem
        };
    }
}
