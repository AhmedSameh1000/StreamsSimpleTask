using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SimpleEcommerce.Infrastructure.RepositoryImplementation;
using SimpleTask.BAL.Helpers;
using SimpleTask.BAL.Services.Implementation;
using SimpleTask.BAL.Services.Interfaces;
using SimpleTask.DAL.Data;
using SimpleTask.DAL.Domains;
using SimpleTask.DAL.Repositories.RepositoryImplementation;
using SimpleTask.DAL.Repositories.RepositoryInterfaces;
using SimpleTask.DAL.Seeding;
using System.Text;
using UdemyProject.Contract.RepositoryContracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("ConStr"));
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 5;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false; // Set this to true to require at least one non-alphanumeric character
    options.SignIn.RequireConfirmedEmail = true;
})
 .AddEntityFrameworkStores<ApplicationDbContext>();

var _JWT = builder.Services.BuildServiceProvider().GetRequiredService<IOptions<JWT>>().Value;
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
          .AddJwtBearer(o =>
          {
              o.RequireHttpsMetadata = false;
              o.SaveToken = false;
              o.TokenValidationParameters = new TokenValidationParameters
              {
                  ValidateIssuerSigningKey = true,
                  ValidateIssuer = true,
                  ValidateAudience = true,
                  ValidateLifetime = true,
                  ValidIssuer = _JWT.Issuer,
                  ValidAudience = _JWT.Audience,
                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_JWT.Key)),
                  ClockSkew = TimeSpan.Zero
              };
          });

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
builder.Services.AddScoped<IDocumentFileRepository, DocumentFileRepository>();
builder.Services.AddScoped<IPriorityRepository, PriorityRepository>();

builder.Services.AddScoped<IFileServices, FileServices>();
builder.Services.AddScoped<IDocumentFileService, DocumentFileService>();
builder.Services.AddScoped<IDocumentService, DocumentService>();
builder.Services.AddScoped<IAuthServices, AuthServices>();
var app = builder.Build();
app.UseStaticFiles();
using (var Scope = app.Services.CreateScope())
{
    var priorityRepository = Scope.ServiceProvider.GetRequiredService<IPriorityRepository>();
    var UserManger = Scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var RoleManger = Scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    await new SeedAdmin(RoleManger, UserManger).SeedData();

    await new SeedPriorities(priorityRepository).SeedPrioritiesAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();