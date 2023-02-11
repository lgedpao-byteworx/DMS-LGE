namespace DataVault.Application.UseCases.Person.Commands.Create;

using Interfaces.CQRS;

public class CreatePersonCommand : ICommand<PersonDto>
{
	public string Name { get; init; } = string.Empty;
}