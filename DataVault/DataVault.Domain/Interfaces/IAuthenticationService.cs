namespace DataVault.Domain.Interfaces
{
	using DataVault.Domain.Models;
    public interface IAuthenticationService
    {
		Task<AuthenticationResponse> SignIn(string username, string password);
	}
}
