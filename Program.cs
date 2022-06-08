using GraphQL;
using GraphQL.DataLoader;
using GraphQL.MicrosoftDI;
using GraphQL.Server;
using GraphQL.SystemTextJson;
using Microsoft.EntityFrameworkCore;
using MovieTracker;
using MovieTracker.GQL.Schemas;
using MovieTracker.Services;
using MovieTracker.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("MovieTrackerDatabase");

//DB Config
builder.Services.AddScoped<MovieTrackerContext>()
    .AddDbContext<MovieTrackerContext>(options => 
    options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddScoped<IActorService, ActorService>();
builder.Services
    .AddScoped<IEpisodeService, EpisodeService>();
builder.Services
    .AddScoped<IMovieService, MovieService>();
builder.Services
    .AddScoped<IRoleService, RoleService>();
builder.Services
    .AddScoped<ISeriesService, SeriesService>();

builder.Services.AddScoped<IServiceProvider>(provider => 
    new FuncServiceProvider(provider.GetRequiredService));

builder.Services.AddScoped<ActorSchema>();
builder.Services.AddScoped<EpisodeSchema>();
builder.Services.AddScoped<MovieSchema>();
builder.Services.AddScoped<RoleSchema>();
builder.Services.AddScoped<SeriesSchema>();

builder.Services.AddGraphQL(gqlBuilder => gqlBuilder
    .ConfigureExecutionOptions(options =>
    {
        options.EnableMetrics = builder.Environment.IsDevelopment();
        var logger = options.RequestServices?.GetRequiredService<ILogger<Program>>();
        options.UnhandledExceptionDelegate = ctx =>
        {
            logger?.LogError("{Error} occurred", ctx.OriginalException.Message);
            return Task.CompletedTask;
        };
    })
    .AddHttpMiddleware<ActorSchema>()
    .AddHttpMiddleware<EpisodeSchema>()
    .AddHttpMiddleware<MovieSchema>()
    .AddHttpMiddleware<RoleSchema>()
    .AddHttpMiddleware<SeriesSchema>()
    .AddDefaultEndpointSelectorPolicy()
    .AddSystemTextJson()
    .AddErrorInfoProvider(opt => 
        opt.ExposeExceptionStackTrace = builder.Environment.IsDevelopment())
    .AddWebSockets()
    .AddDataLoader()
    .AddGraphTypes()
);

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

app.UseGraphQL<ActorSchema>();
app.UseGraphQL<EpisodeSchema>();
app.UseGraphQL<MovieSchema>();
app.UseGraphQL<RoleSchema>();
app.UseGraphQL<SeriesSchema>();

app.UseGraphQLAltair();

app.MapControllers();

app.Run();