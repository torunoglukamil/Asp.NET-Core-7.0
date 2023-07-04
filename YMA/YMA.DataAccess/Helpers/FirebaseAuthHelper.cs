using Firebase.Auth;
using Microsoft.Extensions.Configuration;

namespace YMA.DataAccess.Helpers
{
    public class FirebaseAuthHelper
    {
        private readonly FirebaseAuthProvider _firebaseAuth;

        public FirebaseAuthHelper(IConfiguration config)
        {
            _firebaseAuth = new FirebaseAuthProvider(new FirebaseConfig(config.GetValue<string>("FirebaseWebApiKey")));
        }

        public FirebaseAuthProvider FirebaseAuth { get { return _firebaseAuth; } }

        public static String GetMessageByAuthErrorReason(AuthErrorReason reason)
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
    }
}
