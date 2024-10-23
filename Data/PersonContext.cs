using Microsoft.EntityFrameworkCore;
using Pessoa.Models;

namespace Person.Data;
public class PersonContext : DbContext
{
    public DbSet<PersonModel> People{ get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=person.sqLite");
        base.OnConfiguring(optionsBuilder);
    }
}