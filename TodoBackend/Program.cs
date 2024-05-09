using TodoBackend.Models.Data;
using TodoBackend.Repo.Implementation;
using TodoBackend.Repo.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// DEPENDENCY INJECTION FOR CONNECTION TO DB

builder.Services.AddTransient<AppDbContext>();

builder.Services.AddTransient<PersonsRepoInterface, PersonRepo>();

builder.Services.AddTransient<TaskRepoInterface, TaskRepo>();

// CORS for us to connect to the other server or frontend

builder.Services.AddCors(options => options.AddPolicy(name: "enable",
    policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
    }
    ));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// adding the CORS
app.UseCors("enable");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
