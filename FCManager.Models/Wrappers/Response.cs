namespace FCManager.Models.Wrappers
{
    public class Response<T>
    {
        public Response()
        {
        }
        public Response(T data, string message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }
        public Response(string message)
        {
            Succeeded = false;
            Message = message;
        }
        public int StatusCode { get; set; }
        public string ResponseCode { get; set; }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public T Data { get; set; }
    }

    public class ApplicationResponse
    {
        public ApplicationResponse()
        {
        }


        public static Response<string> SuccessMessage(string message, string returnString = null)
        {
            var response = new Response<string>
            {
                Data = null,
                Message = message,
                ResponseCode = "00",
                StatusCode = 200,
                Succeeded = true,
            };
            return response;
        }
        public static Response<T> SuccessMessage<T>(T obj, string message)
        {
            var response = new Response<T>
            {
                Data = obj,
                Message = message,
                ResponseCode = "00",
                StatusCode = 200,
                Succeeded = true,
            };
            return response;
        }

        public static Response<T> NullResponse<T>(T obj, string message)
        {
            var response = new Response<T>
            {
                Data = obj,
                Message = message,
                ResponseCode = "00",
                StatusCode = 200,
                Succeeded = true,
            };
            return response;
        }

        public static Response<string> FailureMessage(string message)
        {
            var response = new Response<string>
            {
                Data = null,
                Message = message,
                ResponseCode = "-1",
                StatusCode = 400,
                Succeeded = false,
            };
            return response;
        }

        public static Response<string> NotFoundMessage(string message)
        {
            var response = new Response<string>
            {
                Data = null,
                Message = message,
                ResponseCode = "-1",
                StatusCode = 404,
                Succeeded = false,
            };
            return response;
        }
        public static Response<T> FailureMessage<T>(T obj, string message)
        {
            var response = new Response<T>
            {
                Data = obj,
                Message = message,
                ResponseCode = "-1",
                StatusCode = 400,
                Succeeded = false,
            };
            return response;
        }

        public static Response<T> NotFoundMessage<T>(T obj, string message)
        {
            var response = new Response<T>
            {
                Data = obj,
                Message = message,
                ResponseCode = "-1",
                StatusCode = 404,
                Succeeded = false,
            };
            return response;
        }
        public static Response<string> AlreadyExistMessage(string message)
        {
            var response = new Response<string>
            {
                Data = null,
                Message = message,
                ResponseCode = "-1",
                StatusCode = 409,
                Succeeded = false,
            };
            return response;
        }

        public static Response<T> AlreadyExistMessage<T>(T obj, string message)
        {
            var response = new Response<T>
            {
                Data = obj,
                Message = message,
                ResponseCode = "-1",
                StatusCode = 409,
                Succeeded = false,
            };
            return response;
        }
    }
}
