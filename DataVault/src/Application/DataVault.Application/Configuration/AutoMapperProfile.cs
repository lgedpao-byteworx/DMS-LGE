namespace DataVault.Application.Configuration;

using System.Collections.ObjectModel;
using AutoMapper;
using Domain.Entities;
using Interfaces;
using UseCases;
using UseCases.Person;
using UseCases.Person.Commands.Create;
using UseCases.Person.Commands.Update;
using UseCases.Person.Queries.GetPerson;

public class AutoMapperProfile : Profile
{
	public AutoMapperProfile()
	{
		MapPerson();
	}

	private void MapPerson()
	{
		CreateMap<Person, PersonDto>().ReverseMap();
		CreateMap<PaginatedList<Person>, GetPersonByFilterResponse>()
			.ForMember(x => x.Items, dest => dest.MapFrom(x => x.Items.ToList()));
		CreateMap<IEnumerable<Person>, GetPersonByFilterResponse>()
			.ForMember(x => x.Items, dest => dest.MapFrom(x => x.ToList()))
			.ForMember(x => x.TotalCount, dest => dest.MapFrom(x => x.Count()));

		CreateMap<CreatePersonCommand, Person>()
			.Ignore(x => x.Id)
			.Ignore(x => x.CreatedBy)
			.Ignore(x => x.CreatedOn)
			.Ignore(x => x.ModifiedBy)
			.Ignore(x => x.ModifiedOn);
		CreateMap<UpdatePersonCommand, Person>()
			.Ignore(x => x.CreatedBy)
			.Ignore(x => x.CreatedOn)
			.Ignore(x => x.ModifiedBy)
			.Ignore(x => x.ModifiedOn);
	}
}