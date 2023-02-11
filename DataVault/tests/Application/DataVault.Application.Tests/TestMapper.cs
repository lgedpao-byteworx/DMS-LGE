namespace DataVault.Application.Tests;

using DataVault.Application.Configuration;
using AutoMapper;

internal static class TestMapper
{
	static TestMapper()
	{
		var configuration = new MapperConfiguration(cfg => cfg.AddProfile(typeof(AutoMapperProfile)));

		Instance = configuration.CreateMapper();
	}

	public static IMapper Instance { get; }
}