namespace DataVault.Client.ViewModels;

using System.ComponentModel;
using System.Windows.Input;
using DataVault.Application.Commands;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

public class LoginViewModel : BindableBase
{
	#region Fields

	private readonly IMediator mediator;
	private readonly IRegionManager regionManager;
	private readonly IValidator<LoginViewModel> validator;

	private string email;
	private string password;

	#endregion

	#region Constructor
	public LoginViewModel(IRegionManager regionManager,
						  IMediator mediator,
						  IValidator<LoginViewModel> validator)
	{
		this.regionManager = regionManager;
		this.mediator = mediator;
		this.validator = validator;

		SignInCommand = new DelegateCommand(SignIn);
	}

	#endregion

	#region Properties
	public string Email
	{
		get => email;
		set
		{
			SetProperty(ref email, value);
			RaisePropertyChanged(nameof(CanSignin));
		}
	}
	public string Password
	{
		get => password;
		set
		{
			SetProperty(ref password, value);
			RaisePropertyChanged(nameof(CanSignin));
		}
	}

	public bool CanSignin
	{
		get
		{
			var validationResult = validator.Validate(this);
			return validationResult.IsValid;
		}
	}
	public string RememberMe { get; set; } = "Remember me";

	public DelegateCommand SignInCommand { get; private set; }

	public string EmailValidationText { get; set; }

	#endregion

	#region Private methods

	private async void SignIn()
	{
		var command = new AuthenticateUserCommand
		{
			Username = Email,
			Password = Password,
		};

		var response = await mediator.Send(command);

		if (response?.Response != null)
		{
			//User is authenticated
			//Navigate to Main Page
		}
		else
		{
			//Show error message - invalid username or password
		}
	}

	#endregion
}