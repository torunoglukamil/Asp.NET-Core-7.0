using Firebase.Auth;
using YMA.DataAccess.Constants;

namespace YMA.DataAccess.Providers
{
    public class FirebaseAuthenticationProvider
    {
        private readonly FirebaseAuthProvider _firebaseAuth;

        public FirebaseAuthenticationProvider()
        {
            _firebaseAuth = new FirebaseAuthProvider(new FirebaseConfig(StringConstants.FirebaseWebApiKey));
        }

        public FirebaseAuthProvider FirebaseAuth { get { return _firebaseAuth; } }
    }
}
