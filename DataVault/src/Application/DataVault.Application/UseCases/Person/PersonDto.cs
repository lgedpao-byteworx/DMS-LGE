namespace DataVault.Application.UseCases.Person;

public class PersonDto
{
	public int Id { get; set; }

	public string CreatedBy { get; set; } = string.Empty;

	public DateTime CreatedOn { get; set; }

	public string? ModifiedBy { get; set; }

	public DateTime? ModifiedOn { get; set; }
	public string Name { get; set; } = null!;
}