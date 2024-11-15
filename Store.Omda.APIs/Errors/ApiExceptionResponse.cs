namespace Store.Omda.APIs.Errors
{
    public class ApiExceptionResponse : ApiErrorResponse
    {
        private string? Details {  get; set; }

        public ApiExceptionResponse(int statusCode, string? message= null, string? details = null)
            : base(statusCode,message)
        {
            Details = details;
        }
    }
}
