using MediatR;
using DataVault.Domain.Interfaces;
using DataVault.Domain.Models;

namespace DataVault.Application.Commands
{
    public class AuthenticateUserCommand : IRequest<AuthenticateUserCommandResponse>
	{
		public string Username { get; set; }

		public string Password { get; set; }
	}

	public class AuthenticateUserCommandResponse
	{
		public AuthenticationResponse Response { get; set; }
	}

	public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, AuthenticateUserCommandResponse>
	{
		private readonly IAuthenticationService authenticationService;

		public AuthenticateUserCommandHandler(IAuthenticationService authenticationService)
		{
			this.authenticationService = authenticationService;
		}

		public async Task<AuthenticateUserCommandResponse> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
		{
			var response = await authenticationService.SignIn(request.Username, request.Password);

			return new AuthenticateUserCommandResponse
			{
				Response = response
			};
		}
	}
}
