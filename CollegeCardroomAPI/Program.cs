using CollegeCardroomAPI.Managers;
using CollegeCardroomAPI.Repositories;
using CollegeCardroomAPI.Repositories.Interfaces;
using CollegeCardroomAPI.Managers.Interfaces;
using CollegeCardroomAPI.Hubs;

string clientUrl = "http://localhost:3000";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddScoped<IHelloRepository, HelloRepository>();
builder.Services.AddScoped<IHelloManager, HelloManager>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IUsersManager, UsersManager>();
builder.Services.AddScoped<ILobbiesRepository, LobbiesRepository>();
builder.Services.AddScoped<ILobbiesManager, LobbiesManager>();
builder.Services.AddScoped<IPokerGamesRepository, PokerGamesRepository>();
builder.Services.AddScoped<IPokerGamesManager, PokerGamesManager>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowSpecificOrigins",
        policy =>
        {
            policy.WithOrigins(clientUrl)
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowSpecificOrigins");
//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapHub<PokerRoomHub>("/hubs/pokerRoom");
app.Run();
