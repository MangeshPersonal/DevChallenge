namespace TODO.MODELS.ResponseModel
{
    public class ToDoResponse
    {
        public string Version { get { return "1.0"; } }

        public int StatusCode { get; set; }


        public string ErrorMessage { get; set; }


        public object Data { get; set; }

        public int DataCount { get; set; }

        public ToDoResponse(int statusCode, object result = null, string errorMessage = null,int _DataCount=0)
        {
            StatusCode = (int)statusCode;
            Data = result;
            ErrorMessage = errorMessage;
            DataCount = _DataCount;
        }
    }
}
