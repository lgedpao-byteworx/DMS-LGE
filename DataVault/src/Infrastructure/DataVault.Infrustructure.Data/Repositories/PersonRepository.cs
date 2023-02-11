namespace DataVault.Infrastructure.Data.Repositories;

using DataVault.Application.Interfaces;
using DataVault.Application.Interfaces.Repositories;
using DataVault.Application.UseCases;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Models;
using DomainPerson = Domain.Entities.Person;

public class PersonRepository : BaseRepository, IPersonRepository
{
	private readonly IDbContextFactory<ApplicationContext> factory;
	private readonly IMapper mapper;

	public PersonRepository(IDbContextFactory<ApplicationContext> factory, IMapper mapper)
	{
		this.factory = factory;
		this.mapper = mapper;
	}

	public async Task<DomainPerson> Add(DomainPerson person, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		var newPerson = mapper.Map<Person>(person);
		await using var context = await factory.CreateDbContextAsync(cancellationToken);
		await context.Person.AddAsync(newPerson, cancellationToken);
		await SaveChangesAsync(context, cancellationToken);
		return mapper.Map<DomainPerson>(newPerson);
	}

	public async Task<DomainPerson> Update(DomainPerson person, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		var personToUpdate = mapper.Map<Person>(person);
		await using var context = await factory.CreateDbContextAsync(cancellationToken);
		context.Person.Update(personToUpdate);
		await SaveChangesAsync(context, cancellationToken);
		return mapper.Map<DomainPerson>(personToUpdate);
	}

	public async Task Delete(DomainPerson person, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		var personToRemove = mapper.Map<Person>(person);
		await using var context = await factory.CreateDbContextAsync(cancellationToken);
		context.Person.Remove(personToRemove);
		await SaveChangesAsync(context, cancellationToken);
	}

	public async Task<DomainPerson?> GetById(int id, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		await using var context = await factory.CreateDbContextAsync(cancellationToken);
		var person = await context.Person.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
		return mapper.Map<DomainPerson>(person);
	}

	public async Task<bool> IsExist(string parameter, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		await using var context = await factory.CreateDbContextAsync(cancellationToken);
		return await context.Person.AnyAsync(x => x.Name == parameter, cancellationToken);
	}

	public async Task<IEnumerable<DomainPerson>> GetAll(CancellationToken cancellationToken)
	{
		await using var context = await factory.CreateDbContextAsync(cancellationToken);
		var person = context.Person.AsNoTracking().ToListAsync(cancellationToken);
		return mapper.Map<IEnumerable<DomainPerson>>(person);
	}

	public async Task<IPaginatedList<DomainPerson>> GetPagedAsync(string? requestName,
		int requestOffset,
		int requestLimit,
		CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		await using var context = await factory.CreateDbContextAsync(cancellationToken);
		var totalCount = await context.Person.AsNoTracking()
									  .CountAsync(x => x.Name.Contains(requestName ?? string.Empty), cancellationToken);

		var result = await context.Person.AsNoTracking()
								  .Where(x => x.Name.Contains(requestName ?? string.Empty))
								  .OrderBy(q => q.Id)
								  .Skip(requestOffset)
								  .Take(requestLimit)
								  .ToListAsync(cancellationToken);

		return new PaginatedList<DomainPerson>(mapper.Map<List<DomainPerson>>(result), totalCount, requestOffset,
											   requestLimit);
	}

	public async Task<DomainPerson?> GetByName(string name, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		await using var context = await factory.CreateDbContextAsync(cancellationToken);
		var person = await context.Person.AsNoTracking()
								  .SingleOrDefaultAsync(x => x.Name.Contains(name), cancellationToken);
		return mapper.Map<DomainPerson>(person);
	}
}