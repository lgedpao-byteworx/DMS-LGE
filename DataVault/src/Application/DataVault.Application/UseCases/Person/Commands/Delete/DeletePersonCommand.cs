namespace DataVault.Application.UseCases.Person.Commands.Delete;

using Interfaces.CQRS;

public class DeletePersonCommand : ICommand<bool>
{
	public DeletePersonCommand(int personId)
	{
		PersonId = personId;
	}

	public int PersonId { get; }
}