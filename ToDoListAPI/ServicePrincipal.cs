//using Microsoft.IdentityModel.Clients.ActiveDirectory;
//using System.Configuration;

//namespace ToDoListAPI
//{
//    public static class ServicePrincipal
//    {
//        // The issuer URL of the tenant. For example: login.microsoftonline.com/contoso.onmicrosoft.com
//        static string authority = ConfigurationManager.AppSettings["ida:Authority"];

//        // The Client ID of the calling AAD app (i.e. the one associated with this project). For example: 960adec2-b74a-484a-960adec2-b74a-484a
//        static string clientId = ConfigurationManager.AppSettings["ida:ClientId"];

//        // The key that was created for the calling AAD app. For example: oCgdj3EYLfnR0p6iR3UvHFAfkn+zQB+0VqZT/6=
//        static string clientSecret = ConfigurationManager.AppSettings["ida:ClientSecret"];

//        // The Client ID of the called AAD app. For example: e65e8fc9-5f6b-48e8-e65e8fc9-5f6b-48e8
//        // The called AAD app may be the same as the calling AAD app, in which case this value will be the same as the ClientId value.
//        static string resource = ConfigurationManager.AppSettings["ida:Resource"];

//        public static AuthenticationResult GetS2SAccessTokenForProdMSA()
//        {
//            return GetS2SAccessToken(authority, resource, clientId, clientSecret);
//        }

//        ///<summary>
//        /// Gets an application token used for service-to-service (S2S) API calls.
//        ///</summary>
//        static AuthenticationResult GetS2SAccessToken(string authority, string resource, string clientId, string clientSecret)
//        {
//            // Client credential consists of the "client" AAD web application's Client ID
//            // and the key that was generated for the application in the AAD Azure portal extension.
//            var clientCredential = new ClientCredential(clientId, clientSecret);

//            // The authentication context represents the AAD directory.
//            AuthenticationContext context = new AuthenticationContext(authority, false);

//            // Fetch an access token from AAD.
//            AuthenticationResult authenticationResult = context.AcquireToken(
//                resource,
//                clientCredential);
//            return authenticationResult;
//        }
//    }
//}
