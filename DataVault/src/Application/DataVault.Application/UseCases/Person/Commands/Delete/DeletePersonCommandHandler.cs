namespace DataVault.Application.UseCases.Person.Commands.Delete;

using AutoMapper;
using Interfaces.CQRS;
using Interfaces.Repositories;

public class DeletePersonCommandHandler : BasePersonHandler, ICommandHandler<bool, DeletePersonCommand>
{
	public DeletePersonCommandHandler(IPersonRepository personRepository, IMapper mapper) : base(personRepository, mapper)
	{
	}

	public async Task<IOperationResult<bool>> Handle(DeletePersonCommand command, CancellationToken cancellationToken)
	{
		var person = await PersonRepository.GetById(command.PersonId, cancellationToken);
		if (person is not null)
		{
			await PersonRepository.Delete(person, cancellationToken);
			return new OperationResult<bool>
			{
				Value = true
			};
		}

		var result = new OperationResult<bool>();
		result.Errors.Add("Person not found");
		return result;
	}
}