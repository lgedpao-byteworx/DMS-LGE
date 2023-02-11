namespace DataVault.Application.Interfaces.Repositories;

using Domain.Entities;

public interface IPersonRepository
{
	Task<Person> Add(Person person, CancellationToken cancellationToken);
	Task<Person> Update(Person person, CancellationToken cancellationToken);
	Task Delete(Person person, CancellationToken cancellationToken);
	Task<Person?> GetById(int id, CancellationToken cancellationToken);
	Task<bool> IsExist(string parameter, CancellationToken cancellationToken);
	Task<IEnumerable<Person>> GetAll(CancellationToken cancellationToken);

	Task<IPaginatedList<Person>> GetPagedAsync(string? parameter,
		int requestOffset,
		int requestLimit,
		CancellationToken cancellationToken);
}