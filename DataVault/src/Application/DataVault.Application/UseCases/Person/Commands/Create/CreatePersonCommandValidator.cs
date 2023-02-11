namespace DataVault.Application.UseCases.Person.Commands.Create;

using FluentValidation;
using Interfaces.Repositories;

public class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
{
	private readonly IPersonRepository personRepository;

	public CreatePersonCommandValidator(IPersonRepository personRepository)
	{
		this.personRepository = personRepository;

		ConfigureValidation();
	}

	private void ConfigureValidation()
	{
		RuleFor(x => x.Name)
			.NotEmpty()
			.MustAsync(async (command, name, ctx, cancellationToken) =>
			{
				var isExist = await personRepository.IsExist(name, cancellationToken);

				if (!isExist)
				{
					return true;
				}

				ctx.AddFailure(nameof(command.Name), $"Person with Name:'{command.Name}' already exist");
				return false;
			});
	}
}