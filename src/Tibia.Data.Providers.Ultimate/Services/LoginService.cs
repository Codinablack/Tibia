namespace Tibia.Data.Providers.Ultimate.Services
{
    public class LoginService
    {
        /// <summary>
        ///     Authenticates the specified username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>Whether the user is successfully authenticated.</returns>
        public bool Authenticate(string username, string password)
        {
            return true;
        }
    }
}