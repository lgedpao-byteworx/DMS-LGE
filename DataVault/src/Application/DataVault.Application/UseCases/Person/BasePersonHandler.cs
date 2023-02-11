namespace DataVault.Application.UseCases.Person;

using AutoMapper;
using Interfaces.Repositories;

public abstract class BasePersonHandler
{
	protected readonly IPersonRepository PersonRepository;
	protected readonly IMapper Mapper;

	protected BasePersonHandler(IPersonRepository personRepository, IMapper mapper)
	{
		PersonRepository = personRepository;
		Mapper = mapper;
	}
}