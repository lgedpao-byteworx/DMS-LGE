namespace DataVault.Application.UseCases.Person.Commands.Update;

using AutoMapper;
using Domain.Entities;
using Interfaces.CQRS;
using Interfaces.Repositories;

public class UpdatePersonCommandHandler : BasePersonHandler, ICommandHandler<PersonDto, UpdatePersonCommand>
{
	public UpdatePersonCommandHandler(IPersonRepository personRepository, IMapper mapper) : base(personRepository, mapper)
	{
	}

	public async Task<IOperationResult<PersonDto>> Handle(UpdatePersonCommand command, CancellationToken cancellationToken)
	{
		var person = await PersonRepository.GetById(command.Id, cancellationToken);
		if (person is not null)
		{
			var personToUpdate = Mapper.Map<Person>(command);
			personToUpdate.CreatedBy = person.CreatedBy;
			personToUpdate.CreatedOn = person.CreatedOn;
			var updatedClass = await PersonRepository.Update(personToUpdate, cancellationToken);
			return new OperationResult<PersonDto>
			{
				Value = Mapper.Map<PersonDto>(updatedClass)
			};
		}

		var result = new OperationResult<PersonDto>();
		result.Errors.Add("Person not found");
		return result;
	}
}