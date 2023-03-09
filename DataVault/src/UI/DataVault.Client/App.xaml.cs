namespace DataVault.Client;

using Application = Microsoft.Maui.Controls.Application;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		UserAppTheme = AppTheme.Light;
	}

	protected override Window CreateWindow(IActivationState activationState)
	{
		var window = base.CreateWindow(activationState);

		window.MinimumHeight = 650;
		window.MinimumWidth = 850;

		return window;
	}
}
