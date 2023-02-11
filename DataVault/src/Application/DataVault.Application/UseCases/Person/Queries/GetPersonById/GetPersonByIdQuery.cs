namespace DataVault.Application.UseCases.Person.Queries.GetPersonById;

using Interfaces.CQRS;

public class GetPersonByIdQuery : IQuery<PersonDto>
{
	public GetPersonByIdQuery(int id)
	{
		Id = id;
	}

	public int Id { get; }
}