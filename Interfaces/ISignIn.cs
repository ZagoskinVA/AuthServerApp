using AuthServerApp.Models;

namespace AuthServerApp.Interfaces
{
    public interface ISignIn
    {
        List<string> Errors { get; }
        Task<RefreshToken?> SignInAsync(SignInModel model);
    }
}
