namespace DataVault.Client.Validation.Validators
{
	using DataVault.Client.ViewModels;
	using FluentValidation;

	public class LoginValidator : AbstractValidator<LoginViewModel>
	{
		public LoginValidator()
		{
			RuleFor(x => x.Email)
			 .NotEmpty()
			 .EmailAddress();

			RuleFor(x => x.Password)
			 .NotEmpty();
		}
	}
}