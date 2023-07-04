using Microsoft.AspNetCore.Http;
using YMA.Entities.Models;

namespace YMA.DataAccess.Helpers
{
    public class ResponseHelper
    {
        private static ResponseModel GetExceptionResponse(Exception e) => new()
        {
            status_code = StatusCodes.Status400BadRequest,
            message = "Bir hata oluştu. Lütfen sonra tekrar deneyin.",
            data = e.Message,
        };

        public static ResponseModel TryCatch(Func<ResponseModel> function)
        {
            try
            {
                return function();
            }
            catch (Exception e)
            {
                return GetExceptionResponse(e);
            }
        }

        public static async Task<ResponseModel> TryCatch(Func<Task<ResponseModel>> function)
        {
            try
            {
                return await function();
            }
            catch (Exception e)
            {
                return GetExceptionResponse(e);
            }
        }
    }
}
