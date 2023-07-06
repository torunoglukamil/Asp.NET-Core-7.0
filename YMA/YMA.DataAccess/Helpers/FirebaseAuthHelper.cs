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
    }
}
