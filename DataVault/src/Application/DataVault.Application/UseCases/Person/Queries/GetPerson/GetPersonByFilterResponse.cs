namespace DataVault.Application.UseCases.Person.Queries.GetPerson;

public class GetPersonByFilterResponse
{
	public List<PersonDto> Items { get; set; } = new ();
	public int PageIndex { get; }
	public int TotalPages { get; }
	public int TotalCount { get; }
}