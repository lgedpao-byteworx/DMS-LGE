namespace DataVault.Client.ViewModels;

using System.Collections.ObjectModel;
using DataVault.Application.Interfaces.CQRS;
using DataVault.Application.UseCases.Person;
using DataVault.Application.UseCases.Person.Commands.Create;
using DataVault.Application.UseCases.Person.Commands.Delete;
using DataVault.Application.UseCases.Person.Commands.Update;
using DataVault.Application.UseCases.Person.Queries.GetPerson;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

public partial class MainViewModel : ObservableObject
{
	private readonly ICommandDispatcher commandDispatcher;
	private readonly IQueryDispatcher queryDispatcher;

	[ObservableProperty]
	private ObservableCollection<PersonDto> items = new();

	public MainViewModel(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
	{
		this.queryDispatcher = queryDispatcher;
		this.commandDispatcher = commandDispatcher;
		GetItemsCommand.Execute(null);
	}

	[RelayCommand]
	private async Task GetItems(CancellationToken cancellationToken)
	{
		var result = await queryDispatcher.SendAsync<GetPersonByFilterResponse, GetPersonQuery>(new GetPersonQuery
		{
			Limit = 25
		}, cancellationToken);
		if (result.IsSuccessful)
		{
			Items.Clear();
			foreach (var item in result.Value.Items)
			{
				Items.Add(item);
			}
		}
		else
		{
			var errors = string.Join(Environment.NewLine, result.Errors);
			await Toast.Make(errors, ToastDuration.Long).Show(cancellationToken);
		}
	}

	[RelayCommand]
	private async Task CreateItem(CancellationToken cancellationToken)
	{
		var result = await commandDispatcher.SendAsync<PersonDto, CreatePersonCommand>(new CreatePersonCommand
		{
			Name = DateTime.Now.ToString("O")
		}, cancellationToken);
		if (result.IsSuccessful)
		{
			await GetItems(cancellationToken);
		}
		else
		{
			var errors = string.Join(Environment.NewLine, result.Errors);
			await Toast.Make(errors, ToastDuration.Long).Show(cancellationToken);
		}
	}

	[RelayCommand]
	private async Task UpdateItem(int itemId, CancellationToken cancellationToken)
	{
		var result = await commandDispatcher.SendAsync<PersonDto, UpdatePersonCommand>(new UpdatePersonCommand(itemId)
		{
			Name = DateTime.Now.ToString("O")
		}, cancellationToken);
		if (result.IsSuccessful)
		{
			await GetItems(cancellationToken);
		}
		else
		{
			var errors = string.Join(Environment.NewLine, result.Errors);
			await Toast.Make(errors, ToastDuration.Long).Show(cancellationToken);
		}
	}

	[RelayCommand]
	private async Task DeleteItem(int itemId, CancellationToken cancellationToken)
	{
		var result = await commandDispatcher.SendAsync<bool, DeletePersonCommand>(new DeletePersonCommand(itemId), cancellationToken);
		if (result.IsSuccessful)
		{
			await GetItems(cancellationToken);
		}
		else
		{
			var errors = string.Join(Environment.NewLine, result.Errors);
			await Toast.Make(errors, ToastDuration.Long).Show(cancellationToken);
		}
	}
}