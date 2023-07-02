using Microsoft.AspNetCore.Http;
using YMA.Models.Models;

namespace YMA.DataAccess.Helpers
{
    public class ResponseHelper
    {
        public ResponseModel TryCatch(Func<ResponseModel> function)
        {
            try
            {
                return function();
            }
            catch (Exception e)
            {
                return new ResponseModel()
                {
                    status_code = StatusCodes.Status400BadRequest,
                    message = "Bir hata oluştu. Lütfen sonra tekrar deneyin.",
                    data = e.Message,
                };
            }
        }

        public async Task<ResponseModel> TryCatch(Func<Task<ResponseModel>> function)
        {
            try
            {
                return await function();
            }
            catch (Exception e)
            {
                return new ResponseModel()
                {
                    status_code = StatusCodes.Status400BadRequest,
                    message = "Bir hata oluştu. Lütfen sonra tekrar deneyin.",
                    data = e.Message,
                };
            }
        }
    }
}
