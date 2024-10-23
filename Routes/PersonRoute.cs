namespace Pessoa.Routes;

using Microsoft.EntityFrameworkCore;
using Person.Data;
using Pessoa.Models;

public static class PersonRoute 
{
    public static void PersonRoutes(this WebApplication app) 
    {
        var route = app.MapGroup("person");
        
        route.MapPost("", 
            async (PersonRequest req, PersonContext context) => 
            { 
                var person = new PersonModel(req.name);
                await context.AddAsync(person);
                await context.SaveChangesAsync();
            });
        
        route.MapGet("", async (PersonContext context) => 
        {
            List<PersonModel> people = await context.People.ToListAsync();
            return Results.Ok(people);
        });

        route.MapPut("{id:guid}", async (Guid id, PersonRequest req, PersonContext context) => 
        {
            var person = await context.People.SingleOrDefaultAsync(x => x.Id == id);
            if (person == null) return Results.NotFound();
            person.ChangeName(req.name);
            await context.SaveChangesAsync();
            return Results.Ok(person);
        });

        route.MapDelete("{id:guid}", async (Guid id, PersonContext context) => 
        {
            var person = context.People.SingleOrDefault(x => x.Id == id);
            if (person == null) return Results.NotFound();
            context.Remove(person);
            await context.SaveChangesAsync();
            return Results.Ok(person);
        });
    }
}