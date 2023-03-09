namespace DataVault.Client;

using CommunityToolkit.Maui;
using Microsoft.Extensions.DependencyInjection;
using ViewModels;
using Views;
using Microsoft.Extensions.Logging;
using SkiaSharp.Views.Maui.Controls.Hosting;
using Material.Components.Maui.Extensions;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UsePrism(PrismStartup.Configure);
		      
		builder.UseMauiCommunityToolkit(options =>
		{
			options.SetShouldSuppressExceptionsInAnimations(true);
			options.SetShouldSuppressExceptionsInBehaviors(true);
			options.SetShouldSuppressExceptionsInConverters(true);
		});

		builder
			.UseMaterialComponents(new List<string>
			{
				"Montserrat-Regular.ttf",
				"Montserrat-Italic.ttf",
				"Montserrat-Medium.ttf",
				"Montserrat-MediumItalic.ttf",
				"Montserrat-Bold.ttf",
				"Montserrat-BoldItalic.ttf",
			});
		
		builder.UseSkiaSharp();
		builder.Services.AddMediatR(x =>
		{
			x.RegisterServicesFromAssemblies(typeof(DataVault.Application.Commands.AuthenticateUserCommand).Assembly);
		});

		var app = builder.Build();
		return app;

	}

	private static string GetDatabaseConnectionString(string filename)
	{
		return $"Filename={Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), filename)}.db";
	}
}