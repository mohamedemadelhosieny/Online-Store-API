namespace Store.Omda.APIs.Errors
{
    public class ApiValidationErrorResponse : ApiErrorResponse
    {
        public IEnumerable<string> errors { get; set; } = new List<string>();

        public ApiValidationErrorResponse() : base(400)
        {
            
        }
    }
}
