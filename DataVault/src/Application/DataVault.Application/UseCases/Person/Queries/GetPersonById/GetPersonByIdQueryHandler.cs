namespace DataVault.Application.UseCases.Person.Queries.GetPersonById;

using AutoMapper;
using Interfaces.CQRS;
using Interfaces.Repositories;

public class GetPersonByIdQueryHandler : BasePersonHandler, IQueryHandler<PersonDto, GetPersonByIdQuery>
{
	public GetPersonByIdQueryHandler(IPersonRepository personRepository, IMapper mapper) : base(personRepository, mapper)
	{
	}

	public async Task<IOperationResult<PersonDto>> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
	{
		var person = await PersonRepository.GetById(request.Id, cancellationToken);
		if (person is not null)
		{
			return new OperationResult<PersonDto>
			{
				Value = Mapper.Map<PersonDto>(person)
			};
		}

		var result = new OperationResult<PersonDto>();
		result.Errors.Add("Person not found");
		return result;
	}
}