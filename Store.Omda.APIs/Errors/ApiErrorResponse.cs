namespace Store.Omda.APIs.Errors
{
    public class ApiErrorResponse
    {
        public int StatusCode { get; set; }

        public string? Message { get; set; }

        public ApiErrorResponse(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefultMessageForStatusCode(statusCode);
        }

        private string? GetDefultMessageForStatusCode(int statusCode)
        {

            var message = statusCode switch
            {
                400 => " a bad requset, you have made",
                401 => "Authorized, you are not",
                404 => "Resource was not found",
                500 => "Server Error"
                
            } ;

            return message;
        }
    }
}
