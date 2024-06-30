using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proji.Domain.Dtos
{
    public record ApiResponse<T>(bool Success, string Message, T Data)
    {
        public static ApiResponse<T> SuccessResponse(T data, string message = null)
        {
            return new ApiResponse<T>(true, message, data);
        }

        public static ApiResponse<T> FailureResponse(string message)
        {
            return new ApiResponse<T>(false, message, default(T));
        }
    }
}
