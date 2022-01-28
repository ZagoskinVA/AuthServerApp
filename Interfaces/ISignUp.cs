using AuthServerApp.Models;

namespace AuthServerApp.Interfaces
{
    public interface ISignUp
    {
        List<string> Errors { get; }
        Task<bool> SignUp(SignUpModel model);
    }
}
