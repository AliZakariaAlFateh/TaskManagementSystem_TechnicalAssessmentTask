namespace TaskManagementSystem.Shared.Responses;

public class ApiResponse<T>
{
    //public bool Success { get; set; }
    //public string Message { get; set; } = string.Empty;
    //public T? Data { get; set; }

    //public static ApiResponse<T> SuccessResponse(T data, string message = "Success")
    //{
    //    return new ApiResponse<T>
    //    {
    //        Success = true,
    //        Message = message,
    //        Data = data
    //    };
    //}

    //public static ApiResponse<T> FailResponse(string message)
    //{
    //    return new ApiResponse<T>
    //    {
    //        Success = false,
    //        Message = message
    //    };
    //}

        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public List<string> Errors { get; set; } = new();

        public ApiResponse() => Success = true;

        public ApiResponse(T data, string message = "Success")
        {
            Success = true;
            Data = data;
            Message = message;
        }

        public ApiResponse(string message, List<string>? errors = null)
        {
            Success = false;
            Message = message;
            Errors = errors ?? new();
        }

        public static ApiResponse<T> SuccessResponse(T data, string message = "Success")
            => new(data, message);

        public static ApiResponse<T> FailResponse(string message, List<string>? errors = null)
            => new(message, errors);
    
}