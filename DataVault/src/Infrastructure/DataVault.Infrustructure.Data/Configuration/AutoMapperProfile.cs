namespace DataVault.Infrastructure.Data.Configuration;

using AutoMapper;
using Repositories.Models;

public class AutoMapperProfile : Profile
{
	public AutoMapperProfile()
	{
		SetupBanner();
	}

	private void SetupBanner()
	{
		CreateMap<Person, Domain.Entities.Person>().ReverseMap();
	}
}