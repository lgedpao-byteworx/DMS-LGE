namespace DataVault.Client
{
	using DataVault.Client.Validation.Validators;
	using DataVault.Client.ViewModels;
	using DataVault.Client.Views;
	using DataVault.Domain.Interfaces;
	using DataVault.Infrastructure.Services.Mocks;
	using FluentValidation;

	public class PrismStartup
    {
		public static void Configure(PrismAppBuilder builder)
		{
			builder.RegisterTypes(RegisterTypes)
				.OnAppStart("NavigationPage/LoginPage");
		}

		private static void RegisterTypes(IContainerRegistry containerRegistry)
		{
			containerRegistry.RegisterForNavigation<LoginPage, LoginViewModel>()
				   .RegisterInstance(SemanticScreenReader.Default);
			containerRegistry.Register<IValidator<LoginViewModel>, LoginValidator>();

			containerRegistry.RegisterSingleton<IAuthenticationService, MockAuthenticationService>();
		}
	}
}
