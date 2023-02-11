namespace DataVault.Application.UseCases.Person.Queries.GetPerson;

using AutoMapper;
using Interfaces.CQRS;
using Interfaces.Repositories;

public class GetPersonQueryHandler : BasePersonHandler, IQueryHandler<GetPersonByFilterResponse, GetPersonQuery>
{
	public GetPersonQueryHandler(IPersonRepository personRepository, IMapper mapper) : base(personRepository, mapper)
	{
	}

	public async Task<IOperationResult<GetPersonByFilterResponse>> Handle(GetPersonQuery request, CancellationToken cancellationToken)
	{
		var result = await PersonRepository.GetPagedAsync(request.Name, request.Offset, request.Limit, cancellationToken);
		return new OperationResult<GetPersonByFilterResponse>
		{
			Value = Mapper.Map<GetPersonByFilterResponse>(result)
		};
	}
}