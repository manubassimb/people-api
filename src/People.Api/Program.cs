using FluentValidation;
using Microsoft.EntityFrameworkCore;
using People.Api.Dtos;
using People.Api.Extensions;
using People.Api.Validators;
using People.Data.Context;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddValidatorsFromAssemblyContaining<CreatePersonDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdatePersonDtoValidator>();

builder.Services.AddDbContext<Context>(options =>
    options.UseInMemoryDatabase($"PeopleInMemoryDb"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Health check endpoint
app.MapGet("/health", () =>
    Results.Ok(
        new
        {
            Status = "Healthy",
            Timestamp = DateTime.UtcNow
        }))
    .WithName("Health")
    .WithOpenApi();

// 1. Add: new person endpoint
app.MapPost("/people", async (CreatePersonDto personDto, Context context, IValidator<CreatePersonDto> validator) =>
{
    var validationResult = await validator.ValidateAsync(personDto);
    if (!validationResult.IsValid)
    {
        return Results.ValidationProblem(validationResult.ToDictionary());
    }

    var person = personDto.ToEntity();
    context.People.Add(person);
    await context.SaveChangesAsync();

    return Results.Created($"/people/{person.Id}", person);
})
    .WithName("CreatePerson")
    .WithOpenApi();

// 2. Update: update person endpoint
app.MapPut("/people/{id:int}", async (int id, UpdatePersonDto personDto, Context context, IValidator<UpdatePersonDto> validator) =>
{
    var validationResult = await validator.ValidateAsync(personDto);
    if (!validationResult.IsValid)
    {
        return Results.ValidationProblem(validationResult.ToDictionary());
    }

    var person = await context.People.FindAsync(id);
    if (person == null)
    {
        return Results.NotFound(new { Message = "Not found" });
    }

    person.UpdateFromDto(personDto);
    await context.SaveChangesAsync();

    return Results.Ok(person);
})
    .WithName("UpdatePerson")
    .WithOpenApi();

// 3. Delete: delete person endpoint
app.MapDelete("/people/{id:int}", async (int id, Context context) =>
{
    var person = await context.People.FindAsync(id);
    if (person == null)
    {
        return Results.NotFound(new { Message = "Not found" });
    }

    context.People.Remove(person);
    await context.SaveChangesAsync();
    return Results.NoContent();
})
    .WithName("DeletePerson")
    .WithOpenApi();

// 4. List: get all people endpoint
app.MapGet("/people", async (Context context) =>
{
    var people = await context.People.ToListAsync();
    return Results.Ok(people.Select(p => p.ToDto()));
})
    .WithName("GetAllPeople")
    .WithOpenApi();

// Redirect root URL to Swagger UI
app.MapGet("/", () => Results.Redirect("/swagger"));

app.Run();

public partial class Program { }