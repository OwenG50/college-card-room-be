using CollegeCardroomAPI.Managers;
using CollegeCardroomAPI.Repositories;
using CollegeCardroomAPI.Repositories.Interfaces;
using CollegeCardroomAPI.Managers.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<IHelloRepository, HelloRepository>();
builder.Services.AddScoped<IHelloManager, HelloManager>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IUsersManager, UsersManager>();
builder.Services.AddScoped<ILobbiesRepository, LobbiesRepository>();
builder.Services.AddScoped<ILobbiesManager, LobbiesManager>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Optional CORS policy, if calling from React
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");
//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
