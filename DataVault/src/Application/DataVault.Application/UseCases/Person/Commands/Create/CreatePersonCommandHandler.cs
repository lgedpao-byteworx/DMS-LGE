namespace DataVault.Application.UseCases.Person.Commands.Create;

using AutoMapper;
using Domain.Entities;
using Interfaces.CQRS;
using Interfaces.Repositories;

public class CreatePersonCommandHandler : BasePersonHandler, ICommandHandler<PersonDto, CreatePersonCommand>
{
	public CreatePersonCommandHandler(IPersonRepository personRepository, IMapper mapper) : base(personRepository, mapper)
	{
	}

	public async Task<IOperationResult<PersonDto>> Handle(CreatePersonCommand command, CancellationToken cancellationToken)
	{
		var person = Mapper.Map<Person>(command);
		var result = await PersonRepository.Add(person, cancellationToken);
		return new OperationResult<PersonDto>
		{
			Value = Mapper.Map<PersonDto>(result)
		};
	}
}