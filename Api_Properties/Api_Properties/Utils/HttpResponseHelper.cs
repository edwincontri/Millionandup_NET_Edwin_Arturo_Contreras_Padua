using Microsoft.AspNetCore.Mvc;

namespace Api_Properties.Utils
{
    public static class HttpResponseHelper
    {
        public static IActionResult SuccessResult(object response)
        {
            return new ObjectResult(response);
        }

        public static ObjectResult Response(int code, object value)
        {
            var result = new ObjectResult(value);
            result.StatusCode = code;
            return result;
        }
    }
}
