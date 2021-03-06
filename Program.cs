using System.Text.Json.Serialization;
using GraphQL;
using GraphQL.MicrosoftDI;
using Microsoft.EntityFrameworkCore;
using MovieTracker;
using MovieTracker.Extensions;
using MovieTracker.GQL.Schemas;
using MovieTracker.Services;
using MovieTracker.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("MovieTrackerDatabase");

//DB Config
builder.Services.AddScoped<MovieTrackerContext>()
    .AddDbContext<MovieTrackerContext>(options => options
        .UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddScoped<IActorRepository, ActorRepository>();
builder.Services
    .AddScoped<IEpisodeRepository, EpisodeRepository>();
builder.Services
    .AddScoped<IMovieRepository, MovieRepository>();
builder.Services
    .AddScoped<IRoleRepository, RoleRepository>();
builder.Services
    .AddScoped<ISeriesRepository, SeriesRepository>();

builder.Services.AddScoped<IServiceProvider>(provider => 
    new FuncServiceProvider(provider.GetRequiredService));

builder.Services.AddScoped<CoreSchema>();

builder.Services.AddGraphQL(gqlBuilder => gqlBuilder.BuildGqlService<CoreSchema>(builder.Environment.IsDevelopment()));

var app = builder.Build();

//GQL settings
app.UseRouting();

app.UseWebSockets();

app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseGraphQL<CoreSchema>();

app.UseGraphQLAltair();

app.MapControllers();

app.Run();