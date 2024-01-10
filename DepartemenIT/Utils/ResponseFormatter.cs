using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DepartemenIT.Utils
{
    internal static class ResponseFormatter
    {
        public static ActionResult CreateResponse(HttpStatusCode statusCode, string message, object? data = null)
        {
            if (data == null)
            {
                var responseNull = new JsonResult(new
                {
                    status_code = (int)statusCode,
                    message
                });

                return responseNull;
            }
            else
            {
                var response = new JsonResult(new
                {
                    status_code = (int)statusCode,
                    message,
                    data
                });
                return response;
            }
        }
    }
}
