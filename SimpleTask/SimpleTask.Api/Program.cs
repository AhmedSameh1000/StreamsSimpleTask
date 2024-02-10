using Microsoft.EntityFrameworkCore;
using SimpleEcommerce.Infrastructure.RepositoryImplementation;
using SimpleTask.BAL.Services.Implementation;
using SimpleTask.BAL.Services.Interfaces;
using SimpleTask.DAL.Data;
using SimpleTask.DAL.Repositories.RepositoryImplementation;
using SimpleTask.DAL.Repositories.RepositoryInterfaces;
using SimpleTask.DAL.Seeding;
using UdemyProject.Contract.RepositoryContracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("ConStr"));
});
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
builder.Services.AddScoped<IDocumentFileRepository, DocumentFileRepository>();
builder.Services.AddScoped<IPriorityRepository, PriorityRepository>();

builder.Services.AddScoped<IFileServices, FileServices>();
var app = builder.Build();

using (var Scope = app.Services.CreateScope())
{
    var priorityRepository = Scope.ServiceProvider.GetRequiredService<IPriorityRepository>();

    await new SeedPriorities(priorityRepository).SeedPrioritiesAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();