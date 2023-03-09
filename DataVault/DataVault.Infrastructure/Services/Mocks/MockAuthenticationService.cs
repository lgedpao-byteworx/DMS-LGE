namespace DataVault.Infrastructure.Services.Mocks
{
	using System.Threading.Tasks;
	using DataVault.Domain.Interfaces;
	using DataVault.Domain.Models;
	public class MockAuthenticationService : IAuthenticationService
	{
		private List<User> users = new List<User>()
		{
			new User { Id = "0123", Email = "lgedpao020687@gmail.com", Password = "admin123" },
			new User { Id = "1234", Email = "bleyte30@gmail.com", Password = "admin123" }
		};

		public Task<AuthenticationResponse> SignIn(string username, string password)
		{
			var user = users.FirstOrDefault(x => x.Email.Equals(username, StringComparison.OrdinalIgnoreCase) && x.Password == password);

			if (user != null)
			{
				return Task.FromResult(new AuthenticationResponse
				{
					UserId = user.Id,
					AccessToken = "accesstoken",
				});
			}

			return Task.FromResult(default(AuthenticationResponse));
		}
	}
}
