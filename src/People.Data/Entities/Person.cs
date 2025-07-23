namespace People.Data.Entities;

public class Person
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public DateOnly DateOfBirth { get; set; }
}