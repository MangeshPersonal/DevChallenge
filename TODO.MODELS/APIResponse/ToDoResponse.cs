namespace TODO.MODELS.ResponseModel
{
    public class ToDoResponse
    {
        public string Version { get { return "1.0"; } }

        public int StatusCode { get; set; }


        public string ErrorMessage { get; set; }


        public object Data { get; set; }

        public ToDoResponse(int statusCode, object result = null, string errorMessage = null)
        {
            StatusCode = (int)statusCode;
            Data = result;
            ErrorMessage = errorMessage;
        }
    }
}
