namespace DataVault.Application.UseCases.Person.Queries.GetPerson;

using Interfaces.CQRS;

public class GetPersonQuery : IQuery<GetPersonByFilterResponse>
{
	public string? Name { get; set; }
	public int Limit { get; set; }
	public int Offset { get; set; }
}