using Firebase.Auth;
using YMA.DataAccess.Constants;
using YMA.DataAccess.Repositories;
using YMA.Entities.Models;

namespace YMA.DataAccess.Helpers
{
    public class ResponseHelper
    {
        private readonly LogRepository _logRepository;

        public ResponseHelper(LogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        private static string GetMessageByAuthErrorReason(AuthErrorReason reason)
        {
            switch (reason)
            {
                case AuthErrorReason.UserDisabled:
                    return "Hesap devre dışı.";
                case AuthErrorReason.InvalidEmailAddress:
                    return "E-posta adresi geçersiz.";
                case AuthErrorReason.WeakPassword:
                    return "Lütfen daha güçlü bir şifre oluşturunuz.";
                case AuthErrorReason.EmailExists:
                    return "E-posta adresi zaten kullanımda.";
                case AuthErrorReason.UnknownEmailAddress:
                    return "Hesap bulunamadı.";
                case AuthErrorReason.WrongPassword:
                    return "Şifrenizi yanlış girdiniz.";
                default:
                    return "Bir hata oluştu. Lütfen sonra tekrar deneyin.";
            }
        }

        private static ResponseModel GetExceptionResponse(string message) => new()
        {
            message = message,
        };

        private static ResponseModel GetExceptionResponse(FirebaseAuthException e) => new()
        {
            message = GetMessageByAuthErrorReason(e.Reason),
        };

        public void CreateExceptionLog(string message, string data)
        {
            _logRepository.CreateLog(new LogModel()
            {
                type = "exception",
                message = message,
                data = data,
            });
        }

        public ResponseModel TryCatch(string methodName, Func<ResponseModel> function, string message = StringConstants.DefaultExceptionMessage)
        {
            try
            {
                return function();
            }
            catch (FirebaseAuthException e)
            {
                return GetExceptionResponse(e);
            }
            catch (Exception e)
            {
                CreateExceptionLog(methodName + ": " + e.Message, "");
                return GetExceptionResponse(message);
            }
        }

        public async Task<ResponseModel> TryCatch(string methodName, Func<Task<ResponseModel>> function, string message = StringConstants.DefaultExceptionMessage)
        {
            try
            {
                return await function();
            }
            catch (FirebaseAuthException e)
            {
                return GetExceptionResponse(e);
            }
            catch (Exception e)
            {
                CreateExceptionLog(methodName + ": " + e.Message, "");
                return GetExceptionResponse(message);
            }
        }
    }
}
