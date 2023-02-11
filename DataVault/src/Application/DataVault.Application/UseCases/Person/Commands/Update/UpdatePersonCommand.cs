namespace DataVault.Application.UseCases.Person.Commands.Update;

using Interfaces.CQRS;

public class UpdatePersonCommand : ICommand<PersonDto>
{
	public UpdatePersonCommand(int id)
	{
		Id = id;
	}

	public int Id { get; }
	public string Name { get; init; } = string.Empty;
}